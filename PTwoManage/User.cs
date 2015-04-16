using System;
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
        private int _id;
        private string _userName;
        private string _password;
        private string _name;
        private int _cprNumber;
        private int _phone;
        private string _email;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string UserName
        {
            get { return _userName; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int CprNumber
        {
            get { return _cprNumber; }
            set { _cprNumber = value; }
        }


        public int Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public User(int id, string userName, string password, string name, int cprNummer, int phone, string email)
        {
            _id = id;
            _userName = userName;
            _password = password;
            _name = name;
            _cprNumber = cprNummer;
            _phone = phone;
            _email = email;
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
            return new User(1, "User was not found", "Password","Hej per", 90, 90, "asd");
        }
    }
}
