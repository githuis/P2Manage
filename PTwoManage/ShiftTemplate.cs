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
        public bool IsTemplate;


        public ShiftTemplate(string date, DateTime starttime, DateTime endtime)//List<string> tag
        {
            Date = date;
            _startTime = starttime;
            _endTime = endtime;
            IsTemplate = true;
            Tag.Add("TestTag");
        }

        public void SaveInfoShiftTemplate()
        {
            ShiftTemplate template = this;
            int test = 1;
            string s = "test";
            /*for (int i = 0; i <= template.Tag.Count; i++)
            {
                if(i>0)
                    s += "-";
                s += template.Tag[i];
            }
             */

            string sql = "INSERT INTO ShiftTemplate (id, date, start, end, tag) values (" + test + ", '" + template.Date + "', '" + template._startTime.ToString("dd-MM-yyyy-HH-mm-ss") + "', '" + template._endTime.ToString("dd-MM-yyyy-HH-mm-ss") + "', '" + s + "')";
            Database.Instance.Execute(sql);
        }
    }
}
