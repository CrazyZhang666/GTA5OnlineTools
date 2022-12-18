using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Helper;
using GTA5OnlineTools.Windows;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// ToolsView.xaml 的交互逻辑
/// </summary>
public partial class ToolsView : UserControl
{
    private InjectorWindow InjectorWindow = null;

    public ToolsView()
    {
        InitializeComponent();
        this.DataContext = this;
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;
    }

    private void MainWindow_WindowClosingEvent()
    {

    }

    /// <summary>
    /// 超链接请求导航事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessUtil.OpenPath(e.Uri.OriginalString);
        e.Handled = true;
    }

    /// <summary>
    /// 工具按钮点击
    /// </summary>
    /// <param name="name"></param>
    [RelayCommand]
    private void ToolsButtonClick(string name)
    {
        switch (name)
        {
            #region 分组1
            case "CurrentDirectory":
                CurrentDirectoryClick();
                break;
            case "ReleaseDirectory":
                ReleaseDirectoryClick();
                break;
            case "InitCPDPath":
                InitCPDPathClick();
                break;
            case "RestartApp":
                RestartAppClick();
                break;
            case "BaseInjector":
                BaseInjectorClick();
                break;
            case "OpenUpdateWindow":
                OpenUpdateWindowClick();
                break;
            case "RefreshDNSCache":
                RefreshDNSCacheClick();
                break;
            case "EditHosts":
                EditHostsClick();
                break;
            #endregion
            ////////////////////////////////////
            #region 分组2

            case "ReNameAppCN":
                ReNameAppCNClick();
                break;
            case "ReNameAppEN":
                ReNameAppENClick();
                break;
            case "StoryModeArchive":
                StoryModeArchiveClick();
                break;
            case "MinimizeToTray":
                MinimizeToTrayClick();
                break;
            case "ManualGC":
                ManualGCClick();
                break;
            #endregion
            ////////////////////////////////////
            default:
                break;
        }
    }

    ////////////////////////////////////////////////////////////////////////

    #region 分组1
    /// <summary>
    /// 程序当前目录
    /// </summary>
    private void CurrentDirectoryClick()
    {
        ProcessUtil.OpenPath(FileUtil.CurrentDirectory_Path);
    }

    /// <summary>
    /// 程序释放目录
    /// </summary>
    private void ReleaseDirectoryClick()
    {
        ProcessUtil.OpenPath(FileUtil.Default_Path);
    }

    /// <summary>
    /// 初始化配置文件夹
    /// </summary>
    private void InitCPDPathClick()
    {
        try
        {
            if (MessageBox.Show("你确定要初始化配置文件吗？将恢复小助手全部配置文件为默认版本，对于修复崩溃问题很有帮助\n\n" +
                "程序会自动重置此文件夹：「C:\\ProgramData\\GTA5OnlineTools\\」\n\n",
                "初始化配置文件", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ProcessUtil.CloseThirdProcess();
                Thread.Sleep(100);
                FileUtil.DelectDir(FileUtil.Default_Path);
                Thread.Sleep(100);

                App.AppMainMutex.Dispose();
                ProcessUtil.OpenPath(FileUtil.Current_Path);
                Application.Current.Shutdown();
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 重启程序
    /// </summary>
    private void RestartAppClick()
    {
        ProcessUtil.CloseThirdProcess();
        App.AppMainMutex.Dispose();
        ProcessUtil.OpenPath(FileUtil.Current_Path);
        Application.Current.Shutdown();
    }

    /// <summary>
    /// 基础DLL注入器
    /// </summary>
    private void BaseInjectorClick()
    {
        if (InjectorWindow == null)
        {
            InjectorWindow = new InjectorWindow();
            InjectorWindow.Show();
        }
        else
        {
            if (InjectorWindow.IsVisible)
            {
                InjectorWindow.Topmost = true;
                InjectorWindow.Topmost = false;
                InjectorWindow.WindowState = WindowState.Normal;
            }
            else
            {
                InjectorWindow = null;
                InjectorWindow = new InjectorWindow();
                InjectorWindow.Show();
            }
        }
    }

    /// <summary>
    /// 打开更新窗口
    /// </summary>
    private void OpenUpdateWindowClick()
    {
        var UpdateWindow = new UpdateWindow
        {
            Owner = MainWindow.MainWindowInstance
        };
        UpdateWindow.ShowDialog();
    }

    /// <summary>
    /// 刷新DNS缓存
    /// </summary>
    private void RefreshDNSCacheClick()
    {
        CoreUtil.FlushDNSCache();
        MainWindow.ActionShowNoticeInfo("成功刷新DNS解析程序缓存");
    }

    /// <summary>
    /// 编辑Hosts文件
    /// </summary>
    private void EditHostsClick()
    {
        ProcessUtil.Notepad2EditTextFile(@"C:\windows\system32\drivers\etc\hosts");
    }
    #endregion

    ////////////////////////////////////////////////////////////////////////

    #region 分组2
    /// <summary>
    /// 重命名小助手为中文
    /// </summary>
    private void ReNameAppCNClick()
    {
        try
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileUtil.Current_Path);
            if (fileNameWithoutExtension != (CoreUtil.MainAppWindowName + CoreUtil.ClientVersion))
            {
                FileUtil.FileReName(FileUtil.Current_Path, FileUtil.GetCurrFullPath(CoreUtil.MainAppWindowName + CoreUtil.ClientVersion + ".exe"));

                ProcessUtil.CloseThirdProcess();
                App.AppMainMutex.Dispose();
                ProcessUtil.OpenPath(FileUtil.GetCurrFullPath(CoreUtil.MainAppWindowName + CoreUtil.ClientVersion + ".exe"));
                Application.Current.Shutdown();
            }
            else
            {
                NotifierHelper.Show(NotifierType.Notification, "程序文件名已经符合中文命名标准，无需继续重命名");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 重命名小助手为英文
    /// </summary>
    private void ReNameAppENClick()
    {
        try
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileUtil.Current_Path);
            if (fileNameWithoutExtension != "GTA5OnlineTools")
            {
                FileUtil.FileReName(FileUtil.Current_Path, "GTA5OnlineTools.exe");

                ProcessUtil.CloseThirdProcess();
                App.AppMainMutex.Dispose();
                ProcessUtil.OpenPath(FileUtil.GetCurrFullPath("GTA5OnlineTools.exe"));
                Application.Current.Shutdown();
            }
            else
            {
                NotifierHelper.Show(NotifierType.Notification, "程序文件名已经符合英文命名标准，无需继续重命名");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 故事模式完美存档
    /// </summary>
    private void StoryModeArchiveClick()
    {
        var path = Path.Combine(FileUtil.MyDocuments_Path, @"Rockstar Games\GTA V\Profiles");
        if (!Directory.Exists(path))
        {
            NotifierHelper.Show(NotifierType.Error, "GTA5故事模式存档路径不存在，操作取消");
            return;
        }

        if (MessageBox.Show("你确定替换GTA5故事模式存档吗？将替换GTA5正版故事模式默认存档（存档进度：100%）", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        {
            try
            {
                var dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    var dirIf = new DirectoryInfo(dir);
                    string fullName = Path.Combine(dirIf.FullName, "SGTA50000");
                    FileUtil.ExtractResFile(FileUtil.Resource_Path + "Other.SGTA50000", fullName);
                }

                NotifierHelper.Show(NotifierType.Success, $"GTA5故事模式存档替换成功\n{path}");
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowException(ex);
            }
        }
    }

    /// <summary>
    /// 最小化程序到系统托盘
    /// </summary>
    private void MinimizeToTrayClick()
    {
        MainWindow.MainWindowInstance.WindowState = WindowState.Minimized;
        MainWindow.MainWindowInstance.ShowInTaskbar = false;
        MainWindow.ActionShowNoticeInfo("程序已最小化到托盘");
    }

    /// <summary>
    /// GC垃圾回收
    /// </summary>
    private void ManualGCClick()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        NotifierHelper.Show(NotifierType.Notification, "执行GC垃圾回收成功");
    }
    #endregion
}
