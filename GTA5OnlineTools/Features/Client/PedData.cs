namespace GTA5OnlineTools.Features.Client;

public static class PedData
{
    public class PedInfo
    {
        public string Name;
        public string DisplayName;
        public long Hash;
    }

    /// <summary>
    /// Ped模型列表
    /// </summary>
    public static List<PedInfo> PedDataClass = new()
    {
        new PedInfo(){ Name="MP_M_Freemode_01", DisplayName="默认线上角色", Hash=0x705E61F2 },
        new PedInfo(){ Name="A_C_Cat_01", DisplayName="[动物] 喵咪", Hash=0x573201B8 },
        new PedInfo(){ Name="A_C_Coyote", DisplayName="[动物] 草原狼", Hash=0x644AC75E },
        new PedInfo(){ Name="A_C_Hen", DisplayName="[动物] 鸡", Hash=0x6AF51FAF },
        new PedInfo(){ Name="A_C_Husky", DisplayName="[动物] 哈士奇", Hash=0x4E8F95A2 },
        new PedInfo(){ Name="A_C_Rabbit_01", DisplayName="[动物] 兔子", Hash=0xDFB55C81 },
        new PedInfo(){ Name="A_C_Chop", DisplayName="[动物] 小查", Hash=0x14EC17EA },
        new PedInfo(){ Name="A_C_Shepherd", DisplayName="[动物] 牧羊犬", Hash=0x431FC24C },
        new PedInfo(){ Name="A_C_Rat", DisplayName="[动物] 老鼠", Hash=0xC3B52966 },
        new PedInfo(){ Name="A_C_Pigeon", DisplayName="[动物] 鸽子", Hash=0x06A20728 },
        new PedInfo(){ Name="A_C_Mtlion", DisplayName="[动物] 狮子", Hash=0x1250D7BA },
        new PedInfo(){ Name="A_C_Chickenhawk", DisplayName="[动物] 老鹰", Hash=0xAAB71F62 },
        new PedInfo(){ Name="A_C_Chimp", DisplayName="[动物] 黑猩猩", Hash=0xA8683715 },
        new PedInfo(){ Name="A_C_Cow", DisplayName="[动物] 奶牛", Hash=0xFCFA9E1E },
        new PedInfo(){ Name="A_C_Deer", DisplayName="[动物] 鹿", Hash=0xD86B5A95 },

        new PedInfo(){ Name="CSB_MP_Agent14", DisplayName="14号探员", Hash=0x6DBBFC8B },
        new PedInfo(){ Name="CS_LesterCrest", DisplayName="莱斯特", Hash=0xB594F5C3 },
        new PedInfo(){ Name="CS_LesterCrest_2", DisplayName="莱斯特2", Hash=0xB63911D },
        new PedInfo(){ Name="CSB_Agatha", DisplayName="贝克女士", Hash=0x2D145A18 },
        new PedInfo(){ Name="CSB_Rashcosvki", DisplayName="越狱光头", Hash=0x188099A9 },
        new PedInfo(){ Name="CS_MartinMadrazo", DisplayName="马丁", Hash=0x43595670 },
        new PedInfo(){ Name="CSB_Mimi", DisplayName="米米", Hash=0x86C1FFE8 },
        new PedInfo(){ Name="IG_JimmyDiSanto", DisplayName="吉米", Hash=0x570462B9 },
        new PedInfo(){ Name="IG_JimmyDiSanto2", DisplayName="吉米2", Hash=0x842DC2D6 },
        new PedInfo(){ Name="IG_SiemonYetarian", DisplayName="西门", Hash=0x4C7B2F05 },
    };
}
