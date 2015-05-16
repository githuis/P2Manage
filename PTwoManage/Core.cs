using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PTwoManage
{
    public sealed class Core
    {
        static readonly Core _instance = new Core();

        private List<User> _allUsers;
        private List<string> _allTags;
        private List<Shift> _allShifts;
        private List<ShiftTemplate> _allTemplates;
		private List<UserFreeRequest> _allRequests;
        private List<Holiday> _allHolidays;
        private List<string> _info;

        public static Core Instance
        {
            get { return _instance; }
        }

        Core()
        {
            _allTags = new List<string>();
            _allUsers = new List<User>();
            _allTemplates = new List<ShiftTemplate>();
            _allRequests = new List<UserFreeRequest>();
            _allHolidays = new List<Holiday>();
            _info = new List<string>();
            _allShifts = new List<Shift>();

            string sql = "SELECT * FROM userTable";
            
            Database.Instance.Read(sql, ref _info, Database.Instance.userTableColumns);
            foreach (var item in _info)
            {
                string[] split = item.Split(new Char[]{','});
                _allUsers.Add(new User(int.Parse(split[0]), split[1], split[2], split[3], split[4], split[5], split[6], Database.Instance.StringToList(split[7]), int.Parse(split[8])));
                //_allUsers.Add(new User(int.Parse(split[0]), split[1], split[2], split[3], split[4], split[5], split[6], Database.Instance.stringToList(split[7]), 400));
            }
            
            string sql2 = "SELECT * FROM ShiftTemplate";
            Database.Instance.Read(sql2, ref _info, Database.Instance.ShiftTemplateTableColumns);
            ShiftTemplate t;
            foreach (var item in _info)
            {
                string[] split2 = item.Split(new Char[] {','});
                t = new ShiftTemplate(DateTime.Parse(split2[1]), DateTime.Parse(split2[2]), split2[3]);
                t.GeneratePrintableInfo();
                _allTemplates.Add(t);

            }

            string sql3 = "SELECT * FROM TagTable";
            Database.Instance.Read(sql3, ref _info, Database.Instance.TagTableColumns);
            foreach (var item in _info)
            {
                string[] split3 = item.Split(new Char[]{','});
                if(!(split3[0] == ""))
                    _allTags.Add(split3[0]);
            }

            sql = "SELECT * FROM FreeRequestTable";
            Database.Instance.Read(sql, ref _info, Database.Instance.FreeTimeRequestColumns);
            string holder;
            foreach (var item in _info)
            {
                string[] split4 = item.Split(new Char[] { ',' });
                if (split4[2] == null)
                    holder = "empty";
                else
                    holder = split4[2];
                _allRequests.Add(new UserFreeRequest(DateTime.Parse(split4[0]), DateTime.Parse(split4[1]), holder, split4[3]));
            }

            sql = "SELECT * FROM HolidayTable";
            Database.Instance.Read(sql, ref _info, Database.Instance.HolidayTableColumns);
            foreach (var item in _info)
            {
                string[] split = item.Split(new Char[] { ',' });
                _allHolidays.Add(new Holiday(DateTime.Parse(split[0])));
            }

            sql = "SELECT * FROM ShiftTable";
            Database.Instance.Read(sql, ref _info, Database.Instance.ShiftTableColumns);
            foreach (var item in _info)
            {
                string[] split = item.Split(new Char[] { ',' });
                _allShifts.Add(new Shift(DateTime.Parse(split[1]),DateTime.Parse(split[2]),split[3],split[4],int.Parse(split[5])));
            }

        }
       
       public void Run()
        {
           //Midlertidig funktion til at se om shifts kan vises
            /*_allShifts.Add(new Shift(new DateTime(2015, 04, 20, 12, 30, 00), new DateTime(2015, 04, 20, 12, 45, 00), "Åbner", "Hansi", 1));
           
            _allShifts.Add(new Shift(new DateTime(2015, 04, 21, 15, 30, 00), new DateTime(2015, 04, 21, 17, 30, 00), "Åbner", "Jens", 1));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 22, 15, 30, 00), new DateTime(2015, 04, 22, 17, 30, 00), "Åbner", "Jens", 1));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 23, 15, 30, 00), new DateTime(2015, 04, 23, 17, 30, 00), "Åbner", "Jens", 1));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 24, 15, 30, 00), new DateTime(2015, 04, 24, 17, 30, 00), "Åbner", "Jens", 1));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 25, 15, 30, 00), new DateTime(2015, 04, 25, 17, 30, 00), "Åbner", "Jens", 1));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 26, 15, 30, 00), new DateTime(2015, 04, 26, 17, 30, 00), "Åbner", "Jens", 1));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 27, 15, 30, 00), new DateTime(2015, 04, 27, 17, 30, 00), "Åbner", "Jens", 1));

            _allShifts.Add(new Shift(new DateTime(2015, 04, 28, 15, 30, 00), new DateTime(2015, 04,28, 18, 20, 00), "Åbner", "Peter", 2));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 29, 15, 30, 00), new DateTime(2015, 04,29, 18, 20, 00), "Åbner", "Peter", 2));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 30, 15, 30, 00), new DateTime(2015, 04,30, 18, 20, 00), "Åbner", "Peter", 2));
            _allShifts.Add(new Shift(new DateTime(2015, 05, 1 , 15, 30, 00), new DateTime(2015, 04, 1, 18, 20, 00), "Åbner", "Peter", 2));
            _allShifts.Add(new Shift(new DateTime(2015, 05, 2 , 15, 30, 00), new DateTime(2015, 04, 2, 18, 20, 00), "Åbner", "Peter", 2));
            _allShifts.Add(new Shift(new DateTime(2015, 05, 3 , 15, 30, 00), new DateTime(2015, 04, 3, 18, 20, 00), "Åbner", "Peter", 2));
            _allShifts.Add(new Shift(new DateTime(2015, 05, 4 , 15, 30, 00), new DateTime(2015, 04, 4, 18, 20, 00), "Åbner", "Peter", 2));

            _allShifts.Add(new Shift(new DateTime(2015, 05, 5 , 15, 30, 00), new DateTime(2015, 04, 5 , 19, 30, 00), "Åbner", "Ole", 3));
            _allShifts.Add(new Shift(new DateTime(2015, 05, 6 , 15, 30, 00), new DateTime(2015, 04, 6 , 19, 30, 00), "Åbner", "Ole", 3));
            _allShifts.Add(new Shift(new DateTime(2015, 05, 7 , 15, 30, 00), new DateTime(2015, 04, 7 , 19, 30, 00), "Åbner", "Ole", 3));
            _allShifts.Add(new Shift(new DateTime(2015, 05, 8 , 15, 30, 00), new DateTime(2015, 04, 8 , 19, 30, 00), "Åbner", "Ole", 3));
            _allShifts.Add(new Shift(new DateTime(2015, 05, 9 , 15, 30, 00), new DateTime(2015, 04, 9 , 19, 30, 00), "Åbner", "Ole", 3));
            _allShifts.Add(new Shift(new DateTime(2015, 05, 10, 15, 30, 00), new DateTime(2015, 04, 10, 19, 30, 00), "Åbner", "Ole", 3));
            _allShifts.Add(new Shift(new DateTime(2015, 05, 11, 15, 30, 00), new DateTime(2015, 04, 11, 19, 30, 00), "Åbner", "Ole", 3));
            */
        }
        
        public List<User> GetAllUsers()
        {
            return _allUsers;
        }

        public List<UserFreeRequest> GetAllUserRequests()
        {
            return _allRequests;
        }

        public List<ShiftTemplate> GetAllTemplates()
        {
            return _allTemplates;
        }

        public void AddUserToList(User user)
        {
            _allUsers.Add(user);
        }

        public void RemoveUserFromList(User user)
        {
            _allUsers.Remove(user);
        }

        public  List<string> GetAllTags()
        {
            return _allTags;
        }

        public void AddTemplateToList(ShiftTemplate template)
        {
            _allTemplates.Add(template);
        }

        public void AddShiftToList(Shift shift)
        {
            _allShifts.Add(shift);
        }
        public List<Shift> GetAllShifts()
        {
            return _allShifts;
        }

        public List<Shift> GetAllShifts(DayOfWeek day, int weekNum, int year)
        {
            List<Shift> dShifts = new List<Shift>();
           foreach(Shift s in _allShifts)
           {

               if (s.Day == day && s.Week == weekNum && s.GetYear() == year)
                   dShifts.Add(s);
           }
           return dShifts;
		}   
		
        public void AddTagToList(string tag)
        {
            _allTags.Add(tag);
        }

        public void DeleteTagFromList(string s)
        {
            _allTags.Remove(s);
        }

        public List<Holiday> GetAllHolidays()
        {
            _allHolidays = _allHolidays.OrderBy(holiday => holiday.Date).ToList();
            return _allHolidays;
        }

        public void AddToHolidayList(Holiday NewHoliday)
        {
            _allHolidays.Add(NewHoliday);
        }

        public void RemoveHolidayFromList(Holiday toRemove)
        {
            _allHolidays.Remove(toRemove);
        }

        public void ScheduleGenerator(int weeknumber, int year)
        {
            List<User> AllUsers = Core.Instance.GetAllUsers();
            List<ShiftTemplate> AllShiftTemplates = Core.Instance.GetAllTemplates();
            List<Holiday> AllHolidays = Core.Instance.GetAllHolidays();

            int PossitiveDayCost = 0;
            int NegativeDayCost = 0;

            int TemplateCount = AllShiftTemplates.Count;
            for (int i = 0; i <= TemplateCount-1; i++)
            {
                DateTime YearStartingDate = new DateTime(year, 1, 1);
                int FirstDayInYear = 0;
                FirstDayInYear = CalcFirstDayInYear(YearStartingDate, FirstDayInYear);

                int DayInYear;
                if (weeknumber == 1)
                    DayInYear = AllShiftTemplates[i]._startTime.Day;
                else if (weeknumber == 2)
                    DayInYear = FirstDayInYear + AllShiftTemplates[i]._startTime.Day;
                else
                    DayInYear = FirstDayInYear + (weeknumber - 2) * 7 + AllShiftTemplates[i]._startTime.Day;

                int dayH = 15; //Skal sende en værdi med til funktionen så den returnerer remainder
                int md = 1;
                TotalDayToDayInMonth(DayInYear, year, ref dayH, ref md);

                DateTime start = new DateTime(year, md, dayH, AllShiftTemplates[i]._startTime.Hour, AllShiftTemplates[i]._startTime.Minute, AllShiftTemplates[i]._startTime.Second);
                DateTime end = new DateTime(year, md, dayH, AllShiftTemplates[i]._endTime.Hour, AllShiftTemplates[i]._endTime.Minute, AllShiftTemplates[i]._endTime.Second);

                if (IfDateIsNotHoliday(start, AllHolidays))
                {

                    // Sortering af usere - Først findes de medarbejdere som kan arbejde på den type vagt
                    List<User> CompatibleUsers = new List<User>();

                    foreach (User u in AllUsers)
                    {
                        if (AllShiftTemplates[i].Tag.Any())
                            CompatibleUsers.Add(u);

                        else if (CompareTags(u.UserCategories, AllShiftTemplates[i].Tag))
                            CompatibleUsers.Add(u);
                    }
                    CalculateDayPrice(AllShiftTemplates[i]._startTime.Day, ref PossitiveDayCost, ref NegativeDayCost, false);
                    string UserName = SortUserList(CompatibleUsers, PossitiveDayCost, NegativeDayCost, start);

                    Shift resultShift = new Shift(start, end, Database.Instance.ListToString(AllShiftTemplates[i].Tag), UserName, weeknumber);

                    resultShift.SaveShift();
                    Core._instance.AddShiftToList(resultShift);
                }
            }
        }

        private int CalcFirstDayInYear(DateTime YearStartingDate, int FirstDayInYear)
        {
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
            return FirstDayInYear;
        }

        private void TotalDayToDayInMonth(int DayInYear, int year, ref int RemaindingDaysInCurrentMonth, ref int ResultMonth)
        {
            int RunningDays = 0;
            int MonthLenght = 0;
            ResultMonth = 1;

            for (int i = 1; i <= 12; i++)
            {
                MonthLenght = DateTime.DaysInMonth(year, ResultMonth);
                if ((DayInYear - RunningDays) <= MonthLenght)
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

        private string SortUserList(List<User> UserList, int PossitiveDayWeight, int NegativeDayweight, DateTime Date)
        {

            // Er der kun en user er vagten hans, er der ingen skal der returneres en fejl
            Console.WriteLine("Number of user to be sorted = "+UserList.Count);
            if (UserList.Count <= 1)
            {
                if (UserList.Count == 1)
                    return UserList.First().UserName;
                else
                    throw new ArgumentException("The userlist is currently empty so no user match the tags on a template");
            }

            // Skal først tage forbehold for medarbejder ønsker ved at undersøge om deres point > vægt og så fjerne pointsne og  fjrerne dem fra listen
            List<UserFreeRequest> Requests = Core.Instance.GetAllUserRequests();
            List<User> UnAvalibleUsers = new List<User>();
            int TotalNumberOfRequests = Requests.Count;
            int TotalNumberOfUsers = UserList.Count;
            
            foreach(UserFreeRequest q in Requests)
            {
                foreach(User u in UserList)
                {
                    if (q.User.Equals(u)  && (Date != q.StartTime))
                    {
                        UnAvalibleUsers.Add(u);
                        break;
                    }
                }
            }

            var SortedUnAvalibleUsers = UnAvalibleUsers.OrderBy(user => user.Points);
            UnAvalibleUsers = SortedUnAvalibleUsers.ToList();

            if (UnAvalibleUsers.Count == UserList.Count)
            {
                foreach (User u in UnAvalibleUsers)
                {
                    if (UnAvalibleUsers.Count > 1)
                    {
                        if (u.Points >= NegativeDayweight)
                        {
                            u.UpdateUserPointBalance(-NegativeDayweight);
                            UserList.Remove(u);
                        }
                        else
                        {
                            u.Points = 0;
                            UserList.Remove(u);
                        }
                    }
                }
            }
            else
            {
                foreach (User u in UnAvalibleUsers)
                {
                    if (u.Points >= NegativeDayweight)
                    {
                        u.UpdateUserPointBalance(-NegativeDayweight);
                        UserList.Remove(u);
                    }
                    else
                    {
                        u.Points = 0;
                        UserList.Remove(u);
                    }
                }
            }
            // Skal derefter sorterer efter personen med færrest point
            var SortedList = UserList.OrderBy(user => user.Points);
            UserList = SortedList.ToList();

            // Til sidst tjekkes det om det er samme person som sidste år som arbejede på denne dato
            /*if (RepetingWorker(UserList.First(), UserList))
            {
                UserList.Remove(UserList.First());
            }
            else*/
                UserList.First().UpdateUserPointBalance(PossitiveDayWeight);
                return UserList.First().UserName;
        }

        private bool CompareTags(List<string> UserTags, List<string> ShiftTags)
        {
            /*if (!ShiftTags.Any() || (!ShiftTags.Any() && !UserTags.Any()))
                return true;*/
            return !ShiftTags.Except(UserTags).Any();
        }

        public bool RepetingWorker(User repeat, List<Shift> shifts)
        {
            foreach (Shift s in shifts)
            {
                
            }
            return false;
        }

        public void CalculateDayPrice(int day, ref int PossitiveDayPrice, ref int NegativeDayPrice, bool isHoliday = false)
        {
            if (!isHoliday)
            {
                switch (day)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        PossitiveDayPrice = 2;
                        NegativeDayPrice = 4;
                        break;
                    case 5:
                    case 6:
                    case 7:
                        PossitiveDayPrice = 4;
                        NegativeDayPrice = 6;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                PossitiveDayPrice = 6;
                NegativeDayPrice = 8;
            }
        }

        private bool IfDateIsNotHoliday(DateTime startDate, List<Holiday> Holidays)
        {
            foreach (Holiday h in Holidays) 
            {
                if (h.Date == startDate)
                    return false;
            }

            return true;
        } 

        public List<UserFreeRequest> GetAllFreeRequests()
        {
            return _allRequests;
        }

        public void WarningError(string msg)
        {
            throw new NotImplementedException();
        }

        public int GetWeeksInYear(int year)
        {
            System.Globalization.DateTimeFormatInfo dfi = System.Globalization.DateTimeFormatInfo.CurrentInfo;
            DateTime dt = new DateTime(year, 12, 31);
            System.Globalization.Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(dt, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }
    }
}
