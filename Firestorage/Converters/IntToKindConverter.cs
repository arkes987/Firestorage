using System;
using System.Collections.Generic;
using System.Text;

namespace Firestorage.Converters
{
    public class IntToKindConverter : MarkuExtensionConverterBase<IntToKindConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var type = (int)value;

            return type switch
            {
                1 => "Password",
                2 => "File",
                3 => "Note",
                _ => "Fingerprint",
            };
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

//file-cog