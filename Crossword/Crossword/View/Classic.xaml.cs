using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Crossword.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Classic : Page
    {
        public Classic()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += Classic_BackRequested;
        }

        private void Classic_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFram = Window.Current.Content as Frame;
            if(rootFram.CanGoBack)
            {
                rootFram.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }
    }
}
