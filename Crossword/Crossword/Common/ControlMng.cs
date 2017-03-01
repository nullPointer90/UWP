using Windows.UI.Xaml;

namespace Crossword.Common
{
    class ControlMng : BindableBase
    {
        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
        }

        private Visibility _visibleBkgndGameOver;
        public Visibility VisibleBkgndGameOver
        {
            get { return _visibleBkgndGameOver; }
            set { SetProperty(ref _visibleBkgndGameOver, value); }
        }

        public ControlMng()
        {
            _visibleBkgndGameOver = Visibility.Collapsed;
            _imagePath = GameDef.IMAGE_PLAY_PATH;
        }
    }
}
