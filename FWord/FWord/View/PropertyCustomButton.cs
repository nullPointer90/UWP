using Windows.UI.Xaml;

namespace FWord.View
{
    public class PropertyCustomButton
    {
        #region Normal image property
        public static readonly DependencyProperty NormalImageProperty =
            DependencyProperty.RegisterAttached("NormalImage", typeof(string), typeof(PropertyCustomButton), new PropertyMetadata(string.Empty));
        public static void SetNormalImage(DependencyObject obj, string value)
        {
            obj.SetValue(NormalImageProperty, value);
        }

        public static string GetNormalImage(DependencyObject obj)
        {
            return (string)obj.GetValue(NormalImageProperty);
        }
        #endregion
    }
}
