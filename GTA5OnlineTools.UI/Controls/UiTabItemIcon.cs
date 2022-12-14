﻿namespace GTA5OnlineTools.UI.Controls;

public class UiTabItemIcon : TabItem
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
        DependencyProperty.Register("Icon", typeof(string), typeof(UiTabItemIcon), new PropertyMetadata("\xe63b"));
}
