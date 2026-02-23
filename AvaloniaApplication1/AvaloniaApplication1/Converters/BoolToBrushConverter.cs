using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace AvaloniaApplication1.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b && b)
                return new SolidColorBrush(Color.Parse("#62BD67")); // enabled - green
            // disabled levels: slightly darker gray for border/background
            return new SolidColorBrush(Color.Parse("#7A7A7A")); // disabled - darker gray
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
