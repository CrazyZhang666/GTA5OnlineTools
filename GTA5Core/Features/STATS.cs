using GTA5Core.Native;
using GTA5Core.Offsets;

namespace GTA5Core.Features;

public static class STATS
{
    private const int statGetIntValue = 2805862 + 267;

    private const int characterSlot = 1574918;
    private const int callStat = 1654054 + 1136;

    private const int statSetIntHash = 1665476 + 1 + 3;
    private const int statSetIntValue = 980531 + 5525;

    private const int statSetIntMinusOne = 1654054 + 1139;   // https://pastebin.com/VbfAmLYB 

    private const int statBoolHash = 1665484 + 1 + 17;
    private const int statBoolValue = 2694581;

    private static long StatGetIntHash()
    {
        return Memory.Read<long>(Memory.GTA5ProBaseAddress + 0x269cd78) + 0xAE8;
    }

    private static long StatSetFloatHash()
    {
        return Memory.Read<long>(Memory.GTA5ProBaseAddress + 0x269cd90) + 0x568;
    }

    private static long StatBoolMaskHash()
    {
        return Memory.Read<long>(Memory.GTA5ProBaseAddress + 0x269cda8) + 0xCD8;
    }

    private static long ShopControllerMask()
    {
        var pointer = Memory.Read<long>(Pointers.GlobalPTR - 0x120) + 0x10D8;
        return Memory.Read<long>(pointer) + 0x398;
    }

    private static uint GET_STAT_HASH(string statName)
    {
        if (statName.StartsWith("MPx_"))
        {
            var index = Globals.GetPlayerIndex();
            statName = statName.Replace("MPx_", $"MP{index}_");
        }

        return JOATT(statName);
    }

    /// <summary>
    /// 字符串转HASH值
    /// </summary>
    /// <param name="statName"></param>
    /// <returns></returns>
    public static uint JOATT(string statName)
    {
        var hash = 0u;

        foreach (var c in statName.ToLower())
        {
            hash += c;
            hash += hash << 10;
            hash ^= hash >> 6;
        }

        hash += hash << 3;
        hash ^= hash >> 11;
        hash += hash << 15;

        return hash;
    }

    /// <summary>
    /// 设置INT类型STAT值
    /// </summary>
    /// <param name="statName"></param>
    /// <returns></returns>
    public static async Task STAT_SET_INT(string statName, int value)
    {
        await STAT_SET_INT1(statName, value);
    }

    /// <summary>
    /// 设置INT类型STAT值
    /// </summary>
    /// <param name="statName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private static async Task STAT_SET_INT1(string statName, int value)
    {
        var hash = GET_STAT_HASH(statName);

        var oldGetIntHash = Memory.Read<uint>(StatGetIntHash() + Globals.Get_Global_Value<int>(characterSlot) * 4);
        var oldGetIntValue = Globals.Get_Global_Value<int>(statGetIntValue);

        var oldSetIntHash = Globals.Get_Global_Value<uint>(statSetIntHash);
        var oldSetIntValue = Globals.Get_Global_Value<int>(statSetIntValue);

        Memory.Write(StatGetIntHash() + Globals.Get_Global_Value<int>(characterSlot), hash);

        Globals.Set_Global_Value(statSetIntHash, hash);
        Globals.Set_Global_Value(statSetIntValue, value);

        Globals.Set_Global_Value(statGetIntValue, value - 1);

        for (var i = 0; i < 10; i++)
        {
            if (Globals.Get_Global_Value<int>(statGetIntValue) == value)
                break;

            if (Globals.Get_Global_Value<int>(callStat) != 9)
                Globals.Set_Global_Value(callStat, 9);

            if (Globals.Get_Global_Value<int>(callStat + 3) != 3)
                Globals.Set_Global_Value(callStat + 3, 3);

            await Task.Delay(100);

            if (i > 5)
                Globals.Set_Global_Value(statGetIntValue, value);
        }

        Memory.Write(StatGetIntHash() + Globals.Get_Global_Value<int>(characterSlot), oldGetIntHash);
        Globals.Set_Global_Value(statGetIntValue, oldGetIntValue);

        Globals.Set_Global_Value(statSetIntHash, oldSetIntHash);
        Globals.Set_Global_Value(statSetIntValue, oldSetIntValue);
    }

    /// <summary>
    /// 设置INT类型STAT值（旧方法）
    /// </summary>
    /// <param name="statName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private static async Task STAT_SET_INT2(string statName, int value)
    {
        var hash = GET_STAT_HASH(statName);

        var oldHash = Globals.Get_Global_Value<uint>(statSetIntHash);             // if (STATS::STAT_GET_INT(statHash,
        var oldValue = Globals.Get_Global_Value<int>(statSetIntValue);

        Globals.Set_Global_Value(statSetIntHash, hash);
        Globals.Set_Global_Value(statSetIntValue, value);
        Globals.Set_Global_Value(statSetIntMinusOne, -1);

        await Task.Delay(1000);

        Globals.Set_Global_Value(statSetIntHash, oldHash);
        Globals.Set_Global_Value(statSetIntValue, oldValue);
    }

