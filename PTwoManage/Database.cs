﻿using System;
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
        private string _companyName;
        static readonly Database _instance = new Database();
        public string[] userTableColumns = new string[9]{"id", "username", "password", "name", "cprNumber", "phone", "email", "tag", "points"};
        public string[] ShiftTemplateTableColumns = new string[4] { "id", "start", "end", "tag" };
        public string[] TagTableColumns = new string[1] { "tag" };
        public string[] ShiftTableColumns = new string[6] { "id", "start", "end", "tag", "employeeName", "weekNumber" };
        public string[] FreeTimeRequestColumns = new string[4] { "start", "end", "text", "username" };
        public string[] HolidayTableColumns = new string[1] { "day" };
        public SQLiteConnection m_dbConnection;
        public List<string> readInfo = new List<string>();

        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (value != "" && value != null)
                {
                    _companyName = value;
                    InitDatabase();
                }
            }
        }

        public static Database Instance
        {
            get { return _instance; }
        }

        Database()
        {
            readInfo = new List<string>();

            //InitDatabase();

        }

        private void InitDatabase()
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + CompanyName + ".sqlite";
            if (!File.Exists(file))
                SQLiteConnection.CreateFile(CompanyName + ".sqlite");

            m_dbConnection = new SQLiteConnection("Data Source=" + CompanyName + ".sqlite;Version=3;");
            m_dbConnection.Open();

            CreateTables();
        }

        private void CreateTables()
        {
            Execute("CREATE TABLE IF NOT EXISTS userTable (id INTEGER PRIMARY KEY AUTOINCREMENT, username VARCHAR(50), password VARCHAR(50), name VARCHAR(50), cprNumber VARCHAR(50), phone VARCHAR(50), email VARCHAR(50), tag VARCHAR(1000), points VARCHAR(1000))");
            Execute("CREATE TABLE IF NOT EXISTS ShiftTable (id INTEGER PRIMARY KEY AUTOINCREMENT, start VARCHAR(50), end VARCHAR(50), tag VARCHAR(1000), employeeName VARCHAR(50), weekNumber INT)");
            Execute("CREATE TABLE IF NOT EXISTS ShiftTemplate (id INTEGER PRIMARY KEY AUTOINCREMENT, start VARCHAR(50), end VARCHAR(50), tag VARCHAR(1000))");
            Execute("CREATE TABLE IF NOT EXISTS TagTable (tag VARCHAR(1000))");
            Execute("CREATE TABLE IF NOT EXISTS FreeRequestTable (start VARCHAR(50), end VARCHAR(50), text VARCHAR(300), username varchar(50))");
            Execute("CREATE TABLE IF NOT EXISTS HolidayTable (day VARCHAR(50))");
        }

        public void Execute(string sql)
        {
            if(sql.Contains(";"))
            {
                System.Windows.Forms.MessageBox.Show("Cannot execute database request, please do not use semicolon ';' anywhere");
                return;
            }
            
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        public void Read(string sql, ref List<string> resultData, params string[] elements)
        {
            resultData.Clear();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                string s = "";
                foreach (string el in elements)
                {
                    s += reader[el] + ",";
                }
                resultData.Add(s);
            }
        }

        public string ListToString(List<string> inputList)
        {
            string returnString = string.Join(":", inputList.ToArray());
            return (returnString);
        }

        public List<string> StringToList(string inputString)
        {
            List<string> outputList = inputString.Split(':').ToList();
            return (outputList);
        }
    }
}
