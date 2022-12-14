using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Settings;

using GameOverlay.Drawing;
using GameOverlay.Windows;

namespace GTA5OnlineTools.Features.SDK;

public class Overlay : IDisposable
{
    private readonly GraphicsWindow _window;

    private readonly Dictionary<string, SolidBrush> _brushes;
    private readonly Dictionary<string, Font> _fonts;

    private WindowData windowData;

    // 视角宽和视角高
    private float gview_width;
    private float gview_height;

    private bool isDraw = true;

    private bool isRun = true;

    public Overlay()
    {
        windowData = Memory.GetGameWindowData();
        Memory.SetForegroundWindow();

        /////////////////////////////////////////////

        _brushes = new Dictionary<string, SolidBrush>();
        _fonts = new Dictionary<string, Font>();

        var gfx = new Graphics()
        {
            VSync = MenuSetting.Overlay.VSync,
            MeasureFPS = true,
            PerPrimitiveAntiAliasing = true,
            TextAntiAliasing = true,
            UseMultiThreadedFactories = true
        };

        _window = new GraphicsWindow(windowData.Left, windowData.Top, windowData.Width, windowData.Height, gfx)
        {
            FPS = MenuSetting.Overlay.FPS,
            IsTopmost = true,
            IsVisible = true
        };

        _window.DestroyGraphics += _window_DestroyGraphics;
        _window.DrawGraphics += _window_DrawGraphics;
        _window.SetupGraphics += _window_SetupGraphics;

        new Thread(AimbotThread)
        {
            Name = "AimbotThread",
            IsBackground = true
        }.Start();

        new Thread(IsDrawGameOverlay)
        {
            Name = "IsDrawGameOverlay",
            IsBackground = true
        }.Start();
    }

    private void IsDrawGameOverlay()
    {
        while (isRun)
        {
            if (MenuSetting.Overlay.NoTOPMostHide)
                isDraw = Memory.IsForegroundWindow();
            else
                isDraw = true;

            Thread.Sleep(1000);
        }
    }

    private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
    {
        var gfx = e.Graphics;

        if (e.RecreateResources)
        {
            foreach (var pair in _brushes) pair.Value.Dispose();
        }

        _brushes["black"] = gfx.CreateSolidBrush(0, 0, 0);
        _brushes["white"] = gfx.CreateSolidBrush(255, 255, 255);
        _brushes["red"] = gfx.CreateSolidBrush(255, 0, 98);
        _brushes["green"] = gfx.CreateSolidBrush(0, 128, 0);
        _brushes["blue"] = gfx.CreateSolidBrush(30, 144, 255);
        _brushes["bgcolor"] = gfx.CreateSolidBrush(11, 11, 11, 150);
        _brushes["grid"] = gfx.CreateSolidBrush(255, 255, 255, 0.2f);
        _brushes["deepPink"] = gfx.CreateSolidBrush(247, 63, 147, 255);

        _brushes["transparency"] = gfx.CreateSolidBrush(0, 0, 0, 0);

        if (e.RecreateResources) return;

        _fonts["Microsoft YaHei"] = gfx.CreateFont("Microsoft YaHei", 12);
    }

    private void _window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
    {
        foreach (var pair in _brushes) pair.Value.Dispose();
        foreach (var pair in _fonts) pair.Value.Dispose();
    }

