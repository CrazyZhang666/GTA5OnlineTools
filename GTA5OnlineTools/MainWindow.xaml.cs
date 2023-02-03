using GTA5OnlineTools.Data;
using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Views;
using GTA5OnlineTools.Models;
using GTA5OnlineTools.Helper;
using GTA5OnlineTools.Windows;
using GTA5OnlineTools.GTA.Core;

using RestSharp;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace GTA5OnlineTools;

/// <summary>
/// MainWindow.xaml 的交互逻辑
/// </summary>
public partial class MainWindow
{
    /// <summary>
    /// 主窗口数据模型
    /// </summary>
    public MainModel MainModel { get; set; } = new();

    ///////////////////////////////////////////////////////////////

    private readonly HomeView HomeView = new();
    private readonly HacksView CheatsView = new();
    private readonly ModulesView ModulesView = new();
    private readonly ToolsView ToolsView = new();
    private readonly AboutView AboutView = new();

    ///////////////////////////////////////////////////////////////

    /// <summary>
    /// 主程序是否在运行，用于结束线程内循环
    /// </summary>
    public static bool IsAppRunning = true;

    /// <summary>
    /// 主窗口关闭委托
    /// </summary>
    public delegate void WindowClosingDelegate();
    /// <summary>
    /// 主窗口关闭事件
    /// </summary>
    public static event WindowClosingDelegate WindowClosingEvent;

    /// <summary>
    /// 用于向外暴露主窗口实例
    /// </summary>
    public static Window MainWindowInstance { get; private set; } = null;

    /// <summary>
    /// 存储软件开始运行的时间
    /// </summary>
    private DateTime OriginDateTime;

    ///////////////////////////////////////////////////////////////

    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 窗口加载完成事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Main_Loaded(object sender, RoutedEventArgs e)
    {
        // 设置当前上下文数据
        this.DataContext = this;
        // 向外暴露主窗口实例
        MainWindowInstance = this;

        // 首页导航
        Navigate("HomeView");

        // 获取当前时间，存储到对于变量中
        OriginDateTime = DateTime.Now;

        ///////////////////////////////////////////////////////////////

        // 设置主窗口标题
        this.Title = CoreUtil.MainAppWindowName + CoreUtil.ClientVersion;

        MainModel.AppRunTime = "00:00:00";
        MainModel.AppVersion = $"{CoreUtil.ClientVersion}";

        // 更新主窗口UI线程
        new Thread(UpdateUiThread)
        {
            Name = "UpdateUiThread",
            IsBackground = true
        }.Start();

        // 检查GTA5是否运行线程
        new Thread(CheckGTA5IsRunThread)
        {
            Name = "CheckGTA5IsRunThread",
            IsBackground = true
        }.Start();

        // 检查更新线程
        new Thread(CheckUpdateThread)
        {
            Name = "CheckUpdateThread",
            IsBackground = true
        }.Start();
    }

    /// <summary>
    /// 窗口关闭事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Main_Closing(object sender, CancelEventArgs e)
    {
        // 终止线程内循环
        IsAppRunning = false;

        WindowClosingEvent();
        LoggerHelper.Info("调用主窗口关闭事件成功");

        Memory.CloseHandle();
        LoggerHelper.Info("释放内存模块进程句柄成功");

        ModulesView.ActionCloseAllModulesWindow();
        LoggerHelper.Info("关闭小助手功能窗口成功");

        ProcessUtil.CloseThirdProcess();
        LoggerHelper.Info("关闭第三方进程成功");

        Application.Current.Shutdown();
        LoggerHelper.Info("主程序关闭\n\n");
    }

    ///////////////////////////////////////////////////////////////

    /// <summary>
    /// View页面导航
    /// </summary>
    /// <param name="viewName"></param>
    [RelayCommand]
    private void Navigate(string viewName)
    {
        switch (viewName)
        {
            case "HomeView":
                if (ContentControl_Main.Content != HomeView)
                    ContentControl_Main.Content = HomeView;
                break;
            case "CheatsView":
                if (ContentControl_Main.Content != CheatsView)
                    ContentControl_Main.Content = CheatsView;
                break;
            case "ModulesView":
                if (ContentControl_Main.Content != ModulesView)
                    ContentControl_Main.Content = ModulesView;
                break;
            case "ToolsView":
                if (ContentControl_Main.Content != ToolsView)
                    ContentControl_Main.Content = ToolsView;
                break;
            case "AboutView":
                if (ContentControl_Main.Content != AboutView)
                    ContentControl_Main.Content = AboutView;
                break;
        }
    }

