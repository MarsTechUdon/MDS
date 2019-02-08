using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class ResultBookingModel
    {
        public string ind { get; set; }
        public string paraid { get; set; }        
        public string bookingdetailind { get; set; }
        public string bookingid { get; set; }
        public string voucherid { get; set; }
        public string result { get; set; }
        public string msg { get; set; }
        public string studentind { get; set; }
    }
}