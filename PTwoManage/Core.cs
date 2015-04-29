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
        private List<string> _AllTags;
        private List<Shift> _allShifts;

        public static Core Instance
        {
            get { return _instance; }
        }

        Core()
        {
            
            _allUsers = new List<User>();
            List<string> info = Database.Instance.readInfo;
            string sql = "SELECT * FROM userTable";
            Database.Instance.Read(sql, Database.Instance.userTableColumns);
            foreach (var item in info)
            {
                string[] split = item.Split(new Char[]{','});
                _allUsers.Add(new User(int.Parse(split[0]), split[1], split[2], split[3], split[4], split[5], split[6]));
                Console.WriteLine("User loaded: " + split[1]);
            } 
            
            _AllTags = new List<string>();
            _allShifts = new List<Shift>();

        }
        
       
        public void Run()
        {
            User bruger = new User(1,"lucrah2", "1234", "Jens Petersen", "564455648", "88888888", "jgdagmailcom");

            DateTime start = new DateTime(2015, 04, 20, 15, 30, 00);
            DateTime end = new DateTime(2015, 04, 20, 18, 00, 00);
            DateTime end2 = new DateTime(2015, 04, 20, 19, 00,00);

            Shift vagt = new Shift(bruger, start, end);

            Console.WriteLine(vagt.ToString());
            _allShifts.Add(vagt);
            _allShifts.Add(vagt);
            _allShifts.Add(new Shift(bruger, new DateTime(2015, 04, 21, 15, 30, 00), new DateTime(2015, 04, 21, 16, 30, 00)));
            _allShifts.Add(new Shift(bruger, new DateTime(2015, 04, 22, 15, 30, 00), new DateTime(2015, 04, 22, 16, 30, 00)));
            _allShifts.Add(new Shift(bruger, new DateTime(2015, 04, 23, 15, 30, 00), new DateTime(2015, 04, 23, 16, 30, 00)));
            _allShifts.Add(new Shift(bruger, new DateTime(2015, 04, 24, 15, 30, 00), new DateTime(2015, 04, 24, 16, 30, 00)));
            _allShifts.Add(new Shift(bruger, new DateTime(2015, 04, 25, 15, 30, 00), new DateTime(2015, 04, 25, 16, 30, 00)));
            _allShifts.Add(new Shift(bruger, new DateTime(2015, 04, 26, 15, 30, 00), new DateTime(2015, 04, 26, 16, 30, 00)));
            _allShifts.Add(new Shift(bruger, new DateTime(2015, 04, 27, 15, 30, 00), new DateTime(2015, 04, 27, 16, 30, 00)));
        }

        public List<User> GetAllUsers()
        {
            return _allUsers;
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
            string s = "lol";
            _AllTags.Add(s);
            Console.WriteLine(_AllTags);
            return _AllTags;
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
    }
}
