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
        public void ScheduleGenerator(int weeknumber, int year)
        {
            List<ShiftTemplate> AllShiftTemplates = new List<ShiftTemplate>();
            foreach (ShiftTemplate t in Core.Instance.GetAllTemplates())
            {
                AllShiftTemplates.Add(t);
            }


            int TemplateCount = AllShiftTemplates.Count;
            for (int i = 0; i <= TemplateCount; i++)
            {

                string Date = AllShiftTemplates[i]._startTime.DayOfWeek.ToString();

                int testweek = weeknumber * 7;

                DateTime start;
                DateTime end;

                DateTime test4 = new DateTime(year, 1, 1);
                int FirstDayInYear;
                switch (test4.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        FirstDayInYear = 7;
                        break;
                    case DayOfWeek.Tuesday:
                        FirstDayInYear = 6;
                        break;
                    case DayOfWeek.Wednesday:
                        FirstDayInYear = 5;
                        break;
                    case DayOfWeek.Thursday:
                        FirstDayInYear = 4;
                        break;
                    case DayOfWeek.Friday:
                        FirstDayInYear = 3;
                        break;
                    case DayOfWeek.Saturday:
                        FirstDayInYear = 2;
                        break;
                    case DayOfWeek.Sunday:
                        FirstDayInYear = 1;
                        break;
                    default:
                        break;
                }

                int md;
                int dayH;
                int RunningMonth;
                int month;



                /* switch (month)
                 {
                     case DayOfWeek.Monday:
                         DayInCurrentWeek = 7;
                         break;
                     case DayOfWeek.Tuesday:
                         DayInCurrentWeek = 6;
                          break;
                     case DayOfWeek.Wednesday:
                         DayInCurrentWeek = 5;
                          break;
                     case DayOfWeek.Thursday:
                         DayInCurrentWeek = 4;
                          break;
                     case DayOfWeek.Friday:
                         DayInCurrentWeek = 3;
                          break;
                     case DayOfWeek.Saturday:
                         DayInCurrentWeek = 2;
                          break;
                     case DayOfWeek.Sunday:
                         DayInCurrentWeek = 1;
                          break;
                     default:
                          break;
                 }*/

                //int dayNumber = FirstDayInYear + (weeknumber - 2);


                string tag;
                int UserID;
                int Weeknumber;

                //Shift resultShift = new Shift();
                //resultShift.SaveInfoShiftTemplate();
            }
        }
    }
}
