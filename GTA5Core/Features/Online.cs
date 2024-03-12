using GTA5Core.Native;
using GTA5Core.Offsets;

namespace GTA5Core.Features;

public static class Online
{
    /// <summary>
    /// 线上战局切换
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
                Globals.Set_Global_Value(Base.SessionSwitchCache, -1);
            }
            else
            {
                // 正常切换战局，修改战局类型
                Globals.Set_Global_Value(Base.SessionSwitchType, sessionID);
            }

            // 切换战局状态
            Globals.Set_Global_Value(Base.SessionSwitchState, 1);
            await Task.Delay(200);
            Globals.Set_Global_Value(Base.SessionSwitchState, 0);
        });
    }

    /// <summary>
    /// 空战局
    /// </summary>
    public static void EmptySession()
    {
        Task.Run(async () =>
        {
            ProcessMgr.SuspendProcess();
            await Task.Delay(10000);
            ProcessMgr.ResumeProcess();
        });
    }

    /// <summary>
    /// 挂机防踢
    /// </summary>
    /// <param name="isEnable"></param>
    public static void AntiAFK(bool isEnable)
    {
        // STATS::PLAYSTATS_IDLE_KICK
        Globals.Set_Global_Value(Base.Default + 87, isEnable ? 99999999 : 120000);        // 120000     joaat("IDLEKICK_WARNING1") 
        Globals.Set_Global_Value(Base.Default + 88, isEnable ? 99999999 : 300000);        // 300000     joaat("IDLEKICK_WARNING2")
        Globals.Set_Global_Value(Base.Default + 89, isEnable ? 99999999 : 600000);        // 600000     joaat("IDLEKICK_WARNING3")
        Globals.Set_Global_Value(Base.Default + 90, isEnable ? 99999999 : 900000);        // 900000     joaat("IDLEKICK_KICK")

        Globals.Set_Global_Value(Base.Default + 8426, isEnable ? 2000000000 : 30000);     // 30000      joaat("ConstrainedKick_Warning1")
        Globals.Set_Global_Value(Base.Default + 8427, isEnable ? 2000000000 : 60000);     // 60000      joaat("ConstrainedKick_Warning2")
        Globals.Set_Global_Value(Base.Default + 8428, isEnable ? 2000000000 : 90000);     // 90000      joaat("ConstrainedKick_Warning3")
        Globals.Set_Global_Value(Base.Default + 8429, isEnable ? 2000000000 : 120000);    // 120000     joaat("ConstrainedKick_Kick")
    }

    /// <summary>
    /// 免费更改角色外观
    /// </summary>
    /// <param name="isEnable"></param>
    public static void FreeChangeAppearance(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 19320, isEnable ? 0 : 100000);         // joaat("CHARACTER_APPEARANCE_CHARGE")
    }

    /// <summary>
    /// 移除更改角色外观冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void ChangeAppearanceCooldown(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 19321, isEnable ? 0 : 2880000);         // joaat("CHARACTER_APPEARANCE_COOLDOWN")
    }

    /// <summary>
    /// 模型变更
    /// </summary>
    /// <param name="hash"></param>
    public static async void ModelChange(long hash)
    {
        // else if (!PED::HAS_PED_HEAD_BLEND_FINISHED(PLAYER::PLAYER_PED_ID())
        Globals.Set_Global_Value(Base.oVGETIn + 62, 1);                 // triggerModelChange
        Globals.Set_Global_Value(Base.oVGETIn + 49, hash);              // modelChangeHash
        await Task.Delay(10);
        Globals.Set_Global_Value(Base.oVGETIn + 62, 0);
    }

    /// <summary>
    /// 允许非公共战局运货
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SellOnNonPublic(bool isEnable)
    {
        Globals.Set_Global_Value(2684312 + 744, isEnable ? 0 : 1);         // NETWORK::NETWORK_SESSION_GET_PRIVATE_SLOTS()
    }

    /// <summary>
    /// 移除被动模式CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void PassiveModeCooldown(bool isEnable)
    {
        Globals.Set_Global_Value(2738587 + 4497, isEnable ? 0 : 1);        // AUDIO::REQUEST_SCRIPT_AUDIO_BANK("DLC_HEI4/DLC_HEI4_Submarine" // _STOPWATCH_RESET(&(Global_2794162.f_4497), false, false);
        Globals.Set_Global_Value(1963748, isEnable ? 0 : 1);               // joaat("VEHICLE_WEAPON_SUB_MISSILE_HOMING")
    }

    /// <summary>
    /// 移除自杀CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SuicideCooldown(bool isEnable)
    {
        if (isEnable)
            Globals.Set_Global_Value(Base.oVMYCar + 6937, 0);      // joaat("XPCATEGORY_ACTION_KILLS")

        Globals.Set_Global_Value(Base.Default + 28685, isEnable ? 1 : 300000);         // 247954694
        Globals.Set_Global_Value(Base.Default + 28686, isEnable ? 1 : 60000);          // -1771488297
    }

    /// <summary>
    /// 移除轨道炮CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void OrbitalCooldown(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 23294, isEnable ? 0 : 2880000);         // -1707434973
    }

    /// <summary>
    /// 进入线上个人载具
    /// </summary>
    public static void GetInOnlinePV()
    {
        Globals.Set_Global_Value(Base.oVGETIn + 8, 1);     // (PLAYER::PLAYER_ID()), 0f, 0f, 0f, Global_
    }

    /// <summary>
    /// 战局雪天
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SessionSnow(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 4575, isEnable ? 1 : 0);            // joaat("turn_snow_on_off")
    }

    /// <summary>
    /// 雷达影踪/人间蒸发
    /// </summary>
    /// <param name="isEnable"></param>
    public static void OffRadar(bool isEnable)
    {
        // Global_2657704[PLAYER::PLAYER_ID() /*463*/].f_210
        // timeDifference = NETWORK::GET_TIME_DIFFERENCE(NETWORK::GET_NETWORK_TIME(), Global_2672524.f_57);
        Globals.Set_Global_Value(Base.oPlayerIDHelp + 1 + Globals.GetPlayerID() * 463 + 210, isEnable ? 1 : 0);
        if (isEnable)
            Globals.Set_Global_Value(Base.oNETTimeHelp + 58, Globals.GetNetworkTime() + 3600000);
        Globals.Set_Global_Value(Base.oVMYCar + 4667, isEnable ? 3 : 0);
    }

    /// <summary>
    /// 幽灵组织
    /// </summary>
    /// <param name="isEnable"></param>
    public static void GhostOrganization(bool isEnable)
    {
        Globals.Set_Global_Value(Base.oPlayerIDHelp + 1 + Globals.GetPlayerID() * 463 + 210, isEnable ? 1 : 0);
        if (isEnable)
            Globals.Set_Global_Value(Base.oNETTimeHelp + 58, Globals.GetNetworkTime() + 3600000);        // iVar0 = NETWORK::GET_TIME_DIFFERENCE(NETWORK::GET_NETWORK_TIME()
        Globals.Set_Global_Value(Base.oVMYCar + 4667, isEnable ? 4 : 0);
    }

    /// <summary>
    /// 警察无视犯罪
    /// </summary>
    /// <param name="isEnable"></param>
    public static void BribeOrBlindCops(bool isEnable)
    {
        Globals.Set_Global_Value(Base.oVMYCar + 4661 + 1, isEnable ? 1 : 0);           // _DISPLAY_HELP_TEXT("FM_LEST_NCPW"
        Globals.Set_Global_Value(Base.oVMYCar + 4661 + 3, isEnable ? Globals.GetNetworkTime() + 3600000 : 0);
        Globals.Set_Global_Value(Base.oVMYCar + 4661, isEnable ? 5 : 0);
    }

    /// <summary>
    /// 贿赂当局
    /// </summary>
    /// <param name="isEnable"></param>
    public static void BribeAuthorities(bool isEnable)
    {
        Globals.Set_Global_Value(Base.oVMYCar + 4661 + 1, isEnable ? 1 : 0);
        Globals.Set_Global_Value(Base.oVMYCar + 4661 + 3, isEnable ? Globals.GetNetworkTime() + 3600000 : 0);
        Globals.Set_Global_Value(Base.oVMYCar + 4661, isEnable ? 21 : 0);
    }

    /// <summary>
    /// 显示玩家
    /// </summary>
    /// <param name="isEnable"></param>
    public static void RevealPlayers(bool isEnable)
    {
        Globals.Set_Global_Value(Base.oPlayerIDHelp + 1 + Globals.GetPlayerID() * 463 + 213, isEnable ? 1 : 0);
        Globals.Set_Global_Value(Base.oNETTimeHelp + 59, isEnable ? Globals.GetNetworkTime() + 3600000 : 0);
    }

    /// <summary>
    /// 设置角色等级经验倍数
    /// </summary>
    /// <param name="multiplier"></param>
    public static void RPMultiplier(float multiplier)
    {
        Globals.Set_Global_Value(Base.Default + 1, multiplier);           // joaat("XP_MULTIPLIER")
    }

    /// <summary>
    /// 设置角色AP经验倍数
    /// </summary>
    /// <param name="multiplier"></param>
    public static void APMultiplier(float multiplier)
    {
        Globals.Set_Global_Value(Base.Default + 26184, multiplier);      // joaat("AP_MULTIPLIER")
    }

    /// <summary>
    /// 设置车友会等级经验倍数
    /// </summary>
    /// <param name="multiplier"></param>
    public static void REPMultiplier(float multiplier)
    {
        Globals.Set_Global_Value(Base.Default + 31966, multiplier);        // Street Race         街头比赛        -147149995  joaat("TUNER_STREET_RACE_PLACE_XP_MULTIPLIER")
        Globals.Set_Global_Value(Base.Default + 31967, multiplier);        // Pursuit Race        追逐赛
        Globals.Set_Global_Value(Base.Default + 31968, multiplier);        // Scramble            攀登
        Globals.Set_Global_Value(Base.Default + 31969, multiplier);        // Head 2 Head         头对头          1434998920

        Globals.Set_Global_Value(Base.Default + 31971, multiplier);        // LS Car Meet         汽车见面会       1819417801  joaat("TUNER_CARCLUB_TIME_XP_MULTIPLIER")
        Globals.Set_Global_Value(Base.Default + 31972, multiplier);        // LS Car Meet Track
        Globals.Set_Global_Value(Base.Default + 31973, multiplier);        // LS Car Meet Cloth Shop
    }

    /// <summary>
    /// 使用牛鲨睾酮
    /// </summary>
    /// <param name="isEnable"></param>
    public static void InstantBullShark(bool isEnable)
    {
        if (isEnable)
            Globals.Set_Global_Value(Base.oNETTimeHelp + 3694, 1);
        else
        {
            var temp = Globals.Get_Global_Value<int>(Base.oNETTimeHelp + 3694);
            if (temp != 0)
                Globals.Set_Global_Value(Base.oNETTimeHelp + 3694, 5);
        }
    }

    /// <summary>
    /// 呼叫支援直升机
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CallBackupHeli(bool isEnable)
    {
        Globals.Set_Global_Value(Base.oVMYCar + 4491, isEnable ? 1 : 0);   // SCRIPT::GET_NUMBER_OF_THREADS_RUNNING_THE_SCRIPT_WITH_THIS_HASH(joaat("am_backup_heli")
    }

    /// <summary>
    /// 呼叫空袭
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CallAirstrike(bool isEnable)
    {
        Globals.Set_Global_Value(Base.oVMYCar + 4492, isEnable ? 1 : 0);   // SCRIPT::GET_NUMBER_OF_THREADS_RUNNING_THE_SCRIPT_WITH_THIS_HASH(joaat("am_airstrike")
    }

    /// <summary>
    /// 启用CEO特殊货物
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOSpecialCargo(bool isEnable)
    {
        Globals.Set_Global_Value(1942640, isEnable ? 1 : 0);           // MISC::GET_RANDOM_INT_IN_RANGE(1, 101);   // Global_1950703 = 1;
    }

    /// <summary>
    /// 设置CEO特殊货物类型
    /// </summary>
    /// <param name="cargoID"></param>
    public static void CEOCargoType(int cargoID)
    {
        Globals.Set_Global_Value(1942486, cargoID);                    // MISC::GET_RANDOM_INT_IN_RANGE(1, 101);   // Global_1950549 = num;
    }

    /// <summary>
    /// 移除购买CEO板条箱冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOBuyingCratesCooldown(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 15756, isEnable ? 0 : 300000);          // 153204142 joaat("EXEC_BUY_COOLDOWN")
    }

    /// <summary>
    /// 移除出售CEO板条箱冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOSellingCratesCooldown(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 15757, isEnable ? 0 : 1800000);         // 1291620941 joaat("EXEC_SELL_COOLDOWN")
    }

    /// <summary>
    /// 移除地堡进货延迟
    /// </summary>
    /// <param name="isEnable"></param>
    public static void BunkerSupplyDelay(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 21767, isEnable ? 0 : 600);            // -2094564985 joaat("GR_PURCHASE_SUPPLIES_DELAY")
    }

    /// <summary>
    /// 解锁地堡所有研究 (临时)
    /// </summary>
    /// <param name="isEnable"></param>
    public static void UnlockBunkerResearch(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 21895, isEnable ? 1 : 0);                // 886070202
    }

    /// <summary>
    /// 移除摩托帮进货延迟
    /// </summary>
    /// <param name="isEnable"></param>
    public static void MCSupplyDelay(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 19209, isEnable ? 0 : 600);          // 728170457  joaat("WEBSITE_PEGASSI_FAGGIO_SPORT")
    }

    /// <summary>
    /// 设置梅利威瑟服务类型
    /// </summary>
    /// <param name="serverId"></param>
    public static void MerryWeatherServices(int serverId)
    {
        Globals.Set_Global_Value(Base.oVMYCar + serverId, 1);
    }

    /// <summary>
    /// 移除进出口大亨出货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void ExportVehicleDelay(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 19892, isEnable ? 0 : 1200000);        // 1001423248  tuneables_processing.c
        Globals.Set_Global_Value(Base.Default + 19893, isEnable ? 0 : 1680000);        // 240134765
        Globals.Set_Global_Value(Base.Default + 19894, isEnable ? 0 : 2340000);        // 1915379148
        Globals.Set_Global_Value(Base.Default + 19895, isEnable ? 0 : 2880000);        // -824005590
    }

    /// <summary>
    /// 断开战局连接
    /// </summary>
    public static async void Disconnect()
    {
        Globals.Set_Global_Value(33106, 1);              // NETWORK::NETWORK_BAIL(1, 0, 0)
        await Task.Delay(200);
        Globals.Set_Global_Value(33106, 0);              // return NETWORK::NETWORK_CAN_ACCESS_MULTIPLAYER
    }

    /// <summary>
    /// 结束过场动画
    /// </summary>
    public static async void StopCutscene()
    {
        Globals.Set_Global_Value(2710132 + 3, 1);        // return MISC::GET_HASH_KEY("AVISA")
        Globals.Set_Global_Value(1575079, 1);            // NETWORK::NETWORK_TRANSITION_ADD_STAGE(hashKey, 1, num, etsParam0, 0);
        await Task.Delay(1000);
        Globals.Set_Global_Value(2710132 + 3, 0);
        Globals.Set_Global_Value(1575079, 0);
    }

    /// <summary>
    /// 移除机库进货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SmugglerRunInDelay(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 22961, isEnable ? 0 : 120000);         // 1278611667  tuneables_processing.c
        Globals.Set_Global_Value(Base.Default + 22962, isEnable ? 0 : 180000);         // -1424847540
        Globals.Set_Global_Value(Base.Default + 22963, isEnable ? 0 : 240000);         // -1817541754
        Globals.Set_Global_Value(Base.Default + 22964, isEnable ? 0 : 60000);          // 1722502526
    }

    /// <summary>
    /// 移除机库出货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SmugglerRunOutDelay(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 23002, isEnable ? 0 : 180000);         // -1525481945  tuneables_processing.c
    }

    /// <summary>
    /// 移除夜总会出货CD
    /// </summary>
    /// <param name="isEnable"></param>
    public static void NightclubOutDelay(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 24659, isEnable ? 0 : 300000);         // 1763921019  tuneables_processing.c
        Globals.Set_Global_Value(Base.Default + 24701, isEnable ? 0 : 300000);         // -1004589438  Global_262145.f_24671 = 300000;
        Globals.Set_Global_Value(Base.Default + 24702, isEnable ? 0 : 300000);         // 464940095
    }

    /// <summary>
    /// 移除CEO工作冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void CEOWorkCooldown(bool isEnable)
    {
        // CEO工作
        Globals.Set_Global_Value(Base.Default + 13281, isEnable ? 0 : 300000);       // -1404265088 joaat("GB_COOLDOWN_UNTIL_NEXT_BOSS_WORK")
        // 观光客
        Globals.Set_Global_Value(Base.Default + 13178, isEnable ? 0 : 600000);       // -1911318106 joaat("GB_SIGHTSEER_COOLDOWN")
    }

    /// <summary>
    /// 移除恐霸客户差事冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void ClientJobCooldown(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 24848 + 0, isEnable ? 0 : 300000);       // Between Jobs           -926426916
        Globals.Set_Global_Value(Base.Default + 24848 + 1, isEnable ? 0 : 1800000);      // Robbery in Progress    1733390598
        Globals.Set_Global_Value(Base.Default + 24848 + 2, isEnable ? 0 : 1800000);      // Data Sweep             724724668
        Globals.Set_Global_Value(Base.Default + 24848 + 3, isEnable ? 0 : 1800000);      // Targeted Data          846317886
        Globals.Set_Global_Value(Base.Default + 24848 + 4, isEnable ? 0 : 1800000);      // Diamond Shopping       443623246
    }

    /// <summary>
    /// 移除事务所安保合约冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SecurityHitCooldown(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 32024, isEnable ? 0 : 300000);           // -1462622971 joaat("FIXER_SECURITY_CONTRACT_COOLDOWN_TIME")
    }

    /// <summary>
    /// 移除公共电话任务合约冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void PayphoneHitCooldown(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 32105, isEnable ? 0 : 1200000);          // 1872071131
    }

    /// <summary>
    /// 进入RC匪徒
    /// </summary>
    /// <param name="isEnable"></param>
    public static void TriggerRCBandito(bool isEnable)
    {
        Globals.Set_Global_Value(Base.oVMYCar + 6918, isEnable ? 1 : 0);                 // if (PED::IS_PED_IN_ANY_VEHICLE(PLAYER::PLAYER_PED_ID(), true) || PED::IS_PED_GETTING_INTO_A_VEHICLE(PLAYER::PLAYER_PED_ID()))
    }

    /// <summary>
    /// 进入迷你坦克
    /// </summary>
    /// <param name="isEnable"></param>
    public static void TriggerMiniTank(bool isEnable)
    {
        Globals.Set_Global_Value(Base.oVMYCar + 6919, isEnable ? 1 : 0);
    }

    /// <summary>
    /// 请求虎鲸
    /// </summary>
    /// <param name="isEnable"></param>
    public static async void RequestKosatka()
    {
        Globals.Set_Global_Value(Base.oNETTimeHelp + 63 + 10, 17);
        await Task.Delay(100);
        Globals.Set_Global_Value(Base.oVMYCar + 960, 1);
    }

    /// <summary>
    /// 虎鲸导弹冷却
    /// </summary>
    /// <param name="isEnable"></param>
    public static void KosatkaMissleCooldown(bool isEnable)
    {
        Globals.Set_Global_Value(Base.Default + 30464, isEnable ? 0 : 60000); // joaat("IH_SUBMARINE_MISSILES_COOLDOWN")
    }

    /// <summary>
    /// 即时完成fm_mission_controller
    /// </summary>
    /// <param name="isEnable"></param>
    public static void instant_fm_mission_controller() // 公寓抢劫 | 末日豪劫 | 名钻赌场豪劫
    {
        if (Locals.LocalAddress("fm_mission_controller") != 0)
        {
            if (Locals.ReadLocalAddress<int>("fm_mission_controller", 0x6510/8) != 0)
            {
                Locals.WriteLocalAddress<int>("fm_mission_controller", 0x3DDC0/8, 264666);
                Locals.WriteLocalAddress<int>("fm_mission_controller", 0x26880/8, 12);
            }
        }
    }

    /// <summary>
    /// 即时完成fm_mission_controller_2020
    /// </summary>
    /// <param name="isEnable"></param>
    public static void instant_fm_mission_controller_2020() // 改装铺合约 | 佩里科岛 | 德瑞
    {
        if (Locals.LocalAddress("fm_mission_controller_2020") != 0)
        {
            if (Locals.ReadLocalAddress<int>("fm_mission_controller_2020", 0x24FF8/8) != 0)
            {
                Locals.WriteLocalAddress<int>("fm_mission_controller_2020", 0x3DDC0/8, 264666);
                Locals.WriteLocalAddress<int>("fm_mission_controller_2020", 0x62338/8, 9);
            }
        }
    }

    /// <summary>
    /// 单人启动某个任务
    /// </summary>
    /// <param name="isEnable"></param>
    public static void alone_launch_heist(string name) // 一个不够聪明的p2c skid menu尝试反转Alice.lua的偏移量 lol~
    {
        // public by Alice2333
        // post by https://www.unknowncheats.me/forum/4004717-post4751.html
        // 某个p2c skid menu我相信你也在看
        if (name == "fleeca") // 全福银行
        {
            Globals.Set_Global_Value(864505, 1);
        }
        if (name == "act3") // 末日将至
        {
            Globals.Set_Global_Value(803718, 1);
        }
        if (name == "aggressive") // 名钻赌场豪劫-气势汹汹
        {
            Globals.Set_Global_Value(864238, 1);
        }
        Globals.Set_Global_Value(4721848, 1);
        Globals.Set_Global_Value(4895268, 0);
        Globals.Set_Global_Value(4721844, 1);
        Globals.Set_Global_Value(4721845, 1);
    }
}
