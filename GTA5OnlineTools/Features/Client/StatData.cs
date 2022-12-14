namespace GTA5OnlineTools.Features.Client;

public static class StatData
{
    public class StatClass
    {
        public string Name;
        public List<StatInfo> StatInfo;
    }

    public class StatInfo
    {
        public string Hash;
        public int Value;
    }

    #region 玩家
    /// <summary>
    /// 玩家护甲全满
    /// </summary>
    public static List<StatInfo> _MP_CHAR_ARMOUR = new()
    {
        new StatInfo(){ Hash="_MP_CHAR_ARMOUR_1_COUNT", Value=100 },
        new StatInfo(){ Hash="_MP_CHAR_ARMOUR_2_COUNT", Value=100 },
        new StatInfo(){ Hash="_MP_CHAR_ARMOUR_3_COUNT", Value=100 },
        new StatInfo(){ Hash="_MP_CHAR_ARMOUR_4_COUNT", Value=100 },
        new StatInfo(){ Hash="_MP_CHAR_ARMOUR_5_COUNT", Value=100 },
    };

    /// <summary>
    /// 玩家零食全满
    /// </summary>
    public static List<StatInfo> _NO_BOUGHT = new()
    {
        new StatInfo(){ Hash="_NO_BOUGHT_YUM_SNACKS", Value=100 },
        new StatInfo(){ Hash="_NO_BOUGHT_HEALTH_SNACKS", Value=100 },
        new StatInfo(){ Hash="_NO_BOUGHT_EPIC_SNACKS", Value=100 },
        new StatInfo(){ Hash="_NUMBER_OF_ORANGE_BOUGHT", Value=100 },
        new StatInfo(){ Hash="_NUMBER_OF_BOURGE_BOUGHT", Value=100 },
        new StatInfo(){ Hash="_CIGARETTES_BOUGHT", Value=100 },
        new StatInfo(){ Hash="_NUMBER_OF_CHAMP_BOUGHT", Value=100 },
    };

    /// <summary>
    /// 玩家属性全满
    /// </summary>
    public static List<StatInfo> _SCRIPT_INCREASE = new()
    {
        new StatInfo(){ Hash="_SCRIPT_INCREASE_STAM", Value=100 },
        new StatInfo(){ Hash="_SCRIPT_INCREASE_SHO", Value=100 },
        new StatInfo(){ Hash="_SCRIPT_INCREASE_STRN", Value=100 },
        new StatInfo(){ Hash="_SCRIPT_INCREASE_STL", Value=100 },
        new StatInfo(){ Hash="_SCRIPT_INCREASE_FLY", Value=100 },
        new StatInfo(){ Hash="_SCRIPT_INCREASE_DRIV", Value=100 },
        new StatInfo(){ Hash="_SCRIPT_INCREASE_LUNG", Value=100 },
    };

    /// <summary>
    /// 玩家隐藏属性全满
    /// </summary>
    public static List<StatInfo> _CHAR_ABILITY = new()
    {
        new StatInfo(){ Hash="_CHAR_ABILITY_1_UNLCK", Value=-1 },
        new StatInfo(){ Hash="_CHAR_ABILITY_2_UNLCK", Value=-1 },
        new StatInfo(){ Hash="_CHAR_ABILITY_3_UNLCK", Value=-1 },
        new StatInfo(){ Hash="_CHAR_FM_ABILITY_1_UNLCK", Value=-1 },
        new StatInfo(){ Hash="_CHAR_FM_ABILITY_2_UNLCK", Value=-1 },
        new StatInfo(){ Hash="_CHAR_FM_ABILITY_3_UNLCK", Value=-1 },
    };

    /// <summary>
    /// 玩家性别修改（去重新捏脸）
    /// </summary>
    public static List<StatInfo> _GENDER_CHANGE = new()
    {
        new StatInfo(){ Hash="_ALLOW_GENDER_CHANGE", Value=52 },
    };

    /// <summary>
    /// 设置玩家等级为1
    /// </summary>
    public static List<StatInfo> _CHAR_SET_RP1 = new()
    {
        new StatInfo(){ Hash="_CHAR_SET_RP_GIFT_ADMIN", Value=0 },
    };

    /// <summary>
    /// 设置玩家等级为30
    /// </summary>
    public static List<StatInfo> _CHAR_SET_RP30 = new()
    {
        new StatInfo(){ Hash="_CHAR_SET_RP_GIFT_ADMIN", Value=177100 },
    };

    /// <summary>
    /// 设置玩家等级为60
    /// </summary>
    public static List<StatInfo> _CHAR_SET_RP60 = new()
    {
        new StatInfo(){ Hash="_CHAR_SET_RP_GIFT_ADMIN", Value=625400 },
    };

