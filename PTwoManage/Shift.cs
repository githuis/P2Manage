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
        private int _weeknumber;

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

        public Shift(DateTime starttime, DateTime endtime, string tag, string userName, int weeknumber)
            : base(starttime, endtime, tag)
        {
            EmployeeName = userName;
            _weeknumber = weeknumber;
            _startTime = starttime;
            _endTime = endtime;
            Day = _startTime.DayOfWeek;
            Week = 2;
            PrintableStartTime = _startTime.Hour.ToString() + ":" + _startTime.Minute.ToString();
            PrintableEndTime = _endTime.Hour.ToString() + ":" + _endTime.Minute.ToString();
        }

        private void CalculateBreakTime(String hours, String breakTime)
        {

        }

        public void SaveShift()
        {
            Shift shift = this;
            string sql = "INSERT INTO ShiftTable (start, end, tag, employeeName, weekNumber) values ('" + shift._startTime.ToString() + "', '" + shift._endTime.ToString() + "', '" + Database.Instance.listToString(shift.Tag) + "', '" + shift.EmployeeName + "', " + shift._weeknumber + ")";
            Database.Instance.Execute(sql);
        }
    }
}
