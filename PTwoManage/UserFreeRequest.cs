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
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        

        public DateTime Endtime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
        

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public UserFreeRequest(DateTime start, DateTime end, string message, string userName)
        {
            _startTime = start;
            _endTime = end;
            _message = message;
            _userName = userName;
        }

        public void SaveUserRequest()
        {
            UserFreeRequest request = this;
            string sql = "INSERT INTO FreeRequestTable (start, end, text, userID) values ('" + request._startTime.ToString() + "', '" + request._endTime.ToString() + "', '" + request._message + "', '" + request._userName + "')";
            Database.Instance.Execute(sql);
        }
        
    }
}
