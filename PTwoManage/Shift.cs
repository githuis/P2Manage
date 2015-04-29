using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public class Shift
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private int _breakTime;
        public User Employee;

        public string EmployeeName { get; set; }
        public DayOfWeek Day { get; set; }
        public int Week { get; set; }
        public string PrintableStartTime { get; set; }
        public string PrintableEndTime { get; set; }

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        public Shift(User user, DateTime start, DateTime end)
        {
            Employee = user;
            _startTime = start;
            _endTime = end;
            EmployeeName = Employee.Name;
            Day = _startTime.DayOfWeek;
            Week = 2;
            PrintableStartTime = _startTime.Hour.ToString() + ":" + _startTime.Minute.ToString();
            PrintableEndTime = _endTime.Hour.ToString() + ":" + _endTime.Minute.ToString();
           
        }

        private void CalculateBreakTime(String hours, String breakTime)
        {

        }

        public override string ToString()
        {
            return ("Shift: " + Employee.UserName + " " + StartTime.Day + " At " + StartTime.TimeOfDay + " to " + EndTime);
        }
        
    }
}
