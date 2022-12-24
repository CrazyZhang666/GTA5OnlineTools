using GTA5OnlineTools.GTA.Core;

namespace GTA5OnlineTools.GTA.SDK;

public static class Locals
{
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
