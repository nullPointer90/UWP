using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI;

namespace Couple.Model
{
    class DataColors : IData
    {
        public DataColors()
        {
            CreateData();
        }

        public void CreateData()
        {
        }

        public List<string> GetListRndData(int counts)
        {
            List<string> res = new List<string>();
            Random rnd = new Random();
            Byte[] rndBytes = new Byte[3];
            while(res.Count < counts)
            {
                rnd.NextBytes(rndBytes);
                Color color = Color.FromArgb(0, rndBytes[0], rndBytes[1], rndBytes[2]);
                string strColor = Color2String(color);
                if (res.Contains(strColor) || strColor == "#000000")
                    continue;
                res.Add(strColor);
            }
            return res;
        }

        private string Color2String(Color color)
        {
            string res = color.ToString();
            res = res.Substring(3);
            res = res.Insert(0, "#");
            return res;
        }
    }
}
