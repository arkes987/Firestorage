using System;
using System.Windows;
using System.Windows.Media;

namespace Firestorage.Converters
{
    public class IntToColorConverter : MarkuExtensionConverterBase<IntToColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var type = (int)value;

            switch (type)
            {
                case 1:
                    return new SolidColorBrush(Color.FromRgb(51, 102, 255));
                case 2:
                    return new SolidColorBrush(Color.FromRgb(255, 102, 0));
                case 3:
                    return new SolidColorBrush(Color.FromRgb(255, 51, 0));
                case 0:
                default:
                    return new SolidColorBrush(Color.FromRgb(76, 175, 80));
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
