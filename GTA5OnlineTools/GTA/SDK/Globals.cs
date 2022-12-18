using GTA5OnlineTools.GTA.Core;

namespace GTA5OnlineTools.GTA.SDK;

public static class Globals
{
    /// <summary>
    /// 获取 CPed 指针
    /// </summary>
    /// <returns></returns>
    public static long GetCPed()
    {
        long pCPedFactory = Memory.Read<long>(Pointers.WorldPTR);
        return Memory.Read<long>(pCPedFactory + Offsets.CPed);
    }

    /// <summary>
    /// 获取 CPlayerInfo 指针
    /// </summary>
    /// <returns></returns>
    public static long GetCPlayerInfo()
    {
        long pCPed = GetCPed();
        return Memory.Read<long>(pCPed + Offsets.CPed_CPlayerInfo);
    }

    /// <summary>
    /// 获取 CVehicle 指针
    /// </summary>
    /// <param name="pCVehicle"></param>
    /// <returns></returns>
    public static bool GetCVehicle(out long pCVehicle)
    {
        long pCPed = GetCPed();
        byte oInVehicle = Memory.Read<byte>(pCPed + Offsets.CPed_InVehicle);

        if (oInVehicle == 0x01)
        {
            pCVehicle = Memory.Read<long>(pCPed + Offsets.CPed_CVehicle);
            return true;
        }

        pCVehicle = 0;
        return false;
    }

    /// <summary>
    /// 获取 CPedList 指针
    /// </summary>
    /// <returns></returns>
    public static long GetCPedList()
    {
        long pCReplayInterface = Memory.Read<long>(Pointers.ReplayInterfacePTR);
        long pCPedInterface = Memory.Read<long>(pCReplayInterface + Offsets.CReplayInterface_CPedInterface);
        return Memory.Read<long>(pCPedInterface + Offsets.CReplayInterface_CPedInterface_CPedList);
    }

    /// <summary>
    /// 获取 CVehicleList 指针
    /// </summary>
    /// <returns></returns>
    public static long GetCVehicleList()
    {
        long pCReplayInterface = Memory.Read<long>(Pointers.ReplayInterfacePTR);
        long pCVehicleInterface = Memory.Read<long>(pCReplayInterface + Offsets.CReplayInterface_CVehicleInterface);
        return Memory.Read<long>(pCVehicleInterface + Offsets.CReplayInterface_CVehicleInterface_CVehicleList);
    }
}
