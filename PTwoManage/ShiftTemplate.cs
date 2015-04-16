using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    class ShiftTemplate
    {
        List<TimeTemplate> TemplateCollection = new List<TimeTemplate>();
        
        DateTime time1 = new DateTime(2015, 12, 24, 08, 00, 00);
        DateTime time2 = new DateTime(2015, 12, 24, 16, 00, 00);

        public void MakeShift()
        {
            TimeTemplate MondayShift = new TimeTemplate(time1, time2);
            TimeTemplate TuesdayShift = new TimeTemplate(time1.AddDays(1), time2.AddDays(1));
        }

    }
    
    class TimeTemplate
    {
        public DateTime Start;
        public DateTime End;

        public TimeTemplate(DateTime StartShift, DateTime EndShift)
        {
            Start = StartShift;
            End = EndShift;
        }
    }
}
