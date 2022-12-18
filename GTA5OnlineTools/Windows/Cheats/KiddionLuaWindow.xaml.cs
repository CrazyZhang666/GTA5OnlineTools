using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Helper;
using GTA5OnlineTools.GTA.Client;

namespace GTA5OnlineTools.Windows.Cheats;

/// <summary>
/// KiddionLuaWindow.xaml 的交互逻辑
/// </summary>
public partial class KiddionLuaWindow
{
    public KiddionLuaWindow()
    {
        InitializeComponent();
    }

    private void Window_KiddionLua_Loaded(object sender, RoutedEventArgs e)
    {
        // STAT列表
        foreach (var item in StatData.StatDataClass)
        {
            ListBox_StatClass.Items.Add(item.Name);
        }
        ListBox_StatClass.SelectedIndex = 0;
    }

    private void TextBox_AppendLine(string str = "")
    {
        TextBox_PreviewLua.AppendText($"{str}\n");
    }

    private void TextBox_AppendText_MP(string str, string value)
    {
        TextBox_PreviewLua.AppendText($"    stats.set_int(MPx .. \"{str}\", {value})\n");
    }

    private void TextBox_AppendText_NoMP(string str, string value)
    {
        TextBox_PreviewLua.AppendText($"    stats.set_int(\"{str}\", {value})\n");
    }

    private void Window_KiddionLua_Closing(object sender, CancelEventArgs e)
    {

    }

    private void ListBox_StatClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var statName = ListBox_StatClass.SelectedItem.ToString();
        int index = StatData.StatDataClass.FindIndex(t => t.Name == statName);
        if (index != -1)
        {
            TextBox_PreviewLua.Clear();

            TextBox_AppendLine("local sub_menu = menu.add_submenu(\"★简易Lua脚本（小助手自动生成）★\")");
            TextBox_AppendLine();
            TextBox_AppendLine("if stats.get_int(\"MPPLY_LAST_MP_CHAR\") == 0 then");
            TextBox_AppendLine("    MPx = \"MP0_\"");
            TextBox_AppendLine("else");
            TextBox_AppendLine("    MPx = \"MP1_\"");
            TextBox_AppendLine("end");
            TextBox_AppendLine();
            if (statName.Length >= 21)
                TextBox_AppendLine($"sub_menu:add_action(\"{statName[..20]}\", function()");
            else
                TextBox_AppendLine($"sub_menu:add_action(\"{statName}\", function()");

            for (int i = 0; i < StatData.StatDataClass[index].StatInfo.Count; i++)
            {
                var hash = StatData.StatDataClass[index].StatInfo[i].Hash;
                var value = StatData.StatDataClass[index].StatInfo[i].Value;

                if (hash.IndexOf("_") == 0)
                {
                    TextBox_AppendText_MP(hash, value.ToString());
                }
                else
                {
                    TextBox_AppendText_NoMP(hash, value.ToString());
                }
            }

            TextBox_AppendLine("end)");
        }
    }

    private void Button_BulidLua_Click(object sender, RoutedEventArgs e)
    {
        

        try
        {
            File.WriteAllText(FileUtil.D_KiddionScripts_Path + "00AA.lua", TextBox_PreviewLua.Text);
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }
}
