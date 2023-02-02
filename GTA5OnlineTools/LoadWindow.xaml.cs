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
    /// Load 数据模型绑定
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
                Directory.CreateDirectory(FileUtil.Dir_Cache);
                Directory.CreateDirectory(FileUtil.Dir_Config);
                Directory.CreateDirectory(FileUtil.Dir_Kiddion);
                Directory.CreateDirectory(FileUtil.Dir_Kiddion_Scripts);
                Directory.CreateDirectory(FileUtil.Dir_Inject);
                Directory.CreateDirectory(FileUtil.Dir_Log);

                // 清空缓存文件夹
                FileUtil.DelectDir(FileUtil.Dir_Cache);

                // 释放必要文件
                FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Kiddion, FileUtil.File_Kiddion_Kiddion);
                FileUtil.ExtractResFile(FileUtil.Res_Kiddion_KiddionChs, FileUtil.File_Kiddion_KiddionChs);

                // 释放前先判断，防止覆盖配置文件
                if (!File.Exists(FileUtil.File_Kiddion_Config))
                    FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Config, FileUtil.File_Kiddion_Config);
                if (!File.Exists(FileUtil.File_Kiddion_Themes))
                    FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Themes, FileUtil.File_Kiddion_Themes);
                if (!File.Exists(FileUtil.File_Kiddion_Teleports))
                    FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Teleports, FileUtil.File_Kiddion_Teleports);
                if (!File.Exists(FileUtil.File_Kiddion_Vehicles))
                    FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Vehicles, FileUtil.File_Kiddion_Vehicles);

                // Kiddion Lua脚本
                FileUtil.ExtractResFile(FileUtil.Res_Kiddion_Scripts_Readme, FileUtil.File_Kiddion_Scripts_Readme);

                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                FileUtil.ExtractResFile(FileUtil.Res_Cache_GTAHax, FileUtil.File_Cache_GTAHax);
                FileUtil.ExtractResFile(FileUtil.Res_Cache_BincoHax, FileUtil.File_Cache_BincoHax);
                FileUtil.ExtractResFile(FileUtil.Res_Cache_LSCHax, FileUtil.File_Cache_LSCHax);
                FileUtil.ExtractResFile(FileUtil.Res_Cache_Stat, FileUtil.File_Cache_Stat);

                FileUtil.ExtractResFile(FileUtil.Res_Cache_Notepad2, FileUtil.File_Cache_Notepad2);

                // 判断DLL文件是否存在以及是否被占用
                if (!File.Exists(FileUtil.File_Inject_YimMenu))
                {
                    FileUtil.ExtractResFile(FileUtil.Res_Inject_YimMenu, FileUtil.File_Inject_YimMenu);
                }
                else
                {
                    if (!FileUtil.IsOccupied(FileUtil.File_Inject_YimMenu))
                        FileUtil.ExtractResFile(FileUtil.Res_Inject_YimMenu, FileUtil.File_Inject_YimMenu);
                }

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
                        FileUtil.DelectDir(FileUtil.Default);
                        Thread.Sleep(100);

                        App.AppMainMutex.Dispose();
                        ProcessUtil.OpenPath(FileUtil.Current_Path);
                        Application.Current.Shutdown();
                    }
                    break;
                case "OpenDefaultPath":
                    ProcessUtil.OpenPath(FileUtil.Default);
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
