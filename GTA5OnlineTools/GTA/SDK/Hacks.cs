using GTA5OnlineTools.GTA.Core;

namespace GTA5OnlineTools.GTA.SDK;

public static class Hacks
{
    /// <summary>
    /// 全局地址
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static long GlobalAddress(int index)
    {
        return Memory.Read<long>(Pointers.GlobalPTR + 0x8 * ((index >> 0x12) & 0x3F)) + 8 * (index & 0x3FFFF);
    }

    /// <summary>
    /// 泛型读取全局地址
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <returns></returns>
    public static T ReadGA<T>(int index) where T : struct
    {
        return Memory.Read<T>(GlobalAddress(index));
    }

    /// <summary>
    /// 泛型写入全局地址
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <param name="vaule"></param>
    public static void WriteGA<T>(int index, T vaule) where T : struct
    {
        Memory.Write(GlobalAddress(index), vaule);
    }

    /// <summary>
    /// 读取全局地址字符串
    /// </summary>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string ReadGAString(int index, int length = 20)
    {
        return Memory.ReadString(GlobalAddress(index), length);
    }

    /// <summary>
    /// 写入全局地址字符串
    /// </summary>
    /// <param name="index"></param>
    /// <param name="str"></param>
    public static void WriteGAString(int index, string str)
    {
        Memory.WriteString(GlobalAddress(index), str);
    }

    /////////////////////////////////////////////////////

    /// <summary>
    /// 判断是否在线上模式
    /// </summary>
    /// <returns></returns>
    public static bool IsOnlineMode()
    {
        int character_slot = ReadGA<int>(113386 + 2363 + 539 + 4321);
        return (character_slot == 0 || character_slot == 1 || character_slot == 2) == false;
    }

    /// <summary>
    /// 获取网络时间
    /// </summary>
    /// <returns></returns>
    public static int GetNetworkTime()
    {
        return ReadGA<int>(1574757 + 11);
    }

    /// <summary>
    /// 获取玩家ID
    /// </summary>
    /// <returns></returns>
    public static int GetPlayerID()
    {
        return ReadGA<int>(Offsets.oPlayerGA);
    }

    /// <summary>
    /// 获取游戏内产业索引
    /// </summary>
    /// <param name="ID">范围：0~5</param>
    /// <returns></returns>
    public static int GetBusinessIndex(int ID)
    {
        return 1853131 + 1 + (GetPlayerID() * 888) + 267 + 187 + 1 + (ID * 13);
    }

    /////////////////////////////////////////

    /// <summary>
    /// 字符串转Hash值
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static uint Joaat(string input)
    {
        uint num1 = 0U;
        input = input.ToLower();
        foreach (char c in input)
        {
            uint num2 = num1 + c;
            uint num3 = num2 + (num2 << 10);
            num1 = num3 ^ num3 >> 6;
        }
        uint num4 = num1 + (num1 << 3);
        uint num5 = num4 ^ num4 >> 11;

        return num5 + (num5 << 15);
    }

    /// <summary>
    /// 写入stat值，只支持int类型
    /// </summary>
    public static void STATS_WriteInt(string hash, int value)
    {
        if (hash.IndexOf("_") == 0)
        {
            int stat_MP = ReadGA<int>(1574918);
            hash = $"MP{stat_MP}{hash}";
        }

        uint oldhash = ReadGA<uint>(1655453 + 4);
        int oldvalue = ReadGA<int>(1010831 + 5525);

        WriteGA(1665454 + 4, Joaat(hash));
        WriteGA(1010831 + 5525, value);
        WriteGA(1653913 + 1139, -1);
        Thread.Sleep(1000);
        WriteGA(1665454 + 4, oldhash);
        WriteGA(1010831 + 5525, oldvalue);
    }

    /////////////////////////////////////////

    /// <summary>
    /// 掉落物品
    /// </summary>
    public static void CreateAmbientPickup(string pickup)
    {
        if (string.IsNullOrEmpty(pickup))
            return;

        uint pickupHash = Joaat(pickup);

        Vector3 vector3 = Teleport.GetPlayerPosition();

        WriteGA(2764230 + 3, vector3.X);
        WriteGA(2764230 + 4, vector3.Y);
        WriteGA(2764230 + 5, vector3.Z + 3.0f);
        WriteGA(2764230 + 1, 9999);

        WriteGA(4535172 + 1 + (ReadGA<int>(2764230) * 85) + 66 + 2, 2);
        WriteGA(2764230 + 6, 1);

        Thread.Sleep(200);

        long pReplayInterface = Memory.Read<long>(Pointers.ReplayInterfacePTR);
        long pCPickupInterface = Memory.Read<long>(pReplayInterface + 0x20);    // pCPickupInterface

        long oPickupNum = Memory.Read<long>(pCPickupInterface + 0x110);         // oPickupNum
        long pPickupList = Memory.Read<long>(pCPickupInterface + 0x100);        // pPickupList

        for (long i = 0; i < oPickupNum; i++)
        {
            long dwpPickup = Memory.Read<long>(pPickupList + i * 0x10);
            uint dwPickupHash = Memory.Read<uint>(dwpPickup + 0x468);

            if (dwPickupHash == 4263048111)
            {
                Memory.Write(dwpPickup + 0x468, pickupHash);
                break;
            }
        }
    }
}
