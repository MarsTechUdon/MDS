using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class TeacherModel
    {
        public string Tid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string ind { get; set; }
        public string idcard { get; set; }
        public string titleT { get; set; }
        public string nameT { get; set; }
        public string surnameT { get; set; }

        public string teachername { get; set; }

        public string titleE { get; set; }
        public string nameE { get; set; }
        public string surnameE { get; set; }
        public string birthdate { get; set; }
        public string age { get; set; }

        public string email { get; set; }
        public string nation { get; set; }
        public string gentle { get; set; }
        public string mobileno { get; set; }
        public string address { get; set; }

        public string tumbol { get; set; }
        public string amphur { get; set; }
        public string changwat { get; set; }
        public string zipcode { get; set; }
        public string cardimg { get; set; }

        public string Insertdate { get; set; }
        public string lastupdate { get; set; }
        public string insertby { get; set; }
        public string updateby { get; set; }
        public string status { get; set; }

        public string nickname { get; set; }

        public string Qrurl { get; set; }
        public string URL { get; set; }

        public string result { get; set; }
        public string msg { get; set; }
        public string User { get; set; }

        public List<TableTeacherModel> ListOfTableTeacher { get;internal set; }

    }
}