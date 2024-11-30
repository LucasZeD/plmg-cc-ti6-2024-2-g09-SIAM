using System;
using Avalonia.Data.Converters;

namespace PlaneDetectionv2.Converters;

public class InverseBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool boolean)
        {
            return !boolean;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool boolean)
        {
            return !boolean;
        }

        return false;
    }
}