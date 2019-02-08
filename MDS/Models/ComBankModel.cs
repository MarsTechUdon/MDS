using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class ComBankModel
    {
        public string bid { get; set; }
        public string bankabbr { get; set; }
        public string bankname { get; set; }
        public string status { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
    }
}