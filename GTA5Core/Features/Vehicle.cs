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
        Globals.Set_Global_Value(Base.oVMYCar + 992, index);
        Globals.Set_Global_Value(Base.oVMYCar + 989, 1);
    }

    /// <summary>
    /// 提前解锁1.67 and 1.68新载具（避免刷出消失）
    /// </summary>
    public static void Unlock168Vehicle()
    {
        Globals.Set_Global_Value(Base.Default + 35462, 1);         // walton
        Globals.Set_Global_Value(Base.Default + 35463, 1);         // vapid ratel
        Globals.Set_Global_Value(Base.Default + 35464, 1);         // maibatsu
        Globals.Set_Global_Value(Base.Default + 35465, 1);         // vapid vagon
        Globals.Set_Global_Value(Base.Default + 35466, 1);         // stinger
        Globals.Set_Global_Value(Base.Default + 35467, 1);         // streame216
        Globals.Set_Global_Value(Base.Default + 35468, 1);         // f-160
        Globals.Set_Global_Value(Base.Default + 35469, 1);         // buffalo5
        Globals.Set_Global_Value(Base.Default + 35471, 1);         // penaud
        Globals.Set_Global_Value(Base.Default + 35472, 1);         // conada
        Globals.Set_Global_Value(Base.Default + 35473, 1);         // bravodo hotring
        Globals.Set_Global_Value(Base.Default + 35474, 1);         // albany brigrham

        Globals.Set_Global_Value(Base.Default + 36301, 1);         // enable_vehicle_baller8
        Globals.Set_Global_Value(Base.Default + 36286, 1);         // enable_vehicle_turismo3
        Globals.Set_Global_Value(Base.Default + 36291, 1);         // enable_vehicle_aleutian
        Globals.Set_Global_Value(Base.Default + 36302, 1);         // enable_vehicle_terminus
        Globals.Set_Global_Value(Base.Default + 36296, 1);         // enable_vehicle_impaler6
        Globals.Set_Global_Value(Base.Default + 36295, 1);         // enable_vehicle_cavalcade
        Globals.Set_Global_Value(Base.Default + 36300, 1);         // enable_vehicle_dorado
        Globals.Set_Global_Value(Base.Default + 36293, 1);         // enable_vehicle_asterope2
        Globals.Set_Global_Value(Base.Default + 36296, 1);         // enable_vehicle_impaler5
        Globals.Set_Global_Value(Base.Default + 36298, 1);         // enable_vehicle_vivanite
        Globals.Set_Global_Value(Base.Default + 36292, 1);         // enable_vehicle_dominator9
        Globals.Set_Global_Value(Base.Default + 36287 , 1);         // enable_vehicle_vigero3
        Globals.Set_Global_Value(Base.Default + 36285 , 1);         // enable_vehicle_fr36
        Globals.Set_Global_Value(Base.Default + 36297 , 1);         // enable_vehicle_polgauntlet
        Globals.Set_Global_Value(Base.Default + 36304 , 1);         // enable_vehicle_benson2
        Globals.Set_Global_Value(Base.Default + 36303 , 1);         // enable_vehicle_boxville6
        Globals.Set_Global_Value(Base.Default + 36289 , 1);         // enable_vehicle_police4
        Globals.Set_Global_Value(Base.Default + 36288 , 1);         // enable_vehicle_police5
        Globals.Set_Global_Value(Base.Default + 36290 , 1);         // enable_vehicle_riot
        // 来源: https://www.unknowncheats.me/forum/3936713-post229.html
    }
}