    private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
    {
        var gfx = e.Graphics;
        gfx.ClearScene(_brushes["transparency"]);

        if (isDraw)
        {
            ResizeWindow(gfx);

            // 绘制帧数
            gfx.DrawText(_fonts["Microsoft YaHei"], 12, _brushes["green"], 10, _window.Height / 3, $"FPS：{gfx.FPS}");

            ///////////////////////////////////////////////////////
            //                 用户自定义绘制区域                   //
            ///////////////////////////////////////////////////////

            // 视角宽和视角高
            gview_width = windowData.Width / 2;
            gview_height = windowData.Height / 2;

            long m_ped_factory = Memory.Read<long>(Pointers.WorldPTR);
            long m_local_ped = Memory.Read<long>(m_ped_factory + 0x08);

            // 自己坐标
            long pCNavigation = Memory.Read<long>(m_local_ped + 0x30);
            Vector3 myPosV3 = Memory.Read<Vector3>(pCNavigation + 0x50);

            /////////////////////////////////////////////////////////////////////

            // 玩家列表
            long pCNetworkPlayerMgr = Memory.Read<long>(Pointers.NetworkPlayerMgrPTR);
            int playerCount = Memory.Read<int>(pCNetworkPlayerMgr + 0x178);

            // Ped数量
            long m_replay = Memory.Read<long>(Pointers.ReplayInterfacePTR);
            long m_ped_interface = Memory.Read<long>(m_replay + 0x18);
            int m_max_peds = Memory.Read<int>(m_ped_interface + 0x108);
            int m_cur_peds = Memory.Read<int>(m_ped_interface + 0x110);

            gfx.DrawText(_fonts["Microsoft YaHei"], 12, _brushes["blue"], 10, _window.Height / 3 + 30,
                $"GTA5线上小助手\n\nX: {myPosV3.X:0.0000}\nY: {myPosV3.Y:0.0000}\nZ: {myPosV3.Z:0.0000}\n\n" +
                $"玩家数量: {playerCount}\nPed数量: {m_cur_peds}");

            long pAimingPedPTR = Memory.Read<long>(Pointers.AimingPedPTR);
            bool isAimPed = Memory.Read<long>(pAimingPedPTR + 0x280) > 0;

            if (MenuSetting.Overlay.ESP_Crosshair)
            {
                // 当玩家按住右键准心对准敌人，准心变成粉红色，否则为绿色
                if (isAimPed && Convert.ToBoolean(Win32.GetAsyncKeyState((int)WinVK.RBUTTON) & Win32.KEY_PRESSED))
                    DrawCrosshair(gfx, _brushes["deepPink"], 7.0f, 1.5f);
                else
                    DrawCrosshair(gfx, _brushes["green"], 7.0f, 1.5f);
            }

            ///////////////////////////////////////////////////////

            for (int i = 0; i < m_max_peds; i++)
            {
                long m_ped_list = Memory.Read<long>(m_ped_interface + 0x100);
                m_ped_list = Memory.Read<long>(m_ped_list + i * 0x10);
                if (!Memory.IsValid(m_ped_list))
                    continue;

                // 如果是自己，跳过
                if (m_local_ped == m_ped_list)
                    continue;

                // 如果ped死亡，跳过
                float ped_Health = Memory.Read<float>(m_ped_list + 0x280);
                if (ped_Health <= 0)
                    continue;

                float ped_MaxHealth = Memory.Read<float>(m_ped_list + 0x284);
                float ped_HPPercentage = ped_Health / ped_MaxHealth;

                long m_player_info = Memory.Read<long>(m_ped_list + 0x10A8);
                //if (!Memory.IsValid(m_player_info))
                //    continue;

                string pedName = Memory.ReadString(m_player_info + 0xA4, 20);

                // 绘制玩家
                if (!MenuSetting.Overlay.ESP_Player)
                    if (pedName != "")
                        continue;

                // 绘制Ped
                if (!MenuSetting.Overlay.ESP_NPC)
                    if (pedName == "")
                        continue;

                long m_navigation = Memory.Read<long>(m_ped_list + 0x30);
                if (!Memory.IsValid(m_navigation))
                    continue;

                // ped坐标
                var pedPosV3 = Memory.Read<Vector3>(m_navigation + 0x50);

                // Ped与自己的距离
                float myToPedDistance = (float)Math.Sqrt(
                    Math.Pow(myPosV3.X - pedPosV3.X, 2) +
                    Math.Pow(myPosV3.Y - pedPosV3.Y, 2) +
                    Math.Pow(myPosV3.Z - pedPosV3.Z, 2));

                // m_heading    0x20
                // m_heading2   0x24
                var v2PedSinCos = new Vector2
                {
                    X = Memory.Read<float>(m_navigation + 0x20),
                    Y = Memory.Read<float>(m_navigation + 0x30)
                };

                if (MenuSetting.Overlay.ESP_3DBox)
                {
                    if (pedName != "")
                    {
                        // 玩家 3DBox
                        DrawAABBBox(gfx, _brushes["red"], pedPosV3, v2PedSinCos);
                    }
                    else
                    {
                        // Ped 3DBox
                        DrawAABBBox(gfx, _brushes["white"], pedPosV3, v2PedSinCos);
                    }
                }

                Vector2 pedPosV2 = WorldToScreen(pedPosV3);
                Vector2 pedBoxV2 = GetBoxWH(pedPosV3);

                if (!IsNullVector2(pedPosV2))
                {
                    //int ped_type = Memory.Read<int>(m_ped_list + 0x10B8);
                    //ped_type = ped_type << 11 >> 25;
                    //Draw2DNameText(gfx, _brushes["white"], pedPosV2, pedBoxV2, ped_type.ToString(), myToPedDistance);

                    if (pedName != "")
                    {
                        if (MenuSetting.Overlay.ESP_2DBox)
                        {
                            // 2D方框
                            Draw2DBox(gfx, _brushes["red"], pedPosV2, pedBoxV2, 0.7f);
                        }

                        if (MenuSetting.Overlay.ESP_2DLine)
                        {
                            if (MenuSetting.Overlay.ESP_2DBox)
                            {
                                // 2DBox射线
                                Draw2DLine(gfx, _brushes["red"], pedPosV2, pedBoxV2, 0.7f);
                            }
                            else
                            {
                                // 3DBox射线
                                DrawAABBLine(gfx, _brushes["red"], pedPosV3, 0.7f);
                            }
                        }

                        if (MenuSetting.Overlay.ESP_2DHealthBar)
                        {
                            if (MenuSetting.Overlay.ESP_2DBox)
                            {
                                // 2DBox血条
                                Draw2DHealthBar(gfx, _brushes["green"], pedPosV2, pedBoxV2, ped_HPPercentage, 0.7f);
                            }
                            else
                            {
                                // 3DBox血条
                                Draw3DHealthBar(gfx, _brushes["green"], pedPosV2, pedBoxV2, ped_HPPercentage, 0.7f);
                            }
                        }

                        if (MenuSetting.Overlay.ESP_HealthText)
                        {
                            if (MenuSetting.Overlay.ESP_2DBox)
                            {
                                // 2DBox血量数字
                                Draw2DHealthText(gfx, _brushes["red"], pedPosV2, pedBoxV2, ped_Health, ped_MaxHealth, i);
                            }
                            else
                            {
                                // 3DBox血量数字
                                Draw3DHealthText(gfx, _brushes["red"], pedPosV2, pedBoxV2, ped_Health, ped_MaxHealth, i);
                            }
                        }

                        if (MenuSetting.Overlay.ESP_NameText)
                        {
                            if (MenuSetting.Overlay.ESP_2DBox)
                            {
                                // 2DBox玩家名称
                                Draw2DNameText(gfx, _brushes["red"], pedPosV2, pedBoxV2, pedName, myToPedDistance);
                            }
                            else
                            {
                                // 3DBox玩家名称
                                Draw3DNameText(gfx, _brushes["red"], pedPosV2, pedBoxV2, pedName, myToPedDistance);
                            }
                        }
                    }
                    else
                    {
                        if (MenuSetting.Overlay.ESP_2DBox)
                        {
                            // 2D方框
                            Draw2DBox(gfx, _brushes["white"], pedPosV2, pedBoxV2, 0.7f);
                        }

                        if (MenuSetting.Overlay.ESP_2DLine)
                        {
                            if (MenuSetting.Overlay.ESP_2DBox)
                            {
                                // 2DBox射线
                                Draw2DLine(gfx, _brushes["white"], pedPosV2, pedBoxV2, 0.7f);
                            }
                            else
                            {
                                // 3DBox射线
                                DrawAABBLine(gfx, _brushes["white"], pedPosV3, 0.7f);
                            }
                        }

                        if (MenuSetting.Overlay.ESP_2DHealthBar)
                        {
                            if (MenuSetting.Overlay.ESP_2DBox)
                            {
                                // 2DBox血条
                                Draw2DHealthBar(gfx, _brushes["green"], pedPosV2, pedBoxV2, ped_HPPercentage, 0.7f);
                            }
                            else
                            {
                                // 3DBox血条
                                Draw3DHealthBar(gfx, _brushes["green"], pedPosV2, pedBoxV2, ped_HPPercentage, 0.7f);
                            }
                        }

                        if (MenuSetting.Overlay.ESP_HealthText)
                        {
                            if (MenuSetting.Overlay.ESP_2DBox)
                            {
                                // 2DBox血量数字
                                Draw2DHealthText(gfx, _brushes["white"], pedPosV2, pedBoxV2, ped_Health, ped_MaxHealth, i);
                            }
                            else
                            {
                                // 3DBox血量数字
                                Draw3DHealthText(gfx, _brushes["white"], pedPosV2, pedBoxV2, ped_Health, ped_MaxHealth, i);
                            }
                        }

                        if (MenuSetting.Overlay.ESP_NameText)
                        {
                            if (MenuSetting.Overlay.ESP_2DBox)
                            {
                                // 2DBox玩家名称
                                Draw2DNameText(gfx, _brushes["white"], pedPosV2, pedBoxV2, pedName, myToPedDistance);
                            }
                            else
                            {
                                // 3DBox玩家名称
                                Draw3DNameText(gfx, _brushes["white"], pedPosV2, pedBoxV2, pedName, myToPedDistance);
                            }
                        }
                    }

                    //int pedEntityType = Memory.Read<int>(ped_offset_0 + 0x10B8);
                    //byte pedEntityType = Memory.Read<byte>(ped_offset_0 + 0x2B);
                    //byte oHostility = Memory.Read<byte>(ped_offset_0 + 0x18C);

                    //pedEntityType = pedEntityType >> 14 & 0x1F;

                    //gfx.DrawText(_fonts["Microsoft YaHei"], 10, _brushes["red"],
                    //    pedPosV2.X, pedPosV2.Y,
                    //    $"Type : {pedEntityType}");
                }

                if (MenuSetting.Overlay.ESP_Bone)
                {
                    // 骨骼
                    DrawBone(gfx, m_ped_list, 0, 7);
                    DrawBone(gfx, m_ped_list, 7, 8);
                    DrawBone(gfx, m_ped_list, 8, 3);
                    DrawBone(gfx, m_ped_list, 8, 4);
                    DrawBone(gfx, m_ped_list, 7, 5);
                    DrawBone(gfx, m_ped_list, 7, 6);
                }
            }

        }
    }

