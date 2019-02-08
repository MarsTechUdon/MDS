using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class ManageMenuModel
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuLevel { get; set; }
        public string LavelName { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        public string MenuNo { get; set; }
        public string MenuGroup { get; set; }
        public string MenuType { get; set; }
        public string User { get; set; }
        public string LangId { get; set; }
        public string LangName { get; set; }
        public string LangCode { get; set; }
    }
}