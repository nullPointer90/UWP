using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWord.Model
{
    interface IDatabase
    {
        void Connect(string strConnect);
        List<string> ReadData();
    }
}
