﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SecondApp.Common
{
    public class EditedUserConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool edit)
            {
                // In case we need to hide some elements from UI
                if (parameter is string str && str == "Inverted")
                    edit = !edit;

                if (targetType == typeof(object))
                {
                    return edit ? "Save" : "Edit";
                }
                else if (targetType == typeof(Visibility))
                {
                    return edit ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            //throw new InvalidCastException("Type of cast is not supported");
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}