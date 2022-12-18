using GTA5OnlineTools.GTA.Core;

namespace GTA5OnlineTools.GTA.SDK;

public static class Online
{
    /// <summary>
    /// 线上战局切换
    /// 
    /// -1, 离开线上模式
    ///  0, 公共战局
    ///  1, 创建公共战局
    ///  2, 私人帮会战局
    ///  3, 帮会战局
    ///  6, 私人好友战局
    ///  9, 加入好友
    /// 10, 单人战局
    /// 11, 仅限邀请战局
    /// 12, 加入帮会伙伴
    /// </summary>
    /// <param name="sessionID">战局ID</param>
    public static void LoadSession(int sessionID)
    {
        Task.Run(async () =>
        {
            Memory.SetForegroundWindow();

            if (sessionID == -1)
            {
                // 离开线上模式需要特殊处理
                Hacks.WriteGA(Offsets.InitSessionCache, -1);
            }
            else
            {
                // 正常切换战局，修改战局类型
                Hacks.WriteGA(Offsets.InitSessionType, sessionID);
            }

            // 切换战局状态
            Hacks.WriteGA(Offsets.InitSessionState, 1);
            await Task.Delay(200);
            Hacks.WriteGA(Offsets.InitSessionState, 0);
        });
    }

    /// <summary>
    /// 空战局（原理：暂停GTA5进程10秒钟）
    /// </summary>
    public static void EmptySession()
    {
        Task.Run(async () =>
        {
            ProcessMgr.SuspendProcess(Memory.GTA5ProId);
            await Task.Delay(10000);
            ProcessMgr.ResumeProcess(Memory.GTA5ProId);
        });
    }

    /// <summary>
    /// 挂机防踢
    /// </summary>
    /// <param name="isEnable"></param>
    public static void AntiAFK(bool isEnable)
    {
        /*
         idleKick: {('262145', '87')}    GLOBAL IDLEKICK_WARNING1 
         idleKick: {('262145', '88')}    GLOBAL IDLEKICK_WARNING2 
         idleKick: {('262145', '89')}    GLOBAL IDLEKICK_WARNING3 
         idleKick: {('262145', '90')}    GLOBAL IDLEKICK_KICK 
         idleKick: {('262145', '8248')}    GLOBAL ConstrainedKick_Warning1 
         idleKick: {('262145', '8249')}    GLOBAL ConstrainedKick_Warning2 
         idleKick: {('262145', '8250')}    GLOBAL ConstrainedKick_Warning3 
         idleKick: {('262145', '8251')}    GLOBAL ConstrainedKick_Kick 
         timeStoodIdle: {('1648034', '1156')}    GLOBAL time in ms  
         idleKick: {('1648034', '1172')}    GLOBAL
         */

        // joaat("weapon_minigun");
        Hacks.WriteGA(262145 + 87, isEnable ? 99999999 : 120000);        // 120000     GLOBAL IDLEKICK_WARNING1 
        Hacks.WriteGA(262145 + 88, isEnable ? 99999999 : 300000);        // 300000     GLOBAL IDLEKICK_WARNING2 
        Hacks.WriteGA(262145 + 89, isEnable ? 99999999 : 600000);        // 600000     GLOBAL IDLEKICK_WARNING3
        Hacks.WriteGA(262145 + 90, isEnable ? 99999999 : 900000);        // 900000     GLOBAL IDLEKICK_KICK 
        // 742014
        Hacks.WriteGA(262145 + 8248, isEnable ? 2000000000 : 30000);     // 30000      GLOBAL ConstrainedKick_Warning1
        Hacks.WriteGA(262145 + 8249, isEnable ? 2000000000 : 60000);     // 60000      GLOBAL ConstrainedKick_Warning2
        Hacks.WriteGA(262145 + 8250, isEnable ? 2000000000 : 90000);     // 90000      GLOBAL ConstrainedKick_Warning3
        Hacks.WriteGA(262145 + 8251, isEnable ? 2000000000 : 120000);    // 120000     GLOBAL ConstrainedKick_Kick
    }

