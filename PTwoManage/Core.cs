using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PTwoManage
{
    public sealed class Core
    {
        // Definition of the Core singelton object
        static readonly Core _instance = new Core();

        //Lists of object contained in the Core instance
        private List<User> _allUsers;
        private List<string> _allTags;
        private List<Shift> _allShifts;
        private List<ShiftTemplate> _allTemplates;
        private List<UserFreeRequest> _allRequests;
        private List<Holiday> _allHolidays;
        private List<string> _info;

        // A method for retrieving a reference to the Core instance
        public static Core Instance
        {
            get { return _instance; }
        }

        // The static instructor which is called the first time a method or field from Core is called
        Core()
        {
            _allTags = new List<string>();
            _allUsers = new List<User>();
            _allTemplates = new List<ShiftTemplate>();
            _allRequests = new List<UserFreeRequest>();
            _allHolidays = new List<Holiday>();
            _allShifts = new List<Shift>();
            _info = new List<string>();

        }

        // Below are a series of method for loading different object from the database and instanciate them in the lists contained on the Core indstance
        public void LoadHolidaysFromDatabase()
        {
            string sql = "SELECT * FROM HolidayTable";
            Database.Instance.Read(sql, ref _info, Database.Instance.HolidayTableColumns);
            foreach (var item in _info)
            {
                string[] split = item.Split(new Char[] { ',' });
                _allHolidays.Add(new Holiday(DateTime.Parse(split[0])));
            }
        }

        public void LoadTagsFromDatabase()
        {
            string sql = "SELECT * FROM TagTable";
            Database.Instance.Read(sql, ref _info, Database.Instance.TagTableColumns);
            foreach (var item in _info)
            {
                string[] split3 = item.Split(new Char[] { ',' });
                if (!(split3[0] == ""))
                    _allTags.Add(split3[0]);
            }
        }

        public void LoadUsersFromDatabase()
        {
            string sql = "SELECT * FROM userTable";
            Database.Instance.Read(sql, ref _info, Database.Instance.userTableColumns);
            foreach (var item in _info)
            {
                string[] split = item.Split(new Char[] { ',' });
                _allUsers.Add(new User(int.Parse(split[0]), split[1], split[2], split[3], split[4], split[5], split[6], Database.Instance.StringToList(split[7]), int.Parse(split[8])));
            }
        }

        public void LoadShiftTemplatesFromDatabase()
        {
            string sql = "SELECT * FROM ShiftTemplate";
            Database.Instance.Read(sql, ref _info, Database.Instance.ShiftTemplateTableColumns);
            ShiftTemplate t;
            foreach (var item in _info)
            {
                string[] split2 = item.Split(new Char[] { ',' });
                t = new ShiftTemplate(DateTime.Parse(split2[1]), DateTime.Parse(split2[2]), split2[3]);
                t.GeneratePrintableInfo();
                _allTemplates.Add(t);
            }
        }

        public void LoadShiftsFromDatabase()
        {
            string sql = "SELECT * FROM ShiftTable";
            Database.Instance.Read(sql, ref _info, Database.Instance.ShiftTableColumns);
            foreach (var item in _info)
            {
                string[] split = item.Split(new Char[] { ',' });
                _allShifts.Add(new Shift(DateTime.Parse(split[1]), DateTime.Parse(split[2]), split[3], split[4], int.Parse(split[5])));
            }
        }

        public void LoadUserFreeRequestsFromDatabase()
        {
            string sql = "SELECT * FROM FreeRequestTable";
            Database.Instance.Read(sql, ref _info, Database.Instance.FreeTimeRequestColumns);
            string holder = "";
            foreach (var item in _info)
            {
                string[] split4 = item.Split(new Char[] { ',' });
                if (split4[2] == "")
                    holder = "No message was entered";
                else
                    holder = split4[2];
                _allRequests.Add(new UserFreeRequest(DateTime.Parse(split4[0]), DateTime.Parse(split4[1]), holder, split4[3]));
            }
        }

        // A series of methods for retieving the list of objects in the Core instance
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

        public List<string> GetAllTags()
        {
            return _allTags;
        }

        public List<UserFreeRequest> GetAllFreeRequests()
        {
            return _allRequests;
        }

        public List<Holiday> GetAllHolidays()
        {
            //Returns alle holidays in order from oldest to newest
            _allHolidays = _allHolidays.OrderBy(holiday => holiday.Date).ToList();
            return _allHolidays;
        }

        // A method for retrieving Shifts with a specific Day, week and year
        public List<Shift> GetAllShiftsForDayInWeekInYear(DayOfWeek day, int weekNum, int year)
        {
            List<Shift> dShifts = new List<Shift>();
            foreach (Shift s in _allShifts)
            {

                if (s.Day == day && s.Week == weekNum && s.GetYear() == year)
                    dShifts.Add(s);
            }
            return dShifts;
        }

        // A series of methods for adding or removing a singe object from one of the Core instance lists
        public void AddUserToList(User user)
        {
            _allUsers.Add(user);
        }

        public void RemoveUserFromList(User user)
        {
            _allUsers.Remove(user);
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

        public void AddTagToList(string tag)
        {
            _allTags.Add(tag);
        }

        public void DeleteTagFromList(string s)
        {
            _allTags.Remove(s);
        }

        public void AddToHolidayList(Holiday NewHoliday)
        {
            _allHolidays.Add(NewHoliday);
        }

        public void RemoveHolidayFromList(Holiday toRemove)
        {
            _allHolidays.Remove(toRemove);
        }

        //The main method for generating shifts
        public void ScheduleGenerator(int weeknumber, int year)
        {
            // Instead of retreiving a reference to the lists from core multiple times
            // The method saves the reference for further use
            List<User> AllUsers = Core.Instance.GetAllUsers();
            List<ShiftTemplate> AllShiftTemplates = Core.Instance.GetAllTemplates();
            List<Holiday> AllHolidays = Core.Instance.GetAllHolidays();

            //The templates are sorted so that the generetor generates from monday to sunday
            var SortedShiftTemplates = AllShiftTemplates.OrderBy(template => template.StartTime);
            AllShiftTemplates = SortedShiftTemplates.ToList();

            // Since it is the beginning of a new week to generate for all users WorkInWeek is set to 0
            foreach (User u in AllUsers)
                u.WorkInWeek = 0;

            // Local variables for further use
            int PossitiveDayCost = 0;
            int NegativeDayCost = 0;
            int resultDay = 0;
            int resultMonth = 0;

            // The loop which generates a Shift object for each template in the list of templates
            int TemplateCount = AllShiftTemplates.Count;
            for (int i = 0; i <= TemplateCount - 1; i++)
            {
                // The year startingdate is found by makeing a date with january the 1st for the selected year
                DateTime YearStartingDate = new DateTime(year, 1, 1);

                // The first day is calculated
                int FirstDayInYear = CalcFirstDayInYear(YearStartingDate);
                if (weeknumber == 1)
                {
                    //If the date is one of the days not exsisting in the first week of the year, the loop continues to the next template
                    if (AllShiftTemplates[i].StartTime.Day <= FirstDayInYear)
                        continue;
                }

                int DayInYear = GetDayInYear(weeknumber, AllShiftTemplates[i].StartTime, FirstDayInYear);
                TotalDayToDayInMonth(DayInYear, year, ref resultDay, ref resultMonth);

                DateTime start = new DateTime(year, resultMonth, resultDay, AllShiftTemplates[i].StartTime.Hour, AllShiftTemplates[i].StartTime.Minute, AllShiftTemplates[i].StartTime.Second);
                DateTime end = new DateTime(year, resultMonth, resultDay, AllShiftTemplates[i].EndTime.Hour, AllShiftTemplates[i].EndTime.Minute, AllShiftTemplates[i].EndTime.Second);

                //If the date is a holiday the loop continues to next iteration
                if (IfDateIsNotHoliday(start, AllHolidays))
                {

                    // Sorting of Users. The compatible Users are found and saved in a list
                    List<User> CompatibleUsers = new List<User>();
                    GetCompatibleUsers(AllUsers, AllShiftTemplates[i], CompatibleUsers);

                    CalculateDayPrice(AllShiftTemplates[i].StartTime.Day, ref PossitiveDayCost, ref NegativeDayCost, false);

                    // The SortUserList method determines the user to take the Shift based on different parameters
                    string UserName = SortUserList(CompatibleUsers, PossitiveDayCost, NegativeDayCost, start);

                    // The shift is created and saved to the Core and to the Database 
                    Shift resultShift = new Shift(start, end, Database.Instance.ListToString(AllShiftTemplates[i].Tag), UserName, weeknumber);
                    resultShift.SaveShift();
                    Core.Instance.AddShiftToList(resultShift);
                }
                else
                    continue;
            }
        }

        // Serches the userlist for Users matching the tags in the specified ShiftTemplate
        private void GetCompatibleUsers(List<User> AllUsers, ShiftTemplate shiftTemplates, List<User> CompatibleUsers)
        {
            foreach (User u in AllUsers)
            {
                if (shiftTemplates.Tag.Any())
                    CompatibleUsers.Add(u);

                else if (CompareTags(u.Tags, shiftTemplates.Tag))
                    CompatibleUsers.Add(u);
            }
        }

        // Finds the current day in the year being generated for
        private static int GetDayInYear(int weeknumber, DateTime StartTime, int FirstDayInYear)
        {
            int DayInYear;
            if (weeknumber == 1)
            {
                DayInYear = StartTime.Day - (7 - FirstDayInYear);
                if (DayInYear <= 0)
                    DayInYear = 1;
            }
            else if (weeknumber == 2)
                DayInYear = FirstDayInYear + StartTime.Day;
            else
                DayInYear = FirstDayInYear + (weeknumber - 2) * 7 + StartTime.Day;
            return DayInYear;
        }

        //  Returns which day is the first day of the year in a DayOfWeek format
        public int CalcFirstDayInYear(DateTime YearStartingDate)
        {
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
            return FirstDayInYear;
        }

        // Alters a DayInYear to a corresponding month and day in the month
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

            // If the list only contains one user this user is returned
            if (UserList.Count <= 1)
            {
                if (UserList.Count == 1)
                    return UserList.First().UserName;
                else
                    throw new ArgumentException("The userlist is currently empty so no user match the tags on a template");
            }

            // Checks for each request if it is in the week being generated for and then finds the user
            List<UserFreeRequest> Requests = Core.Instance.GetAllUserRequests();
            List<User> UnAvalibleUsers = new List<User>();
            int TotalNumberOfRequests = Requests.Count;
            int TotalNumberOfUsers = UserList.Count;

            foreach (UserFreeRequest q in Requests)
            {
                foreach (User u in UserList)
                {
                    if (q.User.Equals(u) && (Date.DayOfYear >= q.StartTime.DayOfYear) && (Date.DayOfYear <= q.EndTime.DayOfYear))
                    {
                        UnAvalibleUsers.Add(u);
                        break;
                    }
                }
            }

            // Quick sorting of the list by sorting for points
            UnAvalibleUsers = UnAvalibleUsers.OrderBy(user => user.Points).ToList();

            // If everyone has requested time off, there must be at least one worker foreced to take the shift.
            if (UnAvalibleUsers.Count == UserList.Count)
            {
                foreach (User u in UnAvalibleUsers)
                {
                    if (UserList.Count > 1)
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
                    else
                    {
                        UserList.First().WorkInWeek++;
                        UserList.First().UpdateUserPointBalance(PossitiveDayWeight);
                        return UserList.First().UserName;
                    }
                }
            }
            // if some (not all, maybe none) Users want time off they get their time off
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

            // Then the list ils sorted with the Quicksort 3 way algorithm
            User[] ar = UserList.ToArray();
            Qsort3(ar);
            UserList = ar.ToList();

            // After sorting the list, the first user on the list is the user which gets the Shift
            UserList.First().WorkInWeek++;
            UserList.First().UpdateUserPointBalance(PossitiveDayWeight);
            return UserList.First().UserName;
        }

        //Qsort with 3 way partitioning 
        public void Qsort3(IComparable[] a)
        {
            Qsort3(a, 0, a.Length - 1);
        }

        public void Qsort3(IComparable[] compares, int low, int high)
        {
            if (high <= low) return;
            int lt = low, gt = high;
            IComparable v = compares[low];
            int i = low;
            while (i <= gt)
            {
                int cmp = compares[i].CompareTo(v);
                if (cmp < 0)
                    Exchange(compares, lt++, i++);
                else if (cmp > 0)
                    Exchange(compares, i, gt--);
                else
                    i++;
            }
            Qsort3(compares, low, lt - 1);
            Qsort3(compares, gt + 1, high);
        }

        // A function for switching to elements of an array
        public void Exchange(Object[] arr, int i, int j)
        {
            Object swap = arr[i];
            arr[i] = arr[j];
            arr[j] = swap;
        }

        public void Debug_PrintSortedUserList()
        {
            for (int i = 0; i < _allUsers.Count - 1; i++)
            {
                Console.WriteLine(_allUsers[i].UserName + " Days: " + _allUsers[i].WorkInWeek + " - Points: " + _allUsers[i].Points);
            }
        }

        // A method used for checking if a User contains all tags specified by a ShiftTemplate
        public bool CompareTags(List<string> UserTags, List<string> ShiftTags)
        {
            return !ShiftTags.Except(UserTags).Any();
        }

        // A method used for calculating the price of a day
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

        // Check if the given day is a holiday defined in the list of holidays
        private bool IfDateIsNotHoliday(DateTime startDate, List<Holiday> Holidays)
        {
            foreach (Holiday h in Holidays)
            {
                if (h.Date == startDate)
                    return false;
            }
            return true;
        }

        public void WarningError(string msg)
        {
            throw new NotImplementedException();
        }

        // This method returns the number of weeks in a given year
        public int GetWeeksInYear(int year, int month = 12, int day = 31)
        {
            System.Globalization.DateTimeFormatInfo dfi = System.Globalization.DateTimeFormatInfo.CurrentInfo;
            DateTime dt = new DateTime(year, month, day);
            System.Globalization.Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(dt, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }
    }
}
