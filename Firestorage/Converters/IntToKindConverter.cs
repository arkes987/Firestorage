using System;

namespace Firestorage.Converters
{
    public class IntToKindConverter : MarkuExtensionConverterBase<IntToKindConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var type = (int)value;

            return type switch
            {
                1 => "Fingerprint",
                2 => "File",
                3 => "Notebook",
                _ => "Account",
            };
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}