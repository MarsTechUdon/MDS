using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class StatementModel
    {

        public string accountID { get; set; }
        public string accountno { get; set; }
        public string accountname { get; set; }

        public string ind { get; set; }
        public string accountno2 { get; set; }
        public string approvedate { get; set; } //วันโอน
        public string insertdate { get; set; }//วันยืนยัน
        public string detail { get; set; }
        public string amount { get; set; }
        public string re_f { get; set; }
        public string transdate { get; set; }

        public string transdate_day { get; set; }
        public string transdate_time { get; set; }

        public string fdate { get; set; }
        public string tdate { get; set; }

        public string result { get; set; }
        public string msg { get; set; }

        public string effectdate { get; set; }
        public string description { get; set; }
        public string debit { get; set; }
        public string credit { get; set; }
        public string balance { get; set; }
        public string channel { get; set; }
        public string status { get; set; }

        public string chk_acno { get; set; }

        internal static void Add(StatementModel statementModel)
        {
            throw new NotImplementedException();
        }
    }
}