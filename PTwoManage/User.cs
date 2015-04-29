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
       // private List<string> _userCategories;
        private int _id;
        private string _userName;
        private string _password;
        private string _name;
        private string _cprNumber;
        private string _phone;
        private string _email;
        //static List<string> userCategories = new List<string>();

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

      /*  void AddCategory(string newCategory)
        {
            userCategories.Add(newCategory);
        }

        void RemoveCategory(int categoryKey)
        {
            categories.Remove(categoryKey);
        }*/

        public static User GetUserByName(string userName)
        {
            foreach (User u in Core.Instance.GetAllUsers())
            {
                if (u.UserName == userName)
                {
                    return u;
                }
            }
            throw new UserNotFoundException();
        }

        public void SaveUserInfoToDatabase()
        {
            User user = this;
            string sql;
            sql = "DELETE FROM userTable WHERE username IN (SELECT username FROM userTable WHERE username ='" + user.UserName +"')";
            Database.Instance.Execute(sql);
            sql = "INSERT OR REPLACE INTO userTable  (id, username, password, name, cprNumber, phone, email) values (" + user.Id + ", '" + user.UserName + "', '" + user.Password + "', '" + user.Name + "' , " + user.CprNumber + " , " + user.Phone + ", '" + user.Email + "')";
            Database.Instance.Execute(sql);
        }

        public void DeleteUser()
        {
            User user = this;
            string sql;
            sql = "DELETE FROM userTable WHERE username IN (SELECT username FROM userTable WHERE username ='" + user.UserName + "')";
            Database.Instance.Execute(sql);
        }
    }
}
