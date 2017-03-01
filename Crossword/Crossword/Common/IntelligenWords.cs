using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Common
{
    class IntelligenWords : IWords
    {
        private List<char> _listDataChars;

        public IntelligenWords()
        {
            _listDataChars = new List<char>();
        }

        public override string GetRnd()
        {
            StringBuilder strData = new StringBuilder();
            Random rnd = new Random();
            int i = 0;
            while(i < GetNumberChar())
            {
                int index = rnd.Next(0, _listDataChars.Count);
                char val = _listDataChars.ElementAt<char>(index);
                if(strData.ToString().Contains(val))
                    continue;
                strData.Append(val);
                i++;
            }
            return strData.ToString();
        }

        public override void Initial()
        {
            for(char c = 'A'; c <= 'Z'; c++)
            {
                _listDataChars.Add(c);
            }
        }

        private int GetNumberChar()
        {
            int numbers = 0;
            switch(_difficult)
            {
                case (int)GameDef.Difficult.EASY:
                    numbers = 8;
                    break;
                case (int)GameDef.Difficult.MEDIUM:
                    numbers = 8;
                    break;
                case (int)GameDef.Difficult.DIFFICULT:
                    numbers = 8;
                    break;
                default:
                    numbers = 8;
                    break;
            }
            return numbers;
        }

    }
}
