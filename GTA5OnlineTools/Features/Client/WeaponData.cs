namespace GTA5OnlineTools.Features.Client;

public static class WeaponData
{
    public class WeaponClass
    {
        public string Name;
        public string Emoji;
        public List<WeaponInfo> WeaponInfo;
    }

    public class WeaponInfo
    {
        public string Name;
        public string DisplayName;
        public long Hash;
    }

    /// <summary>
    /// 近战
    /// </summary>
    public static List<WeaponInfo> Melee = new()
    {
        new WeaponInfo(){ Name="weapon_dagger", DisplayName="古董骑兵匕首", Hash=0x92A27487 },
        new WeaponInfo(){ Name="weapon_bat", DisplayName="球棒", Hash=0x958A4A8F },
        new WeaponInfo(){ Name="weapon_bottle", DisplayName="酒瓶", Hash=0xF9E6AA4B },
        new WeaponInfo(){ Name="weapon_crowbar", DisplayName="撬棒", Hash=0x84BD7BFD },
        new WeaponInfo(){ Name="weapon_unarmed", DisplayName="徒手", Hash=0xA2719263 },
        new WeaponInfo(){ Name="weapon_flashlight", DisplayName="手电筒", Hash=0x8BB05FD7 },
        new WeaponInfo(){ Name="weapon_golfclub", DisplayName="高尔夫球杆", Hash=0x440E4788 },
        new WeaponInfo(){ Name="weapon_hammer", DisplayName="铁锤", Hash=0x4E875F73 },
        new WeaponInfo(){ Name="weapon_hatchet", DisplayName="手斧", Hash=0xF9DCBF2D },
        new WeaponInfo(){ Name="weapon_knuckle", DisplayName="手指虎", Hash=0xD8DF3C3C },
        new WeaponInfo(){ Name="weapon_knife", DisplayName="小刀", Hash=0x99B507EA },
        new WeaponInfo(){ Name="weapon_machete", DisplayName="开山刀", Hash=0xDD5DF8D9 },
        new WeaponInfo(){ Name="weapon_switchblade", DisplayName="弹簧刀", Hash=0xDFE37640 },
        new WeaponInfo(){ Name="weapon_nightstick", DisplayName="警棍", Hash=0x678B81B1 },
        new WeaponInfo(){ Name="weapon_wrench", DisplayName="管钳扳手", Hash=0x19044EE0 },
        new WeaponInfo(){ Name="weapon_battleaxe", DisplayName="战斧", Hash=0xCD274149 },
        new WeaponInfo(){ Name="weapon_poolcue", DisplayName="台球杆", Hash=0x94117305 },
        new WeaponInfo(){ Name="weapon_stone_hatchet", DisplayName="石斧", Hash=0x3813FC08 },
    };

    /// <summary>
    /// 手枪
    /// </summary>
    public static List<WeaponInfo> Handguns = new()
    {
        new WeaponInfo(){ Name="weapon_pistol", DisplayName="手枪", Hash=0x1B06D571 },
        new WeaponInfo(){ Name="weapon_pistol_mk2", DisplayName="MK2 手枪", Hash=0xBFE256D4 },
        new WeaponInfo(){ Name="weapon_combatpistol", DisplayName="战斗手枪", Hash=0x5EF9FEC4 },
        new WeaponInfo(){ Name="weapon_appistol", DisplayName="穿甲手枪", Hash=0x22D8FE39 },
        new WeaponInfo(){ Name="weapon_stungun", DisplayName="电击枪", Hash=0x3656C8C1 },
        new WeaponInfo(){ Name="weapon_pistol50", DisplayName="0.5口径手枪", Hash=0x99AEEB3B },
        new WeaponInfo(){ Name="weapon_snspistol", DisplayName="劣质手枪", Hash=0xBFD21232 },
        new WeaponInfo(){ Name="weapon_snspistol_mk2", DisplayName="MK2 劣质手枪", Hash=0x88374054 },
        new WeaponInfo(){ Name="weapon_heavypistol", DisplayName="重型手枪", Hash=0xD205520E },
        new WeaponInfo(){ Name="weapon_vintagepistol", DisplayName="老式手枪", Hash=0x83839C4 },
        new WeaponInfo(){ Name="weapon_flaregun", DisplayName="信号枪", Hash=0x47757124 },
        new WeaponInfo(){ Name="weapon_marksmanpistol", DisplayName="射手手枪", Hash=0xDC4DB296 },
        new WeaponInfo(){ Name="weapon_revolver", DisplayName="重型左轮手枪", Hash=0xC1B3C3D1 },
        new WeaponInfo(){ Name="weapon_revolver_mk2", DisplayName="MK2 重型左轮手枪", Hash=0xCB96392F },
        new WeaponInfo(){ Name="weapon_doubleaction", DisplayName="双动式左轮手枪 ", Hash=0x97EA20B8 },
        new WeaponInfo(){ Name="weapon_raypistol", DisplayName="原子能手枪", Hash=0xAF3696A1 },
        new WeaponInfo(){ Name="weapon_ceramicpistol", DisplayName="陶瓷手枪", Hash=0x2B5EF5EC },
        new WeaponInfo(){ Name="weapon_navyrevolver", DisplayName="海军左轮手枪", Hash=0x917F6C8C },
        new WeaponInfo(){ Name="weapon_gadgetpistol", DisplayName="佩里科手枪", Hash=0x57A4368C },
        new WeaponInfo(){ Name="weapon_stungun_mp", DisplayName="电击枪（多人）", Hash=0x45CD9CF3 },
    };

