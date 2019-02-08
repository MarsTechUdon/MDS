using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class ComCreditcardModel
    {
        public string Cid { get; set; }
        public string CrType { get; set; }
        public string CrCharge { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; }
        public string result { get; set; }
        public string Username { get; internal set; }
        public string msg { get; set; }

    }
}