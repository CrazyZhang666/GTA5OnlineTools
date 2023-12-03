using GTA5MenuExtra.Views.SpeedMeter;

using GTA5Core.Native;
using GTA5Shared.Helper;

namespace GTA5MenuExtra;

/// <summary>
/// SpeedMeterWindow.xaml 的交互逻辑
/// </summary>
public partial class SpeedMeterWindow
{
    private DrawWindow DrawWindow = null;

    public SpeedMeterWindow()
    {
        InitializeComponent();
        DrawData.IsShowMPH = true;
    }

    private void Window_SpeedMeter_Loaded(object sender, RoutedEventArgs e)
    {
        Task.Run(() =>
        {
            this.Dispatcher.Invoke(() =>
            {
                var windowData = Memory.GetGameWindowData();

                TextBlock_ScreenResolution.Text = $"屏幕分辨率 {SystemParameters.PrimaryScreenWidth} x {SystemParameters.PrimaryScreenHeight}";
                TextBlock_GameResolution.Text = $"游戏分辨率 {windowData.Width} x {windowData.Height}";
                TextBlock_ScreenScale.Text = $"缩放比例 {ScreenMgr.GetScalingRatio()}";
            });
        });
    }

    private void Window_SpeedMeter_Closing(object sender, CancelEventArgs e)
    {
        if (DrawWindow != null)
        {
            DrawWindow.Close();
            DrawWindow = null;
        }
    }

    private void Button_RunDraw_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Memory.SetForegroundWindow();

        if (DrawWindow == null)
        {
            DrawWindow = new DrawWindow();
            DrawWindow.Show();
        }

        var windowData = Memory.GetGameWindowData();

        TextBlock_ScreenResolution.Text = $"屏幕分辨率 {SystemParameters.PrimaryScreenWidth} x {SystemParameters.PrimaryScreenHeight}";
        TextBlock_GameResolution.Text = $"游戏分辨率 {windowData.Width} x {windowData.Height}";
        TextBlock_ScreenScale.Text = $"缩放比例 {ScreenMgr.GetScalingRatio()}";
    }

    private void Button_StopDraw_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (DrawWindow != null)
        {
            DrawWindow.Close();
            DrawWindow = null;
        }

        var windowData = Memory.GetGameWindowData();

        TextBlock_ScreenResolution.Text = $"屏幕分辨率 {SystemParameters.PrimaryScreenWidth} x {SystemParameters.PrimaryScreenHeight}";
        TextBlock_GameResolution.Text = $"游戏分辨率 {windowData.Width} x {windowData.Height}";
        TextBlock_ScreenScale.Text = $"缩放比例 {ScreenMgr.GetScalingRatio()}";
    }

    private void RadioButton_SpeedMeterPos_Center_Click(object sender, RoutedEventArgs e)
    {
        DrawData.IsDrawCenter = RadioButton_SpeedMeterPos_Center.IsChecked == true;
    }

    private void RadioButton_SpeedMeterUnit_MPH_Click(object sender, RoutedEventArgs e)
    {
        DrawData.IsShowMPH = RadioButton_SpeedMeterUnit_MPH.IsChecked == true;
    }

    private void RadioButton_SpeedMeterUnit_KPH_Click(object sender, RoutedEventArgs e)
    {
        DrawData.IsShowMPH   = RadioButton_SpeedMeterUnit_MPH.IsChecked == true;
    }
}
