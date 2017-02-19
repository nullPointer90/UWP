using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FWord.Model
{
    class XMLDatabase : IDatabase
    {
        private XDocument _doc = null;
        public void Connect(string strConnect)
        {
           _doc = XDocument.Load(strConnect);
        }

        public List<string> ReadData()
        {
            if (_doc != null)
            {
                List<string> listData = new List<string>();
                var listNote = _doc.Descendants("word")?.ToList();
                foreach (var item in listNote)
                {
                    listData.Add(item.Value);
                }
                return listData;
            }
            return null;
        }
    }
}
