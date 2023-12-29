using GTA5Core.Features;
using GTA5Shared.Helper;

namespace GTA5MenuExtra.Views.HeistsEditor.Doomsday;

/// <summary>
/// MoneyView.xaml 的交互逻辑
/// </summary>
public partial class MoneyView : UserControl
{
    private const int player_ratio = 1959865 + 812 + 50;    
    private const int player_money = 262145 + 9319;         // joaat("GANGOPS_THE_IAA_JOB_CASH_REWARD")

    public MoneyView()
    {
        InitializeComponent();
    }

    private void Button_Read_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        TextBox_Doomsday_Player1.Text = Globals.Get_Global_Value<int>(player_ratio + 1).ToString();
        TextBox_Doomsday_Player2.Text = Globals.Get_Global_Value<int>(player_ratio + 2).ToString();
        TextBox_Doomsday_Player3.Text = Globals.Get_Global_Value<int>(player_ratio + 3).ToString();
        TextBox_Doomsday_Player4.Text = Globals.Get_Global_Value<int>(player_ratio + 4).ToString();

        TextBox_Doomsday_ActI.Text = Globals.Get_Global_Value<int>(player_money + 0).ToString();
        TextBox_Doomsday_ActII.Text = Globals.Get_Global_Value<int>(player_money + 1).ToString();
        TextBox_Doomsday_ActIII.Text = Globals.Get_Global_Value<int>(player_money + 2).ToString();

        NotifierHelper.Show(NotifierType.Success, "读取 末日抢劫 玩家分红数据 成功");
    }

    private void Button_Write_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (!int.TryParse(TextBox_Doomsday_Player1.Text, out int player1) ||
           !int.TryParse(TextBox_Doomsday_Player2.Text, out int player2) ||
           !int.TryParse(TextBox_Doomsday_Player3.Text, out int player3) ||
           !int.TryParse(TextBox_Doomsday_Player4.Text, out int player4) ||

           !int.TryParse(TextBox_Doomsday_ActI.Text, out int act1) ||
           !int.TryParse(TextBox_Doomsday_ActII.Text, out int act2) ||
           !int.TryParse(TextBox_Doomsday_ActIII.Text, out int act3))
        {
            NotifierHelper.Show(NotifierType.Warning, "部分数据不合法，请检查后重新写入");
            return;
        }

        Globals.Set_Global_Value(player_ratio + 1, player1);
        Globals.Set_Global_Value(player_ratio + 2, player2);
        Globals.Set_Global_Value(player_ratio + 3, player3);
        Globals.Set_Global_Value(player_ratio + 4, player4);

        Globals.Set_Global_Value(player_money + 0, act1);
        Globals.Set_Global_Value(player_money + 1, act2);
        Globals.Set_Global_Value(player_money + 2, act3);

        NotifierHelper.Show(NotifierType.Success, "写入 末日抢劫 玩家分红数据 成功");
    }
}
