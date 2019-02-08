using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class MenuLangModel
    {
        public List<ManageMenuModel> MenuList { get; set; }
        public List<string> LangCode { get; set; }
        public string MenuId { get; set; }
    }
}