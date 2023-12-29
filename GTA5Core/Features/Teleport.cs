using GTA5Core.Native;
using GTA5Core.Offsets;

namespace GTA5Core.Features;

public static class Teleport
{

    /// <summary>
    /// 获取玩家当前坐标
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetPlayerPosition()
    {
        var pCPed = Game.GetCPed();
        return Memory.Read<Vector3>(pCPed + CPed.VisualX);
    }

    /// <summary>
    /// 获取准星当前坐标
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetCrossHairPosition()
    {
        var pCPlayerInfo = Game.GetCPlayerInfo();
        return Memory.Read<Vector3>(pCPlayerInfo + CPlayerInfo.CrossHairX);
    }

    /// <summary>
    /// 传送到准星坐标
    /// </summary>
    public static void ToCrossHair()
    {
        SetTeleportPosition(GetCrossHairPosition());
    }

    /// <summary>
    /// 传送到导航点
    /// </summary>
    public static void ToWaypoint()
    {
        SetTeleportPosition(GetWaypointPosition());
    }

    /// <summary>
    /// 传送到目标点
    /// </summary>
    public static void ToObjective()
    {
        SetTeleportPosition(GetObjectivePosition());
    }

    /// <summary>
    /// 传送到Blips
    /// </summary>
    public static void ToBlips(int blipId, byte blipColor = 0)
    {
        Vector3 vector3;

        if (blipColor == 0)
            vector3 = GetBlipPosition(new int[] { blipId });
        else
            vector3 = GetBlipPosition(new int[] { blipId }, new byte[] { blipColor });

        SetTeleportPosition(vector3);
    }

    /// <summary>
    /// 坐标传送功能
    /// </summary>
    public static void SetTeleportPosition(Vector3 vector3)
    {
        if (vector3 == Vector3.Zero)
            return;

        // 禁用越界死亡
        Globals.Set_Global_Value(2738587 + 6920, 1);     // freemode - joaat("TRI_WARP")

        var pCPed = Game.GetCPed();

        if (Vehicle.IsInVehicle(pCPed))
        {
            // 玩家在载具
            var pCVehicle = Memory.Read<long>(pCPed + CPed.CVehicle);
            Memory.Write(pCVehicle + CVehicle.VisualX, vector3);

            var pCNavigation = Memory.Read<long>(pCVehicle + CVehicle.CNavigation);
            Memory.Write(pCNavigation + CNavigation.PositionX, vector3);
        }
        else
        {
            // 玩家不在载具
            var pCNavigation = Memory.Read<long>(pCPed + CPed.CNavigation);

            Memory.Write(pCPed + CPed.VisualX, vector3);
            Memory.Write(pCNavigation + CNavigation.PositionX, vector3);
        }
    }

    /// <summary>
    /// 获取Blip坐标
    /// </summary>
    public static Vector3 GetBlipPosition(int[] blipIds, byte[] blipColors = null)
    {
        if (blipIds is null || blipIds.Length == 0)
            return Vector3.Zero;

        var isIgnoreColor = false;
        if (blipColors is null || blipColors.Length == 0)
            isIgnoreColor = true;

        for (var i = 1; i <= 2000; i++)
        {
            var pBlip = Memory.Read<long>(Pointers.BlipPTR + i * 0x08);
            if (!Memory.IsValid(pBlip))
                continue;

            var dwIcon = Memory.Read<int>(pBlip + 0x40);
            var dwColor = Memory.Read<byte>(pBlip + 0x48);

            if (isIgnoreColor)
            {
                if (!blipIds.Contains(dwIcon))
                    continue;
            }
            else
            {
                if (!blipIds.Contains(dwIcon) ||
                    !blipColors.Contains(dwColor))
                    continue;
            }

            var vector3 = Memory.Read<Vector3>(pBlip + 0x10);
            vector3.Z = vector3.Z == 20.0f ? -225.0f : vector3.Z + 1.0f;

            return vector3;
        }

        return Vector3.Zero;
    }

    /// <summary>
    /// 获取导航点坐标
    /// </summary>
    public static Vector3 GetWaypointPosition()
    {
        return GetBlipPosition(new int[] { 8 }, new byte[] { 84 });
    }

    /// <summary>
    /// 获取目标点坐标
    /// </summary>
    public static Vector3 GetObjectivePosition()
    {
        Vector3 vector3;

        vector3 = GetBlipPosition(new int[] { 1 }, new byte[] { 5, 60, 66 });
        if (vector3 != Vector3.Zero)
            return vector3;

        vector3 = GetBlipPosition(new int[] { 1, 225, 427, 478, 501, 523, 556 }, new byte[] { 1, 2, 3, 54, 78 });
        if (vector3 != Vector3.Zero)
            return vector3;

        vector3 = GetBlipPosition(new int[] { 432, 443 }, new byte[] { 59 });
        if (vector3 != Vector3.Zero)
            return vector3;

        return vector3;
    }

    /// <summary>
    /// 坐标向前微调
    /// </summary>
    public static void MoveFoward(float distance)
    {
        var pCPed = Game.GetCPed();
        var pCNavigation = Memory.Read<long>(pCPed + CPed.CNavigation);

        var head = Memory.Read<float>(pCNavigation + CNavigation.RightX);
        var head2 = Memory.Read<float>(pCNavigation + CNavigation.RightY);

        var vector3 = Memory.Read<Vector3>(pCPed + CPed.VisualX);

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
        var pCPed = Game.GetCPed();
        var pCNavigation = Memory.Read<long>(pCPed + CPed.CNavigation);

        var head = Memory.Read<float>(pCNavigation + CNavigation.RightX);
        var head2 = Memory.Read<float>(pCNavigation + CNavigation.RightY);

        var vector3 = Memory.Read<Vector3>(pCPed + CPed.VisualX);

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
        var pCPed = Game.GetCPed();
        var pCNavigation = Memory.Read<long>(pCPed + CPed.CNavigation);

        var head2 = Memory.Read<float>(pCNavigation + CNavigation.RightY);

        var vector3 = Memory.Read<Vector3>(pCPed + CPed.VisualX);

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
        var pCPed = Game.GetCPed();
        var pCNavigation = Memory.Read<long>(pCPed + CPed.CNavigation);

        var head2 = Memory.Read<float>(pCNavigation + CNavigation.RightY);

        var vector3 = Memory.Read<Vector3>(pCPed + CPed.VisualX);

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
        var vector3 = GetPlayerPosition();
        vector3.Z += distance;

        SetTeleportPosition(vector3);
    }

    /// <summary>
    /// 坐标向下微调
    /// </summary>
    /// <param name="distance">微调距离</param>
    public static void MoveDown(float distance)
    {
        var vector3 = GetPlayerPosition();
        vector3.Z -= distance;

        SetTeleportPosition(vector3);
    }
}
