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
        public void ScheduleGenerator(int weeknumber)
        {
            List<ShiftTemplate> AllShiftTemplates = new List<ShiftTemplate>();
            foreach (ShiftTemplate t in Core.Instance.GetAllTemplates())
            {
                AllShiftTemplates.Add(t);
            }


            int TemplateCount = AllShiftTemplates.Count;
            for (int i = 0; i <= TemplateCount; i++ )
                switch (AllShiftTemplates[i]._startTime.DayOfWeek)
                {
                    case DayOfWeek.Friday:
                        break;
                    case DayOfWeek.Monday:
                        break;
                    case DayOfWeek.Saturday:
                        break;
                    case DayOfWeek.Sunday:
                        break;
                    case DayOfWeek.Thursday:
                        break;
                    case DayOfWeek.Tuesday:
                        break;
                    case DayOfWeek.Wednesday:
                        break;
                    default:
                        break;
                }

        }
        
        public static void NewCalendar()
        {
            DateTime dt = new DateTime(2015, 05, 25, new GregorianCalendar());
            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            dt = cal.AddYears(dt, 2);
            Console.WriteLine(cal.GetYear(dt));
        }
  
        public void isHoliday(int dayInYear)
        {

        }
    }
    
}
