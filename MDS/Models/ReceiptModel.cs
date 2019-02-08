using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class ReceiptModel
    {
        public string receiptno { get; set; }
        public string payid { get; set; }
        public string receiptdate { get; set; }
        public string insertdate { get; set; }
        public string insertby { get; set; }
        public string receivefrom { get; set; }
        public string receivefromAddr { get; set; }
        public string receiveTelno { get; set; }
        public string branchid { get; set; }
        public string amount { get; set; }
        public string ip { get; set; }
        public string status { get; set; }
        public string canceldate { get; set; }
        public string cancelremark { get; set; }
        public string cancelby { get; set; }
        public string studentind { get; set; }
        public string voucherid { get; set; }

        public string result { get; set; }
        public string msg { get; set; }
    }
}