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
        public string[] userTableColumns = new string[7] { "id", "username", "password", "name", "cprNumber", "phone", "email" };
        SQLiteConnection m_dbConnection;
        public List<string> readInfo;
        public Array tagArray;

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

            Execute("CREATE TABLE IF NOT EXISTS userTable (id int NOT NULL, username VARCHAR(50), password VARCHAR(50), name VARCHAR(50), cprNumber VARCHAR(50), phone VARCHAR(50), email VARCHAR(50))");
            Execute("CREATE TABLE IF NOT EXISTS ShiftTemplate (id int NOT NULL, date VARCHAR(50), start VARCHAR(50), end VARCHAR(50), tag VARCHAR(1000))");
            Execute("CREATE TABLE IF NOT EXISTS Shifts (id int NOT NULL, employeeId INT, weekNumber INT");

            stringToList("asdfasdf,asdfasdf,asdfasdf,g");
            Console.WriteLine(tagArray);

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
            while (reader.Read())
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

        public void SaveInfoShiftTemplate(ShiftTemplate input)
        {
            string s="";
            for (int i = 0; i < input.Tag.Count; i++)
            {
                s += input.Tag[i];
            }
            string sql = "INSERT INTO ShiftTemplate (date, start, end, tag) values (" + input.Date + "', '" + input._startTime.ToString("dd, MM, yyyy, HH, mm, ss") + "', '" + input._endTime.ToString("dd, MM, yyyy, HH, mm, ss") + "', '" + s + "')";
            Database.Instance.Execute(sql);
        }

        public void SaveInfoShifts(int employeeId, int weekNumber)
        {
            string sql = "INSERT INTO Shifts (employeeId, weekNumber) values (" + employeeId + ", " + weekNumber + ")";
            Database.Instance.Execute(sql);
        }

        private void stringToList(string String)
        {
            string[] tagArray = String.Split(',');
        }

    }
}