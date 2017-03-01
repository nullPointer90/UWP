using Crossword.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Common
{
    abstract class IGame
    {       
        protected string _dataWords;       
        public Action GameOverEvent;
        public Action<int> NextLeveEvent;
        protected ObservableCollection<CharacterMng> _listCharactersCorrect = null;
        public virtual ObservableCollection<CharacterMng> ListCharactersCorrect
        {
            get { return _listCharactersCorrect; }
            set { _listCharactersCorrect = value; }
        }

        protected GameMng _gameMng;
        public GameMng GameMng
        {
            get { return _gameMng; }
            set { _gameMng = value; }
        }

        
        public virtual void InitGame()
        {
            _gameMng.Level = 1;
            _gameMng.Scores = 0;
            _gameMng.State = (int)GameDef.State.NONE;
            try
            {
                HighScoreMng.CreateFileName("score.xml");
            }
            catch(Exception) { }
        }
        public abstract void CalScores();
        public abstract void NextLevel();
        public abstract void StartGame();
        public abstract void ProcessGame(string str);
        public abstract void GameOver();
        public abstract void TimerTick();
 
        public bool IsGameOver()
        {
            if(_gameMng.Life <= 0)
                return true;
            return false;
        }

        public bool IsNextLevel()
        {
            if(_listCharactersCorrect == null)
                return false;
            foreach(var item in _listCharactersCorrect)
            {
                if(item.IsCorrect == false)
                    return false;
            }
            return true;
        }

        public int GetState()
        {
            return (int)_gameMng.State;
        }
        public void SetDataWords(string data)
        {
            _dataWords = data;
        }

        public GameDef.Difficult GetDifficult()
        {
           if(_gameMng.Level <= 5 )
            {
                return GameDef.Difficult.EASY;
            }else if(_gameMng.Level <= 10 && _gameMng.Level > 5)
            {
                return GameDef.Difficult.MEDIUM;
            }
           else
            {
                return GameDef.Difficult.DIFFICULT;
            }
        }

        public bool IsHighScore(Score score,int typeGame)
        {
            if(_gameMng.Scores == 0)
                return false;
            if(score == null)
                return true;
            if(typeGame == (int)GameDef.Type.CLASSIC)
            {
                if(_gameMng.Scores > score.ScoreClassic)
                    return true;
            }
            else if(typeGame == (int)GameDef.Type.INTELLIGEN)
            {
                if(_gameMng.Scores > score.ScoreIntelligen)
                    return true;
            }         
            return false;
        }

        public async void GetHighScore(int typeGame)
        {
            Score score = null;
            try
            {
                score = await HighScoreMng.ReadObjectFromXmlFileAsync<Score>("score.xml");
                if(typeGame == (int)GameDef.Type.CLASSIC)
                {
                    _gameMng.Name = score.NameClassic;
                    _gameMng.HighScore = score.ScoreClassic;
                }
                else if(typeGame == (int)GameDef.Type.INTELLIGEN)
                {
                    _gameMng.Name = score.NameIntelligen;
                    _gameMng.HighScore = score.ScoreIntelligen;
                }
            }
            catch(Exception)
            {
               
            }
        }

        public async void UpdateHighScore(int typeGame)
        {
            Score score = null;
            try
            {
                score = await HighScoreMng.ReadObjectFromXmlFileAsync<Score>("score.xml");
            }
            catch(Exception)
            {
            }
            if(IsHighScore(score,typeGame))
            {
                var dialog = new CustomDialog();
                await dialog.ShowAsync();
                if(dialog.Res == 10)
                {
                    if(score == null)
                        score = new Score();
                    if(typeGame == (int)GameDef.Type.CLASSIC)
                    {
                        score.ScoreClassic = _gameMng.Scores;
                        score.NameClassic = dialog.User;
                    }
                    else if(typeGame == (int)GameDef.Type.INTELLIGEN)
                    {
                        score.ScoreIntelligen = _gameMng.Scores;
                        score.NameIntelligen = dialog.User;
                    }
                    try
                    {
                        await HighScoreMng.SaveObjectToXml<Score>(score, "score.xml");
                        GetHighScore(typeGame);
                    }
                    catch(Exception)
                    {
                    }
                }
            }
        }
    }
}
