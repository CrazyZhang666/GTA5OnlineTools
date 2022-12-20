using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5OnlineTools.Models;

public partial class CasinoHackModel : ObservableObject
{
    /// <summary>
    /// 黑杰克 是否 开启预测
    /// </summary>
    [ObservableProperty]
    private bool blackjackIsCheck;

    /// <summary>
    /// 三张扑克 是否 开启预测
    /// </summary>
    [ObservableProperty]
    private bool pokerIsCheck;

    /// <summary>
    /// 轮盘赌 是否 操控中奖
    /// </summary>
    [ObservableProperty]
    private bool rouletteIsCheck;

    /// <summary>
    /// 老虎机 是否 操控中奖
    /// </summary>
    [ObservableProperty]
    private bool slotMachineIsCheck;

    /// <summary>
    /// 幸运轮盘 是否 操控中奖
    /// </summary>
    [ObservableProperty]
    private bool luckyWheelIsCheck;

    /////////////////////////////////////////////////////////

    /// <summary>
    /// 轮盘赌 选中索引
    /// </summary>
    [ObservableProperty]
    private int rouletteSelectedIndex;

    /// <summary>
    /// 老虎机 选中索引
    /// </summary>
    [ObservableProperty]
    private int slotMachineSelectedIndex;

    /// <summary>
    /// 幸运轮盘 选中索引
    /// </summary>
    [ObservableProperty]
    private int luckyWheelSelectedIndex;

    /////////////////////////////////////////////////////////

    /// <summary>
    /// 黑杰克 内容
    /// </summary>
    [ObservableProperty]
    private string blackjackContent;

    /// <summary>
    /// 黑杰克 下一张牌内容
    /// </summary>
    [ObservableProperty]
    private string blackjackNextContent;

    /// <summary>
    /// 三张扑克 内容
    /// </summary>
    [ObservableProperty]
    private string pokerContent;
}