    /// <summary>
    /// 读取INT类型STAT值
    /// </summary>
    /// <param name="statName"></param>
    /// <returns></returns>
    private static int STAT_GET_INT(string statName)
    {
        var hash = GET_STAT_HASH(statName);

        var oldGetIntHash = Memory.Read<uint>(StatGetIntHash() + Globals.Get_Global_Value<int>(characterSlot) * 4);
        var oldGetIntValue = Globals.Get_Global_Value<int>(statGetIntValue);

        Memory.Write(StatGetIntHash() + Globals.Get_Global_Value<int>(characterSlot), hash);

        do
        {
            if (Globals.Get_Global_Value<int>(callStat) != 9)
                Globals.Set_Global_Value(callStat, 9);

        } while (Globals.Get_Global_Value<int>(statGetIntValue) != oldGetIntValue);

        var result = Globals.Get_Global_Value<int>(statGetIntValue);

        Memory.Write(StatGetIntHash() + Globals.Get_Global_Value<int>(characterSlot), oldGetIntHash);
        Globals.Set_Global_Value(statGetIntValue, oldGetIntValue);

        return result;
    }

    /// <summary>
    /// 设置FLOAT类型STAT值（丢失精度）
    /// </summary>
    /// <param name="statName"></param>
    /// <param name="value"></param>
    public static async void STAT_SET_FLOAT(string statName, int value)
    {
        var hash = GET_STAT_HASH(statName);

        var oldRaceStartValue = STAT_GET_INT("MPx_RACE_START");
        var oldRacesWonValue = STAT_GET_INT("MPx_RACES_WON");

        await STAT_SET_INT("MPx_RACE_START", 100);
        await STAT_SET_INT("MPx_RACES_WON", value);

        var oldGetIntHash = Memory.Read<uint>(StatGetIntHash() + Globals.Get_Global_Value<int>(characterSlot) * 4);
        var oldGetIntValue = Globals.Get_Global_Value<int>(statGetIntValue);

        var oldFloatHash = Memory.Read<uint>(StatSetFloatHash() + Globals.Get_Global_Value<int>(characterSlot) * 4);

        Memory.Write(StatGetIntHash() + Globals.Get_Global_Value<int>(characterSlot) * 4, hash);
        Memory.Write(StatSetFloatHash() + Globals.Get_Global_Value<int>(characterSlot) * 4, hash);

        Globals.Set_Global_Value(statGetIntValue, value - 1);

        do
        {
            if (Globals.Get_Global_Value<int>(callStat) != 9)
                Globals.Set_Global_Value(callStat, 9);

            await Task.Delay(100);

        } while (Globals.Get_Global_Value<int>(statGetIntValue) != value);

        Memory.Write(StatGetIntHash() + Globals.Get_Global_Value<int>(characterSlot) * 4, oldGetIntHash);
        Globals.Set_Global_Value(statGetIntValue, oldGetIntValue);
        Memory.Write(StatSetFloatHash() + Globals.Get_Global_Value<int>(characterSlot) * 4, oldFloatHash);

        do
        {
            await Task.Delay(100);

        } while (Memory.Read<uint>(StatSetFloatHash() + Globals.Get_Global_Value<int>(characterSlot) * 4) == oldFloatHash);

        await STAT_SET_INT("MPx_RACE_START", oldRaceStartValue);
        await STAT_SET_INT("MPx_RACES_WON", oldRacesWonValue);
    }

    /// <summary>
    /// 设置BOOL类型STAT值（最大同时支持5个写入）
    /// true不需要戴头盔，false需要头盔
    /// </summary>
    /// <param name="statName"></param>
    /// <param name="value"></param>
    public static async void STAT_SET_BOOL(string[] statName, bool value)
    {
        if (value)
        {
            var oldBoolHash = new int[5];

            for (int i = 0; i < statName.Length; i++)
            {
                if (i >= 5)
                    break;

                oldBoolHash[i] = Globals.Get_Global_Value<int>(statBoolHash + i);

                Globals.Set_Global_Value(statBoolHash + i, GET_STAT_HASH(statName[i]));
                Globals.Set_Global_Value(statBoolValue + i, 1);
            }

            for (int i = 0; i < oldBoolHash.Length; i++)
            {
                do
                {
                    if (Globals.Get_Global_Value<int>(statBoolValue + i) == 0)
                        Globals.Set_Global_Value(statBoolHash + i, oldBoolHash[i]);

                    await Task.Delay(100);

                } while (Globals.Get_Global_Value<int>(statBoolHash + i) == oldBoolHash[i]);
            }
        }
        else
        {
            for (int i = 0; i < statName.Length; i++)
            {
                if (i >= 5)
                    break;

                Memory.Write(StatBoolMaskHash() + Globals.Get_Global_Value<int>(characterSlot) * 4, GET_STAT_HASH(statName[i]));

                do
                {
                    if (Memory.Read<int>(ShopControllerMask()) == 1)
                        Memory.Write(ShopControllerMask(), 0);

                } while (Memory.Read<int>(ShopControllerMask()) == 0);
            }
        }
    }
}
