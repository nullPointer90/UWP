using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couple.ViewModel
{
    class GameDef
    {
        public const int SIZE_5 = 5;
        public const int SIZE_7 = 7;
        public const int SIZE_9 = 9;
        public const int WIDTH_MATRIX_5 = 340;
        public const int WIDTH_MATRIX_7 = 478;
        public const int WIDTH_MATRIX_9 = 616;
        public const string COLOR_ITEM_NORMAL = "#966F33";
        public const string COLOR_ITEM_SELECTED = "LightBlue";

        public const int TIME_5 = 60;
        public const int TIME_7 = 80;
        public const int TIME_9 = 120;

        public enum Type
        {
            NONE,
            CHARACTER,
            COLOR
        }

        public enum State
        {
            NONE,
            PLAY,
            STOP

        }

    }
}
