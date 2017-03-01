using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Crossword.Common
{
    public class CharacterMng : BindableBase
    {
        private string _val;
        public string Val
        {
            get { return _val; }
            set { SetProperty(ref _val, value); }
        }

        private bool _isCorrect;
        public bool IsCorrect
        {
            get { return _isCorrect; }
            set { SetProperty(ref _isCorrect, value); }
        }

        private string _color;
        public string Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        private double _opacity;
        public double Opacity
        {
            get { return _opacity; }
            set { SetProperty(ref _opacity, value); }
        }

        public CharacterMng()
        {
        }
    }
}
