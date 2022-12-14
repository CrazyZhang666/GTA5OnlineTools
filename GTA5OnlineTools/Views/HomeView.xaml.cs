using CommunityToolkit.Mvvm.Messaging;

namespace GTA5OnlineTools.Views;

/// <summary>
/// HomeView.xaml 的交互逻辑
/// </summary>
public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();

        string content = "网络异常，获取最新公告内容失败！这并不影响小助手程序使用\n\n" +
            "建议你定期去小助手网址查看是否有最新版本：https://crazyzhang.cn/\n\n" +
            "强烈建议大家使用最新版本以获取bug修复和安全性更新";

        WeakReferenceMessenger.Default.Register<string, string>(this, "Notice", (s, e) =>
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                TextBox_Notice.Text = e == "404" ? content : e;
            });
        });

        WeakReferenceMessenger.Default.Register<string, string>(this, "Change", (s, e) =>
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                TextBox_Change.Text = e == "404" ? content : e;
            });
        });
    }
}
