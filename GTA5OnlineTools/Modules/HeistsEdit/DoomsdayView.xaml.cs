using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Helper;
using GTA5OnlineTools.Features.SDK;

namespace GTA5OnlineTools.Modules.HeistsEdit;

/// <summary>
/// DoomsdayView.xaml 的交互逻辑
/// </summary>
public partial class DoomsdayView : UserControl
{
    public DoomsdayView()
    {
        InitializeComponent();
    }

    #region 末日抢劫 - 分红数据
    private void Button_Read_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        // 末日抢劫玩家分红比例
        TextBox_Doomsday_Player1.Text = Hacks.ReadGA<int>(1962546 + 812 + 50 + 1).ToString();
        TextBox_Doomsday_Player2.Text = Hacks.ReadGA<int>(1962546 + 812 + 50 + 2).ToString();
        TextBox_Doomsday_Player3.Text = Hacks.ReadGA<int>(1962546 + 812 + 50 + 3).ToString();
        TextBox_Doomsday_Player4.Text = Hacks.ReadGA<int>(1962546 + 812 + 50 + 4).ToString();

        TextBox_Doomsday_ActI.Text = Hacks.ReadGA<int>(262145 + 9132).ToString();
        TextBox_Doomsday_ActII.Text = Hacks.ReadGA<int>(262145 + 9133).ToString();
        TextBox_Doomsday_ActIII.Text = Hacks.ReadGA<int>(262145 + 9134).ToString();
    }

    private void Button_Write_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        try
        {
            if (TextBox_Doomsday_Player1.Text.Trim() != "" &&
                TextBox_Doomsday_Player2.Text.Trim() != "" &&
                TextBox_Doomsday_Player3.Text.Trim() != "" &&
                TextBox_Doomsday_Player4.Text.Trim() != "" &&

                TextBox_Doomsday_ActI.Text.Trim() != "" &&
                TextBox_Doomsday_ActII.Text.Trim() != "" &&
                TextBox_Doomsday_ActIII.Text.Trim() != "")
            {
                // 末日抢劫玩家分红比例
                Hacks.WriteGA(1962546 + 812 + 50 + 1, Convert.ToInt32(TextBox_Doomsday_Player1.Text.Trim()));
                Hacks.WriteGA(1962546 + 812 + 50 + 2, Convert.ToInt32(TextBox_Doomsday_Player2.Text.Trim()));
                Hacks.WriteGA(1962546 + 812 + 50 + 3, Convert.ToInt32(TextBox_Doomsday_Player3.Text.Trim()));
                Hacks.WriteGA(1962546 + 812 + 50 + 4, Convert.ToInt32(TextBox_Doomsday_Player4.Text.Trim()));

                Hacks.WriteGA(262145 + 9132, Convert.ToInt32(TextBox_Doomsday_ActI.Text.Trim()));
                Hacks.WriteGA(262145 + 9133, Convert.ToInt32(TextBox_Doomsday_ActII.Text.Trim()));
                Hacks.WriteGA(262145 + 9134, Convert.ToInt32(TextBox_Doomsday_ActIII.Text.Trim()));
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

    #region 末日抢劫 - 高级
    private void WriteStatWithDelay(string hash, int value)
    {
        Task.Run(() =>
        {
            Hacks.STATS_WriteInt(hash, value);
            Task.Delay(1000).Wait();
        });
    }

    ////////////////////////////////////////////////////
    private void Button_GANGOPS_FLOW_MISSION_PROG_1_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_FLOW_MISSION_PROG", -1);
    }

    private void Button_GANGOPS_FLOW_MISSION_PROG_503_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_FLOW_MISSION_PROG", 503);
        WriteStatWithDelay("_GANGOPS_HEIST_STATUS", 819193);
        WriteStatWithDelay("_GANGOPS_FLOW_NOTIFICATIONS", 1557);
    }

    private void Button_GANGOPS_FLOW_MISSION_PROG_240_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_FLOW_MISSION_PROG", 240);
        WriteStatWithDelay("_GANGOPS_HEIST_STATUS", 819198);
        WriteStatWithDelay("_GANGOPS_FLOW_NOTIFICATIONS", 1557);
    }

    private void Button_GANGOPS_FLOW_MISSION_PROG_16368_Click(object sender, RoutedEventArgs e)
    {
        WriteStatWithDelay("_GANGOPS_FLOW_MISSION_PROG", 16368);
        WriteStatWithDelay("_GANGOPS_HEIST_STATUS", 819190);
        WriteStatWithDelay("_GANGOPS_FLOW_NOTIFICATIONS", 1557);
    }
    #endregion
}
