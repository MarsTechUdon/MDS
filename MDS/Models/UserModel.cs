using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class UserModel
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmpCode { get; set; }
        public string UserDetailID { get; set; }
        public string DepartId { get; set; }
        public string PrefixId { get; set; }
        public string PrefixNameTH { get; set; }
        public string UserImage { get; set; }
        public HttpPostedFileBase UserImageFile { get; set; }
        public string FirstNameTH { get; set; }
        public string LastNameTH { get; set; }
        public string PrefixNameEN { get; set; }
        public string FirstNameEN { get; set; }
        public string LastNameEN { get; set; }
        public string IdCard { get; set; }
        public string Email { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string IP { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Status { get; set; }
        public string UserGNameTh { get; set; }
        public string DepartName { get; set; }
        public string UserGId { get; set; }
        public string RoleName { get; set; }
        public UserLoginModel UserLoginData { get; set; }
    }
}