using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncNotify.Helper
{
    internal class MessageSQLHelper
    {
        private string databaseName = "Message";
        public MessageSQLHelper()
        {
            string connectionString = "Data Source=" + databaseName + ".db;Version=3;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string createTableQuery = "CREATE TABLE IF NOT EXISTS MessageState (Id INTEGER PRIMARY KEY AUTOINCREMENT, FileName TEXT NOT NULL, Displayed INTEGER NOT NULL);";
            SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        public Update()
        {

        }
    }
}
