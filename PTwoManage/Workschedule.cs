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

                DateTime test4 = new DateTime(year, 1, 1);
                int FirstDayInYear = 0;
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

                int DayInYear = FirstDayInYear + (weeknumber - 2) * 7 + AllShiftTemplates[i]._startTime.Day;

                int dayH=1; //Skal sende en værdi med til funktionen så den returnerer remainder
                int md = TotalDayToDayInMonth(DayInYear, year);

                DateTime start = new DateTime(year,md,dayH,AllShiftTemplates[i]._startTime.Hour, AllShiftTemplates[i]._startTime.Minute,AllShiftTemplates[i]._startTime.Second);
                DateTime end   = new DateTime(year,md,dayH,AllShiftTemplates[i]._endTime.Hour, AllShiftTemplates[i]._endTime.Minute,AllShiftTemplates[i]._endTime.Second);
;


                string tag;
                int UserID;
                int Weeknumber;

               // Shift resultShift = new Shift(Date, start, end, AllShiftTemplates[i].Tag, missing, weeknumber);
                //resultShift.SaveInfoShiftTemplate();
            }
        }
        
        int TotalDayToDayInMonth(int DayInYear, int year)
        {
            int RunningDays = 0;
            int MonthLenght = 0;
            int Month = 1;
            int Remainder = 0;

            for (int i = 1; i <= 12; i++)
            {
                MonthLenght = DateTime.DaysInMonth(year, Month);
                if ((DayInYear - RunningDays) < Month)
                {
                    Remainder = DayInYear - RunningDays;
                    return Month;
                }
                    
                else
                {
                    RunningDays += MonthLenght;
                    Month++;
                }
            }
            return -1;
        }
    }
}
