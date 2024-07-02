using GTA5Core.Native;
using GTA5Core.Offsets;

namespace GTA5Core.Features;

public static class Vehicle
{
    /// <summary>
    /// 玩家是否在载具中
    /// </summary>
    public static bool IsInVehicle(long pCPed)
    {
        return Memory.Read<byte>(pCPed + CPed.InVehicle) == 0x01;
    }

    /// <summary>
    /// 玩家是否在载具中
    /// </summary>
    public static bool IsInVehicle()
    {
        var pCPed = Game.GetCPed();
        if (!Memory.IsValid(pCPed))
            return false;

        return IsInVehicle(pCPed);
    }

    /// <summary>
    /// 载具无敌模式
    /// </summary>
    public static void GodMode(bool isEnable)
    {
        if (Game.GetCVehicle(out long pCVehicle))
            Memory.Write(pCVehicle + CVehicle.God, (byte)(isEnable ? 0x01 : 0x00));
    }

    /// <summary>
    /// 载具安全带
    /// </summary>
    public static void Seatbelt(bool isEnable)
    {
        var pCPed = Game.GetCPed();
        if (!Memory.IsValid(pCPed))
            return;

        Memory.Write(pCPed + CPed.Seatbelt, (byte)(isEnable ? 0xC9 : 0xC8));
    }

    /// <summary>
    /// 载具隐形
    /// </summary>
    public static void Invisible(bool isEnable)
    {
        if (Game.GetCVehicle(out long pCVehicle))
            Memory.Write(pCVehicle + CVehicle.Invisible, (byte)(isEnable ? 0x01 : 0x27));
    }

    /// <summary>
    /// 载具附加功能
    /// </summary>
    public static void Extras(short flag)
    {
        if (Game.GetCVehicle(out long pCVehicle))
        {
            var pCModelInfo = Memory.Read<long>(pCVehicle + CVehicle.CModelInfo);
            Memory.Write(pCModelInfo + CModelInfo.Extras, flag);
        }
    }

    /// <summary>
    /// 载具降落伞，关0，开1
    /// </summary>
    public static void Parachute(bool isEnable)
    {
        if (Game.GetCVehicle(out long pCVehicle))
        {
            var pCModelInfo = Memory.Read<long>(pCVehicle + CVehicle.CModelInfo);
            Memory.Write(pCModelInfo + CModelInfo.Parachute, (byte)(isEnable ? 0x01 : 0x00));
        }
    }

    /// <summary>
    /// 补满载具血量
    /// </summary>
    public static void FillHealth()
    {
        if (Game.GetCVehicle(out long pCVehicle))
        {
            var oVHealth = Memory.Read<float>(pCVehicle + CVehicle.Health);
            var oVHealthMax = Memory.Read<float>(pCVehicle + CVehicle.HealthMax);

            if (oVHealth <= oVHealthMax)
            {
                Memory.Write(pCVehicle + CVehicle.Health, oVHealthMax);
                Memory.Write(pCVehicle + CVehicle.HealthBody, oVHealthMax);
                Memory.Write(pCVehicle + CVehicle.HealthPetrolTank, oVHealthMax);
                Memory.Write(pCVehicle + CVehicle.HealthEngine, oVHealthMax);
            }
            else
            {
                Memory.Write(pCVehicle + CVehicle.Health, 1000.0f);
                Memory.Write(pCVehicle + CVehicle.HealthBody, 1000.0f);
                Memory.Write(pCVehicle + CVehicle.HealthPetrolTank, 1000.0f);
                Memory.Write(pCVehicle + CVehicle.HealthEngine, 1000.0f);
            }
        }
    }

    /// <summary>
    /// 修复载具外观
    /// </summary>
    public static async Task FixVehicleByBST()
    {
        await Task.Run(async () =>
        {
            if (Game.GetCVehicle(out long pCVehicle))
            {
                Globals.Set_Global_Value(Base.oVMYCar + 899, 1);       // if (!NETWORK::NETWORK_IS_SCRIPT_ACTIVE("AM_BRU_BOX", PLAYER::PLAYER_ID(), true, 0))

                await Task.Delay(1000);

                var pCPickupData = Memory.Read<long>(Pointers.PickupDataPTR);
                var FixVehValue = Memory.Read<long>(pCPickupData + 0x230);       // pFixVeh
                var BSTValue = Memory.Read<long>(pCPickupData + 0x160);          // pBST

                Memory.Write(pCVehicle + CVehicle.Health, 999.0f);

                var pCPickupInterface = Memory.Read<long>(Pointers.ReplayInterfacePTR);
                var pCReplayInterface_CPickupInterface = Memory.Read<long>(pCPickupInterface + CReplayInterface.CPickupInterface);

                var mPickupCount = Memory.Read<int>(pCReplayInterface_CPickupInterface + 0x110);       // oPickupNum
                var pPickupList = Memory.Read<long>(pCReplayInterface_CPickupInterface + 0x100);       // pPickupList

                await Task.Delay(100);

                for (var i = 0; i < mPickupCount; i++)
                {
                    var dwpPickup = Memory.Read<long>(pPickupList + i * 0x10);
                    if (!Memory.IsValid(dwpPickup))
                        continue;

                    var dwPickupValue = Memory.Read<long>(dwpPickup + 0x470);        // pDroppedPickupData
                    if (!Memory.IsValid(dwPickupValue))
                        continue;

                    if (dwPickupValue == BSTValue || dwPickupValue == FixVehValue)
                    {
                        Memory.Write(dwpPickup + 0x470, FixVehValue);

                        await Task.Delay(10);

                        Vector3 dwpPickupV3 = Memory.Read<Vector3>(dwpPickup + 0x90);
                        Vector3 vehicleV3 = Memory.Read<Vector3>(pCVehicle + CVehicle.VisualX);

                        await Task.Delay(10);

                        Memory.Write(dwpPickup + 0x90, vehicleV3);      // oVPositionX
                        Memory.Write(pCVehicle + 0x9D8, 0.0f);          // oVDirt  float  Wash Vehicle
                    }
                }

                await Task.Delay(1000);

                if (Globals.Get_Global_Value<int>(Base.oNETTimeHelp + 3690) != 0)
                    Globals.Set_Global_Value(Base.oNETTimeHelp + 3690, -1);
            }
        });
    }

    /// <summary>
    /// 请求个人载具
    /// </summary>
    /// <param name="index"></param>
    public static void RequestPersonalVehicle(int index)
    {
        Globals.Set_Global_Value(Base.oVMYCar + 1007, index);
        Globals.Set_Global_Value(Base.oVMYCar + 1004, 1);
    }

    /// <summary>
    /// 提前解锁1.69新载具（避免刷出消失）
    /// </summary>
    public static void UnlockDlcVehicle()
    {
        for (int i = 0; i <= 29; i++)
        {
            Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("LAUNCHPOSIX_VEHICLE_" + i.ToString())), 0);
        }
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_CASTIGATOR")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_COQUETTE5")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_DOMINATOR10")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_ENVISAGE")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_EUROSX32")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_NIOBE")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_PARAGON3")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_PIPISTRELLO")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_PIZZABOY")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_POLDORADO")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_POLGREENWOOD")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_POLDOMINATOR10")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_POLIMPALER5")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_POLIMPALER6")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_VORSCHLAGHAMMER")), 1);
        Globals.Set_Global_Value(Tunables.Index(RAGE.JOAAT("ENABLE_VEHICLE_YOSEMITE1500")), 1);

        // 来源: https://www.unknowncheats.me/forum/3936713-post229.html
    }
}
