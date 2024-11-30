using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace PlaneDetectionv2.Converters;

public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool boolean)
        {
            return boolean ? Avalonia.Controls.Visibility.Visible : Avalonia.Controls.Visibility.Collapsed;
        }
        return Avalonia.Controls.Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is Avalonia.Controls.Visibility visibility && visibility == Avalonia.Controls.Visibility.Visible;
    }
}