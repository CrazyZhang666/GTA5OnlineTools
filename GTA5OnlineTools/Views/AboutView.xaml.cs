using GTA5OnlineTools.Utils;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Views;

/// <summary>
/// AboutView.xaml 的交互逻辑
/// </summary>
public partial class AboutView : UserControl
{
    public AboutView()
    {
        InitializeComponent();
        this.DataContext = this;
    }

    [RelayCommand]
    private void HyperlinkClick(string url)
    {
        ProcessUtil.OpenPath(url);
    }
}
