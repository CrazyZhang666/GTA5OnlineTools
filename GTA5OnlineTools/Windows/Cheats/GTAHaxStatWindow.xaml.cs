using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Helper;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Client;

namespace GTA5OnlineTools.Windows.Cheats;

/// <summary>
/// GTAHaxStatWindow.xaml 的交互逻辑
/// </summary>
public partial class GTAHaxStatWindow
{
    public GTAHaxStatWindow()
    {
        InitializeComponent();
    }

    private void Window_GTAHaxStat_Loaded(object sender, RoutedEventArgs e)
    {
        TextBox_PreviewGTAHax.Text = "INT32\n";

        // STAT列表
        foreach (var item in StatData.StatDataClass)
        {
            ListBox_GTAHaxClass.Items.Add(item.Name);
        }
        ListBox_GTAHaxClass.SelectedIndex = 0;
    }

    private void Window_GTAHaxStat_Closing(object sender, CancelEventArgs e)
    {

    }

    private void TextBox_AppendText_MP(string str, string value)
    {
        TextBox_PreviewGTAHax.AppendText($"$MPx{str}\n");
        TextBox_PreviewGTAHax.AppendText($"{value}\n");
    }

    private void TextBox_AppendText_NoMP(string str, string value)
    {
        TextBox_PreviewGTAHax.AppendText($"${str}\n");
        TextBox_PreviewGTAHax.AppendText($"{value}\n");
    }

    private void ListBox_GTAHaxClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var statClassName = ListBox_GTAHaxClass.SelectedItem.ToString();
        int index = StatData.StatDataClass.FindIndex(t => t.Name == statClassName);
        if (index != -1)
        {
            TextBox_PreviewGTAHax.Clear();
            TextBox_PreviewGTAHax.AppendText("INT32\n");

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
        }
    }

    private void Button_ImportGTAHax_Click(object sender, RoutedEventArgs e)
    {
        

        try
        {
            File.WriteAllText(FileUtil.F_GTAHaxStat_Path, TextBox_PreviewGTAHax.Text);
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }

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
}
