using GTA5Menu.Models;
using GTA5HotKey;
using GTA5Core.Native;
using GTA5Core.Offsets;
using GTA5Core.Features;
using GTA5Core.GTA.Rage;
using GTA5Core.GTA.Enum;
using GTA5Shared.Helper;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5Menu.Views.ExternalMenu;

/// <summary>
/// SelfStateView.xaml 的交互逻辑
/// </summary>
public partial class SelfStateView : UserControl
{
    /// <summary>
    /// 数据模型绑定
    /// </summary>
    public SelfStateModel SelfStateModel { get; set; } = new();

    public partial class Options : ObservableObject
    {
        [ObservableProperty] public bool godMode = false;
        [ObservableProperty] public bool antiAFK = false;
        [ObservableProperty] public bool noRagdoll = false;

        [ObservableProperty] public bool undeadOffRadar = false;
        [ObservableProperty] public bool nPCIgnore = false;
        [ObservableProperty] public bool policeIgnore = false;

        [ObservableProperty] public bool noCollision = false;

        [ObservableProperty] public bool clearWanted = false;
        [ObservableProperty] public bool killNPC = false;
        [ObservableProperty] public bool killHostilityNPC = false;
        [ObservableProperty] public bool killPolice = false;

        [ObservableProperty] public bool proofBullet = false;
        [ObservableProperty] public bool proofFire = false;
        [ObservableProperty] public bool proofCollision = false;
        [ObservableProperty] public bool proofMelee = false;
        [ObservableProperty] public bool proofExplosion = false;
        [ObservableProperty] public bool proofSteam = false;
        [ObservableProperty] public bool proofDrown = false;
        [ObservableProperty] public bool proofWater = false;
    }
    public Options SelfStateOption { get; set; } = new();

    /////////////////////////////////////////////////////////

    /// <summary>
    /// 坐标微调距离
    /// </summary>
    private float _moveDistance = 1.5f;

    /////////////////////////////////////////////////////////

    public SelfStateView()
    {
        InitializeComponent();
        GTA5MenuWindow.WindowClosingEvent += GTA5MenuWindow_WindowClosingEvent;
        GTA5MenuWindow.LoopSpeedNormalEvent += GTA5MenuWindow_LoopSpeedNormalEvent;
        GTA5MenuWindow.LoopSpeedFastEvent += GTA5MenuWindow_LoopSpeedFastEvent;

        // 添加快捷键
        HotKeys.AddKey(Keys.F3);
        HotKeys.AddKey(Keys.F4);
        HotKeys.AddKey(Keys.F5);
        HotKeys.AddKey(Keys.F6);
        HotKeys.AddKey(Keys.F7);
        HotKeys.AddKey(Keys.F8);
        HotKeys.AddKey(Keys.D0);
        HotKeys.AddKey(Keys.Oemplus);
        // 订阅按钮事件
        HotKeys.KeyDownEvent += HotKeys_KeyDownEvent;

        ///////////  读取配置文件  ///////////

        ReadConfig();
    }

    private void GTA5MenuWindow_WindowClosingEvent()
    {
        // 移除快捷键
        HotKeys.RemoveKey(Keys.F3);
        HotKeys.RemoveKey(Keys.F4);
        HotKeys.RemoveKey(Keys.F5);
        HotKeys.RemoveKey(Keys.F6);
        HotKeys.RemoveKey(Keys.F7);
        HotKeys.RemoveKey(Keys.F8);
        HotKeys.RemoveKey(Keys.D0);
        HotKeys.RemoveKey(Keys.Oemplus);
        // 取消订阅按钮事件
        HotKeys.KeyDownEvent -= HotKeys_KeyDownEvent;

        // 保存配置文件
        SaveConfig();

        GTA5MenuWindow.WindowClosingEvent -= GTA5MenuWindow_WindowClosingEvent;
        GTA5MenuWindow.LoopSpeedNormalEvent -= GTA5MenuWindow_LoopSpeedNormalEvent;
        GTA5MenuWindow.LoopSpeedFastEvent -= GTA5MenuWindow_LoopSpeedFastEvent;
    }

