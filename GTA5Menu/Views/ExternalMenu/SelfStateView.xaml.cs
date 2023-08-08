using GTA5Menu.Models;

using GTA5HotKey;
using GTA5Core.Native;
using GTA5Core.Offsets;
using GTA5Core.Features;
using GTA5Core.GTA.Rage;
using GTA5Core.GTA.Enum;
using GTA5Shared.Helper;

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

    private class Options
    {
        public bool GodMode = false;
        public bool AntiAFK = false;
        public bool NoRagdoll = false;

        public bool UndeadOffRadar = false;
        public bool NPCIgnore = false;
        public bool PoliceIgnore = false;

        public bool NoCollision = false;

        public bool ClearWanted = false;
        public bool KillNPC = false;
        public bool KillHostilityNPC = false;
        public bool KillPolice = false;

        public bool ProofBullet = false;
        public bool ProofFire = false;
        public bool ProofCollision = false;
        public bool ProofMelee = false;
        public bool ProofExplosion = false;
        public bool ProofSteam = false;
        public bool ProofDrown = false;
        public bool ProofWater = false;
    }
    private readonly Options _options = new();

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
        SelfStateModel.IsHotKeyToWaypoint = IniHelper.ReadValue("ExternalMenu", "IsHotKeyToWaypoint").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateModel.IsHotKeyToObjective = IniHelper.ReadValue("ExternalMenu", "IsHotKeyToObjective").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateModel.IsHotKeyFillHealthArmor = IniHelper.ReadValue("ExternalMenu", "IsHotKeyFillHealthArmor").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateModel.IsHotKeyClearWanted = IniHelper.ReadValue("ExternalMenu", "IsHotKeyClearWanted").Equals("True", StringComparison.OrdinalIgnoreCase);

        SelfStateModel.IsHotKeyFillAllAmmo = IniHelper.ReadValue("ExternalMenu", "IsHotKeyFillAllAmmo").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateModel.IsHotKeyMovingFoward = IniHelper.ReadValue("ExternalMenu", "IsHotKeyMovingFoward").Equals("True", StringComparison.OrdinalIgnoreCase);

        SelfStateModel.IsHotKeyNoCollision = IniHelper.ReadValue("ExternalMenu", "IsHotKeyNoCollision").Equals("True", StringComparison.OrdinalIgnoreCase);
        SelfStateModel.IsHotKeyToCrossHair = IniHelper.ReadValue("ExternalMenu", "IsHotKeyToCrossHair").Equals("True", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 保存配置文件
    /// </summary>
    private void SaveConfig()
    {
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyToWaypoint", $"{SelfStateModel.IsHotKeyToWaypoint}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyToObjective", $"{SelfStateModel.IsHotKeyToObjective}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyFillHealthArmor", $"{SelfStateModel.IsHotKeyFillHealthArmor}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyClearWanted", $"{SelfStateModel.IsHotKeyClearWanted}");

        IniHelper.WriteValue("ExternalMenu", "IsHotKeyFillAllAmmo", $"{SelfStateModel.IsHotKeyFillAllAmmo}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyMovingFoward", $"{SelfStateModel.IsHotKeyMovingFoward}");

        IniHelper.WriteValue("ExternalMenu", "IsHotKeyNoCollision", $"{SelfStateModel.IsHotKeyNoCollision}");
        IniHelper.WriteValue("ExternalMenu", "IsHotKeyToCrossHair", $"{SelfStateModel.IsHotKeyToCrossHair}");
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
                    _options.NoCollision = !_options.NoCollision;
                    Player.NoCollision(_options.NoCollision);

                    if (_options.NoCollision)
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
        if (_options.GodMode)
            Player.GodMode(true);
        // 挂机防踢
        if (_options.AntiAFK)
            Online.AntiAFK(true);
        // 无布娃娃
        if (_options.NoRagdoll)
            Player.NoRagdoll(true);
        // 雷达影踪（假死）
        if (_options.UndeadOffRadar)
            Player.UndeadOffRadar(true);
        // 玩家无碰撞体积
        if (_options.NoCollision)
            Player.NoCollision(true);

        // NPC无视、警察无视
        if (_options.NPCIgnore == true && _options.PoliceIgnore == false)
        {
            Player.NPCIgnore(0x040000);
        }
        else if (_options.NPCIgnore == false && _options.PoliceIgnore == true)
        {
            Player.NPCIgnore(0xC30000);
        }
        else if (_options.NPCIgnore == true && _options.PoliceIgnore == true)
        {
            Player.NPCIgnore(0xC70000);
        }

        ///////////////////////////////////////////////////

        // 防子弹（防止子弹掉血）
        if (_options.ProofBullet)
            Player.ProofBullet(true);
        // 防火烧（防止燃烧掉血）
        if (_options.ProofFire)
            Player.ProofFire(true);
        // 防撞击（防止撞击掉血）
        if (_options.ProofCollision)
            Player.ProofCollision(true);
        // 防近战（防止近战掉血）
        if (_options.ProofMelee)
            Player.ProofMelee(true);

        // 防爆炸（防止爆炸掉血）
        if (_options.ProofExplosion)
            Player.ProofExplosion(true);
        // 防蒸汽（具体场景未知）
        if (_options.ProofSteam)
            Player.ProofSteam(true);
        // 防溺水（具体场景未知）
        if (_options.ProofDrown)
            Player.ProofDrown(true);
        // 防海水（可以水下行走）
        if (_options.ProofWater)
            Player.ProofWater(true);
    }

    private void GTA5MenuWindow_LoopSpeedFastEvent()
    {
        // 自动消星
        if (_options.ClearWanted)
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
            if (_options.KillNPC)
                Memory.Write(pCPed + CPed.Health, 0.0f);

            // 自动击杀敌对NPC
            if (_options.KillHostilityNPC)
            {
                var oHostility = Memory.Read<byte>(pCPed + CPed.Hostility);
                if (oHostility > 0x01)
                {
                    Memory.Write(pCPed + CPed.Health, 0.0f);
                }
            }

            // 自动击杀警察
            if (_options.KillPolice)
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
        _options.GodMode = CheckBox_PlayerGodMode.IsChecked == true;
        Player.GodMode(_options.GodMode);
    }

    private void CheckBox_AntiAFK_Click(object sender, RoutedEventArgs e)
    {
        _options.AntiAFK = CheckBox_AntiAFK.IsChecked == true;
        Online.AntiAFK(_options.AntiAFK);
    }

    private void CheckBox_NoRagdoll_Click(object sender, RoutedEventArgs e)
    {
        _options.NoRagdoll = CheckBox_NoRagdoll.IsChecked == true;
        Player.NoRagdoll(_options.NoRagdoll);
    }

    private void CheckBox_UndeadOffRadar_Click(object sender, RoutedEventArgs e)
    {
        _options.UndeadOffRadar = CheckBox_UndeadOffRadar.IsChecked == true;
        Player.UndeadOffRadar(_options.UndeadOffRadar);
    }

    private void CheckBox_NPCIgnore_Click(object sender, RoutedEventArgs e)
    {
        _options.NPCIgnore = CheckBox_NPCIgnore.IsChecked == true;
        _options.PoliceIgnore = CheckBox_PoliceIgnore.IsChecked == true;

        if (_options.NPCIgnore == true && _options.PoliceIgnore == false)
        {
            Player.NPCIgnore(0x040000);
            return;
        }

        if (_options.NPCIgnore == false && _options.PoliceIgnore == true)
        {
            Player.NPCIgnore(0xC30000);
            return;
        }

        if (_options.NPCIgnore == true && _options.PoliceIgnore == true)
        {
            Player.NPCIgnore(0xC70000);
            return;
        }

        Player.NPCIgnore(0x00);
    }

    private void CheckBox_AutoClearWanted_Click(object sender, RoutedEventArgs e)
    {
        _options.ClearWanted = CheckBox_AutoClearWanted.IsChecked == true;
        Player.WantedLevel((byte)WantedLevel.Level0);
    }

    private void CheckBox_AutoKillNPC_Click(object sender, RoutedEventArgs e)
    {
        _options.KillNPC = CheckBox_AutoKillNPC.IsChecked == true;
        World.KillNPC(false);
    }

    private void CheckBox_AutoKillHostilityNPC_Click(object sender, RoutedEventArgs e)
    {
        _options.KillHostilityNPC = CheckBox_AutoKillHostilityNPC.IsChecked == true;
        World.KillNPC(true);
    }

    private void CheckBox_AutoKillPolice_Click(object sender, RoutedEventArgs e)
    {
        _options.KillPolice = CheckBox_AutoKillPolice.IsChecked == true;
        World.KillPolice();
    }

    private void CheckBox_NoCollision_Click(object sender, RoutedEventArgs e)
    {
        _options.NoCollision = SelfStateModel.IsHotKeyNoCollision;
        Player.NoCollision(_options.NoCollision);
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
        _options.ProofBullet = CheckBox_ProofBullet.IsChecked == true;
        Player.ProofBullet(_options.ProofBullet);
    }

    private void CheckBox_ProofFire_Click(object sender, RoutedEventArgs e)
    {
        _options.ProofFire = CheckBox_ProofFire.IsChecked == true;
        Player.ProofFire(_options.ProofFire);
    }

    private void CheckBox_ProofCollision_Click(object sender, RoutedEventArgs e)
    {
        _options.ProofCollision = CheckBox_ProofCollision.IsChecked == true;
        Player.ProofCollision(_options.ProofCollision);
    }

    private void CheckBox_ProofMelee_Click(object sender, RoutedEventArgs e)
    {
        _options.ProofMelee = CheckBox_ProofMelee.IsChecked == true;
        Player.ProofMelee(_options.ProofMelee);
    }

    private void CheckBox_ProofExplosion_Click(object sender, RoutedEventArgs e)
    {
        _options.ProofExplosion = CheckBox_ProofExplosion.IsChecked == true;
        Player.ProofExplosion(_options.ProofExplosion);
    }

    private void CheckBox_ProofSteam_Click(object sender, RoutedEventArgs e)
    {
        _options.ProofSteam = CheckBox_ProofSteam.IsChecked == true;
        Player.ProofSteam(_options.ProofSteam);
    }

    private void CheckBox_ProofDrown_Click(object sender, RoutedEventArgs e)
    {
        _options.ProofDrown = CheckBox_ProofDrown.IsChecked == true;
        Player.ProofDrown(_options.ProofDrown);
    }

    private void CheckBox_ProofWater_Click(object sender, RoutedEventArgs e)
    {
        _options.ProofWater = CheckBox_ProofWater.IsChecked == true;
        Player.ProofWater(_options.ProofWater);
    }
}
