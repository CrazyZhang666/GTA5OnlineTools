using GTA5Core.Native;
using GTA5Core.Offsets;

namespace GTA5Core.Features;

public enum MPStatTypes
{
    TYPE_INT = 0,
    TYPE_FLOAT,
    TYPE_BOOL,
    TYPE_STRING,
    TYPE_LABEL,
    TYPE_DATE,
    TYPE_POS,
    TYPE_USERID,
    TYPE_INT_AWD,
    TYPE_FLOAT_AWD,
    TYPE_BOOL_AWD
};

public static class STATS
{
    private const int character = 1574925;

    private const int stat_data_type = 0x026b1578;

    private const int stat_structure = 2749147 + 305;

    private const int stat_get_int_outvalue = 2750546 + 267;

    private const int stat_get_int_switch = 1668317;
    private const int stat_get_int_case = stat_get_int_switch + 1136;   // https://pastebin.com/VbfAmLYB 
    private const int stat_set_int_case = stat_get_int_switch + 1139;

    private const int stat_set_int_hash = 1679796 + 1 + 3;
    private const int stat_set_int_value = 982384 + 5587;

    private const int stat_bool_hash = 1679804 + 1 + 17;
    private const int stat_bool_value = 2695956;

    private static long StatGetIntHash()
    {
        return Memory.Read<long>(Memory.GTA5ProBaseAddress + (stat_data_type + ((int)MPStatTypes.TYPE_INT * 0x18))) + (0xAE8 + (Globals.Get_Global_Value<int>(character) * 4));
    }

    private static long StatSetFloatHash()
    {
        return Memory.Read<long>(Memory.GTA5ProBaseAddress + (stat_data_type + ((int)MPStatTypes.TYPE_FLOAT * 0x18))) + (0x568 + (Globals.Get_Global_Value<int>(character) * 4));
    }

    private static long StatBoolMaskHash()
    {
        return Memory.Read<long>(Memory.GTA5ProBaseAddress + (stat_data_type + ((int)MPStatTypes.TYPE_BOOL * 0x18))) + (0xCD8 + (Globals.Get_Global_Value<int>(character) * 4));
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

        var oldGetIntHash = Memory.Read<uint>(StatGetIntHash());
        var oldGetIntValue = Globals.Get_Global_Value<int>(stat_get_int_outvalue);

        var oldSetIntHash = Globals.Get_Global_Value<uint>(stat_set_int_hash);
        var oldSetIntValue = Globals.Get_Global_Value<int>(stat_set_int_value);

        Memory.Write(StatGetIntHash(), hash);

        Globals.Set_Global_Value(stat_set_int_hash, hash);
        Globals.Set_Global_Value(stat_set_int_value, value);

        Globals.Set_Global_Value(stat_get_int_outvalue, value - 1);

        for (var i = 0; i < 10; i++)
        {
            if (Globals.Get_Global_Value<int>(stat_get_int_outvalue) == value)
                break;

            if ((Globals.Get_Global_Value<int>(stat_get_int_case) != 9) & (Globals.Get_Global_Value<int>(stat_set_int_case) != 3) & (Globals.Get_Global_Value<int>(stat_structure) != 3))
                Globals.Set_Global_Value(stat_get_int_case, 9);
                Globals.Set_Global_Value(stat_structure, 3);
                Globals.Set_Global_Value(stat_set_int_case, 3);

            await Task.Delay(100);

            if (i > 5)
                Globals.Set_Global_Value(stat_get_int_outvalue, value);
        }

        Memory.Write(StatGetIntHash(), oldGetIntHash);
        Globals.Set_Global_Value(stat_get_int_outvalue, oldGetIntValue);

        Globals.Set_Global_Value(stat_set_int_hash, oldSetIntHash);
        Globals.Set_Global_Value(stat_set_int_value, oldSetIntValue);
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

        var oldHash = Globals.Get_Global_Value<uint>(stat_set_int_hash);             // if (STATS::STAT_GET_INT(statHash,
        var oldValue = Globals.Get_Global_Value<int>(stat_set_int_value);

        Globals.Set_Global_Value(stat_set_int_hash, hash);
        Globals.Set_Global_Value(stat_set_int_value, value);
        Globals.Set_Global_Value(stat_set_int_case, -1);

        await Task.Delay(1000);

        Globals.Set_Global_Value(stat_set_int_hash, oldHash);
        Globals.Set_Global_Value(stat_set_int_value, oldValue);
    }

