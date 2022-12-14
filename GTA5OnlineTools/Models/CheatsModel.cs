using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5OnlineTools.Models;

public partial class CheatsModel : ObservableObject
{
    /// <summary>
    /// Kiddion运行状态
    /// </summary>
    [ObservableProperty]
    private bool kiddionIsRun = false;

    /// <summary>
    /// GTAHax运行状态
    /// </summary>
    [ObservableProperty]
    private bool gTAHaxIsRun = false;

    /// <summary>
    /// BincoHax运行状态
    /// </summary>
    [ObservableProperty]
    private bool bincoHaxIsRun = false;

    /// <summary>
    /// LSCHax运行状态
    /// </summary>
    [ObservableProperty]
    private bool lSCHaxIsRun = false;

    /// <summary>
    /// Frame的呈现内容
    /// </summary>
    [ObservableProperty]
    private object frameContent;

    /// <summary>
    /// Frame的显示状态
    /// </summary>
    [ObservableProperty]
    private Visibility frameState = Visibility.Collapsed;

    /// <summary>
    /// 是否使用Kiddion汉化
    /// </summary>
    [ObservableProperty]
    private bool isUseKiddionChs = true;
}
