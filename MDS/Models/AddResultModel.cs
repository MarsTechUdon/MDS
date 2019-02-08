using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class AddResultModel
    {
        public string result { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string pwd { get; set; }

        public UserModel UserDataForForm { get; set; }
        public UserLoginModel UserLoginForForm { get; set; }
    }
}