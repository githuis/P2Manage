using System;

namespace PTwoManage
{
    public class UserFreeRequest
    {
        // A UserFreeRequest is constructed with a timespan, a message and a user which is found with the GetUserByname method
        public UserFreeRequest(DateTime start, DateTime end, string message, string userName)
        {
            StartTime = start;
            EndTime = end;
            Message = message;
            User = User.GetUserByName(userName);
        }

        public User User { get; set; }

        public string Message { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime StartTime { get; set; }

        //Saves a request to the database.
        public void SaveUserRequest()
        {
            var request = this;
            var sql = "INSERT INTO FreeRequestTable (start, end, text, username) values ('" + request.StartTime + "', '" +
                      request.EndTime + "', '" + request.Message + "', '" + request.User.UserName + "')";
            Database.Instance.Execute(sql);
            Core.Instance.GetAllFreeRequests().Add(this);
        }
    }
}