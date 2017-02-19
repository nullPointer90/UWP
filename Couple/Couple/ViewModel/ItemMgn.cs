using Couple.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Couple.ViewModel
{
    class ItemMgn : BindableBase
    {
        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        private string _color;
        public string Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        private Visibility _visible;
        public Visibility Visible
        {
            get { return _visible; }
            set { SetProperty(ref _visible, value); }
        }

        private Visibility _textVisible;
        public Visibility TextVisible
        {
            get { return _textVisible; }
            set { SetProperty(ref _textVisible, value); }
        }

        private string _bkgndItem;
        public string BkgndItem
        {
            get { return _bkgndItem; }
            set { SetProperty(ref _bkgndItem, value); }
        }

        public ItemMgn()
        {
            _bkgndItem = GameDef.COLOR_ITEM_NORMAL;
            _textVisible = Visibility.Collapsed;
            _visible = Visibility.Visible;
        }
    }
}
