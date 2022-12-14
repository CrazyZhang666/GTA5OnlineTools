namespace GTA5OnlineTools.Utils;

public static class AudioUtil
{
    public static SoundPlayer SP_Click_01 = new(Properties.Resources.Click_01);
    public static SoundPlayer SP_Click_02 = new(Properties.Resources.Click_02);
    public static SoundPlayer SP_Click_03 = new(Properties.Resources.Click_03);
    public static SoundPlayer SP_Click_04 = new(Properties.Resources.Click_04);
    public static SoundPlayer SP_Click_05 = new(Properties.Resources.Click_05);

    public static SoundPlayer SP_GTA5_Email = new(Properties.Resources.GTA5_Email);
    public static SoundPlayer SP_GTA5_Job = new(Properties.Resources.GTA5_Job);
    public static SoundPlayer SP_DownloadOK = new(Properties.Resources.DownloadOK);

    /// <summary>
    /// 点击提示音索引
    /// </summary>
    public static int ClickAudioIndex = 3;

    /// <summary>
    /// 播放点击音效
    /// </summary>
    public static void PlayClickSound()
    {
        switch (ClickAudioIndex)
        {
            case 0:
                break;
            case 1:
                SP_Click_01.Play();
                break;
            case 2:
                SP_Click_02.Play();
                break;
            case 3:
                SP_Click_03.Play();
                break;
            case 4:
                SP_Click_04.Play();
                break;
            case 5:
                SP_Click_05.Play();
                break;
        }
    }
}
