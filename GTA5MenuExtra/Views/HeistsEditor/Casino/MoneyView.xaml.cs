using GTA5Core.Features;
using GTA5Shared.Helper;

namespace GTA5MenuExtra.Views.HeistsEditor.Casino;

/// <summary>
/// MoneyView.xaml 的交互逻辑
/// </summary>
public partial class MoneyView : UserControl
{
    private const int player_ratio = 1963945 + 1497 + 736 + 92;
    private const int player_money = 262145 + 29082;     // -1638885821

    private const int ai_ratio = 262145 + 29093;
    private const int lester_ratio = 262145 + 29068;     // joaat("CH_LESTER_CUT")

    public MoneyView()
    {
        InitializeComponent();
    }

    private void Button_Read_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        TextBox_Casino_Player1.Text = Globals.Get_Global_Value<int>(player_ratio + 1).ToString();
        TextBox_Casino_Player2.Text = Globals.Get_Global_Value<int>(player_ratio + 2).ToString();
        TextBox_Casino_Player3.Text = Globals.Get_Global_Value<int>(player_ratio + 3).ToString();
        TextBox_Casino_Player4.Text = Globals.Get_Global_Value<int>(player_ratio + 4).ToString();

        TextBox_Casino_Lester.Text = Globals.Get_Global_Value<int>(lester_ratio).ToString();

        TextBox_CasinoPotential_Money.Text = Globals.Get_Global_Value<int>(player_money + 0).ToString();
        TextBox_CasinoPotential_Artwork.Text = Globals.Get_Global_Value<int>(player_money + 1).ToString();
        TextBox_CasinoPotential_Gold.Text = Globals.Get_Global_Value<int>(player_money + 2).ToString();
        TextBox_CasinoPotential_Diamonds.Text = Globals.Get_Global_Value<int>(player_money + 3).ToString();

        TextBox_CasinoAI_1.Text = Globals.Get_Global_Value<int>(ai_ratio + 1).ToString();
        TextBox_CasinoAI_2.Text = Globals.Get_Global_Value<int>(ai_ratio + 2).ToString();
        TextBox_CasinoAI_3.Text = Globals.Get_Global_Value<int>(ai_ratio + 3).ToString();
        TextBox_CasinoAI_4.Text = Globals.Get_Global_Value<int>(ai_ratio + 4).ToString();
        TextBox_CasinoAI_5.Text = Globals.Get_Global_Value<int>(ai_ratio + 5).ToString();

        TextBox_CasinoAI_6.Text = Globals.Get_Global_Value<int>(ai_ratio + 6).ToString();
        TextBox_CasinoAI_7.Text = Globals.Get_Global_Value<int>(ai_ratio + 7).ToString();
        TextBox_CasinoAI_8.Text = Globals.Get_Global_Value<int>(ai_ratio + 8).ToString();
        TextBox_CasinoAI_9.Text = Globals.Get_Global_Value<int>(ai_ratio + 9).ToString();
        TextBox_CasinoAI_10.Text = Globals.Get_Global_Value<int>(ai_ratio + 10).ToString();

        TextBox_CasinoAI_11.Text = Globals.Get_Global_Value<int>(ai_ratio + 11).ToString();
        TextBox_CasinoAI_12.Text = Globals.Get_Global_Value<int>(ai_ratio + 12).ToString();
        TextBox_CasinoAI_13.Text = Globals.Get_Global_Value<int>(ai_ratio + 13).ToString();
        TextBox_CasinoAI_14.Text = Globals.Get_Global_Value<int>(ai_ratio + 14).ToString();
        TextBox_CasinoAI_15.Text = Globals.Get_Global_Value<int>(ai_ratio + 15).ToString();

