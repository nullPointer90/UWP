using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couple.Model
{
    interface IData
    {
        void CreateData();
        List<string> GetListRndData(int counts);
    }
}
