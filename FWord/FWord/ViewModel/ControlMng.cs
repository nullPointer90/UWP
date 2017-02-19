using FWord.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace FWord.ViewModel
{
    class AttibuteControl : BindableBase
    {
        private int _opacity;
        public int Opacity
        {
            get { return _opacity; }
            set {
                SetProperty(ref _opacity, value);
            }
        }

        private Visibility _visible;
        public Visibility Visible
        {
            get { return _visible; }
            set { SetProperty(ref _visible, value); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        private string _color;
        public string Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }
    }
}