    /// <summary>
    /// 模型变更
    /// </summary>
    /// <param name="hash"></param>
    public static void ModelChange(long hash)
    {
        Hacks.WriteGA(Offsets.oVGETIn + 59, 1);                 // triggerModelChange   Global_2671449.f_59
        Hacks.WriteGA(Offsets.oVGETIn + 46, hash);              // modelChangeHash      Global_2671449.f_46
        Thread.Sleep(10);
        Hacks.WriteGA(Offsets.oVGETIn + 59, 0);
    }

    /// <summary>
    /// 允许非公共战局运货
    /// </summary>
    /// <param name="isEnable"></param>
    public static void AllowSellOnNonPublic(bool isEnable)
    {
        Hacks.WriteGA(2714762 + 744, isEnable ? 0 : 1);
    }

    /// <summary>
    /// 移除被动模式CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void PassiveModeCooldown(bool isEnable)
    {
        Hacks.WriteGA(2815059 + 4460, isEnable ? 0 : 1);
        Hacks.WriteGA(1966337, isEnable ? 0 : 1);
    }

    /// <summary>
    /// 移除自杀CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SuicideCooldown(bool isEnable)
    {
        if (isEnable)
            Hacks.WriteGA(2815059 + 6774, 0);

        Hacks.WriteGA(262145 + 28396, isEnable ? 3 : 300000);
        Hacks.WriteGA(262145 + 28397, isEnable ? 3 : 60000);
    }