    /// <summary>
    /// 设置玩家等级为90
    /// </summary>
    public static List<StatInfo> _CHAR_SET_RP90 = new()
    {
        new StatInfo(){ Hash="_CHAR_SET_RP_GIFT_ADMIN", Value=1308100 },
    };

    /// <summary>
    /// 设置玩家等级为120
    /// </summary>
    public static List<StatInfo> _CHAR_SET_RP120 = new()
    {
        new StatInfo(){ Hash="_CHAR_SET_RP_GIFT_ADMIN", Value=2165850 },
    };
    #endregion

    #region 资产
    /// <summary>
    /// 补满夜总会人气
    /// </summary>
    public static List<StatInfo> _CLUB_POPULARITY = new()
    {
        new StatInfo(){ Hash="_CLUB_POPULARITY", Value=1000 },
    };

    /// <summary>
    /// 摩托帮自动进货（切换战局）
    /// </summary>
    public static List<StatInfo> _PAYRESUPPLYTIMER04 = new()
    {
        new StatInfo(){ Hash="_PAYRESUPPLYTIMER0", Value=1 },
        new StatInfo(){ Hash="_PAYRESUPPLYTIMER1", Value=1 },
        new StatInfo(){ Hash="_PAYRESUPPLYTIMER2", Value=1 },
        new StatInfo(){ Hash="_PAYRESUPPLYTIMER3", Value=1 },
        new StatInfo(){ Hash="_PAYRESUPPLYTIMER4", Value=1 },
    };

    /// <summary>
    /// 地堡自动进货（切换战局）
    /// </summary>
    public static List<StatInfo> _PAYRESUPPLYTIMER5 = new()
    {
        new StatInfo(){ Hash="_PAYRESUPPLYTIMER5", Value=1 },
    };

    /// <summary>
    /// 跳过过场动画 (地堡、摩托帮、办公室等)
    /// </summary>
    public static List<StatInfo> _FM_CUT_DONE = new()
    {
        new StatInfo(){ Hash="_FM_CUT_DONE", Value=-1 },
        new StatInfo(){ Hash="_FM_CUT_DONE_2", Value=-1 },
    };

    /// <summary>
    /// 设置车友会等级为1
    /// </summary>
    public static List<StatInfo> _CAR_CLUB_REP = new()
    {
        new StatInfo(){ Hash="_CAR_CLUB_REP", Value=5 },
    };
    #endregion

    #region 抢劫任务
    /// <summary>
    /// 佩里科岛抢劫-玩家x1/100%分红/困难模式/无精英/不拿保险柜/人均245W（粉钻x1+画作x2）
    /// </summary>
    public static List<StatInfo> _H4CNF_1 = new()
    {
        new StatInfo(){ Hash="_H4CNF_BS_GEN", Value=131071 },
        new StatInfo(){ Hash="_H4CNF_BS_ENTR", Value=63 },
        new StatInfo(){ Hash="_H4CNF_BS_ABIL", Value=63 },
        new StatInfo(){ Hash="_H4CNF_APPROACH", Value=-1 },
        new StatInfo(){ Hash="_H4_PROGRESS", Value=131055 },
        new StatInfo(){ Hash="_H4CNF_TARGET", Value=3 },
        new StatInfo(){ Hash="_H4LOOT_PAINT", Value=-1 },
        new StatInfo(){ Hash="_H4LOOT_PAINT_SCOPED", Value=-1 },
        new StatInfo(){ Hash="_H4LOOT_PAINT_V", Value=677045 },
        new StatInfo(){ Hash="_H4_MISSIONS", Value=65535 },
        new StatInfo(){ Hash="_H4CNF_WEAPONS", Value=2 },
        new StatInfo(){ Hash="_H4CNF_WEP_DISRP", Value=3 },
        new StatInfo(){ Hash="_H4CNF_ARM_DISRP", Value=3 },
        new StatInfo(){ Hash="_H4CNF_HEL_DISRP", Value=3 },
        new StatInfo(){ Hash="_H4CNF_GRAPPEL", Value=-1 },
        new StatInfo(){ Hash="_H4CNF_UNIFORM", Value=-1 },
        new StatInfo(){ Hash="_H4CNF_BOLTCUT", Value=-1 },
        new StatInfo(){ Hash="_H4CNF_TROJAN", Value=4 },
        new StatInfo(){ Hash="_H4_PLAYTHROUGH_STATUS", Value=10 },
    };

