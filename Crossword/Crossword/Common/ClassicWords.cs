using Crossword.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Common
{
    class ClassicWords : IWords
    {
        private Database _database;
        private List<string> _listDataWords;

        public ClassicWords()
        {
            _database = new Database(new XMLDatabase());
            _database.Connect("WordsData.xml");
        }

        public override void Initial()
        {
            _listDataWords = _database?.GetData((GameDef.Difficult)_difficult);
        }

        public override string GetRnd()
        {
            string result = "string empty";
            if(_listDataWords == null)
                return result;
            Random rnd = new Random();
            int index = rnd.Next(0, _listDataWords.Count - 1);
            try
            {
                result = _listDataWords.ElementAt<string>(index);
            }
            catch(Exception) { }
            return result;
        }
    }
}
