using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public class Shift : ShiftTemplate
    {
        public int Employee;
        private int _weeknumber;

        public string EmployeeName { get; set; }
        public DayOfWeek Day { get; set; }        
        public string PrintableStartTime { get; set; }
        public string PrintableEndTime { get; set; }

        public int Week 
        { 
            get { return _weeknumber; } 
        }

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
            PrintableStartTime = _startTime.Hour.ToString() + ":" + _startTime.Minute.ToString();
            PrintableEndTime = _endTime.Hour.ToString() + ":" + _endTime.Minute.ToString();
		}

        private void CalculateBreakTime(String hours, String breakTime)
        {

        }

		/*
        public void SaveInfoShiftTemplate()
        {
            ShiftTemplate template = this;
            int test = 1;
            string s = "test";
            //for (int i = 0; i <= template.Tag.Count; i++)
            {
                if(i>0)
                    s += "-";
                s += template.Tag[i];
            }
             

            string sql = "INSERT INTO ShiftTemplate (id, date, start, end, tag) values (" + test + ", '" + template.Date + "', '" + template._startTime.ToString("dd-MM-yyyy-HH-mm-ss") + "', '" + template._endTime.ToString("dd-MM-yyyy-HH-mm-ss") + "', '" + Database.Instance.listToString(template.Tag) + "')";
		*/
        public void SaveShift()
        {
            Shift shift = this;
            string sql = "INSERT INTO ShiftTable (start, end, tag, employeeName, weekNumber) values ('" + shift._startTime.ToString() + "', '" + shift._endTime.ToString() + "', '" + Database.Instance.ListToString(shift.Tag) + "', '" + shift.EmployeeName + "', " + shift._weeknumber + ")";
            Database.Instance.Execute(sql);
        }

    }
}
