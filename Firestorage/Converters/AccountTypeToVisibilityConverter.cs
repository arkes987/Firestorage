using Firestorage.Enums;
using System;
using System.Windows;

namespace Firestorage.Converters
{
    public class AccountTypeToVisibilityConverter : MarkuExtensionConverterBase<AccountTypeToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var type = (int)value;
            var controlType = (ModifyControlType)parameter;

            switch(controlType)
            {
                case ModifyControlType.LoginBox:
                    if (type == 0)
                        return Visibility.Visible;
                    return Visibility.Collapsed;
                case ModifyControlType.PasswordBox:
                    if (type == 0 || type == 1)
                        return Visibility.Visible;
                    return Visibility.Collapsed;
                case ModifyControlType.NoteBox:
                    if (type == 2 || type == 3)
                        return Visibility.Visible;
                    return Visibility.Collapsed;
                default:
                    return Visibility.Collapsed;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