    /// <summary>
    /// 佩里科岛抢劫-玩家x2/50%50%分红/困难模式/无精英/不拿保险柜/人均245W（粉钻x1+画作x4）
    /// </summary>
    public static List<StatInfo> _H4CNF_2 = new()
    {
        new StatInfo(){ Hash="_H4CNF_BS_GEN", Value=131071 },
        new StatInfo(){ Hash="_H4CNF_BS_ENTR", Value=63 },
        new StatInfo(){ Hash="_H4CNF_BS_ABIL", Value=63 },
        new StatInfo(){ Hash="_H4CNF_APPROACH", Value=-1 },
        new StatInfo(){ Hash="_H4_PROGRESS", Value=131055 },
        new StatInfo(){ Hash="_H4CNF_TARGET", Value=3 },
        new StatInfo(){ Hash="_H4LOOT_PAINT", Value=-1 },
        new StatInfo(){ Hash="_H4LOOT_PAINT_SCOPED", Value=-1 },
        new StatInfo(){ Hash="_H4LOOT_PAINT_V", Value=1034545 },
        new StatInfo(){ Hash="_H4_MISSIONS", Value=65535 },
        new StatInfo(){ Hash="_H4CNF_WEAPONS", Value=2 },
        new StatInfo(){ Hash="_H4CNF_WEP_DISRP", Value=3 },
        new StatInfo(){ Hash="_H4CNF_ARM_DISRP", Value=3 },
        new StatInfo(){ Hash="_H4CNF_HEL_DISRP", Value=3 },
        new StatInfo(){ Hash="_H4CNF_GRAPPEL", Value=-1 },
        new StatInfo(){ Hash="_H4CNF_UNIFORM", Value=-1 },
        new StatInfo(){ Hash="_H4CNF_BOLTCUT", Value=-1 },
        new StatInfo(){ Hash="_H4CNF_TROJAN", Value=4 },
        new StatInfo(){ Hash="_H4_PLAYTHROUGH_STATUS", Value=10 },
    };

    /// <summary>
    /// 佩里科岛抢劫-玩家x3/35%35%30%分红/困难模式/无精英/不拿保险柜/人均245W（粉钻x1+画作x6）
    /// </summary>
    public static List<StatInfo> _H4CNF_3 = new()
    {
        new StatInfo(){ Hash="_H4CNF_BS_GEN", Value=131071 },
        new StatInfo(){ Hash="_H4CNF_BS_ENTR", Value=63 },
        new StatInfo(){ Hash="_H4CNF_BS_ABIL", Value=63 },
        new StatInfo(){ Hash="_H4CNF_APPROACH", Value=-1 },
        new StatInfo(){ Hash="_H4_PROGRESS", Value=131055 },
        new StatInfo(){ Hash="_H4CNF_TARGET", Value=3 },
        new StatInfo(){ Hash="_H4LOOT_PAINT", Value=-1 },
        new StatInfo(){ Hash="_H4LOOT_PAINT_SCOPED", Value=-1 },
        new StatInfo(){ Hash="_H4LOOT_PAINT_V", Value=1087424 },
        new StatInfo(){ Hash="_H4_MISSIONS", Value=65535 },
        new StatInfo(){ Hash="_H4CNF_WEAPONS", Value=2 },
        new StatInfo(){ Hash="_H4CNF_WEP_DISRP", Value=3 },
        new StatInfo(){ Hash="_H4CNF_ARM_DISRP", Value=3 },
        new StatInfo(){ Hash="_H4CNF_HEL_DISRP", Value=3 },
        new StatInfo(){ Hash="_H4CNF_GRAPPEL", Value=-1 },
        new StatInfo(){ Hash="_H4CNF_UNIFORM", Value=-1 },
        new StatInfo(){ Hash="_H4CNF_BOLTCUT", Value=-1 },
        new StatInfo(){ Hash="_H4CNF_TROJAN", Value=4 },
        new StatInfo(){ Hash="_H4_PLAYTHROUGH_STATUS", Value=10 },
    };

    /// <summary>
    /// 佩里科岛抢劫-玩家x4/100%分红/困难模式/无精英/不拿保险柜/人均245W（粉钻x1+画作x7）
    /// </summary>
    public static List<StatInfo> _H4CNF_4 = new()
    {
        new StatInfo(){ Hash="_H4CNF_BS_GEN", Value=131071 },
        new StatInfo(){ Hash="_H4CNF_BS_ENTR", Value=63 },
        new StatInfo(){ Hash="_H4CNF_BS_ABIL", Value=63 },
        new StatInfo(){ Hash="_H4CNF_APPROACH", Value=-1 },
        new StatInfo(){ Hash="_H4_PROGRESS", Value=131055 },
        new StatInfo(){ Hash="_H4CNF_TARGET", Value=3 },
        new StatInfo(){ Hash="_H4LOOT_PAINT", Value=-1 },
        new StatInfo(){ Hash="_H4LOOT_PAINT_SCOPED", Value=-1 },
        new StatInfo(){ Hash="_H4LOOT_PAINT_V", Value=1213295 },
        new StatInfo(){ Hash="_H4_MISSIONS", Value=65535 },
        new StatInfo(){ Hash="_H4CNF_WEAPONS", Value=2 },
        new StatInfo(){ Hash="_H4CNF_WEP_DISRP", Value=3 },
        new StatInfo(){ Hash="_H4CNF_ARM_DISRP", Value=3 },
        new StatInfo(){ Hash="_H4CNF_HEL_DISRP", Value=3 },
        new StatInfo(){ Hash="_H4CNF_GRAPPEL", Value=-1 },
        new StatInfo(){ Hash="_H4CNF_UNIFORM", Value=-1 },
        new StatInfo(){ Hash="_H4CNF_BOLTCUT", Value=-1 },
        new StatInfo(){ Hash="_H4CNF_TROJAN", Value=4 },
        new StatInfo(){ Hash="_H4_PLAYTHROUGH_STATUS", Value=10 },
    };

