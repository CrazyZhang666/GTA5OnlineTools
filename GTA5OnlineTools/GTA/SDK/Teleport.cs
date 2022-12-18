using GTA5OnlineTools.Helper;
using GTA5OnlineTools.GTA.Core;
using System.Windows.Media.Media3D;

namespace GTA5OnlineTools.GTA.SDK;

public static class Teleport
{
    /// <summary>
    /// 传送到导航点
    /// </summary>
    public static void ToWaypoint()
    {
        SetTeleportPosition(WaypointPosition());
    }

    /// <summary>
    /// 传送到导航点(精准)
    /// </summary>
    public static void ToWaypoint_Super()
    {

        Vector3 coords = WaypointPosition();
        if (coords.X != 0.0f && coords.Y != 0.0f && coords.Z != 0.0f)
        {
            if (coords.Z == -225.0f)
            {
                bool GroundFound = false;
                float[] GroundCheckHeight = new float[22] { 
                    100.0f, 150.0f, 50.0f, 0.0f, 200.0f, 250.0f, 
                    300.0f, 350.0f, 400.0f, 450.0f, 500.0f, 550.0f, 
                    600.0f, 650.0f, 700.0f, 750.0f, 800.0f,
                    850.0f, 900.0f, 950.0f, 1000.0f, 1050.0f
                };//游戏内最高建筑为800左右,但使用HHD硬盘可能存在加载地图较慢,所以增加至1000
                float old_height = GET_GROUND_Z_COORD();
                for (int i = 0; i < 22; i++)
                {
                    Thread.Sleep(100);
                    Vector3 TempCoords = coords;
                    TempCoords.Z = GroundCheckHeight[i];
                    SET_CAMERA_COORDS(TempCoords);
                    coords.Z = GET_GROUND_Z_COORD();
                    if (coords.Z != 0.0f && coords.Z != old_height)
                    {
                        GroundFound = true;
                        coords.Z += 1.0f;
                        break;
                    }
                }
                if (GroundFound != true)
                {
                    coords.Z = -301.0f;
                }
            }
            SET_CAMERA_COORDS(coords);
            SetTeleportPosition(coords);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Information, "当前没有标点");
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 传送到目标点
    /// </summary>
    public static void ToObjective()
    {
        SetTeleportPosition(ObjectivePosition());
    }

    /// <summary>
    /// 传送到Blips
    /// </summary>
    public static void ToBlips(int blipID)
    {
        SetTeleportPosition(CustomObjectivePosition(blipID));
    }

    /// <summary>
    /// 获取玩家当前坐标
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetPlayerPosition()
    {
        long pCPed = Globals.GetCPed();
        return Memory.Read<Vector3>(pCPed + Offsets.CPed_VisualX);
    }

    /// <summary>
    /// 坐标传送功能
    /// </summary>
    public static void SetTeleportPosition(Vector3 vector3)
    {
        if (vector3 != Vector3.Zero)
        {
            long pCPed = Globals.GetCPed();

            if (Memory.Read<int>(pCPed + Offsets.CPed_InVehicle) == 0)
            {
                // 玩家不在载具
                long pCNavigation = Memory.Read<long>(pCPed + Offsets.CPed_CNavigation);
                Memory.Write(pCPed + Offsets.CPed_VisualX, vector3);
                Memory.Write(pCNavigation + Offsets.CPed_CNavigation_PositionX, vector3);
            }
            else
            {
                // 玩家在载具
                long pCVehicle = Memory.Read<long>(pCPed + Offsets.CPed_CVehicle);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_VisualX, vector3);
                long pCNavigation = Memory.Read<long>(pCVehicle + Offsets.CPed_CVehicle_CNavigation);
                Memory.Write(pCNavigation + Offsets.CPed_CVehicle_CNavigation_PositionX, vector3);
            }
        }
    }

