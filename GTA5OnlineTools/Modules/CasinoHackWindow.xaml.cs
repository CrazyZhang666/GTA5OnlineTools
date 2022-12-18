using GTA5OnlineTools.GTA;
using GTA5OnlineTools.GTA.SDK;
using GTA5OnlineTools.GTA.Core;

using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5OnlineTools.Modules;

/// <summary>
/// CasinoHackWindow.xaml 的交互逻辑
/// </summary>
public partial class CasinoHackWindow
{
    public CasinoHackModel CasinoHackModel { get; set; } = new();

    public CasinoHackWindow()
    {
        InitializeComponent();
    }

    private void Window_CasinoHack_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        for (int i = 0; i < 37; i++)
        {
            ComboBox_Roulette.Items.Add($"号码 {i}");
        }
        ComboBox_Roulette.Items.Add("号码 00");

        new Thread(CasinoHackMainThread)
        {
            Name = "CasinoHackMainThread",
            IsBackground = true
        }.Start();
    }

    private void Window_CasinoHack_Closing(object sender, CancelEventArgs e)
    {

    }

    private void CasinoHackMainThread()
    {
        while (MainWindow.IsAppRunning)
        {
            // 黑杰克（21点）
            if (CasinoHackModel.BlackjackIsCheck)
            {
                long pointer = Locals.LocalAddress("blackjack");
                if (pointer != 0)
                {
                    pointer = Memory.Read<long>(pointer);
                    int index = Memory.Read<int>(pointer + (2026 + 2 + (1 + 1 * 1)) * 8);

                    var sb = new StringBuilder();
                    if ((index - 1) / 13 == 0)
                        sb.Append($"♣梅花{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 1)
                        sb.Append($"♦方块{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 2)
                        sb.Append($"♥红心{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 3)
                        sb.Append($"♠黑桃{(index - 1) % 13 + 1}");

                    CasinoHackModel.BlackjackContent = sb.ToString();

                    ///////////////////////////////////////////////////////

                    int current_table = Memory.Read<int>(pointer + (1769 + (1 + Hacks.ReadGA<int>(2703735) * 8) + 4) * 8);
                    int nums = Memory.Read<int>(pointer + (109 + 1 + (1 + current_table * 211) + 209) * 8);

                    index = Memory.Read<int>(pointer + (2026 + 2 + 1 + nums * 1) * 8);

                    sb.Clear();
                    if ((index - 1) / 13 == 0)
                        sb.Append($"♣梅花{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 1)
                        sb.Append($"♦方块{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 2)
                        sb.Append($"♥红心{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 3)
                        sb.Append($"♠黑桃{(index - 1) % 13 + 1}");

                    CasinoHackModel.BlackjackNextContent = sb.ToString();
                }
            }

            // 三张扑克
            if (CasinoHackModel.PokerIsCheck)
            {
                long pointer = Locals.LocalAddress("three_card_poker");
                if (pointer != 0)
                {
                    pointer = Memory.Read<long>(pointer);
                    int index = Memory.Read<int>(pointer + (1031 + 799 + 2 + (1 + 2 * 1)) * 8);

                    var sb = new StringBuilder();
                    if ((index - 1) / 13 == 0)
                        sb.Append($"♣梅花{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 1)
                        sb.Append($"♦方块{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 2)
                        sb.Append($"♥红心{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 3)
                        sb.Append($"♠黑桃{(index - 1) % 13 + 1}");

                    sb.Append('\n');
                    index = Memory.Read<int>(pointer + (1031 + 799 + 2 + (1 + 0 * 1)) * 8);
                    if ((index - 1) / 13 == 0)
                        sb.Append($"♣梅花{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 1)
                        sb.Append($"♦方块{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 2)
                        sb.Append($"♥红心{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 3)
                        sb.Append($"♠黑桃{(index - 1) % 13 + 1}");

                    sb.Append('\n');
                    index = Memory.Read<int>(pointer + (1031 + 799 + 2 + (1 + 1 * 1)) * 8);
                    if ((index - 1) / 13 == 0)
                        sb.Append($"♣梅花{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 1)
                        sb.Append($"♦方块{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 2)
                        sb.Append($"♥红心{(index - 1) % 13 + 1}");
                    if ((index - 1) / 13 == 3)
                        sb.Append($"♠黑桃{(index - 1) % 13 + 1}");

                    CasinoHackModel.PokerContent = sb.ToString();
                }
            }

            // 轮盘
            if (CasinoHackModel.RouletteIsCheck && CasinoHackModel.RouletteSelectedIndex != -1)
            {
                long pointer = Locals.LocalAddress("casinoroulette");
                if (pointer != 0)
                {
                    pointer = Memory.Read<long>(pointer);
                    for (int i = 0; i < 6; i++)
                    {
                        Memory.Write<int>(pointer + (117 + 1357 + 153 + (1 + i * 1)) * 8, CasinoHackModel.RouletteSelectedIndex);
                    }
                }
            }

            // 老虎机
            if (CasinoHackModel.SlotMachineIsCheck && CasinoHackModel.SlotMachineSelectedIndex != -1)
            {
                long pointer = Locals.LocalAddress("casino_slots");
                if (pointer != 0)
                {
                    pointer = Memory.Read<long>(pointer);
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 64; j++)
                        {
                            int index = 1341 + 1 + (1 + i * 65) + (1 + j * 1);
                            Memory.Write<int>(pointer + index * 8, CasinoHackModel.SlotMachineSelectedIndex);
                        }
                    }
                }
            }

            // 幸运轮盘
            if (CasinoHackModel.LuckyWheelIsCheck && CasinoHackModel.LuckyWheelSelectedIndex != -1)
            {
                // https://www.unknowncheats.me/forum/grand-theft-auto-v/483416-gtavcsmm.html
                long pointer = Locals.LocalAddress("casino_lucky_wheel");
                if (pointer != 0)
                {
                    pointer = Memory.Read<long>(pointer);
                    int index = 273 + 14;
                    Memory.Write<int>(pointer + index * 8, CasinoHackModel.LuckyWheelSelectedIndex);
                }
            }

            Thread.Sleep(250);
        }
    }
}

/// <summary>
/// CasinoHack 数据模型
/// </summary>
public class CasinoHackModel : ObservableObject
{
    private bool _blackjackIsCheck;
    /// <summary>
    /// 黑杰克 是否 开启预测
    /// </summary>
    public bool BlackjackIsCheck
    {
        get => _blackjackIsCheck;
        set => SetProperty(ref _blackjackIsCheck, value);
    }

    private bool _pokerIsCheck;
    /// <summary>
    /// 三张扑克 是否 开启预测
    /// </summary>
    public bool PokerIsCheck
    {
        get => _pokerIsCheck;
        set => SetProperty(ref _pokerIsCheck, value);
    }

    private bool _rouletteIsCheck;
    /// <summary>
    /// 轮盘赌 是否 操控中奖
    /// </summary>
    public bool RouletteIsCheck
    {
        get => _rouletteIsCheck;
        set => SetProperty(ref _rouletteIsCheck, value);
    }

    private bool _slotMachineIsCheck;
    /// <summary>
    /// 老虎机 是否 操控中奖
    /// </summary>
    public bool SlotMachineIsCheck
    {
        get => _slotMachineIsCheck;
        set => SetProperty(ref _slotMachineIsCheck, value);
    }

    private bool _luckyWheelIsCheck;
    /// <summary>
    /// 幸运轮盘 是否 操控中奖
    /// </summary>
    public bool LuckyWheelIsCheck
    {
        get => _luckyWheelIsCheck;
        set => SetProperty(ref _luckyWheelIsCheck, value);
    }

    /////////////////////////////////////////////////////////

    private int _rouletteSelectedIndex;
    /// <summary>
    /// 轮盘赌 选中索引
    /// </summary>
    public int RouletteSelectedIndex
    {
        get => _rouletteSelectedIndex;
        set => SetProperty(ref _rouletteSelectedIndex, value);
    }

    private int _slotMachineSelectedIndex;
    /// <summary>
    /// 老虎机 选中索引
    /// </summary>
    public int SlotMachineSelectedIndex
    {
        get => _slotMachineSelectedIndex;
        set => SetProperty(ref _slotMachineSelectedIndex, value);
    }

    private int _luckyWheelSelectedIndex;
    /// <summary>
    /// 幸运轮盘 选中索引
    /// </summary>
    public int LuckyWheelSelectedIndex
    {
        get => _luckyWheelSelectedIndex;
        set => SetProperty(ref _luckyWheelSelectedIndex, value);
    }

    /////////////////////////////////////////////////////////

    private string _blackjackContent;
    /// <summary>
    /// 黑杰克 内容
    /// </summary>
    public string BlackjackContent
    {
        get => _blackjackContent;
        set => SetProperty(ref _blackjackContent, value);
    }

    private string _blackjackNextContent;
    /// <summary>
    /// 黑杰克 下一张牌内容
    /// </summary>
    public string BlackjackNextContent
    {
        get => _blackjackNextContent;
        set => SetProperty(ref _blackjackNextContent, value);
    }

    private string _pokerContent;
    /// <summary>
    /// 三张扑克 内容
    /// </summary>
    public string PokerContent
    {
        get => _pokerContent;
        set => SetProperty(ref _pokerContent, value);
    }
}
