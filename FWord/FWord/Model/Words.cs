using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWord.Model
{
    class Words
    {
        private List<string> _words;
        public List<string> ListWords
        {
            get { return _words; }
        }

        private Database _database;

        public Words()
        {
            Initial();
        }

        public void Initial()
        {
            _database = new Database(new XMLDatabase());
            ConnectDB();
            GetData();
        }

        public void ConnectDB()
        {
            _database.Connect("ListWord.xml");
        }

        public void GetData()
        {
            _words = _database.ReadData();
        }

        public string GetRndWord()
        {
            string result = "string empty";
            if (ListWords == null)
                return result;
            Random rnd = new Random();
            int index = rnd.Next(0, ListWords.Count - 1);
            try
            {
                result = ListWords.ElementAt<string>(index);
            }
            catch (Exception) { }
            return result;
        }

        public List<string> CutWord(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;
            return str.Split(' ').ToList();
        }
    }
}
