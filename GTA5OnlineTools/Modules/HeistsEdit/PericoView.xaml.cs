using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Helper;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.HeistsEdit;

/// <summary>
/// PericoView.xaml 的交互逻辑
/// </summary>
public partial class PericoView : UserControl
{
    public PericoView()
    {
        InitializeComponent();

        TextBox_PreviewGTAHax.Text = "INT32\n";
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessUtil.OpenPath(e.Uri.OriginalString);
        e.Handled = true;
    }

    private string GetSelectedComboBoxItemContent(ComboBox comboBox)
    {
        return (comboBox.SelectedItem as ComboBoxItem).Content.ToString();
    }

    #region 佩里克岛 - 分红数据
    private void Button_Read_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        // 佩里克岛抢劫玩家分红比例
        TextBox_Cayo_Player1.Text = Hacks.ReadGA<int>(1973321 + 823 + 56 + 1).ToString();
        TextBox_Cayo_Player2.Text = Hacks.ReadGA<int>(1973321 + 823 + 56 + 2).ToString();
        TextBox_Cayo_Player3.Text = Hacks.ReadGA<int>(1973321 + 823 + 56 + 3).ToString();
        TextBox_Cayo_Player4.Text = Hacks.ReadGA<int>(1973321 + 823 + 56 + 4).ToString();

        TextBox_Cayo_Tequila.Text = Hacks.ReadGA<int>(262145 + 29970).ToString();
        TextBox_Cayo_RubyNecklace.Text = Hacks.ReadGA<int>(262145 + 29971).ToString();
        TextBox_Cayo_BearerBonds.Text = Hacks.ReadGA<int>(262145 + 29972).ToString();
        TextBox_Cayo_PinkDiamond.Text = Hacks.ReadGA<int>(262145 + 29973).ToString();
        TextBox_Cayo_MadrazoFiles.Text = Hacks.ReadGA<int>(262145 + 29974).ToString();
        TextBox_Cayo_BlackPanther.Text = Hacks.ReadGA<int>(262145 + 29975).ToString();

        TextBox_Cayo_LocalBagSize.Text = Hacks.ReadGA<int>(262145 + 29720).ToString();

