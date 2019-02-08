using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class UserGroupModel
    {
        public string UserGId { get; set; }
        public string UserGName { get; set; }
        public string UserGNameTh { get; set; }
        public string UserGNameEn { get; set; }
        public string Status { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string result { get; set; }
        public string msg { get; set; }
        public List<string> arrMenuId { get; set; }
    }
}