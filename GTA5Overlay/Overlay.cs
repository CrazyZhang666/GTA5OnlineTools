using GTA5HotKey;
using GTA5Core.Native;
using GTA5Core.Offsets;
using GTA5Core.Features;

using GameOverlay.Drawing;
using GameOverlay.Windows;

namespace GTA5Overlay;

public class Overlay : IDisposable
{
    private readonly GraphicsWindow _window;

    private readonly List<SolidBrush> _brushes;
    private readonly List<Font> _fonts;

    //////////////////////////////////

    private SolidBrush _brush_white;
    private SolidBrush _brush_red;
    private SolidBrush _brush_green;
    private SolidBrush _brush_blue;
    private SolidBrush _brush_yellow;
    private SolidBrush _brush_deepPink;
    private SolidBrush _brush_transparency;

    private Font _font_YaHei;

    //////////////////////////////////

    private WindowData _windowData;

    // 视角宽和视角高
    private float _gviewWidth;
    private float _gviewHeight;

    private readonly StringBuilder _infoText = new();

    private bool _isDraw = true;
    private bool _isRun = true;

    //////////////////////////////////

    public Overlay()
    {
        _windowData = Memory.GetGameWindowData();
        Memory.SetForegroundWindow();

        Core.SetWindowData(_windowData.Width, _windowData.Height);
        Draw.SetWindowData(_windowData.Width, _windowData.Height);

        /////////////////////////////////////////////

        _brushes = new List<SolidBrush>();
        _fonts = new List<Font>();

        var gfx = new Graphics()
        {
            VSync = Setting.VSync,
            MeasureFPS = true,
            PerPrimitiveAntiAliasing = true,
            TextAntiAliasing = true,
            UseMultiThreadedFactories = true
        };

        _window = new GraphicsWindow(_windowData.Left, _windowData.Top, _windowData.Width, _windowData.Height, gfx)
        {
            FPS = Setting.FPS,
            IsTopmost = true,
            IsVisible = true
        };

        _window.SetupGraphics += _window_SetupGraphics;
        _window.DestroyGraphics += _window_DestroyGraphics;
        _window.DrawGraphics += _window_DrawGraphics;

        SetViewSize();
        Draw.SetGraphicsIns(gfx);

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
        while (_isRun)
        {
            if (Setting.NoTopMostHide)
                _isDraw = Memory.IsForegroundWindow();
            else
                _isDraw = true;

            Thread.Sleep(1000);
        }
    }

    private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
    {
        var gfx = e.Graphics;

        if (e.RecreateResources)
        {
            foreach (var pair in _brushes)
                pair.Dispose();
        }

        _brush_white = gfx.CreateSolidBrush(255, 255, 255);
        _brush_red = gfx.CreateSolidBrush(255, 0, 98);
        _brush_green = gfx.CreateSolidBrush(0, 128, 0);
        _brush_blue = gfx.CreateSolidBrush(30, 144, 255);
        _brush_yellow = gfx.CreateSolidBrush(254, 215, 0);
        _brush_deepPink = gfx.CreateSolidBrush(247, 63, 147, 255);
        _brush_transparency = gfx.CreateSolidBrush(0, 0, 0, 0);

        _brushes.Add(_brush_white);
        _brushes.Add(_brush_red);
        _brushes.Add(_brush_green);
        _brushes.Add(_brush_blue);
        _brushes.Add(_brush_yellow);
        _brushes.Add(_brush_deepPink);
        _brushes.Add(_brush_transparency);

        if (e.RecreateResources)
            return;

        _font_YaHei = gfx.CreateFont("Microsoft YaHei", 12);

        _fonts.Add(_font_YaHei);
    }

    private void _window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
    {
        // 清理颜色资源
        foreach (var pair in _brushes)
            pair.Dispose();
        // 清理字体资源
        foreach (var pair in _fonts)
            pair.Dispose();
    }

