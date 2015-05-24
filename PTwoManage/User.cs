using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public class User : IComparable
    {
        static List<User> allUsers = new List<User>();
        public List<string> UserCategories;
        private int _id;
        private string _userName;
        private string _password;
        private string _name;
        private string _cprNumber;
        private string _phone;
        private string _email;
        private int _points;
        private int _workInWeek;

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

        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public int WorkInWeek
        {
            get { return _workInWeek; }
            set { _workInWeek = value; }
        }

        public User(int id, string userName, string password, string name, string cprNummer, string phone, string email, List<string> tag, int points)
        {
            _id = id;
            _userName = userName;
            _password = password;
            _name = name;
            _cprNumber = cprNummer;
            _phone = phone;
            _email = email;
            UserCategories = tag;
            _points = points;
            _workInWeek = 0;
        }

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

        public static bool CheckUserExists(string userName)
        {
            foreach (User u in Core.Instance.GetAllUsers())
            {
                if (u.UserName == userName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasTag(string tag)
        {
            foreach (string t in UserCategories)
            {
                if (t == tag)
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            return this.UserName;
        }

        public void SaveUserInfoToDatabase()
        {
            User user = this;
            string sql = "DELETE FROM userTable WHERE username IN (SELECT username FROM userTable WHERE username ='" + user.UserName +"')";
            Database.Instance.Execute(sql);
            sql = "INSERT OR REPLACE INTO userTable  (username, password, name, cprNumber, phone, email, tag, points) values ('" + user.UserName + "', '" + user.Password + "', '" + user.Name + "' , " + user.CprNumber + " , " + user.Phone + ", '" + user.Email + "', '" + Database.Instance.ListToString(user.UserCategories) +"', " + user.Points + ")";
            Database.Instance.Execute(sql);
        }

        public void UpdateUserInfoDatabase()
        {
            User user = this;
            string sql = "UPDATE userTable SET password='" + user.Password + "', name='" + user.Name + "', cprNumber='" + user.CprNumber + "', phone='" + user.Phone + "', email='" + user.Email + "', tag='" + Database.Instance.ListToString(user.UserCategories) + "', points='" + user.Points + "'  WHERE username='" + user.UserName + "'";
            Database.Instance.Execute(sql);
        }

        public void DeleteUser()
        {
            User user = this;
            string sql;
            sql = "DELETE FROM userTable WHERE username IN (SELECT username FROM userTable WHERE username ='" + user.UserName + "')";
            Database.Instance.Execute(sql);
            sql = "DELETE FROM FreeRequestTable WHERE username IN (SELECT username FROM FreeRequestTable WHERE username ='" + user.UserName + "')";
            Database.Instance.Execute(sql);
            sql = "DELETE FROM ShiftTable WHERE employeeName IN (SELECT employeeName FROM ShiftTable WHERE EmployeeName ='" + user.UserName + "')";
            Database.Instance.Execute(sql);
        }

        public void UpdateUserPointBalance(int points)
        {
            User user = this;
            user.Points += points;
            string sql;
            sql = "UPDATE userTable SET points='" + user.Points + "' WHERE username='" + user.UserName + "'";
            Database.Instance.Execute(sql);
        }

        public bool HasShiftInTimeFrame(DateTime from, DateTime to)
        {
            User u = this;

            foreach (Shift s in Core.Instance.GetAllShifts())
            {
                if (IsWithinTimespan(s.StartTime, s.EndTime, from) && IsWithinTimespan(s.StartTime, s.EndTime, to))
                    return true;         
            }

            return false;
        }

        private bool IsWithinTimespan(DateTime start, DateTime end, DateTime check)
        {
            return ((check > start) && (check < end));
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            User p = obj as User;
            if ((System.Object)p == null)
            {
                return false;
            }

            return (UserName == p.UserName);
        }

        public bool Equals(User u)
        {
            // If parameter is null return false:
            if ((object)u == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (UserName == u.UserName);
        }

        public override int GetHashCode()
        {
            //45358 is an arbitrary number, but it is the same for all users
            //It ensures a spread of the hash values
            return Id * 45358;
        }

        public int CompareTo(object obj)
        {
            User u = obj as User;
            if (!(u is User))
                throw new ArgumentException("Attempting to compare to an object that is not of type user.");
            if (u == this)
                return 0;

            if (this.WorkInWeek < u.WorkInWeek)
                return -1;
            else if (this.WorkInWeek > u.WorkInWeek)
                return 1;
            else
            {
                if (this.Points < u.Points)
                    return -1;
                else if (this.Points > u.Points)
                    return 1;
                else
                {
                    //Somewhat 'random' value to sort the objects are (almost) the same
                   return (DateTime.Now.Millisecond%3) - 3;
                }
            }
        }

    }
}
