using Couple.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace Couple.ViewModel
{
    class MainpageVM
    {
        private bool isProcessing = false;
        private int _preIndexClick = -1;

        private GameMgn _game;
        public GameMgn Game
        {
            get { return _game; }
            set { Game = value; }
        }

        private TimerMgn _timerData;
        public TimerMgn TimerData
        {
            get { return _timerData; }
            set { _timerData = value; }
        }
        public MainpageVM()
        {
            _game = new GameMgn();
            _timerData = new TimerMgn();
            _timerData.GameOverEventHandler += GameOver;
        }

        #region Command
        private DelegateCommand _sizeChecked;
        public DelegateCommand SizeChecked
        {
            get
            {
                return _sizeChecked = _sizeChecked ?? new DelegateCommand(
                param =>
                {
                    string val = param as string;
                    if (val == "1")
                    {
                        Game.SetSizeMatrix(GameDef.SIZE_5);
                    }
                    else if (val == "2")
                    {
                        Game.SetSizeMatrix(GameDef.SIZE_7);
                    }
                    else if (val == "3")
                    {
                        Game.SetSizeMatrix(GameDef.SIZE_9);
                    }
                    else { }
                    Game.CreateListData();
                    SetTimeCountDown(1);
                });
            }
         }

        private DelegateCommand _typeChecked;
        public DelegateCommand TypeChecked
        {
            get
            {
                return _typeChecked = _typeChecked ?? new DelegateCommand(
                param =>
                {
                    string val = param as string;
                    if (val == "1")
                    {
                        Game.TypeGame = (int)GameDef.Type.CHARACTER;
                    }
                    else if (val == "2")
                    {
                        Game.TypeGame = (int)GameDef.Type.COLOR;
                    }
                    else { }
                    Game.CreateListData();
                });
            }
        }

        private DelegateCommand _itemMTClick;
        public DelegateCommand ItemMTClick
        {
            get
            {
                return _itemMTClick = _itemMTClick ?? new DelegateCommand(
                param =>
                {
                    if (isProcessing)
                        return;
                    //(param as TappedRoutedEventArgs).OriginalSource; ListViewItemPresenter
                    ItemMgn att =  ((param as TappedRoutedEventArgs)?.OriginalSource as Border)?.DataContext as ItemMgn;
                    if(att != null)
                    {
                        int index = Game.ListData.IndexOf(att);
                        ProcessGame(index);
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
                    StartGame();
                }
                );
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
                    Game.VisibleBkgndGameOver = Visibility.Collapsed;
                    Game.CreateListData();
                    SetTimeCountDown(1);
                }
                );
            }
        }
        #endregion

        private void StartGame()
        {
            Game.IsEnableControl = false;
            Game.SetAllListVisible(2000);
            TimerData.Start();
        }

        public async void ProcessGame(int indexClick)
        {
            isProcessing = true;
            if (_preIndexClick == -1)
            {
                ItemMgn data = Game.ListData.ElementAt(indexClick);
                if (IsBoxEmpty(data))
                {
                    //Game over Data
                    isProcessing = false;
                    return;
                }
                _preIndexClick = indexClick;
                Game.ListData[indexClick].TextVisible = Visibility.Visible;
                Game.ListData[indexClick].BkgndItem = GameDef.COLOR_ITEM_SELECTED;
                isProcessing = false;
                return;
            }
            else if (indexClick == _preIndexClick)
            {
                isProcessing = false;
                return;
            }
            Game.ListData[indexClick].TextVisible = Visibility.Visible;
            Game.ListData[indexClick].BkgndItem = GameDef.COLOR_ITEM_SELECTED;
            await Task.Delay(500);
            ItemMgn preData = Game.ListData.ElementAt(_preIndexClick);
            ItemMgn curData = Game.ListData.ElementAt(indexClick);
            if (IsBoxEmpty(curData))
            {
                //Game Over
                isProcessing = false;
                return;
            }
            if (Game.TypeGame == (int)GameDef.Type.CHARACTER)
            {
                if (preData.Content == curData.Content)
                {
                    Game.ListData[_preIndexClick].Visible = Visibility.Collapsed;
                    Game.ListData[indexClick].Visible = Visibility.Collapsed;
                }
                else
                {
                    Game.ListData[_preIndexClick].TextVisible = Visibility.Collapsed;
                    Game.ListData[indexClick].TextVisible = Visibility.Collapsed;
                    Game.ListData[indexClick].BkgndItem = GameDef.COLOR_ITEM_NORMAL;
                    Game.ListData[_preIndexClick].BkgndItem = GameDef.COLOR_ITEM_NORMAL;
                }
                _preIndexClick = -1;
            }
            else if (Game.TypeGame == (int)GameDef.Type.COLOR)
            {
                if (preData.Color == curData.Color)
                {

                }
                else
                {
                    //incorrect
                }
            }
            isProcessing = false;
        }

        public bool IsBoxEmpty(ItemMgn att)
        {
            if (att == null)
                return true;
            if (Game.TypeGame == (int)GameDef.Type.CHARACTER)
            {
                if (string.IsNullOrWhiteSpace(att.Content))
                    return true;
            }
            else if (Game.TypeGame == (int)GameDef.Type.CHARACTER)
            {
                if (att.Color == "#000000")
                    return true;
            }
            return false;
        }

        public void GameOver()
        {
            TimerData.Stop();
            Game.IsEnableControl = true ;
            Game.ShowDataWhenGameOver();
            Game.VisibleBkgndGameOver = Visibility.Visible;
        }
        
        private void SetTimeCountDown(int level)
        {
            switch(Game.MatrixData.SizeMatrix)
            {
                case GameDef.SIZE_5:
                    TimerData.ValCountDown = GameDef.TIME_5 - (level - 1) * 2;
                    if (TimerData.ValCountDown < 30)
                        TimerData.ValCountDown = 30;
                    break;
                case GameDef.SIZE_7:
                    TimerData.ValCountDown = GameDef.TIME_7 - (level - 1) * 2;
                    if (TimerData.ValCountDown < 40)
                        TimerData.ValCountDown = 40;
                    break;
                case GameDef.SIZE_9:
                    TimerData.ValCountDown = GameDef.TIME_9 - (level - 1) * 2;
                    if (TimerData.ValCountDown < 60)
                        TimerData.ValCountDown = 60;
                    break;
                default:
                    TimerData.ValCountDown = GameDef.TIME_5;
                    break;
            }
        }
    }
}
