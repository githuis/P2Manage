using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public class User
    {
        Core core;
        static List<User> allUsers = new List<User>();
        List<string> userCategories;
        private int _id;
        private string _userName;
        private string _password;
        private string _name;
        private string _cprNumber;
        private string _phone;
        private string _email;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
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

        public string CprNumber
        {
            get { return _cprNumber; }
            set { _cprNumber = value; }
        }


        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public User(int id, string userName, string password, string name, string cprNummer, string phone, string email)
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
            foreach (User u in Core.Instance.GetAllUsers())
            {
                if (u.Name == name)
                {
                    return u;
                }
            }
            // Todo: Skal kaste error
            return new User(999999, "User not found", "User not found", "User not found", "564455648", "88888888", "User not found");
        }

        public void SaveUserInfoToDatabase()
        {
            User user = this;
            string sql = "INSERT INTO userTable (id, username, password, name ,cprNumber, phone , email) values (" + user.Id + ", '" + user.UserName + "', '" + user.Password + "', '" + user.Name + "' , " + user.CprNumber + " , " + user.Phone + ", '" + user.Email + "')";
            Database.Instance.Execute(sql);
        }
    }
}
