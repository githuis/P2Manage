﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PTwoManage
{
    // Definition of the Core singelton object
    public sealed class Database
    {
        private Database()
        {
            readInfo = new List<string>();
        }

        //Lists of object contained in the Database instance used to retrieve data from the database file
        private string _companyName;
        public string[] FreeTimeRequestColumns = new string[4] {"start", "end", "text", "username"};
        public string[] HolidayTableColumns = new string[1] {"day"};
        public SQLiteConnection m_dbConnection;
        public List<string> readInfo = new List<string>();
        public string[] ShiftTableColumns = new string[6] {"id", "start", "end", "tag", "employeeName", "weekNumber"};
        public string[] ShiftTemplateTableColumns = new string[4] {"id", "start", "end", "tag"};
        public string[] TagTableColumns = new string[1] {"tag"};

        public string[] userTableColumns = new string[9]
            {"id", "username", "password", "name", "cprNumber", "phone", "email", "tag", "points"};

        // A propterty for the CompanyName field with a setter not alowing the value to be empty
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                if ((value != "") && (value != null))
                {
                    _companyName = value;
                    InitDatabase();
                }
            }
        }

        // A method for retrieving a reference to the Database instance
        public static Database Instance { get; } = new Database();

        // A method for opening the conection to the database file
        private void InitDatabase()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + CompanyName + ".sqlite";
            if (!File.Exists(file))
                SQLiteConnection.CreateFile(CompanyName + ".sqlite");

            m_dbConnection = new SQLiteConnection("Data Source=" + CompanyName + ".sqlite;Version=3;");
            m_dbConnection.Open();

            CreateTables();
        }

        // A method for creating tables in the databasefile
        private void CreateTables()
        {
            Execute(
                "CREATE TABLE IF NOT EXISTS userTable (id INTEGER PRIMARY KEY AUTOINCREMENT, username VARCHAR(50), password VARCHAR(50), name VARCHAR(50), cprNumber CHAR(10), phone CHAR(12), email VARCHAR(50), tag VARCHAR(1000), points VARCHAR(1000))");
            Execute(
                "CREATE TABLE IF NOT EXISTS ShiftTable (id INTEGER PRIMARY KEY AUTOINCREMENT, start VARCHAR(50), end VARCHAR(50), tag VARCHAR(1000), employeeName VARCHAR(50), weekNumber INT)");
            Execute(
                "CREATE TABLE IF NOT EXISTS ShiftTemplate (id INTEGER PRIMARY KEY AUTOINCREMENT, start VARCHAR(50), end VARCHAR(50), tag VARCHAR(1000))");
            Execute("CREATE TABLE IF NOT EXISTS TagTable (tag VARCHAR(1000))");
            Execute(
                "CREATE TABLE IF NOT EXISTS FreeRequestTable (start VARCHAR(50), end VARCHAR(50), text VARCHAR(300), username varchar(50))");
            Execute("CREATE TABLE IF NOT EXISTS HolidayTable (day VARCHAR(50))");
        }

        // A method for executing SQL string in the database
        public void Execute(string sql)
        {
            if (sql.Contains(";"))
            {
                MessageBox.Show("Cannot execute database request, please do not use semicolon ';' anywhere");
                return;
            }

            var command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        // A method for retrieving information from the database file and seperating it acording to a string array
        public void Read(string sql, ref List<string> resultData, params string[] elements)
        {
            resultData.Clear();
            var command = new SQLiteCommand(sql, m_dbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var s = "";
                foreach (var el in elements)
                    s += reader[el] + ",";
                resultData.Add(s);
            }
        }

        // Two methods used for converting Lists to strings and back
        public string ListToString(List<string> inputList)
        {
            var returnString = string.Join(":", inputList.ToArray());
            return returnString;
        }

        public List<string> StringToList(string inputString)
        {
            var outputList = inputString.Split(':').ToList();
            return outputList;
        }
    }
}