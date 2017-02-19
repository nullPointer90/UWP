using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Couple.View
{
    class ConvertBoolToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isChecked = (bool)value;
            if (isChecked)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
