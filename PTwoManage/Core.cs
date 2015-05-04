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

            _info = new List<string>();
            _allShifts = new List<Shift>();
            
            string sql = "SELECT * FROM userTable";
            Database.Instance.Read(sql, ref _info, Database.Instance.userTableColumns);
            foreach (var item in _info)
            {
                string[] split = item.Split(new Char[]{','});
                _allUsers.Add(new User(int.Parse(split[0]), split[1], split[2], split[3], split[4], split[5], split[6], Database.Instance.stringToList(split[7]), int.Parse(split[8])));
            }

            Console.WriteLine("Test1");
            
            string sql2 = "SELECT * FROM ShiftTemplate";
            Database.Instance.Read(sql2, ref _info, Database.Instance.ShiftTemplateTableColumns);
            foreach (var item in _info)
            {
                string[] split2 = item.Split(new Char[] {','});
                _allTemplates.Add(new ShiftTemplate(DateTime.Parse(split2[1]), DateTime.Parse(split2[2]), split2[3]));
            }

            Console.WriteLine("Test2");

            string sql3 = "SELECT * FROM TagTable";
            Database.Instance.Read(sql3, ref _info, Database.Instance.TagTableColumns);
            foreach (var item in _info)
            {
                string[] split3 = item.Split(new Char[]{','});
                if(!(split3[0] == ""))
                    _allTags.Add(split3[0]);
            }

            Console.WriteLine("Test3");

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

        }
       
       public void Run()
        {
            _allShifts.Add(new Shift(new DateTime(2015, 04, 21, 15, 30, 00), new DateTime(2015, 04, 21, 16, 30, 00), "Åbner", "10", 2));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 22, 15, 30, 00), new DateTime(2015, 04, 22, 16, 30, 00), "Åbner", "10", 2));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 23, 15, 30, 00), new DateTime(2015, 04, 23, 16, 30, 00), "Åbner", "10", 2));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 24, 15, 30, 00), new DateTime(2015, 04, 24, 16, 30, 00), "Åbner", "10", 2));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 25, 15, 30, 00), new DateTime(2015, 04, 25, 16, 30, 00), "Åbner", "10", 2));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 26, 15, 30, 00), new DateTime(2015, 04, 26, 16, 30, 00), "Åbner", "10", 2));
            _allShifts.Add(new Shift(new DateTime(2015, 04, 27, 15, 30, 00), new DateTime(2015, 04, 27, 16, 30, 00), "Åbner", "10", 2));
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

        public List<Shift> GetAllShifts()
        {
            return _allShifts;
        }

        public void AddTemplateToList(ShiftTemplate template)
        {
            _allTemplates.Add(template);
        }

        public List<Shift> GetAllShifts(DayOfWeek day, int weekNum)
        {
            List<Shift> dShifts = new List<Shift>();
           foreach(Shift s in _allShifts)
           {
               if (s.Day == day && s.Week == weekNum)
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

        public void CoreInit()
        {
            Console.WriteLine("Core activated");
        }

        public void ScheduleGenerator(int weeknumber, int year)
        {
            Console.WriteLine("Test0");

            List<User> AllUsers = Core.Instance.GetAllUsers();
            List<ShiftTemplate> AllShiftTemplates = Core.Instance.GetAllTemplates();
            List<Holiday> AllHolidays = Core.Instance.GetAllHolidays();
            Console.WriteLine("Test1");

            int PossitiveDayCost = 0;
            int NegativeDayCost = 0;

            int TemplateCount = AllShiftTemplates.Count;
            for (int i = 0; i <= TemplateCount-1; i++)
            {
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

                Console.WriteLine("Test1");
                int DayInYear = FirstDayInYear + (weeknumber - 2) * 7 + AllShiftTemplates[i]._startTime.Day;

                int dayH = 0; //Skal sende en værdi med til funktionen så den returnerer remainder
                int md = 0;
                TotalDayToDayInMonth(DayInYear, year, ref dayH, ref md);

                Console.WriteLine(year);
                Console.WriteLine(md);
                Console.WriteLine(dayH);
                Console.WriteLine(FirstDayInYear);
                Console.WriteLine(DayInYear);

                DateTime start = new DateTime(year, md, dayH, AllShiftTemplates[i]._startTime.Hour, AllShiftTemplates[i]._startTime.Minute, AllShiftTemplates[i]._startTime.Second);
                DateTime end = new DateTime(year, md, dayH, AllShiftTemplates[i]._endTime.Hour, AllShiftTemplates[i]._endTime.Minute, AllShiftTemplates[i]._endTime.Second);

                if (IfDateIsNotHoliday(start, AllHolidays))
                {

                    // Sortering af usere - Først findes de medarbejdere som kan arbejde på den type vagt
                    List<User> CompatibleUsers = new List<User>();

                    int NumberOfUsers = AllUsers.Count;

                    foreach (User u in AllUsers)
                    {
                        if (AllShiftTemplates[i].Tag.Any())
                            CompatibleUsers.Add(u);

                        else if (CompareTags(u.UserCategories, AllShiftTemplates[i].Tag))
                            CompatibleUsers.Add(u);
                    }
                    CalculateDayPrice(AllShiftTemplates[i]._startTime.Day, ref PossitiveDayCost, ref NegativeDayCost, false);
                    string UserName = SortUserList(CompatibleUsers, PossitiveDayCost, NegativeDayCost, start);

                    Shift resultShift = new Shift(start, end, Database.Instance.listToString(AllShiftTemplates[i].Tag), UserName, weeknumber);
                    resultShift.SaveShift();
                }
            }
        }

        private void TotalDayToDayInMonth(int DayInYear, int year, ref int RemaindingDaysInCurrentMonth, ref int ResultMonth)
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
                    if (q.UserName == u.UserName && (Date != q.StartTime))
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

            return "DunDUnDUN";
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
    }
}
