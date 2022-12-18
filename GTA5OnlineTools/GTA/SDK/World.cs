using GTA5OnlineTools.GTA.Core;
using GTA5OnlineTools.GTA.Client;

namespace GTA5OnlineTools.GTA.SDK;

public static class World
{
    /// <summary>
    /// 设置本地天气
    /// 
    /// -1, Default
    ///  0, Extra Sunny
    ///  1, Clear
    ///  2, Clouds
    ///  3, Smog
    ///  4, Foggy
    ///  5, Overcast
    ///  6, Rain
    ///  7, Thunder
    ///  8, Light Rain
    ///  9, Smoggy Light Rain
    /// 10, Very Light Snow
    /// 11, Windy Snow
    /// 12, Light Snow
    /// 14, Halloween
    /// </summary>
    /// <param name="weatherID">天气ID</param>
    public static void Set_Local_Weather(int weatherID)
    {
        if (weatherID == -1)
        {
            Memory.Write(Pointers.WeatherPTR + 0x24, -1);
            Memory.Write(Pointers.WeatherPTR + 0x104, 13);
        }
        if (weatherID == 13)
        {
            Memory.Write(Pointers.WeatherPTR + 0x24, 13);
            Memory.Write(Pointers.WeatherPTR + 0x104, 13);
        }

        Memory.Write(Pointers.WeatherPTR + 0x104, weatherID);
    }

    /// <summary>
    /// 杀死全部NPC（仅敌对？）
    /// </summary>
    public static void KillAllNPC(bool isOnlyKillHostility)
    {
        long pCPedList = Globals.GetCPedList();

        for (int i = 0; i < Offsets.oMaxPeds; i++)
        {
            long pCPed = Memory.Read<long>(pCPedList + i * 0x10);
            if (!Memory.IsValid(pCPed))
                continue;

            // 跳过玩家
            long pCPlayerInfo = Memory.Read<long>(pCPed + Offsets.CPed_CPlayerInfo);
            if (Memory.IsValid(pCPlayerInfo))
                continue;

            if (isOnlyKillHostility)
            {
                byte oHostility = Memory.Read<byte>(pCPed + Offsets.CPed_Hostility);
                if (oHostility > 0x01)
                {
                    Memory.Write(pCPed + Offsets.CPed_Health, 0.0f);
                }
            }
            else
            {
                Memory.Write(pCPed + Offsets.CPed_Health, 0.0f);
            }
        }
    }

    /// <summary>
    /// 杀死全部警察
    /// </summary>
    public static void KillAllPolice()
    {
        long pCPedList = Globals.GetCPedList();

        for (int i = 0; i < Offsets.oMaxPeds; i++)
        {
            long pCPed = Memory.Read<long>(pCPedList + i * 0x10);
            if (!Memory.IsValid(pCPed))
                continue;

            // 跳过玩家
            long pCPlayerInfo = Memory.Read<long>(pCPed + Offsets.CPed_CPlayerInfo);
            if (Memory.IsValid(pCPlayerInfo))
                continue;

            int ped_type = Memory.Read<int>(pCPed + Offsets.CPed_Ragdoll);
            ped_type = ped_type << 11 >> 25;

            if (ped_type == (int)EnumData.PedTypes.COP ||
                ped_type == (int)EnumData.PedTypes.SWAT ||
                ped_type == (int)EnumData.PedTypes.ARMY)
            {
                Memory.Write(pCPed + Offsets.CPed_Health, 0.0f);
            }
        }
    }

    /// <summary>
    /// 摧毁全部NPC载具（仅敌对？）
    /// </summary>
    public static void DestroyAllNPCVehicles(bool isOnlyKillHostility)
    {
        long pCPedList = Globals.GetCPedList();

        for (int i = 0; i < Offsets.oMaxPeds; i++)
        {
            long pCPed = Memory.Read<long>(pCPedList + i * 0x10);
            if (!Memory.IsValid(pCPed))
                continue;

            // 跳过玩家
            long pCPlayerInfo = Memory.Read<long>(pCPed + Offsets.CPed_CPlayerInfo);
            if (Memory.IsValid(pCPlayerInfo))
                continue;

            long pCVehicle = Memory.Read<long>(pCPed + Offsets.CPed_CVehicle);
            if (!Memory.IsValid(pCVehicle))
                continue;

            if (isOnlyKillHostility)
            {
                byte oHostility = Memory.Read<byte>(pCPed + Offsets.CPed_Hostility);
                if (oHostility > 0x01)
                {
                    Memory.Write(pCVehicle + Offsets.CPed_CVehicle_Health, -1.0f);
                    Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthBody, -1.0f);
                    Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthPetrolTank, -1.0f);
                    Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthEngine, -1.0f);
                }
            }
            else
            {
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_Health, -1.0f);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthBody, -1.0f);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthPetrolTank, -1.0f);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthEngine, -1.0f);
            }
        }
    }

    /// <summary>
    /// 摧毁全部载具（玩家自己的载具也会摧毁）
    /// </summary>
    public static void DestroyAllVehicles()
    {
        long pCVehicleList = Globals.GetCVehicleList();

        for (int i = 0; i < Offsets.oMaxPeds; i++)
        {
            long pCVehicle = Memory.Read<long>(pCVehicleList + i * 0x10);
            if (!Memory.IsValid(pCVehicle))
                continue;

            Memory.Write(pCVehicle + Offsets.CPed_CVehicle_Health, -1.0f);
            Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthBody, -1.0f);
            Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthPetrolTank, -1.0f);
            Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthEngine, -1.0f);
        }
    }

    /// <summary>
    /// 传送全部NPC到我这里，仅敌对？
    /// </summary>
    public static void TeleportAllNPCToMe(bool isOnlyHostility)
    {
        Vector3 v3MyPos = Teleport.GetPlayerPosition();

        long pCPedList = Globals.GetCPedList();

        for (int i = 0; i < Offsets.oMaxPeds; i++)
        {
            long pCPed = Memory.Read<long>(pCPedList + i * 0x10);
            if (!Memory.IsValid(pCPed))
                continue;

            // 跳过玩家
            long pCPlayerInfo = Memory.Read<long>(pCPed + Offsets.CPed_CPlayerInfo);
            if (Memory.IsValid(pCPlayerInfo))
                continue;

            long pCNavigation = Memory.Read<long>(pCPed + Offsets.CPed_CNavigation);
            if (!Memory.IsValid(pCNavigation))
                continue;

            if (isOnlyHostility)
            {
                byte oHostility = Memory.Read<byte>(pCPed + Offsets.CPed_Hostility);
                if (oHostility > 0x01)
                {
                    Memory.Write(pCPed + Offsets.CPed_CVehicle_VisualX, v3MyPos);
                    Memory.Write(pCNavigation + Offsets.CPed_CVehicle_CNavigation_PositionX, v3MyPos);
                }
            }
            else
            {
                Memory.Write(pCPed + Offsets.CPed_CVehicle_VisualX, v3MyPos);
                Memory.Write(pCNavigation + Offsets.CPed_CVehicle_CNavigation_PositionX, v3MyPos);
            }
        }
    }
}
