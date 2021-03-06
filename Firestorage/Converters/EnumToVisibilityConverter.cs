﻿using System;
using System.Windows;

namespace Firestorage.Converters
{
    public class EnumToVisibilityConverter : MarkuExtensionConverterBase<EnumToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            var parameterValue = Enum.Parse(value.GetType(), parameter.ToString());

            return parameterValue.Equals(value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Enum.Parse(targetType, parameter.ToString());
        }
    }
}