    /// <summary>
    /// 冲锋枪
    /// </summary>
    public static List<WeaponInfo> SubmachineGuns = new()
    {
        new WeaponInfo(){ Name="weapon_microsmg", DisplayName="微型冲锋枪", Hash=0x13532244 },
        new WeaponInfo(){ Name="weapon_smg", DisplayName="冲锋枪", Hash=0x2BE6766B },
        new WeaponInfo(){ Name="weapon_smg_mk2", DisplayName="MK2 冲锋枪", Hash=0x78A97CD0 },
        new WeaponInfo(){ Name="weapon_assaultsmg", DisplayName="突击冲锋枪", Hash=0xEFE7E2DF },
        new WeaponInfo(){ Name="weapon_combatpdw", DisplayName="作战自卫冲锋枪", Hash=0x0A3D4D34 },
        new WeaponInfo(){ Name="weapon_machinepistol", DisplayName="冲锋手枪", Hash=0xDB1AA450 },
        new WeaponInfo(){ Name="weapon_minismg", DisplayName="迷你冲锋枪", Hash=0xBD248B55 },
        new WeaponInfo(){ Name="weapon_gusenberg", DisplayName="古森柏冲锋枪", Hash=0x61012683 },
    };

    /// <summary>
    /// 霰弹枪
    /// </summary>
    public static List<WeaponInfo> Shotguns = new()
    {
        new WeaponInfo(){ Name="weapon_pumpshotgun", DisplayName="泵动式霰弹枪", Hash=0x1D073A89 },
        new WeaponInfo(){ Name="weapon_pumpshotgun_mk2", DisplayName="MK2 泵动式霰弹枪", Hash=0x555AF99A },
        new WeaponInfo(){ Name="weapon_sawnoffshotgun", DisplayName="短管霰弹枪", Hash=0x7846A318 },
        new WeaponInfo(){ Name="weapon_assaultshotgun", DisplayName="突击霰弹枪", Hash=0xE284C527 },
        new WeaponInfo(){ Name="weapon_bullpupshotgun", DisplayName="无托式霰弹枪", Hash=0x9D61E50F },
        new WeaponInfo(){ Name="weapon_musket", DisplayName="老式火枪", Hash=0xA89CB99E },
        new WeaponInfo(){ Name="weapon_heavyshotgun", DisplayName="重型霰弹枪", Hash=0x3AABBBAA },
        new WeaponInfo(){ Name="weapon_dbshotgun", DisplayName="双管霰弹枪", Hash=0xEF951FBB },
        new WeaponInfo(){ Name="weapon_autoshotgun", DisplayName="冲锋霰弹枪", Hash=0x12E82D3D },
        new WeaponInfo(){ Name="weapon_combatshotgun", DisplayName="战斗霰弹枪", Hash=0x5A96BA4 },
    };

