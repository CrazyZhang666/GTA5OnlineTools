using GTA5Shared.Helper;

namespace GTA5OnlineTools.Views;

/// <summary>
/// HomeView.xaml 的交互逻辑
/// </summary>
public partial class HomeView : UserControl
{
    private readonly StringBuilder builder = new();

    public HomeView()
    {
        InitializeComponent();
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        builder = new StringBuilder();
        builder.AppendLine("网络异常，获取最新公告内容失败！这并不影响小助手程序使用");
        builder.AppendLine("建议你定期去小助手网站查看是否有最新版本：https://crazyzhang.cn/\n");
        builder.AppendLine("【程序开源】");
        builder.AppendLine("----------------");
        builder.AppendLine("小助手是开源的，完全无毒无害，大家可以自行下载源码审查");
        builder.AppendLine("https://github.com/CrazyZhang666/GTA5OnlineTools\n");
        builder.AppendLine("【最新版本】");
        builder.AppendLine("----------------");
        builder.AppendLine("> 蓝奏云盘：https://crazyzhang.lanzouh.com/b04md71ve");
        builder.AppendLine("> 123云盘：https://www.123pan.com/s/QEorVv-Bfzv3\n");
        builder.AppendLine("强烈建议大家使用最新版本以获取bug修复和安全性更新");

        GetNoticeInfo();
        GetChangeInfo();
    }

    private void MainWindow_WindowClosingEvent()
    {

    }

    private async void GetNoticeInfo()
    {
        try
        {
            var notice = await HttpHelper.DownloadString("https://api.crazyzhang.cn/update/server/notice.txt");

            if (string.IsNullOrEmpty(notice))
                TextBox_Notice.Text = builder.ToString();
            else
                TextBox_Notice.Text = notice;
        }
        catch (Exception ex)
        {
            TextBox_Notice.Text = ex.Message;
        }
    }

    private async void GetChangeInfo()
    {
        try
        {
            var change = await HttpHelper.DownloadString("https://api.crazyzhang.cn/update/server/change.txt");

            if (string.IsNullOrEmpty(change))
                TextBox_Change.Text = builder.ToString();
            else
            {
                change = "2024/01/23  v3.1.8.9  \n-优化小助手菜单处支持拖动调节宽度\t优化" + change;
                TextBox_Change.Text = change;
            }
        }
        catch (Exception ex)
        {
            TextBox_Change.Text = ex.Message;
        }
    }
}
