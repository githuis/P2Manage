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
    }
    
}