        TextBox_Cayo_FencingFee.Text = Hacks.ReadGA<float>(262145 + 29979).ToString();
        TextBox_Cayo_PavelCut.Text = Hacks.ReadGA<float>(262145 + 29980).ToString();
    }

    private void Button_Write_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        try
        {
            if (TextBox_Cayo_Player1.Text.Trim() != "" &&
                TextBox_Cayo_Player2.Text.Trim() != "" &&
                TextBox_Cayo_Player3.Text.Trim() != "" &&
                TextBox_Cayo_Player4.Text.Trim() != "" &&

                TextBox_Cayo_Tequila.Text.Trim() != "" &&
                TextBox_Cayo_RubyNecklace.Text.Trim() != "" &&
                TextBox_Cayo_BearerBonds.Text.Trim() != "" &&
                TextBox_Cayo_PinkDiamond.Text.Trim() != "" &&
                TextBox_Cayo_MadrazoFiles.Text.Trim() != "" &&
                TextBox_Cayo_BlackPanther.Text.Trim() != "" &&

                TextBox_Cayo_LocalBagSize.Text.Trim() != "" &&

                TextBox_Cayo_FencingFee.Text.Trim() != "" &&
                TextBox_Cayo_PavelCut.Text.Trim() != "")
            {
                // 佩里克岛抢劫玩家分红比例
                Hacks.WriteGA(1973321 + 823 + 56 + 1, Convert.ToInt32(TextBox_Cayo_Player1.Text.Trim()));
                Hacks.WriteGA(1973321 + 823 + 56 + 2, Convert.ToInt32(TextBox_Cayo_Player2.Text.Trim()));
                Hacks.WriteGA(1973321 + 823 + 56 + 3, Convert.ToInt32(TextBox_Cayo_Player3.Text.Trim()));
                Hacks.WriteGA(1973321 + 823 + 56 + 4, Convert.ToInt32(TextBox_Cayo_Player4.Text.Trim()));

                Hacks.WriteGA(262145 + 29970, Convert.ToInt32(TextBox_Cayo_Tequila.Text.Trim()));
                Hacks.WriteGA(262145 + 29971, Convert.ToInt32(TextBox_Cayo_RubyNecklace.Text.Trim()));
                Hacks.WriteGA(262145 + 29972, Convert.ToInt32(TextBox_Cayo_BearerBonds.Text.Trim()));
                Hacks.WriteGA(262145 + 29973, Convert.ToInt32(TextBox_Cayo_PinkDiamond.Text.Trim()));
                Hacks.WriteGA(262145 + 29974, Convert.ToInt32(TextBox_Cayo_MadrazoFiles.Text.Trim()));
                Hacks.WriteGA(262145 + 29975, Convert.ToInt32(TextBox_Cayo_BlackPanther.Text.Trim()));

                Hacks.WriteGA(262145 + 29720, Convert.ToInt32(TextBox_Cayo_LocalBagSize.Text.Trim()));

                Hacks.WriteGA(262145 + 29979, Convert.ToSingle(TextBox_Cayo_FencingFee.Text.Trim()));
                Hacks.WriteGA(262145 + 29980, Convert.ToSingle(TextBox_Cayo_PavelCut.Text.Trim()));
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

    #region 佩里克岛 - 前置任务
    private void TextBox_AppendText_MP(string stat, string value)
    {
        TextBox_PreviewGTAHax.AppendText($"$MPx{stat}\n");
        TextBox_PreviewGTAHax.AppendText($"{value}\n");
    }

    private void WirtePreviewGTAHaxStat()
    {
        try
        {
            File.WriteAllText(FileUtil.F_GTAHaxStat_Path, TextBox_PreviewGTAHax.Text);
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    private void Button_BuildGTAHax_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        TextBox_PreviewGTAHax.Clear();
        TextBox_PreviewGTAHax.AppendText("INT32\n");

        if (RadioButton_H4CNF_P1.IsChecked == true)
        {
            if (CheckBox_H4CNF_BS_GEN.IsChecked == true)
            {
                TextBox_AppendText_MP("_H4CNF_BS_GEN", "131071");
            }
            if (CheckBox_H4CNF_BS_ENTR.IsChecked == true)
            {
                TextBox_AppendText_MP("_H4CNF_BS_ENTR", "63");
            }
            if (CheckBox_H4CNF_BS_ABIL.IsChecked == true)
            {
                TextBox_AppendText_MP("_H4CNF_BS_ABIL", "63");
            }
            if (CheckBox_H4CNF_APPROACH.IsChecked == true)
            {
                TextBox_AppendText_MP("_H4CNF_APPROACH", "-1");
            }

            //////////////////////////

            if (GetSelectedComboBoxItemContent(ComboBox_H4_PROGRESS) == "普通模式")
            {
                TextBox_AppendText_MP("_H4_PROGRESS", "126823");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4_PROGRESS) == "困难模式")
            {
                TextBox_AppendText_MP("_H4_PROGRESS", "131055");
            }

            //////////////////////////

            if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_TARGET) == "西西米托龙舌兰")
            {
                TextBox_AppendText_MP("_H4CNF_TARGET", "0");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_TARGET) == "红宝石项链")
            {
                TextBox_AppendText_MP("_H4CNF_TARGET", "1");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_TARGET) == "不记名债券")
            {
                TextBox_AppendText_MP("_H4CNF_TARGET", "2");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_TARGET) == "粉钻")
            {
                TextBox_AppendText_MP("_H4CNF_TARGET", "3");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_TARGET) == "玛德拉索文件")
            {
                TextBox_AppendText_MP("_H4CNF_TARGET", "4");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_TARGET) == "猎豹雕像")
            {
                TextBox_AppendText_MP("_H4CNF_TARGET", "5");
            }

            //////////////////////////

            /**************************** 现金 ****************************/
            if (GetSelectedComboBoxItemContent(ComboBox_H4LOOT) == "已发现/侦察 现金（室内/室外）")
            {
                TextBox_AppendText_MP("_H4LOOT_CASH_I", "-1");
                TextBox_AppendText_MP("_H4LOOT_CASH_C", "-1");
                TextBox_AppendText_MP("_H4LOOT_CASH_I_SCOPED", "-1");
                TextBox_AppendText_MP("_H4LOOT_CASH_C_SCOPED", "-1");
                TextBox_AppendText_MP("_H4LOOT_CASH_V", "90000");
            }
            else
            {
                if (CheckBox_H4LOOT_Random.IsChecked == false)
                {
                    TextBox_AppendText_MP("_H4LOOT_CASH_I", "0");
                    TextBox_AppendText_MP("_H4LOOT_CASH_C", "0");
                    TextBox_AppendText_MP("_H4LOOT_CASH_I_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_CASH_C_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_CASH_V", "0");
                }
            }

            /**************************** 大麻 ****************************/
            if (GetSelectedComboBoxItemContent(ComboBox_H4LOOT) == "已发现/侦察 大麻（室内/室外）")
            {
                TextBox_AppendText_MP("_H4LOOT_WEED_I", "-1");
                TextBox_AppendText_MP("_H4LOOT_WEED_C", "-1");
                TextBox_AppendText_MP("_H4LOOT_WEED_I_SCOPED", "-1");
                TextBox_AppendText_MP("_H4LOOT_WEED_C_SCOPED", "-1");
                TextBox_AppendText_MP("_H4LOOT_WEED_V", "145000");
            }
            else
            {
                if (CheckBox_H4LOOT_Random.IsChecked == false)
                {
                    TextBox_AppendText_MP("_H4LOOT_WEED_I", "0");
                    TextBox_AppendText_MP("_H4LOOT_WEED_C", "0");
                    TextBox_AppendText_MP("_H4LOOT_WEED_I_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_WEED_C_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_WEED_V", "0");
                }
            }

            /**************************** 可可 ****************************/
            if (GetSelectedComboBoxItemContent(ComboBox_H4LOOT) == "已发现/侦察 可可（室内/室外）")
            {
                TextBox_AppendText_MP("_H4LOOT_COKE_I", "-1");
                TextBox_AppendText_MP("_H4LOOT_COKE_C", "-1");
                TextBox_AppendText_MP("_H4LOOT_COKE_I_SCOPED", "-1");
                TextBox_AppendText_MP("_H4LOOT_COKE_C_SCOPED", "-1");
                TextBox_AppendText_MP("_H4LOOT_COKE_V", "220000");
            }
            else
            {
                if (CheckBox_H4LOOT_Random.IsChecked == false)
                {
                    TextBox_AppendText_MP("_H4LOOT_COKE_I", "0");
                    TextBox_AppendText_MP("_H4LOOT_COKE_C", "0");
                    TextBox_AppendText_MP("_H4LOOT_COKE_I_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_COKE_C_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_COKE_V", "0");
                }
            }

            /**************************** 黄金 ****************************/
            if (GetSelectedComboBoxItemContent(ComboBox_H4LOOT) == "已发现/侦察 黄金（室内/室外）")
            {
                TextBox_AppendText_MP("_H4LOOT_GOLD_I", "-1");
                TextBox_AppendText_MP("_H4LOOT_GOLD_C", "-1");
                TextBox_AppendText_MP("_H4LOOT_GOLD_I_SCOPED", "-1");
                TextBox_AppendText_MP("_H4LOOT_GOLD_C_SCOPED", "-1");
                TextBox_AppendText_MP("_H4LOOT_GOLD_V", "320000");
            }
            else
            {
                if (CheckBox_H4LOOT_Random.IsChecked == false)
                {
                    TextBox_AppendText_MP("_H4LOOT_GOLD_I", "0");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_C", "0");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_I_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_C_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_GOLD_V", "0");
                }
            }

            //////////////////////////

            /**************************** 画作 ****************************/
            if (GetSelectedComboBoxItemContent(ComboBox_H4LOOT_PAINT) == "已发现/侦察 画作（室内/室外）")
            {
                TextBox_AppendText_MP("_H4LOOT_PAINT", "-1");
                TextBox_AppendText_MP("_H4LOOT_PAINT_SCOPED", "-1");
                TextBox_AppendText_MP("_H4LOOT_PAINT_V", "180000");
            }
            else
            {
                if (CheckBox_H4LOOT_Random.IsChecked == false)
                {
                    TextBox_AppendText_MP("_H4LOOT_PAINT", "0");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_SCOPED", "0");
                    TextBox_AppendText_MP("_H4LOOT_PAINT_V", "0");
                }
            }

            //////////////////////////

            if (GetSelectedComboBoxItemContent(ComboBox_H4_MISSIONS) == "潜水艇：虎鲸")
            {
                TextBox_AppendText_MP("_H4_MISSIONS", "65283");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4_MISSIONS) == "飞机：阿尔科诺斯特")
            {
                TextBox_AppendText_MP("_H4_MISSIONS", "65413");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4_MISSIONS) == "飞机：梅杜莎")
            {
                TextBox_AppendText_MP("_H4_MISSIONS", "65289");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4_MISSIONS) == "直升机：隐形歼灭者")
            {
                TextBox_AppendText_MP("_H4_MISSIONS", "65425");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4_MISSIONS) == "船只：巡逻艇")
            {
                TextBox_AppendText_MP("_H4_MISSIONS", "65313");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4_MISSIONS) == "船只：长鳍")
            {
                TextBox_AppendText_MP("_H4_MISSIONS", "65345");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4_MISSIONS) == "全部载具可用")
            {
                TextBox_AppendText_MP("_H4_MISSIONS", "65535");
            }

            //////////////////////////

            if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_WEAPONS) == "侵略者（连发散弹，连发手枪，手雷，砍刀）")
            {
                TextBox_AppendText_MP("_H4CNF_WEAPONS", "1");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_WEAPONS) == "阴谋者（军用步枪，单发手枪，粘弹，指虎）")
            {
                TextBox_AppendText_MP("_H4CNF_WEAPONS", "2");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_WEAPONS) == "神枪手（轻狙，连发手枪，燃烧瓶，小刀）")
            {
                TextBox_AppendText_MP("_H4CNF_WEAPONS", "3");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_WEAPONS) == "破坏者（MK2冲锋枪，单发手枪，土质炸弹，小刀）")
            {
                TextBox_AppendText_MP("_H4CNF_WEAPONS", "4");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_WEAPONS) == "神射手（MK2突击步枪，单发手枪，土质炸弹，砍刀）")
            {
                TextBox_AppendText_MP("_H4CNF_WEAPONS", "5");
            }

            //////////////////////////

            if (CheckBox_H4CNF_WEP_DISRP.IsChecked == true)
            {
                TextBox_AppendText_MP("_H4CNF_WEP_DISRP", "3");
            }
            if (CheckBox_H4CNF_ARM_DISRP.IsChecked == true)
            {
                TextBox_AppendText_MP("_H4CNF_ARM_DISRP", "3");
            }
            if (CheckBox_H4CNF_HEL_DISRP.IsChecked == true)
            {
                TextBox_AppendText_MP("_H4CNF_HEL_DISRP", "3");
            }

            //////////////////////////

            if (CheckBox_H4CNF_GRAPPEL.IsChecked == true)
            {
                TextBox_AppendText_MP("_H4CNF_GRAPPEL", "-1");
            }
            if (CheckBox_H4CNF_UNIFORM.IsChecked == true)
            {
                TextBox_AppendText_MP("_H4CNF_UNIFORM", "-1");
            }
            if (CheckBox_H4CNF_BOLTCUT.IsChecked == true)
            {
                TextBox_AppendText_MP("_H4CNF_BOLTCUT", "-1");
            }

            //////////////////////////

            if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_TROJAN) == "机场")
            {
                TextBox_AppendText_MP("_H4CNF_TROJAN", "1");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_TROJAN) == "北船坞")
            {
                TextBox_AppendText_MP("_H4CNF_TROJAN", "2");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_TROJAN) == "主码头-东")
            {
                TextBox_AppendText_MP("_H4CNF_TROJAN", "3");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_TROJAN) == "主码头-西")
            {
                TextBox_AppendText_MP("_H4CNF_TROJAN", "4");
            }
            else if (GetSelectedComboBoxItemContent(ComboBox_H4CNF_TROJAN) == "混合粉")
            {
                TextBox_AppendText_MP("_H4CNF_TROJAN", "5");
            }

            //////////////////////////

            if (CheckBox_H4_PLAYTHROUGH_STATUS.IsChecked == true)
            {
                TextBox_AppendText_MP("_H4_PLAYTHROUGH_STATUS", "10");
            }
        }
        else if (RadioButton_H4CNF_P2.IsChecked == true)
        {
            if (CheckBox_ResetEverything.IsChecked == true)
            {
                TextBox_AppendText_MP("_H4_MISSIONS", "0");
                TextBox_AppendText_MP("_H4_PROGRESS", "0");
                TextBox_AppendText_MP("_H4_PLAYTHROUGH_STATUS", "0");
                TextBox_AppendText_MP("_H4CNF_APPROACH", "0");
                TextBox_AppendText_MP("_H4CNF_BS_ENTR", "0");
                TextBox_AppendText_MP("_H4CNF_BS_GEN", "0");
            }
        }

        WirtePreviewGTAHaxStat();
    }

    private void Button_ImportGTAHax_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        if (!ProcessUtil.IsAppRun("GTAHax"))
            ProcessUtil.OpenProcess("GTAHax", false);

        Task.Run(() =>
        {
            bool isRun = false;
            do
            {
                if (ProcessUtil.IsAppRun("GTAHax"))
                {
                    isRun = true;

                    var pGTAHax = Process.GetProcessesByName("GTAHax").ToList()[0];

                    bool isShow = false;
                    do
                    {
                        IntPtr Menu_handle = pGTAHax.MainWindowHandle;
                        IntPtr child_handle = Win32.FindWindowEx(Menu_handle, IntPtr.Zero, "Static", null);
                        child_handle = Win32.FindWindowEx(Menu_handle, child_handle, "Static", null);
                        child_handle = Win32.FindWindowEx(Menu_handle, child_handle, "Static", null);
                        child_handle = Win32.FindWindowEx(Menu_handle, child_handle, "Static", null);

                        child_handle = Win32.FindWindowEx(Menu_handle, child_handle, "Edit", null);
                        child_handle = Win32.FindWindowEx(Menu_handle, child_handle, "Edit", null);

                        child_handle = Win32.FindWindowEx(Menu_handle, child_handle, "Button", null);
                        child_handle = Win32.FindWindowEx(Menu_handle, child_handle, "Button", null);

                        child_handle = Win32.FindWindowEx(Menu_handle, child_handle, "Button", null);

                        if (child_handle != IntPtr.Zero)
                        {
                            isShow = true;

                            Win32.SendMessage(child_handle, Win32.WM_LBUTTONDOWN, IntPtr.Zero, null);
                            Win32.SendMessage(child_handle, Win32.WM_LBUTTONUP, IntPtr.Zero, null);

                            this.Dispatcher.Invoke(() =>
                            {
                                NotifierHelper.Show(NotifierType.Success, "导入到GTAHax成功！代码正在执行，请返回GTAHax和GTA5游戏查看\n如果执行成功游戏内会出现大受好评奖章");
                            });
                        }
                        else
                        {
                            isShow = false;
                        }

                        Task.Delay(100).Wait();
                    } while (!isShow);
                }
                else
                {
                    isRun = false;
                }

                Task.Delay(100).Wait();
            } while (!isRun);
        });
    }
    #endregion
}