    /// <summary>
    /// 突击步枪
    /// </summary>
    public static List<WeaponInfo> AssaultRifles = new()
    {
        new WeaponInfo(){ Name="weapon_assaultrifle", DisplayName="突击步枪", Hash=0xBFEFFF6D },
        new WeaponInfo(){ Name="weapon_assaultrifle_mk2", DisplayName="MK2 突击步枪", Hash=0x394F415C },
        new WeaponInfo(){ Name="weapon_carbinerifle", DisplayName="卡宾步枪", Hash=0x83BF0278 },
        new WeaponInfo(){ Name="weapon_carbinerifle_mk2", DisplayName="MK2 卡宾步枪", Hash=0xFAD1F1C9 },
        new WeaponInfo(){ Name="weapon_advancedrifle", DisplayName="高级步枪", Hash=0xAF113F99 },
        new WeaponInfo(){ Name="weapon_specialcarbine", DisplayName="特制卡宾步枪", Hash=0xC0A3098D },
        new WeaponInfo(){ Name="weapon_specialcarbine_mk2", DisplayName="MK2 特制卡宾步枪", Hash=0x969C3D67 },
        new WeaponInfo(){ Name="weapon_bullpuprifle", DisplayName="无托式步枪", Hash=0x7F229F94 },
        new WeaponInfo(){ Name="weapon_bullpuprifle_mk2", DisplayName="MK2 无托式步枪", Hash=0x84D6FAFD },
        new WeaponInfo(){ Name="weapon_compactrifle", DisplayName="紧凑型步枪", Hash=0x624FE830 },
        new WeaponInfo(){ Name="weapon_militaryrifle", DisplayName="军用步枪", Hash=0x9D1F17E6 },
        new WeaponInfo(){ Name="weapon_heavyrifle", DisplayName="重型步枪", Hash=0xC78D71B4 },
        new WeaponInfo(){ Name="weapon_tacticalrifle", DisplayName="制式卡宾步枪", Hash=0xC78D71B4 },
    };

    /// <summary>
    /// 轻机枪
    /// </summary>
    public static List<WeaponInfo> LightMachineGuns = new()
    {
        new WeaponInfo(){ Name="weapon_mg", DisplayName="机枪", Hash=0x9D07F764 },
        new WeaponInfo(){ Name="weapon_combatmg", DisplayName="战斗机枪", Hash=0x7FD62962 },
        new WeaponInfo(){ Name="weapon_combatmg_mk2", DisplayName="MK2 战斗机枪", Hash=0xDBBD7280 },
        new WeaponInfo(){ Name="weapon_raycarbine", DisplayName="邪恶冥王", Hash=0x476BF155 },
    };

    /// <summary>
    /// 狙击枪
    /// </summary>
    public static List<WeaponInfo> SniperRifles = new()
    {
        new WeaponInfo(){ Name="weapon_sniperrifle", DisplayName="狙击步枪", Hash=0x05FC3C11 },
        new WeaponInfo(){ Name="weapon_heavysniper", DisplayName="重型狙击步枪", Hash=0x0C472FE2 },
        new WeaponInfo(){ Name="weapon_heavysniper_mk2", DisplayName="MK2 重型狙击步枪", Hash=0xA914799 },
        new WeaponInfo(){ Name="weapon_marksmanrifle", DisplayName="射手步枪", Hash=0xC734385A },
        new WeaponInfo(){ Name="weapon_marksmanrifle_mk2", DisplayName="MK2 射手步枪", Hash=0x6A6C02E0 },
        new WeaponInfo(){ Name="weapon_precisionrifle", DisplayName="精确步枪", Hash=0x6A6C02E0 },
    };

