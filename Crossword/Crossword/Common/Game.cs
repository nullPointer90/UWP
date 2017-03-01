using System;

namespace Crossword.Common
{
    class Game
    {
        private TimerMng _timer;
        private Words _words;
        private IGame _game;
        private string _wordData;
        public IGame TypeGame
        {
            get { return _game; }
            set { _game = value; }
        }

        public Game(IGame game,Words words)
        {
            _game = game;
            _words = words;
        }

        public void Init()
        {
            _timer = new TimerMng();
            _words.SetDifficult(GameDef.Difficult.EASY);
            _words.Init();
            _wordData = _words.GetRndString();
            _game?.SetDataWords(_wordData);
            _game?.InitGame();
            _timer.TimerTickEvent += _timer_TimerTickEvent;
        }

        private void _timer_TimerTickEvent()
        {
            _game.TimerTick();
        }

        public void StartGame()
        {
            _game?.StartGame();
        }

        public void Process(string c)
        {
            _game?.ProcessGame(c);
        }

        public int GetState()
        {
            return _game.GetState();
        }

        public void StartTimer()
        {
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }
        public void SetEventHandler(Action gameOverHandler, Action<int> nextLevelHandler)
        {
            _game.GameOverEvent = gameOverHandler;
            _game.NextLeveEvent = nextLevelHandler;
        }

        public void NextLevel()
        {
            _words.SetDifficult(_game.GetDifficult());
            _words.Init();
            _wordData = _words.GetRndString();
            _game?.SetDataWords(_wordData);
            _game.NextLevel();
        }

        public void GameOver()
        {
            _game.GameOver();
        }
    }
}
