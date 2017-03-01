using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Common
{
    class KeyboardMng : BindableBase
    {
        private string _key;
        public string Key
        {
            get { return _key; }
            set { SetProperty(ref _key,value); }
        }

        private double _opacity;
        public double Opacity
        {
            get { return _opacity; }
            set { SetProperty(ref _opacity, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        public KeyboardMng()
        {
            _isSelected = false;
            _opacity = 1;
        }
    }
}
