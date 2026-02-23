using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia;

namespace AvaloniaApplication1.Converters
{
    public class BoolToBorderThicknessConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b && b)
                return new Thickness(3);
            return new Thickness(1);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
