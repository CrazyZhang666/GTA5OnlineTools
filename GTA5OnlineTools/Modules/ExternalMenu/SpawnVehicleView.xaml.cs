using GTA5OnlineTools.Utils;
using GTA5OnlineTools.GTA.SDK;
using GTA5OnlineTools.GTA.Data;
using GTA5OnlineTools.GTA.Client;
using GTA5OnlineTools.GTA.Settings;

namespace GTA5OnlineTools.Modules.ExternalMenu;

/// <summary>
/// SpawnVehicleView.xaml 的交互逻辑
/// </summary>
public partial class SpawnVehicleView : UserControl
{
    private VehicleSpawn VehicleSpawn = new();

    public SpawnVehicleView()
    {
        InitializeComponent();
        this.DataContext = this;
        ExternalMenuWindow.WindowClosingEvent += ExternalMenuWindow_WindowClosingEvent;

        // 载具列表
        foreach (var item in VehicleData.VehicleClassData)
        {
            ComboBox_VehicleClass.Items.Add(new IconMenu()
            {
                Icon = item.Icon,
                Title = item.Name
            });
        }
        ComboBox_VehicleClass.SelectedIndex = 0;

        // 载具附加功能
        foreach (var item in MiscData.VehicleExtras)
        {
            ComboBox_VehicleExtras.Items.Add(item.Name);
        }
    }

    private void ExternalMenuWindow_WindowClosingEvent()
    {

    }

    private void ComboBox_VehicleClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        lock (this)
        {
            var index = ComboBox_VehicleClass.SelectedIndex;
            if (index != -1)
            {
                ListBox_VehicleInfo.Items.Clear();

                Task.Run(() =>
                {
                    var className = VehicleData.VehicleClassData[index].Name;

                    for (int i = 0; i < VehicleData.VehicleClassData[index].VehicleInfo.Count; i++)
                    {
                        var name = VehicleData.VehicleClassData[index].VehicleInfo[i].Name;
                        var displayName = VehicleData.VehicleClassData[index].VehicleInfo[i].Display;

                        this.Dispatcher.BeginInvoke(DispatcherPriority.Background, () =>
                        {
                            if (index == ComboBox_VehicleClass.SelectedIndex)
                            {
                                ListBox_VehicleInfo.Items.Add(new ModelPreview()
                                {
                                    Id = name,
                                    Name = displayName,
                                    Image = $"\\Assets\\Images\\Client\\Vehicles\\{name}.png"
                                });
                            }
                        });
                    }
                });

                ListBox_VehicleInfo.SelectedIndex = 0;
            }
        }
    }

    private void ListBox_VehicleInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        VehicleSpawn.VehicleHash = 0;

        var index1 = ComboBox_VehicleClass.SelectedIndex;
        var index2 = ListBox_VehicleInfo.SelectedIndex;
        if (index1 != -1 && index2 != -1)
        {
            VehicleSpawn.VehicleHash = VehicleData.VehicleClassData[index1].VehicleInfo[index2].Hash;
            VehicleSpawn.VehicleMod = VehicleData.VehicleClassData[index1].VehicleInfo[index2].Mod;
        }
    }

    private void Button_SpawnOnlineVehicleA_Click(object sender, RoutedEventArgs e)
    {
        

        Vehicle.SpawnVehicle(VehicleSpawn.VehicleHash, -255.0f, 5, VehicleSpawn.VehicleMod);
        //Vehicle.SpawnVehicle(vehicleSpawn.VehicleHash, -255.0f);
    }

    private void Button_SpawnOnlineVehicleB_Click(object sender, RoutedEventArgs e)
    {
        

        Vehicle.SpawnVehicle(VehicleSpawn.VehicleHash, 0.0f, 5, VehicleSpawn.VehicleMod);
        //Vehicle.SpawnVehicle(vehicleSpawn.VehicleHash, -255.0f);
    }

    /////////////////////////////////////////////////////////////////////////////////

    private void CheckBox_VehicleGodMode_Click(object sender, RoutedEventArgs e)
    {
        MenuSetting.Vehicle.GodMode = CheckBox_VehicleGodMode.IsChecked == true;
        Vehicle.GodMode(CheckBox_VehicleGodMode.IsChecked == true);
    }

    private void CheckBox_VehicleSeatbelt_Click(object sender, RoutedEventArgs e)
    {
        MenuSetting.Vehicle.Seatbelt = CheckBox_VehicleSeatbelt.IsChecked == true;
        Vehicle.Seatbelt(CheckBox_VehicleSeatbelt.IsChecked == true);
    }

    private void CheckBox_VehicleParachute_Click(object sender, RoutedEventArgs e)
    {
        Vehicle.Parachute(CheckBox_VehicleParachute.IsChecked == true);
    }

    private void CheckBox_VehicleInvisibility_Click(object sender, RoutedEventArgs e)
    {
        Vehicle.Invisible(CheckBox_VehicleInvisibility.IsChecked == true);
    }

    private void Button_FillVehicleHealth_Click(object sender, RoutedEventArgs e)
    {
        

        Vehicle.FillHealth();
    }

    private void Button_RepairVehicle_Click(object sender, RoutedEventArgs e)
    {
        

        Vehicle.FixVehicleByBST();
    }

    private void Button_TurnOffBST_Click(object sender, RoutedEventArgs e)
    {
        

        Online.InstantBullShark(false);
    }

    private void ComboBox_VehicleExtras_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var index = ComboBox_VehicleExtras.SelectedIndex;
        if (index != -1)
        {
            Vehicle.Extras((short)MiscData.VehicleExtras[index].ID);
        }
    }

    private void CheckBox_TriggerRCBandito_Click(object sender, RoutedEventArgs e)
    {
        Online.TriggerRCBandito(CheckBox_TriggerRCBandito.IsChecked == true);
    }

    private void CheckBox_TriggerMiniTank_Click(object sender, RoutedEventArgs e)
    {
        Online.TriggerMiniTank(CheckBox_TriggerMiniTank.IsChecked == true);
    }
}
