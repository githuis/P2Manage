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
        private List<User> _allUsers = new List<User>();
        private List<string> _allTags = new List<string>();
        private List<ShiftTemplate> _allTemplates = new List<ShiftTemplate>();
        private List<UserFreeRequest> _allRequests = new List<UserFreeRequest>();
        private List<string> info = new List<string>();

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
            info = new List<string>();
            
            string sql = "SELECT * FROM userTable";
            Database.Instance.Read(sql, ref info, Database.Instance.userTableColumns);
            foreach (var item in info)
            {
                string[] split = item.Split(new Char[]{','});
                _allUsers.Add(new User(int.Parse(split[0]), split[1], split[2], split[3], split[4], split[5], split[6], Database.Instance.stringToList(split[7]), int.Parse(split[8])));
            }

            sql = "SELECT * FROM ShiftTemplate";
            Database.Instance.Read(sql, ref info, Database.Instance.ShiftTemplateTableColumns);
            foreach (var item in info)
            {
                string[] split2 = item.Split(new Char[] {','});
                _allTemplates.Add(new ShiftTemplate(split2[1], DateTime.Parse(split2[2]), DateTime.Parse(split2[3]), split2[4]));
            }

            sql = "SELECT * FROM TagTable";
            Database.Instance.Read(sql, ref info, Database.Instance.TagTableColumns);
            foreach (var item in info)
            {
                string[] split3 = item.Split(new Char[]{','});
                if(!(split3[0] == ""))
                    _allTags.Add(split3[0]);
            }

            sql = "SELECT * FROM FreeRequestTable";
            Database.Instance.Read(sql, ref info, Database.Instance.FreeTimeRequestColumns);
            foreach (var item in info)
            {
                string holder;
                string[] split4 = item.Split(new Char[] { ',' });
                if(split4[2] == null)
                    holder = "empty";
                else
                    holder = split4[2];
                _allRequests.Add(new UserFreeRequest(DateTime.Parse(split4[0]), DateTime.Parse(split4[1]), holder, split4[3]));
                    
            }

        }
        
       
       /* public void Run()
        {
           // User bruger = new User(1,"lucrah2", "1234", "luca2", "564455648", "88888888", "jgdagmailcom");

            DateTime start = new DateTime(2015, 04, 20, 15, 30, 00);
            DateTime end = new DateTime(2015, 04, 20, 18, 00, 00);

           // Shift vagt = new Shift(bruger, start, end);
			
            Console.WriteLine(vagt.ToString()); 
            
        }*/

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
