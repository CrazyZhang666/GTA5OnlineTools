namespace GTA5OnlineTools.UI.Controls;

public class LoadingSpinner : Control
{
    /// <summary>
    /// 是否显示加载动画
    /// </summary>
    public bool IsLoading
    {
        get { return (bool)GetValue(IsLoadingProperty); }
        set { SetValue(IsLoadingProperty, value); }
    }
    public static readonly DependencyProperty IsLoadingProperty =
        DependencyProperty.Register("IsLoading", typeof(bool), typeof(LoadingSpinner), new PropertyMetadata(false));

    /// <summary>
    /// 圆弧直径
    /// </summary>
    public double Diameter
    {
        get { return (double)GetValue(DiameterProperty); }
        set { SetValue(DiameterProperty, value); }
    }
    public static readonly DependencyProperty DiameterProperty =
        DependencyProperty.Register("Diameter", typeof(double), typeof(LoadingSpinner), new PropertyMetadata(100.0));

    /// <summary>
    /// 圆弧边框厚度
    /// </summary>
    public double Thickness
    {
        get { return (double)GetValue(ThicknessProperty); }
        set { SetValue(ThicknessProperty, value); }
    }
    public static readonly DependencyProperty ThicknessProperty =
        DependencyProperty.Register("Thickness", typeof(double), typeof(LoadingSpinner), new PropertyMetadata(1.0));

    /// <summary>
    /// 圆弧边框颜色
    /// </summary>
    public Brush Color
    {
        get { return (Brush)GetValue(ColorProperty); }
        set { SetValue(ColorProperty, value); }
    }
    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register("Color", typeof(Brush), typeof(LoadingSpinner), new PropertyMetadata(Brushes.Black));

    /// <summary>
    /// 圆弧两端形状
    /// </summary>
    public PenLineCap Cap
    {
        get { return (PenLineCap)GetValue(CapProperty); }
        set { SetValue(CapProperty, value); }
    }
    public static readonly DependencyProperty CapProperty =
        DependencyProperty.Register("Cap", typeof(PenLineCap), typeof(LoadingSpinner), new PropertyMetadata(PenLineCap.Flat));
}
