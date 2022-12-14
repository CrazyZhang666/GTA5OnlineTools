using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;
using GTA5OnlineTools.Features.Client;

namespace GTA5OnlineTools.Modules.ExternalMenu;

/// <summary>
/// OtherMiscView.xaml 的交互逻辑
/// </summary>
public partial class OtherMiscView : UserControl
{
    private List<PerVehInfo> PerVehInfos = new();

    public OtherMiscView()
    {
        InitializeComponent();
        this.DataContext = this;
        ExternalMenuWindow.WindowClosingEvent += ExternalMenuWindow_WindowClosingEvent;

        // Ped列表
        foreach (var item in PedData.PedDataClass)
        {
            ListBox_PedModel.Items.Add(item.DisplayName);
        }
        ListBox_PedModel.SelectedIndex = 0;
    }

    private void ExternalMenuWindow_WindowClosingEvent()
    {
        
    }

    private void Button_RefushPersonalVehicleList_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        lock (this)
        {
            ListBox_PersonalVehicle.Items.Clear();
            PerVehInfos.Clear();

            Task.Run(() =>
            {
                int max_slots = Hacks.ReadGA<int>(1585857);
                for (int i = 0; i < max_slots; i++)
                {
                    long hash = Hacks.ReadGA<long>(1585857 + 1 + (i * 142) + 66);
                    if (hash == 0)
                        continue;

                    string plate = Hacks.ReadGAString(1585857 + 1 + (i * 142) + 1);

                    PerVehInfos.Add(new PerVehInfo()
                    {
                        Index = i,
                        Name = Vehicle.FindVehicleDisplayName(hash, true),
                        Hash = hash,
                        Plate = plate
                    });
                }

                foreach (var item in PerVehInfos)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        ListBox_PersonalVehicle.Items.Add($"[{item.Plate}]\t{item.Name}");
                    });
                }
            });
        }
    }

    private void Button_SpawnPersonalVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        var index = ListBox_PersonalVehicle.SelectedIndex;
        if (index != -1)
        {
            Task.Run(() =>
            {
                Vehicle.SpawnPersonalVehicle(PerVehInfos[index].Index);
            });
        }
    }

    private void Button_GetInOnlinePV_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        Online.GetInOnlinePV();
    }

    ////////////////////////////////////////////////////////////////////////////////

    private void Button_ModelChange_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        var index = ListBox_PedModel.SelectedIndex;
        if (index != -1)
            Online.ModelChange(PedData.PedDataClass[index].Hash);
    }

    private void ListBox_PedModel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        Button_ModelChange_Click(null, null);
    }
}
