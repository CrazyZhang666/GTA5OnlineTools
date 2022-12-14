using GTA5OnlineTools.Modules;
using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Helper;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// ModulesView.xaml 的交互逻辑
/// </summary>
public partial class ModulesView : UserControl
{
    private ExternalMenuWindow ExternalMenuWindow = null;
    private SpawnVehicleWindow SpawnVehicleWindow = null;
    private HeistsEditWindow HeistsEditWindow = null;

    private OutfitsEditWindow OutfitsEditWindow = null;
    private StatScriptsWindow StatScriptsWindow = null;
    private CasinoHackWindow CasinoHackWindow = null;
    private SpeedMeterWindow SpeedMeterWindow = null;

    public RelayCommand<string> ModelsClickCommand { get; private set; }

    /// <summary>
    /// 关闭全部第三方模块窗口委托
    /// </summary>
    public static Action ActionCloseAllModulesWindow;

    public ModulesView()
    {
        InitializeComponent();
        this.DataContext = this;

        ModelsClickCommand = new(ModelsClick);

        ActionCloseAllModulesWindow = CloseAllModulesWindow;
    }

    private void ModelsClick(string modelName)
    {
        AudioUtil.PlayClickSound();

        if (ProcessUtil.IsGTA5Run())
        {
            switch (modelName)
            {
                case "ExternalMenu":
                    ExternalMenuClick();
                    break;
                case "SpawnVehicle":
                    SpawnVehicleClick();
                    break;
                case "HeistsEdit":
                    HeistsEditClick();
                    break;
                case "OutfitsEdit":
                    OutfitsEditClick();
                    break;
                case "StatScripts":
                    StatScriptsClick();
                    break;
                case "CasinoHack":
                    CasinoHackClick();
                    break;
                case "SpeedMeter":
                    SpeedMeterClick();
                    break;
            }
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "未发现《GTA5》进程，请先运行《GTA5》游戏");
        }
    }

    #region 第三方模块按钮点击事件
    private void ExternalMenuClick()
    {
        if (ExternalMenuWindow == null)
        {
            ExternalMenuWindow = new ExternalMenuWindow();
            ExternalMenuWindow.Show();
        }
        else
        {
            if (ExternalMenuWindow.IsVisible)
            {
                if (!ExternalMenuWindow.Topmost)
                {
                    ExternalMenuWindow.Topmost = true;
                    ExternalMenuWindow.Topmost = false;
                }

                ExternalMenuWindow.WindowState = WindowState.Normal;
            }
            else
            {
                ExternalMenuWindow = null;
                ExternalMenuWindow = new ExternalMenuWindow();
                ExternalMenuWindow.Show();
            }
        }
    }

    private void SpawnVehicleClick()
    {
        if (SpawnVehicleWindow == null)
        {
            SpawnVehicleWindow = new SpawnVehicleWindow();
            SpawnVehicleWindow.Show();
        }
        else
        {
            if (SpawnVehicleWindow.IsVisible)
            {
                SpawnVehicleWindow.Topmost = true;
                SpawnVehicleWindow.Topmost = false;
                SpawnVehicleWindow.WindowState = WindowState.Normal;
            }
            else
            {
                SpawnVehicleWindow = null;
                SpawnVehicleWindow = new SpawnVehicleWindow();
                SpawnVehicleWindow.Show();
            }
        }
    }

    private void HeistsEditClick()
    {
        if (HeistsEditWindow == null)
        {
            HeistsEditWindow = new HeistsEditWindow();
            HeistsEditWindow.Show();
        }
        else
        {
            if (HeistsEditWindow.IsVisible)
            {
                HeistsEditWindow.Topmost = true;
                HeistsEditWindow.Topmost = false;
                HeistsEditWindow.WindowState = WindowState.Normal;
            }
            else
            {
                HeistsEditWindow = null;
                HeistsEditWindow = new HeistsEditWindow();
                HeistsEditWindow.Show();
            }
        }
    }

    private void OutfitsEditClick()
    {
        if (OutfitsEditWindow == null)
        {
            OutfitsEditWindow = new OutfitsEditWindow();
            OutfitsEditWindow.Show();
        }
        else
        {
            if (OutfitsEditWindow.IsVisible)
            {
                OutfitsEditWindow.Topmost = true;
                OutfitsEditWindow.Topmost = false;
                OutfitsEditWindow.WindowState = WindowState.Normal;
            }
            else
            {
                OutfitsEditWindow = null;
                OutfitsEditWindow = new OutfitsEditWindow();
                OutfitsEditWindow.Show();
            }
        }
    }

    private void StatScriptsClick()
    {
        if (StatScriptsWindow == null)
        {
            StatScriptsWindow = new StatScriptsWindow();
            StatScriptsWindow.Show();
        }
        else
        {
            if (StatScriptsWindow.IsVisible)
            {
                StatScriptsWindow.Topmost = true;
                StatScriptsWindow.Topmost = false;
                StatScriptsWindow.WindowState = WindowState.Normal;
            }
            else
            {
                StatScriptsWindow = null;
                StatScriptsWindow = new StatScriptsWindow();
                StatScriptsWindow.Show();
            }
        }
    }

    private void CasinoHackClick()
    {
        if (CasinoHackWindow == null)
        {
            CasinoHackWindow = new CasinoHackWindow();
            CasinoHackWindow.Show();
        }
        else
        {
            if (CasinoHackWindow.IsVisible)
            {
                CasinoHackWindow.Topmost = true;
                CasinoHackWindow.Topmost = false;
                CasinoHackWindow.WindowState = WindowState.Normal;
            }
            else
            {
                CasinoHackWindow = null;
                CasinoHackWindow = new CasinoHackWindow();
                CasinoHackWindow.Show();
            }
        }
    }

    private void SpeedMeterClick()
    {
        if (SpeedMeterWindow == null)
        {
            SpeedMeterWindow = new SpeedMeterWindow();
            SpeedMeterWindow.Show();
        }
        else
        {
            if (SpeedMeterWindow.IsVisible)
            {
                SpeedMeterWindow.Topmost = true;
                SpeedMeterWindow.Topmost = false;
                SpeedMeterWindow.WindowState = WindowState.Normal;
            }
            else
            {
                SpeedMeterWindow = null;
                SpeedMeterWindow = new SpeedMeterWindow();
                SpeedMeterWindow.Show();
            }
        }
    }
    #endregion

    ///////////////////////////////////////////////////////////////////

    /// <summary>
    /// 关闭全部模块窗口
    /// </summary>
    private void CloseAllModulesWindow()
    {
        this.Dispatcher.BeginInvoke(() =>
        {
            if (ExternalMenuWindow != null)
            {
                ExternalMenuWindow.Close();
                ExternalMenuWindow = null;
            }

            if (SpawnVehicleWindow != null)
            {
                SpawnVehicleWindow.Close();
                SpawnVehicleWindow = null;
            }

            if (HeistsEditWindow != null)
            {
                HeistsEditWindow.Close();
                HeistsEditWindow = null;
            }

            if (OutfitsEditWindow != null)
            {
                OutfitsEditWindow.Close();
                OutfitsEditWindow = null;
            }

            if (StatScriptsWindow != null)
            {
                StatScriptsWindow.Close();
                StatScriptsWindow = null;
            }

            if (CasinoHackWindow != null)
            {
                CasinoHackWindow.Close();
                CasinoHackWindow = null;
            }

            if (SpeedMeterWindow != null)
            {
                SpeedMeterWindow.Close();
                SpeedMeterWindow = null;
            }
        });
    }
}
