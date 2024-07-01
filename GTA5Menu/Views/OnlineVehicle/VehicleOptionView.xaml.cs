using CommunityToolkit.Mvvm.ComponentModel;
using GTA5Core.Features;
using GTA5Core.GTA.Onlines;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.OnlineVehicle;

/// <summary>
/// VehicleOptionView.xaml 的交互逻辑
/// </summary>
public partial class VehicleOptionView : UserControl
{
    public partial class Options : ObservableObject
    {
        [ObservableProperty] public bool godMode = false;
        [ObservableProperty] public bool seatbelt = false;
        [ObservableProperty] public bool parachute = false;

        [ObservableProperty] public bool extra = false;
        [ObservableProperty] public short extraFlag = 0;
    }
    public Options VO_Options { get; set; } = new();

    public VehicleOptionView()
    {
        InitializeComponent();
        GTA5MenuWindow.WindowClosingEvent += GTA5MenuWindow_WindowClosingEvent;
        GTA5MenuWindow.LoopSpeedNormalEvent += GTA5MenuWindow_LoopSpeedNormalEvent;

        ReadConfig();

        // 载具附加功能
        foreach (var item in OnlineData.VehicleExtras)
        {
            ListBox_VehicleExtras.Items.Add(item.Name);
        }
        ListBox_VehicleExtras.SelectedIndex = 0;
    }

    private void GTA5MenuWindow_WindowClosingEvent()
    {
        GTA5MenuWindow.WindowClosingEvent -= GTA5MenuWindow_WindowClosingEvent;
        GTA5MenuWindow.LoopSpeedNormalEvent -= GTA5MenuWindow_LoopSpeedNormalEvent;
        SaveConfig();
    }

    private void ReadConfig()
    {
        VO_Options.GodMode = IniHelper.ReadValue("VehicleOption", "GodMode").Equals("True", StringComparison.OrdinalIgnoreCase);
        VO_Options.Seatbelt = IniHelper.ReadValue("VehicleOption", "Seatbelt").Equals("True", StringComparison.OrdinalIgnoreCase);
        VO_Options.Parachute = IniHelper.ReadValue("VehicleOption", "Parachute").Equals("True", StringComparison.OrdinalIgnoreCase);
    }

    private void SaveConfig()
    {
        IniHelper.WriteValue("VehicleOption", "GodMode", $"{VO_Options.GodMode}");
        IniHelper.WriteValue("VehicleOption", "Seatbelt", $"{VO_Options.Seatbelt}");
        IniHelper.WriteValue("VehicleOption", "Parachute", $"{VO_Options.Parachute}");
    }

    private void GTA5MenuWindow_LoopSpeedNormalEvent()
    {
        // 载具无敌
        if (VO_Options.GodMode)
            Vehicle.GodMode(true);
        // 载具安全带
        if (VO_Options.Seatbelt)
            Vehicle.Seatbelt(true);
        // 载具降落伞
        if (VO_Options.Parachute)
            Vehicle.Parachute(true);

        // 载具附加功能
        if (VO_Options.Extra)
            Vehicle.Extras(VO_Options.ExtraFlag);
    }

    /////////////////////////////////////////////////

    private void VehicleExtras()
    {
        var index = ListBox_VehicleExtras.SelectedIndex;
        if (index == -1 || index == 0)
        {
            VO_Options.Extra = false;
            return;
        }

        VO_Options.Extra = true;
        VO_Options.ExtraFlag = (short)OnlineData.VehicleExtras[index].Value;
        Vehicle.Extras(VO_Options.ExtraFlag);
    }

    private void CheckBox_VehicleGodMode_Click(object sender, RoutedEventArgs e)
    {
        VO_Options.GodMode = CheckBox_VehicleGodMode.IsChecked == true;
        Vehicle.GodMode(VO_Options.GodMode);
    }

    private void CheckBox_VehicleSeatbelt_Click(object sender, RoutedEventArgs e)
    {
        VO_Options.Seatbelt = CheckBox_VehicleSeatbelt.IsChecked == true;
        Vehicle.Seatbelt(VO_Options.Seatbelt);
    }

    private void CheckBox_VehicleParachute_Click(object sender, RoutedEventArgs e)
    {
        VO_Options.Parachute = CheckBox_VehicleParachute.IsChecked == true;
        Vehicle.Parachute(VO_Options.Parachute);
    }

    private void Button_FillVehicleHealth_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Vehicle.FillHealth();
    }

    private async void Button_RepairVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        await Vehicle.FixVehicleByBST();
    }

    private void Button_RemoveBullShark_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Online.InstantBullShark(false);
    }

    private void Button_Unlock168Vehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Vehicle.Unlock168Vehicle();
    }

    private void ListBox_VehicleExtras_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        VehicleExtras();
    }

    private void ListBox_VehicleExtras_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        VehicleExtras();
    }
}
