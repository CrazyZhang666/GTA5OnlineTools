using GTA5Core.Native;
using GTA5Shared.API;
using GTA5Shared.Helper;

namespace GTA5OnlineTools.Windows;

/// <summary> 
/// KiddionWindow.xaml 的交互逻辑
/// </summary>
public partial class KiddionWindow
{
    public KiddionWindow()
    {
        InitializeComponent();
    }

    private void Window_Kiddion_Loaded(object sender, RoutedEventArgs e)
    {

    }

    private void Window_Kiddion_Closing(object sender, CancelEventArgs e)
    {

    }

    /////////////////////////////////////////////////////

    private void ClearLogger()
    {
        TextBox_Logger.Clear();
    }

    private void AppendLogger(string log = "")
    {
        TextBox_Logger.AppendText($"{log}\n");
        TextBox_Logger.ScrollToEnd();
    }

    /// <summary>
    /// 超链接请求导航事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessHelper.OpenLink(e.Uri.OriginalString);
        e.Handled = true;
    }

    /////////////////////////////////////////////////////

    private List<string> GetKiddionUITextInfos()
    {
        var textInfos = new List<string>();

        var pArray = Process.GetProcessesByName("Kiddion");
        if (pArray.Length == 0)
            return textInfos;

        var main_handle = pArray.First().MainWindowHandle;

        var button_handle = Win32.FindWindowEx(main_handle, IntPtr.Zero, "Button", null);
        while (button_handle != IntPtr.Zero)
        {
            var length = Win32.GetWindowTextLength(button_handle);
            var builder = new StringBuilder(length + 1);
            _ = Win32.GetWindowText(button_handle, builder, builder.Capacity);

            var text = builder.ToString();
            var split = text.IndexOf("|");
            if (split != -1)
                text = text[..split];

            button_handle = Win32.FindWindowEx(main_handle, button_handle, "Button", null);
            textInfos.Add($"{text}");
        }

        return textInfos;
    }

    /// <summary>
    /// 获取Kiddion文本
    /// </summary>
    private async void Button_GetKiddionUI_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (!ProcessHelper.IsAppRun("Kiddion"))
        {
            NotifierHelper.Show(NotifierType.Warning, "未发现《Kiddion》进程，请先运行《Kiddion》程序");
            return;
        }

        ClearLogger();
        foreach (var text in GetKiddionUITextInfos())
        {
            AppendLogger($"{{ L\"{text}\", L\"中文翻译\" }},");
            await Task.Delay(1);
        }
    }
}