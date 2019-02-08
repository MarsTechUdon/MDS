using MDS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.DBExec
{
    public class ManageExamManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region "List exam"
        public List<ManageExamModel> GetListExam(string ugmData, string ugmData2)
        {
            List<ManageExamModel> model = new List<ManageExamModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                string langue = ""; string group = ""; 
                if (string.IsNullOrEmpty(ugmData2))
                {
                     langue = "TH";
                }
                else
                {
                    langue = ugmData2;
                }
                if (string.IsNullOrEmpty(ugmData))
                {
                    group = "0";
                }
                else
                {
                    group = ugmData;
                }
                SqlCommand cm = new SqlCommand("[sp_Pretest]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "ListExam");
                cm.Parameters.AddWithValue("@langid", langue);
                cm.Parameters.AddWithValue("@subjectgroupid", group);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ManageExamModel x = new ManageExamModel();
                    x.ind = row["ind"].ToString();
                    x.Langid = row["Langid"].ToString();
                    x.Question = row["Question"].ToString();
                    x.QuestionImage = row["QuestionImage"].ToString();
                    x.Questionsound = row["Questionsound"].ToString();
                    x.dltNo = row["dltNo"].ToString();

                    x.qind = row["qind"].ToString();
                    x.Choice = row["Choice"].ToString();
                    x.ChoiceImage = row["ChoiceImage"].ToString();
                    x.ChoiceSound = row["ChoiceSound"].ToString();
                    x.iscorrect = row["iscorrect"].ToString();

                    model.Add(x);
                }
            }
            return model;
        }
        #endregion

        #region "List exam by id"

        public ManageExamModel GetManageExamByID(ManageExamModel model)
        {
         //   ManageExamModel model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_Pretest]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "ListExamById");
                cm.Parameters.AddWithValue("@qind", model.ind);

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                for (int i = 1; i<5;i++)
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        model.ind = dr["ind"].ToString();
                        model.Question = dr["Question"].ToString();
                        model.dltNo = dr["dltNo"].ToString();
                        if (i==1)
                        {

                            model.qind = dr["qind"].ToString();
                            model.Question = dr["Question"].ToString();
                            model.QuestionImage = dr["QuestionImage"].ToString();
                            model.Questionsound = dr["Questionsound"].ToString();

                            model.Langid = dr["Langid"].ToString();
                            model.subjectGroupid = dr["subjectGroupid"].ToString();

                            model.choice1 = dr["Choice"].ToString();
                            model.choiceimage1 = dr["ChoiceImage"].ToString();
                            model.choicesound1 = dr["ChoiceSound"].ToString();
                            model.iscorrect1 = dr["iscorrect"].ToString();
                        }
                        else if (i==2)
                        {
                            model.choice2 = dr["Choice"].ToString();
                            model.choiceimage2 = dr["ChoiceImage"].ToString();
                            model.choicesound2 = dr["ChoiceSound"].ToString();
                            model.iscorrect2 = dr["iscorrect"].ToString();
                        }
                        else if (i == 3)
                        {
                            model.choice3 = dr["Choice"].ToString();
                            model.choiceimage3 = dr["ChoiceImage"].ToString();
                            model.choicesound3 = dr["ChoiceSound"].ToString();
                            model.iscorrect3 = dr["iscorrect"].ToString();
                        }
                        else if (i == 4)
                        {
                            model.choice4 = dr["Choice"].ToString();
                            model.choiceimage4 = dr["ChoiceImage"].ToString();
                            model.choicesound4 = dr["ChoiceSound"].ToString();
                            model.iscorrect4 = dr["iscorrect"].ToString();
                        }
                    }
                }
                
            }
            return model;
        }
        #endregion

        #region "add exam"
        public ManageExamModel AddManageExam(ManageExamModel ugmData, string Username)
        {
            var model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_Pretest]", db);

                cm.Parameters.AddWithValue("@flag", "Add");
                cm.Parameters.AddWithValue("@langid", ugmData.Langid);
                cm.Parameters.AddWithValue("@question", ugmData.Question);
                cm.Parameters.AddWithValue("@questionimage", ugmData.QuestionImage);
                cm.Parameters.AddWithValue("@questionsound", ugmData.Questionsound);
                cm.Parameters.AddWithValue("@subjectgroupid", ugmData.subjectGroupid);
                cm.Parameters.AddWithValue("@user", Username);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                if (ugmData.iscorrect=="1")
                {
                    cm.Parameters.AddWithValue("@iscorrect1",1);
                }
                else if (ugmData.iscorrect == "2")
                {
                    cm.Parameters.AddWithValue("@iscorrect2", 1);
                }
                else if (ugmData.iscorrect == "3")
                {
                    cm.Parameters.AddWithValue("@iscorrect3", 1);
                }
                else if (ugmData.iscorrect == "4")
                {
                    cm.Parameters.AddWithValue("@iscorrect4", 1);
                }
                cm.Parameters.AddWithValue("@choice1", ugmData.choice1);
                cm.Parameters.AddWithValue("@choiceimage1", ugmData.choiceimage1);
                cm.Parameters.AddWithValue("@choicesound1", ugmData.choicesound1);

                cm.Parameters.AddWithValue("@choice2", ugmData.choice2);
                cm.Parameters.AddWithValue("@choiceimage2", ugmData.choiceimage2);
                cm.Parameters.AddWithValue("@choicesound2", ugmData.choicesound2);

                cm.Parameters.AddWithValue("@choice3", ugmData.choice3);
                cm.Parameters.AddWithValue("@choiceimage3", ugmData.choiceimage3);
                cm.Parameters.AddWithValue("@choicesound3", ugmData.choicesound3);

                cm.Parameters.AddWithValue("@choice4", ugmData.choice4);
                cm.Parameters.AddWithValue("@choiceimage4", ugmData.choiceimage4);
                cm.Parameters.AddWithValue("@choicesound4", ugmData.choicesound4);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.ind = dr["ind"].ToString();
                }
            }
            return model;
        }
        #endregion

        #region "add exam"
        public ManageExamModel EditManageExam(ManageExamModel ugmData, string Username)
        {
            var model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_Pretest]", db);

                cm.Parameters.AddWithValue("@flag", "Edit");
                cm.Parameters.AddWithValue("@qind", ugmData.qind);
                cm.Parameters.AddWithValue("@langid", ugmData.Langid);
                cm.Parameters.AddWithValue("@question", ugmData.Question);
                cm.Parameters.AddWithValue("@questionimage", ugmData.QuestionImage);
                cm.Parameters.AddWithValue("@questionsound", ugmData.Questionsound);
                cm.Parameters.AddWithValue("@subjectgroupid", ugmData.subjectGroupid);
                cm.Parameters.AddWithValue("@user", Username);
                cm.Parameters.AddWithValue("@ip", HttpContext.Current.Request.UserHostAddress);
                if (ugmData.iscorrect == "1")
                {
                    cm.Parameters.AddWithValue("@iscorrect1", 1);
                }
                else if (ugmData.iscorrect == "2")
                {
                    cm.Parameters.AddWithValue("@iscorrect2", 1);
                }
                else if (ugmData.iscorrect == "3")
                {
                    cm.Parameters.AddWithValue("@iscorrect3", 1);
                }
                else if (ugmData.iscorrect == "4")
                {
                    cm.Parameters.AddWithValue("@iscorrect4", 1);
                }
                cm.Parameters.AddWithValue("@choice1", ugmData.choice1);
                cm.Parameters.AddWithValue("@choiceimage1", ugmData.choiceimage1);
                cm.Parameters.AddWithValue("@choicesound1", ugmData.choicesound1);

                cm.Parameters.AddWithValue("@choice2", ugmData.choice2);
                cm.Parameters.AddWithValue("@choiceimage2", ugmData.choiceimage2);
                cm.Parameters.AddWithValue("@choicesound2", ugmData.choicesound2);

                cm.Parameters.AddWithValue("@choice3", ugmData.choice3);
                cm.Parameters.AddWithValue("@choiceimage3", ugmData.choiceimage3);
                cm.Parameters.AddWithValue("@choicesound3", ugmData.choicesound3);

                cm.Parameters.AddWithValue("@choice4", ugmData.choice4);
                cm.Parameters.AddWithValue("@choiceimage4", ugmData.choiceimage4);
                cm.Parameters.AddWithValue("@choicesound4", ugmData.choicesound4);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.ind = dr["ind"].ToString();
                }
            }
            return model;
        }
        #endregion

        #region "Delete exam"
        public ManageExamModel DeleteManageExam(ManageExamModel ugmData)
        {
            var model = new ManageExamModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_Pretest]", db);

                cm.Parameters.AddWithValue("@flag", "Delete");
                cm.Parameters.AddWithValue("@qind", ugmData.qind);

                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["msg"].ToString();
                    model.ind = dr["ind"].ToString();
                }
            }
            return model;
        }
        #endregion
        #region "List หมวดวิชา"
        public static List<SelectListItem> GetSubjectGroup()
        {
            List<SelectListItem> SectionList = new List<SelectListItem>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "lsubjectgroup");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new SelectListItem()
                    {
                        Text = dr["subjectgroupDesc"].ToString(),
                        Value = dr["subjectgroupid"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion

        #region "List ภาษา"
        public static List<SelectListItem> GetLanguage()
        {
            List<SelectListItem> SectionList = new List<SelectListItem>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_SCLookup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "llanguage");

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    SectionList.Add(new SelectListItem()
                    {
                        Text = dr["langid"].ToString(),
                        Value = dr["langdesc"].ToString()
                    });
                }
            }
            return SectionList;
        }
        #endregion
    }
}