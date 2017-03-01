using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Common
{
    abstract class IWords
    {
        protected int _difficult;
        public abstract void Initial();
        public abstract string GetRnd();

        public void SetDifficult(GameDef.Difficult difficult)
        {
            _difficult = (int)difficult;
        }
    }
}
