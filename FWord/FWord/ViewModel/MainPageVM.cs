using FWord.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FWord.ViewModel
{
    class MainPageVM
    {
        public event Action RefresAdControlHandler;
        public event Action ShowAdsEventHandler;
        private TimerMng _timerMng;
        public TimerMng TimerMng
        {
            get { return _timerMng; }
            set { _timerMng = value; }
        }

        private PlayMng _player;
        public PlayMng Player
        {
            get { return _player; }
            set { _player = value; }
        }

        private AttibuteControl _bkgndCotrol;
        public AttibuteControl BkgndControl
        {
            get { return _bkgndCotrol; }
            set { _bkgndCotrol = value; }
        }
        public MainPageVM()
        {
            _player = new PlayMng();
            _timerMng = new TimerMng();
            _bkgndCotrol = new AttibuteControl();
            _player.PreDataGame();
            _bkgndCotrol.Visible = Windows.UI.Xaml.Visibility.Collapsed;
            _timerMng.CoundownEventHandler += ProcessCoundownEventHandler;
            _player.GameOverEventHandler += GameOverEventHandler;
        }

        #region Command
        private DelegateCommand _btnLetterClick;
        public DelegateCommand BtnLetterClick
            => _btnLetterClick = _btnLetterClick ?? new DelegateCommand(
                    param =>
                    {
                        var c = param as string;
                        int indexListOpacity = char.Parse(c) - 65;
                        if (_player.ListOpacityCharacters[indexListOpacity].Opacity == 1)
                        {
                            ProcessGame(c);
                        }
                        else
                        {
                            UpdateStatus("ms-appx:///Assets/block.png");
                        }
                    });

        private DelegateCommand _btnPlayClick;
        public DelegateCommand BtnPlayClick
            => _btnPlayClick = _btnPlayClick ?? new DelegateCommand
            (
                param =>
                {
                    StartGame();
                }
                );
        #endregion

        #region Event handler
        private void ProcessCoundownEventHandler()
        {
            _timerMng.Reset();
            _player.UpdateTimes(false);
        }

        private void GameOverEventHandler()
        {
            _timerMng.Stop();
            EndGame("Try again !");
            for (int i = 0; i < _player.ListCharacterFind.Count; i++)
            {
                if (_player.ListCharacterFind[i].CharOpacity.Opacity == 0)
                {
                    _player.ListCharacterFind[i].CharOpacity.Opacity = 1;
                    _player.ListCharacterFind[i].CharOpacity.Color = "Red";
                }
            }
        }

        private void EndGame(string result, bool isNextLevel = false)
        {
            if(isNextLevel)
            {
                _player.Level++;
            }
            else
            {
                ShowAdsEventHandler?.Invoke();
                _player.Level = 1;
            }
            _player.StateGame = (int)PlayMng.State.STOP;
            _timerMng.Stop();
            _bkgndCotrol.Text = result;
            _bkgndCotrol.Visible = Windows.UI.Xaml.Visibility.Visible;
            RefresAdControlHandler?.Invoke();
        }
        #endregion

        public void ProcessGame(string cstr)
        {
            if (string.IsNullOrWhiteSpace(cstr))
                return;
            List<int> listPosChar = _player.GetPosCharacterInWord(cstr);
            _timerMng.Reset();
            if (listPosChar.Count > 0)
            {
                UpdateStatus("ms-appx:///Assets/check.png");
                foreach (var item in listPosChar)
                {
                    _player.ListCharacterFind[item].CharOpacity.Opacity = 1;
                    _player.ListCharacterFind[item].CharOpacity.Color = "#FAFAFA";
                }
                if (_player.IsWinner())
                {
                    EndGame("Next Level !",true);
                }
            }
            else
            {
                UpdateStatus("ms-appx:///Assets/fail.png");
                _player.UpdateTimes(false);
            }
            int indexListOpacity = char.Parse(cstr) - 65;
            _player.ListOpacityCharacters[indexListOpacity].Opacity = 0;            
        }

        public int GetStateGame()
        {
            return _player.StateGame;
        }

        public void StartGame()
        {
            _player.StartGame();
            _timerMng.Reset();
            _bkgndCotrol.Visible = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void UpdateStatus(string imagePath)
        {
            _player.PathImageStatus = imagePath;
            _player.OpacityStatus = 1;
        }
    }
}
