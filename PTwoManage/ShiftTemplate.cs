using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public class ShiftTemplate
    {
        //Properties used by the ShiftTemplate class
        protected DateTime _startTime;
        protected DateTime _endTime;
        public List<string> Tag = new List<string>();

        //Variables used for printing information in various windows
        public string PrintableDay { get; set; }
        public string PrintableTime { get; set; }
        public string PrintableTags { get; set; }
        public string SchedulePrintableTags { get; set; }

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        // The constructor for ShiftTemplate objects which generates printable for the ShiftTemplate
        public ShiftTemplate(DateTime starttime, DateTime endtime, string tag)//List<string> tag
        {
            _startTime = starttime;
            _endTime = endtime;
            Tag = Database.Instance.StringToList(tag);
            GeneratePrintableInfo();
        }

        // A method used for saving the ShiftTemplate to the database by calling the Execute function
        public void SaveInfoShiftTemplate()
        {
            ShiftTemplate template = this;
            string sql = "INSERT INTO ShiftTemplate (start, end, tag) values ('" + template.StartTime.ToString() + "', '" + template.EndTime.ToString() + "', '" + Database.Instance.ListToString(template.Tag) + "')";
            Database.Instance.Execute(sql);
        }

        // A method for deleting the ShiftTemplate from the database
        public void DeleteShiftTemplate()
        {
            string sql = "DELETE FROM ShiftTemplate WHERE (start = '" + this.StartTime.ToString() + "') AND (end = '" + this.EndTime.ToString() + "') AND (tag = '" + Database.Instance.ListToString(this.Tag) + "')";
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

            if (Tag.Count == 1)
                SchedulePrintableTags = Tag[0].ToString();
            else if (Tag.Count >= 2)
                SchedulePrintableTags = Tag[0] + "+[" + (Tag.Count - 1).ToString() + " more]";
            else
                SchedulePrintableTags = "";

            Tag.ForEach(x => PrintableTags += (x.ToString() + ", "));
        }
    }
}
