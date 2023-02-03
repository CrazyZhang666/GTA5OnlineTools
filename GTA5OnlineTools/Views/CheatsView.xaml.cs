using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Models;
using GTA5OnlineTools.Helper;
using GTA5OnlineTools.GTA.Core;
using GTA5OnlineTools.Views.ReadMe;
using GTA5OnlineTools.Windows;
using GTA5OnlineTools.Windows.Cheats;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// CheatsView.xaml 的交互逻辑
/// </summary>
public partial class CheatsView : UserControl
{
    /// <summary>
    /// Cheats 数据模型绑定
    /// </summary>
    public CheatsModel CheatsModel { get; set; } = new();

    private readonly Kiddion Kiddion = new();
    private readonly GTAHax GTAHax = new();
    private readonly BincoHax BincoHax = new();
    private readonly LSCHax LSCHax = new();
    private readonly YimMenu YimMenu = new();

    private GTAHaxStatWindow GTAHaxWindow = null;
    private KiddionChsWindow KiddionChsWindow = null;

    public CheatsView()
    {
        InitializeComponent();
        this.DataContext = this;

        new Thread(CheckCheatsIsRun)
        {
            Name = "CheckCheatsIsRun",
            IsBackground = true
        }.Start();
    }

    /// <summary>
    /// 检查第三方辅助是否正在运行线程
    /// </summary>
    private void CheckCheatsIsRun()
    {
        while (MainWindow.IsAppRunning)
        {
            // 判断 Kiddion 是否运行
            CheatsModel.KiddionIsRun = ProcessUtil.IsAppRun("Kiddion");
            // 判断 GTAHax 是否运行
            CheatsModel.GTAHaxIsRun = ProcessUtil.IsAppRun("GTAHax");
            // 判断 BincoHax 是否运行
            CheatsModel.BincoHaxIsRun = ProcessUtil.IsAppRun("BincoHax");
            // 判断 LSCHax 是否运行
            CheatsModel.LSCHaxIsRun = ProcessUtil.IsAppRun("LSCHax");

            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// 点击第三方辅助开关按钮
    /// </summary>
    /// <param name="hackName"></param>
    [RelayCommand]
    private void CheatsClick(string hackName)
    {
        if (ProcessUtil.IsGTA5Run())
        {
            switch (hackName)
            {
                case "Kiddion":
                    KiddionClick();
                    break;
                case "GTAHax":
                    GTAHaxClick();
                    break;
                case "BincoHax":
                    BincoHaxClick();
                    break;
                case "LSCHax":
                    LSCHaxClick();
                    break;
                case "YimMenu":
                    YimMenuClick();
                    break;
            }
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "未发现《GTA5》进程，请先运行《GTA5》游戏");
        }
    }

    /// <summary>
    /// 点击第三方辅助使用说明
    /// </summary>
    /// <param name="Name"></param>
    [RelayCommand]
    private void ReadMeClick(string Name)
    {
        switch (Name)
        {
            case "Kiddion":
                ShowReadMe(Kiddion);
                break;
            case "GTAHax":
                ShowReadMe(GTAHax);
                break;
            case "BincoHax":
                ShowReadMe(BincoHax);
                break;
            case "LSCHax":
                ShowReadMe(LSCHax);
                break;
            case "YimMenu":
                ShowReadMe(YimMenu);
                break;
        }
    }

    /// <summary>
    /// 点击第三方辅助配置文件路径
    /// </summary>
    /// <param name="funcName"></param>
    [RelayCommand]
    private void ExtraClick(string funcName)
    {
        switch (funcName)
        {
            #region Kiddion额外功能
            case "KiddionKey104":
                KiddionKey104Click();
                break;
            case "KiddionKey87":
                KiddionKey87Click();
                break;
            case "KiddionConfigDirectory":
                KiddionConfigDirectoryClick();
                break;
            case "KiddionScriptsDirectory":
                KiddionScriptsDirectoryClick();
                break;
            case "EditKiddionConfig":
                EditKiddionConfigClick();
                break;
            case "EditKiddionTheme":
                EditKiddionThemeClick();
                break;
            case "EditKiddionTP":
                EditKiddionTPClick();
                break;
            case "EditKiddionVC":
                EditKiddionVCClick();
                break;
            case "ResetKiddionConfig":
                ResetKiddionConfigClick();
                break;
            #endregion
            ////////////////////////////////////
            #region 其他额外功能
            case "EditGTAHaxStat":
                EditGTAHaxStatClick();
                break;
            case "DefaultGTAHaxStat":
                DefaultGTAHaxStatClick();
                break;
            case "YimMenuDirectory":
                YimMenuDirectoryClick();
                break;
            case "ResetYimMenuConfig":
                ResetYimMenuConfigClick();
                break;
            #endregion
            ////////////////////////////////////
            default:
                break;
        }
    }

    /// <summary>
    /// 显示使用说明窗口
    /// </summary>
    /// <param name="userControl"></param>
    private void ShowReadMe(UserControl userControl)
    {
        var readMeWindow = new ReadMeWindow(userControl)
        {
            Owner = MainWindow.MainWindowInstance
        };
        readMeWindow.ShowDialog();
    }

    #region 第三方辅助功能开关事件
    /// <summary>
    /// Kiddion点击事件
    /// </summary>
    private void KiddionClick()
    {
        lock (this)
        {
            int count = 0;

            Task.Run(async () =>
            {
                if (CheatsModel.KiddionIsRun)
                {
                    ProcessUtil.OpenProcess(FileUtil.File_Kiddion_Kiddion);

                    do
                    {
                        // 等待Kiddion启动
                        if (ProcessUtil.IsAppRun("Kiddion"))
                        {
                            // 拿到Kiddion进程
                            var pKiddion = Process.GetProcessesByName("Kiddion").ToList()[0];
                            BaseInjector.DLLInjector(pKiddion.Id, FileUtil.File_Kiddion_KiddionChs);
                            return;
                        }

                        await Task.Delay(250);
                    } while (count++ > 10);
                }
                else
                {
                    ProcessUtil.CloseProcess("Kiddion");
                }
            });
        }
    }

    /// <summary>
    /// GTAHax点击事件
    /// </summary>
    private void GTAHaxClick()
    {
        if (CheatsModel.GTAHaxIsRun)
            ProcessUtil.OpenProcess(FileUtil.File_Cache_GTAHax);
        else
            ProcessUtil.CloseProcess("GTAHax");
    }

    /// <summary>
    /// BincoHax点击事件
    /// </summary>
    private void BincoHaxClick()
    {
        if (CheatsModel.BincoHaxIsRun)
            ProcessUtil.OpenProcess(FileUtil.File_Cache_BincoHax);
        else
            ProcessUtil.CloseProcess("BincoHax");
    }

    /// <summary>
    /// LSCHax点击事件
    /// </summary>
    private void LSCHaxClick()
    {
        if (CheatsModel.LSCHaxIsRun)
            ProcessUtil.OpenProcess(FileUtil.File_Cache_LSCHax);
        else
            ProcessUtil.CloseProcess("LSCHax");
    }

    /// <summary>
    /// YimMenu点击事件
    /// </summary>
    private void YimMenuClick()
    {
        if (!File.Exists(FileUtil.File_Inject_YimMenu))
        {
            NotifierHelper.Show(NotifierType.Error, "发生异常，YimMenu菜单DLL文件不存在");
            return;
        }

        foreach (ProcessModule module in Process.GetProcessById(Memory.GTA5ProId).Modules)
        {
            if (module.FileName == FileUtil.File_Inject_YimMenu)
            {
                NotifierHelper.Show(NotifierType.Warning, "该DLL已经被注入过了，请勿重复注入");
                return;
            }
        }

        try
        {
            BaseInjector.DLLInjector(Memory.GTA5ProId, FileUtil.File_Inject_YimMenu);
            Memory.SetForegroundWindow();
            NotifierHelper.Show(NotifierType.Success, "YimMenu注入成功，请前往游戏查看");
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
    #endregion

    #region Kiddion额外功能
    /// <summary>
    /// 启用Kiddion[104键]
    /// </summary>
    private void KiddionKey104Click()
    {
        ProcessUtil.CloseProcess("Kiddion");
        FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Config, FileUtil.File_Kiddion_Config);
        NotifierHelper.Show(NotifierType.Success, "切换到《Kiddion [104键]》成功");
    }

    /// <summary>
    /// 启用Kiddion[87键]
    /// </summary>
    private void KiddionKey87Click()
    {
        ProcessUtil.CloseProcess("Kiddion");
        FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Key87_Config, FileUtil.File_Kiddion_Config);
        NotifierHelper.Show(NotifierType.Success, "切换到《Kiddion [87键]》成功");
    }

    /// <summary>
    /// Kiddion配置目录
    /// </summary>
    private void KiddionConfigDirectoryClick()
    {
        ProcessUtil.OpenLink(FileUtil.Dir_Kiddion);
    }

    /// <summary>
    /// Kiddion脚本目录
    /// </summary>
    private void KiddionScriptsDirectoryClick()
    {
        ProcessUtil.OpenLink(FileUtil.Dir_Kiddion_Scripts);
    }

    /// <summary>
    /// 编辑Kiddion配置文件
    /// </summary>
    private void EditKiddionConfigClick()
    {
        ProcessUtil.Notepad2EditTextFile(FileUtil.File_Kiddion_Config);
    }

    /// <summary>
    /// 编辑Kiddion主题文件
    /// </summary>
    private void EditKiddionThemeClick()
    {
        ProcessUtil.Notepad2EditTextFile(FileUtil.File_Kiddion_Themes);
    }

    /// <summary>
    /// 编辑Kiddion自定义传送
    /// </summary>
    private void EditKiddionTPClick()
    {
        ProcessUtil.Notepad2EditTextFile(FileUtil.File_Kiddion_Teleports);
    }

    /// <summary>
    /// 编辑Kiddion自定义载具
    /// </summary>
    private void EditKiddionVCClick()
    {
        ProcessUtil.Notepad2EditTextFile(FileUtil.File_Kiddion_Vehicles);
    }

    /// <summary>
    /// 重置Kiddion配置文件
    /// </summary>
    private void ResetKiddionConfigClick()
    {
        try
        {
            if (MessageBox.Show("你确定要重置Kiddion配置文件吗？\n\n将清空「C:\\ProgramData\\GTA5OnlineTools\\Kiddion」文件夹，如有重要文件请提前备份",
                "重置Kiddion配置文件", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ProcessUtil.CloseProcess("Kiddion");
                ProcessUtil.CloseProcess("Notepad2");
                Thread.Sleep(100);

                FileUtil.DelectDir(FileUtil.Dir_Kiddion);
                Directory.CreateDirectory(FileUtil.Dir_Kiddion_Scripts);
                Thread.Sleep(100);

                FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Kiddion, FileUtil.File_Kiddion_Kiddion);
                FileUtil.ExtractResFile(FileUtil.Res_Kiddion_KiddionChs, FileUtil.File_Kiddion_KiddionChs);

                FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Config, FileUtil.File_Kiddion_Config);
                FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Themes, FileUtil.File_Kiddion_Themes);
                FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Teleports, FileUtil.File_Kiddion_Teleports);
                FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Vehicles, FileUtil.File_Kiddion_Vehicles);

                FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Scripts_Readme, FileUtil.File_Kiddion_Scripts_Readme);

                NotifierHelper.Show(NotifierType.Success, "重置Kiddion配置文件成功");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
    #endregion

    #region 其他额外功能
    /// <summary>
    /// 编辑GTAHax导入文件
    /// </summary>
    private void EditGTAHaxStatClick()
    {
        ProcessUtil.Notepad2EditTextFile(FileUtil.File_Cache_Stat);
    }

    /// <summary>
    /// GTAHax预设代码
    /// </summary>
    private void DefaultGTAHaxStatClick()
    {
        if (GTAHaxWindow == null)
        {
            GTAHaxWindow = new GTAHaxStatWindow();
            GTAHaxWindow.Show();
        }
        else
        {
            if (GTAHaxWindow.IsVisible)
            {
                if (!GTAHaxWindow.Topmost)
                {
                    GTAHaxWindow.Topmost = true;
                    GTAHaxWindow.Topmost = false;
                }

                GTAHaxWindow.WindowState = WindowState.Normal;
            }
            else
            {
                GTAHaxWindow = null;
                GTAHaxWindow = new GTAHaxStatWindow();
                GTAHaxWindow.Show();
            }
        }
    }

    /// <summary>
    /// YimMenu配置目录
    /// </summary>
    private void YimMenuDirectoryClick()
    {
        ProcessUtil.OpenPath(FileUtil.YimMenu_Path);
    }

    /// <summary>
    /// 重置YimMenu配置文件
    /// </summary>
    private void ResetYimMenuConfigClick()
    {
        try
        {
            if (FileUtil.IsOccupied(FileUtil.File_Inject_YimMenu))
            {
                NotifierHelper.Show(NotifierType.Warning, "YimMenu被占用，请先卸载YimMenu菜单后再执行操作");
                return;
            }

            if (MessageBox.Show($"你确定要重置YimMenu配置文件吗？\n\n将清空「{FileUtil.YimMenu_Path}」文件夹，如有重要文件请提前备份",
                "重置YimMenu配置文件", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                FileUtil.DelectDir(FileUtil.YimMenu_Path);

                NotifierHelper.Show(NotifierType.Success, "重置YimMenu配置文件成功");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
    #endregion
}