    /// <summary>
    /// 窗口移动跟随
    /// </summary>
    /// <param name="gfx"></param>
    private void ResizeWindow(Graphics gfx)
    {
        windowData = Memory.GetGameWindowData();

        if (_window.X != windowData.Left)
        {
            _window.X = windowData.Left;
            _window.Y = windowData.Top;
            _window.Width = windowData.Width;
            _window.Height = windowData.Height;
            gfx.Resize(_window.Width, _window.Height);
        }
    }

    private void AimbotThread()
    {
        while (isRun)
        {
            if (MenuSetting.Overlay.AimBot_Enabled)
            {
                float aimBot_Min_Distance = MenuSetting.Overlay.AimBot_Fov;
                Vector3 aimBot_ViewAngles = new() { X = 0, Y = 0, Z = 0 };
                Vector3 teleW_pedCoords = new() { X = 0, Y = 0, Z = 0 };

                long pCPedFactory = Memory.Read<long>(Pointers.WorldPTR);
                long pCPed = Memory.Read<long>(pCPedFactory + Offsets.CPed);
                byte oInVehicle = Memory.Read<byte>(pCPed + Offsets.CPed_InVehicle);
                long pCPlayerInfo = Memory.Read<long>(pCPed + Offsets.CPed_CPlayerInfo);

                // 玩家自己RID
                long myRID = Memory.Read<long>(pCPlayerInfo + Offsets.CPed_CPlayerInfo_RockstarID);

                // 相机坐标
                long pCCameraPTR = Memory.Read<long>(Pointers.CCameraPTR);
                long pCCameraPTR_0 = Memory.Read<long>(pCCameraPTR + 0x00);
                pCCameraPTR_0 = Memory.Read<long>(pCCameraPTR_0 + 0x3C0);
                Vector3 cameraV3Pos = Memory.Read<Vector3>(pCCameraPTR_0 + 0x60);

                // 是否是第一人称，当Fov=0为第一人称或者开镜状态，第三人称50
                long offset = Memory.Read<long>(pCCameraPTR_0 + 0x10);
                float isFPP = Memory.Read<float>(offset + 0x30);

                // Ped实体
                long pReplayInterfacePTR = Memory.Read<long>(Pointers.ReplayInterfacePTR);
                long my_offset_0x18 = Memory.Read<long>(pReplayInterfacePTR + 0x18);

                for (int i = 0; i < 128; i++)
                {
                    long ped_offset_0 = Memory.Read<long>(my_offset_0x18 + 0x100);
                    ped_offset_0 = Memory.Read<long>(ped_offset_0 + i * 0x10);
                    if (ped_offset_0 == 0)
                    {
                        continue;
                    }

                    float pedHealth = Memory.Read<float>(ped_offset_0 + 0x280);
                    if (pedHealth <= 0)
                    {
                        continue;
                    }

                    long ped_offset_1 = Memory.Read<long>(ped_offset_0 + 0x10A8);
                    long pedRID = Memory.Read<long>(ped_offset_1 + 0x90);
                    if (myRID == pedRID)
                    {
                        continue;
                    }

                    string pedName = Memory.ReadString(ped_offset_1 + 0xA4, 20);

                    // 绘制玩家
                    if (!MenuSetting.Overlay.ESP_Player)
                    {
                        if (pedName != "")
                        {
                            continue;
                        }
                    }

                    // 绘制Ped
                    if (!MenuSetting.Overlay.ESP_NPC)
                    {
                        if (pedName == "")
                        {
                            continue;
                        }
                    }

                    Vector3 pedV3Pos = Memory.Read<Vector3>(ped_offset_0 + 0x90);
                    var pedV2Pos = WorldToScreen(pedV3Pos);

                    // 自瞄数据
                    float aimBot_Distance = (float)Math.Sqrt(Math.Pow(pedV2Pos.X - gview_width, 2) + Math.Pow(pedV2Pos.Y - gview_height, 2));
                    // 获取距离准心最近的方框
                    if (aimBot_Distance < aimBot_Min_Distance)
                    {
                        aimBot_Min_Distance = aimBot_Distance;
                        aimBot_ViewAngles = GetCCameraViewAngles(cameraV3Pos, GetBonePosition(ped_offset_0, MenuSetting.Overlay.AimBot_BoneIndex));
                        teleW_pedCoords = pedV3Pos;
                    }
                }

                // 玩家处于载具或者掩护状态中不启用自瞄，无目标取消自瞄
                if (oInVehicle != 0x01 && aimBot_Min_Distance != MenuSetting.Overlay.AimBot_Fov)
                {
                    // 默认按住Ctrl键自瞄
                    if (Convert.ToBoolean(Win32.GetAsyncKeyState((int)MenuSetting.Overlay.AimBot_Key) & Win32.KEY_PRESSED))
                    {
                        if (isFPP == 0)
                        {
                            // 第一人称及开镜自瞄
                            Memory.Write(pCCameraPTR_0 + 0x40, aimBot_ViewAngles);
                        }
                        else
                        {
                            // 第三人称及自瞄
                            Memory.Write(pCCameraPTR_0 + 0x3D0, aimBot_ViewAngles);
                        }

                        if (Convert.ToBoolean(Win32.GetAsyncKeyState((int)WinVK.F5) & Win32.KEY_PRESSED))
                        {
                            Teleport.SetTeleportPosition(teleW_pedCoords);
                        }
                    }
                }
            }

            Thread.Sleep(1);
        }
    }

