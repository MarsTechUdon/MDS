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
    public class ENDEtxtManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;
        public static string Encrypt(string plainText)
        {
            string result = "";
            if (plainText == null)
            {
                return null;
            }
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_comENDEtxt]", db);
                cm.Parameters.AddWithValue("@flag", "EN");
                cm.Parameters.AddWithValue("@plaintxt", plainText);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result = dr["branchtxt"].ToString();

                    }
                }
                dr.Close();
            }
            return result;
        }
        public static string Decrypt(string encryptedText)
        {
            string result = "";
            if (encryptedText == null)
            {
                return null;
            }
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_comENDEtxt]", db);
                cm.Parameters.AddWithValue("@flag", "DE");
                cm.Parameters.AddWithValue("@encodetxt", encryptedText);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result = dr["branchtxt"].ToString();

                    }
                }
                dr.Close();
            }
            return result;
        }
    }
}