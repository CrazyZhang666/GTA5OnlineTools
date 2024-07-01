using CommunityToolkit.Mvvm.ComponentModel;
using GTA5Core.Features;
using GTA5Core.GTA.Onlines;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.OnlineWeapon;

/// <summary>
/// WeaponOptionView.xaml 的交互逻辑
/// </summary>
public partial class WeaponOptionView : UserControl
{
    public partial class Options : ObservableObject
    {
        [ObservableProperty] public bool ammoModifier = false;
        [ObservableProperty] public byte ammoModifierFlag = 0;

        [ObservableProperty] public bool fastReload = false;
        [ObservableProperty] public bool noRecoil = false;
        [ObservableProperty] public bool noSpread = false;
        [ObservableProperty] public bool longRange = false;
    }
    public Options WO_Options { get; set; } = new();

    public WeaponOptionView()
    {
        InitializeComponent();
        GTA5MenuWindow.WindowClosingEvent += GTA5MenuWindow_WindowClosingEvent;
        GTA5MenuWindow.LoopSpeedNormalEvent += GTA5MenuWindow_LoopSpeedNormalEvent;

        ReadConfig();

        // 子弹类型
        foreach (var item in OnlineData.ImpactExplosions)
        {
            ListBox_ImpactExplosion.Items.Add(item.Name);
        }
        ListBox_ImpactExplosion.SelectedIndex = 0;
    }

    private void GTA5MenuWindow_WindowClosingEvent()
    {
        GTA5MenuWindow.WindowClosingEvent -= GTA5MenuWindow_WindowClosingEvent;
        GTA5MenuWindow.LoopSpeedNormalEvent -= GTA5MenuWindow_LoopSpeedNormalEvent;
        SaveConfig();
    }

    private void ReadConfig()
    {
        WO_Options.FastReload = IniHelper.ReadValue("WeaponOption", "FastReload").Equals("True", StringComparison.OrdinalIgnoreCase);
        WO_Options.NoRecoil = IniHelper.ReadValue("WeaponOption", "NoRecoil").Equals("True", StringComparison.OrdinalIgnoreCase);
        WO_Options.NoSpread = IniHelper.ReadValue("WeaponOption", "NoSpread").Equals("True", StringComparison.OrdinalIgnoreCase);
        WO_Options.LongRange = IniHelper.ReadValue("WeaponOption", "LongRange").Equals("True", StringComparison.OrdinalIgnoreCase);
    }


    private void SaveConfig()
    {
        IniHelper.WriteValue("WeaponOption", "FastReload", $"{WO_Options.FastReload}");
        IniHelper.WriteValue("WeaponOption", "NoRecoil", $"{WO_Options.NoRecoil}");
        IniHelper.WriteValue("WeaponOption", "NoSpread", $"{WO_Options.NoSpread}");
        IniHelper.WriteValue("WeaponOption", "LongRange", $"{WO_Options.LongRange}");
    }

    /////////////////////////////////////////////////////

    private void GTA5MenuWindow_LoopSpeedNormalEvent()
    {
        // 弹药编辑
        if (WO_Options.AmmoModifier)
            Weapon.AmmoModifier(WO_Options.AmmoModifierFlag);

        // 快速换弹
        if (WO_Options.FastReload)
            Weapon.FastReload(true);
        // 无后坐力
        if (WO_Options.NoRecoil)
            Weapon.NoRecoil();
        // 无子弹扩散
        if (WO_Options.NoSpread)
            Weapon.NoSpread();
        // 提升射程
        if (WO_Options.LongRange)
            Weapon.LongRange();
    }

    /// <summary>
    /// 弹药编辑
    /// </summary>
    private void AmmoModifier()
    {
        var index = ListBox_AmmoModifier.SelectedIndex;
        if (index == -1 || index == 0)
        {
            WO_Options.AmmoModifier = false;
            return;
        }

        WO_Options.AmmoModifier = true;
        WO_Options.AmmoModifierFlag = (byte)(index - 1);
        Weapon.AmmoModifier(WO_Options.AmmoModifierFlag);
    }

    /// <summary>
    /// 子弹类型
    /// </summary>
    private void ImpactExplosion()
    {
        var index = ListBox_ImpactExplosion.SelectedIndex;
        if (index == -1 || index == 0)
            return;

        if (index == 1)
            Weapon.ImpactType(3);
        else
            Weapon.ImpactType(5);

        Weapon.ImpactExplosion(OnlineData.ImpactExplosions[index].Value);
    }

    private void ListBox_AmmoModifier_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        AmmoModifier();
    }

    private void ListBox_AmmoModifier_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        AmmoModifier();
    }

    private void CheckBox_FastReload_Click(object sender, RoutedEventArgs e)
    {
        WO_Options.FastReload = CheckBox_FastReload.IsChecked == true;
        Weapon.FastReload(WO_Options.FastReload);
    }

    private void CheckBox_NoRecoil_Click(object sender, RoutedEventArgs e)
    {
        WO_Options.NoRecoil = CheckBox_NoRecoil.IsChecked == true;
        Weapon.NoRecoil();
    }

    private void CheckBox_NoSpread_Click(object sender, RoutedEventArgs e)
    {
        WO_Options.NoSpread = CheckBox_NoSpread.IsChecked == true;
        Weapon.NoSpread();
    }

    private void CheckBox_LongRange_Click(object sender, RoutedEventArgs e)
    {
        WO_Options.LongRange = CheckBox_LongRange.IsChecked == true;
        Weapon.LongRange();
    }

    private void Button_FillCurrentAmmo_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Weapon.FillCurrentAmmo();
    }

    private void Button_FillAllAmmo_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        Weapon.FillAllAmmo();
    }

    private void ListBox_ImpactExplosion_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ImpactExplosion();
    }

    private void ListBox_ImpactExplosion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ImpactExplosion();
    }
}
