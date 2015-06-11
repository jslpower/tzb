using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Data.Linq.SqlClient;
using System.Web;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class SysUser
    {
        /// <summary>
        /// 获取收银列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_CashierList> GetCashierList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MManagerSearch SearchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_CashierList";
            string OrderString = " ORDER BY Id ASC";
            string strWhere = "(1=1)";
            #region 城市权限控制
            if (SearchModel.IsAllCity == false)
            {
                if (!String.IsNullOrWhiteSpace(SearchModel.CityLimitList))
                {
                    strWhere += " AND (CountyId IN (" + SearchModel.CityLimitList + "))";
                }
                else { strWhere += " AND (CountyId = 0)"; }
            }
            #endregion
            if (!String.IsNullOrWhiteSpace(SearchModel.UserName))
            {
                strWhere = strWhere + " AND (UserName like '%" + StringValidate.CheckSql(SearchModel.UserName.Trim()) + "%')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.KeyWord))
            {
                strWhere = strWhere + " AND (ContactName like '%" + StringValidate.CheckSql(SearchModel.KeyWord.Trim()) + "%')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_CashierList> q = rdc.ExecuteQuery<dt_CashierList>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<ManagerList> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MManagerSearch SearchModel)
        {
            string FieldsList = "*";
            string TableName = "tbl_ManagerList";
            string OrderString = " ORDER BY Id ASC";
            string strWhere = "(1=1)";
            if (SearchModel.IsCashier) { strWhere = "(FieldId<>'00000000-0000-0000-0000-000000000000')"; } else { strWhere = "(FieldId='00000000-0000-0000-0000-000000000000')"; }
            if (!String.IsNullOrWhiteSpace(SearchModel.UserName))
            {
                strWhere = strWhere + " AND (UserName like '%" + StringValidate.CheckSql(SearchModel.UserName.Trim()) + "%')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.KeyWord)) {
                strWhere = strWhere + " AND (ContactName like '%" + StringValidate.CheckSql(SearchModel.KeyWord.Trim()) + "%')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<ManagerList> q = rdc.ExecuteQuery<ManagerList>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 取得用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static ManagerList GetModel(int Id)
        { 
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.ManagerList.FirstOrDefault(n=>n.Id==Id);
                return model;
            }
        }
        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Add(ManagerList model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.ManagerList.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(ManagerList model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    if (!String.IsNullOrEmpty(model.Password))
                        m.Password = model.Password;
                    if (!String.IsNullOrEmpty(model.ContactName))
                        m.ContactName = model.ContactName;
                    m.ContactTel = model.ContactTel;
                    m.RoleId = model.RoleId;
                    m.PermissionList = model.PermissionList;
                    if (!String.IsNullOrEmpty(model.FieldId))
                        m.FieldId = model.FieldId;
                    if (!String.IsNullOrEmpty(model.FieldName))
                        m.FieldName = model.FieldName;
                    m.IsAllCity = model.IsAllCity;
                    m.CityList = model.CityList;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改用户电话及密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ContactTel"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static bool UpdateBasicInfo(int Id,string ContactTel,string Password)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    if (!String.IsNullOrEmpty(Password))
                        m.Password = Password;
                    m.ContactTel = ContactTel;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdateContactInfo(string UserName,string ContactName)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.UserName == UserName);
                if (m != null)
                {
                    m.ContactName = ContactName;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdatePassword(int Id,string Password)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    if (!String.IsNullOrEmpty(Password))
                        m.Password = Password;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 用户独立授权
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdatePer(ManagerList model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.PermissionList = model.PermissionList;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 用户独立授权
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdateQuickMenu(ManagerList model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.QuickMenu = model.QuickMenu;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(int EmployeeId)
        {
            using (FWDC rdc = new FWDC())
            {                
                var m = rdc.ManagerList.FirstOrDefault(n => n.EmployeeId == EmployeeId);
                if (m != null)
                {
                    rdc.ManagerList.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool Enable(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    m.IsEnable = true;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool Disabled(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.ManagerList.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    m.IsEnable = false;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
    }
}