    /// <summary>
    /// 赌场抢劫面板重置
    /// </summary>
    public static List<StatInfo> _H3OPT = new()
    {
        new StatInfo(){ Hash="_H3OPT_BITSET1", Value=0 },
        new StatInfo(){ Hash="_H3OPT_BITSET0", Value=0 },
        new StatInfo(){ Hash="_H3OPT_POI", Value=0 },
        new StatInfo(){ Hash="_H3OPT_ACCESSPOINTS", Value=0 },
        new StatInfo(){ Hash="_CAS_HEIST_FLOW", Value=0 },
    };

    /// <summary>
    /// 佩里克岛抢劫面板重置
    /// </summary>
    public static List<StatInfo> _H4 = new()
    {
        new StatInfo(){ Hash="_H4_MISSIONS", Value=0 },
        new StatInfo(){ Hash="_H4_PROGRESS", Value=0 },
        new StatInfo(){ Hash="_H4_PLAYTHROUGH_STATUS", Value=0 },
        new StatInfo(){ Hash="_H4CNF_APPROACH", Value=0 },
        new StatInfo(){ Hash="_H4CNF_BS_ENTR", Value=0 },
        new StatInfo(){ Hash="_H4CNF_BS_GEN", Value=0 },
    };

    /// <summary>
    /// 跳过公寓抢劫准备任务
    /// </summary>
    public static List<StatInfo> _HEIST_PLANNING_STAGE = new()
    {
        new StatInfo(){ Hash="_HEIST_PLANNING_STAGE", Value=-1 },
    };

    /// <summary>
    /// 取消抢劫并重新开始
    /// </summary>
    public static List<StatInfo> _CAS_HEIST_NOTS = new()
    {
        new StatInfo(){ Hash="_CAS_HEIST_NOTS", Value=-1 },
        new StatInfo(){ Hash="_CAS_HEIST_FLOW", Value=-1 },
    };

    /// <summary>
    /// 末日抢劫-1数据泄露（M键-设施管理-关闭/开启策划大屏）
    /// </summary>
    public static List<StatInfo> _GANGOPS_FLOW_MISSION_PROG1 = new()
    {
        new StatInfo(){ Hash="_GANGOPS_FLOW_MISSION_PROG", Value=503 },
        new StatInfo(){ Hash="_GANGOPS_HEIST_STATUS", Value=229383 },
        new StatInfo(){ Hash="_GANGOPS_FLOW_NOTIFICATIONS", Value=1557 },
    };

    /// <summary>
    /// 末日抢劫-2波格丹危机（M键-设施管理-关闭/开启策划大屏）
    /// </summary>
    public static List<StatInfo> _GANGOPS_FLOW_MISSION_PROG2 = new()
    {
        new StatInfo(){ Hash="_GANGOPS_FLOW_MISSION_PROG", Value=240 },
        new StatInfo(){ Hash="_GANGOPS_HEIST_STATUS", Value=229378 },
        new StatInfo(){ Hash="_GANGOPS_FLOW_NOTIFICATIONS", Value=1557 },
    };

    /// <summary>
    /// 末日抢劫-3末日将至（M键-设施管理-关闭/开启策划大屏）
    /// </summary>
    public static List<StatInfo> _GANGOPS_FLOW_MISSION_PROG3 = new()
    {
        new StatInfo(){ Hash="_GANGOPS_FLOW_MISSION_PROG", Value=16368 },
        new StatInfo(){ Hash="_GANGOPS_HEIST_STATUS", Value=229380 },
        new StatInfo(){ Hash="_GANGOPS_FLOW_NOTIFICATIONS", Value=1557 },
    };
    #endregion

