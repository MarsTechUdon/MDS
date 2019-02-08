using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class lsubjectModel
    {
        public string bookingid { get; set; }
        public string subjectid { get; set; }
        public string subjectnickname { get; set; }
        public string subjectname { get; set; }
        public string subjecttype { get; set; }
        public string subjectdesc { get; set; }
        public string subjectmin { get; set; }
        public string sumstudytime { get; set; }
        public string remaintime { get; set; }
        public string courseid { get; set; }
        public string status { get; set; }
    }
}