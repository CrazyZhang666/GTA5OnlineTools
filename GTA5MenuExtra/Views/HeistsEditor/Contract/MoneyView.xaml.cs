using GTA5Core.Features;
using GTA5Shared.Helper;

namespace GTA5MenuExtra.Views.HeistsEditor.Contract;

/// <summary>
/// MoneyView.xaml 的交互逻辑
/// </summary>
public partial class MoneyView : UserControl
{
    private const int fixer_ratio = 262145 + 32071;     // -2108119120  joaat("FIXER_FINALE_LEADER_CASH_REWARD")     Global_262145.f_31955
    private const int tuner_ratio = 262145 + 31323;     // -920277662   joaat("TUNER_ROBBERY_LEADER_CASH_REWARD0")   Global_262145.f_31249[0]

    public MoneyView()
    {
        InitializeComponent();
    }

    private void Button_Read_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        TextBox_FIXER_Value.Text = Globals.Get_Global_Value<int>(fixer_ratio).ToString();

        TextBox_TUNER_Value1.Text = Globals.Get_Global_Value<int>(tuner_ratio + 1).ToString();
        TextBox_TUNER_Value2.Text = Globals.Get_Global_Value<int>(tuner_ratio + 2).ToString();
        TextBox_TUNER_Value3.Text = Globals.Get_Global_Value<int>(tuner_ratio + 3).ToString();
        TextBox_TUNER_Value4.Text = Globals.Get_Global_Value<int>(tuner_ratio + 4).ToString();
        TextBox_TUNER_Value5.Text = Globals.Get_Global_Value<int>(tuner_ratio + 5).ToString();
        TextBox_TUNER_Value6.Text = Globals.Get_Global_Value<int>(tuner_ratio + 6).ToString();
        TextBox_TUNER_Value7.Text = Globals.Get_Global_Value<int>(tuner_ratio + 7).ToString();
        TextBox_TUNER_Value8.Text = Globals.Get_Global_Value<int>(tuner_ratio + 8).ToString();

        NotifierHelper.Show(NotifierType.Success, "读取 事所合约 玩家分红数据 成功");
    }

    private void Button_Write_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (!int.TryParse(TextBox_FIXER_Value.Text, out int fixer) ||

            !int.TryParse(TextBox_TUNER_Value1.Text, out int tuner1) ||
            !int.TryParse(TextBox_TUNER_Value2.Text, out int tuner2) ||
            !int.TryParse(TextBox_TUNER_Value3.Text, out int tuner3) ||
            !int.TryParse(TextBox_TUNER_Value4.Text, out int tuner4) ||
            !int.TryParse(TextBox_TUNER_Value5.Text, out int tuner5) ||
            !int.TryParse(TextBox_TUNER_Value6.Text, out int tuner6) ||
            !int.TryParse(TextBox_TUNER_Value7.Text, out int tuner7) ||
            !int.TryParse(TextBox_TUNER_Value8.Text, out int tuner8))
        {
            NotifierHelper.Show(NotifierType.Warning, "部分数据不合法，请检查后重新写入");
            return;
        }

        Globals.Set_Global_Value(fixer_ratio, fixer);

        Globals.Set_Global_Value(tuner_ratio + 1, tuner1);
        Globals.Set_Global_Value(tuner_ratio + 2, tuner2);
        Globals.Set_Global_Value(tuner_ratio + 3, tuner3);
        Globals.Set_Global_Value(tuner_ratio + 4, tuner4);
        Globals.Set_Global_Value(tuner_ratio + 5, tuner5);
        Globals.Set_Global_Value(tuner_ratio + 6, tuner6);
        Globals.Set_Global_Value(tuner_ratio + 7, tuner7);
        Globals.Set_Global_Value(tuner_ratio + 8, tuner8);

        NotifierHelper.Show(NotifierType.Success, "写入 事所合约 玩家分红数据 成功");
    }
}
