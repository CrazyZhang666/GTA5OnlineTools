using GTA5Core.Native;
using GTA5Core.Offsets;

namespace GTA5Core.Features;

public static class Globals
{
    /// <summary>
    /// 全局地址
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static long GlobalAddress(int index)
    {
        return Memory.Read<long>(Pointers.GlobalPTR + 0x8 * (index >> 0x12 & 0x3F)) + 8 * (index & 0x3FFFF);
    }

    /// <summary>
    /// 泛型读取全局地址
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <returns></returns>
    public static T Get_Global_Value<T>(int index) where T : struct
    {
        return Memory.Read<T>(GlobalAddress(index));
    }

    /// <summary>
    /// 泛型写入全局地址
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <param name="vaule"></param>
    public static void Set_Global_Value<T>(int index, T vaule) where T : struct
    {
        Memory.Write(GlobalAddress(index), vaule);
    }

    /// <summary>
    /// 读取全局地址字符串
    /// </summary>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string Get_Global_String(int index, int length = 20)
    {
        return Memory.ReadString(GlobalAddress(index), length);
    }

    /// <summary>
    /// 写入全局地址字符串
    /// </summary>
    /// <param name="index"></param>
    /// <param name="str"></param>
    public static void Set_Global_String(int index, string str)
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
        return Get_Global_Value<int>(114370 + 2366 + 539 + 4321) == 145;
    }

    /// <summary>
    /// 获取网络时间
    /// </summary>
    /// <returns></returns>
    public static int GetNetworkTime()
    {
        return Get_Global_Value<int>(1574764 + 11);
    }

    /// <summary>
    /// 获取玩家ID
    /// </summary>
    /// <returns></returns>
    public static int GetPlayerID()
    {
        return Get_Global_Value<int>(Base.oPlayerGA);
    }

    /// <summary>
    /// 获取玩家角色索引
    /// </summary>
    /// <returns></returns>
    public static int GetPlayerIndex()
    {
        return Get_Global_Value<int>(1574925);
    }

    /// <summary>
    /// 获取游戏内产业索引
    /// </summary>
    /// <param name="ID">范围：0~5</param>
    /// <returns></returns>
    public static int GetBusinessIndex(int ID)
    {
        return 1853131 + 1 + GetPlayerID() * 888 + 267 + 187 + 1 + ID * 13;
    }

    /////////////////////////////////////////

    /// <summary>
    /// 掉落物品
    /// </summary>
    public static async Task CreateAmbientPickup(string pickup)
    {
        await Task.Run(async () =>
        {
            if (string.IsNullOrEmpty(pickup))
                return;

            uint pickupHash = STATS.JOATT(pickup);

            Vector3 vector3 = Teleport.GetPlayerPosition();

            Set_Global_Value(2707016 + 3, vector3.X);
            Set_Global_Value(2707016 + 4, vector3.Y);
            Set_Global_Value(2707016 + 5, vector3.Z + 3.0f);
            Set_Global_Value(2707016 + 1, 9999);

            Set_Global_Value(4535851 + 1 + Get_Global_Value<int>(2707016) * 85 + 66 + 2, 2);
            Set_Global_Value(2707016 + 6, 1);

            await Task.Delay(200);

            long pReplayInterface = Memory.Read<long>(Pointers.ReplayInterfacePTR);
            long pCPickupInterface = Memory.Read<long>(pReplayInterface + 0x20);        // pCPickupInterface

            long oPickupNum = Memory.Read<long>(pCPickupInterface + 0x110);             // oPickupNum
            long pPickupList = Memory.Read<long>(pCPickupInterface + 0x100);            // pPickupList

            for (long i = 0; i < oPickupNum; i++)
            {
                long dwpPickup = Memory.Read<long>(pPickupList + i * 0x10);
                uint dwPickupHash = Memory.Read<uint>(dwpPickup + 0x468);

                if (dwPickupHash != 4263048111)
                    continue;

                Memory.Write(dwpPickup + 0x468, pickupHash);
                break;
            }
        });
    }
}
