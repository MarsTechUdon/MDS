using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class SubjectModel
    {
        public string Sid { get; set; }
        public string SName { get; set; }
        public string SnickName { get; set; }
        public string Cid { get; set; }
        public string Stype { get; set; }
        public string smin { get; set; }
        public string status { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
        public List<CourseModel> CourseDataList { get; internal set; }
        public List<SubjectModel> SubjectDataList { get; internal set; }

        public string CourseName { get; set; }
    }
}