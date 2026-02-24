using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace AvaloniaApplication1.Converters
{
    public class BoolToBorderBrushConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b && b)
                return new SolidColorBrush(Color.Parse("#A8E6AE")); // enabled - light green border
            return new SolidColorBrush(Color.Parse("#A0A0A0")); // disabled - light gray border (lighter than fill)
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
