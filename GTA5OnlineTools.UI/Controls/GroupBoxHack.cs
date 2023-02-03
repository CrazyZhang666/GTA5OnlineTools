namespace GTA5OnlineTools.UI.Controls;

public class GroupBoxHack : GroupBox
{
    /// <summary>
    /// Hack图片
    /// </summary>
    public string Image
    {
        get { return (string)GetValue(ImageProperty); }
        set { SetValue(ImageProperty, value); }
    }
    public static readonly DependencyProperty ImageProperty =
        DependencyProperty.Register("Image", typeof(string), typeof(GroupBoxHack), new PropertyMetadata(default));

    /// <summary>
    /// Hack标题
    /// </summary>
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(GroupBoxHack), new PropertyMetadata(default));

    /// <summary>
    /// Hack描述
    /// </summary>
    public string Description
    {
        get { return (string)GetValue(DescriptionProperty); }
        set { SetValue(DescriptionProperty, value); }
    }
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register("Description", typeof(string), typeof(GroupBoxHack), new PropertyMetadata(default));

    /// <summary>
    /// Hack标题内容
    /// </summary>
    public UIElement TitleContent
    {
        get { return (UIElement)GetValue(TitleContentProperty); }
        set { SetValue(TitleContentProperty, value); }
    }
    public static readonly DependencyProperty TitleContentProperty =
        DependencyProperty.Register("TitleContent", typeof(UIElement), typeof(GroupBoxHack), new PropertyMetadata(default));

    /// <summary>
    /// Hack描述内容
    /// </summary>
    public UIElement DescriptionContent
    {
        get { return (UIElement)GetValue(DescriptionContentProperty); }
        set { SetValue(DescriptionContentProperty, value); }
    }
    public static readonly DependencyProperty DescriptionContentProperty =
        DependencyProperty.Register("DescriptionContent", typeof(UIElement), typeof(GroupBoxHack), new PropertyMetadata(default));
}
