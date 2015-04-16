using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    class Core
    {
        public static void Temp()
        {
            User bruger = new User(1,"lucrah2", "1234", "luca2", 564455648, 88888888, "jgdagmailcom");

            DateTime start = new DateTime(2015, 04, 20, 15, 30, 00);
            DateTime end = new DateTime(2015, 04, 20, 18, 00, 00);

            Shift vagt = new Shift(bruger, start, end);

            Console.WriteLine(vagt.ToString());

            Database db = new Database();
            db.FillUserTable(bruger);
        }
    }
}
