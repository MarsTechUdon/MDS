using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class ComBankAccountModel
    {
        public string AccountID { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string status { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
    }
}