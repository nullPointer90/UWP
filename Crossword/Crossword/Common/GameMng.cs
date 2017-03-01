using System.Collections.Generic;

namespace Crossword.Common
{
    public class OpacityLife : BindableBase
    {
        private double _opacity;
        public double Opacity
        {
            get { return _opacity; }
            set { SetProperty(ref _opacity, value); }
        }
    }
    public class GameMng : BindableBase
    {
        private int _state;
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        private int _level;
        public int Level
        {
            get { return _level; }
            set { SetProperty(ref _level , value); }
        }

        private int _scores;
        public int Scores
        {
            get { return _scores; }
            set { SetProperty(ref _scores, value); }
       }

        private int _type;
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private int _life;
        public int Life
        {
            get { return _life; }
            set { _life = value; }
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value); }
        }

        private List<OpacityLife> _listOpaciLife;
        public List<OpacityLife> ListOpaciLife
        {
            get { return _listOpaciLife; }
            set { _listOpaciLife = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private int _highScore;
        public int HighScore
        {
            get { return _highScore; }
            set { SetProperty(ref _highScore, value); }
        }
        public GameMng()
        {
            _state = (int)GameDef.State.NONE;
            _level = 1;
            _scores = 0;
        }
    }
}
