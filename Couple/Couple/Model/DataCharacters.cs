using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Couple.Model
{
    class DataCharacters : IData
    {
        private List<string> _listCharacters;

        public DataCharacters()
        {
            CreateData();
        }

        public void CreateData()
        {
            if (_listCharacters == null)
                _listCharacters = new List<string>();
            _listCharacters.Clear();
            _listCharacters.Add("@");
            _listCharacters.Add("#");
            _listCharacters.Add("$");
            _listCharacters.Add("&");
            for (char c = 'A'; c <= 'Z'; c++)
            {
                _listCharacters.Add(c.ToString());
            }
            for (char i = '0'; i <= '9'; i++)
            {
                _listCharacters.Add(i.ToString());
            }
        }

        public List<string> GetListRndData(int counts)
        {
            List<string> res = new List<string>();
            if (counts == 40)
            {
                res = _listCharacters;
                return res;
            }
            Random rnd = new Random();
            int countRes = 0;
            while ((countRes = res.Count) < (counts))
            {
                int indexItem = rnd.Next(0, _listCharacters.Count());
                string str = _listCharacters.ElementAt(indexItem);
                if (res.Contains(str))
                    continue;
                res.Add(str);
            }
            return res;
        }
    }
}
