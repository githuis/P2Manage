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
            _info = new List<string>();
            _allShifts = new List<Shift>();
            
            string sql = "SELECT * FROM userTable";
            Database.Instance.Read(sql, ref _info, Database.Instance.userTableColumns);
            foreach (var item in _info)
            {
                string[] split = item.Split(new Char[]{','});
                _allUsers.Add(new User(int.Parse(split[0]), split[1], split[2], split[3], split[4], split[5], split[6], Database.Instance.stringToList(split[7]), int.Parse(split[8])));
            } 
            
            sql = "SELECT * FROM ShiftTemplate";
            Database.Instance.Read(sql, ref _info, Database.Instance.ShiftTemplateTableColumns);
            foreach (var item in _info)
            {
                string[] split2 = item.Split(new Char[] {','});
                _allTemplates.Add(new ShiftTemplate(split2[1], DateTime.Parse(split2[2]), DateTime.Parse(split2[3]), split2[4]));
            }

            sql = "SELECT * FROM TagTable";
            Database.Instance.Read(sql, ref _info, Database.Instance.TagTableColumns);
            foreach (var item in _info)
            {
                string[] split3 = item.Split(new Char[]{','});
                if(!(split3[0] == ""))
                    _allTags.Add(split3[0]);
            }

            sql = "SELECT * FROM FreeRequestTable";
            Database.Instance.Read(sql, ref _info, Database.Instance.FreeTimeRequestColumns);
            foreach (var item in _info)
            {
                string holder;
                string[] split4 = item.Split(new Char[] { ',' });
                if (split4[2] == null)
                    holder = "empty";
                else
                    holder = split4[2];
                _allRequests.Add(new UserFreeRequest(DateTime.Parse(split4[0]), DateTime.Parse(split4[1]), holder, split4[3]));
            }
       

        }
       
       public void Run()
        {

            _allShifts.Add(new Shift("", new DateTime(2015, 04, 21, 15, 30, 00), new DateTime(2015, 04, 21, 16, 30, 00), "Åbner", 10, 2));
            _allShifts.Add(new Shift("", new DateTime(2015, 04, 22, 15, 30, 00), new DateTime(2015, 04, 22, 16, 30, 00), "Åbner", 10, 2));
            _allShifts.Add(new Shift("", new DateTime(2015, 04, 23, 15, 30, 00), new DateTime(2015, 04, 23, 16, 30, 00), "Åbner", 10, 2));
            _allShifts.Add(new Shift("", new DateTime(2015, 04, 24, 15, 30, 00), new DateTime(2015, 04, 24, 16, 30, 00), "Åbner", 10, 2));
            _allShifts.Add(new Shift("", new DateTime(2015, 04, 25, 15, 30, 00), new DateTime(2015, 04, 25, 16, 30, 00), "Åbner", 10, 2));
            _allShifts.Add(new Shift("", new DateTime(2015, 04, 26, 15, 30, 00), new DateTime(2015, 04, 26, 16, 30, 00), "Åbner", 10, 2));
            _allShifts.Add(new Shift("", new DateTime(2015, 04, 27, 15, 30, 00), new DateTime(2015, 04, 27, 16, 30, 00), "Åbner", 10, 2));
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

        public void CoreInit()
        {
            Console.WriteLine("Core activated");
        }
    }
}
