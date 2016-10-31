using System;

namespace PTwoManage
{
    public class Shift : ShiftTemplate
    {
        // The constructor for a Shift object. It takes base in the ShiftTemplate constructor
        public Shift(DateTime starttime, DateTime endtime, string tag, string userName, int weeknumber)
            : base(starttime, endtime, tag)
        {
            ValidateShiftTimeAndUser(starttime, endtime, userName);
            UserName = userName;
            Week = weeknumber;
            Day = StartTime.DayOfWeek;
            PrintableStartTime = StartTime.Hour + ":" + StartTime.Minute;
            PrintableEndTime = EndTime.Hour + ":" + EndTime.Minute;
        }

        // Private field for Shift needed to display a Shift

        // Fields used to print Shift object in various windows
        public DayOfWeek Day { get; set; }
        public string PrintableStartTime { get; set; }
        public string PrintableEndTime { get; set; }

        public string UserName { get; set; }

        public int Week { get; }

        // This methoc checks if a given user already has a Shift in the given timespan
        private void ValidateShiftTimeAndUser(DateTime starttime, DateTime endtime, string userName)
        {
            if (User.CheckUserExists(userName))
                if (User.GetUserByName(userName).HasShiftInTimeFrame(starttime, endtime))
                    throw new ArgumentException(" User: " + UserName + " already has a shift at this time");
        }

        public int GetYear()
        {
            return StartTime.Year;
        }

        // A method for saving Shifts to the database
        public void SaveShift()
        {
            var shift = this;
            var sql = "INSERT INTO ShiftTable (start, end, tag, employeeName, weekNumber) values ('" + shift._startTime +
                      "', '" + shift._endTime + "', '" + Database.Instance.ListToString(shift.Tag) + "', '" +
                      shift.UserName + "', " + shift.Week + ")";
            Database.Instance.Execute(sql);
        }

        // A method for updating a exsisting Shifts data with new infromation. Specified to update a Shifts User.
        public void UpdateShift(string newUser)
        {
            var shift = this;
            var sql = "UPDATE ShiftTable SET employeeName='" + newUser + "'  WHERE employeeName='" + shift.UserName +
                      "' AND start='" + shift._startTime + "' AND end='" + shift._endTime + "'";
            shift.UserName = newUser;
            Database.Instance.Execute(sql);
        }

        // A method for deleting a given shift form the database
        public void DeleteShift()
        {
            var shift = this;
            var sql = "DELETE FROM ShiftTable WHERE employeeName='" + shift.UserName + "' AND start='" +
                      shift._startTime + "' AND end='" + shift._endTime + "'";
            Database.Instance.Execute(sql);
            Core.Instance.GetAllShifts().Remove(shift);
        }
    }
}