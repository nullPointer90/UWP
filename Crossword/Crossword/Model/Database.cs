using Crossword.Common;
using System.Collections.Generic;

namespace Crossword.Model
{
    class Database
    {
        private IDatabase _database;

        public Database(IDatabase database)
        {
            _database = database;
        }

        public void Connect(string connect)
        {
            _database?.Connect(connect);
        }

        public List<string> GetData(GameDef.Difficult difficult)
        {
            return _database?.GetData(difficult);
        }
    }
}
