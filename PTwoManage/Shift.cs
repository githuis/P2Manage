using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public class Shift : ShiftTemplate
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private int _breakTime;
        public int Employee;

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

        public Shift(string date, DateTime starttime, DateTime endtime, string tag, int userID, int weeknumber) : base(date, starttime, endtime, tag)
        {
            Employee = userID;
            _startTime = starttime;
            _endTime = endtime;
            EmployeeName = Employee.Name;
            Day = _startTime.DayOfWeek;
            Week = 2;
            PrintableStartTime = _startTime.Hour.ToString() + ":" + _startTime.Minute.ToString();
            PrintableEndTime = _endTime.Hour.ToString() + ":" + _endTime.Minute.ToString();
        }

        private void CalculateBreakTime(String hours, String breakTime)
        {

        }
    }
}