    /// <summary>
    /// 移除轨道炮CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void DisableOrbitalCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 23112, isEnable ? 0 : 2880000);         // -1707434973
    }

    /// <summary>
    /// 进入线上个人载具
    /// </summary>
    public static void GetInOnlinePV()
    {
        Hacks.WriteGA(Offsets.oVGETIn + 8, 1);
    }

    /// <summary>
    /// 战局雪天
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SessionSnow(bool isEnable)
    {
        Hacks.WriteGA(262145 + 4751, isEnable ? 1 : 0);            // turn snow on / off Global_262145.f_4751
    }

    /// <summary>
    /// 雷达影踪/人间蒸发
    /// </summary>
    /// <param name="isEnable"></param>
    public static void OffRadar(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oPlayerIDHelp + 1 + Hacks.GetPlayerID() * 453 + 208, isEnable ? 1 : 0);
        if (isEnable)
            Hacks.WriteGA(Offsets.oNETTimeHelp + 56, Hacks.GetNetworkTime() + 3600000);
        Hacks.WriteGA(Offsets.oVMYCar + 4630, isEnable ? 3 : 0);
    }

    /// <summary>
    /// 幽灵组织
    /// </summary>
    /// <param name="isEnable"></param>
    public static void GhostOrganization(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oPlayerIDHelp + 1 + Hacks.GetPlayerID() * 453 + 208, isEnable ? 1 : 0);
        if (isEnable)
            Hacks.WriteGA(Offsets.oNETTimeHelp + 56, Hacks.GetNetworkTime() + 3600000);        // iVar0 = NETWORK::GET_TIME_DIFFERENCE(NETWORK::GET_NETWORK_TIME()
        Hacks.WriteGA(Offsets.oVMYCar + 4630, isEnable ? 4 : 0);
    }

    /// <summary>
    /// 警察无视犯罪
    /// </summary>
    /// <param name="isEnable"></param>
    public static void BribeOrBlindCops(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 4625, isEnable ? 1 : 0);
        Hacks.WriteGA(Offsets.oVMYCar + 4627, isEnable ? Hacks.GetNetworkTime() + 3600000 : 0);
        Hacks.WriteGA(Offsets.oVMYCar + 4624, isEnable ? 5 : 0);
    }

    /// <summary>
    /// 贿赂当局
    /// </summary>
    /// <param name="isEnable"></param>
    public static void BribeAuthorities(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 4625, isEnable ? 1 : 0);
        Hacks.WriteGA(Offsets.oVMYCar + 4627, isEnable ? Hacks.GetNetworkTime() + 3600000 : 0);
        Hacks.WriteGA(Offsets.oVMYCar + 4624, isEnable ? 21 : 0);
    }

    /// <summary>
    /// 显示玩家
    /// </summary>
    /// <param name="isEnable"></param>
    public static void RevealPlayers(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oPlayerIDHelp + 1 + Hacks.GetPlayerID() * 453 + 211, isEnable ? 1 : 0);
        Hacks.WriteGA(Offsets.oNETTimeHelp + 57, isEnable ? Hacks.GetNetworkTime() + 3600000 : 0);
    }

    /// <summary>
    /// 设置角色等级经验倍数
    /// </summary>
    /// <param name="multiplier"></param>
    public static void RPMultiplier(float multiplier)
    {
        Hacks.WriteGA(262145 + 1, multiplier);           // xpMultiplier Global_262145.f_1
    }

    /// <summary>
    /// 设置角色AP经验倍数
    /// </summary>
    /// <param name="multiplier"></param>
    public static void APMultiplier(float multiplier)
    {
        Hacks.WriteGA(262145 + 25914, multiplier);
    }

    /// <summary>
    /// 设置车友会等级经验倍数
    /// </summary>
    /// <param name="multiplier"></param>
    public static void REPMultiplier(float multiplier)
    {
        Hacks.WriteGA(262145 + 31636, multiplier);        // Street Race         街头比赛        -147149995
        Hacks.WriteGA(262145 + 31637, multiplier);        // Pursuit Race        追逐赛
        Hacks.WriteGA(262145 + 31638, multiplier);        // Scramble            攀登
        Hacks.WriteGA(262145 + 31639, multiplier);        // Head 2 Head         头对头          1434998920

        Hacks.WriteGA(262145 + 31641, multiplier);        // LS Car Meet         汽车见面会       1819417801
        Hacks.WriteGA(262145 + 31642, multiplier);        // LS Car Meet Track
        Hacks.WriteGA(262145 + 31643, multiplier);        // LS Car Meet Cloth Shop
    }

    /// <summary>
    /// 使用牛鲨睾酮
    /// </summary>
    /// <param name="isEnable"></param>
    public static void InstantBullShark(bool isEnable)
    {
        if (isEnable)
            Hacks.WriteGA(Offsets.oNETTimeHelp + 3576, 1);
        else
        {
            int temp = Hacks.ReadGA<int>(Offsets.oNETTimeHelp + 3576);
            if (temp != 0)
                Hacks.WriteGA(Offsets.oNETTimeHelp + 3576, 5);
        }
    }

    /// <summary>
    /// 呼叫支援直升机
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CallBackupHeli(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 4454, isEnable ? 1 : 0);
    }

    /// <summary>
    /// 呼叫空袭
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CallAirstrike(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 4455, isEnable ? 1 : 0);
    }

    /// <summary>
    /// 启用CEO特殊货物
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOSpecialCargo(bool isEnable)
    {
        Hacks.WriteGA(1946111, isEnable ? 1 : 0);
    }

    /// <summary>
    /// 设置CEO特殊货物类型
    /// </summary>
    /// <param name="cargoID"></param>
    public static void CEOCargoType(int cargoID)
    {
        Hacks.WriteGA(1945957, cargoID);
    }

    /// <summary>
    /// 移除购买CEO板条箱冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOBuyingCratesCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 15608, isEnable ? 0 : 300000);          // Special cargo buy cooldown Global_262145.f_15608
    }

    /// <summary>
    /// 移除出售CEO板条箱冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOSellingCratesCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 15609, isEnable ? 0 : 1800000);         // Special cargo sell cooldown Global_262145.f_15609
    }

    /// <summary>
    /// 设置CEO板条箱每箱出售单价为2W
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOPricePerCrateAtCrates(bool isEnable)
    {
        // Global_262145.f_15843    -1445480509                             // tuneables_processing.c
        Hacks.WriteGA(262145 + 15843, isEnable ? 20000 : 10000);            // 1        specialCargo1CratesPrice
        Hacks.WriteGA(262145 + 15843 + 1, isEnable ? 20000 : 11000);        // 2        specialCargo2CratesPrice
        Hacks.WriteGA(262145 + 15843 + 2, isEnable ? 20000 : 12000);        // 3        specialCargo3CratesPrice                                                
        Hacks.WriteGA(262145 + 15843 + 3, isEnable ? 20000 : 13000);        // 4-5      specialCargo4to5CratesPrice
        Hacks.WriteGA(262145 + 15843 + 4, isEnable ? 20000 : 13500);        // 6-7      specialCargo6to7CratesPrice
        Hacks.WriteGA(262145 + 15843 + 5, isEnable ? 20000 : 14000);        // 8-9      ...
        Hacks.WriteGA(262145 + 15843 + 6, isEnable ? 20000 : 14500);        // 10-14
        Hacks.WriteGA(262145 + 15843 + 7, isEnable ? 20000 : 15000);        // 15-19
        Hacks.WriteGA(262145 + 15843 + 8, isEnable ? 20000 : 15500);        // 20-24
        Hacks.WriteGA(262145 + 15843 + 9, isEnable ? 20000 : 16000);        // 25-29
        Hacks.WriteGA(262145 + 15843 + 10, isEnable ? 20000 : 16500);       // 30-34
        Hacks.WriteGA(262145 + 15843 + 11, isEnable ? 20000 : 17000);       // 35-39
        Hacks.WriteGA(262145 + 15843 + 12, isEnable ? 20000 : 17500);       // 40-44
        Hacks.WriteGA(262145 + 15843 + 13, isEnable ? 20000 : 17750);       // 45-49
        Hacks.WriteGA(262145 + 15843 + 14, isEnable ? 20000 : 18000);       // 50-59
        Hacks.WriteGA(262145 + 15843 + 15, isEnable ? 20000 : 18250);       // 60-69
        Hacks.WriteGA(262145 + 15843 + 16, isEnable ? 20000 : 18500);       // 70-79
        Hacks.WriteGA(262145 + 15843 + 17, isEnable ? 20000 : 18750);       // 80-89
        Hacks.WriteGA(262145 + 15843 + 18, isEnable ? 20000 : 19000);       // 90-990
        Hacks.WriteGA(262145 + 15843 + 19, isEnable ? 20000 : 19500);       // 100-11
        Hacks.WriteGA(262145 + 15843 + 20, isEnable ? 20000 : 20000);       // 111
    }

    /// <summary>
    /// 移除地堡进货延迟
    /// </summary>
    /// <param name="isEnable"></param>
    public static void BunkerSupplyDelay(bool isEnable)
    {
        Hacks.WriteGA(262145 + 21601, isEnable ? 0 : 600);
    }

    /// <summary>
    /// 设置地堡生产和研究时间为指定时间，单位秒
    /// </summary>
    /// <param name="isEnable"></param>
    /// <param name="produce_time"></param>
    public static void SetBunkerProduceResearchTime(bool isEnable, int produce_time)
    {
        // Base Time to Produce                                                 // tuneables_processing.c
        Hacks.WriteGA(262145 + 21576, isEnable ? produce_time : 600000);        // Product                  215868155 
        Hacks.WriteGA(262145 + 21592, isEnable ? produce_time : 300000);        // Research                 -676414773

        // Time to Produce Reductions
        Hacks.WriteGA(262145 + 21577, isEnable ? produce_time : 90000);         // Production Equipment     631477612
        Hacks.WriteGA(262145 + 21578, isEnable ? produce_time : 90000);         // Production Staff         818645907
        Hacks.WriteGA(262145 + 21593, isEnable ? produce_time : 45000);         // Research Equipment       -1148432846
        Hacks.WriteGA(262145 + 21594, isEnable ? produce_time : 45000);         // Research Staff           510883248
    }

    /// <summary>
    /// 设置地堡进货单价为200元
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetBunkerResupplyCosts(bool isEnable)
    {
        Hacks.WriteGA(262145 + 21347, isEnable ? 200 : 15000);
        Hacks.WriteGA(262145 + 21348, isEnable ? 200 : 15000);
    }

    /// <summary>
    /// 设置地堡远近出货倍数
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetBunkerSaleMultipliers(bool isEnable)
    {
        // Sale Multipliers                                             // tuneables_processing.c
        Hacks.WriteGA(262145 + 21553, isEnable ? 2.0f : 1.0f);          // Near         1865029244
        Hacks.WriteGA(262145 + 21554, isEnable ? 3.0f : 1.5f);          // Far          1021567941
    }

    /// <summary>
    /// 设置摩托帮远近出货倍数
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetMCSaleMultipliers(bool isEnable)
    {
        // Sale Multipliers                                             // tuneables_processing.c
        Hacks.WriteGA(262145 + 19111, isEnable ? 2.0f : 1.0f);          // Near         -823848572
        Hacks.WriteGA(262145 + 19112, isEnable ? 3.0f : 1.5f);          // Far          1763638426
    }

    /// <summary>
    /// 设置地堡原材料消耗量
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetBunkerSuppliesPerUnitProduced(bool isEnable)
    {
        // Supplies Per Unit Produced                                   // tuneables_processing.c
        Hacks.WriteGA(262145 + 21579, isEnable ? 1 : 10);               // Product Base              -1652502760
        Hacks.WriteGA(262145 + 21580, isEnable ? 1 : 5);                // Product Upgraded          1647327744
        Hacks.WriteGA(262145 + 21595, isEnable ? 1 : 2);                // Research Base             1485279815
        Hacks.WriteGA(262145 + 21596, isEnable ? 1 : 1);                // Research Upgraded         2041812011
    }

    /// <summary>
    /// 设置摩托帮原材料消耗量
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetMCSuppliesPerUnitProduced(bool isEnable)
    {
        // Supplies Per Unit Produced                                   // tuneables_processing.c
        Hacks.WriteGA(262145 + 17461, isEnable ? 1 : 4);                // Documents Base            -1839004359
        Hacks.WriteGA(262145 + 17462, isEnable ? 1 : 10);               // Cash Base
        Hacks.WriteGA(262145 + 17463, isEnable ? 1 : 50);               // Cocaine Base
        Hacks.WriteGA(262145 + 17464, isEnable ? 1 : 24);               // Meth Base
        Hacks.WriteGA(262145 + 17465, isEnable ? 1 : 4);                // Weed Base
        Hacks.WriteGA(262145 + 17466, isEnable ? 1 : 2);                // Documents Upgraded
        Hacks.WriteGA(262145 + 17467, isEnable ? 1 : 5);                // Cash Upgraded
        Hacks.WriteGA(262145 + 17468, isEnable ? 1 : 25);               // Cocaine Upgraded
        Hacks.WriteGA(262145 + 17469, isEnable ? 1 : 12);               // Meth Upgraded
        Hacks.WriteGA(262145 + 17470, isEnable ? 1 : 2);                // Weed Upgraded
    }

    /// <summary>
    /// 解锁地堡所有研究 (临时)
    /// </summary>
    /// <param name="isEnable"></param>
    public static void UnlockBunkerResearch(bool isEnable)
    {
        Hacks.WriteGA(262145 + 21729, isEnable ? 1 : 0);                // 886070202
    }

    /// <summary>
    /// 设置夜总会生产时间为指定时间，单位秒
    /// </summary>
    /// <param name="isEnable"></param>
    /// <param name="produce_time"></param>
    public static void SetNightclubProduceTime(bool isEnable, int produce_time)
    {
        // Time to Produce                                                      // tuneables_processing.c
        Hacks.WriteGA(262145 + 24394, isEnable ? produce_time : 4800000);       // Sporting Goods               -147565853
        Hacks.WriteGA(262145 + 24395, isEnable ? produce_time : 14400000);      // South American Imports
        Hacks.WriteGA(262145 + 24396, isEnable ? produce_time : 7200000);       // Pharmaceutical Research
        Hacks.WriteGA(262145 + 24397, isEnable ? produce_time : 2400000);       // Organic Produce
        Hacks.WriteGA(262145 + 24398, isEnable ? produce_time : 1800000);       // Printing and Copying
        Hacks.WriteGA(262145 + 24399, isEnable ? produce_time : 3600000);       // Cash Creation
        Hacks.WriteGA(262145 + 24400, isEnable ? produce_time : 8400000);       // Cargo and Shipments          1607981264
    }

    /// <summary>
    /// 设置摩托帮生产时间为指定时间，单位秒
    /// </summary>
    /// <param name="isEnable"></param>
    /// <param name="produce_time"></param>
    public static void SetMCProduceTime(bool isEnable, int produce_time)
    {
        // Base Time to Produce                                                 // tuneables_processing.c
        Hacks.WriteGA(262145 + 17446, isEnable ? produce_time : 360000);        // Weed                     -635596193
        Hacks.WriteGA(262145 + 17447, isEnable ? produce_time : 1800000);       // Meth
        Hacks.WriteGA(262145 + 17448, isEnable ? produce_time : 3000000);       // Cocaine
        Hacks.WriteGA(262145 + 17449, isEnable ? produce_time : 300000);        // Documents
        Hacks.WriteGA(262145 + 17450, isEnable ? produce_time : 720000);        // Cash                     1310272402

        // Time to Produce Reductions
        Hacks.WriteGA(262145 + 17451, isEnable ? 1 : 60000);                    // Documents Equipment      1672482518
        Hacks.WriteGA(262145 + 17452, isEnable ? 1 : 120000);                   // Cash Equipment
        Hacks.WriteGA(262145 + 17453, isEnable ? 1 : 600000);                   // Cocaine Equipment
        Hacks.WriteGA(262145 + 17454, isEnable ? 1 : 360000);                   // Meth Equipment
        Hacks.WriteGA(262145 + 17455, isEnable ? 1 : 60000);                    // Weed Equipment
        Hacks.WriteGA(262145 + 17456, isEnable ? 1 : 60000);                    // Documents Staff
        Hacks.WriteGA(262145 + 17457, isEnable ? 1 : 120000);                   // Cash Staff
        Hacks.WriteGA(262145 + 17458, isEnable ? 1 : 600000);                   // Cocaine Staff
        Hacks.WriteGA(262145 + 17459, isEnable ? 1 : 360000);                   // Meth Staff
        Hacks.WriteGA(262145 + 17460, isEnable ? 1 : 60000);                    // Weed Staff               1575359233
    }

    /// <summary>
    /// 移除摩托帮进货延迟
    /// </summary>
    /// <param name="isEnable"></param>
    public static void MCSupplyDelay(bool isEnable)
    {
        Hacks.WriteGA(262145 + 18999, isEnable ? 0 : 600);          // 728170457  tuneables_processing.c
    }

    /// <summary>
    /// 设置摩托帮进货单价为200元
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetMCResupplyCosts(bool isEnable)
    {
        Hacks.WriteGA(262145 + 18998, isEnable ? 200 : 15000);      // Discounted Resupply Cost, BIKER_PURCHASE_SUPPLIES_COST_PER_SEGMENT
    }

    /// <summary>
    /// 设置梅利威瑟服务类型
    /// </summary>
    /// <param name="serverId"></param>
    public static void MerryWeatherServices(int serverId)
    {
        Hacks.WriteGA(2815059 + serverId, 1);
    }

    /// <summary>
    /// 移除进出口大亨出货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void ExportVehicleDelay(bool isEnable)
    {
        Hacks.WriteGA(262145 + 19727, isEnable ? 0 : 1200000);         // 1001423248  tuneables_processing.c
        Hacks.WriteGA(262145 + 19728, isEnable ? 0 : 1680000);
        Hacks.WriteGA(262145 + 19729, isEnable ? 0 : 2340000);
        Hacks.WriteGA(262145 + 19730, isEnable ? 0 : 2880000);         // -824005590
    }

    /// <summary>
    /// 断开战局连接
    /// </summary>
    public static void Disconnect()
    {
        // at 1.63 freemode.c func_20826
        Hacks.WriteGA(32236, 1);
        Thread.Sleep(200);
        Hacks.WriteGA(32236, 0);
    }

    /// <summary>
    /// 结束过场动画
    /// </summary>
    public static void StopCutscene()
    {
        // at 1.63 freemode.c func_12850
        Hacks.WriteGA(2789756 + 3, 1);
        Hacks.WriteGA(1575058, 1);
    }

    /// <summary>
    /// 移除机库进货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SmugglerRunInDelay(bool isEnable)
    {
        Hacks.WriteGA(262145 + 22779, isEnable ? 0 : 120000);          // 1278611667  tuneables_processing.c
        Hacks.WriteGA(262145 + 22780, isEnable ? 0 : 180000);
        Hacks.WriteGA(262145 + 22781, isEnable ? 0 : 240000);
        Hacks.WriteGA(262145 + 22782, isEnable ? 0 : 60000);
    }

    /// <summary>
    /// 移除机库出货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SmugglerRunOutDelay(bool isEnable)
    {
        Hacks.WriteGA(262145 + 22820, isEnable ? 0 : 180000);          // -1525481945  tuneables_processing.c
    }

    /// <summary>
    /// 移除夜总会出货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void NightclubOutDelay(bool isEnable)
    {
        Hacks.WriteGA(262145 + 24475, isEnable ? 0 : 300000);          // 1763921019  tuneables_processing.c
        Hacks.WriteGA(262145 + 24517, isEnable ? 0 : 300000);          // -1004589438
        Hacks.WriteGA(262145 + 24518, isEnable ? 0 : 300000);
    }

    /// <summary>
    /// 夜总会托尼洗钱费用
    /// </summary>
    /// <param name="isEnable"></param>
    public static void NightclubNoTonyLaunderingMoney(bool isEnable)
    {
        Hacks.WriteGA(262145 + 24524, isEnable ? 0.000001f : 0.1f);        // -1002770353  tuneables_processing.c
    }

    /// <summary>
    /// 未知功能
    /// </summary>
    /// <param name="index"></param>
    public static void DeliverPersonalVehicle(int index)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 965, index);
        Hacks.WriteGA(Offsets.oVMYCar + 962, 1);
    }

    /// <summary>
    /// 移除CEO工作冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOWorkCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 13078, isEnable ? 0 : 300000);
        Hacks.WriteGA(262145 + 12975, isEnable ? 0 : 600000);
    }

    /// <summary>
    /// 移除恐霸客户差事冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void ClientJonCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 24664 + 0, isEnable ? 0 : 300000);       // Between Jobs
        Hacks.WriteGA(262145 + 24664 + 1, isEnable ? 0 : 1800000);      // Robbery in Progress
        Hacks.WriteGA(262145 + 24664 + 2, isEnable ? 0 : 1800000);      // Data Sweep
        Hacks.WriteGA(262145 + 24664 + 3, isEnable ? 0 : 1800000);      // Targeted Data
        Hacks.WriteGA(262145 + 24664 + 4, isEnable ? 0 : 1800000);      //  Diamond Shopping
    }

    /// <summary>
    /// 移除事务所安保合约冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SecurityHitCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 31689, isEnable ? 0 : 300000);
    }

    /// <summary>
    /// 移除公共电话任务合约冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void PayphoneHitCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 31769, isEnable ? 0 : 1200000);
    }

    /// <summary>
    /// 进入RC匪徒
    /// </summary>
    /// <param name="isEnable"></param>
    public static void TriggerRCBandito(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 6751, isEnable ? 1 : 0);
    }

    /// <summary>
    /// 进入迷你坦克
    /// </summary>
    /// <param name="isEnable"></param>
    public static void TriggerMiniTank(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 6752, isEnable ? 1 : 0);
    }
}
