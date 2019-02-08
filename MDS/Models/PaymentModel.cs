using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class PaymentModel
    {
        public string rno { get; set; }
        public string payno { get; set; }
        public string payid { get; set; }
        public string paydate { get; set; }
        public string keydate { get; set; }
        public string detail { get; set; }
        public string amount { get; set; }
        public string amountDbnote { get; set; }
        public string amountDiscount { get; set; }
        public string remark { get; set; }
        public string status { get; set; }

        public string rcid { get; set; }
        public string user { get; set; }
        public string branchid { get; set; }
        public string ip { get; set; }
        public string Rcby { get; set; }
        public string accountno { get; set; }
        public string cid { get; set; }
        public string CrCardNo { get; set; }
        public string bankabbr { get; set; }
        public string chequeno { get; set; }
        public string bankbranch { get; set; }
        public string chequedate { get; set; }
        public string reference { get; set; }
        public string voucherid { get; set; }
        public string studentind { get; set; }
        public string bookingid { get; set; }

        public string receiptno { get; set; }
        public string remain { get; set; }
        public string courseprice { get; set; }

        public string Result { get; set; }
        public string Message { get; set; }
    }
}