        NotifierHelper.Show(NotifierType.Success, "读取 赌场抢劫 玩家分红数据 成功");
    }

    private void Button_Write_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (!int.TryParse(TextBox_Casino_Player1.Text, out int player1) ||
            !int.TryParse(TextBox_Casino_Player2.Text, out int player2) ||
            !int.TryParse(TextBox_Casino_Player3.Text, out int player3) ||
            !int.TryParse(TextBox_Casino_Player4.Text, out int player4) ||

            !int.TryParse(TextBox_Casino_Lester.Text, out int lester) ||

            !int.TryParse(TextBox_CasinoPotential_Money.Text, out int money) ||
            !int.TryParse(TextBox_CasinoPotential_Artwork.Text, out int artwork) ||
            !int.TryParse(TextBox_CasinoPotential_Gold.Text, out int gold) ||
            !int.TryParse(TextBox_CasinoPotential_Diamonds.Text, out int diamonds) ||

            !int.TryParse(TextBox_CasinoAI_1.Text, out int ai1) ||
            !int.TryParse(TextBox_CasinoAI_2.Text, out int ai2) ||
            !int.TryParse(TextBox_CasinoAI_3.Text, out int ai3) ||
            !int.TryParse(TextBox_CasinoAI_4.Text, out int ai4) ||
            !int.TryParse(TextBox_CasinoAI_5.Text, out int ai5) ||

            !int.TryParse(TextBox_CasinoAI_6.Text, out int ai6) ||
            !int.TryParse(TextBox_CasinoAI_7.Text, out int ai7) ||
            !int.TryParse(TextBox_CasinoAI_8.Text, out int ai8) ||
            !int.TryParse(TextBox_CasinoAI_9.Text, out int ai9) ||
            !int.TryParse(TextBox_CasinoAI_10.Text, out int ai10) ||

            !int.TryParse(TextBox_CasinoAI_11.Text, out int ai11) ||
            !int.TryParse(TextBox_CasinoAI_12.Text, out int ai12) ||
            !int.TryParse(TextBox_CasinoAI_13.Text, out int ai13) ||
            !int.TryParse(TextBox_CasinoAI_14.Text, out int ai14) ||
            !int.TryParse(TextBox_CasinoAI_15.Text, out int ai15))
        {
            NotifierHelper.Show(NotifierType.Warning, "部分数据不合法，请检查后重新写入");
            return;
        }

        Globals.Set_Global_Value(player_ratio + 1, player1);
        Globals.Set_Global_Value(player_ratio + 2, player2);
        Globals.Set_Global_Value(player_ratio + 3, player3);
        Globals.Set_Global_Value(player_ratio + 4, player4);

        Globals.Set_Global_Value(lester_ratio, lester);

        Globals.Set_Global_Value(player_money + 0, money);
        Globals.Set_Global_Value(player_money + 1, artwork);
        Globals.Set_Global_Value(player_money + 2, gold);
        Globals.Set_Global_Value(player_money + 3, diamonds);

        Globals.Set_Global_Value(ai_ratio + 1, ai1);
        Globals.Set_Global_Value(ai_ratio + 2, ai2);
        Globals.Set_Global_Value(ai_ratio + 3, ai3);
        Globals.Set_Global_Value(ai_ratio + 4, ai4);
        Globals.Set_Global_Value(ai_ratio + 5, ai5);

        Globals.Set_Global_Value(ai_ratio + 6, ai6);
        Globals.Set_Global_Value(ai_ratio + 7, ai7);
        Globals.Set_Global_Value(ai_ratio + 8, ai8);
        Globals.Set_Global_Value(ai_ratio + 9, ai9);
        Globals.Set_Global_Value(ai_ratio + 10, ai10);

        Globals.Set_Global_Value(ai_ratio + 11, ai11);
        Globals.Set_Global_Value(ai_ratio + 12, ai12);
        Globals.Set_Global_Value(ai_ratio + 13, ai13);
        Globals.Set_Global_Value(ai_ratio + 14, ai14);
        Globals.Set_Global_Value(ai_ratio + 15, ai15);

        NotifierHelper.Show(NotifierType.Success, "写入 赌场抢劫 玩家分红数据 成功");
    }
}
