using System;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace PlaneDetectionv2.Converters;

public class BooleanToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool isDragging && isDragging)
        {
            return Brushes.LightBlue; // Highlight color
        }
        return Brushes.Transparent; // Default color
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}