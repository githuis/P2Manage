using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public class Shift : ShiftTemplate
    {
        // Private properties for Shift needed to display a Shift
        private string _userName;
        private int _weeknumber;

        // Variables used to print Shift object in various windows
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

        public Shift(DateTime starttime, DateTime endtime, string tag, string userName, int weeknumber)
            : base(starttime, endtime, tag)
        {
                ValidateShiftTimeAndUser(starttime, endtime, userName);
                _userName = userName;
                _weeknumber = weeknumber;
                _startTime = starttime;
                _endTime = endtime;
                Day = _startTime.DayOfWeek;
                PrintableStartTime = _startTime.Hour.ToString() + ":" + _startTime.Minute.ToString();
                PrintableEndTime = _endTime.Hour.ToString() + ":" + _endTime.Minute.ToString();
        }

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

        public void SaveShift()
        {
            Shift shift = this;
            string sql = "INSERT INTO ShiftTable (start, end, tag, employeeName, weekNumber) values ('" + shift._startTime.ToString() + "', '" + shift._endTime.ToString() + "', '" + Database.Instance.ListToString(shift.Tag) + "', '" + shift.UserName + "', " + shift._weeknumber + ")";
            Database.Instance.Execute(sql);
        }

        public void UpdateShift(string newUser)
        {
            Shift shift = this;
            string sql = "UPDATE ShiftTable SET employeeName='" + newUser + "'  WHERE employeeName='" + shift.UserName + "' AND start='" + shift._startTime.ToString() + "' AND end='" + shift._endTime.ToString() + "'";
            shift.UserName = newUser;
            Database.Instance.Execute(sql);
        }

        public void DeleteShift()
        {
            Shift shift = this;
            string sql = "DELETE FROM ShiftTable WHERE employeeName='" + shift.UserName + "' AND start='" + shift._startTime.ToString() + "' AND end='" + shift._endTime.ToString() + "'";
            Database.Instance.Execute(sql);
            Core.Instance.GetAllShifts().Remove(shift);
        }
    }
}
