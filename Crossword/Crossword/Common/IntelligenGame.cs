using System.Text;

namespace Crossword.Common
{
    class IntelligenGame : IGame
    { 
        private IntelligenObject _intelligenObj;
        public  IntelligenObject IntellidenObj
        {
            get { return _intelligenObj; }
            set { _intelligenObj = value; }
        }
        public IntelligenGame()
        {
            _gameMng = new GameMng();
            _intelligenObj = new IntelligenObject();
        }

        public override void InitGame()
        {
            base.InitGame();
            InitListData();
            _gameMng.Life = (int)GameDef.LifeDefault.INTELLIGEN_LIFE;
            _gameMng.Count = 1;
            GetHighScore((int)GameDef.Type.INTELLIGEN);
        }

        public override void CalScores()
        {
            if(_gameMng.Count <= 20)
            {
                _gameMng.Scores += 10;
            }else if(_gameMng.Count > 20 && _gameMng.Count <= 60)
            {
                _gameMng.Scores += 5;
            }
            else
            {
                _gameMng.Scores += 1;
            }

        }

        public override void NextLevel()
        {
            _gameMng.Count = 1;
            CalScores();
            InitListData();
        }

        public override void StartGame()
        {
            GameMng.State = (int)GameDef.State.START;
        }

        public override void ProcessGame(string str)
        {
            StringBuilder strBuild = new StringBuilder();
            foreach (var item in _intelligenObj.ListDataCorrect)
            {
                strBuild.Append(item.ToUpper());
            }
            if (strBuild.ToString().CompareTo(str) == 0)
            {
                NextLeveEvent?.Invoke(_gameMng.Level++);
            }
           else
            {
                GameOverEvent?.Invoke();
            }
        }


        private void InitListData()
        {
            _intelligenObj?.ListData?.Clear();
            for (int i = 0; i < _dataWords.Length; i++)
            {
                _intelligenObj?.ListData?.Add(_dataWords[i].ToString());

            }
            _intelligenObj.InitListData(GameDef.COUNT_CHARACTER_INTEL);
        }

        public override void GameOver()
        {
            GameMng.State = (int)GameDef.State.STOP;
            UpdateHighScore((int)GameDef.Type.INTELLIGEN);
        }

        public override void TimerTick()
        {
            _gameMng.Count++;
        }
    }
}
