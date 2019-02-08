using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class StudentBookingModel
    {
        public string studentind { get; set; }
        public string voucherid { get; set; }
        public string bookingid { get; set; }
        public string bookingname { get; set; }
        public string courseid { get; set; }
        public string coursename { get; set; }
        public string coursenickname { get; set; }
        public string insertby { get; set; }
        public string insertdate { get; set; }

        public string user { get; set; }
        public string ip { get; set; }

        public string result { get; set; }
        public string msg { get; set; }

        public string tabActive { get; set; }
    }
}