    /// <summary>
    /// 读取INT类型STAT值
    /// </summary>
    /// <param name="statName"></param>
    /// <returns></returns>
    private static int STAT_GET_INT(string statName)
    {
        var hash = GET_STAT_HASH(statName);

        var oldGetIntHash = Memory.Read<uint>(StatGetIntHash());
        var oldGetIntValue = Globals.Get_Global_Value<int>(stat_get_int_outvalue);

        Memory.Write(StatGetIntHash(), hash);

        do
        {
            if (Globals.Get_Global_Value<int>(stat_get_int_case) != 9)
                Globals.Set_Global_Value(stat_get_int_case, 9);
        } while (Globals.Get_Global_Value<int>(stat_get_int_outvalue) != oldGetIntValue);

        var result = Globals.Get_Global_Value<int>(stat_get_int_outvalue);

        Memory.Write(StatGetIntHash(), oldGetIntHash);
        Globals.Set_Global_Value(stat_get_int_outvalue, oldGetIntValue);

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

        var oldGetIntHash = Memory.Read<uint>(StatGetIntHash());
        var oldGetIntValue = Globals.Get_Global_Value<int>(stat_get_int_outvalue);

        var oldFloatHash = Memory.Read<uint>(StatSetFloatHash());

        Memory.Write(StatGetIntHash(), hash);
        Memory.Write(StatSetFloatHash(), hash);

        Globals.Set_Global_Value(stat_get_int_outvalue, value - 1);

        do
        {
            if (Globals.Get_Global_Value<int>(stat_get_int_case) != 9)
                Globals.Set_Global_Value(stat_get_int_case, 9);

            await Task.Delay(1000);

        } while (Globals.Get_Global_Value<int>(stat_get_int_outvalue) != value);

        Memory.Write(StatGetIntHash(), oldGetIntHash);
        Globals.Set_Global_Value(stat_get_int_outvalue, oldGetIntValue);
        Memory.Write(StatSetFloatHash(), oldFloatHash);

        do
        {
            await Task.Delay(100);

        } while (Memory.Read<uint>(StatSetFloatHash()) == oldFloatHash);

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

                oldBoolHash[i] = Globals.Get_Global_Value<int>(stat_bool_hash + i);

                Globals.Set_Global_Value(stat_bool_hash + i, GET_STAT_HASH(statName[i]));
                Globals.Set_Global_Value(stat_bool_value + i, 1);
            }

            for (int i = 0; i < oldBoolHash.Length; i++)
            {
                do
                {
                    if (Globals.Get_Global_Value<int>(stat_bool_value + i) == 0)
                        Globals.Set_Global_Value(stat_bool_hash + i, oldBoolHash[i]);

                    await Task.Delay(100);

                } while (Globals.Get_Global_Value<int>(stat_bool_hash + i) == oldBoolHash[i]);
            }
        }
        else
        {
            for (int i = 0; i < statName.Length; i++)
            {
                if (i >= 5)
                    break;

                Memory.Write(StatBoolMaskHash(), GET_STAT_HASH(statName[i]));

                do
                {
                    if (Memory.Read<int>(ShopControllerMask()) == 1)
                        Memory.Write(ShopControllerMask(), 0);

                } while (Memory.Read<int>(ShopControllerMask()) == 0);
            }
        }
    }
}
