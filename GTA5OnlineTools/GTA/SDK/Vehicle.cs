using GTA5OnlineTools.GTA.Core;
using GTA5OnlineTools.GTA.Client;

namespace GTA5OnlineTools.GTA.SDK;

public static class Vehicle
{
    /// <summary>
    /// 玩家是否在载具中
    /// </summary>
    public static bool IsInVehicle()
    {
        long pCPed = Globals.GetCPed();
        return Memory.Read<byte>(pCPed + Offsets.CPed_InVehicle) == 0x01;
    }

    /// <summary>
    /// 载具无敌模式
    /// </summary>
    public static void GodMode(bool isEnable)
    {
        if (Globals.GetCVehicle(out long pCVehicle))
            Memory.Write(pCVehicle + Offsets.CPed_CVehicle_God, (byte)(isEnable ? 0x01 : 0x00));
    }

    /// <summary>
    /// 载具安全带
    /// </summary>
    public static void Seatbelt(bool isEnable)
    {
        long pCPed = Globals.GetCPed();
        Memory.Write(pCPed + Offsets.CPed_Seatbelt, (byte)(isEnable ? 0xC9 : 0xC8));
    }

    /// <summary>
    /// 载具隐形
    /// </summary>
    public static void Invisible(bool isEnable)
    {
        if (Globals.GetCVehicle(out long pCVehicle))
            Memory.Write(pCVehicle + Offsets.CPed_CVehicle_Invisible, (byte)(isEnable ? 0x01 : 0x27));
    }

    /// <summary>
    /// 载具附加功能
    /// </summary>
    public static void Extras(short flag)
    {
        if (Globals.GetCVehicle(out long pCVehicle))
        {
            long pCModelInfo = Memory.Read<long>(pCVehicle + Offsets.CPed_CVehicle_CModelInfo);
            Memory.Write(pCModelInfo + Offsets.CPed_CVehicle_CModelInfo_Extras, flag);
        }
    }

    /// <summary>
    /// 载具降落伞，关0，开1
    /// </summary>
    public static void Parachute(bool isEnable)
    {
        if (Globals.GetCVehicle(out long pCVehicle))
        {
            long pCModelInfo = Memory.Read<long>(pCVehicle + Offsets.CPed_CVehicle_CModelInfo);
            Memory.Write(pCModelInfo + Offsets.CPed_CVehicle_CModelInfo_Parachute, (byte)(isEnable ? 0x01 : 0x00));
        }
    }

