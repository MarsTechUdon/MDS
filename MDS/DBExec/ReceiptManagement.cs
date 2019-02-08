using System;
using MDS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace MDS.DBExec
{
    public class ReceiptManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;
        //get ข้อมูล
        public List<ReceiptModel> GetAllReceipt()
        {
            List<ReceiptModel> model = new List<ReceiptModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_genReceipt]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "listReceipt");

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ReceiptModel x = new ReceiptModel();
                    x.receiptno = row["receiptno"].ToString();
                    x.insertby = row["insertby"].ToString();
                    x.receiptdate = row["receiptdate"].ToString();
                    x.receivefrom = row["receivefrom"].ToString();
                    x.amount = row["amount"].ToString();
                    x.canceldate = row["canceldate"].ToString();
                    x.cancelby = row["cancelby"].ToString();
                    x.cancelremark = row["cancelremark"].ToString();
                    x.status = row["status"].ToString();

                    model.Add(x);
                }
            }
            return model;
        }
        public ReceiptModel CancelReceipt2(ReceiptModel ugmData,string username)
        {
            var model = new ReceiptModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_genReceipt]", db);

                cm.Parameters.AddWithValue("@flag", "Cancel");
                cm.Parameters.AddWithValue("@receiptno", ugmData.receiptno);
                cm.Parameters.AddWithValue("@cancelremark", ugmData.cancelremark);
                cm.Parameters.AddWithValue("@user", username);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    //model.result = dr["result"].ToString();
                    //model.msg = dr["msg"].ToString();

                }
            }
            return model;
        }

    }
}