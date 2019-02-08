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
    public class UserManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region "ดึงข้อมูล SelectAllUser"
        public List<ManageUserModel> SelectAllUser()
        {
            var listUser = new List<ManageUserModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_GetUser", db);
                cm.Parameters.AddWithValue("@flag", "GetAll");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var manageUserModel = new ManageUserModel()
                    {
                        UserId = dr["UserId"].ToString(),
                        Username = dr["Username"].ToString(),
                        FullNameTh = dr["FullNameTh"].ToString(),
                        FullNameEn = dr["FullNameEn"].ToString(),
                        UserGId = dr["UserGId"].ToString(),
                        UserGnameTh = dr["UserGnameTh"].ToString(),
                        Email = dr["Email"].ToString(),
                        Tel = dr["Tel"].ToString(),
                        Status = dr["Status"].ToString()
                    };
                    listUser.Add(manageUserModel);

                }

            }
            return listUser;
        }
        #endregion

        #region "เปลี่ยนรหัสผ่าน"
        public ResultModel ChangePassword(ChangePasswordModel changePassData)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_UM_AddUserDetail]", db);
                cm.Parameters.AddWithValue("@flag", "ChangePassword");
                cm.Parameters.AddWithValue("@UserId", changePassData.UserID);
                cm.Parameters.AddWithValue("@PasswordOld", changePassData.OldPassword);
                cm.Parameters.AddWithValue("@PasswordNew", changePassData.NewPassword);
                cm.Parameters.AddWithValue("@User", changePassData.UserID);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["AddMessage"].ToString();
                }
            }
            return model;
        }
        #endregion

        #region "ดึงข้อมูล user by id"
        public UserDataModel SelectUserDataById(string userId)
        {
            var model = new UserDataModel();
            model.UserData = new UserModel();
            model.PrefixDataList = new List<PrefixModel>();
            model.DepartmentDataList = new List<DepartmentModel>();
            model.UserGroupDataList = new List<UserGroupModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_GetUser", db);
                cm.Parameters.AddWithValue("@flag", "GetById");
                cm.Parameters.AddWithValue("@UserId", userId);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.UserData.UserID = dr["UserId"].ToString();
                    model.UserData.Username = dr["UserName"].ToString();
                    model.UserData.EmpCode = dr["EmpCode"].ToString();
                    model.UserData.PrefixId = dr["Prefix"].ToString();
                    model.UserData.PrefixNameTH = dr["PrefixNameTH"].ToString();
                    model.UserData.FirstNameTH = dr["FirstNameTh"].ToString();
                    model.UserData.LastNameTH = dr["LastNameTh"].ToString();
                    model.UserData.FirstNameEN = dr["FirstNameEn"].ToString();
                    model.UserData.LastNameEN = dr["LastnameEn"].ToString();
                    model.UserData.DepartId = dr["DepartId"].ToString();
                    model.UserData.DepartName = dr["DepartNameTh"].ToString();
                    model.UserData.UserGId = dr["UserGId"].ToString();
                    model.UserData.UserGNameTh = dr["UserGNameTh"].ToString();
                    model.UserData.Email = dr["Email"].ToString();
                    model.UserData.Tel = dr["Tel"].ToString();
                    model.UserData.Mobile = dr["Mobile"].ToString();
                    model.UserData.Fax = dr["Fax"].ToString();
                    model.UserData.Status = dr["Status"].ToString();
                    model.UserData.UserImage = dr["UserImage"].ToString();
                }
                dr.NextResult();
                while (dr.Read())
                {
                    var prefixModel = new PrefixModel()
                    {
                        PrefixCode = dr["PrefixCode"].ToString(),
                        PrefixName = dr["PrefixShortTh"].ToString()
                    };
                    model.PrefixDataList.Add(prefixModel);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    var departModel = new DepartmentModel()
                    {
                        DepartId = dr["DepartId"].ToString(),
                        DepartNameTh = dr["DepartNameTh"].ToString()
                    };
                    model.DepartmentDataList.Add(departModel);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    var userGModel = new UserGroupModel()
                    {
                        UserGId = dr["UserGId"].ToString(),
                        UserGName = dr["UserGNameTh"].ToString()
                    };
                    model.UserGroupDataList.Add(userGModel);
                }
            }
            return model;
        }
        #endregion

        #region "ดึงข้อมูล Add User"
        public UserDataModel SelectAddUser()
        {
            var model = new UserDataModel();
            model.PrefixDataList = new List<PrefixModel>();
            model.DepartmentDataList = new List<DepartmentModel>();
            model.UserGroupDataList = new List<UserGroupModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_GetUser", db);
                cm.Parameters.AddWithValue("@flag", "GetAdd");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var prefixModel = new PrefixModel()
                    {
                        PrefixCode = dr["PrefixCode"].ToString(),
                        PrefixName = dr["PrefixShortTh"].ToString()
                    };
                    model.PrefixDataList.Add(prefixModel);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    var departModel = new DepartmentModel()
                    {
                        DepartId = dr["DepartId"].ToString(),
                        DepartNameTh = dr["DepartNameTh"].ToString()
                    };
                    model.DepartmentDataList.Add(departModel);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    var userGModel = new UserGroupModel()
                    {
                        UserGId = dr["UserGId"].ToString(),
                        UserGName = dr["UserGNameTh"].ToString()
                    };
                    model.UserGroupDataList.Add(userGModel);
                }
            }
            return model;
        }
        #endregion

        #region "บันทึกข้อมูล edit profile"
        public ResultModel EditProfile(UserModel addData)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_UM_AddUserDetail]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "SetEditProfile");
                cm.Parameters.AddWithValue("@Prefix", addData.PrefixId);
                cm.Parameters.AddWithValue("@FirstnameTh", addData.FirstNameTH);
                cm.Parameters.AddWithValue("@LastnameTh", addData.LastNameTH);
                cm.Parameters.AddWithValue("@FirstnameEn", addData.FirstNameEN);
                cm.Parameters.AddWithValue("@LastnameEn", addData.LastNameEN);
                cm.Parameters.AddWithValue("@Email", addData.Email);
                cm.Parameters.AddWithValue("@Tel", addData.Tel);
                cm.Parameters.AddWithValue("@Mobile", addData.Mobile);
                cm.Parameters.AddWithValue("@Fax", addData.Fax);
                cm.Parameters.AddWithValue("@UserImage", addData.UserImage);
                cm.Parameters.AddWithValue("@User", addData.Username);
                cm.Parameters.AddWithValue("@UserId", addData.UserID);
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Message = dr["AddMessage"].ToString();
                    model.Result = dr["result"].ToString();
                    model.ReturnId = dr["UserId"].ToString();
                    dr.Close();
                }
            }
            return model;
        }
        #endregion

        #region "บันทึกข้อมูล edit User"
        public ResultModel EditUser(UserModel addData)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_UM_AddUserDetail]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "SetEditUser");
                cm.Parameters.AddWithValue("@EmpCode", addData.EmpCode);
                cm.Parameters.AddWithValue("@UserGId", addData.UserGId);
                cm.Parameters.AddWithValue("@Username", addData.Username);
                cm.Parameters.AddWithValue("@Password", addData.Password);
                cm.Parameters.AddWithValue("@Prefix", addData.PrefixId);
                cm.Parameters.AddWithValue("@FirstnameTh", addData.FirstNameTH);
                cm.Parameters.AddWithValue("@LastnameTh", addData.LastNameTH);
                cm.Parameters.AddWithValue("@FirstnameEn", addData.FirstNameEN);
                cm.Parameters.AddWithValue("@LastnameEn", addData.LastNameEN);
                cm.Parameters.AddWithValue("@DepartId", addData.DepartId);
                cm.Parameters.AddWithValue("@Email", addData.Email);
                cm.Parameters.AddWithValue("@Tel", addData.Tel);
                cm.Parameters.AddWithValue("@Mobile", addData.Mobile);
                cm.Parameters.AddWithValue("@Fax", addData.Fax);
                cm.Parameters.AddWithValue("@UserImage", addData.UserImage);
                cm.Parameters.AddWithValue("@User", addData.CreateBy);
                cm.Parameters.AddWithValue("@UserId", addData.UserID);
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Message = dr["AddMessage"].ToString();
                    model.Result = dr["result"].ToString();
                    model.ReturnId = dr["UserId"].ToString();
                    dr.Close();
                }
            }
            return model;
        }
        #endregion

        #region "ลบข้อมูล User"
        public ResultModel DeleteUser(string userId)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_UM_AddUserDetail]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "SetDelete");
                cm.Parameters.AddWithValue("@UserId", userId);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Result = dr["result"].ToString();
                    model.Message = dr["AddMessage"].ToString();
                }
            }
            return model;
        }
        #endregion

        #region "บันทึกข้อมูล User"
        public ResultModel AddUser(UserModel addData)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_UM_AddUserDetail]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "SetAdd");
                cm.Parameters.AddWithValue("@EmpCode", addData.EmpCode);
                cm.Parameters.AddWithValue("@UserGId", addData.UserGId);
                cm.Parameters.AddWithValue("@Username", addData.Username);
                cm.Parameters.AddWithValue("@Password", addData.Password);
                cm.Parameters.AddWithValue("@Prefix", addData.PrefixId);
                cm.Parameters.AddWithValue("@FirstnameTh", addData.FirstNameTH);
                cm.Parameters.AddWithValue("@LastnameTh", addData.LastNameTH);
                cm.Parameters.AddWithValue("@FirstnameEn", addData.FirstNameEN);
                cm.Parameters.AddWithValue("@LastnameEn", addData.LastNameEN);
                cm.Parameters.AddWithValue("@DepartId", addData.DepartId);
                cm.Parameters.AddWithValue("@Email", addData.Email);
                cm.Parameters.AddWithValue("@Tel", addData.Tel);
                cm.Parameters.AddWithValue("@Mobile", addData.Mobile);
                cm.Parameters.AddWithValue("@Fax", addData.Fax);
                cm.Parameters.AddWithValue("@UserImage", addData.UserImage);
                cm.Parameters.AddWithValue("@User", addData.CreateBy);
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.Message = dr["AddMessage"].ToString();
                    model.Result = dr["result"].ToString();
                    dr.Close();
                }
            }
            return model;
        }
        #endregion

        #region GetAllUserGroup
        public List<UserGroupModel> GetAllUserGroup()
        {
            List<UserGroupModel> model = new List<UserGroupModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_UM_GetUserGroup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "GetAll");

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    UserGroupModel x = new UserGroupModel();
                    x.UserGId = row["UserGId"].ToString();
                    x.UserGNameTh = row["UserGNameTh"].ToString();
                    x.UserGNameEn = row["UserGNameEn"].ToString();
                    x.Status = row["Status"].ToString();
                    x.CreateDate = row["CreateDate"].ToString();
                    x.CreateBy = row["CreateBy"].ToString();
                    model.Add(x);
                }
            }
            return model;
        }
        #endregion

        public UserGroupModel AddUserGroup(UserGroupModel ugmData, string username)
        {
            var model = new UserGroupModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_UM_SetGrantMenu]", db);
                cm.Parameters.AddWithValue("@flag", "SetAdd");
                cm.Parameters.AddWithValue("@UserGNameTh", ugmData.UserGNameTh);
                cm.Parameters.AddWithValue("@UserGNameEn", ugmData.UserGNameEn);
                cm.Parameters.AddWithValue("@User", username);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["AddMessage"].ToString();
                    model.UserGId = dr["UserId"].ToString();
                }
            }
            return model;
        }

        public UserGroupModel GetUserGroupByID(int UserGId)
        {
            UserGroupModel model = new UserGroupModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_UM_GetUserGroup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "GetById");
                cm.Parameters.AddWithValue("@UserGId", UserGId);

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.UserGId = dr["UserGId"].ToString();
                    model.UserGNameEn = dr["UserGNameEN"].ToString();
                    model.UserGNameTh = dr["UserGNameTh"].ToString();
                }
            }
            return model;
        }
        public UserGroupModel EditUserGroup(UserGroupModel ugmData, string username)
        {
            var model = new UserGroupModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_UM_SetGrantMenu]", db);
                cm.Parameters.AddWithValue("@flag", "SetEdit");
                cm.Parameters.AddWithValue("@UserGNameTh", ugmData.UserGNameTh);
                cm.Parameters.AddWithValue("@UserGNameEn", ugmData.UserGNameEn);
                cm.Parameters.AddWithValue("@UserGId", ugmData.UserGId);
                cm.Parameters.AddWithValue("@User", username);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    model.result = dr["result"].ToString();
                    model.msg = dr["AddMessage"].ToString();
                    model.UserGId = dr["GrantId"].ToString();
                }
            }
            return model;
        }

        public UserGroupModel DeleteUserGroupByID(int UserGId)
        {
            UserGroupModel result = new UserGroupModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_UM_SetGrantMenu]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "SetDelete");
                cm.Parameters.AddWithValue("@UserGId", UserGId);

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result.result = dr["result"].ToString();
                    result.msg = dr["AddMessage"].ToString();
                }
            }
            return result;
        }

        public List<UserGroupModel> GetMenuByUserGroupId(int UserGId)
        {
            List<UserGroupModel> model = new List<UserGroupModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("[sp_UM_GetUserGroup]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "GetGrantMenu");
                cm.Parameters.AddWithValue("@UserGId", UserGId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    UserGroupModel x = new UserGroupModel();
                    x.UserGId = row["UserGId"].ToString();
                    x.UserGNameTh = row["UserGNameTh"].ToString();
                    x.MenuId = row["MenuId"].ToString();
                    x.MenuName = row["MenuName"].ToString();
                    x.Status = row["Status"].ToString();
                    x.CreateDate = row["CreateDate"].ToString();
                    model.Add(x);
                }
            }
            return model;
        }

        public UserGroupModel SetGrantMenuById(int MenuId,int UserGId,string sTs)
        {
            UserGroupModel result = new UserGroupModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_UM_SetGrantMenu]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "SetEditGrantMenu");
                cm.Parameters.AddWithValue("@UserGId", UserGId);
                cm.Parameters.AddWithValue("@MenuId", MenuId);
                cm.Parameters.AddWithValue("@Status", sTs);

                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result.UserGId = dr["GrantId"].ToString();
                    result.result = dr["result"].ToString();
                    result.msg = dr["AddMessage"].ToString();
                }
            }
            return result;
        }
    }
}