using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public class UserFreeRequest
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private string _message;
        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }
        
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        
        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
        
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        // A UserFreeRequest is constructed with a timespan, a message and a user which is found with the GetUserByname method
        public UserFreeRequest(DateTime start, DateTime end, string message, string userName)
        {
            _startTime = start;
            _endTime = end;
            _message = message;
            _user = User.GetUserByName(userName);
        }

        //Saves a request to the database.
        public void SaveUserRequest()
        {
            UserFreeRequest request = this;
            string sql = "INSERT INTO FreeRequestTable (start, end, text, username) values ('" + request._startTime.ToString() + "', '" + request._endTime.ToString() + "', '" + request._message + "', '" + request.User.UserName + "')";
            Database.Instance.Execute(sql);
            Core.Instance.GetAllFreeRequests().Add(this);
        }
        
    }
}
