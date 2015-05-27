using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public class Shift : ShiftTemplate
    {
        // Private field for Shift needed to display a Shift
        private string _userName;
        private int _weeknumber;

        // Fields used to print Shift object in various windows
        public DayOfWeek Day { get; set; }
        public string PrintableStartTime { get; set; }
        public string PrintableEndTime { get; set; }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public int Week
        {
            get { return _weeknumber; }
        }

        // The constructor for a Shift object. It takes base in the ShiftTemplate constructor
        public Shift(DateTime starttime, DateTime endtime, string tag, string userName, int weeknumber)
            : base(starttime, endtime, tag)
        {
                ValidateShiftTimeAndUser(starttime, endtime, userName);
                _userName = userName;
                _weeknumber = weeknumber;
                Day = StartTime.DayOfWeek;
                PrintableStartTime = StartTime.Hour.ToString() + ":" + StartTime.Minute.ToString();
                PrintableEndTime = EndTime.Hour.ToString() + ":" + EndTime.Minute.ToString();
        }

        // This methoc checks if a given user already has a Shift in the given timespan
        private void ValidateShiftTimeAndUser(DateTime starttime, DateTime endtime, string userName)
        {
            if (User.CheckUserExists(userName))
                if (User.GetUserByName(userName).HasShiftInTimeFrame(starttime, endtime))
                {
                    throw new ArgumentException(" User: " + this.UserName + " already has a shift at this time"); 
                }
        }

        public int GetYear()
        {
            return StartTime.Year;
        }

        // A method for saving Shifts to the database
        public void SaveShift()
        {
            Shift shift = this;
            string sql = "INSERT INTO ShiftTable (start, end, tag, employeeName, weekNumber) values ('" + shift._startTime.ToString() + "', '" + shift._endTime.ToString() + "', '" + Database.Instance.ListToString(shift.Tag) + "', '" + shift.UserName + "', " + shift._weeknumber + ")";
            Database.Instance.Execute(sql);
        }

        // A method for updating a exsisting Shifts data with new infromation. Specified to update a Shifts User.
        public void UpdateShift(string newUser)
        {
            Shift shift = this;
            string sql = "UPDATE ShiftTable SET employeeName='" + newUser + "'  WHERE employeeName='" + shift.UserName + "' AND start='" + shift._startTime.ToString() + "' AND end='" + shift._endTime.ToString() + "'";
            shift.UserName = newUser;
            Database.Instance.Execute(sql);
        }

        // A method for deleting a given shift form the database
        public void DeleteShift()
        {
            Shift shift = this;
            string sql = "DELETE FROM ShiftTable WHERE employeeName='" + shift.UserName + "' AND start='" + shift._startTime.ToString() + "' AND end='" + shift._endTime.ToString() + "'";
            Database.Instance.Execute(sql);
            Core.Instance.GetAllShifts().Remove(shift);
        }
    }
}
