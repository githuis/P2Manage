using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public class ShiftTemplate
    {
        public string Date;
        public DateTime _startTime;
        public DateTime _endTime;
        public List<string> Tag = new List<string>();

        public ShiftTemplate(DateTime starttime, DateTime endtime, string tag)//List<string> tag
        {
            _startTime = starttime;
            _endTime = endtime;
            Tag = Database.Instance.stringToList(tag);
        }

        public void SaveInfoShiftTemplate()
        {
            ShiftTemplate template = this;
            string sql = "INSERT INTO ShiftTemplate (start, end, tag) values ('" + template._startTime.ToString() + "', '" + template._endTime.ToString() + "', '" + Database.Instance.listToString(template.Tag) + "')";
            Database.Instance.Execute(sql);
        }
    }
}
