using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class RoleSelectionModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int? UserRoleID { get; set; }
    }
}