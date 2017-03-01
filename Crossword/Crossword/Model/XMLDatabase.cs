using Crossword.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Crossword.Model
{
    class XMLDatabase : IDatabase
    {
        private XDocument _document;
        public XMLDatabase()
        {
        }
        public void Connect(string connection)
        {
            _document = XDocument.Load(connection);
        }

        public List<string> GetData(GameDef.Difficult difficult)
        {
            string strNote = GetStrTag(difficult);
            List<string> listWords = new List<string>();
            var listNotes = _document?.Descendants(strNote);
            foreach(var item in listNotes)
            {
                listWords.Add(item.Value);
            }
            return listWords;
        }

        private string GetStrTag(GameDef.Difficult difficult)
        {
            string strNote = string.Empty;
            switch(difficult)
            {
                case GameDef.Difficult.EASY:
                    strNote = "word";
                    break;
                case GameDef.Difficult.MEDIUM:
                    strNote = "word1";
                    break;
                case GameDef.Difficult.DIFFICULT:
                    strNote = "word2";
                    break;
                default:
                    strNote = "word";
                    break;
            }
            return strNote;
        }
    }
}
