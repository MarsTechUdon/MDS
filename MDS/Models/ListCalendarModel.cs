using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class ListCalendarModel
    {
        public List<TeacherCalendarModel> TeacherList { get; set; }
        public List<CalendarModel> CalendarList { get; set; }
        public List<lgearModel> GearList { get; set; }
    }
}