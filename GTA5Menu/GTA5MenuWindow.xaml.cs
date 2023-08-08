using GTA5HotKey;
using GTA5Core.Native;
using GTA5Shared.Helper;

using CommunityToolkit.Mvvm.Input;

namespace GTA5Menu;

/// <summary>
/// GTA5MenuWindow.xaml 的交互逻辑
/// </summary>
public partial class GTA5MenuWindow
{
    /// <summary>
    /// 导航字典
    /// </summary>
    private readonly Dictionary<string, UserControl> NavDictionary = new();

    ///////////////////////////////////////////

    /// <summary>
    /// 主窗口关闭事件
    /// </summary>
    public static event Action WindowClosingEvent;

    /// <summary>
    /// 周期性调用事件 普通速度
    /// </summary>
    public static event Action LoopSpeedNormalEvent;
    /// <summary>
    /// 周期性调用事件 较快速度
    /// </summary>
    public static event Action LoopSpeedFastEvent;

    ///////////////////////////////////////////

    /// <summary>
    /// 窗口句柄
    /// </summary>
    private IntPtr ThisWindowHandle = IntPtr.Zero;

    /// <summary>
    /// 窗口鼠标坐标数据
    /// </summary>
    private POINT ThisWinPOINT;

    /// <summary>
    /// 是否显示外置窗口菜单
    /// </summary>
    private bool IsShowExternalMenu = true;

    /// <summary>
    /// 判断程序是否在运行，用于结束线程
    /// </summary>
    private bool IsAppRunning = true;

    /// <summary>
    /// 显示隐藏菜单按键是否使用Del键
    /// </summary>
    private bool IsUseDelKeyShowMenu = false;

    ///////////////////////////////////////////

    public GTA5MenuWindow()
    {
        InitializeComponent();

        CreateView();

        ///////////  读取配置文件  ///////////

        ReadConfig();
    }

    private void Window_GTA5Menu_Loaded(object sender, RoutedEventArgs e)
    {
        Navigate(NavDictionary.First().Key);

        // 获取当前窗口句柄
        ThisWindowHandle = new WindowInteropHelper(this).Handle;

        // 添加快捷键
        HotKeys.AddKey(Keys.Delete);
        HotKeys.AddKey(Keys.Oem3);
        // 订阅按钮事件
        HotKeys.KeyDownEvent += HotKeys_KeyDownEvent;

        new Thread(LoopSpeedNormalThread)
        {
            Name = "LoopSpeedNormalThread",
            IsBackground = true
        }.Start();

        new Thread(LoopSpeedFastThread)
        {
            Name = "LoopSpeedFastThread",
            IsBackground = true
        }.Start();
    }

    private void Window_GTA5Menu_Closing(object sender, CancelEventArgs e)
    {
        IsAppRunning = false;
        WindowClosingEvent?.Invoke();

        // 保存配置文件
        SaveConfig();

        // 移除快捷键
        HotKeys.RemoveKey(Keys.Delete);
        HotKeys.RemoveKey(Keys.Oem3);
        // 取消订阅按钮事件（2023/06/24 这里一定要取消订阅，否则会照成事件累加）
        HotKeys.KeyDownEvent -= HotKeys_KeyDownEvent;
    }

    /////////////////////////////////////////////////

    /// <summary>
    /// 读取配置文件
    /// </summary>
    private void ReadConfig()
    {
        var isUseDelKey = IniHelper.ReadValue("GTA5Menu", "IsUseDelKeyShowMenu").Equals("True", StringComparison.OrdinalIgnoreCase);
        var isTopMost = IniHelper.ReadValue("GTA5Menu", "IsTopMostMenu").Equals("True", StringComparison.OrdinalIgnoreCase);
        var isShowInTaskbar = IniHelper.ReadValue("GTA5Menu", "IsShowInTaskbarMenu").Equals("True", StringComparison.OrdinalIgnoreCase);

        RadioButton_ShowMenuKey_Oem3.IsChecked = !isUseDelKey;
        RadioButton_ShowMenuKey_Del.IsChecked = isUseDelKey;

        CheckBox_IsTopMost.IsChecked = isTopMost;
        CheckBox_IsShowInTaskbar.IsChecked = isShowInTaskbar;

        this.Topmost = isTopMost;
        this.ShowInTaskbar = isShowInTaskbar;

        this.IsUseDelKeyShowMenu = RadioButton_ShowMenuKey_Del.IsChecked == true;
    }