    /// <summary>
    /// 更新主窗口UI线程
    /// </summary>
    private void UpdateUiThread()
    {
        while (IsAppRunning)
        {
            // 获取软件运行时间
            MainModel.AppRunTime = CoreUtil.ExecDateDiff(OriginDateTime, DateTime.Now);

            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// 检查GTA5是否运行线程
    /// </summary>
    private void CheckGTA5IsRunThread()
    {
        bool isExecute = true;

        while (IsAppRunning)
        {
            // 判断 GTA5 是否运行
            MainModel.IsGTA5Run = ProcessUtil.IsGTA5Run();

            if (MainModel.IsGTA5Run)
            {
                isExecute = false;

                if (Memory.GTA5ProHandle == IntPtr.Zero)
                {
                    if (Memory.Initialize())
                    {
                        Memory.PatternInit();
                    }
                }
                else
                {
                    Memory.PatternInit();
                }
            }
            else
            {
                // 下列功能只会在GTA5停止运行时执行一次，直到下一次GTA5停止运行
                if (!isExecute)
                {
                    isExecute = true;

                    Memory.CloseHandle();
                    ModulesView.ActionCloseAllModulesWindow();
                }
            }

            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// 检查更新线程
    /// </summary>
    private async void CheckUpdateThread()
    {
        try
        {
            // 刷新DNS缓存
            CoreUtil.FlushDNSCache();
            LoggerHelper.Info("刷新DNS缓存成功");

            LoggerHelper.Info("正在检测版本更新...");
            this.Dispatcher.Invoke(() =>
            {
                NotifierHelper.Show(NotifierType.Notification, "正在检测版本更新...");
            });

            // 检测版本更新
            var options = new RestClientOptions("https://api.crazyzhang.cn")
            {
                MaxTimeout = 20000,
                FollowRedirects = true
            };
            var client = new RestClient(options);

            var request = new RestRequest("/update/config.json");
            var response = await client.ExecuteGetAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // 解析web返回的数据
                CoreUtil.UpdateInfo = JsonUtil.JsonDese<UpdateInfo>(response.Content);
                // 获取对应数据
                CoreUtil.ServerVersion = Version.Parse(CoreUtil.UpdateInfo.Version);

                // 获取最新公告
                request = new RestRequest("/update/server/notice.txt");
                response = await client.ExecuteGetAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                    WeakReferenceMessenger.Default.Send(response.Content, "Notice");
                else
                    WeakReferenceMessenger.Default.Send("404", "Notice");

                // 获取更新日志
                request = new RestRequest("/update/server/change.txt");
                response = await client.ExecuteGetAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                    WeakReferenceMessenger.Default.Send(response.Content, "Change");
                else
                    WeakReferenceMessenger.Default.Send("404", "Change");

                // 如果线上版本号大于本地版本号，则提示更新
                if (CoreUtil.ServerVersion > CoreUtil.ClientVersion)
                {
                    // 打开更新对话框
                    this.Dispatcher.Invoke(() =>
                    {
                        var UpdateWindow = new UpdateWindow
                        {
                            // 设置父窗口
                            Owner = this
                        };
                        // 以对话框形式显示更新窗口
                        UpdateWindow.ShowDialog();
                    });
                }
                else
                {
                    LoggerHelper.Info($"当前已是最新版本 {CoreUtil.ServerVersion}");
                    this.Dispatcher.Invoke(() =>
                    {
                        NotifierHelper.Show(NotifierType.Notification, $"当前已是最新版本 {CoreUtil.ServerVersion}");
                    });
                }
            }
            else
            {
                LoggerHelper.Error("网络异常");
                this.Dispatcher.Invoke(() =>
                {
                    NotifierHelper.Show(NotifierType.Error, "网络异常，这并不影响小助手程序使用");
                });

                WeakReferenceMessenger.Default.Send("404", "Notice");
                WeakReferenceMessenger.Default.Send("404", "Change");
            }
        }
        catch (Exception ex)
        {
            LoggerHelper.Error($"初始化错误", ex);
            this.Dispatcher.Invoke(() =>
            {
                NotifierHelper.Show(NotifierType.Error, $"初始化错误\n{ex.Message}");
            });

            WeakReferenceMessenger.Default.Send("404", "Notice");
            WeakReferenceMessenger.Default.Send("404", "Change");
        }
    }
}
