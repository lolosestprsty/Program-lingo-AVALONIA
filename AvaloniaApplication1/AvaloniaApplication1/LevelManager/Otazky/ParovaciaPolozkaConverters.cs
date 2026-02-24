using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace AvaloniaApplication1.LevelManager.Otazky
{
    public class ParovaciaPolozkaConverter : IValueConverter
    {
        public static readonly ParovaciaPolozkaConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isMatched = value is bool matched && matched;
            bool isSelected = parameter is bool selected && selected;

            if (isMatched)
                return new SolidColorBrush(Color.Parse("#5CAD61")); // Green for matched
            if (isSelected)
                return new SolidColorBrush(Color.Parse("#4A90E2")); // Blue for selected

            return new SolidColorBrush(Colors.Gray); // Default gray
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ParovaciaPolozkaThicknessConverter : IValueConverter
    {
        public static readonly ParovaciaPolozkaThicknessConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isMatched = value is bool matched && matched;
            bool isSelected = parameter is bool selected && selected;

            if (isMatched || isSelected)
                return new Avalonia.Thickness(3); // Thicker border for selected/matched

            return new Avalonia.Thickness(2); // Default thickness
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ParovaciaPolozkaBackgroundConverter : IValueConverter
    {
        public static readonly ParovaciaPolozkaBackgroundConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isMatched = value is bool matched && matched;
            bool isSelected = parameter is bool selected && selected;

            if (isMatched)
                return new SolidColorBrush(Color.Parse("#E8F5E9")); // Light green for matched
            if (isSelected)
                return new SolidColorBrush(Color.Parse("#E3F2FD")); // Light blue for selected

            return new SolidColorBrush(Color.Parse("#D9D9D9")); // Default background
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

