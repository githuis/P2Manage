using System;

namespace PTwoManage
{
    public class Holiday
    {
        // A holiday is denined only by a date which is saved in the database
        public Holiday(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; set; }

        // This method saves a holiday to the database by sending a SQL string to the execute command
        public void SaveHoliday()
        {
            var sql = "INSERT INTO HolidayTable (day) values ('" + Date + "')";
            Database.Instance.Execute(sql);
        }

        // This method serches for a specific holiday and removes it from the database
        public void RemoveHoliday()
        {
            Core.Instance.RemoveHolidayFromList(this);
            var sql = "DELETE FROM HolidayTable WHERE day='" + Date + "'";
            Database.Instance.Execute(sql);
        }
    }
}