    #region 解锁
    /// <summary>
    /// CEO办公室满地钱+小金人
    /// </summary>
    public static List<StatInfo> _LIFETIME_BUY_UNDERTAKEN = new()
    {
        new StatInfo(){ Hash="_LIFETIME_BUY_UNDERTAKEN", Value=1000 },
        new StatInfo(){ Hash="_LIFETIME_BUY_COMPLETE", Value=1000 },
        new StatInfo(){ Hash="_LIFETIME_SELL_UNDERTAKEN", Value=10 },
        new StatInfo(){ Hash="_LIFETIME_SELL_COMPLETE", Value=10 },
        new StatInfo(){ Hash="_LIFETIME_CONTRA_EARNINGS", Value=28000000 },
    };

    /// <summary>
    /// 解锁电话联系人功能
    /// </summary>
    public static List<StatInfo> _FM_ACT_PHN = new()
    {
        new StatInfo(){ Hash="_FM_ACT_PHN", Value=-1 },
        new StatInfo(){ Hash="_FM_ACT_PH2", Value=-1 },
        new StatInfo(){ Hash="_FM_ACT_PH3", Value=-1 },
        new StatInfo(){ Hash="_FM_ACT_PH4", Value=-1 },
        new StatInfo(){ Hash="_FM_ACT_PH5", Value=-1 },
        new StatInfo(){ Hash="_FM_VEH_TX1", Value=-1 },
        new StatInfo(){ Hash="_FM_ACT_PH6", Value=-1 },
        new StatInfo(){ Hash="_FM_ACT_PH7", Value=-1 },
        new StatInfo(){ Hash="_FM_ACT_PH8", Value=-1 },
        new StatInfo(){ Hash="_FM_ACT_PH9", Value=-1 },
        new StatInfo(){ Hash="_FM_CUT_DONE", Value=-1 },
        new StatInfo(){ Hash="_FM_CUT_DONE_2", Value=-1 },
    };


