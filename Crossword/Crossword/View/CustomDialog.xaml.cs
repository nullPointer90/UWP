using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Crossword.View
{
    public sealed partial class CustomDialog : ContentDialog
    {
        private string _user;
        public string User
        {
            get { return _user; }
            private set { _user = value; }
        }

        private int _res;
        public int Res
        {
            get { return _res; }
            private set { _res = value; }
        }

        public CustomDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            User = tbUser.Text;
            if(string.IsNullOrWhiteSpace(User))
                return;
            Res = 10;
            this.Hide();
        }
    }
}
