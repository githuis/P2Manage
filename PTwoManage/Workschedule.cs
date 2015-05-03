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
            List<User> AllUsers = Core.Instance.GetAllUsers();

            foreach (ShiftTemplate t in Core.Instance.GetAllTemplates())
            {
                AllShiftTemplates.Add(t);
            }


            int TemplateCount = AllShiftTemplates.Count;
            for (int i = 0; i <= TemplateCount; i++)
            {

                string Date = AllShiftTemplates[i]._startTime.DayOfWeek.ToString();

                DateTime YearStartingDate = new DateTime(year, 1, 1);
                int FirstDayInYear = 0;
                switch (YearStartingDate.DayOfWeek)
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

                int dayH = 0; //Skal sende en værdi med til funktionen så den returnerer remainder
                int md = 0;
                TotalDayToDayInMonth(DayInYear, year, ref dayH, ref md);

                DateTime start = new DateTime(year, md, dayH, AllShiftTemplates[i]._startTime.Hour, AllShiftTemplates[i]._startTime.Minute, AllShiftTemplates[i]._startTime.Second);
                DateTime end = new DateTime(year, md, dayH, AllShiftTemplates[i]._endTime.Hour, AllShiftTemplates[i]._endTime.Minute, AllShiftTemplates[i]._endTime.Second);

                // Sortering af usere - Først findes de medarbejdere som kan arbejde på den type vagt
                List<User> CompatibleUsers = new List<User>();

                int NumberOfUsers = AllUsers.Count;
                for (int j = 0; i < NumberOfUsers - 1; i++)
                {
                    if (CompareTags(AllUsers[j].UserCategories, AllShiftTemplates[i].Tag))
                        CompatibleUsers.Add(AllUsers[j]);
                }

                int UserID = SortUserList(AllUsers, 5);

                Shift resultShift = new Shift(Date, start, end, Database.Instance.listToString(AllShiftTemplates[i].Tag), UserID, weeknumber);
                resultShift.SaveInfoShiftTemplate();
            }
        }

        void TotalDayToDayInMonth(int DayInYear, int year, ref int RemaindingDaysInCurrentMonth, ref int ResultMonth)
        {
            int RunningDays = 0;
            int MonthLenght = 0;
            ResultMonth = 1;

            for (int i = 1; i <= 12; i++)
            {
                MonthLenght = DateTime.DaysInMonth(year, ResultMonth);
                if ((DayInYear - RunningDays) < ResultMonth)
                {
                    RemaindingDaysInCurrentMonth = DayInYear - RunningDays;
                    break;
                }

                else
                {
                    RunningDays += MonthLenght;
                    ResultMonth++;
                }
            }
        }

        public int SortUserList(List<User> UserList, int DayWeight)
        {
            // Er der kun en user er vagten hans, er der ingen skal der returneres en fejl
            if (UserList.Count <= 1)
            {
                if (UserList.Count == 1)
                    return UserList.First().Id;
                else
                    throw new ArgumentException("The userlist is currently empty so no user match the tags on a template");
            }

            // Skal først tage forbehold for medarbejder ønsker ved at undersøge om deres point > vægt og så fjerne pointsne og  fjrerne dem fra listen
            List<UserFreeRequest> Requests = Core.Instance.GetAllUserRequests();
            List<User> UnAvalibleUsers = new List<User>();
            int TotalNumberOfRequests = Requests.Count;
            int TotalNumberOfUsers = UserList.Count;

            for (int h = 0; h <= TotalNumberOfRequests-1; h++)
            {
                for (int k = 0; k <= UserList.Count-1; k++)
                {
                    if (Requests[h].UserName == UserList[k].UserName && UserList[k].Points >= DayWeight) 
                    {
                        UnAvalibleUsers.Add(UserList[k]);
                        break;
                    }
                }
            }

            var SortedUnAvalibleUsers = UnAvalibleUsers.OrderBy(user => user.Points);
            UnAvalibleUsers = SortedUnAvalibleUsers.ToList();

            for (int h = 0; h <= UnAvalibleUsers.Count-2; h++)
            {
                UserList.Remove(UnAvalibleUsers[h]);
            }

            // Skal derefter sorterer efter personen med færrest point
            var SortedList = UserList.OrderBy(user => user.Points);
            UserList = SortedList.ToList();

            // Til sidst tjekkes det om det er samme person som sidste år som arbejede på denne dato
            if (RepetingWorker(UserList.First()))
            {
                UserList.Remove(UserList.First());
            }
            else
                return UserList.First().Id;

            return -1;
        }

        public bool CompareTags(List<string> UserTags, List<string> ShiftTags)
        {
            return !ShiftTags.Except(UserTags).Any();
        }

        public bool RepetingWorker(User repeat)
        {
            return false;
        }

    }
}
