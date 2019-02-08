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
    public class MenuManagement
    {
        public static string _CON_STR = ConfigurationManager.ConnectionStrings["MDS"].ConnectionString;

        #region "ดึงข้อมูล SelectAllMenu"
        public List<ManageMenuModel> SelectAllMenu()
        {
            var listMenu = new List<ManageMenuModel>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_GetMenu", db);
                cm.Parameters.AddWithValue("@flag", "GetAll");
                cm.Parameters.AddWithValue("@LangCode", "TH");
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var menuModel = new ManageMenuModel()
                    {
                        MenuId = dr["MenuId"].ToString(),
                        MenuName = dr["MenuName"].ToString(),
                        MenuLevel = dr["MenuLevel"].ToString(),
                        LavelName = dr["LavelName"].ToString(),
                        ParentId = dr["ParentId"].ToString(),
                        ParentName = dr["ParentName"].ToString(),
                        MenuNo = dr["MenuNo"].ToString(),
                        MenuGroup = dr["MenuGroup"].ToString(),
                        MenuType = dr["MenuType"].ToString(),
                    };
                    listMenu.Add(menuModel);

                }

            }
            return listMenu;
        }
        #endregion

        #region "ดึงข้อมูล SelectAllMenu by id"
        public ManageMenuModel SelectMenuById(string menuId)
        {
            var menuData = new ManageMenuModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_GetMenu", db);
                cm.Parameters.AddWithValue("@flag", "GetById");
                cm.Parameters.AddWithValue("@LangCode", "TH");
                cm.Parameters.AddWithValue("@MenuId", menuId);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if(dr.Read())
                {
                    var menuModel = new ManageMenuModel()
                    {
                        MenuId = dr["MenuId"].ToString(),
                        MenuName = dr["MenuName"].ToString(),
                        MenuLevel = dr["MenuLevel"].ToString(),
                        LavelName = dr["LavelName"].ToString(),
                        ParentId = dr["ParentId"].ToString(),
                        ParentName = dr["ParentName"].ToString(),
                        MenuNo = dr["MenuNo"].ToString(),
                        MenuGroup = dr["MenuGroup"].ToString(),
                        MenuType = dr["MenuType"].ToString(),
                    };
                    menuData = menuModel;
                }
            }
            return menuData;
        }
        #endregion

        #region "ลบข้อมูล Menu"
        public ResultModel DeleteMenu(string menuId)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetMenu]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "SetDelete");
                cm.Parameters.AddWithValue("@MenuId", menuId);
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

        #region "ลบข้อมูลภาษา Menu"
        public ResultModel DeleteLangMenu(string menuId)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cm = new SqlCommand("[sp_SetMenu]", db);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@flag", "SetDeleteLang");
                cm.Parameters.AddWithValue("@MenuId", menuId);
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

        #region "แก้ไขข้อมูล Menu"
        public ResultModel EditMenu(ManageMenuModel editData)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cma = new SqlCommand("[sp_SetMenu]", db);
                cma.CommandType = CommandType.StoredProcedure;
                cma.Parameters.AddWithValue("@flag", "SetEdit");
                cma.Parameters.AddWithValue("@MenuLevel", editData.MenuLevel);
                cma.Parameters.AddWithValue("@MenuParent", editData.ParentId);
                cma.Parameters.AddWithValue("@MenuGroup", editData.MenuGroup);
                cma.Parameters.AddWithValue("@MenuNo", editData.MenuNo);
                cma.Parameters.AddWithValue("@User", editData.User);
                cma.Parameters.AddWithValue("@MenuId", editData.MenuId);
                cma.Parameters.AddWithValue("@LangName", editData.MenuName);
                SqlDataReader dr = cma.ExecuteReader();
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

        #region "ดึงข้อมูลภาษาเมนู GetByLang "
        public MenuLangModel SelectMenuAllLang(string menuId)
        {
            var menuLang = new MenuLangModel();
            menuLang.MenuList = new List<ManageMenuModel>();
            menuLang.LangCode = new List<string>();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                SqlCommand cm = new SqlCommand("sp_GetMenu", db);
                cm.Parameters.AddWithValue("@flag", "GetByLang");
                cm.Parameters.AddWithValue("@MenuId ", menuId);
                cm.CommandType = CommandType.StoredProcedure;
                if (db.State == ConnectionState.Closed) db.Open();
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var menuModel = new ManageMenuModel()
                    {
                        LangId = dr["LangId"].ToString(),
                        LangName = dr["LangName"].ToString(),
                        LangCode = dr["LangCode"].ToString()
                    };
                    menuLang.MenuList.Add(menuModel);
                }
                dr.NextResult();
                while (dr.Read())
                {
                    menuLang.LangCode.Add(dr["LangCode"].ToString());
                }
            }
            return menuLang;
        }
        #endregion

        #region "แก้ไขข้อมูลภาษา Menu"
        public ResultModel EditLangMenu(ManageMenuModel editData)
        {
            var model = new ResultModel();
            using (SqlConnection db = new SqlConnection(_CON_STR))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                SqlCommand cma = new SqlCommand("[sp_SetMenu]", db);
                cma.CommandType = CommandType.StoredProcedure;
                cma.Parameters.AddWithValue("@flag", "SetEditLang");
                cma.Parameters.AddWithValue("@LangName", editData.LangName);
                cma.Parameters.AddWithValue("@LangCode", editData.LangCode);
                cma.Parameters.AddWithValue("@MenuId", editData.MenuId);
                cma.Parameters.AddWithValue("@user", editData.User);
                SqlDataReader dr = cma.ExecuteReader();
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

    }
}