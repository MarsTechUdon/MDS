using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class ManageUserModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string FullNameTh { get; set; }
        public string FullNameEn { get; set; }
        public string UserGId { get; set; }
        public string UserGnameTh { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Status { get; set; }
    }
}