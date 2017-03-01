using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Common
{
    class ClassicGame : IGame
    {
        public ClassicGame()
        {
            _gameMng = new GameMng();
        }

        public override void InitGame()
        {
            base.InitGame();
            _gameMng.Life = (int)GameDef.LifeDefault.CLASSIC_LIFE;
            _gameMng.Count = GameDef.COUNT_DOWN;
            InitListData();
            InitListOpaciLife();
            GetHighScore((int)GameDef.Type.CLASSIC);
        }

        public override void CalScores()
        {
            _gameMng.Scores += _gameMng.Life;
        }

        public override void NextLevel()
        {
            CalScores();           
            InitListData();
            InitListOpaciLife();
            _gameMng.Life = (int)GameDef.LifeDefault.CLASSIC_LIFE;
        }

        public override void StartGame()
        {
            _gameMng.State = (int)GameDef.State.START;
        }

        public override void ProcessGame(string str)
        {
            List<int> listIndex = GetAllIndexofChar(str);
            if(listIndex == null || listIndex.Count == 0)
            {
                _gameMng.Count = GameDef.COUNT_DOWN;
                _gameMng.Life--;
                CalLifeOffset();
            } 
            else if(listIndex.Count > 0)
            {
                UpdataListData(listIndex);
                _gameMng.Count = GameDef.COUNT_DOWN;
            }          
            if(IsGameOver())
            {
                GameOverEvent?.Invoke();
                return;
            }

            if(IsNextLevel())
            {
                NextLeveEvent?.Invoke(_gameMng.Level++);
                return;
            }
        }

        public override void GameOver()
        {
            _gameMng.State = (int)GameDef.State.STOP;
            ShowAllCharacters();
            UpdateHighScore((int)GameDef.Type.CLASSIC);
        }

        private void InitListData()
        {
            if(_listCharactersCorrect == null)
                _listCharactersCorrect = new ObservableCollection<CharacterMng>();
            _listCharactersCorrect.Clear();
            for(int i = 0; i < _dataWords.Length; i++ )
            {
                _listCharactersCorrect.Add(new CharacterMng() { Val = _dataWords[i].ToString().ToUpper() });
            }
        }

        private List<int> GetAllIndexofChar(string str)
        {
            if(_listCharactersCorrect == null)
                return null;
            List<int> res = new List<int>();
            for(int i = 0; i < _listCharactersCorrect.Count; i++)
            {
                if(_listCharactersCorrect.ElementAt(i).Val == str)
                    res.Add(i);
            }
            return res;
        }

        private void UpdataListData(List<int> listIndex)
        {
            if(listIndex == null)
                return;
            foreach(var item in listIndex)
            {
                _listCharactersCorrect[item].IsCorrect = true;
                _listCharactersCorrect[item].Opacity = 1;
                _listCharactersCorrect[item].Color = GameDef.COLOR_SELECT;
            }
        }

        private void ShowAllCharacters()
        {
            for(int i = 0; i < _listCharactersCorrect.Count; i++)
            {
                if(_listCharactersCorrect.ElementAt(i).IsCorrect == false)
                {
                    _listCharactersCorrect[i].Opacity = 1;
                    _listCharactersCorrect[i].Color = GameDef.COLOR_NOT_SELECT;
                }
            }
        }

        public override void TimerTick()
        {
            _gameMng.Count--;
            if(_gameMng.Count < 0)
            {
                _gameMng.Life--;
                CalLifeOffset();
                if(IsGameOver())
                {
                    GameOverEvent?.Invoke();
                }
                _gameMng.Count = GameDef.COUNT_DOWN;
            }
        }

        private void InitListOpaciLife()
        {
            if(_gameMng.ListOpaciLife == null)
            {
                _gameMng.ListOpaciLife = new List<OpacityLife>();
                for(int i = 0; i < 5; i++)
                {
                    _gameMng.ListOpaciLife.Add(new OpacityLife());
                }
            }
            for(int i = 0; i < 5; i++)
            {
                _gameMng.ListOpaciLife[i].Opacity = 0;
            }
        }

        private void CalLifeOffset()
        {
            switch(_gameMng.Life)
            {
                case 4:
                    _gameMng.ListOpaciLife[0].Opacity = 1;
                    break;
                case 3:
                    _gameMng.ListOpaciLife[1].Opacity = 1;
                    break;
                case 2:
                    _gameMng.ListOpaciLife[2].Opacity = 1;
                    break;
                case 1:
                    _gameMng.ListOpaciLife[3].Opacity = 1;
                    break;
                case 0:
                    _gameMng.ListOpaciLife[4].Opacity = 1;
                    break;
                default:
                    break;
            }
        }
    }
}
