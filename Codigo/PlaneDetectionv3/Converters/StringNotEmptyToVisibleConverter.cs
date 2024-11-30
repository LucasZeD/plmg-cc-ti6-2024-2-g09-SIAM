using Avalonia.Data.Converters;
using Avalonia;
using System;
using System.Globalization;

namespace PlaneDetectionv2.Converters;

public class StringNotEmptyToVisibleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolean)
        {
            return !boolean;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolean)
        {
            return !boolean;
        }

        return false;
    }
}