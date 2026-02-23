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
                return new SolidColorBrush(Color.Parse("#ABEDAD")); // enabled - light green border
            return new SolidColorBrush(Color.Parse("#5A5A5A")); // disabled - darker gray border
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
