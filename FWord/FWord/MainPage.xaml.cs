using FWord.Model;
using FWord.ViewModel;
using Microsoft.Advertising.WinRT.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FWord
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        InterstitialAd myInterstitialAd = null;
        string myAppId = "0af823c3-8ac1-4722-9279-3306242fd09a";
        string myAdUnitId = "336180";

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(1360, 768);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            myInterstitialAd = new InterstitialAd();
            myInterstitialAd.AdReady += MyInterstitialAd_AdReady;
            myInterstitialAd.ErrorOccurred += MyInterstitialAd_ErrorOccurred;
            myInterstitialAd.Completed += MyInterstitialAd_Completed;
            myInterstitialAd.Cancelled += MyInterstitialAd_Cancelled;
            myInterstitialAd.RequestAd(AdType.Video, myAppId, myAdUnitId);
            (DataContext as MainPageVM).ShowAdsEventHandler += MainPage_ShowAdsEventHandler;
            (DataContext as MainPageVM).RefresAdControlHandler += MainPage_RefresAdControlHandler;
        }

        private void MainPage_RefresAdControlHandler()
        {
            adControl.Refresh();
        }

        private void MainPage_ShowAdsEventHandler()
        {
            txtLevel.Visibility = Visibility.Collapsed;
            btnReplay.Visibility = Visibility.Visible;
            if (InterstitialAdState.Ready == myInterstitialAd.State)
            {
                myInterstitialAd.Show();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Window.Current.CoreWindow.KeyDown += Page_KeyDown;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Window.Current.CoreWindow.KeyDown -= Page_KeyDown;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            int state = (DataContext as MainPageVM).GetStateGame();
            VirtualKey key = e.VirtualKey;
            if(key >= VirtualKey.A && key <= VirtualKey.Z && state == (int)PlayMng.State.PLAY)
            {
                string cstr = key.ToString();
                int indexListOpacity = char.Parse(cstr) - 65;
                if ((DataContext as MainPageVM).Player.ListOpacityCharacters[indexListOpacity].Opacity == 1)
                {
                    (DataContext as MainPageVM)?.ProcessGame(cstr);
                }
                else
                {
                    (DataContext as MainPageVM)?.UpdateStatus("ms-appx:///Assets/block.png");
                }
            }
            else if (key == VirtualKey.Enter && state != (int)PlayMng.State.PLAY)
            {
                gridStartGame.Visibility = Visibility.Collapsed;
                btnStart.Visibility = Visibility.Collapsed;
                (DataContext as MainPageVM)?.StartGame();
                btnReplay.Visibility = Visibility.Collapsed;
                txtLevel.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gridStartGame.Visibility = Visibility.Collapsed;
            btnStart.Visibility = Visibility.Collapsed;
            (DataContext as MainPageVM)?.StartGame();
            btnReplay.Visibility = Visibility.Collapsed;
            txtLevel.Visibility = Visibility.Visible;
        }

        void MyInterstitialAd_AdReady(object sender, object e)
        {
            // Your code goes here.
        }

        void MyInterstitialAd_ErrorOccurred(object sender, AdErrorEventArgs e)
        {
            // Your code goes here.
        }

        void MyInterstitialAd_Completed(object sender, object e)
        {
            // Your code goes here.
        }

        void MyInterstitialAd_Cancelled(object sender, object e)
        {
            // Your code goes here.
        }
    }
}
