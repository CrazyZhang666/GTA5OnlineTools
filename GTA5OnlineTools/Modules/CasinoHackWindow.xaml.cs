using GTA5OnlineTools.Models;
using GTA5OnlineTools.GTA.SDK;
using GTA5OnlineTools.GTA.Core;

namespace GTA5OnlineTools.Modules;

/// <summary>
/// CasinoHackWindow.xaml 的交互逻辑
/// </summary>
public partial class CasinoHackWindow
{
    /// <summary>
    /// CasinoHack 的数据模型绑定
    /// </summary>
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
                        Memory.Write(pointer + (117 + 1357 + 153 + 1 + i * 1) * 8, CasinoHackModel.RouletteSelectedIndex);
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
                    Memory.Write(pointer + index * 8, CasinoHackModel.LuckyWheelSelectedIndex);
                }
            }

            Thread.Sleep(250);
        }
    }
}
