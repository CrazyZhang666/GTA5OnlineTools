using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;
using GTA5OnlineTools.Features.Client;
using GTA5OnlineTools.Features.Settings;

namespace GTA5OnlineTools.Modules.ExternalMenu;

/// <summary>
/// SpawnWeaponView.xaml 的交互逻辑
/// </summary>
public partial class SpawnWeaponView : UserControl
{
    /// <summary>
    /// 临时特殊字符串
    /// </summary>
    private string tempWeaponPickup = string.Empty;

    public SpawnWeaponView()
    {
        InitializeComponent();
        this.DataContext = this;
        ExternalMenuWindow.WindowClosingEvent += ExternalMenuWindow_WindowClosingEvent;

        // 武器列表
        foreach (var item in WeaponData.WeaponClassData)
        {
            ComboBox_WeaponClass.Items.Add(new EmojiMenu()
            {
                Emoji = item.Emoji,
                Title = item.Name
            });
        }
        ComboBox_WeaponClass.SelectedIndex = 0;

        // 子弹类型
        foreach (var item in MiscData.ImpactExplosions)
        {
            ComboBox_ImpactExplosion.Items.Add(item.Name);
        }
    }

    private void ExternalMenuWindow_WindowClosingEvent()
    {

    }

    private void ComboBox_WeaponClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        lock (this)
        {
            var index = ComboBox_WeaponClass.SelectedIndex;
            if (index != -1)
            {
                ListBox_WeaponInfo.Items.Clear();

                Task.Run(() =>
                {
                    var className = WeaponData.WeaponClassData[index].Name;

                    for (int i = 0; i < WeaponData.WeaponClassData[index].WeaponInfo.Count; i++)
                    {
                        var name = WeaponData.WeaponClassData[index].WeaponInfo[i].Name;
                        var displayName = WeaponData.WeaponClassData[index].WeaponInfo[i].DisplayName;

                        this.Dispatcher.BeginInvoke(DispatcherPriority.Background, () =>
                        {
                            if (index == ComboBox_WeaponClass.SelectedIndex)
                            {
                                ListBox_WeaponInfo.Items.Add(new ModelPreview()
                                {
                                    Id = name,
                                    Name = displayName,
                                    Image = $"\\Assets\\Images\\Client\\Weapons\\{name}.png"
                                });
                            }
                        });
                    }
                });

                ListBox_WeaponInfo.SelectedIndex = 0;
            }
        }
    }

    private void ListBox_WeaponInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var index1 = ComboBox_WeaponClass.SelectedIndex;
        var index2 = ListBox_WeaponInfo.SelectedIndex;
        if (index1 != -1 && index2 != -1)
        {
            tempWeaponPickup = "pickup_" + WeaponData.WeaponClassData[index1].WeaponInfo[index2].Name;
        }
    }

    private void Button_SpawnWeapon_Click(object sender, RoutedEventArgs e)
    {
        

        Hacks.CreateAmbientPickup(tempWeaponPickup);
    }

    ////////////////////////////////////////////////////////////////////////////////////////

    private void RadioButton_AmmoModifier_None_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_AmmoModifier_None.IsChecked == true)
        {
            MenuSetting.Weapon.AmmoModifierFlag = 0;
            Weapon.AmmoModifier(0);
        }
        else if (RadioButton_AmmoModifier_AMMO.IsChecked == true)
        {
            MenuSetting.Weapon.AmmoModifierFlag = 1;
            Weapon.AmmoModifier(1);
        }
        else if (RadioButton_AmmoModifier_CLIP.IsChecked == true)
        {
            MenuSetting.Weapon.AmmoModifierFlag = 2;
            Weapon.AmmoModifier(2);
        }
        else if (RadioButton_AmmoModifier_Both.IsChecked == true)
        {
            MenuSetting.Weapon.AmmoModifierFlag = 3;
            Weapon.AmmoModifier(3);
        }
    }

    private void CheckBox_ReloadMult_Click(object sender, RoutedEventArgs e)
    {
        Weapon.ReloadMult(CheckBox_ReloadMult.IsChecked == true);
    }

    private void Button_NoRecoil_Click(object sender, RoutedEventArgs e)
    {
        

        Weapon.NoRecoil();
    }

    private void CheckBox_NoSpread_Click(object sender, RoutedEventArgs e)
    {
        

        Weapon.NoSpread();
    }

    private void CheckBox_Range_Click(object sender, RoutedEventArgs e)
    {
        

        Weapon.Range();
    }

    private void ComboBox_ImpactExplosion_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var index = ComboBox_ImpactExplosion.SelectedIndex;
        if (index != -1)
        {
            if (index == 0)
                Weapon.ImpactType(3);
            else
                Weapon.ImpactType(5);

            Weapon.ImpactExplosion(MiscData.ImpactExplosions[index].ID);
        }
    }

    private void Button_FillCurrentAmmo_Click(object sender, RoutedEventArgs e)
    {
        

        Weapon.FillCurrentAmmo();
    }

    private void Button_FillAllAmmo_Click(object sender, RoutedEventArgs e)
    {
        

        Weapon.FillAllAmmo();
    }
}
