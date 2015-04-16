using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    class Week
    {
        DateTime start = new DateTime(2008, 5, 1, 8, 30, 0);
        DateTime slut = new DateTime(2008, 5, 1, 16, 0, 0);
        public int weeknumber;
        day test = new day(Weekdays.Friday, 2, false, start, slut);
        List<day> w = new List<day>();
    }
}
