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
            string file = @"C:\Users\LucaBianchi\GitHub\P2Manage\PTwoManage\server";
            if (!File.Exists(file))
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();
                                                
            Execute("CREATE TABLE userTable (id int NOT NULL PRIMARY KEY, username VARCHAR(50), password VARCHAR(50), name VARCHAR(50),cprNumber INT, phone INT, email VARCHAR(50))");
            UsertableRead("SELECT * FROM userTable WHERE id=2");
            readInfo.ForEach(Console.WriteLine);
        }

        public void Execute(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        public void UsertableRead(string sql)
        {
            string s;
            readInfo.Clear();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                s = reader["id"] + " , " + reader["username"] + " , " + reader["password"] + " , " + reader["name"] + " , " + reader["cprNumber"] + " , " + reader["phone"] + " , " + reader["email"];
                readInfo.Add(s);
            }
        }

        public void Read(string sql, string[] elements)
        {
            readInfo.Clear();
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

        public void CreateTable()
        {
            

        
        }
        public void FillUserTable(User user)
        {
            string str = "INSERT INTO userTable (id, username, password, name ,cprNumber, phone , email) values (" + user.Id + ", '" + user.UserName + "', '" + user.Password + "', '" + user.Name + "' , "+ user.CprNumber +" , " + user.Phone + ", '" + user.Email + "')";
            Console.WriteLine(str);
            Execute(str);
                
        }
    }
}
