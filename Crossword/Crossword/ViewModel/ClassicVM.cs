using Crossword.Command;
using Crossword.Common;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Crossword.ViewModel
{
    class ClassicVM
    {
        private Game _game;
        public Game GameCrossword
        {
            get { return _game; }
            set { _game = value; }
        }
        private ObservableCollection<KeyboardMng> _listCharacterKeyboard;
        public ObservableCollection<KeyboardMng> ListCharacterKeyboard
        {
            get { return _listCharacterKeyboard; }
            set { _listCharacterKeyboard = value; }
        }

        private ControlMng _controlMng;
        public ControlMng ControlMng
        {
            get { return _controlMng; }
            set { _controlMng = value; }
        }
        public ClassicVM()
        {
            _controlMng = new ControlMng();
            _game = new Game(new ClassicGame(),new Words(new ClassicWords()));
            InitListCharacterKeyboard();
            _game.Init();
            _game.SetEventHandler(GameOver,NextLevel);
        }

        private void InitListCharacterKeyboard()
        {
            if(_listCharacterKeyboard == null)
                _listCharacterKeyboard = new ObservableCollection<KeyboardMng>();
            _listCharacterKeyboard.Clear();
            for(char c = 'A'; c <= 'Z'; c++)
            {
                _listCharacterKeyboard.Add(new KeyboardMng() { Key = c.ToString() });
            }
        }

        #region Command
        private DelegateCommand _keyboardTapped;
        public DelegateCommand KeboardTapped
        {
            get
            {
                return _keyboardTapped = _keyboardTapped ?? new DelegateCommand(
                    param =>
                    {
                        if(_game.GetState() != (int)GameDef.State.START)
                            return;
                        KeyboardMng data = ((param as TappedRoutedEventArgs)?.OriginalSource as TextBlock)?.DataContext as KeyboardMng;
                        if(data != null && data.IsSelected == false)
                        {
                            int index = ListCharacterKeyboard.IndexOf(data);
                            ListCharacterKeyboard[index].Opacity = 0;
                            ListCharacterKeyboard[index].IsSelected = true;
                            GameCrossword.Process(data.Key);
                        }
                    });
            }
        }

        private DelegateCommand _btnStartClick;
        public DelegateCommand BtnStartClick
        {
            get
            {
                return _btnStartClick = _btnStartClick ?? new DelegateCommand(
                    param =>
                    {
                        if(_game.GetState() == (int)GameDef.State.START)
                            return;
                        Start();
                    });
            }
        }

        private DelegateCommand _btnReplayClick;
        public DelegateCommand BtnReplayClick
        {
            get
            {
                return _btnReplayClick = _btnReplayClick ?? new DelegateCommand(
                    param =>
                    {
                        _game.Init();
                        InitListCharacterKeyboard();
                        Start();
                    });
            }
        }
        #endregion

        private void Start()
        {
            _game.StartGame();
            _game.StartTimer();
            _controlMng.ImagePath = GameDef.IMAGE_PAUSE_PATH;
            _controlMng.VisibleBkgndGameOver = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private async void NextLevel(int level)
        {
            _game.StopTimer();         
            InitListCharacterKeyboard();
            await Task.Delay(1000);
            _game.NextLevel();
            _game.StartTimer();
        }

        private void GameOver()
        {
            _game.StopTimer();
            _game.GameOver();
            _controlMng.VisibleBkgndGameOver = Windows.UI.Xaml.Visibility.Visible;
            _controlMng.ImagePath = GameDef.IMAGE_PLAY_PATH;
        }
    }
}
