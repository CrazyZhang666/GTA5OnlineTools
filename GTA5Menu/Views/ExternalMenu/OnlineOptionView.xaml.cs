using CommunityToolkit.Mvvm.ComponentModel;
using GTA5Core.Features;
using GTA5Core.GTA.Onlines;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.ExternalMenu;

/// <summary>
/// OnlineOptionView.xaml 的交互逻辑
/// </summary>
public partial class OnlineOptionView : UserControl
{
    public partial class Options : ObservableObject
    {
        [ObservableProperty] public bool freeChangeAppearance = false;
        [ObservableProperty] public bool changeAppearanceCooldown = false;

        [ObservableProperty] public bool passiveModeCooldown = false;
        [ObservableProperty] public bool suicideCooldown = false;
        [ObservableProperty] public bool orbitalCooldown = false;
        [ObservableProperty] public bool sellOnNonPublic = false;
        [ObservableProperty] public bool sessionSnow = false;

        [ObservableProperty] public bool offRadar = false;
        [ObservableProperty] public bool ghostOrganization = false;
        [ObservableProperty] public bool bribeOrBlindCops = false;
        [ObservableProperty] public bool bribeAuthorities = false;
        [ObservableProperty] public bool revealPlayers = false;

    }
    public Options OP_Options { set; get; } = new();

    public OnlineOptionView()
    {
        InitializeComponent();
        GTA5MenuWindow.WindowClosingEvent += GTA5MenuWindow_WindowClosingEvent;
        GTA5MenuWindow.LoopSpeedNormalEvent += GTA5MenuWindow_LoopSpeedNormalEvent;

        ReadConfig();
    }

    private void GTA5MenuWindow_WindowClosingEvent()
    {
        GTA5MenuWindow.WindowClosingEvent -= GTA5MenuWindow_WindowClosingEvent;
        GTA5MenuWindow.LoopSpeedNormalEvent -= GTA5MenuWindow_LoopSpeedNormalEvent;

        SaveConfig();
    }

    private void SaveConfig()
    {
        IniHelper.WriteValue("OnlineOption", "PassiveModeCooldown", $"{OP_Options.PassiveModeCooldown}");
        IniHelper.WriteValue("OnlineOption", "SuicideCooldown", $"{OP_Options.SuicideCooldown}");
        IniHelper.WriteValue("OnlineOption", "OrbitalCooldown", $"{OP_Options.OrbitalCooldown}");
        IniHelper.WriteValue("OnlineOption", "SellOnNonPublic", $"{OP_Options.SellOnNonPublic}");
        IniHelper.WriteValue("OnlineOption", "SessionSnow", $"{OP_Options.SessionSnow}");

        IniHelper.WriteValue("OnlineOption", "OffRadar", $"{OP_Options.OffRadar}");
        IniHelper.WriteValue("OnlineOption", "GhostOrganization", $"{OP_Options.GhostOrganization}");
        IniHelper.WriteValue("OnlineOption", "BribeOrBlindCops", $"{OP_Options.BribeOrBlindCops}");
        IniHelper.WriteValue("OnlineOption", "BribeAuthorities", $"{OP_Options.BribeAuthorities}");
        IniHelper.WriteValue("OnlineOption", "RevealPlayers", $"{OP_Options.RevealPlayers}");
    }

