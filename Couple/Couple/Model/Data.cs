using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couple.Model
{
    class Data
    {
        private IData _iData; 

        public Data(IData data)
        {
            _iData = data;
        }

        public List<string> GetRndListData(int count)
        {
            return _iData?.GetListRndData(count);  
        }
    }
}
