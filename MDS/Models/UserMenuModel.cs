using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class UserMenuModel
    {
        public int UMenuID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string MStatus { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Mlevel { get; set; }
        public string Mgroup { get; set; }
        public string Morder { get; set; }
        public string MenuShow { get; set; }
    }
}