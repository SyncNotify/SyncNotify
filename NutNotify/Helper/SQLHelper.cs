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
        private string tableName = "MessageState";
        private SQLiteConnection connection;
        public MessageSQLHelper()
        {
            string connectionString = "Data Source=" + databaseName + ".db;Version=3;";
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            string createTableQuery = "CREATE TABLE IF NOT EXISTS " + tableName + " (Id INTEGER PRIMARY KEY AUTOINCREMENT, FileName TEXT NOT NULL, Displayed INTEGER NOT NULL);";
            SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        



        //删除
        public void Delete(int id) 
        {
            string deleteDataQuery = "DELETE FROM" + tableName + " WHERE Id = @id;";
            SQLiteCommand deleteCommand = new SQLiteCommand(deleteDataQuery, connection);
            deleteCommand.Parameters.AddWithValue("@id", id);
            deleteCommand.ExecuteNonQuery();
        }


        //增添
        public void Add(String fileName, int displayed)
        {
            string insertDataQuery = "INSERT INTO" + tableName + " (FileName) VALUES (@fileName), (Displayed) VALUES (@displayed);";
            SQLiteCommand command = new SQLiteCommand(insertDataQuery, connection);
            command.Parameters.AddWithValue("@fileName", fileName);
            command.Parameters.AddWithValue("@displayed", displayed);
            command.ExecuteNonQuery();
        }

        //更新
        public void Update(int id, String fileName, int displayed)
        {
            string updateDataQuery = "UPDATE " + tableName + " SET FileName = @newName, Displayed = @displayed  WHERE Id = @id;";
            SQLiteCommand updateCommand = new SQLiteCommand(updateDataQuery, connection);
            updateCommand.Parameters.AddWithValue("@newName", fileName);
            updateCommand.Parameters.AddWithValue("@id", id);
            updateCommand.ExecuteNonQuery();
        }

        ////查询
        //public void Read(string key)
        //{
        //    string selectDataQuery = "SELECT * FROM " + tableName + ";";
        //    SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection);
        //    SQLiteDataReader reader = command.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        int id = reader.GetInt32(0);
        //        string name = reader.GetString(1);
        //        Console.WriteLine($"Id: {id}, Name: {name}");
        //    }

        //    reader.Close();
        //}
    }
}
