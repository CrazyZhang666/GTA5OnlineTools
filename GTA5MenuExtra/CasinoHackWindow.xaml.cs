using GTA5MenuExtra.Models;

using GTA5Core.Native;
using GTA5Core.Features;
using GTA5Shared.Helper;

namespace GTA5MenuExtra;

/// <summary>
/// CasinoHackWindow.xaml 的交互逻辑
/// </summary>
public partial class CasinoHackWindow
{
    public CasinoHackModel CasinoHackModel { get; set; } = new();

    private bool _disposed = false;

    private int luckyWheelSlot = -1;
    private int slotMachineSlot = -1;
    private int rouletteSlot = -1;

    public CasinoHackWindow()
    {
        InitializeComponent();
    }

    private void Window_CasinoHack_Loaded(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 37; i++)
        {
            ListBox_Roulette.Items.Add($"号码 {i}");
        }
        ListBox_Roulette.Items.Add("号码 00");
        ListBox_Roulette.SelectedIndex = 0;

        new Thread(CasinoHackLoopThread)
        {
            Name = "CasinoHackLoopThread",
            IsBackground = true
        }.Start();
    }

    private void Window_CasinoHack_Closing(object sender, CancelEventArgs e)
    {
        _disposed = true;
    }

    private void CasinoHackLoopThread()
    {
        while (!_disposed)
        {
            // 黑杰克（21点）
            var pScript = Locals.LocalAddress("blackjack");
            if (Memory.IsValid(pScript))
            {
                var pointer = Memory.Read<long>(pScript);

                ///////////// 庄家底牌 /////////////

                var index = Memory.Read<int>(pointer + (2031 + 2 + 1 + 1 * 1) * 8);
                CasinoHackModel.BlackjackContent = GetBlackJackContent(index);

                ///////////// 下一张牌 /////////////

                var current_table = Memory.Read<int>(pointer + (1774 + 1 + Globals.GetPlayerID() * 8 + 4) * 8);
                var nums = Memory.Read<int>(pointer + (114 + 1 + 1 + current_table * 211 + 209) * 8);

                index = Memory.Read<int>(pointer + (2031 + 2 + 1 + nums * 1) * 8);
                CasinoHackModel.BlackjackNextContent = GetBlackJackContent(index);
            }

            // 三张扑克
            pScript = Locals.LocalAddress("three_card_poker");
            if (Memory.IsValid(pScript))
            {
                var pointer = Memory.Read<long>(pScript);

                var index = Memory.Read<int>(pointer + (1036 + 799 + 2 + 1 + 2 * 1) * 8);
                CasinoHackModel.Poker1Content = GetBlackJackContent(index);

                index = Memory.Read<int>(pointer + (1036 + 799 + 2 + 1 + 0 * 1) * 8);
                CasinoHackModel.Poker2Content = GetBlackJackContent(index);

                index = Memory.Read<int>(pointer + (1036 + 799 + 2 + 1 + 1 * 1) * 8);
                CasinoHackModel.Poker3Content = GetBlackJackContent(index);
            }

            // 幸运轮盘
            if (luckyWheelSlot != -1)
            {
                Locals.WriteLocalAddress("casino_lucky_wheel", 278 + 14, luckyWheelSlot);
            }

            // 老虎机
            if (slotMachineSlot != -1)
            {
                pScript = Locals.LocalAddress("casino_slots");
                if (Memory.IsValid(pScript))
                {
                    var pointer = Memory.Read<long>(pScript);

                    for (var i = 0; i < 3; i++)
                    {
                        for (var j = 0; j < 64; j++)
                        {
                            var offset = 1346 + 1 + 1 + i * 65 + 1 + j;
                            Memory.Write(pointer + offset * 8, slotMachineSlot);
                        }
                    }
                }
            }

            // 轮盘赌
            pScript = Locals.LocalAddress("casinoroulette");
            if (Memory.IsValid(pScript))
            {
                var pointer = Memory.Read<long>(pScript);

                for (var i = 0; i < 6; i++)
                {
                    Memory.Write(pointer + (122 + 1357 + 153 + 1 + i * 1) * 8, rouletteSlot);
                }
            }

            Thread.Sleep(20);
        }
    }

    private string GetBlackJackContent(int index)
    {
        var flag = (index - 1) / 13;
        var card = (index - 1) % 13 + 1;

        var content = string.Empty;

        switch (flag)
        {
            case 0:
                content = $"♣ 梅花 {GetJackString(card)}";
                break;
            case 1:
                content = $"♦ 方块 {GetJackString(card)}";
                break;
            case 2:
                content = $"♥ 红心 {GetJackString(card)}";
                break;
            case 3:
                content = $"♠ 黑桃 {GetJackString(card)}";
                break;
        }

        return content;
    }

    private string GetJackString(int card)
    {
        return card switch
        {
            1 => "A",
            11 => "J",
            12 => "Q",
            13 => "K",
            _ => $"{card}",
        };
    }

    private void Button_LuckyWheelSlot_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        luckyWheelSlot = ListBox_LuckyWheel.SelectedIndex;

        if (ListBox_LuckyWheel.SelectedItem is ListBoxItem item)
            TextBlock_LuckyWheelValue.Text = $"奖品：{item.Content}";
    }

    private void Button_SlotMachineSlot_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        slotMachineSlot = ListBox_SlotMachine.SelectedIndex;

        if (ListBox_SlotMachine.SelectedItem is ListBoxItem item)
            TextBlock_SlotMachineValue.Text = $"奖品：{item.Content}";
    }

    private void Button_RouletteSlot_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        rouletteSlot = ListBox_Roulette.SelectedIndex;

        if (ListBox_Roulette.SelectedItem is string item)
            TextBlock_RouletteValue.Text = $"中奖号码：{item}";
    }
}
