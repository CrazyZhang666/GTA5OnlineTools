namespace GTA5OnlineTools.UI.Controls;

public class UiRadioButtonIcon : RadioButton
{
    /// <summary>
    /// Icon图标
    /// </summary>
    public string Icon
    {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(string), typeof(UiRadioButtonIcon), new PropertyMetadata("\xe63b"));
}
