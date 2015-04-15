using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace PTwoManage
{
    class Workschedule
    {
        
        public static void NewCalendar()
        {
            DateTime dt = new DateTime(2015, 05, 25, new GregorianCalendar());
            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            dt = cal.AddYears(dt, 2);
            Console.WriteLine(cal.GetYear(dt));
        }

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

        public class Week
        {
            DateTime start = new DateTime(2008, 5, 1, 8, 30, 0);
            DateTime slut = new DateTime(2008, 5, 1, 16, 0, 0);
            public int weeknumber;
            day test = new day(Weekdays.Friday, 2, false, start, slut);
            List<day> w = new List<day>();
            
        }

        public class day
        {
            public Weekdays weekday;
            public int numberOfShifts;
            public bool holiday;
            public DateTime openingHour;
            public DateTime closingHour;
       
            public day (Weekdays dayInWeek, int shiftsInDay, bool isHoliday, DateTime opening, DateTime closing)
            {
                weekday = dayInWeek;
                numberOfShifts = shiftsInDay;
                holiday = isHoliday;
                openingHour = opening;
                closingHour = closing;
            }
        }
    }
    
}