    /// <summary>
    /// 绘制十字准星
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="brush"></param>
    /// <param name="length"></param>
    /// <param name="stroke"></param>
    private void DrawCrosshair(Graphics gfx, IBrush brush, float length, float stroke)
    {
        gfx.DrawLine(brush,
            gview_width - length,
            gview_height,
            gview_width + length,
            gview_height, stroke);
        gfx.DrawLine(brush,
            gview_width,
            gview_height - length,
            gview_width,
            gview_height + length, stroke);

        //gfx.DrawCircle(brush, gview_width, gview_height, gview_height / 4, stroke);
    }

    /// <summary>
    /// 2D方框
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="brush"></param>
    /// <param name="screenV2"></param>
    /// <param name="boxV2"></param>
    /// <param name="stroke"></param>
    private static void Draw2DBox(Graphics gfx, IBrush brush, Vector2 screenV2, Vector2 boxV2, float stroke)
    {
        gfx.DrawRectangle(brush, Rectangle.Create(
            screenV2.X - boxV2.X / 2,
            screenV2.Y - boxV2.Y / 2,
            boxV2.X,
            boxV2.Y), stroke);
    }

    /// <summary>
    /// 2D射线
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="brush"></param>
    /// <param name="screenV2"></param>
    /// <param name="boxV2"></param>
    /// <param name="stroke"></param>
    private void Draw2DLine(Graphics gfx, IBrush brush, Vector2 screenV2, Vector2 boxV2, float stroke)
    {
        gfx.DrawLine(brush,
            windowData.Width / 2,
            0,
            screenV2.X,
            screenV2.Y - boxV2.Y / 2, stroke);
    }

