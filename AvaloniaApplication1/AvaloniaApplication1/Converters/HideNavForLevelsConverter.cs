using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AvaloniaApplication1.Converters
{
    public class HideNavForLevelsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
                return true; // show nav when no VM

            // Hide navigation for any view-models that belong to the LevelManager namespace.
            var ns = value.GetType().Namespace ?? string.Empty;
            if (ns.StartsWith("AvaloniaApplication1.LevelManager", StringComparison.Ordinal))
                return false; // hide nav
            
            // Hide navigation for EnterModel
            if (value.GetType().Name == "EnterModel")
                return false; // hide nav
            
            return true; // show nav for other view-models
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}