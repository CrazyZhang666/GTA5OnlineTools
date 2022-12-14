using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5OnlineTools.Models;

public partial class LoadModel : ObservableObject
{
    /// <summary>
    /// 程序加载状态
    /// </summary>
    [ObservableProperty]
    private string loadState;

    /// <summary>
    /// 程序版本号
    /// </summary>
    [ObservableProperty]
    private Version versionInfo;

    /// <summary>
    /// 程序最后编译时间
    /// </summary>
    [ObservableProperty]
    private DateTime buildDate;
}
