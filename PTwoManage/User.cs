﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    class User
    {
        static List<User> allUsers = new List<User>();
        List<string> userCategories;
        private string _userName;
        private string _password;

        public string UserName
        {
            get { return _userName; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public User(string userName, string password)
        {
            _userName = userName;
            // Todo: Skal laves til hash
            _password = password;
            allUsers.Add(this);
            Console.WriteLine(allUsers[allUsers.Count-1].UserName);
        }

        static Dictionary<int, string> categories = new Dictionary<int, string>()
        {
            {0, "Default"}, 
            {1, "Lukker"},
            {2, "Åbner"}
        };

        void AddCategory(string newCategory)
        {
            categories.Add(categories.Count, newCategory);
        }

        void RemoveCategory(int categoryKey)
        {
            categories.Remove(categoryKey);
        }

        public static User GetUserByName(string name)
        {
            foreach (User u in Core.GetAllUsers())
            {
                if (u.UserName == name)
                {
                    return u;
                }
            }
            return new User(name + "User was not found", "Password");
        }
    }
}
