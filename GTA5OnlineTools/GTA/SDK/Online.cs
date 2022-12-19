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
        // STATS::PLAYSTATS_IDLE_KICK
        Hacks.WriteGA(262145 + 87, isEnable ? 99999999 : 120000);        // 120000     joaat("IDLEKICK_WARNING1") 
        Hacks.WriteGA(262145 + 88, isEnable ? 99999999 : 300000);        // 300000     joaat("IDLEKICK_WARNING2")
        Hacks.WriteGA(262145 + 89, isEnable ? 99999999 : 600000);        // 600000     joaat("IDLEKICK_WARNING3")
        Hacks.WriteGA(262145 + 90, isEnable ? 99999999 : 900000);        // 900000     joaat("IDLEKICK_KICK")

        Hacks.WriteGA(262145 + 8248, isEnable ? 2000000000 : 30000);     // 30000      joaat("ConstrainedKick_Warning1")
        Hacks.WriteGA(262145 + 8249, isEnable ? 2000000000 : 60000);     // 60000      joaat("ConstrainedKick_Warning2")
        Hacks.WriteGA(262145 + 8250, isEnable ? 2000000000 : 90000);     // 90000      joaat("ConstrainedKick_Warning3")
        Hacks.WriteGA(262145 + 8251, isEnable ? 2000000000 : 120000);    // 120000     joaat("ConstrainedKick_Kick")
    }

    /// <summary>
    /// 模型变更
    /// </summary>
    /// <param name="hash"></param>
    public static void ModelChange(long hash)
    {
        // else if (!PED::HAS_PED_HEAD_BLEND_FINISHED(PLAYER::PLAYER_PED_ID())
        Hacks.WriteGA(Offsets.oVGETIn + 61, 1);                 // triggerModelChange   Global_2671449.f_59
        Hacks.WriteGA(Offsets.oVGETIn + 48, hash);              // modelChangeHash      Global_2671449.f_46
        Thread.Sleep(10);
        Hacks.WriteGA(Offsets.oVGETIn + 61, 0);
    }

    /// <summary>
    /// 允许非公共战局运货
    /// </summary>
    /// <param name="isEnable"></param>
    public static void AllowSellOnNonPublic(bool isEnable)
    {
        Hacks.WriteGA(2683862 + 744, isEnable ? 0 : 1);         // NETWORK::NETWORK_SESSION_GET_PRIVATE_SLOTS()
    }

    /// <summary>
    /// 移除被动模式CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void PassiveModeCooldown(bool isEnable)
    {
        Hacks.WriteGA(2793044 + 4490, isEnable ? 0 : 1);            // AUDIO::REQUEST_SCRIPT_AUDIO_BANK("DLC_HEI4/DLC_HEI4_Submarine"
        Hacks.WriteGA(1970698, isEnable ? 0 : 1);                   // if (ENTITY::GET_PED_INDEX_FROM_ENTITY_INDEX(
    }

    /// <summary>
    /// 移除自杀CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SuicideCooldown(bool isEnable)
    {
        if (isEnable)
            Hacks.WriteGA(Offsets.oVMYCar + 6899, 0);

        Hacks.WriteGA(262145 + 28408, isEnable ? 3 : 300000);
        Hacks.WriteGA(262145 + 28409, isEnable ? 3 : 60000);
    }

    /// <summary>
    /// 移除轨道炮CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void DisableOrbitalCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 23084, isEnable ? 0 : 2880000);         // -1707434973
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
        Hacks.WriteGA(262145 + 4752, isEnable ? 1 : 0);            // joaat("turn_snow_on_off")
    }

    /// <summary>
    /// 雷达影踪/人间蒸发
    /// </summary>
    /// <param name="isEnable"></param>
    public static void OffRadar(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oPlayerIDHelp + 1 + Hacks.GetPlayerID() * 466 + 210, isEnable ? 1 : 0);
        if (isEnable)
            Hacks.WriteGA(Offsets.oNETTimeHelp + 56, Hacks.GetNetworkTime() + 3600000);
        Hacks.WriteGA(Offsets.oVMYCar + 4660, isEnable ? 3 : 0);
    }

    /// <summary>
    /// 幽灵组织
    /// </summary>
    /// <param name="isEnable"></param>
    public static void GhostOrganization(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oPlayerIDHelp + 1 + Hacks.GetPlayerID() * 466 + 210, isEnable ? 1 : 0);
        if (isEnable)
            Hacks.WriteGA(Offsets.oNETTimeHelp + 56, Hacks.GetNetworkTime() + 3600000);        // iVar0 = NETWORK::GET_TIME_DIFFERENCE(NETWORK::GET_NETWORK_TIME()
        Hacks.WriteGA(Offsets.oVMYCar + 4660, isEnable ? 4 : 0);
    }

    /// <summary>
    /// 警察无视犯罪
    /// </summary>
    /// <param name="isEnable"></param>
    public static void BribeOrBlindCops(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 4654 + 1, isEnable ? 1 : 0);
        Hacks.WriteGA(Offsets.oVMYCar + 4654 + 3, isEnable ? Hacks.GetNetworkTime() + 3600000 : 0);
        Hacks.WriteGA(Offsets.oVMYCar + 4654, isEnable ? 5 : 0);
    }

    /// <summary>
    /// 贿赂当局
    /// </summary>
    /// <param name="isEnable"></param>
    public static void BribeAuthorities(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 4654 + 1, isEnable ? 1 : 0);
        Hacks.WriteGA(Offsets.oVMYCar + 4654 + 3, isEnable ? Hacks.GetNetworkTime() + 3600000 : 0);
        Hacks.WriteGA(Offsets.oVMYCar + 4654, isEnable ? 21 : 0);
    }

    /// <summary>
    /// 显示玩家
    /// </summary>
    /// <param name="isEnable"></param>
    public static void RevealPlayers(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oPlayerIDHelp + 1 + Hacks.GetPlayerID() * 466 + 213, isEnable ? 1 : 0);
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
        Hacks.WriteGA(262145 + 25926, multiplier);
    }

    /// <summary>
    /// 设置车友会等级经验倍数
    /// </summary>
    /// <param name="multiplier"></param>
    public static void REPMultiplier(float multiplier)
    {
        Hacks.WriteGA(262145 + 31648, multiplier);        // Street Race         街头比赛        -147149995
        Hacks.WriteGA(262145 + 31649, multiplier);        // Pursuit Race        追逐赛
        Hacks.WriteGA(262145 + 31650, multiplier);        // Scramble            攀登
        Hacks.WriteGA(262145 + 31651, multiplier);        // Head 2 Head         头对头          1434998920

        Hacks.WriteGA(262145 + 31653, multiplier);        // LS Car Meet         汽车见面会       1819417801
        Hacks.WriteGA(262145 + 31654, multiplier);        // LS Car Meet Track
        Hacks.WriteGA(262145 + 31655, multiplier);        // LS Car Meet Cloth Shop
    }

    /// <summary>
    /// 使用牛鲨睾酮
    /// </summary>
    /// <param name="isEnable"></param>
    public static void InstantBullShark(bool isEnable)
    {
        if (isEnable)
            Hacks.WriteGA(Offsets.oNETTimeHelp + 3689, 1);
        else
        {
            int temp = Hacks.ReadGA<int>(Offsets.oNETTimeHelp + 3689);
            if (temp != 0)
                Hacks.WriteGA(Offsets.oNETTimeHelp + 3689, 5);
        }
    }

    /// <summary>
    /// 呼叫支援直升机
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CallBackupHeli(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 4484, isEnable ? 1 : 0);
    }

    /// <summary>
    /// 呼叫空袭
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CallAirstrike(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 4485, isEnable ? 1 : 0);
    }

    /// <summary>
    /// 启用CEO特殊货物
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOSpecialCargo(bool isEnable)
    {
        Hacks.WriteGA(1949968, isEnable ? 1 : 0);           // MISC::GET_RANDOM_INT_IN_RANGE(1, 101);
    }

    /// <summary>
    /// 设置CEO特殊货物类型
    /// </summary>
    /// <param name="cargoID"></param>
    public static void CEOCargoType(int cargoID)
    {
        Hacks.WriteGA(1949814, cargoID);
    }

    /// <summary>
    /// 移除购买CEO板条箱冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOBuyingCratesCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 15553, isEnable ? 0 : 300000);          // 153204142 joaat("EXEC_BUY_COOLDOWN")
    }

    /// <summary>
    /// 移除出售CEO板条箱冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOSellingCratesCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 15554, isEnable ? 0 : 1800000);         // 1291620941 joaat("EXEC_SELL_COOLDOWN")
    }

    /// <summary>
    /// 设置CEO板条箱每箱出售单价为2W
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOPricePerCrateAtCrates(bool isEnable)
    {
        // -1445480509 joaat("EXEC_CONTRABAND_SALE_VALUE_THRESHOLD1")
        Hacks.WriteGA(262145 + 15788, isEnable ? 20000 : 10000);            // 1
        Hacks.WriteGA(262145 + 15788 + 1, isEnable ? 20000 : 11000);        // 2
        Hacks.WriteGA(262145 + 15788 + 2, isEnable ? 20000 : 12000);        // 3
        Hacks.WriteGA(262145 + 15788 + 3, isEnable ? 20000 : 13000);        // 4-5
        Hacks.WriteGA(262145 + 15788 + 4, isEnable ? 20000 : 13500);        // 6-7
        Hacks.WriteGA(262145 + 15788 + 5, isEnable ? 20000 : 14000);        // 8-9
        Hacks.WriteGA(262145 + 15788 + 6, isEnable ? 20000 : 14500);        // 10-14
        Hacks.WriteGA(262145 + 15788 + 7, isEnable ? 20000 : 15000);        // 15-19
        Hacks.WriteGA(262145 + 15788 + 8, isEnable ? 20000 : 15500);        // 20-24
        Hacks.WriteGA(262145 + 15788 + 9, isEnable ? 20000 : 16000);        // 25-29
        Hacks.WriteGA(262145 + 15788 + 10, isEnable ? 20000 : 16500);       // 30-34
        Hacks.WriteGA(262145 + 15788 + 11, isEnable ? 20000 : 17000);       // 35-39
        Hacks.WriteGA(262145 + 15788 + 12, isEnable ? 20000 : 17500);       // 40-44
        Hacks.WriteGA(262145 + 15788 + 13, isEnable ? 20000 : 17750);       // 45-49
        Hacks.WriteGA(262145 + 15788 + 14, isEnable ? 20000 : 18000);       // 50-59
        Hacks.WriteGA(262145 + 15788 + 15, isEnable ? 20000 : 18250);       // 60-69
        Hacks.WriteGA(262145 + 15788 + 16, isEnable ? 20000 : 18500);       // 70-79
        Hacks.WriteGA(262145 + 15788 + 17, isEnable ? 20000 : 18750);       // 80-89
        Hacks.WriteGA(262145 + 15788 + 18, isEnable ? 20000 : 19000);       // 90-990
        Hacks.WriteGA(262145 + 15788 + 19, isEnable ? 20000 : 19500);       // 100-11
        Hacks.WriteGA(262145 + 15788 + 20, isEnable ? 20000 : 20000);       // 111
    }

    /// <summary>
    /// 移除地堡进货延迟
    /// </summary>
    /// <param name="isEnable"></param>
    public static void BunkerSupplyDelay(bool isEnable)
    {
        Hacks.WriteGA(262145 + 21557, isEnable ? 0 : 600);                      // -2094564985 joaat("GR_PURCHASE_SUPPLIES_DELAY")
    }

    /// <summary>
    /// 设置地堡生产和研究时间为指定时间，单位秒
    /// </summary>
    /// <param name="isEnable"></param>
    /// <param name="produce_time"></param>
    public static void SetBunkerProduceResearchTime(bool isEnable, int produce_time = 1)
    {
        // Base Time to Produce                                                 // tuneables_processing.c
        Hacks.WriteGA(262145 + 21532, isEnable ? produce_time : 600000);        // Product                  215868155 
        Hacks.WriteGA(262145 + 21548, isEnable ? produce_time : 300000);        // Research                 -676414773 joaat("GR_RESEARCH_PRODUCTION_TIME")

        // Time to Produce Reductions
        Hacks.WriteGA(262145 + 21533, isEnable ? produce_time : 90000);         // Production Equipment     631477612
        Hacks.WriteGA(262145 + 21534, isEnable ? produce_time : 90000);         // Production Staff         818645907
        Hacks.WriteGA(262145 + 21593, isEnable ? produce_time : 45000);         // Research Equipment       -1148432846
        Hacks.WriteGA(262145 + 21594, isEnable ? produce_time : 45000);         // Research Staff           510883248
    }

    /// <summary>
    /// 设置地堡进货单价为200元
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetBunkerResupplyCosts(bool isEnable)
    {
        Hacks.WriteGA(262145 + 21347, isEnable ? 200 : 15000);          // 2022/12/19 未更新
        Hacks.WriteGA(262145 + 21348, isEnable ? 200 : 15000);
    }

    /// <summary>
    /// 设置地堡远近出货倍数
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetBunkerSaleMultipliers(bool isEnable)
    {
        // Sale Multipliers                                             // tuneables_processing.c
        Hacks.WriteGA(262145 + 21509, isEnable ? 2.0f : 1.0f);          // Near         1865029244
        Hacks.WriteGA(262145 + 21510, isEnable ? 3.0f : 1.5f);          // Far          1021567941
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
        Hacks.WriteGA(Offsets.oVMYCar + serverId, 1);
    }

    /// <summary>
    /// 移除进出口大亨出货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void ExportVehicleDelay(bool isEnable)
    {
        Hacks.WriteGA(262145 + 19682, isEnable ? 0 : 1200000);         // 1001423248  tuneables_processing.c
        Hacks.WriteGA(262145 + 19683, isEnable ? 0 : 1680000);
        Hacks.WriteGA(262145 + 19684, isEnable ? 0 : 2340000);
        Hacks.WriteGA(262145 + 19685, isEnable ? 0 : 2880000);         // -824005590
    }

    /// <summary>
    /// 断开战局连接
    /// </summary>
    public static void Disconnect()
    {
        Hacks.WriteGA(32241, 1);        // NETWORK::NETWORK_BAIL(1, 0, 0)
        Thread.Sleep(200);              // return NETWORK::NETWORK_CAN_ACCESS_MULTIPLAYER
        Hacks.WriteGA(32241, 0);
    }

    /// <summary>
    /// 结束过场动画
    /// </summary>
    public static void StopCutscene()
    {
        Hacks.WriteGA(2766500 + 3, 1);      // return MISC::GET_HASH_KEY("AVISA")
        Hacks.WriteGA(1575060, 1);          // HUD::SET_FRONTEND_ACTIVE(false)
    }

    /// <summary>
    /// 移除机库进货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SmugglerRunInDelay(bool isEnable)
    {
        Hacks.WriteGA(262145 + 22751, isEnable ? 0 : 120000);          // 1278611667  tuneables_processing.c
        Hacks.WriteGA(262145 + 22752, isEnable ? 0 : 180000);
        Hacks.WriteGA(262145 + 22753, isEnable ? 0 : 240000);
        Hacks.WriteGA(262145 + 22754, isEnable ? 0 : 60000);
    }

    /// <summary>
    /// 移除机库出货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SmugglerRunOutDelay(bool isEnable)
    {
        Hacks.WriteGA(262145 + 22792, isEnable ? 0 : 180000);          // -1525481945  tuneables_processing.c
    }

    /// <summary>
    /// 移除夜总会出货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void NightclubOutDelay(bool isEnable)
    {
        Hacks.WriteGA(262145 + 24447, isEnable ? 0 : 300000);          // 1763921019  tuneables_processing.c
        Hacks.WriteGA(262145 + 24489, isEnable ? 0 : 300000);          // -1004589438
        Hacks.WriteGA(262145 + 24490, isEnable ? 0 : 300000);
    }

    /// <summary>
    /// 夜总会托尼洗钱费用
    /// </summary>
    /// <param name="isEnable"></param>
    public static void NightclubNoTonyLaunderingMoney(bool isEnable)
    {
        Hacks.WriteGA(262145 + 24496, isEnable ? 0.000001f : 0.1f);        // -1002770353  tuneables_processing.c
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
        Hacks.WriteGA(262145 + 13081, isEnable ? 0 : 300000);       // -1404265088
        Hacks.WriteGA(262145 + 12978, isEnable ? 0 : 600000);       // -1911318106 joaat("GB_SIGHTSEER_COOLDOWN")
    }

    /// <summary>
    /// 移除恐霸客户差事冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void ClientJonCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 24636 + 0, isEnable ? 0 : 300000);       // Between Jobs -926426916
        Hacks.WriteGA(262145 + 24636 + 1, isEnable ? 0 : 1800000);      // Robbery in Progress
        Hacks.WriteGA(262145 + 24636 + 2, isEnable ? 0 : 1800000);      // Data Sweep
        Hacks.WriteGA(262145 + 24636 + 3, isEnable ? 0 : 1800000);      // Targeted Data
        Hacks.WriteGA(262145 + 24636 + 4, isEnable ? 0 : 1800000);      // Diamond Shopping
    }

    /// <summary>
    /// 移除事务所安保合约冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SecurityHitCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 31701, isEnable ? 0 : 300000);           // -1462622971 joaat("FIXER_SECURITY_CONTRACT_COOLDOWN_TIME")
    }

    /// <summary>
    /// 移除公共电话任务合约冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void PayphoneHitCooldown(bool isEnable)
    {
        Hacks.WriteGA(262145 + 31781, isEnable ? 0 : 1200000);          // 1872071131
    }

    /// <summary>
    /// 进入RC匪徒
    /// </summary>
    /// <param name="isEnable"></param>
    public static void TriggerRCBandito(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 6874, isEnable ? 1 : 0);
    }

    /// <summary>
    /// 进入迷你坦克
    /// </summary>
    /// <param name="isEnable"></param>
    public static void TriggerMiniTank(bool isEnable)
    {
        Hacks.WriteGA(Offsets.oVMYCar + 6875, isEnable ? 1 : 0);
    }
}
