using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class StudentModel
    {
        public string bookingno { get; set; }
        public string voucherno { get; set; }
        public string studentind { get; set; }
        public string voucherid { get; set; }
        public string branchid { get; set; }
        public string studenttype { get; set; }
        public string idcard { get; set; }
        public string titleT { get; set; }
        public string nameT { get; set; }
        public string surnameT { get; set; }
        public string titleE { get; set; }
        public string nameE { get; set; }
        public string surnameE { get; set; }
        public string birthdate { get; set; }
        public string age { get; set; }
        public string email { get; set; }
        public string nation { get; set; }
        public string gentle { get; set; }
        public string mobileno { get; set; }
        public string address { get; set; }
        public string tumbol { get; set; }
        public string amphur { get; set; }
        public string changwat { get; set; }
        public string zipcode { get; set; }
        public string cardimg { get; set; }
        public string Disability { get; set; }
        public string disease { get; set; }
        public string documentNo { get; set; }
        public string passportNo { get; set; }
        public string passportCountry { get; set; }
        public string passportimg { get; set; }
        public string passportimgftype { get; set; }
        public string insertdate { get; set; }
        public string lastupdate { get; set; }
        public string insertby { get; set; }
        public string updateby { get; set; }
        public string status { get; set; }
        public string courseid { get; set; }
        public string coursetime { get; set; }
        public string coursename { get; set; }
        public string coursegroup { get; set; }
        public string courseprice { get; set; }
        public string examdate { get; set; }
        public string IP { get; set; }
        public string User { get; set; }
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string qrurl { get; set; }
        public string remark { get; set; }

        public string Result { get; set; }
        public string Message { get; set; }
        public string tabActive { get; set; }

        public IList<PaymentModel> ListPayment { get; set; }
        public PaymentModel ListOfReceipt { get; set; }
        public List<TableStudentModel> ListOfTableStudent { get; set; }
        public List<PretestModel> ListOfPretest { get; set; }

    }
}