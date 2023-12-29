using GTA5Core.Features;
using GTA5Shared.Helper;

namespace GTA5MenuExtra.Views.HeistsEditor.Apartment;

/// <summary>
/// MoneyView.xaml 的交互逻辑
/// </summary>
public partial class MoneyView : UserControl
{
    private const int apart_ratio = 1930201 + 3008;     // +1 +2 +3 +4
    private const int apart_money = 262145 + 9314; //HEIST_FLEECA_JOB_CASH_REWARD

    public MoneyView()
    {
        InitializeComponent();
    }

    private void Button_Read_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        TextBox_Apart_Player1.Text = Globals.Get_Global_Value<int>(apart_ratio + 1).ToString();
        TextBox_Apart_Player2.Text = Globals.Get_Global_Value<int>(apart_ratio + 2).ToString();
        TextBox_Apart_Player3.Text = Globals.Get_Global_Value<int>(apart_ratio + 3).ToString();
        TextBox_Apart_Player4.Text = Globals.Get_Global_Value<int>(apart_ratio + 4).ToString();

        TextBox_Apart_Fleeca.Text = Globals.Get_Global_Value<int>(apart_money + 0).ToString();
        TextBox_Apart_PrisonBreak.Text = Globals.Get_Global_Value<int>(apart_money + 1).ToString();
        TextBox_Apart_HumaneLabs.Text = Globals.Get_Global_Value<int>(apart_money + 2).ToString();
        TextBox_Apart_SeriesA.Text = Globals.Get_Global_Value<int>(apart_money + 3).ToString();
        TextBox_Apart_PacificStandard.Text = Globals.Get_Global_Value<int>(apart_money + 4).ToString();

        NotifierHelper.Show(NotifierType.Success, "读取 公寓抢劫 玩家分红数据 成功");
    }

    private void Button_Write_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (!int.TryParse(TextBox_Apart_Player1.Text, out int player1) ||
            !int.TryParse(TextBox_Apart_Player2.Text, out int player2) ||
            !int.TryParse(TextBox_Apart_Player3.Text, out int player3) ||
            !int.TryParse(TextBox_Apart_Player4.Text, out int player4) ||

            !int.TryParse(TextBox_Apart_Fleeca.Text, out int apart1) ||
            !int.TryParse(TextBox_Apart_PrisonBreak.Text, out int apart2) ||
            !int.TryParse(TextBox_Apart_HumaneLabs.Text, out int apart3) ||
            !int.TryParse(TextBox_Apart_SeriesA.Text, out int apart4) ||
            !int.TryParse(TextBox_Apart_PacificStandard.Text, out int apart5))
        {
            NotifierHelper.Show(NotifierType.Warning, "部分数据不合法，请检查后重新写入");
            return;
        }

        Globals.Set_Global_Value(apart_ratio + 1, player1);
        Globals.Set_Global_Value(apart_ratio + 2, player2);
        Globals.Set_Global_Value(apart_ratio + 3, player3);
        Globals.Set_Global_Value(apart_ratio + 4, player4);

        Globals.Set_Global_Value(apart_money + 0, apart1);
        Globals.Set_Global_Value(apart_money + 1, apart2);
        Globals.Set_Global_Value(apart_money + 2, apart3);
        Globals.Set_Global_Value(apart_money + 3, apart4);
        Globals.Set_Global_Value(apart_money + 4, apart5);

        NotifierHelper.Show(NotifierType.Success, "写入 公寓抢劫 玩家分红数据 成功");
    }
}
