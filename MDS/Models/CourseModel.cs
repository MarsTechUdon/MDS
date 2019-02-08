using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class CourseModel
    {
        public string Cid { get; set; }
        public string CName { get; set; }
        public string CPrice { get; set; }
        public string CgID { get; set; }
        public string Status { get; set; }
        public string result { get; set; }
        public string msg { get; set; }
        public List<CourseGroupModel> CourseGroupDataList { get; internal set; }
        public List<CourseModel> CourseDataList { get; internal set; }

        public string CGroupName { get; set; }
    }
}