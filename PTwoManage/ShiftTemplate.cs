using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    class ShiftTemplate
    {
        public string Date;
        protected DateTime _startTime;
        protected DateTime _endTime;
        public List<string> Tag = new List<string>();
        public bool IsTemplate;


        public ShiftTemplate(string date, DateTime starttime, DateTime endtime, List<string> tag)
        {
            Date = date;
            _startTime = starttime;
            _endTime = endtime;
            IsTemplate = true;
            Tag = tag;
        }
    }
}
