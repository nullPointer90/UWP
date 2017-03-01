using Crossword.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Model
{
    interface IDatabase
    {
        void Connect(string connection);
        List<string> GetData(GameDef.Difficult difficult);
    }
}