    /////////////////////////////////////////////////

    /// <summary>
    /// 读取配置文件
    /// </summary>
    private void ReadConfig()
    {
        // 一般选项
        SelfStateOption.GodMode = IniHelper.ReadValue("ExternalMenu", "GodMode").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.AntiAFK = IniHelper.ReadValue("ExternalMenu", "AntiAFK").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.NoRagdoll = IniHelper.ReadValue("ExternalMenu", "NoRagdoll").Equals("True", StringComparison.OrdinalIgnoreCase);

        SelfStateOption.UndeadOffRadar = IniHelper.ReadValue("ExternalMenu", "UndeadOffRadar").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.NPCIgnore = IniHelper.ReadValue("ExternalMenu", "NPCIgnore").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.PoliceIgnore = IniHelper.ReadValue("ExternalMenu", "PoliceIgnore").Equals("True", StringComparison.OrdinalIgnoreCase);

        // 快捷键
        SelfStateModel.IsHotKeyToWaypoint = IniHelper.ReadValue("ExternalMenu", "IsHotKeyToWaypoint").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateModel.IsHotKeyToObjective = IniHelper.ReadValue("ExternalMenu", "IsHotKeyToObjective").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateModel.IsHotKeyFillHealthArmor = IniHelper.ReadValue("ExternalMenu", "IsHotKeyFillHealthArmor").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateModel.IsHotKeyClearWanted = IniHelper.ReadValue("ExternalMenu", "IsHotKeyClearWanted").Equals("True", StringComparison.OrdinalIgnoreCase);

        SelfStateModel.IsHotKeyFillAllAmmo = IniHelper.ReadValue("ExternalMenu", "IsHotKeyFillAllAmmo").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateModel.IsHotKeyMovingFoward = IniHelper.ReadValue("ExternalMenu", "IsHotKeyMovingFoward").Equals("True", StringComparison.OrdinalIgnoreCase);

        SelfStateModel.IsHotKeyNoCollision = IniHelper.ReadValue("ExternalMenu", "IsHotKeyNoCollision").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateModel.IsHotKeyToCrossHair = IniHelper.ReadValue("ExternalMenu", "IsHotKeyToCrossHair").Equals("True", StringComparison.OrdinalIgnoreCase);

        //实用功能
        SelfStateOption.ClearWanted = IniHelper.ReadValue("ExternalMenu", "ClearWanted").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.KillNPC = IniHelper.ReadValue("ExternalMenu", "KillNPC").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.KillHostilityNPC = IniHelper.ReadValue("ExternalMenu", "KillHostilityNPC").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.KillPolice = IniHelper.ReadValue("ExternalMenu", "KillPolice").Equals("True", StringComparison.OrdinalIgnoreCase);

        //高级
        SelfStateOption.ProofBullet = IniHelper.ReadValue("ExternalMenu", "ProofBullet").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.ProofFire = IniHelper.ReadValue("ExternalMenu", "ProofFire").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.ProofCollision = IniHelper.ReadValue("ExternalMenu", "ProofCollision").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.ProofMelee = IniHelper.ReadValue("ExternalMenu", "ProofMelee").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.ProofExplosion = IniHelper.ReadValue("ExternalMenu", "ProofExplosion").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.ProofSteam = IniHelper.ReadValue("ExternalMenu", "ProofSteam").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.ProofDrown = IniHelper.ReadValue("ExternalMenu", "ProofDrown").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateOption.ProofWater = IniHelper.ReadValue("ExternalMenu", "ProofWater").Equals("True", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 保存配置文件
    /// </summary>
    private void SaveConfig()
    {
        //一般选项
        IniHelper.WriteValue("ExternalMenu", "GodMode", $"{SelfStateOption.GodMode}");
        IniHelper.WriteValue("ExternalMenu", "AntiAFK", $"{SelfStateOption.AntiAFK}");
        IniHelper.WriteValue("ExternalMenu", "NoRagdoll", $"{SelfStateOption.NoRagdoll}");

        IniHelper.WriteValue("ExternalMenu", "UndeadOffRadar", $"{SelfStateOption.UndeadOffRadar}");
        IniHelper.WriteValue("ExternalMenu", "NPCIgnore", $"{SelfStateOption.NPCIgnore}");
        IniHelper.WriteValue("ExternalMenu", "PoliceIgnore", $"{SelfStateOption.PoliceIgnore}");

        //快捷键
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyToWaypoint", $"{SelfStateModel.IsHotKeyToWaypoint}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyToObjective", $"{SelfStateModel.IsHotKeyToObjective}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyFillHealthArmor", $"{SelfStateModel.IsHotKeyFillHealthArmor}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyClearWanted", $"{SelfStateModel.IsHotKeyClearWanted}");

        IniHelper.WriteValue("ExternalMenu", "IsHotKeyFillAllAmmo", $"{SelfStateModel.IsHotKeyFillAllAmmo}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyMovingFoward", $"{SelfStateModel.IsHotKeyMovingFoward}");

        IniHelper.WriteValue("ExternalMenu", "IsHotKeyNoCollision", $"{SelfStateModel.IsHotKeyNoCollision}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyToCrossHair", $"{SelfStateModel.IsHotKeyToCrossHair}");

        //实用功能
        IniHelper.WriteValue("ExternalMenu", "ClearWanted", $"{SelfStateOption.ClearWanted}");
        IniHelper.WriteValue("ExternalMenu", "KillNPC", $"{SelfStateOption.KillNPC}");
        IniHelper.WriteValue("ExternalMenu", "KillHostilityNPC", $"{SelfStateOption.KillHostilityNPC}");
        IniHelper.WriteValue("ExternalMenu", "KillPolice", $"{SelfStateOption.KillPolice}");

        //高级
        IniHelper.WriteValue("ExternalMenu", "ProofBullet", $"{SelfStateOption.ProofBullet}");
        IniHelper.WriteValue("ExternalMenu", "ProofFire", $"{SelfStateOption.ProofFire}");
        IniHelper.WriteValue("ExternalMenu", "ProofCollision", $"{SelfStateOption.ProofCollision}");
        IniHelper.WriteValue("ExternalMenu", "ProofMelee", $"{SelfStateOption.ProofMelee}");
        IniHelper.WriteValue("ExternalMenu", "ProofExplosion", $"{SelfStateOption.ProofExplosion}");
        IniHelper.WriteValue("ExternalMenu", "ProofSteam", $"{SelfStateOption.ProofSteam}");
        IniHelper.WriteValue("ExternalMenu", "ProofDrown", $"{SelfStateOption.ProofDrown}");
        IniHelper.WriteValue("ExternalMenu", "ProofWater", $"{SelfStateOption.ProofWater}");
    }

    /// <summary>
    /// 全局热键 按键按下事件
    /// </summary>
    /// <param name="key"></param>
    private void HotKeys_KeyDownEvent(Keys key)
    {
        switch (key)
        {
            case Keys.F3:
                if (SelfStateModel.IsHotKeyFillAllAmmo)
                {
                    Weapon.FillAllAmmo();
                }
                break;
            case Keys.F4:
                if (SelfStateModel.IsHotKeyMovingFoward)
                {
                    Teleport.MoveFoward(_moveDistance);
                }
                break;
            case Keys.F5:
                if (SelfStateModel.IsHotKeyToWaypoint)
                {
                    Teleport.ToWaypoint();
                }
                break;
            case Keys.F6:
                if (SelfStateModel.IsHotKeyToObjective)
                {
                    Teleport.ToObjective();
                }
                break;
            case Keys.F7:
                if (SelfStateModel.IsHotKeyFillHealthArmor)
                {
                    Player.FillHealth();
                    Player.FillArmor();
                }
                break;
            case Keys.F8:
                if (SelfStateModel.IsHotKeyClearWanted)
                {
                    Player.WantedLevel((byte)WantedLevel.Level0);
                }
                break;
            case Keys.D0:
                if (SelfStateModel.IsHotKeyNoCollision)
                {
                    SelfStateOption.NoCollision = !SelfStateOption.NoCollision;
                    Player.NoCollision(SelfStateOption.NoCollision);

                    if (SelfStateOption.NoCollision)
                        Console.Beep(600, 75);
                    else
                        Console.Beep(500, 75);
                }
                break;
            case Keys.Oemplus:
                if (SelfStateModel.IsHotKeyToCrossHair)
                {
                    Teleport.ToCrossHair();
                }
                break;
        }
    }

    /////////////////////////////////////////////////

    private void GTA5MenuWindow_LoopSpeedNormalEvent()
    {
        var pCPed = Game.GetCPed();
        var pCPlayerInfo = Memory.Read<long>(pCPed + CPed.CPlayerInfo);

        var mHealth = Memory.Read<float>(pCPed + CPed.Health);
        var mHealthMax = Memory.Read<float>(pCPed + CPed.HealthMax);
        var mArmor = Memory.Read<float>(pCPed + CPed.Armor);

        var mWantedLevel = Memory.Read<byte>(pCPlayerInfo + CPlayerInfo.WantedLevel);
        var mWalkSpeed = Memory.Read<float>(pCPlayerInfo + CPlayerInfo.WalkSpeed);
        var mRunSpeed = Memory.Read<float>(pCPlayerInfo + CPlayerInfo.RunSpeed);
        var mSwimSpeed = Memory.Read<float>(pCPlayerInfo + CPlayerInfo.SwimSpeed);

        ///////////////////////////////////////////////////

        this.Dispatcher.BeginInvoke(() =>
        {
            if ((float)Slider_Health.Value != mHealth)
                Slider_Health.Value = mHealth;

            if ((float)Slider_HealthMax.Value != mHealthMax)
                Slider_HealthMax.Value = mHealthMax;

            if ((float)Slider_Armor.Value != mArmor)
                Slider_Armor.Value = mArmor;

            if ((float)Slider_WantedLevel.Value != mWantedLevel)
                Slider_WantedLevel.Value = mWantedLevel;

            if ((float)Slider_RunSpeed.Value != mRunSpeed)
                Slider_RunSpeed.Value = mRunSpeed;

            if ((float)Slider_SwimSpeed.Value != mSwimSpeed)
                Slider_SwimSpeed.Value = mSwimSpeed;

            if ((float)Slider_WalkSpeed.Value != mWalkSpeed)
                Slider_WalkSpeed.Value = mWalkSpeed;
        });

        ///////////////////////////////////////////////////

        // 玩家无敌
        if (SelfStateOption.GodMode)
            Player.GodMode(true);
        // 挂机防踢
        if (SelfStateOption.AntiAFK)
            Online.AntiAFK(true);
        // 无布娃娃
        if (SelfStateOption.NoRagdoll)
            Player.NoRagdoll(true);
        // 雷达影踪（假死）
        if (SelfStateOption.UndeadOffRadar)
            Player.UndeadOffRadar(true);
        // 玩家无碰撞体积
        if (SelfStateOption.NoCollision)
            Player.NoCollision(true);

        // NPC无视、警察无视
        if (SelfStateOption.NPCIgnore == true && SelfStateOption.PoliceIgnore == false)
        {
            Player.NPCIgnore(0x040000);
        }
        else if (SelfStateOption.NPCIgnore == false && SelfStateOption.PoliceIgnore == true)
        {
            Player.NPCIgnore(0xC30000);
        }
        else if (SelfStateOption.NPCIgnore == true && SelfStateOption.PoliceIgnore == true)
        {
            Player.NPCIgnore(0xC70000);
        }

        ///////////////////////////////////////////////////

        // 防子弹（防止子弹掉血）
        if (SelfStateOption.ProofBullet)
            Player.ProofBullet(true);
        // 防火烧（防止燃烧掉血）
        if (SelfStateOption.ProofFire)
            Player.ProofFire(true);
        // 防撞击（防止撞击掉血）
        if (SelfStateOption.ProofCollision)
            Player.ProofCollision(true);
        // 防近战（防止近战掉血）
        if (SelfStateOption.ProofMelee)
            Player.ProofMelee(true);

        // 防爆炸（防止爆炸掉血）
        if (SelfStateOption.ProofExplosion)
            Player.ProofExplosion(true);
        // 防蒸汽（具体场景未知）
        if (SelfStateOption.ProofSteam)
            Player.ProofSteam(true);
        // 防溺水（具体场景未知）
        if (SelfStateOption.ProofDrown)
            Player.ProofDrown(true);
        // 防海水（可以水下行走）
        if (SelfStateOption.ProofWater)
            Player.ProofWater(true);
    }

    private void GTA5MenuWindow_LoopSpeedFastEvent()
    {
        // 自动消星
        if (SelfStateOption.ClearWanted)
            Player.WantedLevel((byte)WantedLevel.Level0);

        // Ped
        var pCPedInterface = Game.GetCPedInterface();
        var pCPedList = Memory.Read<long>(pCPedInterface + CPedInterface.CPedList);
        var mCurPeds = Memory.Read<int>(pCPedInterface + CPedInterface.CurPeds);

        for (var i = 0; i < mCurPeds; i++)
        {
            var pCPed = Memory.Read<long>(pCPedList + i * 0x10);
            if (!Memory.IsValid(pCPed))
                continue;

            // 跳过玩家
            var pCPlayerInfo = Memory.Read<long>(pCPed + CPed.CPlayerInfo);
            if (Memory.IsValid(pCPlayerInfo))
                continue;

            // 自动击杀NPC
            if (SelfStateOption.KillNPC)
                Memory.Write(pCPed + CPed.Health, 0.0f);

            // 自动击杀敌对NPC
            if (SelfStateOption.KillHostilityNPC)
            {
                var oHostility = Memory.Read<byte>(pCPed + CPed.Hostility);
                if (oHostility > 0x01)
                {
                    Memory.Write(pCPed + CPed.Health, 0.0f);
                }
            }

            // 自动击杀警察
            if (SelfStateOption.KillPolice)
            {
                var ped_type = Memory.Read<int>(pCPed + CPed.Ragdoll);
                ped_type = ped_type << 11 >> 25;

                if (ped_type == (int)PedType.COP ||
                    ped_type == (int)PedType.SWAT ||
                    ped_type == (int)PedType.ARMY)
                {
                    Memory.Write(pCPed + CPed.Health, 0.0f);
                }
            }
        }
    }

    /////////////////////////////////////////////////

    private void Slider_Health_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.Health((float)Slider_Health.Value);
    }

    private void Slider_HealthMax_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.HealthMax((float)Slider_HealthMax.Value);
    }

