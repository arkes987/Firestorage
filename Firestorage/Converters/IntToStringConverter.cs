using System;

namespace Firestorage.Converters
{
    public class IntToStringConverter : MarkuExtensionConverterBase<IntToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var type = (int)value;

            return type switch
            {
                1 => "Password",
                2 => "Config",
                3 => "Note",
                _ => "Account",
            };
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
