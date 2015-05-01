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
        public string[] ShiftTemplateTableColumns = new string[5] { "id", "date", "start", "end", "tag" };
        public string[] TagTableColumns = new string[1] { "tag" };
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
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
            
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            Execute("CREATE TABLE IF NOT EXISTS userTable (id int NOT NULL, username VARCHAR(50), password VARCHAR(50), name VARCHAR(50),cprNumber VARCHAR(50), phone VARCHAR(50), email VARCHAR(50), tag VARCHAR(1000))");
           // Execute("CREATE TABLE IF NOT EXISTS Shifts (id int NOT NULL, employeeId INT, weekNumber INT");
            Execute("CREATE TABLE IF NOT EXISTS ShiftTable (id int NOT NULL, date VARCHAR(50), start VARCHAR(50), end VARCHAR(50), tag VARCHAR(1000), employeeId INT, weekNumber INT)");
            Execute("CREATE TABLE IF NOT EXISTS ShiftTemplate (id int NOT NULL, date VARCHAR(50), start VARCHAR(50), end VARCHAR(50), tag VARCHAR(1000))");
            Execute("CREATE TABLE IF NOT EXISTS TagTable (tag VARCHAR(1000))");


            Read("SELECT * FROM userTable WHERE id=2", "id", "name");
            readInfo.ForEach(Console.WriteLine);
        }

        public void Execute(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

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

        public string listToString(List<string> inputList)
        {
            string returnString = string.Join(",", inputList.ToArray());
            return (returnString);
        }

        public List<string> stringToList(string inputString)
        {
            List<string> outputList = inputString.Split(',').ToList();
            return (outputList);
        }
    }
}