    /// <summary>
    /// 2DBox血条
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="brush"></param>
    /// <param name="screenV2"></param>
    /// <param name="boxV2"></param>
    /// <param name="pedHPPercentage"></param>
    /// <param name="stroke"></param>
    private void Draw2DHealthBar(Graphics gfx, IBrush brush, Vector2 screenV2, Vector2 boxV2, float pedHPPercentage, float stroke)
    {
        gfx.DrawRectangle(_brushes["white"], Rectangle.Create(
            screenV2.X - boxV2.X / 2 - boxV2.X / 8,
            screenV2.Y + boxV2.Y / 2,
            boxV2.X / 10,
            boxV2.Y * -1.0f), stroke);
        gfx.FillRectangle(brush, Rectangle.Create(
            screenV2.X - boxV2.X / 2 - boxV2.X / 8,
            screenV2.Y + boxV2.Y / 2,
            boxV2.X / 10,
            boxV2.Y * pedHPPercentage * -1.0f));
    }

    /// <summary>
    /// 3DBox血条
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="brush"></param>
    /// <param name="screenV2"></param>
    /// <param name="boxV2"></param>
    /// <param name="hpPer"></param>
    /// <param name="stroke"></param>
    private void Draw3DHealthBar(Graphics gfx, IBrush brush, Vector2 screenV2, Vector2 boxV2, float hpPer, float stroke)
    {
        gfx.DrawRectangle(_brushes["white"], Rectangle.Create(
            screenV2.X - boxV2.X / 2,
            screenV2.Y + boxV2.Y / 2 + boxV2.X / 10,
            boxV2.X,
            boxV2.X / 10 / 2), stroke);
        gfx.FillRectangle(brush, Rectangle.Create(
            screenV2.X - boxV2.X / 2,
            screenV2.Y + boxV2.Y / 2 + boxV2.X / 10,
            boxV2.X * hpPer,
            boxV2.X / 10 / 2));
    }

    /// <summary>
    /// 2DBox血量数字
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="brush"></param>
    /// <param name="screenV2"></param>
    /// <param name="boxV2"></param>
    /// <param name="health"></param>
    /// <param name="maxHealth"></param>
    /// <param name="index"></param>
    private void Draw2DHealthText(Graphics gfx, IBrush brush, Vector2 screenV2, Vector2 boxV2, float health, float maxHealth, int index)
    {
        gfx.DrawText(_fonts["Microsoft YaHei"], 10, brush,
            screenV2.X - boxV2.X / 2,
            screenV2.Y + boxV2.Y / 2 + boxV2.X / 8 - boxV2.X / 10,
            $"[{index}] HP : {health:0}/{maxHealth:0}");
    }

    /// <summary>
    /// 3DBox血量数字
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="brush"></param>
    /// <param name="screenV2"></param>
    /// <param name="boxV2"></param>
    /// <param name="pedHealth"></param>
    /// <param name="pedMaxHealth"></param>
    /// <param name="index"></param>
    private void Draw3DHealthText(Graphics gfx, IBrush brush, Vector2 screenV2, Vector2 boxV2, float pedHealth, float pedMaxHealth, int index)
    {
        gfx.DrawText(_fonts["Microsoft YaHei"], 10, brush,
            screenV2.X - boxV2.X / 2,
            screenV2.Y + boxV2.Y / 2 + boxV2.X / 10 + boxV2.X / 10 / 2 + boxV2.X / 8 - boxV2.X / 10,
            $"[{index}] HP : {pedHealth:0}/{pedMaxHealth:0}");
    }