    private void ReadConfig()
    {
        OP_Options.PassiveModeCooldown = IniHelper.ReadValue("OnlineOption", "PassiveModeCooldown").Equals("True", StringComparison.OrdinalIgnoreCase);
        OP_Options.SuicideCooldown = IniHelper.ReadValue("OnlineOption", "SuicideCooldown").Equals("True", StringComparison.OrdinalIgnoreCase);
        OP_Options.OrbitalCooldown = IniHelper.ReadValue("OnlineOption", "OrbitalCooldown").Equals("True", StringComparison.OrdinalIgnoreCase);
        OP_Options.SellOnNonPublic = IniHelper.ReadValue("OnlineOption", "SellOnNonPublic").Equals("True", StringComparison.OrdinalIgnoreCase);
        OP_Options.SessionSnow = IniHelper.ReadValue("OnlineOption", "SessionSnow").Equals("True", StringComparison.OrdinalIgnoreCase);

        OP_Options.OffRadar = IniHelper.ReadValue("OnlineOption", "OffRadar").Equals("True", StringComparison.OrdinalIgnoreCase);
        OP_Options.GhostOrganization = IniHelper.ReadValue("OnlineOption", "GhostOrganization").Equals("True", StringComparison.OrdinalIgnoreCase);
        OP_Options.BribeOrBlindCops = IniHelper.ReadValue("OnlineOption", "BribeOrBlindCops").Equals("True", StringComparison.OrdinalIgnoreCase);
        OP_Options.BribeAuthorities = IniHelper.ReadValue("OnlineOption", "BribeAuthorities").Equals("True", StringComparison.OrdinalIgnoreCase);
        OP_Options.RevealPlayers = IniHelper.ReadValue("OnlineOption", "RevealPlayers").Equals("True", StringComparison.OrdinalIgnoreCase);
    }


    private void GTA5MenuWindow_LoopSpeedNormalEvent()
    {
        // 免费更改角色外观
        if (OP_Options.FreeChangeAppearance)
            Online.FreeChangeAppearance(true);
        // 移除更改角色外观冷却
        if (OP_Options.ChangeAppearanceCooldown)
            Online.ChangeAppearanceCooldown(true);

        // 移除被动模式冷却
        if (OP_Options.PassiveModeCooldown)
            Online.PassiveModeCooldown(true);
        // 移除自杀冷却
        if (OP_Options.SuicideCooldown)
            Online.SuicideCooldown(true);
        // 移除天基炮冷却
        if (OP_Options.OrbitalCooldown)
            Online.OrbitalCooldown(true);
        // 非公开战局运货
        if (OP_Options.SellOnNonPublic)
            Online.SellOnNonPublic(true);
        // 战局雪天 (自己可见)
        if (OP_Options.SessionSnow)
            Online.SessionSnow(true);

        // 雷达影踪
        if (OP_Options.OffRadar)
            Online.OffRadar(true);
        // 幽灵组织
        if (OP_Options.GhostOrganization)
            Online.GhostOrganization(true);
        // 警察无视犯罪
        if (OP_Options.BribeOrBlindCops)
            Online.BribeOrBlindCops(true);
        // 贿赂当局
        if (OP_Options.BribeAuthorities)
            Online.BribeAuthorities(true);
        // 显示玩家
        if (OP_Options.RevealPlayers)
            Online.RevealPlayers(true);
    }

    /////////////////////////////////////////////////////////////