    /// <summary>
    /// 保存配置文件
    /// </summary>
    private void SaveConfig()
    {
        IniHelper.WriteValue("GTA5Menu", "IsUseDelKeyShowMenu", $"{RadioButton_ShowMenuKey_Del.IsChecked == true}");

        IniHelper.WriteValue("GTA5Menu", "IsTopMostMenu", $"{CheckBox_IsTopMost.IsChecked == true}");
        IniHelper.WriteValue("GTA5Menu", "IsShowInTaskbarMenu", $"{CheckBox_IsShowInTaskbar.IsChecked == true}");
    }

    /// <summary>
    /// 创建页面
    /// </summary>
    private void CreateView()
    {
        foreach (var item in ControlHelper.GetControls(Grid_NavMenu).Cast<RadioButton>())
        {
            var viewName = item.CommandParameter.ToString();

            if (NavDictionary.ContainsKey(viewName))
                continue;

            var typeView = Type.GetType($"GTA5Menu.Views.{viewName}");
            if (typeView == null)
                continue;

            NavDictionary.Add(viewName, Activator.CreateInstance(typeView) as UserControl);
        }
    }

    /// <summary>
    /// View页面导航
    /// </summary>
    /// <param name="viewName"></param>
    [RelayCommand]
    private void Navigate(string viewName)
    {
        if (!NavDictionary.ContainsKey(viewName))
            return;

        if (ContentControl_NavRegion.Content == NavDictionary[viewName])
            return;

        ContentControl_NavRegion.Content = NavDictionary[viewName];
    }

    /// <summary>
    /// 窗口是否置顶
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckBox_IsTopMost_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_IsTopMost.IsChecked == true)
            this.Topmost = true;
        else
            this.Topmost = false;
    }

    /// <summary>
    /// 窗口是否隐藏任务栏图标
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckBox_IsShowInTaskbar_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_IsShowInTaskbar.IsChecked == true)
            this.ShowInTaskbar = false;
        else
            this.ShowInTaskbar = true;
    }

    /// <summary>
    /// 按键按下事件
    /// </summary>
    /// <param name="key"></param>
    private void HotKeys_KeyDownEvent(Keys key)
    {
        switch (key)
        {
            case Keys.Oem3:
                if (!IsUseDelKeyShowMenu)
                    ShowWindow();
                break;
            case Keys.Delete:
                if (IsUseDelKeyShowMenu)
                    ShowWindow();
                break;
        }
    }

    /// <summary>
    /// 显示隐藏外置菜单窗口
    /// </summary>
    private void ShowWindow()
    {
        this.Dispatcher.Invoke(() =>
        {
            IsShowExternalMenu = !IsShowExternalMenu;

            if (IsShowExternalMenu)
            {
                var thisWindowThreadId = Win32.GetWindowThreadProcessId(ThisWindowHandle, IntPtr.Zero);
                var currentForegroundWindow = Win32.GetForegroundWindow();
                var currentForegroundWindowThreadId = Win32.GetWindowThreadProcessId(currentForegroundWindow, IntPtr.Zero);

                Win32.AttachThreadInput(currentForegroundWindowThreadId, thisWindowThreadId, true);

                this.Show();
                this.Activate();
                this.Focus();

                this.WindowState = WindowState.Normal;

                Win32.AttachThreadInput(currentForegroundWindowThreadId, thisWindowThreadId, false);

                this.Topmost = true;
                this.Topmost = false;

                if (CheckBox_IsTopMost.IsChecked == true)
                    this.Topmost = true;

                Win32.SetForegroundWindow(ThisWindowHandle);
                Win32.SetCursorPos(ThisWinPOINT.X, ThisWinPOINT.Y);
            }
            else
            {
                this.Hide();

                Win32.GetCursorPos(out ThisWinPOINT);
                Memory.SetForegroundWindow();
            }
        });
    }

    private void LoopSpeedNormalThread()
    {
        while (IsAppRunning)
        {
            LoopSpeedNormalEvent?.Invoke();

            Thread.Sleep(1000);
        }
    }

    private void LoopSpeedFastThread()
    {
        while (IsAppRunning)
        {
            LoopSpeedFastEvent?.Invoke();

            Thread.Sleep(500);
        }
    }

    private void RadioButton_ShowMenuKey_Click(object sender, RoutedEventArgs e)
    {
        this.IsUseDelKeyShowMenu = RadioButton_ShowMenuKey_Del.IsChecked == true;
    }
}
