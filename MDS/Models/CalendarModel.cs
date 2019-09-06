using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class CalendarModel
    {
        public string eventID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string constraint { get; set; }
        public string rendering { get; set; }
        public string overlap { get; set; }
        public string color { get; set; }
        public string allDay { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public string textColor { get; set; }
        public string resourceId { get; set; }
        public string flgcalendar { get; set; }


    }
}