    /// <summary>
    /// 2DBox玩家名称
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="brush"></param>
    /// <param name="screenV2"></param>
    /// <param name="boxV2"></param>
    /// <param name="name"></param>
    /// <param name="distance"></param>
    private void Draw2DNameText(Graphics gfx, IBrush brush, Vector2 screenV2, Vector2 boxV2, string name, float distance)
    {
        gfx.DrawText(_fonts["Microsoft YaHei"], 10, brush,
            screenV2.X + boxV2.X / 2 + boxV2.X / 8 - boxV2.X / 10,
            screenV2.Y - boxV2.Y / 2,
            $"[{distance:0m}] ID : {name}");
    }

    /// <summary>
    /// 3DBox玩家名称
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="brush"></param>
    /// <param name="screenV2"></param>
    /// <param name="boxV2"></param>
    /// <param name="name"></param>
    /// <param name="distance"></param>
    private void Draw3DNameText(Graphics gfx, IBrush brush, Vector2 screenV2, Vector2 boxV2, string name, float distance)
    {
        gfx.DrawText(_fonts["Microsoft YaHei"], 10, brush,
            screenV2.X + boxV2.X / 2 + boxV2.X / 10 + boxV2.X / 10 / 2 + boxV2.X / 8 - boxV2.X / 10,
            screenV2.Y - boxV2.Y / 2,
            $"[{distance:0m}] ID : {name}");
    }

    private struct AxisAlignedBox
    {
        public Vector3 Min;
        public Vector3 Max;
    }

    /// <summary>
    /// 绘制3D方框连线
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="brush"></param>
    /// <param name="m_Position"></param>
    /// <param name="stroke"></param>
    private void DrawAABBLine(Graphics gfx, IBrush brush, Vector3 m_Position, float stroke)
    {
        Vector3 aabb_0 = new Vector3(0.0f, 0.0f, 1.0f) + m_Position; // 0
        Vector2 aabb_0V2Pos = WorldToScreen(aabb_0);

        if (!IsNullVector2(aabb_0V2Pos))
        {
            gfx.DrawLine(brush,
                windowData.Width / 2, 0,
                aabb_0V2Pos.X, aabb_0V2Pos.Y, stroke);
        }
    }

