using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Helper;
using GTA5OnlineTools.Features.SDK;

namespace GTA5OnlineTools.Modules.HeistsEdit;

/// <summary>
/// ApartmentView.xaml 的交互逻辑
/// </summary>
public partial class ApartmentView : UserControl
{
    public ApartmentView()
    {
        InitializeComponent();
    }

    #region 公寓抢劫 - 分红数据
    private void Button_Read_Click(object sender, RoutedEventArgs e)
    {
        

        // 公寓抢劫玩家分红比例
        TextBox_Apart_Player1.Text = Hacks.ReadGA<int>(1933908 + 3008 + 1).ToString();
        TextBox_Apart_Player2.Text = Hacks.ReadGA<int>(1933908 + 3008 + 2).ToString();
        TextBox_Apart_Player3.Text = Hacks.ReadGA<int>(1933908 + 3008 + 3).ToString();
        TextBox_Apart_Player4.Text = Hacks.ReadGA<int>(1933908 + 3008 + 4).ToString();

        TextBox_Apart_Fleeca.Text = Hacks.ReadGA<int>(262145 + 9127).ToString();
        TextBox_Apart_PrisonBreak.Text = Hacks.ReadGA<int>(262145 + 9128).ToString();
        TextBox_Apart_HumaneLabs.Text = Hacks.ReadGA<int>(262145 + 9129).ToString();
        TextBox_Apart_SeriesA.Text = Hacks.ReadGA<int>(262145 + 9130).ToString();
        TextBox_Apart_PacificStandard.Text = Hacks.ReadGA<int>(262145 + 9131).ToString();
    }

    private void Button_Write_Click(object sender, RoutedEventArgs e)
    {
        

        try
        {
            if (TextBox_Apart_Player1.Text.Trim() != "" &&
                TextBox_Apart_Player2.Text.Trim() != "" &&
                TextBox_Apart_Player3.Text.Trim() != "" &&
                TextBox_Apart_Player4.Text.Trim() != "" &&

                TextBox_Apart_Fleeca.Text.Trim() != "" &&
                TextBox_Apart_PrisonBreak.Text.Trim() != "" &&
                TextBox_Apart_HumaneLabs.Text.Trim() != "" &&
                TextBox_Apart_SeriesA.Text.Trim() != "" &&
                TextBox_Apart_PacificStandard.Text.Trim() != "")
            {
                // 公寓抢劫玩家分红比例
                Hacks.WriteGA(1933908 + 3008 + 1, Convert.ToInt32(TextBox_Apart_Player1.Text.Trim()));
                Hacks.WriteGA(1933908 + 3008 + 2, Convert.ToInt32(TextBox_Apart_Player2.Text.Trim()));
                Hacks.WriteGA(1933908 + 3008 + 3, Convert.ToInt32(TextBox_Apart_Player3.Text.Trim()));
                Hacks.WriteGA(1933908 + 3008 + 4, Convert.ToInt32(TextBox_Apart_Player4.Text.Trim()));

                Hacks.WriteGA(262145 + 9127, Convert.ToInt32(TextBox_Apart_Fleeca.Text.Trim()));
                Hacks.WriteGA(262145 + 9128, Convert.ToInt32(TextBox_Apart_PrisonBreak.Text.Trim()));
                Hacks.WriteGA(262145 + 9129, Convert.ToInt32(TextBox_Apart_HumaneLabs.Text.Trim()));
                Hacks.WriteGA(262145 + 9130, Convert.ToInt32(TextBox_Apart_SeriesA.Text.Trim()));
                Hacks.WriteGA(262145 + 9131, Convert.ToInt32(TextBox_Apart_PacificStandard.Text.Trim()));
            }
            else
            {
                NotifierHelper.Show(NotifierType.Warning, "部分数据为空，请检查后重新写入");
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
    #endregion

    #region 公寓抢劫 - 高级
    private void WriteStatWithDelay(string hash, int value)
    {
        Task.Run(() =>
        {
            Hacks.STATS_WriteInt(hash, value);
            Task.Delay(1000).Wait();
        });
    }

    ////////////////////////////////////////////////////

    private void Button_HEIST_PLANNING_STAGE_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_HEIST_PLANNING_STAGE", -1);
    }
    #endregion
}