    private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
    {
        var gfx = e.Graphics;

        gfx.ClearScene(_brush_transparency);

        if (!_isDraw)
            return;

        ResizeWindow(gfx);

        ///////////////////////////////////////////////////////
        //                 用户自定义绘制区域                   //
        ///////////////////////////////////////////////////////

        var pWordPTR = Memory.Read<long>(Pointers.WorldPTR);
        var pLocalCPed = Memory.Read<long>(pWordPTR + CPedFactory.CPed);

        // 自己坐标
        var pLocalCNavigation = Memory.Read<long>(pLocalCPed + 0x30);
        var localPosV3 = Memory.Read<Vector3>(pLocalCNavigation + 0x50);

        /////////////////////////////////////////////////////////////////////

        // 玩家列表
        var pCNetworkPlayerMgr = Memory.Read<long>(Pointers.NetworkPlayerMgrPTR);
        var playerCount = Memory.Read<int>(pCNetworkPlayerMgr + 0x180);

        var pReplayInterface = Memory.Read<long>(Pointers.ReplayInterfacePTR);

        // Ped
        var pCPedInterface = Memory.Read<long>(pReplayInterface + CReplayInterface.CPedInterface);
        //var m_max_peds = Memory.Read<int>(pCPedInterface + CPedInterface.MaxPeds);
        var m_cur_peds = Memory.Read<int>(pCPedInterface + CPedInterface.CurPeds);

        // 载具
        var pCVehicleInterface = Memory.Read<long>(pReplayInterface + CReplayInterface.CVehicleInterface);
        //var m_max_vehicles = Memory.Read<int>(pCVehicleInterface + CVehicleInterface.MaxVehicles);
        var m_cur_vehicles = Memory.Read<int>(pCVehicleInterface + CVehicleInterface.CurVehicles);

        // 可拾取
        var pCPickupInterface = Memory.Read<long>(pReplayInterface + CReplayInterface.CPickupInterface);
        //var m_max_pickups = Memory.Read<int>(pCPickupInterface + CPickupInterface.MaxPickups);
        var m_cur_pickups = Memory.Read<int>(pCPickupInterface + CPickupInterface.CurPickups);

        // 对象
        var pCObjectInterface = Memory.Read<long>(pReplayInterface + CReplayInterface.CObjectInterface);
        //var m_max_objects = Memory.Read<int>(pCObjectInterface + CObjectInterface.MaxObjects);
        var m_cur_objects = Memory.Read<int>(pCObjectInterface + CObjectInterface.CurObjects);

        //var hour = Memory.Read<int>(Pointers.TimePTR + 0x10);
        //var minute = Memory.Read<int>(Pointers.TimePTR + 0x14);
        //var second = Memory.Read<int>(Pointers.TimePTR + 0x18);

        _infoText.Clear();
        _infoText.AppendLine("GTA5线上小助手\n");
        //_infoText.AppendLine($"时间: {hour:00}:{minute:00}:{second:00}\n");
        _infoText.AppendLine($"X: {localPosV3.X:0.0000}");
        _infoText.AppendLine($"Y: {localPosV3.Y:0.0000}");
        _infoText.AppendLine($"Z: {localPosV3.Z:0.0000}\n");
        _infoText.AppendLine($"线上玩家: {playerCount}");
        _infoText.AppendLine($"人物模型: {m_cur_peds}");
        _infoText.AppendLine($"载具模型: {m_cur_vehicles}");
        _infoText.AppendLine($"掉落物品: {m_cur_pickups}");
        _infoText.AppendLine($"游戏对象: {m_cur_objects}");

        if (Setting.ESP_InfoText)
        {
            // 绘制帧数文本
            gfx.DrawText(_font_YaHei, 12, _brush_green, 10, _window.Height / 3, $"FPS：{gfx.FPS}");
            // 绘制信息文本
            gfx.DrawText(_font_YaHei, 12, _brush_blue, 10, _window.Height / 3 + 30, _infoText.ToString());
        }

        var pAimingPed = Memory.Read<long>(Pointers.AimingPedPTR);
        var isAimPed = Memory.Read<long>(pAimingPed + 0x280) > 0;

        if (Setting.ESP_Crosshair)
        {
            // 当玩家按住右键准心对准敌人，准心变成粉红色，否则为绿色
            if (isAimPed && KeyHelper.IsKeyPressed(Keys.RButton))
                Draw.DrawCrosshair(_brush_deepPink, 7.0f);
            else
                Draw.DrawCrosshair(_brush_green, 7.0f);
        }

        ///////////////////////////////////////////////////////

        for (var i = 0; i < Base.oMaxPeds; i++)
        {
            var pCPedList = Memory.Read<long>(pCPedInterface + CPedInterface.CPedList);
            var pCPed = Memory.Read<long>(pCPedList + i * 0x10);      // CEntityEntry
            if (!Memory.IsValid(pCPed))
                continue;

            // 如果是自己，跳过
            if (pLocalCPed == pCPed)
                continue;

            // 如果ped死亡，跳过
            var pedHealth = Memory.Read<float>(pCPed + CPed.Health);
            if (pedHealth <= 0)
                continue;

            var pedHealthMax = Memory.Read<float>(pCPed + CPed.HealthMax);
            var pedHPPercentage = pedHealth / pedHealthMax;

            var pCPlayerInfo = Memory.Read<long>(pCPed + CPed.CPlayerInfo);

            // 玩家的pCPlayerInfo为有效指针，IsValid返回为true
            // NPC的pCPlayerInfo为无效指针，IsValid返回为false
            var isPlayer = Memory.IsValid(pCPlayerInfo);
            var isNPC = !isPlayer;

            var pedName = Memory.ReadString(pCPlayerInfo + CPlayerInfo.Name, 20);

            var pCNavigation = Memory.Read<long>(pCPed + CPed.CNavigation);
            if (!Memory.IsValid(pCNavigation))
                continue;

            // ped坐标
            var pedPosV3 = Memory.Read<Vector3>(pCNavigation + CNavigation.PositionX);

            // Ped与自己的距离
            var distance = (float)Math.Sqrt(
                Math.Pow(localPosV3.X - pedPosV3.X, 2) +
                Math.Pow(localPosV3.Y - pedPosV3.Y, 2) +
                Math.Pow(localPosV3.Z - pedPosV3.Z, 2));

            // m_heading    0x20
            // m_heading2   0x24
            var v2PedSinCos = new Vector2
            {
                X = Memory.Read<float>(pCNavigation + CNavigation.RightX),
                Y = Memory.Read<float>(pCNavigation + CNavigation.ForwardX)
            };

            var pedPosV2 = Core.WorldToScreen(pedPosV3);
            var pedBoxV2 = Core.GetBoxSize(pedPosV3);

            if (!Core.IsNullVector2(pedPosV2))
            {
                if (Setting.ESP_Player && isPlayer)
                {
                    // 玩家 2D
                    if (Setting.ESP_Player_2D)
                    {
                        // 玩家 2D方框
                        if (Setting.ESP_Player_Box)
                            Draw.Draw2DBox(_brush_red, pedPosV2, pedBoxV2);

                        // 玩家 2D射线
                        if (Setting.ESP_Player_Line)
                            Draw.Draw2DLine(_brush_red, pedPosV2, pedBoxV2);

                        // 玩家 2D血条
                        if (Setting.ESP_Player_HealthBar)
                            Draw.Draw2DHealthBar(_brush_white, _brush_green, pedPosV2, pedBoxV2, pedHPPercentage);

                        // 玩家 2D血量数字
                        if (Setting.ESP_Player_HealthText)
                            Draw.Draw2DHealthText(_font_YaHei, _brush_red, pedPosV2, pedBoxV2, pedHealth, pedHealthMax, i);

                        // 玩家 2D玩家名称
                        if (Setting.ESP_Player_NameText)
                            Draw.Draw2DNameText(_font_YaHei, _brush_red, pedPosV2, pedBoxV2, pedName, distance);
                    }

                    // 玩家 3D
                    if (Setting.ESP_Player_3D)
                    {
                        // 玩家 3D方框
                        if (Setting.ESP_Player_Box)
                            Draw.DrawAABBBox(_brush_red, pedPosV3, v2PedSinCos);

                        // 玩家 3D射线
                        if (Setting.ESP_Player_Line)
                            Draw.DrawAABBLine(_brush_red, pedPosV3);

                        // 玩家 3D血条
                        if (Setting.ESP_Player_HealthBar)
                            Draw.Draw3DHealthBar(_brush_white, _brush_green, pedPosV2, pedBoxV2, pedHPPercentage);

                        // 玩家 2D血量数字
                        if (Setting.ESP_Player_HealthText)
                            Draw.Draw3DHealthText(_font_YaHei, _brush_red, pedPosV2, pedBoxV2, pedHealth, pedHealthMax, i);

                        // 玩家 2D玩家名称
                        if (Setting.ESP_Player_HealthBar)
                            Draw.Draw3DNameText(_font_YaHei, _brush_red, pedPosV2, pedBoxV2, pedName, distance);
                    }

                    // 玩家 骨骼
                    if (Setting.ESP_Player_Bone)
                    {
                        var boneMatrix = Core.GetBoneMatrix(pCPed);

                        Draw.DrawBone(_brush_white, pCPed, boneMatrix, 0, 7);
                        Draw.DrawBone(_brush_white, pCPed, boneMatrix, 7, 8);
                        Draw.DrawBone(_brush_white, pCPed, boneMatrix, 8, 3);
                        Draw.DrawBone(_brush_white, pCPed, boneMatrix, 8, 4);
                        Draw.DrawBone(_brush_white, pCPed, boneMatrix, 7, 5);
                        Draw.DrawBone(_brush_white, pCPed, boneMatrix, 7, 6);
                    }
                }

                ////////////////////////////////////////////////////////////////////////

                if (Setting.ESP_NPC && isNPC)
                {
                    // NPC 2D
                    if (Setting.ESP_NPC_2D)
                    {
                        // NPC 2D方框
                        if (Setting.ESP_NPC_Box)
                            Draw.Draw2DBox(_brush_white, pedPosV2, pedBoxV2);

                        // NPC 2D射线
                        if (Setting.ESP_NPC_Line)
                            Draw.Draw2DLine(_brush_white, pedPosV2, pedBoxV2);

                        // NPC 2D血条
                        if (Setting.ESP_NPC_HealthBar)
                            Draw.Draw2DHealthBar(_brush_white, _brush_green, pedPosV2, pedBoxV2, pedHPPercentage);

                        // NPC 2D血量数字
                        if (Setting.ESP_NPC_HealthText)
                            Draw.Draw2DHealthText(_font_YaHei, _brush_white, pedPosV2, pedBoxV2, pedHealth, pedHealthMax, i);

                        // NPC 2D玩家名称
                        if (Setting.ESP_NPC_NameText)
                            Draw.Draw2DNameText(_font_YaHei, _brush_white, pedPosV2, pedBoxV2, pedName, distance);
                    }

                    // NPC 3D
                    if (Setting.ESP_NPC_3D)
                    {
                        // NPC 3D方框
                        if (Setting.ESP_NPC_Box)
                            Draw.DrawAABBBox(_brush_white, pedPosV3, v2PedSinCos);

                        // NPC 3D射线
                        if (Setting.ESP_NPC_Line)
                            Draw.DrawAABBLine(_brush_white, pedPosV3);

                        // NPC 3D血条
                        if (Setting.ESP_NPC_HealthBar)
                            Draw.Draw3DHealthBar(_brush_white, _brush_green, pedPosV2, pedBoxV2, pedHPPercentage);

                        // NPC 3D血量数字
                        if (Setting.ESP_NPC_HealthText)
                            Draw.Draw3DHealthText(_font_YaHei, _brush_white, pedPosV2, pedBoxV2, pedHealth, pedHealthMax, i);

                        // NPC 3D玩家名称
                        if (Setting.ESP_NPC_NameText)
                            Draw.Draw3DNameText(_font_YaHei, _brush_white, pedPosV2, pedBoxV2, pedName, distance);
                    }

                    // NPC 骨骼
                    if (Setting.ESP_NPC_Bone)
                    {
                        var boneMatrix = Core.GetBoneMatrix(pCPed);

                        Draw.DrawBone(_brush_white, pCPed, boneMatrix, 0, 7);
                        Draw.DrawBone(_brush_white, pCPed, boneMatrix, 7, 8);
                        Draw.DrawBone(_brush_white, pCPed, boneMatrix, 8, 3);
                        Draw.DrawBone(_brush_white, pCPed, boneMatrix, 8, 4);
                        Draw.DrawBone(_brush_white, pCPed, boneMatrix, 7, 5);
                        Draw.DrawBone(_brush_white, pCPed, boneMatrix, 7, 6);
                    }
                }

                ////////////////////////////////////////////////////////////////////////

                // Debug调试信息

#if DEBUG
                //var ped_type = Memory.Read<int>(pCPed + CPed.Ragdoll);
                //var oHostility = Memory.Read<byte>(pCPed + CPed.Hostility);
                //ped_type = ped_type << 11 >> 25;

                //gfx.DrawText(_font_YaHei, 12, _brush_green, pedPosV2.X, pedPosV2.Y, $"{ped_type}\n{oHostility}");
#endif
            }
        }

        ///////////////////////////////////////////////////////

        for (var i = 0; i < Base.oMaxPickups; i++)
        {
            var pCPickupList = Memory.Read<long>(pCPickupInterface + CPickupInterface.CPickupList);
            var pCPickup = Memory.Read<long>(pCPickupList + i * 0x10);      // CEntityEntry
            if (!Memory.IsValid(pCPickup))
                continue;

            var pCNavigation = Memory.Read<long>(pCPickup + CPed.CNavigation);
            if (!Memory.IsValid(pCNavigation))
                continue;

            // pickup坐标
            var pickupPosV3 = Memory.Read<Vector3>(pCNavigation + CNavigation.PositionX);

            // m_heading    0x20
            // m_heading2   0x24
            var v2PickupSinCos = new Vector2
            {
                X = Memory.Read<float>(pCNavigation + CNavigation.RightX),
                Y = Memory.Read<float>(pCNavigation + CNavigation.ForwardX)
            };

            var pickupPosV2 = Core.WorldToScreen(pickupPosV3);
            var pickupBoxV2 = Core.GetBoxSize(pickupPosV3, 0.2f, 1.0f);

            if (!Core.IsNullVector2(pickupPosV2))
            {
                if (Setting.ESP_Pickup)
                {
                    // Pickup 2D方框
                    if (Setting.ESP_Pickup_2D)
                    {
                        Draw.Draw2DBox(_brush_yellow, pickupPosV2, pickupBoxV2);

                        // Pickup 2D射线
                        if (Setting.ESP_Pickup_Line)
                            Draw.Draw2DLine(_brush_yellow, pickupPosV2, pickupBoxV2);
                    }

                    // Pickup 3D方框
                    if (Setting.ESP_Pickup_3D)
                    {
                        Draw.DrawAABBBox(_brush_yellow, pickupPosV3, v2PickupSinCos, 0.2f, 1.0f);

                        // Pickup 3D射线
                        if (Setting.ESP_Pickup_Line)
                            Draw.DrawAABBLine(_brush_yellow, pickupPosV3);
                    }
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
        _windowData = Memory.GetGameWindowData();

        if (_window.X != _windowData.Left)
        {
            _window.X = _windowData.Left;
            _window.Y = _windowData.Top;
            _window.Width = _windowData.Width;
            _window.Height = _windowData.Height;

            gfx.Resize(_window.Width, _window.Height);

            SetViewSize();

            Core.SetWindowData(_windowData.Width, _windowData.Height);
            Draw.SetWindowData(_windowData.Width, _windowData.Height);
        }
    }

    /// <summary>
    /// 设置视角大小
    /// </summary>
    private void SetViewSize()
    {
        // 视角宽和视角高
        _gviewWidth = _windowData.Width / 2;
        _gviewHeight = _windowData.Height / 2;
    }

    /// <summary>
    /// Aimbot线程
    /// </summary>
    private void AimbotThread()
    {
        while (_isRun)
        {
            if (!Setting.AimBot_Player_Enabled && !Setting.AimBot_NPC_Enabled)
                goto SLEEP;

            var aimBot_Min_Distance = Setting.AimBot_Fov;
            var aimBot_ViewAngles = new Vector3() { X = 0, Y = 0, Z = 0 };
            var teleW_pedCoords = new Vector3() { X = 0, Y = 0, Z = 0 };

            var pWordPTR = Memory.Read<long>(Pointers.WorldPTR);
            var pLocalCPed = Memory.Read<long>(pWordPTR + CPedFactory.CPed);

            var isInVehicle = Vehicle.IsInVehicle(pLocalCPed);

            // 相机坐标
            var pCCameraPTR = Memory.Read<long>(Pointers.CCameraPTR);
            var pCCameraPTR_0 = Memory.Read<long>(pCCameraPTR + 0x00);
            pCCameraPTR_0 = Memory.Read<long>(pCCameraPTR_0 + 0x3C0);
            var cameraV3Pos = Memory.Read<Vector3>(pCCameraPTR_0 + 0x60);

            // 是否是第一人称，当Fov=0为第一人称或者开镜状态，第三人称50
            var offset = Memory.Read<long>(pCCameraPTR_0 + 0x10);
            var isFPP = Memory.Read<float>(offset + 0x30);

            // Ped实体
            var pReplayInterface = Memory.Read<long>(Pointers.ReplayInterfacePTR);
            var pCPedInterface = Memory.Read<long>(pReplayInterface + +CReplayInterface.CPedInterface);

            for (var i = 0; i < Base.oMaxPeds; i++)
            {
                var pCPedList = Memory.Read<long>(pCPedInterface + CPedInterface.CPedList);
                var pCPed = Memory.Read<long>(pCPedList + i * 0x10);      // CEntityEntry
                if (!Memory.IsValid(pCPed))
                    continue;

                // 如果是自己，跳过
                if (pLocalCPed == pCPed)
                    continue;

                // 如果ped死亡，跳过
                var ped_Health = Memory.Read<float>(pCPed + CPed.Health);
                if (ped_Health <= 0)
                    continue;

                var pCPlayerInfo = Memory.Read<long>(pCPed + CPed.CPlayerInfo);

                // 玩家的pCPlayerInfo为有效指针，IsValid返回为true
                // NPC的pCPlayerInfo为无效指针，IsValid返回为false
                var isPlayer = Memory.IsValid(pCPlayerInfo);
                var isNPC = !isPlayer;

                // AimBot 玩家
                if (isPlayer)
                {
                    if (!Setting.ESP_Player)
                        continue;

                    if (!Setting.AimBot_Player_Enabled)
                        continue;
                }

                // AimBot NPC
                if (isNPC)
                {
                    if (!Setting.ESP_NPC)
                        continue;

                    if (!Setting.AimBot_NPC_Enabled)
                        continue;
                }

                var pedV3Pos = Memory.Read<Vector3>(pCPed + CPed.VisualX);
                var pedV2Pos = Core.WorldToScreen(pedV3Pos);

                // 自瞄数据
                var aimBot_Distance = (float)Math.Sqrt(Math.Pow(pedV2Pos.X - _gviewWidth, 2) + Math.Pow(pedV2Pos.Y - _gviewHeight, 2));
                // 获取距离准心最近的方框
                if (aimBot_Distance < aimBot_Min_Distance)
                {
                    var boneMatrix = Core.GetBoneMatrix(pCPed);

                    aimBot_Min_Distance = aimBot_Distance;
                    aimBot_ViewAngles = Core.GetCCameraViewAngles(cameraV3Pos, Core.GetBonePosition(pCPed, boneMatrix, Setting.AimBot_BoneIndex));
                    teleW_pedCoords = pedV3Pos;
                }
            }

            // 玩家处于载具或者掩护状态中不启用自瞄，无目标取消自瞄
            if (!isInVehicle && aimBot_Min_Distance != Setting.AimBot_Fov)
            {
                // 默认按住Ctrl键自瞄
                if (KeyHelper.IsKeyPressed(Setting.AimBot_Key))
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

                    if (KeyHelper.IsKeyPressed(Keys.F5))
                    {
                        Teleport.SetTeleportPosition(teleW_pedCoords);
                    }
                }
            }

        SLEEP:
            Thread.Sleep(10);
        }
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

        _isRun = false;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}