    /// <summary>
    /// 补满载具血量
    /// </summary>
    public static void FillHealth()
    {
        if (Globals.GetCVehicle(out long pCVehicle))
        {
            float oVHealth = Memory.Read<float>(pCVehicle + Offsets.CPed_CVehicle_Health);
            float oVHealthMax = Memory.Read<float>(pCVehicle + Offsets.CPed_CVehicle_HealthMax);
            if (oVHealth <= oVHealthMax)
            {
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_Health, oVHealthMax);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthBody, oVHealthMax);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthPetrolTank, oVHealthMax);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthEngine, oVHealthMax);
            }
            else
            {
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_Health, 1000.0f);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthBody, 1000.0f);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthPetrolTank, 1000.0f);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_HealthEngine, 1000.0f);
            }
        }
    }

    /// <summary>
    /// 修复载具外观
    /// </summary>
    public static void Fix1stfoundBST()
    {
        Task.Run(async () =>
        {
            if (Globals.GetCVehicle(out long pCVehicle))
            {
                var offset = Memory.Read<long>(Pointers.GlobalPTR + 0x08 * 0x0A);

                Memory.Write(offset + 0x17BE28, 1);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_Health, 999.0f);

                await Task.Delay(300);

                long pCPickupData = Memory.Read<long>(Pointers.PickupDataPTR);
                int FixVehValue = Memory.Read<int>(pCPickupData + 0x228);       // pFixVeh
                int BSTValue = Memory.Read<int>(pCPickupData + 0x160);          // pBST

                long pCPickupInterface = Memory.Read<long>(Pointers.ReplayInterfacePTR);
                long pCReplayInterface_CPickupInterface = Memory.Read<long>(pCPickupInterface + Offsets.CReplayInterface_CPickupInterface);
                long mPickupCount = Memory.Read<int>(pCReplayInterface_CPickupInterface + 0x110);       // oPickupNum
                long pPickupList = Memory.Read<long>(pCReplayInterface_CPickupInterface + 0x100);       // pPickupList

                if (!Memory.IsValid(pPickupList))
                    return;

                for (long i = 0; i < mPickupCount; i++)
                {
                    long dwpPickup = Memory.Read<long>(pPickupList + i * 0x10);
                    int dwPickupValue = Memory.Read<int>(dwpPickup + 0x470);        // pDroppedPickupData

                    if (dwPickupValue == BSTValue)
                    {
                        Memory.Write(dwpPickup + 0x470, FixVehValue);

                        await Task.Delay(10);

                        Vector3 dwpPickupV3 = Memory.Read<Vector3>(dwpPickup + 0x90);
                        Vector3 vehicleV3 = Memory.Read<Vector3>(pCVehicle + Offsets.CPed_CVehicle_VisualX);

                        await Task.Delay(10);

                        Memory.Write(dwpPickup + 0x90, vehicleV3);
                    }
                }

                await Task.Delay(700);

                Memory.Write(offset + 0x17BE28, 1);
                Memory.Write(pCVehicle + Offsets.CPed_CVehicle_Health, 999.0f);

                if (Memory.Read<int>(offset + 0xA7B78) != 0)
                    Memory.Write(offset + 0xA7B78, -1);
            }
        });
    }

    /// <summary>
    /// 刷出线上载具
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="z255"></param>
    /// <param name="dist"></param>
    /// <param name="mod"></param>
    public static void SpawnVehicle(long hash, float z255, int dist, int[] mod)
    {
        Task.Run(() =>
        {
            if (hash != 0)
            {
                long pCPed = Globals.GetCPed();
                Vector3 vector3 = Memory.Read<Vector3>(pCPed + Offsets.CPed_VisualX);

                long pCNavigation = Memory.Read<long>(pCPed + Offsets.CPed_CNavigation);

                float sin = Memory.Read<float>(pCNavigation + Offsets.CPed_CNavigation_RightX);
                float cos = Memory.Read<float>(pCNavigation + Offsets.CPed_CNavigation_ForwardX);

                vector3.X += cos * dist;
                vector3.Y += sin * dist;

                if (z255 == -255.0f)
                    vector3.Z = z255;
                else
                    vector3.Z += z255;

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 66, hash);       // 载具哈希值

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 94, 2);          // personal car ownerflag  个人载具拥有者标志
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 95, 14);         // ownerflag  拥有者标志

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 5, -1);          // primary -1 auto 159  主色调
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 6, -1);          // secondary -1 auto 159  副色调

                Hacks.WriteGA(Offsets.oVMCreate + 7 + 0, vector3.X);    // 载具坐标x
                Hacks.WriteGA(Offsets.oVMCreate + 7 + 1, vector3.Y);    // 载具坐标y
                Hacks.WriteGA(Offsets.oVMCreate + 7 + 2, vector3.Z);    // 载具坐标z

                Hacks.WriteGAString(Offsets.oVMCreate + 27 + 1, Guid.NewGuid().ToString()[..8]);    // License plate  车牌

                for (int i = 0; i < 43; i++)
                {
                    if (i < 17)
                    {
                        Hacks.WriteGA(Offsets.oVMCreate + 27 + 10 + i, mod[i]);
                    }
                    else if (i >= 17 && i != 42)
                    {
                        Hacks.WriteGA(Offsets.oVMCreate + 27 + 10 + 6 + i, mod[i]);
                    }
                    else if (mod[42] > 0 && i == 42)
                    {
                        Hacks.WriteGA(Offsets.oVMCreate + 27 + 10 + 6 + 42, new Random().Next(1, mod[42] + 1));
                    }
                }

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 7, -1);      // pearlescent
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 8, -1);      // wheel color
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 33, -1);     // wheel selection
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 69, -1);     // Wheel type

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 28, 1);
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 30, 1);
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 32, 1);
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 65, 1);

                Hacks.WriteGA<long>(Offsets.oVMCreate + 27 + 77, 0xF0400200);   // vehstate  载具状态 没有这个载具起落架是收起状态

                Hacks.WriteGA(Offsets.oVMCreate + 5, 1);                        // can spawn flag must be odd
                Hacks.WriteGA(Offsets.oVMCreate + 2, 1);                        // spawn toggle gets reset to 0 on car spawn
            }
        });
    }

    /// <summary>
    /// 刷出线上载具
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="z255"></param>
    public static void SpawnVehicle(long hash, float z255)
    {
        Task.Run(() =>
        {
            if (hash != 0)
            {
                int dist = 5;

                const int pegasus = 0;

                long pCPed = Globals.GetCPed();
                Vector3 vector3 = Memory.Read<Vector3>(pCPed + Offsets.CPed_VisualX);

                long pCNavigation = Memory.Read<long>(pCPed + Offsets.CPed_CNavigation);

                float sin = Memory.Read<float>(pCNavigation + Offsets.CPed_CNavigation_RightX);
                float cos = Memory.Read<float>(pCNavigation + Offsets.CPed_CNavigation_ForwardX);

                vector3.X += cos * dist;
                vector3.Y += sin * dist;

                if (z255 == -255.0f)
                    vector3.Z = z255;
                else
                    vector3.Z += z255;

                Hacks.WriteGA(Offsets.oVMCreate + 7 + 0, vector3.X);            // 载具坐标x
                Hacks.WriteGA(Offsets.oVMCreate + 7 + 1, vector3.Y);            // 载具坐标y
                Hacks.WriteGA(Offsets.oVMCreate + 7 + 2, vector3.Z);            // 载具坐标z

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 66, hash);               // 载具哈希值
                Hacks.WriteGA(Offsets.oVMCreate + 3, pegasus);                  // 帕格萨斯

                Hacks.WriteGA(Offsets.oVMCreate + 5, 1);                        // can spawn flag must be odd
                Hacks.WriteGA(Offsets.oVMCreate + 2, 1);                        // spawn toggle gets reset to 0 on car spawn
                Hacks.WriteGAString(Offsets.oVMCreate + 27 + 1, Guid.NewGuid().ToString()[..8]);    // License plate  车牌

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 5, -1);          // primary -1 auto 159  主色调
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 6, -1);          // secondary -1 auto 159  副色调
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 7, -1);          // pearlescent
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 8, -1);          // wheel color

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 15, 1);          // primary weapon  主武器
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 19, -1);
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 20, 2);          // secondary weapon  副武器

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 21, 3);          // engine (0-3)  引擎
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 22, 6);          // brakes (0-6)  刹车
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 23, 9);          // transmission (0-9)  变速器
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 24, new Random().Next(0, 78));        // horn (0-77)  喇叭
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 25, 14);         // suspension (0-13)  悬吊系统
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 26, 19);         // armor (0-18)  装甲
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 27, 1);          // turbo (0-1)  涡轮增压
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 28, 1);          // weaponised ownerflag

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 30, 1);
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 32, new Random().Next(0, 15));       // colored light (0-14)
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 33, -1);                             // Wheel Selection

                Hacks.WriteGA<long>(Offsets.oVMCreate + 27 + 60, 1);    // landinggear/vehstate 起落架/载具状态

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 62, new Random().Next(0, 256));      // Tire smoke color R
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 63, new Random().Next(0, 256));      // Green Neon Amount 1-255 100%-0%
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 64, new Random().Next(0, 256));      // Blue Neon Amount 1-255 100%-0%
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 65, new Random().Next(0, 7));        // Window tint 0-6
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 67, 1);          // Livery
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 69, -1);         // Wheel type

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 74, new Random().Next(0, 256));      // Red Neon Amount 1-255 100%-0%
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 75, new Random().Next(0, 256));      // G
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 76, new Random().Next(0, 256));      // B

                Hacks.WriteGA<long>(Offsets.oVMCreate + 27 + 77, 4030726305);                       // vehstate  载具状态 没有这个载具起落架是收起状态
                Memory.Write<byte>(Hacks.ReadGA<long>(Offsets.oVMCreate + 27 + 77) + 1, 0x02);      // 2:bulletproof 0:false  防弹的

                Hacks.WriteGA(Offsets.oVMCreate + 27 + 95, 14);         // ownerflag  拥有者标志
                Hacks.WriteGA(Offsets.oVMCreate + 27 + 94, 2);          // personal car ownerflag  个人载具拥有者标志
            }
        });
    }

    /// <summary>
    /// 请求个人载具
    /// </summary>
    /// <param name="index"></param>
    public static void RequestPersonalVehicle(int index)
    {
        Hacks.WriteGA(Offsets.RequestPersonalVehicle1, index);
        Hacks.WriteGA(Offsets.RequestPersonalVehicle2, 1);
    }

    /// <summary>
    /// 查找载具显示名称
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="isDisplay"></param>
    /// <returns></returns>
    public static string FindVehicleDisplayName(long hash, bool isDisplay)
    {
        foreach (var item in VehicleData.VehicleClassData)
        {
            foreach (var item0 in item.VehicleInfo)
            {
                if (item0.Hash == hash)
                {
                    if (isDisplay)
                        return item0.Display;
                    else
                        return item0.Name;
                }
            }
        }

        return string.Empty;
    }
}
