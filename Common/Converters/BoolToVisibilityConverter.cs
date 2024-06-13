using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SecondApp.Common.Converters
{
    /// <summary>
    /// Realization of BoolToVisibilityConverter from WPF
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var resultValue = Visibility.Collapsed;

            if (value is bool boolValue && boolValue is true)
            {
                resultValue = Visibility.Visible;
            }
            return resultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool resultValue = false;

            if (value is Visibility visibility)
            {
                resultValue = visibility == Visibility.Visible;
            }
            return resultValue;
        }
    }
}
