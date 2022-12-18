using GTA5OnlineTools.GTA.Core;

namespace GTA5OnlineTools.GTA.SDK;

public static class Locals
{
    public const string Casino_Script_Name = "fm_mission_controller";

    public const int Take_Casino_Script_Index = 19652 + 2685;
    public const int Casino_Mission_Life_Script_Index = 26077 + 1322 + 1;
    public const int Vault_Door_Script_Index = 10068 + 7;
    public const int Vault_Door_Total_Script_Index = 10068 + 37;
    public const int Casino_Fingerprint_Script_Index = (0x68CA0 - 0x8) / 8;
    public const int Casino_Keypad_Script_Index = (0x6ADD0 - 0x8) / 8;

    ////////////////////////////////////////////////////////////////////////

    public const string Cayo_Script_Name = "fm_mission_controller_2020";

    public const int Take_Cayo_Script_Index = 40004 + 1392 + 53;
    public const int Cayo_Mission_Life_Script_Index = 43059 + 865 + 1;
    public const int Plasma_Cutter_Progress_Script_Index = 28269 + 3;
    public const int Plasma_Cutter_Heat_Script_Index = 28269 + 4;
    public const int Cayo_Fingerprint_Script_Index = (0x2EBD0 - 0x8) / 8;
    public const int Cayo_Sewer_Cuts_Script_Index = 0x34CE0 / 8;

    public static long LocalAddress(string name)
    {
        long pLocalScripts = Memory.Read<long>(Pointers.LocalScriptsPTR);

        for (int i = 0; i < 54; i++)
        {
            long pointer = Memory.Read<long>(pLocalScripts + i * 0x8);

            string str = Memory.ReadString(pointer + 0xD4, name.Length + 1);
            if (str.ToLower() == name.ToLower())
                return pointer + 0xB0;
        }

        return 0;
    }

    public static long LocalAddress(string name, int index)
    {
        long pLocalScripts = Memory.Read<long>(Pointers.LocalScriptsPTR);

        for (int i = 0; i < 54; i++)
        {
            long pointer = Memory.Read<long>(pLocalScripts + i * 0x8);

            long address = Memory.Read<long>(pointer + 0xB0);
            string str = Memory.ReadString(pointer + 0xD0, name.Length + 1);
            if (str == name && pointer != 0)
                return address + index * 8;
        }

        return 0;
    }

    public static T ReadLocalAddress<T>(string name, int index) where T : struct
    {
        return Memory.Read<T>(LocalAddress(name, index));
    }

    public static void WriteLocalAddress<T>(string name, int index, T value) where T : struct
    {
        Memory.Write(LocalAddress(name, index), value);
    }
}
