using FWord.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWord.ViewModel
{
    class CharaterMng : BindableBase
    {
        private string _val;
        public string Val
        {
            get { return _val; }
            set { SetProperty(ref _val, value); }
        }

        private AttibuteControl _charOpacity;
        public AttibuteControl CharOpacity
        {
            get { return _charOpacity; }
            set { _charOpacity = value; }
        }

        public CharaterMng()
        {
            _charOpacity = new AttibuteControl();
        }
    }
}
