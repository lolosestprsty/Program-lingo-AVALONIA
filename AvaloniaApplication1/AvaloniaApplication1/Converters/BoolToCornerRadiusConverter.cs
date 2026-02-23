using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia;

namespace AvaloniaApplication1.Converters
{
    public class BoolToCornerRadiusConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b && b)
                return new CornerRadius(35);
            return new CornerRadius(10);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