    /// <summary>
    /// 绘制3D方框
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="brush"></param>
    /// <param name="m_Position"></param>
    /// <param name="m_SinCos"></param>
    private void DrawAABBBox(Graphics gfx, IBrush brush, Vector3 m_Position, Vector2 m_SinCos)
    {
        AxisAlignedBox aabb = new AxisAlignedBox
        {
            Min = new Vector3(-0.5f, -0.5f, -1.0f),
            Max = new Vector3(0.5f, 0.5f, 1.0f)
        };

        Vector3 aabb_0 = new Vector3(
            aabb.Min.X * m_SinCos.Y - aabb.Min.Y * m_SinCos.X,
            aabb.Min.X * m_SinCos.X + aabb.Min.Y * m_SinCos.Y,
            aabb.Min.Z) + m_Position; // 0
        Vector3 aabb_1 = new Vector3(
            aabb.Min.X * m_SinCos.Y - aabb.Max.Y * m_SinCos.X,
            aabb.Min.X * m_SinCos.X + aabb.Max.Y * m_SinCos.Y,
            aabb.Min.Z) + m_Position; // 1
        Vector3 aabb_2 = new Vector3(
            aabb.Min.X * m_SinCos.Y - aabb.Min.Y * m_SinCos.X,
            aabb.Min.X * m_SinCos.X + aabb.Min.Y * m_SinCos.Y,
            aabb.Max.Z) + m_Position; // 2
        Vector3 aabb_3 = new Vector3(
            aabb.Min.X * m_SinCos.Y - aabb.Max.Y * m_SinCos.X,
            aabb.Min.X * m_SinCos.X + aabb.Max.Y * m_SinCos.Y,
            aabb.Max.Z) + m_Position; // 3

        Vector3 aabb_4 = new Vector3(
            aabb.Max.X * m_SinCos.Y - aabb.Min.Y * m_SinCos.X,
            aabb.Max.X * m_SinCos.X + aabb.Min.Y * m_SinCos.Y,
            aabb.Min.Z) + m_Position; // 4
        Vector3 aabb_5 = new Vector3(
            aabb.Max.X * m_SinCos.Y - aabb.Max.Y * m_SinCos.X,
            aabb.Max.X * m_SinCos.X + aabb.Max.Y * m_SinCos.Y,
            aabb.Min.Z) + m_Position; // 5
        Vector3 aabb_6 = new Vector3(
            aabb.Max.X * m_SinCos.Y - aabb.Min.Y * m_SinCos.X,
            aabb.Max.X * m_SinCos.X + aabb.Min.Y * m_SinCos.Y,
            aabb.Max.Z) + m_Position; // 6
        Vector3 aabb_7 = new Vector3(
            aabb.Max.X * m_SinCos.Y - aabb.Max.Y * m_SinCos.X,
            aabb.Max.X * m_SinCos.X + aabb.Max.Y * m_SinCos.Y,
            aabb.Max.Z) + m_Position; // 7

        /// <summary>
        ///                    max
        ///         6 --------- 7
        ///       / |         / |
        ///     2 --------- 3   |
        ///     |   |       |   |
        ///     |   |       |   |
        ///     |   4 --------- 5
        ///     | /         | /
        ///     0 --------- 1
        ///    min
        /// </summary>

        Vector2 aabb_0V2Pos = WorldToScreen(aabb_0);
        Vector2 aabb_1V2Pos = WorldToScreen(aabb_1);
        Vector2 aabb_2V2Pos = WorldToScreen(aabb_2);
        Vector2 aabb_3V2Pos = WorldToScreen(aabb_3);
        Vector2 aabb_4V2Pos = WorldToScreen(aabb_4);
        Vector2 aabb_5V2Pos = WorldToScreen(aabb_5);
        Vector2 aabb_6V2Pos = WorldToScreen(aabb_6);
        Vector2 aabb_7V2Pos = WorldToScreen(aabb_7);

        if (!IsNullVector2(aabb_0V2Pos) &&
            !IsNullVector2(aabb_1V2Pos) &&
            !IsNullVector2(aabb_2V2Pos) &&
            !IsNullVector2(aabb_3V2Pos) &&
            !IsNullVector2(aabb_4V2Pos) &&
            !IsNullVector2(aabb_5V2Pos) &&
            !IsNullVector2(aabb_6V2Pos) &&
            !IsNullVector2(aabb_7V2Pos))
        {
            gfx.DrawLine(brush, aabb_0V2Pos.X, aabb_0V2Pos.Y, aabb_1V2Pos.X, aabb_1V2Pos.Y, 0.7f);
            gfx.DrawLine(brush, aabb_0V2Pos.X, aabb_0V2Pos.Y, aabb_2V2Pos.X, aabb_2V2Pos.Y, 0.7f);
            gfx.DrawLine(brush, aabb_3V2Pos.X, aabb_3V2Pos.Y, aabb_1V2Pos.X, aabb_1V2Pos.Y, 0.7f);
            gfx.DrawLine(brush, aabb_3V2Pos.X, aabb_3V2Pos.Y, aabb_2V2Pos.X, aabb_2V2Pos.Y, 0.7f);

            gfx.DrawLine(brush, aabb_4V2Pos.X, aabb_4V2Pos.Y, aabb_5V2Pos.X, aabb_5V2Pos.Y, 0.7f);
            gfx.DrawLine(brush, aabb_4V2Pos.X, aabb_4V2Pos.Y, aabb_6V2Pos.X, aabb_6V2Pos.Y, 0.7f);
            gfx.DrawLine(brush, aabb_7V2Pos.X, aabb_7V2Pos.Y, aabb_5V2Pos.X, aabb_5V2Pos.Y, 0.7f);
            gfx.DrawLine(brush, aabb_7V2Pos.X, aabb_7V2Pos.Y, aabb_6V2Pos.X, aabb_6V2Pos.Y, 0.7f);

            gfx.DrawLine(brush, aabb_0V2Pos.X, aabb_0V2Pos.Y, aabb_4V2Pos.X, aabb_4V2Pos.Y, 0.7f);
            gfx.DrawLine(brush, aabb_1V2Pos.X, aabb_1V2Pos.Y, aabb_5V2Pos.X, aabb_5V2Pos.Y, 0.7f);
            gfx.DrawLine(brush, aabb_2V2Pos.X, aabb_2V2Pos.Y, aabb_6V2Pos.X, aabb_6V2Pos.Y, 0.7f);
            gfx.DrawLine(brush, aabb_3V2Pos.X, aabb_3V2Pos.Y, aabb_7V2Pos.X, aabb_7V2Pos.Y, 0.7f);
        }
    }

    /// <summary>
    /// 绘制骨骼连线
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="offset"></param>
    /// <param name="bone0"></param>
    /// <param name="bone1"></param>
    private void DrawBone(Graphics gfx, long offset, int bone0, int bone1)
    {
        Vector2 v2Bone0 = WorldToScreen(GetBonePosition(offset, bone0));
        Vector2 v2Bone1 = WorldToScreen(GetBonePosition(offset, bone1));

        if (!IsNullVector2(v2Bone0) && !IsNullVector2(v2Bone1))
            gfx.DrawLine(_brushes["white"], v2Bone0.X, v2Bone0.Y, v2Bone1.X, v2Bone1.Y, 1);
    }

    /// <summary>
    /// 骨骼绘制调试
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="offset"></param>
    /// <param name="bone"></param>
    private void DrawBoneDeBug(Graphics gfx, long offset, int bone)
    {
        Vector2 v2Bone = WorldToScreen(GetBonePosition(offset, bone));
        if (!IsNullVector2(v2Bone))
            gfx.DrawText(_fonts["Microsoft YaHei"], 10, _brushes["white"], v2Bone.X, v2Bone.Y, $"{bone}");
    }

