using MDS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MDS.DBExec
{
    public class PaymentManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region ข้อมูล ListPayment
        public StudentModel ListPayment(string studentind)
        {
            StudentModel model = new StudentModel();
            model.ListOfReceipt = new PaymentModel();
            model.ListPayment = new List<PaymentModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[SP_Payment]", db);
                cm.Parameters.AddWithValue("@flag", "listPayment");
                cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@studentind", studentind);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var modelPayment = new PaymentModel()
                        {
                            rno = dr["rno"].ToString(),
                            payno = dr["payno"].ToString(),
                            payid = dr["payid"].ToString(),
                            paydate = dr["paydate"].ToString(),
                            keydate = dr["keydate"].ToString(),
                            detail = dr["detail"].ToString(),
                            amount = dr["amount"].ToString(),
                            remark = dr["remark"].ToString(),
                            status = dr["status"].ToString()
                        };
                        model.ListPayment.Add(modelPayment);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var modelReceipt = new PaymentModel()
                        {
                            receiptno = dr["receiptno"].ToString(),
                            remain = dr["remain"].ToString(),
                            courseprice = dr["courseprice"].ToString()
                        };
                        model.ListOfReceipt = modelReceipt;
                    }
                    dr.Close();
                }
                return model;
            }

        }
        #endregion

        #region ข้อมูล PaymentById
        public PaymentModel GetPaymentById(string paymentId)
        {
            PaymentModel model = new PaymentModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[SP_Payment]", db);
                cm.Parameters.AddWithValue("@flag", "getPaymentById");
                cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@payid", paymentId);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        model.payno = dr["PayNo"].ToString();
                        model.payid = dr["Payid"].ToString();
                        //model.paydate = dr["paydate"].ToString();
                        model.rcid = dr["rcid"].ToString();
                        model.Rcby = dr["Rcby"].ToString();
                        model.accountno = dr["accountno"].ToString();
                        model.cid = dr["cid"].ToString();
                        model.CrCardNo = dr["CrCardNo"].ToString();
                        model.bankabbr = dr["bankabbr"].ToString();
                        model.chequeno = dr["chequeno"].ToString();
                        model.bankbranch = dr["bankbranch"].ToString();
                        
                        model.reference = dr["ref"].ToString();
                        model.amount = Convert.ToInt32(Convert.ToDecimal(dr["amount"].ToString())).ToString();
                        model.remark = dr["remark"].ToString();
                        model.voucherid = dr["voucherid"].ToString();
                        model.studentind = dr["studentind"].ToString();
                        model.bookingid = dr["bookingid"].ToString();
                        model.status = dr["status"].ToString();

                        model.paydate = dr["paydate"].ToString();
                        model.chequedate = dr["chequedate"].ToString();

                        //string paydateConvert = string.Empty;
                        //string chequedateConvert = string.Empty;
                        //try
                        //{
                        //    //En to TH
                        //    DateTime strpaydate = DateTime.ParseExact(dr["paydate"].ToString(), "yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));
                        //    paydateConvert = strpaydate.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("th-TH"));
                        //    //En to TH
                        //    DateTime strchequedate = DateTime.ParseExact(dr["chequedate"].ToString(), "yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));
                        //    chequedateConvert = strpaydate.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("th-TH"));
                        //}
                        //catch (Exception ex) { }
                        //model.paydate = paydateConvert;
                        //model.chequedate = chequedateConvert;
                        if (Convert.ToInt32(model.amount) < 0)
                        {
                            model.amount = (Convert.ToInt32(model.amount) * (-1)).ToString();
                        }
                    }
                }
                return model;
            }

        }
        #endregion

        #region ข้อมูล Verify Coupon
        public CouponModel Verify(string couponNo)
        {
            CouponModel model = new CouponModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCGenCoupon]", db);
                cm.Parameters.AddWithValue("@flag", "Verify");
                cm.Parameters.AddWithValue("@couponno", couponNo);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.success = dr["success"].ToString();
                        model.msg = dr["msg"].ToString();
                        model.couponno = dr["couponno"].ToString();
                        model.amount = dr["amount"].ToString();
                    }
                }
                return model;
            }

        }
        #endregion

        #region ข้อมูล Save Coupon
        public CouponModel UseCoupon(string couponNo,string studentind, string payid, string voucherid)
        {
            CouponModel model = new CouponModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCGenCoupon]", db);
                cm.Parameters.AddWithValue("@flag", "setUsed");
                cm.Parameters.AddWithValue("@couponno", couponNo);
                cm.Parameters.AddWithValue("@studentind", studentind);
                cm.Parameters.AddWithValue("@payid", payid);
                cm.Parameters.AddWithValue("@voucherid", voucherid);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["msg"].ToString();
                    dr.Close();
                }
                return model;
            }
        }
        #endregion

        #region บันทึกข้อมูล Payment
        public PaymentModel InsertPayment(PaymentModel modelData)
        {
            PaymentModel model = new PaymentModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[SP_Payment]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Add");
                cm.Parameters.AddWithValue("@rcid", modelData.rcid);
                cm.Parameters.AddWithValue("@user", modelData.user);
                cm.Parameters.AddWithValue("@branchid", modelData.branchid);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@Rcby", modelData.Rcby);
                cm.Parameters.AddWithValue("@accountno", modelData.accountno);
                cm.Parameters.AddWithValue("@cid", modelData.cid);
                cm.Parameters.AddWithValue("@CrCardNo", modelData.CrCardNo);
                cm.Parameters.AddWithValue("@bankabbr", modelData.bankabbr);
                cm.Parameters.AddWithValue("@chequeno", modelData.chequeno);
                cm.Parameters.AddWithValue("@bankbranch", modelData.bankbranch);

                cm.Parameters.AddWithValue("@amount", modelData.amount);
                cm.Parameters.AddWithValue("@ref", modelData.reference);
                cm.Parameters.AddWithValue("@remark", modelData.remark);
                cm.Parameters.AddWithValue("@voucherid", modelData.voucherid);
                cm.Parameters.AddWithValue("@studentind", modelData.studentind);
                cm.Parameters.AddWithValue("@bookingid", modelData.bookingid);

                cm.Parameters.AddWithValue("@chequedate", modelData.chequedate);
                cm.Parameters.AddWithValue("@paydate", modelData.paydate);

                //string date = string.Empty;
                //string paydate = string.Empty;
                //try
                //{
                //    //TH to EN
                //    DateTime strpaydate = DateTime.ParseExact(modelData.paydate, "yyyy-mm-dd", new System.Globalization.CultureInfo("th-TH"));
                //    paydate = strpaydate.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));

                //    //TH to EN
                //    DateTime strSendDate = DateTime.ParseExact(modelData.chequedate, "yyyy-mm-dd", new System.Globalization.CultureInfo("th-TH"));
                //    date = strSendDate.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));
                    

                //}
                //catch (Exception ex) { }
                //cm.Parameters.AddWithValue("@chequedate", date);
                //cm.Parameters.AddWithValue("@paydate", paydate);
                

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.payid = dr["payid"].ToString();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["msg"].ToString();
                    dr.Close();
                }
            }
            return model;
        }
        #endregion

        #region แก้ไขข้อมูล EditPayment
        public PaymentModel EditPayment(PaymentModel modelData)
        {
            PaymentModel model = new PaymentModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[SP_Payment]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@payid", modelData.payid);
                cm.Parameters.AddWithValue("@rcid", modelData.rcid);
                cm.Parameters.AddWithValue("@user", modelData.user);
                cm.Parameters.AddWithValue("@branchid", modelData.branchid);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@Rcby", modelData.Rcby);
                cm.Parameters.AddWithValue("@accountno", modelData.accountno);
                cm.Parameters.AddWithValue("@cid", modelData.cid);
                cm.Parameters.AddWithValue("@CrCardNo", modelData.CrCardNo);
                cm.Parameters.AddWithValue("@bankabbr", modelData.bankabbr);
                cm.Parameters.AddWithValue("@chequeno", modelData.chequeno);
                cm.Parameters.AddWithValue("@bankbranch", modelData.bankbranch);

                cm.Parameters.AddWithValue("@amount", modelData.amount);
                cm.Parameters.AddWithValue("@ref", modelData.reference);
                cm.Parameters.AddWithValue("@remark", modelData.remark);
                cm.Parameters.AddWithValue("@voucherid", modelData.voucherid);
                cm.Parameters.AddWithValue("@studentind", modelData.studentind);
                cm.Parameters.AddWithValue("@bookingid", modelData.bookingid);

                cm.Parameters.AddWithValue("@chequedate", modelData.chequedate);
                cm.Parameters.AddWithValue("@paydate", modelData.paydate);

                //string date = string.Empty;
                //string paydate = string.Empty;
                //try
                //{
                //    //TH to EN
                //    DateTime strpaydate = DateTime.ParseExact(modelData.paydate, "yyyy-mm-dd", new System.Globalization.CultureInfo("th-TH"));
                //    paydate = strpaydate.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));

                //    //TH to EN
                //    DateTime strSendDate = DateTime.ParseExact(modelData.chequedate, "yyyy-mm-dd", new System.Globalization.CultureInfo("th-TH"));
                //    date = strSendDate.ToString("yyyy-mm-dd", new System.Globalization.CultureInfo("en-GB"));


                //}
                //catch (Exception ex) { }
                //cm.Parameters.AddWithValue("@chequedate", date);
                //cm.Parameters.AddWithValue("@paydate", paydate);


                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["msg"].ToString();
                    dr.Close();
                }
            }
            return model;
        }
        #endregion

        #region GenReceipt
        public string GenReceipt(StudentModel model)
        {
            string receiptno = string.Empty;
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_genReceipt]", db);
                cm.Parameters.AddWithValue("@flag", "New");
                cm.Parameters.AddWithValue("@IP", HttpContext.Current.Request.UserHostAddress);
                cm.Parameters.AddWithValue("@studentind", model.studentind);
                cm.Parameters.AddWithValue("@voucherid", model.voucherid);
                cm.Parameters.AddWithValue("@user", model.User);
                cm.Parameters.AddWithValue("@branchid", model.branchid);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    receiptno = dr["receiptno"].ToString();
                    dr.Close();
                  
                }
                return receiptno;
            }

        }
        #endregion

    }
}