﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    class Shift
    {
        private DateTime _startTime;
        private DateTime _endTime;
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

        public Shift(User user, DateTime start, DateTime end)
        {
            Employee = user;
            _startTime = start;
            _endTime = end;
        }

        private void CalculateBreakTime()
        {

        }

        public override string ToString()
        {
            return (Employee.UserName + " " + StartTime + " " + EndTime);
        }
        
    }
}