    /// <summary>
    /// 重武器
    /// </summary>
    public static List<WeaponInfo> HeavyWeapons = new()
    {
        new WeaponInfo(){ Name="weapon_rpg", DisplayName="火箭炮", Hash=0xB1CA77B1 },
        new WeaponInfo(){ Name="weapon_grenadelauncher", DisplayName="榴弹发射器", Hash=0xA284510B },
        new WeaponInfo(){ Name="weapon_grenadelauncher_smoke", DisplayName="烟雾榴弹发射器", Hash=0x4DD2DC56 },
        new WeaponInfo(){ Name="weapon_minigun", DisplayName="火神机枪", Hash=0x42BF8A85 },
        new WeaponInfo(){ Name="weapon_firework", DisplayName="烟火发射器", Hash=0x7F7497E5 },
        new WeaponInfo(){ Name="weapon_railgun", DisplayName="电磁步枪", Hash=0x6D544C99 },
        new WeaponInfo(){ Name="weapon_hominglauncher", DisplayName="制导火箭发射器", Hash=0x63AB0442 },
        new WeaponInfo(){ Name="weapon_compactlauncher", DisplayName="紧凑型榴弹发射器", Hash=0x0781FE4A },
        new WeaponInfo(){ Name="weapon_rayminigun", DisplayName="寡妇制造者", Hash=0xB62D1F67 },
        new WeaponInfo(){ Name="weapon_emplauncher", DisplayName="紧凑电磁脉冲发射器", Hash=0xDB26713A },
    };

    /// <summary>
    /// 投掷物
    /// </summary>
    public static List<WeaponInfo> Throwables = new()
    {
        new WeaponInfo(){ Name="weapon_grenade", DisplayName="手榴弹", Hash=0x93E220BD },
        new WeaponInfo(){ Name="weapon_bzgas", DisplayName="毒气手榴弹", Hash=0xA0973D5E },
        new WeaponInfo(){ Name="weapon_molotov", DisplayName="汽油弹", Hash=0x24B17070 },
        new WeaponInfo(){ Name="weapon_stickybomb", DisplayName="黏弹", Hash=0x2C3731D9 },
        new WeaponInfo(){ Name="weapon_proxmine", DisplayName="感应地雷", Hash=0xAB564B93 },
        new WeaponInfo(){ Name="weapon_snowball", DisplayName="雪球", Hash=0x787F0BB },
        new WeaponInfo(){ Name="weapon_pipebomb", DisplayName="土制炸弹", Hash=0xBA45E8B8 },
        new WeaponInfo(){ Name="weapon_ball", DisplayName="棒球", Hash=0x23C9F95C },
        new WeaponInfo(){ Name="weapon_smokegrenade", DisplayName="催泪瓦斯", Hash=0xFDBC8A50 },
        new WeaponInfo(){ Name="weapon_flare", DisplayName="信号弹", Hash=0x497FACC3 },
    };

    /// <summary>
    /// 杂项
    /// </summary>
    public static List<WeaponInfo> Miscellaneous = new()
    {
        new WeaponInfo(){ Name="weapon_petrolcan", DisplayName="油桶", Hash=0x34A67B97 },
        new WeaponInfo(){ Name="gadget_parachute", DisplayName="降落伞", Hash=0xFBAB5776 },
        new WeaponInfo(){ Name="weapon_fireextinguisher", DisplayName="灭火器", Hash=0x060EC506 },
        new WeaponInfo(){ Name="weapon_weapon_hazardcan", DisplayName="危险的杰瑞罐", Hash=0xBA536372 },
        new WeaponInfo(){ Name="weapon_weapon_fertilizercan", DisplayName="肥料罐", Hash=0x184140A1 },
    };

    /// <summary>
    /// 武器分类
    /// </summary>
    public static List<WeaponClass> WeaponClassData = new()
    {
        new WeaponClass(){ Name="近战", Emoji="🏂", WeaponInfo=Melee },
        new WeaponClass(){ Name="手枪", Emoji="🏋", WeaponInfo=Handguns },
        new WeaponClass(){ Name="冲锋枪", Emoji="🤸‍♂️", WeaponInfo=SubmachineGuns },
        new WeaponClass(){ Name="霰弹枪", Emoji="⛹", WeaponInfo=Shotguns },
        new WeaponClass(){ Name="突击步枪", Emoji="🤺", WeaponInfo=AssaultRifles },
        new WeaponClass(){ Name="轻机枪", Emoji="🤾‍♀️", WeaponInfo=LightMachineGuns },
        new WeaponClass(){ Name="狙击枪", Emoji="🏌", WeaponInfo=SniperRifles },
        new WeaponClass(){ Name="重武器", Emoji="🏇", WeaponInfo=HeavyWeapons },
        new WeaponClass(){ Name="投掷物", Emoji="🏄", WeaponInfo=Throwables },
        new WeaponClass(){ Name="杂项", Emoji="🏊", WeaponInfo=Miscellaneous }
    };
}
