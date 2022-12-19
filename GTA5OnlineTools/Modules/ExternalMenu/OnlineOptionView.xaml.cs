using GTA5OnlineTools.GTA.SDK;
using GTA5OnlineTools.GTA.Client;
using GTA5OnlineTools.GTA.Settings;

namespace GTA5OnlineTools.Modules.ExternalMenu;

/// <summary>
/// OnlineOptionView.xaml 的交互逻辑
/// </summary>
public partial class OnlineOptionView : UserControl
{
    public OnlineOptionView()
    {
        InitializeComponent();
        this.DataContext = this;
        ExternalMenuWindow.WindowClosingEvent += ExternalMenuWindow_WindowClosingEvent;
    }

    private void ExternalMenuWindow_WindowClosingEvent()
    {
        
    }

    private void Button_Sessions_Click(object sender, RoutedEventArgs e)
    {
        var btnContent = (e.OriginalSource as Button).Content.ToString();
        var index = MiscData.Sessions.FindIndex(t => t.Name == btnContent);
        if (index != -1)
            Online.LoadSession(MiscData.Sessions[index].ID);
    }

    private void Button_EmptySession_Click(object sender, RoutedEventArgs e)
    {
        Online.EmptySession();
    }

    private void Button_Disconnect_Click(object sender, RoutedEventArgs e)
    {
        Online.Disconnect();
    }

    private void Button_StopCutscene_Click(object sender, RoutedEventArgs e)
    {
        Online.StopCutscene();
    }

    private void Button_RPxN_Click(object sender, RoutedEventArgs e)
    {
        var btnContent = (e.OriginalSource as Button).Content.ToString();
        var index = MiscData.RPxNs.FindIndex(t => t.Name == btnContent);
        if (index != -1)
            Online.RPMultiplier(MiscData.RPxNs[index].ID);
    }

    private void Button_REPxN_Click(object sender, RoutedEventArgs e)
    {
        var btnContent = (e.OriginalSource as Button).Content.ToString();
        var index = MiscData.REPxNs.FindIndex(t => t.Name == btnContent);
        if (index != -1)
            Online.REPMultiplier(MiscData.REPxNs[index].ID);
    }

    private void CheckBox_RemovePassiveModeCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.PassiveModeCooldown(CheckBox_RemovePassiveModeCooldown.IsChecked == true);
    }

    private void CheckBox_RemoveSuicideCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.SuicideCooldown(CheckBox_RemoveSuicideCooldown.IsChecked == true);
    }

    private void CheckBox_DisableOrbitalCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.DisableOrbitalCooldown(CheckBox_DisableOrbitalCooldown.IsChecked == true);
    }

    private void CheckBox_OffRadar_Click(object sender, RoutedEventArgs e)
    {
        Online.OffRadar(CheckBox_OffRadar.IsChecked == true);
    }

    private void CheckBox_GhostOrganization_Click(object sender, RoutedEventArgs e)
    {
        Online.GhostOrganization(CheckBox_GhostOrganization.IsChecked == true);
    }

    private void CheckBox_BribeOrBlindCops_Click(object sender, RoutedEventArgs e)
    {
        Online.BribeOrBlindCops(CheckBox_BribeOrBlindCops.IsChecked == true);
    }

    private void CheckBox_BribeAuthorities_Click(object sender, RoutedEventArgs e)
    {
        Online.BribeAuthorities(CheckBox_BribeAuthorities.IsChecked == true);
    }

    private void CheckBox_RevealPlayers_Click(object sender, RoutedEventArgs e)
    {
        Online.RevealPlayers(CheckBox_RevealPlayers.IsChecked == true);
    }

    private void CheckBox_AllowSellOnNonPublic_Click(object sender, RoutedEventArgs e)
    {
        MenuSetting.Online.AllowSellOnNonPublic = CheckBox_AllowSellOnNonPublic.IsChecked == true;
        Online.AllowSellOnNonPublic(CheckBox_AllowSellOnNonPublic.IsChecked == true);
    }

    private void CheckBox_OnlineSnow_Click(object sender, RoutedEventArgs e)
    {
        Online.SessionSnow(CheckBox_OnlineSnow.IsChecked == true);
    }

    private void Button_Blips_Click(object sender, RoutedEventArgs e)
    {
        var btnContent = (e.OriginalSource as Button).Content.ToString();

        var index = MiscData.Blips.FindIndex(t => t.Name == btnContent);
        if (index != -1)
            Teleport.ToBlips(MiscData.Blips[index].ID);
    }

    private void Button_MerryweatherServices_Click(object sender, RoutedEventArgs e)
    {
        var btnContent = (e.OriginalSource as Button).Content.ToString();

        var index = MiscData.MerryWeatherServices.FindIndex(t => t.Name == btnContent);
        if (index != -1)
            Online.MerryWeatherServices(MiscData.MerryWeatherServices[index].ID);
    }

    private void CheckBox_InstantBullShark_Click(object sender, RoutedEventArgs e)
    {
        Online.InstantBullShark(CheckBox_InstantBullShark.IsChecked == true);
    }

    private void CheckBox_BackupHeli_Click(object sender, RoutedEventArgs e)
    {
        Online.CallBackupHeli(CheckBox_BackupHeli.IsChecked == true);
    }

    private void CheckBox_Airstrike_Click(object sender, RoutedEventArgs e)
    {
        Online.CallAirstrike(CheckBox_Airstrike.IsChecked == true);
    }
}
