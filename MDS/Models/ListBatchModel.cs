using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class ListBatchModel
    {
        public string Batchno { get; set; }
        public string BatchDate { get; set; }
        public string depositdate { get; set; }
        public string branchid { get; set; }
        public string item { get; set; }
        public string accountno { get; set; }
        public string amount { get; set; }
        public string remark { get; set; }
        public string insertby { get; set; }
        public string status { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
        public List<ComBankAccountModel> BankAccount { get; internal set; }
        public List<ListBatchModel> BatchList { get; internal set; }
        public List<CurrentBranchModel> BranchList { get; internal set; }

        //ModelDeposit
        public string rno { get; set; }
        public string branch { get; set; }
        public string uname { get; set; }
        public string detail { get; set; }
        public string paydate { get; set; }
        public string keydate { get; set; }
        public string amount2 { get; set; }
        public string payid { get; set; }
        public string status2 { get; set; }
        public string rcby { get; set; }
        public string rcbydesc { get; set; }
        public string summary { get; set; }
        public string DepositUser { get; set; } // เอา user session ไปใส่
    }
}