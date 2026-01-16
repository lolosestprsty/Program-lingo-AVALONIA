using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace AvaloniaApplication1.Converters
{
    public class TypeEqualsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return 0.7;

            var currentVM = value.GetType();
            var targetVM = parameter as Type;

            return currentVM == targetVM ? 1.0 : 0.7;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
