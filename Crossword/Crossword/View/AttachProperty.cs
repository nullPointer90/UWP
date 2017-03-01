using Windows.UI.Xaml;

namespace Crossword.View
{
    public class AttachProperty
    {
        public static readonly DependencyProperty NormalImageProperty =
            DependencyProperty.RegisterAttached("NormalImage", typeof(string), typeof(AttachProperty), new PropertyMetadata(string.Empty));
        public static void SetNormalImage(DependencyObject obj, string value)
        {
            obj.SetValue(NormalImageProperty, value);
        }

        public static string GetNormalImage(DependencyObject obj)
        {
            return (string)obj.GetValue(NormalImageProperty);
        }
    }
}
