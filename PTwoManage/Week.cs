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
        public string[] ShiftTableColumns = new string[7] { "id", "date", "start", "end", "tag", "employeeId", "weekNumber" };

        public Week(int week, int year)
        {
            WeekNumber = week;
            Year = year;

            List<string> info = Database.Instance.readInfo;
            string sql = "SELECT * FROM ShiftTable WHERE weekNumber=" + this.WeekNumber;
            Database.Instance.Read(sql, this.ShiftTableColumns);
            foreach (var item in info)
            {
                string[] split = item.Split(new Char[] { ',' });
                DateTime t1 = new DateTime();
                DateTime t2 = new DateTime();
                t1 = DateTime.Parse(split[2]);
                t2 = DateTime.Parse(split[3]);

                if(t1.DayOfWeek == DayOfWeek.Monday)
                    Monday.Add(new Shift(split[1], t1, t2, split[4], int.Parse(split[5]), int.Parse(split[6])));
                else if(t1.DayOfWeek == DayOfWeek.Tuesday)
                    Tuesday.Add(new Shift(split[1], t1, t2, split[4], int.Parse(split[5]), int.Parse(split[6])));
                else if(t1.DayOfWeek == DayOfWeek.Wednesday)
                    Wednesday.Add(new Shift(split[1], t1, t2, split[4], int.Parse(split[5]), int.Parse(split[6])));
                else if (t1.DayOfWeek == DayOfWeek.Thursday)
                    Thursday.Add(new Shift(split[1], t1, t2, split[4], int.Parse(split[5]), int.Parse(split[6])));
                else if (t1.DayOfWeek == DayOfWeek.Friday)
                    Friday.Add(new Shift(split[1], t1, t2, split[4], int.Parse(split[5]), int.Parse(split[6])));
                else if (t1.DayOfWeek == DayOfWeek.Saturday)
                    Saturday.Add(new Shift(split[1], t1, t2, split[4], int.Parse(split[5]), int.Parse(split[6])));
                else if (t1.DayOfWeek == DayOfWeek.Sunday)
                    Sunday.Add(new Shift(split[1], t1, t2, split[4], int.Parse(split[5]), int.Parse(split[6])));
            } 
        }

    }
}
