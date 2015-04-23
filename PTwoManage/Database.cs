using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace PTwoManage
{
    public sealed class Database
    {
        static readonly Database _instance = new Database();
        public string[] userTableColumns = new string[7]{"id", "username", "password", "name", "cprNumber", "phone", "email"};
        SQLiteConnection m_dbConnection;
        public List<string> readInfo;

        public static Database Instance
        {
            get { return _instance; }
        }

        Database()
        {
            readInfo = new List<string>();

            string file = AppDomain.CurrentDomain.BaseDirectory + "MyDatabase.sqlite";
            if (!File.Exists(file))
            {
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
                Execute("CREATE TABLE userTable (id int NOT NULL, username VARCHAR(50), password VARCHAR(50), name VARCHAR(50),cprNumber INT, phone INT, email VARCHAR(50))");
                Execute("CREATE TABLE calender (id int NOT NULL, date INT, day VARCHAR(10), open INT, close INT, holiday INT, tag VARCHAR(1000))");
                Execute("CREATE TABLE shifts (id int NOT NULL, day VARCHAR(10), date INT, week INT, startTime INT, endTime INT, userTable INT)");
                Execute("CREATE TABLE template (id int NOT NULL, day VARCHAR(10), open INT, close INT, tag VARCHAR(1000))");
            }
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            Read("SELECT * FROM userTable WHERE id=2", "id", "name");
            readInfo.ForEach(Console.WriteLine);
        }

        

        /*public Database()
        {
            readInfo = new List<string>();

            string file = AppDomain.CurrentDomain.BaseDirectory + "MyDatabase.sqlite";
            if (!File.Exists(file))
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();
                                                
            //Execute("CREATE TABLE userTable (id int NOT NULL, username VARCHAR(50), password VARCHAR(50), name VARCHAR(50),cprNumber INT, phone INT, email VARCHAR(50))");
            Read("SELECT * FROM userTable WHERE id=2", "id", "name");
            readInfo.ForEach(Console.WriteLine);
        }*/

        public void Execute(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        /*public void UsertableRead(string sql)
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
        }*/

        public void Read(string sql, params string[] elements)
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

        public void SaveCalenderInfoToDatabase(int id, int date, string day, int open, int close, int holiday, string tag)
        {
            string sql = "INSERT INTO userTable (id, date, day, open ,close , holiday , tag) values (" + id + ", " + date + " , '" + day + "', " + open + " , " + close + " , " + holiday + ", '" + tag + "')";
            Database.Instance.Execute(sql);
        }
    }
}
