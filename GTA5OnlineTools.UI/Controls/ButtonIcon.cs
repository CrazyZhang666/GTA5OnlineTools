namespace GTA5OnlineTools.UI.Controls;

public class ButtonIcon : Button
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
        DependencyProperty.Register("Icon", typeof(string), typeof(ButtonIcon), new PropertyMetadata(default));

    /// <summary>
    /// Title标题
    /// </summary>
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(ButtonIcon), new PropertyMetadata(default));
}
