using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class LeaveDateModel
    {
        public string ind { get; set; }
        public string teacherind { get; set; }
        public string teachername { get; set; }
        public string leavefdate { get; set; }
        
        public string leaveftime { get; set; }
        public string leavettime { get; set; }
        public string remark { get; set; }
        public string fdate_day { get; set; }
        public string tdate_day { get; set; }
        public string fdate { get; set; }
        public string tdate { get; set; }
        public string nickname { get; set; }
        public string day { get; set; }
        public string typedate { get; set; }
        public string listDeteteLeave { get; set; }
        public string[] delind { get; set; }
        public string para { get; set; }
        public string ip { get; set; }
        public string insertby { get; set; }
        public string insertdate { get; set; }
        public string updateby { get; set; }
        public string updatedate { get; set; }
    }
}