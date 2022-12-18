using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Models;
using GTA5OnlineTools.Helper;
using GTA5OnlineTools.Views.Cheats;
using GTA5OnlineTools.Features.Core;
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

    private readonly KiddionPage KiddionPage = new();
    private readonly GTAHaxPage GTAHaxPage = new();
    private readonly BincoHaxPage BincoHaxPage = new();
    private readonly LSCHaxPage LSCHaxPage = new();
    private readonly YimMenuPage YimMenuPage = new();

    private GTAHaxStatWindow GTAHaxWindow = null;
    private KiddionChsWindow KiddionChsWindow = null;
    private KiddionLuaWindow KiddionLuaWindow = null;

    public CheatsView()
    {
        InitializeComponent();
        this.DataContext = this;
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        new Thread(CheckCheatsIsRun)
        {
            Name = "CheckCheatsIsRun",
            IsBackground = true
        }.Start();
    }

    private void MainWindow_WindowClosingEvent()
    {

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
    /// <param name="pageName"></param>
    [RelayCommand]
    private void ReadMeClick(string pageName)
    {
        switch (pageName)
        {
            case "KiddionPage":
                CheatsModel.FrameState = Visibility.Visible;
                CheatsModel.FrameContent = KiddionPage;
                break;
            case "GTAHaxPage":
                CheatsModel.FrameState = Visibility.Visible;
                CheatsModel.FrameContent = GTAHaxPage;
                break;
            case "BincoHaxPage":
                CheatsModel.FrameState = Visibility.Visible;
                CheatsModel.FrameContent = BincoHaxPage;
                break;
            case "LSCHaxPage":
                CheatsModel.FrameState = Visibility.Visible;
                CheatsModel.FrameContent = LSCHaxPage;
                break;
            case "YimMenuPage":
                CheatsModel.FrameState = Visibility.Visible;
                CheatsModel.FrameContent = YimMenuPage;
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
            #region Kiddion增强功能
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
            case "KiddionChsHelper":
                KiddionChsHelperClick();
                break;
            case "KiddionLuaHelper":
                KiddionLuaHelperClick();
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
            #region 其他增强功能
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
    /// 使用说明隐藏按钮点击事件
    /// </summary>
    [RelayCommand]
    private void FrameHideClick()
    {
        CheatsModel.FrameState = Visibility.Collapsed;
        CheatsModel.FrameContent = null;
    }

    #region 第三方辅助功能开关事件
    /// <summary>
    /// Kiddion点击事件
    /// </summary>
    private void KiddionClick()
    {
        bool isRun = false;

        Task.Run(() =>
        {
            if (CheatsModel.KiddionIsRun)
            {
                ProcessUtil.OpenProcess("Kiddion", true);

                if (CheatsModel.IsUseKiddionChs)
                {
                    do
                    {
                        // 等待Kiddion启动
                        if (ProcessUtil.IsAppRun("Kiddion"))
                        {
                            // Kiddion进程启动标志
                            isRun = true;

                            // 拿到Kiddion进程
                            var pKiddion = Process.GetProcessesByName("Kiddion").ToList()[0];
                            BaseInjector.DLLInjector(pKiddion.Id, FileUtil.D_Kiddion_Path + "KiddionChs.dll");
                        }
                        else
                        {
                            isRun = false;
                        }

                        Task.Delay(250).Wait();
                    } while (!isRun);
                }
            }
            else
            {
                ProcessUtil.CloseProcess("Kiddion");
            }
        });

        Task.Run(() =>
        {
            // 模拟任务超时
            Task.Delay(5000);
            isRun = true;
        });
    }

    /// <summary>
    /// GTAHax点击事件
    /// </summary>
    private void GTAHaxClick()
    {
        if (CheatsModel.GTAHaxIsRun)
            ProcessUtil.OpenProcess("GTAHax", false);
        else
            ProcessUtil.CloseProcess("GTAHax");
    }

    /// <summary>
    /// BincoHax点击事件
    /// </summary>
    private void BincoHaxClick()
    {
        if (CheatsModel.BincoHaxIsRun)
            ProcessUtil.OpenProcess("BincoHax", false);
        else
            ProcessUtil.CloseProcess("BincoHax");
    }

    /// <summary>
    /// LSCHax点击事件
    /// </summary>
    private void LSCHaxClick()
    {
        if (CheatsModel.LSCHaxIsRun)
            ProcessUtil.OpenProcess("LSCHax", false);
        else
            ProcessUtil.CloseProcess("LSCHax");
    }

    /// <summary>
    /// YimMenu点击事件
    /// </summary>
    private void YimMenuClick()
    {
        var dllPath = FileUtil.D_Inject_Path + "YimMenu.dll";

        if (!File.Exists(dllPath))
        {
            NotifierHelper.Show(NotifierType.Error, "发生异常，YimMenu菜单DLL文件不存在");
            return;
        }

        foreach (ProcessModule module in Process.GetProcessById(Memory.GTA5ProId).Modules)
        {
            if (module.FileName == dllPath)
            {
                NotifierHelper.Show(NotifierType.Warning, "该DLL已经被注入过了，请勿重复注入");
                return;
            }
        }

        try
        {
            BaseInjector.DLLInjector(Memory.GTA5ProId, dllPath);
            Memory.SetForegroundWindow();
            NotifierHelper.Show(NotifierType.Success, "YimMenu注入成功，请前往游戏查看");
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
    #endregion

    #region Kiddion增强功能
    /// <summary>
    /// 启用Kiddion[104键]
    /// </summary>
    private void KiddionKey104Click()
    {
        ProcessUtil.CloseProcess("Kiddion");
        FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "config.json", FileUtil.D_Kiddion_Path + "config.json");
        NotifierHelper.Show(NotifierType.Success, "切换到《Kiddion [104键]》成功");
    }

    /// <summary>
    /// 启用Kiddion[87键]
    /// </summary>
    private void KiddionKey87Click()
    {
        ProcessUtil.CloseProcess("Kiddion");
        FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "key87.config.json", FileUtil.D_Kiddion_Path + "config.json");
        NotifierHelper.Show(NotifierType.Success, "切换到《Kiddion [87键]》成功");
    }

    /// <summary>
    /// Kiddion配置目录
    /// </summary>
    private void KiddionConfigDirectoryClick()
    {
        ProcessUtil.OpenPath(FileUtil.D_Kiddion_Path);
    }

    /// <summary>
    /// Kiddion脚本目录
    /// </summary>
    private void KiddionScriptsDirectoryClick()
    {
        ProcessUtil.OpenPath(FileUtil.D_KiddionScripts_Path);
    }

    /// <summary>
    /// Kiddion 汉化修正
    /// </summary>
    private void KiddionChsHelperClick()
    {
        if (KiddionChsWindow == null)
        {
            KiddionChsWindow = new KiddionChsWindow();
            KiddionChsWindow.Show();
        }
        else
        {
            if (KiddionChsWindow.IsVisible)
            {
                if (!KiddionChsWindow.Topmost)
                {
                    KiddionChsWindow.Topmost = true;
                    KiddionChsWindow.Topmost = false;
                }

                KiddionChsWindow.WindowState = WindowState.Normal;
            }
            else
            {
                KiddionChsWindow = null;
                KiddionChsWindow = new KiddionChsWindow();
                KiddionChsWindow.Show();
            }
        }
    }

    /// <summary>
    /// Kiddion Lua生成
    /// </summary>
    private void KiddionLuaHelperClick()
    {
        if (KiddionLuaWindow == null)
        {
            KiddionLuaWindow = new KiddionLuaWindow();
            KiddionLuaWindow.Show();
        }
        else
        {
            if (KiddionLuaWindow.IsVisible)
            {
                if (!KiddionLuaWindow.Topmost)
                {
                    KiddionLuaWindow.Topmost = true;
                    KiddionLuaWindow.Topmost = false;
                }

                KiddionLuaWindow.WindowState = WindowState.Normal;
            }
            else
            {
                KiddionLuaWindow = null;
                KiddionLuaWindow = new KiddionLuaWindow();
                KiddionLuaWindow.Show();
            }
        }
    }

    /// <summary>
    /// 编辑Kiddion配置文件
    /// </summary>
    private void EditKiddionConfigClick()
    {
        ProcessUtil.Notepad2EditTextFile(FileUtil.D_Kiddion_Path + "config.json");
    }

    /// <summary>
    /// 编辑Kiddion主题文件
    /// </summary>
    private void EditKiddionThemeClick()
    {
        ProcessUtil.Notepad2EditTextFile(FileUtil.D_Kiddion_Path + "themes.json");
    }

    /// <summary>
    /// 编辑Kiddion自定义传送
    /// </summary>
    private void EditKiddionTPClick()
    {
        ProcessUtil.Notepad2EditTextFile(FileUtil.D_Kiddion_Path + "teleports.json");
    }

    /// <summary>
    /// 编辑Kiddion自定义载具
    /// </summary>
    private void EditKiddionVCClick()
    {
        ProcessUtil.Notepad2EditTextFile(FileUtil.D_Kiddion_Path + "vehicles.json");
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

                FileUtil.DelectDir(FileUtil.D_Kiddion_Path);
                Directory.CreateDirectory(FileUtil.D_KiddionScripts_Path);
                Thread.Sleep(100);

                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "Kiddion.exe", FileUtil.D_Kiddion_Path + "Kiddion.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "KiddionChs.dll", FileUtil.D_Kiddion_Path + "KiddionChs.dll");
                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "config.json", FileUtil.D_Kiddion_Path + "config.json");
                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "themes.json", FileUtil.D_Kiddion_Path + "themes.json");
                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "teleports.json", FileUtil.D_Kiddion_Path + "teleports.json");
                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "vehicles.json", FileUtil.D_Kiddion_Path + "vehicles.json");
                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "scripts.Readme.api", FileUtil.D_KiddionScripts_Path + "Readme.api");

                NotifierHelper.Show(NotifierType.Success, "重置Kiddion配置文件成功");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
    #endregion

    #region 其他增强功能
    /// <summary>
    /// 编辑GTAHax导入文件
    /// </summary>
    private void EditGTAHaxStatClick()
    {
        ProcessUtil.Notepad2EditTextFile(FileUtil.F_GTAHaxStat_Path);
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
            if (FileUtil.IsOccupied(FileUtil.D_Inject_Path + "YimMenu.dll"))
            {
                NotifierHelper.Show(NotifierType.Warning, "请先卸载YimMenu菜单后再执行操作");
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
