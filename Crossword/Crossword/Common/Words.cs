using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Common
{
    class Words
    {
        private IWords _word;
        public Words(IWords words)
        {
            _word = words;
        }

        public void Init()
        {
            _word?.Initial();
        }

        public string GetRndString()
        {
            return _word?.GetRnd();
        }

        public void SetDifficult(GameDef.Difficult difficult)
        {
            _word.SetDifficult(difficult);
        }
    }
}
