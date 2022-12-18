using GTA5OnlineTools.API;
using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Helper;
using GTA5OnlineTools.Features.Core;

using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5OnlineTools.Windows.Cheats;

/// <summary>
/// KiddionChsWindow.xaml 的交互逻辑
/// </summary>
public partial class KiddionChsWindow
{
    public ObservableCollection<KiddionChs> KiddionChsObs { get; set; } = new();

    public KiddionChsWindow()
    {
        InitializeComponent();
    }

    private void Window_KiddionChs_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;
    }

    private void Window_KiddionChs_Closing(object sender, CancelEventArgs e)
    {

    }

    private void Button_GetKiddionUI_Click(object sender, RoutedEventArgs e)
    {
        

        lock (this)
        {
            KiddionChsObs.Clear();

            var pArray = Process.GetProcessesByName("Kiddion");
            if (pArray.Length > 0)
            {
                var main_handle = pArray[0].MainWindowHandle;

                int index = 0;

                var button_handle = Win32.FindWindowEx(main_handle, IntPtr.Zero, "Button", null);
                while (button_handle != IntPtr.Zero)
                {
                    var length = Win32.GetWindowTextLength(button_handle);
                    var builder = new StringBuilder(length + 1);
                    Win32.GetWindowText(button_handle, builder, builder.Capacity);

                    var text = builder.ToString();
                    var split = text.IndexOf("|");
                    if (split != -1)
                        text = text[..split];
                    KiddionChsObs.Add(new KiddionChs()
                    {
                        Index = ++index,
                        Name = text,
                        Value = "中文翻译写这里"
                    });

                    button_handle = Win32.FindWindowEx(main_handle, button_handle, "Button", null);
                }
            }
            else
            {
                NotifierHelper.Show(NotifierType.Warning, "未发现《Kiddion》进程，请先运行《Kiddion》程序");
            }
        }
    }

    private void Button_TranslateSelected_Click(object sender, RoutedEventArgs e)
    {
        

        var value = TextBox_SelectedItem_Vaule.Text.Trim();
        if (!string.IsNullOrEmpty(value))
        {
            var index = DataGrid_KiddionUIs.SelectedIndex;

            KiddionChsObs[index].Value = value;
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "翻译内容为空，操作取消");
        }
    }

    private async void Button_YouDaoTranslateSelected_Click(object sender, RoutedEventArgs e)
    {
        

        var name = TextBox_SelectedItem_Name.Text.Trim();
        if (!string.IsNullOrEmpty(name))
        {
            var index = DataGrid_KiddionUIs.SelectedIndex;

            KiddionChsObs[index].Value = await WebAPI.GetYouDaoContent(name);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "翻译内容为空，操作取消");
        }
    }

    private void Button_TranslateBuild_Click(object sender, RoutedEventArgs e)
    {
        

        TextBox_TranslateBuild.Clear();

        foreach (var item in KiddionChsObs)
        {
            TextBox_TranslateBuild.AppendText($"\t{{ L\"{item.Name}\", L\"{item.Value}\" }},\n");
        }
    }
}

public class KiddionChs : ObservableObject
{
    private int _index;
    /// <summary>
    /// 索引
    /// </summary>
    public int Index
    {
        get => _index;
        set => SetProperty(ref _index, value);
    }

    private string _name;
    /// <summary>
    /// 名称
    /// </summary>
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string _value;
    /// <summary>
    /// 值
    /// </summary>
    public string Value
    {
        get => _value;
        set => SetProperty(ref _value, value);
    }
}