    /// <summary>
    /// 获取高度坐标
    /// </summary>
    public static float GET_GROUND_Z_COORD()
    {
        return (Memory.Read<float>(Memory.GTA5ProBaseAddress + 0x26E9ED4));
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 设置摄像机坐标
    /// </summary>
    public static void SET_CAMERA_COORDS(Vector3 vector3)
    {

        if (vector3 != Vector3.Zero)
        {
            long pCPed = Globals.GetCPed();
            long TmepAddress;
            if (Memory.Read<int>(pCPed + Offsets.CPed_InVehicle) == 0)
            {
                // 玩家不在载具
                TmepAddress = Memory.Read<long>(Pointers.WorldPTR) + Offsets.CPed;
                TmepAddress = Memory.Read<long>(TmepAddress) + Offsets.CPed_VisualX;
                Memory.Write<Vector3>(TmepAddress, vector3);
            }
            else
            {
                // 玩家在载具
                TmepAddress = Memory.Read<long>(Pointers.WorldPTR) + Offsets.CPed;
                TmepAddress = Memory.Read<long>(TmepAddress) + Offsets.CPed_CVehicle;
                TmepAddress = Memory.Read<long>(TmepAddress) + Offsets.CPed_CVehicle_VisualX;
                Memory.Write<Vector3>(TmepAddress, vector3);
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 获取导航点坐标
    /// </summary>
    public static Vector3 WaypointPosition()
    {
        Vector3 vector3 = Vector3.Zero;
        int dwIcon, dwColor;

        for (int i = 2000; i > 1; i--)
        {
            long pBlip = Memory.Read<long>(Pointers.BlipPTR + i * 0x08);
            if (pBlip <= 0)
                continue;

            dwIcon = Memory.Read<int>(pBlip + 0x40);
            dwColor = Memory.Read<int>(pBlip + 0x48);

            if (dwIcon == 8 && dwColor == 84)
            {
                vector3 = Memory.Read<Vector3>(pBlip + 0x10);
                vector3.Z = vector3.Z == 20.0f ? -225.0f : vector3.Z + 1.0f;

                return vector3;
            }
        }

        return vector3;
    }

    /// <summary>
    /// 获取目标点坐标
    /// </summary>
    public static Vector3 ObjectivePosition()
    {
        Vector3 vector3 = Vector3.Zero;
        int dwIcon, dwColor;

        for (int i = 2000; i > 1; i--)
        {
            long pBlip = Memory.Read<long>(Pointers.BlipPTR + i * 0x08);

            dwIcon = Memory.Read<int>(pBlip + 0x40);
            dwColor = Memory.Read<int>(pBlip + 0x48);

            if (dwIcon == 1 &&
                (dwColor == 5 || dwColor == 60 || dwColor == 66))
            {
                vector3 = Memory.Read<Vector3>(pBlip + 0x10);
                vector3.Z += +1.0f;

                return vector3;
            }
        }

        for (int i = 2000; i > 1; i--)
        {
            long pBlip = Memory.Read<long>(Pointers.BlipPTR + i * 0x08);

            dwIcon = Memory.Read<int>(pBlip + 0x40);
            dwColor = Memory.Read<int>(pBlip + 0x48);

            if ((dwIcon == 1 || dwIcon == 225 || dwIcon == 427 || dwIcon == 478 || dwIcon == 501 || dwIcon == 523 || dwIcon == 556) &&
                (dwColor == 1 || dwColor == 2 || dwColor == 3 || dwColor == 54 || dwColor == 78))
            {
                vector3 = Memory.Read<Vector3>(pBlip + 0x10);
                vector3.Z += +1.0f;

                return vector3;
            }
        }

        for (int i = 2000; i > 1; i--)
        {
            long pBlip = Memory.Read<long>(Pointers.BlipPTR + i * 0x08);

            dwIcon = Memory.Read<int>(pBlip + 0x40);
            dwColor = Memory.Read<int>(pBlip + 0x48);

            if ((dwIcon == 432 || dwIcon == 443) &&
                (dwColor == 59))
            {
                vector3 = Memory.Read<Vector3>(pBlip + 0x10);
                vector3.Z += +1.0f;

                return vector3;
            }
        }

        return vector3;
    }

    /// <summary>
    /// 获取自定义目标点坐标
    /// </summary>
    public static Vector3 CustomObjectivePosition(int blipID)
    {
        Vector3 vector3 = Vector3.Zero;
        int dwIcon;

        for (int i = 2000; i > 1; i--)
        {
            long pBlip = Memory.Read<long>(Pointers.BlipPTR + i * 0x08);

            dwIcon = Memory.Read<int>(pBlip + 0x40);

            if (dwIcon == blipID)
            {
                vector3 = Memory.Read<Vector3>(pBlip + 0x10);
                vector3.Z = vector3.Z == 20.0f ? -225.0f : vector3.Z + 1.0f;

                return vector3;
            }
        }

        return vector3;
    }

    /// <summary>
    /// 坐标向前微调
    /// </summary>
    /// <param name="distance">微调距离</param>
    public static void MoveFoward(float distance)
    {
        long pCPed = Globals.GetCPed();
        long pCNavigation = Memory.Read<long>(pCPed + Offsets.CPed_CNavigation);

        float head = Memory.Read<float>(pCNavigation + Offsets.CPed_CNavigation_RightX);
        float head2 = Memory.Read<float>(pCNavigation + Offsets.CPed_CNavigation_RightY);

        Vector3 vector3 = Memory.Read<Vector3>(pCPed + Offsets.CPed_VisualX);

        vector3.X -= head2 * distance;
        vector3.Y += head * distance;

        SetTeleportPosition(vector3);
    }

    /// <summary>
    /// 坐标向后微调
    /// </summary>
    /// <param name="distance">微调距离</param>
    public static void MoveBack(float distance)
    {
        long pCPed = Globals.GetCPed();
        long pCNavigation = Memory.Read<long>(pCPed + Offsets.CPed_CNavigation);

        float head = Memory.Read<float>(pCNavigation + Offsets.CPed_CNavigation_RightX);
        float head2 = Memory.Read<float>(pCNavigation + Offsets.CPed_CNavigation_RightY);

        Vector3 vector3 = Memory.Read<Vector3>(pCPed + Offsets.CPed_VisualX);

        vector3.X += head2 * distance;
        vector3.Y -= head * distance;

        SetTeleportPosition(vector3);
    }

    /// <summary>
    /// 坐标向左微调
    /// </summary>
    /// <param name="distance">微调距离</param>
    public static void MoveLeft(float distance)
    {
        long pCPed = Globals.GetCPed();
        long pCNavigation = Memory.Read<long>(pCPed + Offsets.CPed_CNavigation);

        float head2 = Memory.Read<float>(pCNavigation + Offsets.CPed_CNavigation_RightY);

        Vector3 vector3 = Memory.Read<Vector3>(pCPed + Offsets.CPed_VisualX);

        vector3.X += distance;
        vector3.Y -= head2 * distance;

        SetTeleportPosition(vector3);
    }

    /// <summary>
    /// 坐标向右微调
    /// </summary>
    /// <param name="distance">微调距离</param>
    public static void MoveRight(float distance)
    {
        long pCPed = Globals.GetCPed();
        long pCNavigation = Memory.Read<long>(pCPed + Offsets.CPed_CNavigation);

        float head2 = Memory.Read<float>(pCNavigation + Offsets.CPed_CNavigation_RightY);

        Vector3 vector3 = Memory.Read<Vector3>(pCPed + Offsets.CPed_VisualX);

        vector3.X -= distance;
        vector3.Y += head2 * distance;

        SetTeleportPosition(vector3);
    }

    /// <summary>
    /// 坐标向上微调
    /// </summary>
    /// <param name="distance">微调距离</param>
    public static void MoveUp(float distance)
    {
        Vector3 vector3 = GetPlayerPosition();
        vector3.Z += distance;
        SetTeleportPosition(vector3);
    }

    /// <summary>
    /// 坐标向下微调
    /// </summary>
    /// <param name="distance">微调距离</param>
    public static void MoveDown(float distance)
    {
        Vector3 vector3 = GetPlayerPosition();
        vector3.Z -= distance;
        SetTeleportPosition(vector3);
    }
}
