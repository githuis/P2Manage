using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    class Shift : ShiftTemplate
    {
        public int WeekNumber;
        private int _breakTime;
        public User Employee;

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

        public Shift(User user, DateTime start, DateTime end, string date, List<string> tag)
            : base(date, start, end, tag)
        {
            Employee = user;
            _startTime = start;
            _endTime = end;
        }

        private void CalculateBreakTime(string hours, string breakTime)
        {

        }

        public override string ToString()
        {
            return (Employee.UserName + " " + StartTime + " " + EndTime);
        }
    }
}