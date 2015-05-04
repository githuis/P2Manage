using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public class Holiday
    {
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public Holiday(DateTime date)
        {
            _date = date;
        }

        public void SaveHoliday()
        {
            Holiday holiday = this;
            string sql = "INSERT INTO HolidayTable (day) values ('" + holiday._date.ToString() + "')";
            Database.Instance.Execute(sql);
        }

        public void RemoveHoliday()
        {
            Holiday holiday = this;
            string sql = "DELETE FROM HolidayTable WHERE day='" + holiday._date.ToString() + "'";
            Database.Instance.Execute(sql);
        }

        
    }
}
