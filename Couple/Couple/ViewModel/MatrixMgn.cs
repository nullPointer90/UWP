using Couple.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couple.ViewModel
{
    class MatrixMgn : BindableBase
    {
        private int _widthItem;
        public int WidthItem
        {
            get { return _widthItem; }
            set { SetProperty(ref _widthItem, value); }
        }

        private int _widthMatrix;
        public int WidthMatrix
        {
            get { return _widthMatrix; }
            set { SetProperty(ref _widthMatrix, value); }
        }

        private int _sizeMatrix;
        public int SizeMatrix
        {
            get { return _sizeMatrix; }
            set { _sizeMatrix = value; }
        }
    }
}