    private void Slider_Armor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.Armor((float)Slider_Armor.Value);
    }

    private void Slider_WantedLevel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.WantedLevel((byte)Slider_WantedLevel.Value);
    }

    private void Slider_RunSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.RunSpeed((float)Slider_RunSpeed.Value);
    }

    private void Slider_SwimSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.SwimSpeed((float)Slider_SwimSpeed.Value);
    }

    private void Slider_WalkSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Player.WalkSpeed((float)Slider_WalkSpeed.Value);
    }

    private void Slider_MoveDistance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        _moveDistance = (float)Slider_MoveDistance.Value;
    }

    private void CheckBox_PlayerGodMode_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.GodMode = CheckBox_PlayerGodMode.IsChecked == true;
        Player.GodMode(SelfStateOption.GodMode);
    }

    private void CheckBox_AntiAFK_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.AntiAFK = CheckBox_AntiAFK.IsChecked == true;
        Online.AntiAFK(SelfStateOption.AntiAFK);
    }

    private void CheckBox_NoRagdoll_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.NoRagdoll = CheckBox_NoRagdoll.IsChecked == true;
        Player.NoRagdoll(SelfStateOption.NoRagdoll);
    }

    private void CheckBox_UndeadOffRadar_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.UndeadOffRadar = CheckBox_UndeadOffRadar.IsChecked == true;
        Player.UndeadOffRadar(SelfStateOption.UndeadOffRadar);
    }

    private void CheckBox_NPCIgnore_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.NPCIgnore = CheckBox_NPCIgnore.IsChecked == true;
        SelfStateOption.PoliceIgnore = CheckBox_PoliceIgnore.IsChecked == true;

        if (SelfStateOption.NPCIgnore == true && SelfStateOption.PoliceIgnore == false)
        {
            Player.NPCIgnore(0x040000);
            return;
        }

        if (SelfStateOption.NPCIgnore == false && SelfStateOption.PoliceIgnore == true)
        {
            Player.NPCIgnore(0xC30000);
            return;
        }

        if (SelfStateOption.NPCIgnore == true && SelfStateOption.PoliceIgnore == true)
        {
            Player.NPCIgnore(0xC70000);
            return;
        }

        Player.NPCIgnore(0x00);
    }

    private void CheckBox_AutoClearWanted_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.ClearWanted = CheckBox_AutoClearWanted.IsChecked == true;
        Player.WantedLevel((byte)WantedLevel.Level0);
    }

    private void CheckBox_AutoKillNPC_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.KillNPC = CheckBox_AutoKillNPC.IsChecked == true;
        World.KillNPC(false);
    }

    private void CheckBox_AutoKillHostilityNPC_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.KillHostilityNPC = CheckBox_AutoKillHostilityNPC.IsChecked == true;
        World.KillNPC(true);
    }

    private void CheckBox_AutoKillPolice_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.KillPolice = CheckBox_AutoKillPolice.IsChecked == true;
        World.KillPolice();
    }

    private void CheckBox_NoCollision_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.NoCollision = SelfStateModel.IsHotKeyNoCollision;
        Player.NoCollision(SelfStateOption.NoCollision);
    }

    private void Button_FillHealth_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Player.FillHealth();
    }

    private void Button_FillArmor_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Player.FillArmor();
    }

    private void Button_ClearWanted_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Player.WantedLevel(0x00);
    }

    private void Button_Suicide_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Player.Suicide();
    }

    private void Button_ToWaypoint_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Teleport.ToWaypoint();
    }

    private void Button_ToObjective_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Teleport.ToObjective();
    }

    private void Button_ToCrossHair_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Teleport.ToCrossHair();
    }

    private void CheckBox_ProofBullet_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.ProofBullet = CheckBox_ProofBullet.IsChecked == true;
        Player.ProofBullet(SelfStateOption.ProofBullet);
    }

    private void CheckBox_ProofFire_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.ProofFire = CheckBox_ProofFire.IsChecked == true;
        Player.ProofFire(SelfStateOption.ProofFire);
    }

    private void CheckBox_ProofCollision_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.ProofCollision = CheckBox_ProofCollision.IsChecked == true;
        Player.ProofCollision(SelfStateOption.ProofCollision);
    }

    private void CheckBox_ProofMelee_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.ProofMelee = CheckBox_ProofMelee.IsChecked == true;
        Player.ProofMelee(SelfStateOption.ProofMelee);
    }

    private void CheckBox_ProofExplosion_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.ProofExplosion = CheckBox_ProofExplosion.IsChecked == true;
        Player.ProofExplosion(SelfStateOption.ProofExplosion);
    }

    private void CheckBox_ProofSteam_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.ProofSteam = CheckBox_ProofSteam.IsChecked == true;
        Player.ProofSteam(SelfStateOption.ProofSteam);
    }

    private void CheckBox_ProofDrown_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.ProofDrown = CheckBox_ProofDrown.IsChecked == true;
        Player.ProofDrown(SelfStateOption.ProofDrown);
    }

    private void CheckBox_ProofWater_Click(object sender, RoutedEventArgs e)
    {
        SelfStateOption.ProofWater = CheckBox_ProofWater.IsChecked == true;
        Player.ProofWater(SelfStateOption.ProofWater);
    }
}
