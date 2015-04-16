using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    class Core
    {
        static List<User> AllUsers = new List<User>();
        public static void Temp()
        {
            User bruger = new User("Jesper", "lol");
            DateTime start = new DateTime(2015, 04, 20, 15, 30, 00);
            DateTime end = new DateTime(2015, 04, 20, 18, 00, 00);

            Shift vagt = new Shift(bruger, start, end);

            Console.WriteLine(vagt.ToString()); 
            
        }

        public static List<User> GetAllUsers()
        {
            return AllUsers;
        }

        public static void AddUserToList(User user)
        {
            AllUsers.Add(user);
        }
    }
}
