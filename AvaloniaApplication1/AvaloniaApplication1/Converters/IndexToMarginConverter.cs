using Avalonia;
using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace AvaloniaApplication1.Converters
{
    public class IndexToMarginConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int index)
            {
                // Striedavé posunutie: párne indexy hore, nepárne dole
                // Horizontal margin: 50px na?avo a napravo (vä?ší rozostup)
                // Vertical margin: striedavo 40px hore/140px dole alebo 140px hore/40px dole (vä?ší vertikálny rozostup)
                if (index % 2 == 0)
                {
                    // Párny index - button viac hore
                    return new Thickness(50, 40, 50, 140);
                }
                else
                {
                    // Nepárny index - button viac dole
                    return new Thickness(50, 140, 50, 40);
                }
            }
            
            // Default margin
            return new Thickness(50, 90, 50, 90);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