    /// <summary>
    /// 获取骨骼3D坐标
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="BoneID"></param>
    /// <returns></returns>
    private Vector3 GetBonePosition(long offset, int BoneID)
    {
        float[] bomematrix = Memory.ReadMatrix<float>(offset + 0x60, 16);

        Vector3 bone_offset_pos = Memory.Read<Vector3>(offset + 0x410 + BoneID * 0x10);

        Vector3 bone_pos;
        bone_pos.X = bomematrix[0] * bone_offset_pos.X + bomematrix[4] * bone_offset_pos.Y + bomematrix[8] * bone_offset_pos.Z + bomematrix[12];
        bone_pos.Y = bomematrix[1] * bone_offset_pos.X + bomematrix[5] * bone_offset_pos.Y + bomematrix[9] * bone_offset_pos.Z + bomematrix[13];
        bone_pos.Z = bomematrix[2] * bone_offset_pos.X + bomematrix[6] * bone_offset_pos.Y + bomematrix[10] * bone_offset_pos.Z + bomematrix[14];

        return bone_pos;
    }

    /// <summary>
    /// 鼠标角度
    /// </summary>
    /// <param name="cameraV3"></param>
    /// <param name="targetV3"></param>
    /// <returns></returns>
    private Vector3 GetCCameraViewAngles(Vector3 cameraV3, Vector3 targetV3)
    {
        float distance = (float)Math.Sqrt(Math.Pow(cameraV3.X - targetV3.X, 2) + Math.Pow(cameraV3.Y - targetV3.Y, 2) + Math.Pow(cameraV3.Z - targetV3.Z, 2));

        return new Vector3
        {
            X = (targetV3.X - cameraV3.X) / distance,
            Y = (targetV3.Y - cameraV3.Y) / distance,
            Z = (targetV3.Z - cameraV3.Z) / distance
        };
    }

    /// <summary>
    /// 判断屏幕坐标是否有效
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    private bool IsNullVector2(Vector2 vector)
    {
        return vector.X == 0 && vector.Y == 0;
    }

    /// <summary>
    /// 世界坐标转屏幕坐标
    /// </summary>
    /// <param name="posV3"></param>
    /// <returns></returns>
    private Vector2 WorldToScreen(Vector3 posV3)
    {
        Vector2 screenV2;
        Vector3 cameraV3;

        float[] viewMatrix = Memory.ReadMatrix<float>(Pointers.ViewPortPTR + 0xC0, 16);

        cameraV3.Z = viewMatrix[2] * posV3.X + viewMatrix[6] * posV3.Y + viewMatrix[10] * posV3.Z + viewMatrix[14];
        if (cameraV3.Z < 0.001f)
            return new Vector2(0, 0);

        cameraV3.X = windowData.Width / 2;
        cameraV3.Y = windowData.Height / 2;
        cameraV3.Z = 1 / cameraV3.Z;

        screenV2.X = viewMatrix[0] * posV3.X + viewMatrix[4] * posV3.Y + viewMatrix[8] * posV3.Z + viewMatrix[12];
        screenV2.Y = viewMatrix[1] * posV3.X + viewMatrix[5] * posV3.Y + viewMatrix[9] * posV3.Z + viewMatrix[13];

        screenV2.X = cameraV3.X + cameraV3.X * screenV2.X * cameraV3.Z;
        screenV2.Y = cameraV3.Y - cameraV3.Y * screenV2.Y * cameraV3.Z;

        return screenV2;
    }

    /// <summary>
    /// 获取方框高度
    /// </summary>
    /// <param name="posV3"></param>
    /// <returns></returns>
    private Vector2 GetBoxWH(Vector3 posV3)
    {
        Vector2 boxV2;
        Vector3 cameraV3;

        float[] viewMatrix = Memory.ReadMatrix<float>(Pointers.ViewPortPTR + 0xC0, 16);

        cameraV3.Z = viewMatrix[2] * posV3.X + viewMatrix[6] * posV3.Y + viewMatrix[10] * posV3.Z + viewMatrix[14];
        if (cameraV3.Z < 0.001f)
            return new Vector2(0, 0);

        cameraV3.Y = windowData.Height / 2;
        cameraV3.Z = 1 / cameraV3.Z;

        boxV2.X = viewMatrix[1] * posV3.X + viewMatrix[5] * posV3.Y + viewMatrix[9] * (posV3.Z + 1.0f) + viewMatrix[13];
        boxV2.Y = viewMatrix[1] * posV3.X + viewMatrix[5] * posV3.Y + viewMatrix[9] * (posV3.Z - 1.0f) + viewMatrix[13];

        boxV2.X = cameraV3.Y - cameraV3.Y * boxV2.X * cameraV3.Z;
        boxV2.Y = cameraV3.Y - cameraV3.Y * boxV2.Y * cameraV3.Z;

        boxV2.Y = Math.Abs(boxV2.X - boxV2.Y);
        boxV2.X = boxV2.Y / 2;

        return boxV2;
    }

    /////////////////////////////////////////////

    public void Run()
    {
        _window.Create();
        _window.Join();
    }

    ~Overlay()
    {
        Dispose(false);
    }

    #region IDisposable Support
    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            _window.Dispose();

            disposedValue = true;
        }

        isRun = false;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion

    /////////////////////////////////////////////

    private struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
