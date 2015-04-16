using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    class Day
    {
        enum Weekdays 
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
        
        public Weekdays weekday;
        public int numberOfShifts;
        public bool holiday;
        public DateTime openingHour;
        public DateTime closingHour;
        List<Shift> ThisWeek = new List<Shift>();



        public Day (Weekdays dayInWeek, int shiftsInDay, bool isHoliday, DateTime opening, DateTime closing)
        {
            weekday = dayInWeek;
            numberOfShifts = shiftsInDay;
            holiday = isHoliday;
            openingHour = opening;
            closingHour = closing;
        }
    }
}
