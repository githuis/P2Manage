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
            string sql = "INSERT INTO HolidayTable (day) values ('" + this._date.ToString() + "')";
            Database.Instance.Execute(sql);
        }

        public void RemoveHoliday()
        {
            Core.Instance.RemoveHolidayFromList(this);
            string sql = "DELETE FROM HolidayTable WHERE day='" + this._date.ToString() + "'";
            Database.Instance.Execute(sql);
        }

        
    }
}
