using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class TableStudentModel
    {
        public string studentname { get; set; }
        public string mobileno { get; set; }
        public string coursegroupname { get; set; }
        public string coursenickname { get; set; }
        public string coursename { get; set; }
        public string studydate { get; set; }
        public string studystime { get; set; }
        public string studyetime { get; set; }
        public string subjecttype { get; set; }
        public string subjecttypedesc { get; set; }
        public string subjectname { get; set; }
        public string subjectnickname { get; set; }
        public string nickname { get; set; }
        public string teachername { get; set; }
        public string style { get; set; }
        public string cardimg { get; set; }
    }
}