﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public sealed class Core
    {
        static readonly Core _instance = new Core();
        private List<User> _allUsers;

        public static Core Instance
        {
            get { return _instance; }
        }

        Core()
        {
            _allUsers = new List<User>();
            //_allShiftTemplates = new List<ShiftTemplates>();

            List<string> info = Database.Instance.readInfo;
            string sql = "SELECT * FROM userTable";
            Database.Instance.Read(sql, Database.Instance.userTableColumns);
            foreach (var item in info)
            {
                string[] split = item.Split(new Char[]{','});
                _allUsers.Add(new User(int.Parse(split[0]), split[1], split[2], split[3], split[4], split[5], split[6]));
            }
        }
        
       
        public void Run()
        {
            User bruger = new User(1,"lucrah2", "1234", "luca2", "564455648", "88888888", "jgdagmailcom");   
        }

        public List<User> GetAllUsers()
        {
            return _allUsers;
        }

        public void AddUserToList(User user)
        {
            _allUsers.Add(user);
        }

        /*public List<ShiftTemplate> GetAllShiftTemplates()
        {
            return _allShiftTemplates;
        }*/
    }
}
