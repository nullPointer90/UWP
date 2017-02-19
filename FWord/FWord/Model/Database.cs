using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWord.Model
{
    class Database
    {
        private IDatabase _database = null;

        public Database(IDatabase database)
        {
            _database = database;
        }

        public void Connect(string connect)
        {
            if (_database != null)
            {
                _database.Connect(connect);
            }
        }

        public List<string> ReadData()
        {
            if (_database != null)
            {
                return _database.ReadData();
            }
            return null;
        }
    }
}
