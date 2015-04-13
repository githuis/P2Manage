using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace PTwoManage
{
    class Database
    {
        SQLiteConnection m_dbConnection;
        List<string> readInfo;

        public Database()
        {
            readInfo = new List<string>();
            string file = @"C:\Users\LucaBianchi\GitHub\P2Manage\PTwoManage\bin\Debug";
            if (!File.Exists(file))
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        public void Execute(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        public void Read(string sql, string[] elements)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                string s = "";
                foreach (string el in elements)
                {
                    s += reader[el] + ",";
                }
                readInfo.Add(s);
            }
        }

    }
}
