using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Models;
using GTA5OnlineTools.Helper;

using Chinese;
using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools;

/// <summary>
/// LoadWindow.xaml 的交互逻辑
/// </summary>
public partial class LoadWindow
{
    /// <summary>
    /// Load数据模型
    /// </summary>
    public LoadModel LoadModel { get; set; } = new();

    public LoadWindow()
    {
        InitializeComponent();
    }

    private void Window_Load_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        Task.Run(() =>
        {
            try
            {
                // 关闭第三方进程
                ProcessUtil.CloseThirdProcess();

                LoadModel.LoadState = "正在初始化工具...";

                LoggerHelper.Info("开始初始化程序...");
                LoggerHelper.Info($"当前程序版本号 {CoreUtil.ClientVersion}");
                LoggerHelper.Info($"当前程序最后编译时间 {CoreUtil.BuildTime}");

                // 客户端程序版本号
                LoadModel.VersionInfo = CoreUtil.ClientVersion;
                // 最后编译时间
                LoadModel.BuildDate = CoreUtil.BuildTime;

                /////////////////////////////////////////////////////////////////////

                LoadModel.LoadState = "正在初始化配置文件...";
                LoggerHelper.Info("正在初始化配置文件...");

                // 创建指定文件夹，用于释放必要文件和更新软件（如果已存在则不会创建）
                Directory.CreateDirectory(FileUtil.D_Cache_Path);
                Directory.CreateDirectory(FileUtil.D_Config_Path);
                Directory.CreateDirectory(FileUtil.D_Kiddion_Path);
                Directory.CreateDirectory(FileUtil.D_KiddionScripts_Path);
                Directory.CreateDirectory(FileUtil.D_Log_Path);

                // 清空缓存文件夹
                FileUtil.DelectDir(FileUtil.D_Cache_Path);

                // 释放必要文件
                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "Kiddion.exe", FileUtil.D_Kiddion_Path + "Kiddion.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "KiddionChs.dll", FileUtil.D_Kiddion_Path + "KiddionChs.dll");

                // 释放前先判断，防止覆盖配置文件
                if (!File.Exists(FileUtil.D_Kiddion_Path + "config.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "config.json", FileUtil.D_Kiddion_Path + "config.json");
                if (!File.Exists(FileUtil.D_Kiddion_Path + "themes.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "themes.json", FileUtil.D_Kiddion_Path + "themes.json");
                if (!File.Exists(FileUtil.D_Kiddion_Path + "teleports.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "teleports.json", FileUtil.D_Kiddion_Path + "teleports.json");
                if (!File.Exists(FileUtil.D_Kiddion_Path + "vehicles.json"))
                    FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "vehicles.json", FileUtil.D_Kiddion_Path + "vehicles.json");

                // Kiddion Lua脚本
                FileUtil.ExtractResFile(FileUtil.Resource_Kiddion_Path + "scripts.Readme.api", FileUtil.D_KiddionScripts_Path + "Readme.api");

                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                FileUtil.ExtractResFile(FileUtil.Resource_Path + "GTAHax.exe", FileUtil.D_Cache_Path + "GTAHax.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "stat.txt", FileUtil.D_Cache_Path + "stat.txt");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "BincoHax.exe", FileUtil.D_Cache_Path + "BincoHax.exe");
                FileUtil.ExtractResFile(FileUtil.Resource_Path + "LSCHax.exe", FileUtil.D_Cache_Path + "LSCHax.exe");

                FileUtil.ExtractResFile(FileUtil.Resource_Path + "Notepad2.exe", FileUtil.D_Cache_Path + "Notepad2.exe");

                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                // 提前预加载转换字库
                ChineseConverter.ToTraditional("免费，跨平台，开源！");
                LoggerHelper.Info("简繁翻译库初始化成功");

                this.Dispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow();
                    // 转移主程序控制权
                    Application.Current.MainWindow = mainWindow;
                    // 显示主窗口
                    mainWindow.Show();
                    // 关闭初始化窗口
                    this.Close();
                });
            }
            catch (Exception ex)
            {
                LoadModel.LoadState = $"初始化错误，发生了未知异常！\n\n{ex.Message}\n\n提示：请关闭杀毒软件并尝试清空默认配置文件夹全部文件后重试";
                LoggerHelper.Error("初始化错误，发生了未知异常", ex);

                this.Dispatcher.Invoke(() =>
                {
                    WrapPanel_ExceptionState.Visibility = Visibility.Visible;
                });
            }
        });
    }

    [RelayCommand]
    private void ButtonClick(string name)
    {
        try
        {
            switch (name)
            {
                case "InitDefaultPath":
                    {
                        FileUtil.DelectDir(FileUtil.Default_Path);
                        Thread.Sleep(100);

                        App.AppMainMutex.Dispose();
                        ProcessUtil.OpenPath(FileUtil.Current_Path);
                        Application.Current.Shutdown();
                    }
                    break;
                case "OpenDefaultPath":
                    ProcessUtil.OpenPath(FileUtil.Default_Path);
                    break;
                case "ExitAPP":
                    Application.Current.Shutdown();
                    break;
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
}
