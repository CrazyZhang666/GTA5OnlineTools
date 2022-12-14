namespace GTA5OnlineTools.Utils;

public static class MsgBoxUtil
{
    /// <summary>
    /// 通用信息弹窗，Information
    /// </summary>
    public static void Information(string content, string title = "提示")
    {
        MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    /// <summary>
    /// 通用警告弹窗，Warning
    /// </summary>
    public static void Warning(string content, string title = "警告")
    {
        MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    /// <summary>
    /// 通用错误弹窗，Error
    /// </summary>
    public static void Error(string content, string title = "错误")
    {
        MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Error);
    }

    /// <summary>
    /// 通用异常弹窗，Exception
    /// </summary>
    public static void Exception(Exception ex, string title = "异常")
    {
        MessageBox.Show("发生了未知的异常，查看异常提示以获取更多信息\n\n异常信息 : \n" + ex.Message,
            title, MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
