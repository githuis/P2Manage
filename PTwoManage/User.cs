using System;
using System.Collections.Generic;

namespace PTwoManage
{
    public class User : IComparable
    {
        // The constructor for the User class which sets the parameters as values for the User object. 
        // _workInWeek is set 0 because a user does not start with Shifts at creation
        public User(int id, string userName, string password, string name, string cprNummer, string phone, string email,
            List<string> tag, int point)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Name = name;
            CprNumber = cprNummer;
            Phone = phone;
            Email = email;
            Tags = tag;
            Points = point;
            WorkInWeek = 0;
        }

        // Properties for the User class needed to define a user
        public List<string> Tags;

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string CprNumber { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int Points { get; set; }

        public int WorkInWeek { get; set; }

        // A method used to find a user object by comparing a username with other usernames from the list of users
        public static User GetUserByName(string userName)
        {
            foreach (var u in Core.Instance.GetAllUsers())
                if (u.UserName == userName)
                    return u;
            throw new UserNotFoundException();
        }

        // A method for checking if a User exsist by matching by their UserName
        public static bool CheckUserExists(string userName)
        {
            foreach (var u in Core.Instance.GetAllUsers())
                if (u.UserName == userName)
                    return true;
            return false;
        }

        public override string ToString()
        {
            return UserName;
        }

        // A method for saving a given user to the database by saving the user or replacing an exsisting user with the same values
        public void SaveUserInfoToDatabase()
        {
            var user = this;
            var sql = "DELETE FROM userTable WHERE username IN (SELECT username FROM userTable WHERE username ='" +
                      user.UserName + "')";
            Database.Instance.Execute(sql);
            sql =
                "INSERT OR REPLACE INTO userTable  (username, password, name, cprNumber, phone, email, tag, points) values ('" +
                user.UserName + "', '" + user.Password + "', '" + user.Name + "' , " + user.CprNumber + " , " +
                user.Phone + ", '" + user.Email + "', '" + Database.Instance.ListToString(user.Tags) + "', " +
                user.Points + ")";
            Database.Instance.Execute(sql);
        }

        // A method for updating userinformation by looking up the user and replacing the current information with new information
        public void UpdateUserInfoDatabase()
        {
            var user = this;
            var sql = "UPDATE userTable SET password='" + user.Password + "', name='" + user.Name + "', cprNumber='" +
                      user.CprNumber + "', phone='" + user.Phone + "', email='" + user.Email + "', tag='" +
                      Database.Instance.ListToString(user.Tags) + "', points='" + user.Points + "'  WHERE username='" +
                      user.UserName + "'";
            Database.Instance.Execute(sql);
        }

        // A method for deleting a user from the database. Both the user and its shifts and FreeRequests are deleted from the database
        public void DeleteUser()
        {
            var user = this;
            string sql;
            sql = "DELETE FROM userTable WHERE username IN (SELECT username FROM userTable WHERE username ='" +
                  user.UserName + "')";
            Database.Instance.Execute(sql);
            sql =
                "DELETE FROM FreeRequestTable WHERE username IN (SELECT username FROM FreeRequestTable WHERE username ='" +
                user.UserName + "')";
            Database.Instance.Execute(sql);
            sql =
                "DELETE FROM ShiftTable WHERE employeeName IN (SELECT employeeName FROM ShiftTable WHERE EmployeeName ='" +
                user.UserName + "')";
            Database.Instance.Execute(sql);
        }

        // A method for changing a users pointbalance where the given user mathces a user in the database
        public void UpdateUserPointBalance(int points)
        {
            var user = this;
            user.Points += points;
            string sql;
            sql = "UPDATE userTable SET points='" + user.Points + "' WHERE username='" + user.UserName + "'";
            Database.Instance.Execute(sql);
        }

        // A method for checking if a user already has a shift in a given timespan by looking at each shifts timespan
        public bool HasShiftInTimeFrame(DateTime from, DateTime to)
        {
            var u = this;

            foreach (var s in Core.Instance.GetAllShifts())
                if (IsWithinTimespan(s.StartTime, s.EndTime, from) && IsWithinTimespan(s.StartTime, s.EndTime, to))
                    return true;

            return false;
        }

        private bool IsWithinTimespan(DateTime start, DateTime end, DateTime check)
        {
            return (check > start) && (check < end);
        }

        // A method for comparing two user to check if they are the same user
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var p = obj as User;
            if (p == null)
                return false;

            return UserName == p.UserName;
        }

        public bool Equals(User u)
        {
            // If parameter is null return false:
            if (u == null)
                return false;

            // Return true if the fields match:
            return UserName == u.UserName;
        }

        public override int GetHashCode()
        {
            //45358 is an arbitrary number, but it is the same for all users
            //It ensures a spread of the hash values
            return Id*45358;
        }

        // A method used to compare two users and determine which is higher on the list in the sorting of users.
        public int CompareTo(object obj)
        {
            var u = obj as User;
            if (!(u is User))
                throw new ArgumentException("Attempting to compare to an object that is not of type user.");
            if (u == this)
                return 0;

            if (WorkInWeek < u.WorkInWeek)
                return -1;
            if (WorkInWeek > u.WorkInWeek)
                return 1;
            // If the placement is not determined by the amount of WorkInWeek, the pointbalance is used.
            if (Points < u.Points)
                return -1;
            if (Points > u.Points)
                return 1;
            return DateTime.Now.Millisecond%3 - 3;
        }
    }
}