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
        public string PrintableDay { get; set; }
        public string PrintableTime { get; set; }
        public string PrintableTags { get; set; }
        public string SchedulePrintableTags { get; set; }

        public ShiftTemplate(DateTime starttime, DateTime endtime, string tag)//List<string> tag
        {
            _startTime = starttime;
            _endTime = endtime;
            Tag = Database.Instance.StringToList(tag);
            GeneratePrintableInfo();
        }

        public void SaveInfoShiftTemplate()
        {
            ShiftTemplate template = this;
            string sql = "INSERT INTO ShiftTemplate (start, end, tag) values ('" + template._startTime.ToString() + "', '" + template._endTime.ToString() + "', '" + Database.Instance.ListToString(template.Tag) + "')";
            Database.Instance.Execute(sql);
        }

        public void DeleteShiftTemplate()
        {
            string sql = "DELETE FROM ShiftTemplate WHERE (start = '" + this._startTime.ToString() + "') AND (end = '" + this._endTime.ToString() + "') AND (tag = '" + Database.Instance.ListToString(this.Tag) + "')";
            Database.Instance.Execute(sql);
            Core.Instance.GetAllTemplates().Remove(this);
        }

        public override string ToString()
        {
            return PrintableDay + " " + PrintableTime;
        }

        public void GeneratePrintableInfo()
        {
            PrintableDay = _startTime.DayOfWeek.ToString();
            PrintableTime = _startTime.ToShortTimeString() + " - " + _endTime.ToShortTimeString();
            PrintableTags = "";
            

            Tag.ForEach(x => PrintableTags += (x.ToString() + ", "));
        }
    }
}
