using FWord.Command;
using FWord.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWord.ViewModel
{
    class PlayMng : BindableBase
    {
        public event Action GameOverEventHandler;

        public enum State
        {
            NONE,
            PLAY,
            STOP
        }

        public const int TIMES = 5;

        private int _stateGame;
        public int StateGame
        {
            get { return _stateGame; }
            set { SetProperty(ref _stateGame, value); }
        }

        private int _times;
        public int Times
        {
            get { return _times; }
            set { SetProperty(ref _times, value); }
        }

        private ObservableCollection<string> _listPathImageTimes;
        public ObservableCollection<string> ListPathImageTimes
        {
            get { return _listPathImageTimes; }
            set { SetProperty(ref _listPathImageTimes, value); }
        }

        private Words _words;

        private string _strFind;
        public string StrFind
        {
            get { return _strFind; }
        }

        private ObservableCollection<AttibuteControl> _listOpacityCharacters;
        public ObservableCollection<AttibuteControl> ListOpacityCharacters
        {
            get { return _listOpacityCharacters; }
            set { SetProperty(ref _listOpacityCharacters, value); }
        }

        private ObservableCollection<CharaterMng> _listCharacterFind;
        public ObservableCollection<CharaterMng> ListCharacterFind
        {
            get { return _listCharacterFind; }
            set { SetProperty(ref _listCharacterFind, value); }
        }

        private int _level;
        public int Level
        {
            get { return _level; }
            set { SetProperty(ref _level, value); }
        }

        private double _opacityStatus;
        public double OpacityStatus
        {
            get { return _opacityStatus; }
            set { SetProperty(ref _opacityStatus, value); }
        }

        private string _pathImageStatus;
        public string PathImageStatus
        {
            get { return _pathImageStatus; }
            set { SetProperty(ref _pathImageStatus, value); }
        }


        public PlayMng()
        {
            Initial();
        }

        public void Initial()
        {
            _stateGame = (int)State.NONE;
            _times = TIMES;
            _words = new Words();
            _level = 1;
            InitListImageTimes();
            InitListOpacity();
        }

        public void InitListImageTimes()
        {
            if (_listPathImageTimes == null)
                _listPathImageTimes = new ObservableCollection<string>();
            if (_listPathImageTimes.Count == GetTimes())
                return;
            _listPathImageTimes.Clear();
            for (int i = 0; i < GetTimes(); i++)
            {
                AddPathImageTime();
            }
        }

        public void InitListOpacity()
        {
            if (_listOpacityCharacters == null)
                _listOpacityCharacters = new ObservableCollection<AttibuteControl>();
            _listOpacityCharacters.Clear();
            for (int i = 0; i < 26; i++)
            {
                _listOpacityCharacters.Add(new AttibuteControl { Opacity = 1});
            }
        }
        private void RemovePathImageTime()
        {
            if (_listPathImageTimes == null || _listPathImageTimes.Count <= 0)
                return;
            try
            {
                _listPathImageTimes.RemoveAt(_times);
            }
            catch (ArgumentOutOfRangeException) { }
        }

        private void AddPathImageTime()
        {
            _listPathImageTimes.Add("ms-appx:///Assets/heart.png");
        }
        public void StartGame()
        {
            _stateGame = (int)State.PLAY;
            PreDataGame();
        }

        public void PreDataGame()
        {
            _strFind = _words.GetRndWord().ToUpper();
            _times = GetTimes();
            AddCharToList(_strFind);
            InitListImageTimes();
            InitListOpacity();
        }

        public void GameOver()
        {
            _stateGame = (int)State.STOP;
            GameOverEventHandler?.Invoke();
        }

        public void UpdateTimes(bool isFind, bool isAdd = false)
        {
            if (isAdd)
            {
                _times++;
                AddPathImageTime();
            }
            if (!isFind)
            {
                _times--;
                RemovePathImageTime();
                if (_times <= 0)
                {
                    GameOver();
                    return;
                }
            }
        }
        public List<int> GetPosCharacterInWord(string c)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < StrFind.Length; i++)
            {
                if (c == _strFind[i].ToString())
                {
                    res.Add(i);
                }
            }
            return res;
        }

        private void AddCharToList(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return;
            if (_listCharacterFind == null)
                _listCharacterFind = new ObservableCollection<CharaterMng>();
            _listCharacterFind.Clear();
            for (int i = 0; i < str.Length; i++)
            {
                _listCharacterFind.Add(new CharaterMng() { Val = str[i].ToString() });
            }
        }

        public bool IsWinner()
        {
            foreach (var item in _listCharacterFind)
            {
                if (item.CharOpacity.Opacity == 0 &&  !string.IsNullOrWhiteSpace(item.Val))
                    return false;
            }
            return true;
        }

        private int GetTimes()
        {
            int res = TIMES + _level - 1;
            if (res > 8)
                res = 8;
            return res;
        }
    }
}
