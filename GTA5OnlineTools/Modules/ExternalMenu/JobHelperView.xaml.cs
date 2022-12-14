using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Client;

namespace GTA5OnlineTools.Modules.ExternalMenu;

/// <summary>
/// JobHelperView.xaml 的交互逻辑
/// </summary>
public partial class JobHelperView : UserControl
{
    public JobHelperView()
    {
        InitializeComponent();
        this.DataContext = this;
        ExternalMenuWindow.WindowClosingEvent += ExternalMenuWindow_WindowClosingEvent;
    }

    private void ExternalMenuWindow_WindowClosingEvent()
    {

    }

    private void CheckBox_BunkerSupplyDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.BunkerSupplyDelay(CheckBox_BunkerSupplyDelay.IsChecked == true);
    }

    private void CheckBox_UnlockBunkerResearch_Click(object sender, RoutedEventArgs e)
    {
        Online.UnlockBunkerResearch(CheckBox_UnlockBunkerResearch.IsChecked == true);
    }

    private void CheckBox_CEOBuyingCratesCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.CEOBuyingCratesCooldown(CheckBox_CEOBuyingCratesCooldown.IsChecked == true);
    }

    private void CheckBox_CEOSellingCratesCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.CEOSellingCratesCooldown(CheckBox_CEOSellingCratesCooldown.IsChecked == true);
    }

    private void CheckBox_MCSupplyDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.MCSupplyDelay(CheckBox_MCSupplyDelay.IsChecked == true);
    }

    private void Button_CEOCargos_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        var btnContent = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.CEOCargos.FindIndex(t => t.Name == btnContent);
        if (index != -1)
        {
            Online.CEOSpecialCargo(false);
            Online.CEOCargoType(MiscData.CEOCargos[index].ID);
        }
    }

    private void Button_CEOSpecialCargos_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        var btnContent = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.CEOSpecialCargos.FindIndex(t => t.Name == btnContent);
        if (index != -1)
        {
            // They are in gb_contraband_buy at func_915, for future updates.
            Online.CEOSpecialCargo(true);
            Online.CEOCargoType(MiscData.CEOSpecialCargos[index].ID);
        }
    }

    private void CheckBox_ExportVehicleDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.ExportVehicleDelay(CheckBox_ExportVehicleDelay.IsChecked == true);
    }

    private void CheckBox_SmugglerRunInDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.SmugglerRunInDelay(CheckBox_SmugglerRunInDelay.IsChecked == true);
    }

    private void CheckBox_SmugglerRunOutDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.SmugglerRunOutDelay(CheckBox_SmugglerRunOutDelay.IsChecked == true);
    }

    private void CheckBox_NightclubOutDelay_Click(object sender, RoutedEventArgs e)
    {
        Online.NightclubOutDelay(CheckBox_NightclubOutDelay.IsChecked == true);
    }

    private void CheckBox_CEOWorkCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.CEOWorkCooldown(CheckBox_CEOWorkCooldown.IsChecked == true);
    }

    private void CheckBox_ClientJonCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.ClientJonCooldown(CheckBox_ClientJonCooldown.IsChecked == true);
    }

    private void CheckBox_SecurityHitCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.SecurityHitCooldown(CheckBox_SecurityHitCooldown.IsChecked == true);
    }

    private void CheckBox_PayphoneHitCooldown_Click(object sender, RoutedEventArgs e)
    {
        Online.PayphoneHitCooldown(CheckBox_PayphoneHitCooldown.IsChecked == true);
    }
}
