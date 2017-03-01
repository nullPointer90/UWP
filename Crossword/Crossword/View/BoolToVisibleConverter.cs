using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Crossword.View
{
    public class BoolToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool? isVisible = value as bool?;
            if(isVisible == true)
                return Visibility.Visible;
            return Visibility.Collapsed;
         }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