    private void Button_Sessions_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (sender is Button button)
        {
            var btnContent = button.Content.ToString();

            var session = OnlineData.Sessions.Find(t => t.Name == btnContent);
            if (session != null)
                Online.LoadSession(session.Value);
        }
    }

    private void Button_EmptySession_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Online.EmptySession();
    }

    private void Button_Disconnect_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Online.Disconnect();
    }

    private void Button_StopCutscene_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Online.StopCutscene();
    }

    private void CheckBox_FreeChangeAppearance_Click(object sender, RoutedEventArgs e)
    {
        OP_Options.FreeChangeAppearance = CheckBox_FreeChangeAppearance.IsChecked == true;
        Online.FreeChangeAppearance(OP_Options.FreeChangeAppearance);
    }

    private void CheckBox_ChangeAppearanceCooldown_Click(object sender, RoutedEventArgs e)
    {
        OP_Options.ChangeAppearanceCooldown = CheckBox_ChangeAppearanceCooldown.IsChecked == true;
        Online.ChangeAppearanceCooldown(OP_Options.ChangeAppearanceCooldown);
    }

    private void CheckBox_PassiveModeCooldown_Click(object sender, RoutedEventArgs e)
    {
        OP_Options.PassiveModeCooldown = CheckBox_PassiveModeCooldown.IsChecked == true;
        Online.PassiveModeCooldown(OP_Options.PassiveModeCooldown);
    }

    private void CheckBox_SuicideCooldown_Click(object sender, RoutedEventArgs e)
    {
        OP_Options.SuicideCooldown = CheckBox_SuicideCooldown.IsChecked == true;
        Online.SuicideCooldown(OP_Options.SuicideCooldown);
    }

    private void CheckBox_OrbitalCooldown_Click(object sender, RoutedEventArgs e)
    {
        OP_Options.OrbitalCooldown = CheckBox_OrbitalCooldown.IsChecked == true;
        Online.OrbitalCooldown(OP_Options.OrbitalCooldown);
    }

    private void CheckBox_SellOnNonPublic_Click(object sender, RoutedEventArgs e)
    {
        OP_Options.SellOnNonPublic = CheckBox_SellOnNonPublic.IsChecked == true;
        Online.SellOnNonPublic(OP_Options.SellOnNonPublic);
    }

    private void CheckBox_SessionSnow_Click(object sender, RoutedEventArgs e)
    {
        OP_Options.SessionSnow = CheckBox_SessionSnow.IsChecked == true;
        Online.SessionSnow(OP_Options.SessionSnow);
    }

    /////////////////////////////////////////////////////////////

    private void CheckBox_OffRadar_Click(object sender, RoutedEventArgs e)
    {
        OP_Options.OffRadar = CheckBox_OffRadar.IsChecked == true;
        Online.OffRadar(OP_Options.OffRadar);
    }

    private void CheckBox_GhostOrganization_Click(object sender, RoutedEventArgs e)
    {
        OP_Options.GhostOrganization = CheckBox_GhostOrganization.IsChecked == true;
        Online.GhostOrganization(OP_Options.GhostOrganization);
    }

    private void CheckBox_BribeOrBlindCops_Click(object sender, RoutedEventArgs e)
    {
        OP_Options.BribeOrBlindCops = CheckBox_BribeOrBlindCops.IsChecked == true;
        Online.BribeOrBlindCops(OP_Options.BribeOrBlindCops);
    }

    private void CheckBox_BribeAuthorities_Click(object sender, RoutedEventArgs e)
    {
        OP_Options.BribeAuthorities = CheckBox_BribeAuthorities.IsChecked == true;
        Online.BribeAuthorities(OP_Options.BribeAuthorities);
    }

    private void CheckBox_RevealPlayers_Click(object sender, RoutedEventArgs e)
    {
        OP_Options.RevealPlayers = CheckBox_RevealPlayers.IsChecked == true;
        Online.RevealPlayers(OP_Options.RevealPlayers);
    }

    //////////////////////////////////////////////////////////////////
    private void Button_Blips_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (sender is Button button)
        {
            var btnContent = button.Content.ToString();

            var blip = OnlineData.Blips.Find(t => t.Name == btnContent);
            if (blip != null)
                Teleport.ToBlips(blip.Value);
        }
    }

    private void Button_MerryweatherServices_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (sender is Button button)
        {
            var btnContent = button.Content.ToString();

            var service = OnlineData.MerryWeatherServices.Find(t => t.Name == btnContent);
            if (service != null)
                Online.MerryWeatherServices(service.Value);
        }
    }

    private void Button_InstantBullShark_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Online.InstantBullShark(true);
    }

    private void Button_RemoveBullShark_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Online.InstantBullShark(false);
    }

    private void Button_BackupHeli_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Online.CallBackupHeli(true);
    }

    private void Button_Airstrike_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Online.CallAirstrike(true);
    }

    /////////////////////////////////////////////////////////////

    private void Button_RequestKosatka_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Online.RequestKosatka();
    }

    private async void Button_TriggerRCBandito_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Online.TriggerRCBandito(true);
        await Task.Delay(100);
        Online.TriggerRCBandito(false);
    }

    private async void Button_TriggerMiniTank_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Online.TriggerMiniTank(true);
        await Task.Delay(100);
        Online.TriggerMiniTank(false);
    }

    private void CheckBox_KosatkaMissleCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.KosatkaMissleCooldown(CheckBox_KosatkaMissleCooldown.IsChecked == true);
    }
}
