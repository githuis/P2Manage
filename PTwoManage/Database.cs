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
        static readonly Database _instance = new Database();
        public string[] userTableColumns = new string[9]{"id", "username", "password", "name", "cprNumber", "phone", "email", "tag", "points"};
        public string[] ShiftTemplateTableColumns = new string[4] { "id", "start", "end", "tag" };
        public string[] TagTableColumns = new string[1] { "tag" };
        public string[] ShiftTableColumns = new string[6] { "id", "start", "end", "tag", "employeeName", "weekNumber" };
        public string[] FreeTimeRequestColumns = new string[4] { "start", "end", "text", "userID" };
        public string[] HolidayTableColumns = new string[1] { "day" };
        SQLiteConnection m_dbConnection;
        public List<string> readInfo = new List<string>();

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

            Execute("CREATE TABLE IF NOT EXISTS userTable (id INTEGER PRIMARY KEY AUTOINCREMENT, username VARCHAR(50), password VARCHAR(50), name VARCHAR(50), cprNumber VARCHAR(50), phone VARCHAR(50), email VARCHAR(50), tag VARCHAR(1000), points VARCHAR(1000))");
           // Execute("CREATE TABLE IF NOT EXISTS Shifts (id int NOT NULL, employeeId INT, weekNumber INT");
		   
            Execute("CREATE TABLE IF NOT EXISTS ShiftTable (id INTEGER PRIMARY KEY AUTOINCREMENT, start VARCHAR(50), end VARCHAR(50), tag VARCHAR(1000), employeeName VARCHAR(50), weekNumber INT)");
            Execute("CREATE TABLE IF NOT EXISTS ShiftTemplate (id INTEGER PRIMARY KEY AUTOINCREMENT, start VARCHAR(50), end VARCHAR(50), tag VARCHAR(1000))");
            Execute("CREATE TABLE IF NOT EXISTS TagTable (tag VARCHAR(1000))");
            Execute("CREATE TABLE IF NOT EXISTS FreeRequestTable (start VARCHAR(50), end VARCHAR(50), text VARCHAR(300), userID varchar(50))");
            Execute("CREATE TABLE IF NOT EXISTS HolidayTable (day VARCHAR(50))");

        }

        public void Execute(string sql)
        {
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
            string returnString = string.Join("-", inputList.ToArray());
            Console.WriteLine(returnString);
            return (returnString);
        }

        public List<string> StringToList(string inputString)
        {
            List<string> outputList = inputString.Split('-').ToList();
            return (outputList);
        }

        public void DatabaseInit()
        {
            Console.WriteLine("Database activated");
		}
    }
}
