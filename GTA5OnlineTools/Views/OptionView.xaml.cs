using GTA5OnlineTools.Config;
using GTA5OnlineTools.Utils;

namespace GTA5OnlineTools.Views;

/// <summary>
/// OptionView.xaml 的交互逻辑
/// </summary>
public partial class OptionView : UserControl
{
    /// <summary>
    /// Option配置文件集，以json格式保存到本地
    /// </summary>
    private OptionConfig OptionConfig { get; set; } = new();

    public OptionView()
    {
        InitializeComponent();
        this.DataContext = this;
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        // 如果配置文件不存在就创建
        if (!File.Exists(FileUtil.F_OptionConfig_Path))
        {
            OptionConfig.ClickAudioIndex = AudioUtil.ClickAudioIndex;
            RadioButton_ClickAudio_3.IsChecked = true;
            // 保存配置文件
            SaveConfig();
        }

        // 如果配置文件存在就读取
        if (File.Exists(FileUtil.F_OptionConfig_Path))
        {
            using var streamReader = new StreamReader(FileUtil.F_OptionConfig_Path);
            OptionConfig = JsonUtil.JsonDese<OptionConfig>(streamReader.ReadToEnd());

            AudioUtil.ClickAudioIndex = OptionConfig.ClickAudioIndex;

            switch (AudioUtil.ClickAudioIndex)
            {
                case 0:
                    RadioButton_ClickAudio_0.IsChecked = true;
                    break;
                case 1:
                    RadioButton_ClickAudio_1.IsChecked = true;
                    break;
                case 2:
                    RadioButton_ClickAudio_2.IsChecked = true;
                    break;
                case 3:
                    RadioButton_ClickAudio_3.IsChecked = true;
                    break;
                case 4:
                    RadioButton_ClickAudio_4.IsChecked = true;
                    break;
                case 5:
                    RadioButton_ClickAudio_5.IsChecked = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 主窗口关闭事件
    /// </summary>
    private void MainWindow_WindowClosingEvent()
    {
        SaveConfig();
    }

    /// <summary>
    /// 超链接请求导航事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessUtil.OpenPath(e.Uri.OriginalString);
        e.Handled = true;
    }

    /// <summary>
    /// 保存配置文件
    /// </summary>
    private void SaveConfig()
    {
        if (Directory.Exists(FileUtil.D_Config_Path))
        {
            OptionConfig.ClickAudioIndex = AudioUtil.ClickAudioIndex;

            // 写入到Json文件
            File.WriteAllText(FileUtil.F_OptionConfig_Path, JsonUtil.JsonSeri(OptionConfig));
        }
    }

    /// <summary>
    /// 点击音效切换点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RadioButton_ClickAudio_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_ClickAudio_0.IsChecked == true)
        {
            AudioUtil.ClickAudioIndex = 0;
        }
        else if (RadioButton_ClickAudio_1.IsChecked == true)
        {
            AudioUtil.ClickAudioIndex = 1;
        }
        else if (RadioButton_ClickAudio_2.IsChecked == true)
        {
            AudioUtil.ClickAudioIndex = 2;
        }
        else if (RadioButton_ClickAudio_3.IsChecked == true)
        {
            AudioUtil.ClickAudioIndex = 3;
        }
        else if (RadioButton_ClickAudio_4.IsChecked == true)
        {
            AudioUtil.ClickAudioIndex = 4;
        }
        else if (RadioButton_ClickAudio_5.IsChecked == true)
        {
            AudioUtil.ClickAudioIndex = 5;
        }

        AudioUtil.PlayClickSound();
    }
}
