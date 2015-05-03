using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    class Week
    {
        List<Shift> Monday = new List<Shift>();
        List<Shift> Tuesday = new List<Shift>();
        List<Shift> Wednesday = new List<Shift>();
        List<Shift> Thursday = new List<Shift>();
        List<Shift> Friday = new List<Shift>();
        List<Shift> Saturday = new List<Shift>();
        List<Shift> Sunday = new List<Shift>();
        int WeekNumber;
        int Year;

        public Week(int week, int year)
        {
            WeekNumber = week;
            Year = year;

            List<string> info = new List<string>();
            string sql = "SELECT * FROM ShiftTable WHERE weekNumber=" + this.WeekNumber;
            Database.Instance.Read(sql, ref info, Database.Instance.ShiftTableColumns);
            foreach (var item in info)
            {
                string[] split = item.Split(new Char[] { ',' });
                DateTime t1 = new DateTime();
                DateTime t2 = new DateTime();
                t1 = DateTime.Parse(split[1]);
                t2 = DateTime.Parse(split[2]);

                switch (t1.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        Monday.Add(new Shift(t1, t2, split[3], split[4], int.Parse(split[5])));
                        break;
                    case DayOfWeek.Tuesday:
                        Tuesday.Add(new Shift(t1, t2, split[3], split[4], int.Parse(split[5])));
                        break;
                    case DayOfWeek.Wednesday:
                        Wednesday.Add(new Shift(t1, t2, split[3], split[4], int.Parse(split[5])));
                        break;
                    case DayOfWeek.Thursday:
                        Thursday.Add(new Shift(t1, t2, split[3], split[4], int.Parse(split[5])));
                        break;
                    case DayOfWeek.Friday:
                        Friday.Add(new Shift(t1, t2, split[3], split[4], int.Parse(split[5])));
                        break;
                    case DayOfWeek.Saturday:
                        Saturday.Add(new Shift(t1, t2, split[3], split[4], int.Parse(split[5])));
                        break;
                    case DayOfWeek.Sunday:
                        Sunday.Add(new Shift(t1, t2, split[3], split[4], int.Parse(split[5])));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
