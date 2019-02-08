using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class CreateLearningModel
    {
        public string courseid { get; set; }        
        public string bookingind { get; set; }
        public string bookingname { get; set; }
        public string studentid { get; set; }
        public string remark { get; set; }
        public string gear { get; set; }
        public List<string> arrteacher { get; set; }
    }
}