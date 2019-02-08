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
    public class ExamManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        public ManageExamModel SelectExam(string studentind, string examid)
        {
            var model = new ManageExamModel();
            model.QuestionList = new List<ManageExamModel>();
            model.ChoiceList = new List<ManageExamModel>();
            model.ChoiceList = new List<ManageExamModel>();
            model.Detail = new List<ManageExamModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {

                SqlCommand cm = new SqlCommand("[sp_QrcodePublic]", db);
                cm.Parameters.AddWithValue("@flag", "getExam");
                cm.Parameters.AddWithValue("@studentind", studentind);
                cm.Parameters.AddWithValue("@examid", examid);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                }
                dr.NextResult();
                while (dr.Read())
                {
                    var questionModel = new ManageExamModel()
                    {
                        qind = dr["qind"].ToString(),
                        dltNo = dr["dltNo"].ToString(),
                        examid = dr["examid"].ToString(),
                        examQNO = dr["examQNO"].ToString(),
                        Question = dr["question"].ToString(),
                        QuestionImage = dr["questionimage"].ToString(),
                        Questionsound = dr["questionsound"].ToString(),

                    };
                    model.QuestionList.Add(questionModel);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    var choiceModel = new ManageExamModel()
                    {
                        examid = dr["examid"].ToString(),
                        examQNO = dr["examQNO"].ToString(),
                        examAnswerNo = dr["examAnswerNo"].ToString(),
                        qind = dr["qind"].ToString(),
                        answer = dr["Answer"].ToString(),
                        iscorrect = dr["isCorrect"].ToString(),
                        Choice = dr["choice"].ToString(),
                        ChoiceImage = dr["choiceImage"].ToString(),
                        ChoiceSound = dr["choiceSound"].ToString(),
                        
                    };
                    model.ChoiceList.Add(choiceModel);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    var detail = new ManageExamModel()
                    {
                        studentname = dr["studentname"].ToString(),
                        coursenickname = dr["coursenickname"].ToString(),
                    };
                    model.Detail.Add(detail);
                }

            }
            return model;
        }

        #region "Question image"
        public ManageExamModel GetQImage(string examid, string examQNO)
        {
            var model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {

                SqlCommand cm = new SqlCommand("[sp_SCExam]", db);
                cm.Parameters.AddWithValue("@flag", "getQImage");
                cm.Parameters.AddWithValue("@examid", examid);
                cm.Parameters.AddWithValue("@examqno", examQNO);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.QuestionImage = dr["questionImage"].ToString();
                  //  model.Questionsound = dr["questionsound"].ToString();
                }
            }
            return model;
        }
        #endregion

        #region "Question sound"
        public ManageExamModel GetQSound(string examid, string examQNO)
        {
            var model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCExam]", db);
                cm.Parameters.AddWithValue("@flag", "getQSound");
                cm.Parameters.AddWithValue("@examid", examid);
                cm.Parameters.AddWithValue("@examqno", examQNO);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Questionsound = dr["questionsound"].ToString();
                }
            }
            return model;
        }
        #endregion

        #region "Answer sound"
        public ManageExamModel GetASound(string examid, string examQNO , string examAnswerNo)
        {
            var model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCExam]", db);
                cm.Parameters.AddWithValue("@flag", "getASound");
                cm.Parameters.AddWithValue("@examid", examid);
                cm.Parameters.AddWithValue("@examqno", examQNO);
                cm.Parameters.AddWithValue("@examanswerno", examAnswerNo);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.ChoiceSound = dr["ChoiceSound"].ToString();
                }
            }
            return model;
        }
        #endregion

        #region "Answer img_1"
        public ManageExamModel GetAImage1(string examid, string examQNO)
        {
            var model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCExam]", db);

                cm.Parameters.AddWithValue("@flag", "getAImage");
                cm.Parameters.AddWithValue("@examid", examid);
                cm.Parameters.AddWithValue("@examqno", examQNO); 
                cm.Parameters.AddWithValue("@examanswerno", 1);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.choiceimage1 = dr["ChoiceImage"].ToString();
                }
                
            }
            return model;
        }
        #endregion
        #region "Answer img_2"
        public ManageExamModel GetAImage2(string examid, string examQNO)
        {
            var model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCExam]", db);

                cm.Parameters.AddWithValue("@flag", "getAImage");
                cm.Parameters.AddWithValue("@examid", examid);
                cm.Parameters.AddWithValue("@examqno", examQNO);
                cm.Parameters.AddWithValue("@examanswerno", 2);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.choiceimage2 = dr["ChoiceImage"].ToString();
                }

            }
            return model;
        }
        #endregion
        #region "Answer img_3"
        public ManageExamModel GetAImage3(string examid, string examQNO)
        {
            var model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCExam]", db);

                cm.Parameters.AddWithValue("@flag", "getAImage");
                cm.Parameters.AddWithValue("@examid", examid);
                cm.Parameters.AddWithValue("@examqno", examQNO);
                cm.Parameters.AddWithValue("@examanswerno", 3);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.choiceimage3 = dr["ChoiceImage"].ToString();
                }

            }
            return model;
        }
        #endregion
        #region "Answer img_4"
        public ManageExamModel GetAImage4(string examid, string examQNO)
        {
            var model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCExam]", db);

                cm.Parameters.AddWithValue("@flag", "getAImage");
                cm.Parameters.AddWithValue("@examid", examid);
                cm.Parameters.AddWithValue("@examqno", examQNO);
                cm.Parameters.AddWithValue("@examanswerno", 4);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.choiceimage4 = dr["ChoiceImage"].ToString();
                }

            }
            return model;
        }
        #endregion
        #region "add answer exam"
        public ManageExamModel AddExam(string examid, string examQNO, string examAnswerNo)
        {
            var num2 = 0;var num3 = 0;
            var model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {   
                SqlCommand cm = new SqlCommand("[sp_SCExam]", db);
                var num1 = Convert.ToInt32(examid);
                if (string.IsNullOrEmpty(examQNO))
                {
                     num2 = 0;
                }
                else{
                     num2 = Convert.ToInt32(examQNO);
                }
                if (string.IsNullOrEmpty(examAnswerNo))
                {
                     num3 = 0;
                }
                else
                {
                     num3 = Convert.ToInt32(examAnswerNo);
                }
                if (num2!=0 && num3!=0)
                {
                //  var num2 = Convert.ToInt32(examqno);
               // var num3 = Convert.ToInt32(examanswerno);
                cm.Parameters.AddWithValue("@flag", "ansExam");
                cm.Parameters.AddWithValue("@examid", num1);
                cm.Parameters.AddWithValue("@examqno", num2); 
                cm.Parameters.AddWithValue("@examanswerno", num3); 
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    //model.result = dr["result"].ToString();
                    //model.msg = dr["msg"].ToString();
                    //model.ind = dr["ind"].ToString();
                }
                }

            }
            return model;
        }
        #endregion

        #region "AddFinishExam"
        public ManageExamModel AddFinishExam(string score , string examid)
        {
            var num1 = 0; var num2 = 0;
            var model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCExam]", db);
                    num1 = Convert.ToInt32(examid);
                    num2 = Convert.ToInt32(score);

                cm.Parameters.AddWithValue("@flag", "finishExam");
                cm.Parameters.AddWithValue("@examid", num1);
                cm.Parameters.AddWithValue("@examscore", num2);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    //model.ind = dr["ind"].ToString();
                }
            }
            return model;
        }
        #endregion
    }
}