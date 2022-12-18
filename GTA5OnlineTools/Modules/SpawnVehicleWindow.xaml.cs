using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;
using GTA5OnlineTools.Features.Client;

namespace GTA5OnlineTools.Modules;

/// <summary>
/// SpawnVehicleWindow.xaml 的交互逻辑
/// </summary>
public partial class SpawnVehicleWindow
{
    private VehicleSpawn vehicleSpawn = new();

    public SpawnVehicleWindow()
    {
        InitializeComponent();
    }

    private void Window_SpawnVehicle_Loaded(object sender, RoutedEventArgs e)
    {
        // 载具列表
        foreach (var item in VehicleData.VehicleClassData)
        {
            ListBox_VehicleClass.Items.Add(new EmojiMenu()
            {
                Emoji = item.Emoji,
                Title = item.Name
            });
        }
        ListBox_VehicleClass.SelectedIndex = 0;
    }

    private void Window_SpawnVehicle_Closing(object sender, CancelEventArgs e)
    {

    }

    private void ListBox_VehicleClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        lock (this)
        {
            var index = ListBox_VehicleClass.SelectedIndex;
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
                            if (index == ListBox_VehicleClass.SelectedIndex)
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
        vehicleSpawn.VehicleHash = 0;

        var index1 = ListBox_VehicleClass.SelectedIndex;
        var index2 = ListBox_VehicleInfo.SelectedIndex;
        if (index1 != -1 && index2 != -1)
        {
            vehicleSpawn.VehicleHash = VehicleData.VehicleClassData[index1].VehicleInfo[index2].Hash;
            vehicleSpawn.VehicleMod = VehicleData.VehicleClassData[index1].VehicleInfo[index2].Mod;
        }
    }

    private void Button_SpawnOnlineVehicleA_Click(object sender, RoutedEventArgs e)
    {
        

        Vehicle.SpawnVehicle(vehicleSpawn.VehicleHash, -255.0f, 5, vehicleSpawn.VehicleMod);
        //Vehicle.SpawnVehicle(vehicleSpawn.VehicleHash, -255.0f);
    }

    private void Button_SpawnOnlineVehicleB_Click(object sender, RoutedEventArgs e)
    {
        

        Vehicle.SpawnVehicle(vehicleSpawn.VehicleHash, 0.0f, 5, vehicleSpawn.VehicleMod);
        //Vehicle.SpawnVehicle(vehicleSpawn.VehicleHash, -255.0f);
    }
}
