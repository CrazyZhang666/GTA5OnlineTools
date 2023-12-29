using GTA5Core.Features;
using GTA5Shared.Helper;

namespace GTA5MenuExtra.Views.HeistsEditor.Perico;

/// <summary>
/// MoneyView.xaml 的交互逻辑
/// </summary>
public partial class MoneyView : UserControl
{
    /// <summary>
    /// 玩家分红
    /// </summary>
    private const int player_ratio = 1970744 + 831 + 56;
    /// <summary>
    /// 主要目标价值
    /// </summary>
    private const int target_money = 262145 + 30259;    // 132820683    joaat("IH_PRIMARY_TARGET_VALUE_TEQUILA")     Global_262145.f_30259
    /// <summary>
    /// 背包容量
    /// </summary>
    private const int bag_size = 262145 + 30009;        // 1859395035   Global_262145.f_30009

    public MoneyView()
    {
        InitializeComponent();
    }

    private void Button_Read_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        TextBox_Cayo_Player1.Text = Globals.Get_Global_Value<int>(player_ratio + 1).ToString();
        TextBox_Cayo_Player2.Text = Globals.Get_Global_Value<int>(player_ratio + 2).ToString();
        TextBox_Cayo_Player3.Text = Globals.Get_Global_Value<int>(player_ratio + 3).ToString();
        TextBox_Cayo_Player4.Text = Globals.Get_Global_Value<int>(player_ratio + 4).ToString();

        TextBox_Cayo_Tequila.Text = Globals.Get_Global_Value<int>(target_money + 0).ToString();
        TextBox_Cayo_RubyNecklace.Text = Globals.Get_Global_Value<int>(target_money + 1).ToString();
        TextBox_Cayo_BearerBonds.Text = Globals.Get_Global_Value<int>(target_money + 2).ToString();
        TextBox_Cayo_PinkDiamond.Text = Globals.Get_Global_Value<int>(target_money + 3).ToString();
        TextBox_Cayo_MadrazoFiles.Text = Globals.Get_Global_Value<int>(target_money + 4).ToString();
        TextBox_Cayo_BlackPanther.Text = Globals.Get_Global_Value<int>(target_money + 5).ToString();

        TextBox_Cayo_LocalBagSize.Text = Globals.Get_Global_Value<int>(bag_size).ToString();

        TextBox_Cayo_FencingFee.Text = Globals.Get_Global_Value<float>(target_money + 9).ToString();
        TextBox_Cayo_PavelCut.Text = Globals.Get_Global_Value<float>(target_money + 10).ToString();

        NotifierHelper.Show(NotifierType.Success, "读取 佩里克岛 玩家分红数据 成功");
    }

    private void Button_Write_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (!int.TryParse(TextBox_Cayo_Player1.Text, out int player1) ||
            !int.TryParse(TextBox_Cayo_Player2.Text, out int player2) ||
            !int.TryParse(TextBox_Cayo_Player3.Text, out int player3) ||
            !int.TryParse(TextBox_Cayo_Player4.Text, out int player4) ||

            !int.TryParse(TextBox_Cayo_Tequila.Text, out int cayo1) ||
            !int.TryParse(TextBox_Cayo_RubyNecklace.Text, out int cayo2) ||
            !int.TryParse(TextBox_Cayo_BearerBonds.Text, out int cayo3) ||
            !int.TryParse(TextBox_Cayo_PinkDiamond.Text, out int cayo4) ||
            !int.TryParse(TextBox_Cayo_MadrazoFiles.Text, out int cayo5) ||
            !int.TryParse(TextBox_Cayo_BlackPanther.Text, out int cayo6) ||

            !int.TryParse(TextBox_Cayo_LocalBagSize.Text, out int bagsize) ||

            !float.TryParse(TextBox_Cayo_FencingFee.Text, out float fencingfee) ||
            !float.TryParse(TextBox_Cayo_PavelCut.Text, out float pavecut))
        {
            NotifierHelper.Show(NotifierType.Warning, "部分数据不合法，请检查后重新写入");
            return;
        }

        Globals.Set_Global_Value(player_ratio + 1, player1);
        Globals.Set_Global_Value(player_ratio + 2, player2);
        Globals.Set_Global_Value(player_ratio + 3, player3);
        Globals.Set_Global_Value(player_ratio + 4, player4);

        Globals.Set_Global_Value(target_money + 0, cayo1);
        Globals.Set_Global_Value(target_money + 1, cayo2);
        Globals.Set_Global_Value(target_money + 2, cayo3);
        Globals.Set_Global_Value(target_money + 3, cayo4);
        Globals.Set_Global_Value(target_money + 4, cayo5);
        Globals.Set_Global_Value(target_money + 5, cayo6);

        Globals.Set_Global_Value(bag_size, bagsize);

        Globals.Set_Global_Value(target_money + 9, fencingfee);
        Globals.Set_Global_Value(target_money + 10, pavecut);

        NotifierHelper.Show(NotifierType.Success, "写入 佩里克岛 玩家分红数据 成功");
    }
}
