using Crossword.Command;
using Crossword.Common;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Crossword.ViewModel
{
    class IntelligenVM
    {
        private ObservableCollection<CharacterMng> _listData;
        public ObservableCollection<CharacterMng> ListData
        {
            get { return _listData; }
            set { _listData = value; }
        }
        private Game _game;
        public Game GameCrossword
        {
            get { return _game; }
            set { _game = value; }
        }

        private ControlMng _controlMng;
        public ControlMng ControlMng
        {
            get { return _controlMng; }
            set { _controlMng = value; }
        }

        public IntelligenVM()
        {
            InitListData();
            _game = new Game(new IntelligenGame(),new Words(new IntelligenWords()));
            _game.Init();
            _controlMng = new ControlMng();
            _controlMng.ImagePath = GameDef.IMAGE_LOCK_PATH;
            _game.SetEventHandler(GameOver, NextLevel);
            _game.StartTimer();
        }

        private void GameOver()
        {
            _game.StopTimer();
            _game.GameOver();
            _controlMng.VisibleBkgndGameOver = Windows.UI.Xaml.Visibility.Visible;
        }

        private async void NextLevel(int level)
        {
            _game.StopTimer();
            _controlMng.ImagePath = GameDef.IMAGE_UNLOCK_PATH;
            await Task.Delay(1000);
            InitListData();
            _game.NextLevel();
            _controlMng.ImagePath = GameDef.IMAGE_LOCK_PATH;
            _game.StartTimer();
        }

        private void InitListData()
        {
            if (_listData == null)
                _listData = new ObservableCollection<CharacterMng>();
            _listData.Clear();
            for (int i = 0; i < 3; i++)
            {
                _listData.Add(new CharacterMng());
            }
        }

        #region Command
        private DelegateCommand _btnUnlockClick;
        public DelegateCommand BtnUnlockClick
        {
            get
            {
                return _btnUnlockClick = _btnUnlockClick ?? new DelegateCommand(
                    param =>
                    {
                        StringBuilder strBuild = new StringBuilder();
                        foreach(var item in _listData)
                        {
                            if(string.IsNullOrWhiteSpace(item.Val))
                            {
                                var popup = new MessageDialog("Please input password !");
                                popup.Commands.Add(new UICommand("OK") { Id = 0} );
                                popup.DefaultCommandIndex = 0;
                                popup.ShowAsync();
                                return;
                            }
                            strBuild.Append(item.Val?.ToUpper());
                        }
                        _game.Process(strBuild.ToString());

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
                        InitListData();
                        _game.Init();
                        _controlMng.VisibleBkgndGameOver = Windows.UI.Xaml.Visibility.Collapsed;
                        _game.StartTimer();
                    });
            }
        }
        #endregion
    }
}