    /// <summary>
    /// 限定载具节日涂装
    /// </summary>
    public static List<StatInfo> MPPLY_XMASLIVERIES0 = new()
    {
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES0", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES1", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES2", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES3", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES4", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES5", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES6", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES7", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES8", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES9", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES10", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES11", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES12", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES13", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES14", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES15", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES16", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES17", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES18", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES19", Value=-1 },
        new StatInfo(){ Hash="MPPLY_XMASLIVERIES20", Value=-1 },
    };

    /// <summary>
    /// 解锁-外星人纹身
    /// </summary>
    public static List<StatInfo> _TATTOO_FM_CURRENT_32 = new()
    {
        new StatInfo(){ Hash="_TATTOO_FM_CURRENT_32", Value=-1 },
    };

    /// <summary>
    /// 解锁-全部游艇任务
    /// </summary>
    public static List<StatInfo> _YACHT_MISSION_PROG = new()
    {
        new StatInfo(){ Hash="_YACHT_MISSION_PROG", Value=0 },
        new StatInfo(){ Hash="_YACHT_MISSION_FLOW", Value=21845 },
        new StatInfo(){ Hash="_CASINO_DECORATION_GIFT_1", Value=-1 },
    };

    /// <summary>
    /// 解锁-载具金属质感喷漆与铬合金轮毂
    /// </summary>
    public static List<StatInfo> _CHAR_FM_CARMOD_1_UNLCK = new()
    {
        new StatInfo(){ Hash="_CHAR_FM_CARMOD_1_UNLCK", Value=-1 },
        new StatInfo(){ Hash="_CHAR_FM_CARMOD_2_UNLCK", Value=-1 },
        new StatInfo(){ Hash="_CHAR_FM_CARMOD_3_UNLCK", Value=-1 },
        new StatInfo(){ Hash="_CHAR_FM_CARMOD_4_UNLCK", Value=-1 },
        new StatInfo(){ Hash="_CHAR_FM_CARMOD_5_UNLCK", Value=-1 },
        new StatInfo(){ Hash="_CHAR_FM_CARMOD_6_UNLCK", Value=-1 },
        new StatInfo(){ Hash="_CHAR_FM_CARMOD_7_UNLCK", Value=-1 },
        new StatInfo(){ Hash="_NUMBER_TURBO_STARTS_IN_RACE", Value=50 },
        new StatInfo(){ Hash="_USJS_COMPLETED", Value=50 },
        new StatInfo(){ Hash="_AWD_FM_RACES_FASTEST_LAP", Value=50 },
        new StatInfo(){ Hash="_NUMBER_SLIPSTREAMS_IN_RACE", Value=100 },
        new StatInfo(){ Hash="_AWD_WIN_CAPTURES", Value=50 },
        new StatInfo(){ Hash="_AWD_DROPOFF_CAP_PACKAGES", Value=1 },
        new StatInfo(){ Hash="_AWD_KILL_CARRIER_CAPTURE", Value=1 },
        new StatInfo(){ Hash="_AWD_FINISH_HEISTS", Value=50 },
        new StatInfo(){ Hash="_AWD_FINISH_HEIST_SETUP_JOB", Value=50 },
        new StatInfo(){ Hash="_AWD_NIGHTVISION_KILLS", Value=1 },
        new StatInfo(){ Hash="_AWD_WIN_LAST_TEAM_STANDINGS", Value=50 },
        new StatInfo(){ Hash="_AWD_ONLY_PLAYER_ALIVE_LTS", Value=50 },
        new StatInfo(){ Hash="_AWD_FMRALLYWONDRIVE", Value=1 },
        new StatInfo(){ Hash="_AWD_FMRALLYWONNAV", Value=1 },
        new StatInfo(){ Hash="_AWD_FMWINSEARACE", Value=1 },
        new StatInfo(){ Hash="_AWD_FMWINAIRRACE", Value=1 },
        new StatInfo(){ Hash="_AWD_RACES_WON", Value=50 },
        new StatInfo(){ Hash="_RACES_WON", Value=50 },
        new StatInfo(){ Hash="MPPLY_TOTAL_RACES_WON", Value=50 },
    };

    /// <summary>
    /// 解锁-竞技场-25级解锁出租车
    /// </summary>
    public static List<StatInfo> _ARENAWARS_AP_TIER25 = new()
    {
        new StatInfo(){ Hash="_ARENAWARS_AP_TIER", Value=24 },
        new StatInfo(){ Hash="_ARENAWARS_AP", Value=280 },
    };

    /// <summary>
    /// 解锁-竞技场-50级解锁推土机
    /// </summary>
    public static List<StatInfo> _ARENAWARS_AP_TIER50 = new()
    {
        new StatInfo(){ Hash="_ARENAWARS_AP_TIER", Value=49 },
        new StatInfo(){ Hash="_ARENAWARS_AP", Value=530 },
    };

    /// <summary>
    /// 解锁-竞技场-75级解锁小丑花车
    /// </summary>
    public static List<StatInfo> _ARENAWARS_AP_TIER75 = new()
    {
        new StatInfo(){ Hash="_ARENAWARS_AP_TIER", Value=74 },
        new StatInfo(){ Hash="_ARENAWARS_AP", Value=780 },
    };

    /// <summary>
    /// 解锁-竞技场-100级解锁垃圾大王
    /// </summary>
    public static List<StatInfo> _ARENAWARS_AP_TIER100 = new()
    {
        new StatInfo(){ Hash="_ARENAWARS_AP_TIER", Value=99 },
        new StatInfo(){ Hash="_ARENAWARS_AP", Value=1030 },
    };

    /// <summary>
    /// 解锁-竞技场-200级解锁地霸王拖车
    /// </summary>
    public static List<StatInfo> _ARENAWARS_AP_TIER200 = new()
    {
        new StatInfo(){ Hash="_ARENAWARS_AP_TIER", Value=199 },
        new StatInfo(){ Hash="_ARENAWARS_AP", Value=2030 },
    };

    /// <summary>
    /// 解锁-竞技场-300级解锁混凝土搅拌车
    /// </summary>
    public static List<StatInfo> _ARENAWARS_AP_TIER300 = new()
    {
        new StatInfo(){ Hash="_ARENAWARS_AP_TIER", Value=299 },
        new StatInfo(){ Hash="_ARENAWARS_AP", Value=3030 },
    };

    /// <summary>
    /// 解锁-竞技场-500级解锁星际码头
    /// </summary>
    public static List<StatInfo> _ARENAWARS_AP_TIER500 = new()
    {
        new StatInfo(){ Hash="_ARENAWARS_AP_TIER", Value=499 },
        new StatInfo(){ Hash="_ARENAWARS_AP", Value=5030 },
    };

    /// <summary>
    /// 解锁-竞技场-1000级解锁老式拖拉机
    /// </summary>
    public static List<StatInfo> _ARENAWARS_AP_TIER1000 = new()
    {
        new StatInfo(){ Hash="_ARENAWARS_AP_TIER", Value=999 },
        new StatInfo(){ Hash="_ARENAWARS_AP", Value=10030 },
    };

    /// <summary>
    /// 解锁-竞技场-冲冲猴旅行家购买权限
    /// </summary>
    public static List<StatInfo> _ARENAWARS_SKILL_LEVEL = new()
    {
        new StatInfo(){ Hash="_ARENAWARS_SKILL_LEVEL", Value=19 },
        new StatInfo(){ Hash="_ARENAWARS_SP", Value=209 },
    };

    /// <summary>
    /// 解锁-解锁CEO特殊载具任务
    /// </summary>
    public static List<StatInfo> _AT_FLOW_IMPEXP_NUM = new()
    {
        new StatInfo(){ Hash="_AT_FLOW_IMPEXP_NUM", Value=32 },
    };
    #endregion

    #region 其他
    /// <summary>
    /// 解决赌场侦察拍照问题
    /// </summary>
    public static List<StatInfo> _H3OPT_ACCESSPOINTS = new()
    {
        new StatInfo(){ Hash="_H3OPT_ACCESSPOINTS", Value=0 },
        new StatInfo(){ Hash="_H3OPT_POI", Value=0 },
    };

    /// <summary>
    /// 清除举报（建议单人战局使用）
    /// </summary>
    public static List<StatInfo> MPPLY_REPORT_STRENGTH = new()
    {
        new StatInfo(){ Hash="MPPLY_REPORT_STRENGTH", Value=0 },
        new StatInfo(){ Hash="MPPLY_COMMEND_STRENGTH", Value=0 },
        new StatInfo(){ Hash="MPPLY_FRIENDLY", Value=0 },
        new StatInfo(){ Hash="MPPLY_HELPFUL", Value=0 },
        new StatInfo(){ Hash="MPPLY_GRIEFING", Value=0 },
        new StatInfo(){ Hash="MPPLY_VC_ANNOYINGME", Value=0 },
        new StatInfo(){ Hash="MPPLY_VC_HATE", Value=0 },
        new StatInfo(){ Hash="MPPLY_TC_ANNOYINGME", Value=0 },
        new StatInfo(){ Hash="MPPLY_TC_HATE", Value=0 },
        new StatInfo(){ Hash="MPPLY_OFFENSIVE_LANGUAGE", Value=0 },
        new StatInfo(){ Hash="MPPLY_OFFENSIVE_TAGPLATE", Value=0 },
        new StatInfo(){ Hash="MPPLY_OFFENSIVE_UGC", Value=0 },
        new StatInfo(){ Hash="MPPLY_BAD_CREW_NAME", Value=0 },
        new StatInfo(){ Hash="MPPLY_BAD_CREW_MOTTO", Value=0 },
        new StatInfo(){ Hash="MPPLY_BAD_CREW_STATUS", Value=0 },
        new StatInfo(){ Hash="MPPLY_BAD_CREW_EMBLEM", Value=0 },
        new StatInfo(){ Hash="MPPLY_EXPLOITS", Value=0 },
        new StatInfo(){ Hash="MPPLY_BECAME_BADSPORT_NUM", Value=0 },
        new StatInfo(){ Hash="MPPLY_DESTROYED_PVEHICLES", Value=0 },
        new StatInfo(){ Hash="MPPLY_BECAME_CHEATER_NUM", Value=0 },
        new StatInfo(){ Hash="MPPLY_BADSPORT_MESSAGE", Value=0 },
        new StatInfo(){ Hash="MPPLY_GAME_EXPLOITS", Value=0 },
        new StatInfo(){ Hash="MPPLY_PLAYER_MENTAL_STATE", Value=0 },
        new StatInfo(){ Hash="MPPLY_PLAYERMADE_TITLE", Value=0 },
        new StatInfo(){ Hash="MPPLY_PLAYERMADE_DESC", Value=0 },
        new StatInfo(){ Hash="MPPLY_OVERALL_BADSPORT", Value=0 },
        new StatInfo(){ Hash="MPPLY_OVERALL_CHEAT", Value=0 },
    };
    #endregion

    /// <summary>
    /// Stat数据分类列表
    /// </summary>
    public static List<StatClass> StatDataClass = new()
    {
        new StatClass(){ Name="玩家-护甲全满", StatInfo=_MP_CHAR_ARMOUR },
        new StatClass(){ Name="玩家-零食全满", StatInfo=_NO_BOUGHT },
        new StatClass(){ Name="玩家-属性全满", StatInfo=_SCRIPT_INCREASE },
        new StatClass(){ Name="玩家-隐藏属性全满", StatInfo=_CHAR_ABILITY },
        new StatClass(){ Name="玩家-性别修改（去重新捏脸）", StatInfo=_GENDER_CHANGE },
        new StatClass(){ Name="玩家-修改等级为1", StatInfo=_CHAR_SET_RP1 },
        new StatClass(){ Name="玩家-修改等级为30", StatInfo=_CHAR_SET_RP30 },
        new StatClass(){ Name="玩家-修改等级为60", StatInfo=_CHAR_SET_RP60 },
        new StatClass(){ Name="玩家-修改等级为90", StatInfo=_CHAR_SET_RP90 },
        new StatClass(){ Name="玩家-修改等级为120", StatInfo=_CHAR_SET_RP120 },

        new StatClass(){ Name="资产-补满夜总会人气", StatInfo=_CLUB_POPULARITY },
        new StatClass(){ Name="资产-摩托帮自动进货（切换战局）", StatInfo=_PAYRESUPPLYTIMER04 },
        new StatClass(){ Name="资产-地堡自动进货（切换战局）", StatInfo=_PAYRESUPPLYTIMER5 },
        new StatClass(){ Name="资产-跳过过场动画（地堡、摩托帮、办公室等）", StatInfo=_FM_CUT_DONE },
        new StatClass(){ Name="资产-修改车友会等级为1", StatInfo=_CAR_CLUB_REP },

        new StatClass(){ Name="佩里科岛抢劫-玩家x1/100%分红/困难模式/无精英/不拿保险柜/人均245W（粉钻x1+画作x2）", StatInfo=_H4CNF_1 },
        new StatClass(){ Name="佩里科岛抢劫-玩家x2/50%50%分红/困难模式/无精英/不拿保险柜/人均245W（粉钻x1+画作x4）", StatInfo=_H4CNF_2 },
        new StatClass(){ Name="佩里科岛抢劫-玩家x3/35%35%30%分红/困难模式/无精英/不拿保险柜/人均245W（粉钻x1+画作x6）", StatInfo=_H4CNF_3 },
        new StatClass(){ Name="佩里科岛抢劫-玩家x4/25%25%25%25%分红/困难模式/无精英/不拿保险柜/人均245W（粉钻x1+画作x7）", StatInfo=_H4CNF_4 },

        new StatClass(){ Name="抢劫任务-赌场抢劫面板重置", StatInfo=_H3OPT },
        new StatClass(){ Name="抢劫任务-佩里克岛抢劫面板重置", StatInfo=_H4 },
        new StatClass(){ Name="抢劫任务-跳过公寓抢劫准备任务", StatInfo=_HEIST_PLANNING_STAGE },
        new StatClass(){ Name="抢劫任务-取消抢劫并重新开始", StatInfo=_CAS_HEIST_NOTS },

        new StatClass(){ Name="末日抢劫-1数据泄露（M键-设施管理-关闭/开启策划大屏）", StatInfo=_GANGOPS_FLOW_MISSION_PROG1 },
        new StatClass(){ Name="末日抢劫-2波格丹危机（M键-设施管理-关闭/开启策划大屏）", StatInfo=_GANGOPS_FLOW_MISSION_PROG2 },
        new StatClass(){ Name="末日抢劫-3末日将至（M键-设施管理-关闭/开启策划大屏）", StatInfo=_GANGOPS_FLOW_MISSION_PROG3 },

        new StatClass(){ Name="解锁-CEO办公室满地钱和小金人", StatInfo=_LIFETIME_BUY_UNDERTAKEN },
        new StatClass(){ Name="解锁-电话联系人", StatInfo=_FM_ACT_PHN },
        new StatClass(){ Name="解锁-限定载具节日涂装", StatInfo=MPPLY_XMASLIVERIES0 },
        new StatClass(){ Name="解锁-外星人纹身", StatInfo=_TATTOO_FM_CURRENT_32 },
        new StatClass(){ Name="解锁-全部游艇任务", StatInfo=_YACHT_MISSION_PROG },
        new StatClass(){ Name="解锁-载具金属质感喷漆与铬合金轮毂", StatInfo=_CHAR_FM_CARMOD_1_UNLCK },

        new StatClass(){ Name="解锁-竞技场-25级解锁出租车", StatInfo=_ARENAWARS_AP_TIER25 },
        new StatClass(){ Name="解锁-竞技场-50级解锁推土机", StatInfo=_ARENAWARS_AP_TIER50 },
        new StatClass(){ Name="解锁-竞技场-75级解锁小丑花车", StatInfo=_ARENAWARS_AP_TIER75 },
        new StatClass(){ Name="解锁-竞技场-100级解锁垃圾大王", StatInfo=_ARENAWARS_AP_TIER100 },
        new StatClass(){ Name="解锁-竞技场-200级解锁地霸王拖车", StatInfo=_ARENAWARS_AP_TIER200 },
        new StatClass(){ Name="解锁-竞技场-300级解锁混凝土搅拌车", StatInfo=_ARENAWARS_AP_TIER300 },
        new StatClass(){ Name="解锁-竞技场-500级解锁星际码头", StatInfo=_ARENAWARS_AP_TIER500 },
        new StatClass(){ Name="解锁-竞技场-1000级解锁老式拖拉机", StatInfo=_ARENAWARS_AP_TIER1000 },
        new StatClass(){ Name="解锁-竞技场-解锁冲冲猴旅行家购买权限", StatInfo=_ARENAWARS_SKILL_LEVEL },

        new StatClass(){ Name="解锁-解锁CEO特殊载具任务", StatInfo=_AT_FLOW_IMPEXP_NUM },

        new StatClass(){ Name="其他-解决赌场侦察拍照问题", StatInfo=_H3OPT_ACCESSPOINTS },
        new StatClass(){ Name="其他-清除举报（建议单人战局使用）", StatInfo=MPPLY_REPORT_STRENGTH },

    };
}
