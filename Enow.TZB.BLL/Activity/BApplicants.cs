using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BApplicants
    {
        /// <summary>
        /// 参加活动
        /// </summary>
        /// <param name="model"></param>
        public static void Add(tbl_Applicants model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_Applicants.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 用户删除报名信息
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Deletemodel(tbl_Applicants bmmodel)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Applicants.FirstOrDefault(w => w.IsDelete == 0 && w.Usid == bmmodel.Usid && w.Id == bmmodel.Id);                     
                if (m!=null)
                {
                    m.IsDelete = 1;
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 删除活动报名
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(List<string> listid)
        {
            using (FWDC rdc = new FWDC())
            {
                var liid = listid;
                var m = (from tmp in rdc.tbl_Applicants where liid.Contains(tmp.Id.ToString()) select tmp).ToList();
                if (m.Count > 0)
                {
                    for (int i = 0; i < m.Count; i++)
                    {
                        m[i].IsDelete = 1;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 修改活动信息
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_Applicants model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Applicants.FirstOrDefault(w => w.Id == model.Id);
                if (m != null)
                {
                    m.IsDelete = model.IsDelete;
                    m.IsState = model.IsState;
                    m.Indatetime = model.Indatetime;
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 修改场地预约状态
        /// </summary>
        /// <param name="listid">ID集合</param>
        /// <param name="staterid">报名状态 (1已通过 2未通过)</param>
        /// <returns></returns>
        public static bool UpdateBallStater(List<string> listid, int staterid)
        {
            using (FWDC rdc = new FWDC())
            {
                var liid = listid;
                ManagerList model = ManageUserAuth.GetManageUserModel();
                if (model != null)
                {
                    var m = (from tmp in rdc.dt_AppBallMenberView where tmp.UpUserID == model.Id && liid.Contains(tmp.AppId.ToString()) select tmp.AppId.ToString()).ToList();

                    if (m.Count > 0)
                    {
                        var n = (from tmp in rdc.tbl_Applicants where m.Contains(tmp.Id.ToString()) select tmp).ToList();
                        if (n.Count == 0)
                            return false;
                        for (int i = 0; i < n.Count; i++)
                        {
                            n[i].IsState = staterid;
                        }
                        rdc.SubmitChanges();
                        return true;
                    }
                }

                return false;
            }
        }
        /// <summary>
        /// 修改报名状态
        /// </summary>
        /// <param name="listid">ID集合</param>
        /// <param name="staterid">报名状态 (1已通过 2未通过)</param>
        /// <returns></returns>
        public static bool UpdateStater(List<string> listid,int staterid)
        {
            using (FWDC rdc = new FWDC())
            {
                var liid = listid;
                ManagerList model = ManageUserAuth.GetManageUserModel();
                if (model!=null)
                {
                    var m = (from tmp in rdc.dt_AcAlView where tmp.UpUserId ==model.Id.ToString() && liid.Contains(tmp.AlId.ToString()) select tmp.AlId.ToString()).ToList();
               
                    if (m.Count > 0)
                    {
                        var n = (from tmp in rdc.tbl_Applicants where m.Contains(tmp.Id.ToString()) select tmp).ToList();
                        if (n.Count == 0)
                            return false;
                        for (int i = 0; i < n.Count; i++)
                        {
                            n[i].IsState = staterid;
                        }
                        rdc.SubmitChanges();
                        return true;
                    }
                }
               
                return false;
            }
        }
        /// <summary>
        /// 查询是否已预约场地
        /// </summary>
        /// <param name="AcID">场地ID</param>
        /// <param name="UsID">用户ID</param>
        /// <returns></returns>
        public static tbl_Applicants GetUsCdbool(string AcID, string UsID)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Applicants.FirstOrDefault(w => w.IsState == 0 && w.ActivityID == AcID && w.Usid == UsID);
                if (m != null)
                {

                    return m;
                }
                return null;
            }
        }
        /// <summary>
        /// 查询是否已参加活动
        /// </summary>
        /// <param name="AcID">活动ID</param>
        /// <param name="UsID">用户ID</param>
        /// <returns></returns>
        public static tbl_Applicants GetUsbool(string AcID, string UsID)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Applicants.FirstOrDefault(w =>w.ActivityID == AcID && w.Usid == UsID);
                if (m != null)
                {

                    return m;
                }
                return null;
            }
        }
        /// <summary>
        /// （用户表 报名表）列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_ApplicantsMemberView> GetApplUserList(ref int rowsCount, int intPageSize, int CurrencyPage, MApplicants SearchModel)
        {
            string FieldsList = "*";
            string TableName = " dt_ApplicantsMemberView ";
            string OrderString = " ORDER BY Indatetime desc";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("(1=1)");
            strWhere.Append(" AND (IsDelete=0)");
            #region 查询条件
            if (SearchModel.ActivityId != -1)
                strWhere.Append(" AND ( ActivityID='" + SearchModel.ActivityId + "')");
            if (!string.IsNullOrEmpty(SearchModel.USid))
                strWhere.Append(" AND ( Usid='" + SearchModel.USid + "')");
            if (!string.IsNullOrEmpty(SearchModel.Acttitle))
            {
                strWhere.Append(" AND ( Title like '%" + SearchModel.Acttitle + "%' or  Starname like '%" + SearchModel.Acttitle + "%')");
            }
            #endregion

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere.ToString());
                rowsCount = query.First<int>();
                List<dt_ApplicantsMemberView> q = rdc.ExecuteQuery<dt_ApplicantsMemberView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere.ToString() + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// （活动 报名表）列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_AcAlView> GetAcpUserList(ref int rowsCount, int intPageSize, int CurrencyPage, MApplicants SearchModel)
        {
            string FieldsList = "*";
            string TableName = " dt_AcAlView ";
            string OrderString = " ORDER BY Indatetime desc";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("(1=1)");
            strWhere.Append(" AND (IsDelete=0)");
            strWhere.Append(" AND (AlDelete=0)");
            strWhere.Append(" AND (Activitytypes!=1)");
            #region 查询条件
            if (SearchModel.fbmb != -1)
                strWhere.Append(" AND ( Release=" + SearchModel.fbmb + ")");
            if (SearchModel.ActivityId != -1)
                strWhere.Append(" AND ( ActivityID='" + SearchModel.ActivityId + "')");
            if (!string.IsNullOrEmpty(SearchModel.USid))
                strWhere.Append(" AND ( Usid='" + SearchModel.USid + "')");
            if (SearchModel.Acttypes != -1)
                strWhere.Append(" AND ( Activitytypes=" + SearchModel.Acttypes + ")");
            if (!string.IsNullOrEmpty(SearchModel.Acttitle))
            {
                strWhere.Append(" AND ( Title like '%" + SearchModel.Acttitle + "%' or  Starname like '%" + SearchModel.Acttitle + "%')");
            }
            #endregion

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere.ToString());
                rowsCount = query.First<int>();
                List<dt_AcAlView> q = rdc.ExecuteQuery<dt_AcAlView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere.ToString() + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// （活动表 用户表 报名表）列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_RegistrationView> GetAppActUserList(ref int rowsCount, int intPageSize, int CurrencyPage, MApplicants SearchModel)
        {
            string FieldsList = "*";
            string TableName = " dt_RegistrationView ";
            string OrderString = " ORDER BY Indatetime desc";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("(1=1)");
            strWhere.Append(" AND (ActIsDelete=0)");
            strWhere.Append(" AND (IsDelete=0)");
            strWhere.Append(" AND (Activitytypes!=1)");
            #region 查询条件
            if (SearchModel.fbmb != -1)
                strWhere.Append(" AND ( Release=" + SearchModel.fbmb + ")");
            if (SearchModel.Acttypes != -1)
                strWhere.Append(" AND ( Activitytypes=" + SearchModel.Acttypes + ")");
            if (SearchModel.Appstater != -1)
                strWhere.Append(" AND ( IsState=" + SearchModel.Appstater + ")");
            if (!string.IsNullOrEmpty(SearchModel.Acttitle))
            {
                strWhere.Append(" AND ( Title like '%" + SearchModel.Acttitle + "%' or  Starname like '%" + SearchModel.Acttitle + "%')");
            }
            #endregion

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere.ToString());
                rowsCount = query.First<int>();
                List<dt_RegistrationView> q = rdc.ExecuteQuery<dt_RegistrationView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere.ToString() + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// （场地表 用户表 报名表）列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_AppBallMenberView> GetAppBalltUserList(ref int rowsCount, int intPageSize, int CurrencyPage, MApplicants SearchModel)
        {
            string FieldsList = "*";
            string TableName = " dt_AppBallMenberView ";
            string OrderString = " ORDER BY Indatetime desc";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("(1=1)");
            strWhere.Append(" AND (IsDelete=0)");
            #region 查询条件
            if (SearchModel.Appstater != -1)
                strWhere.Append(" AND ( IsState=" + SearchModel.Appstater + ")");
            if (!string.IsNullOrEmpty(SearchModel.Acttitle))
            {
                strWhere.Append(" AND ( FieldName like '%" + SearchModel.Acttitle + "%')");
            }
            #endregion

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere.ToString());
                rowsCount = query.First<int>();
                List<dt_AppBallMenberView> q = rdc.ExecuteQuery<dt_AppBallMenberView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere.ToString() + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// （场地 报名表）列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_ApplicantsBallView> GetAppActBallList(ref int rowsCount, int intPageSize, int CurrencyPage, MApplicants SearchModel)
        {
            string FieldsList = "*";
            string TableName = " dt_ApplicantsBallView ";
            string OrderString = " ORDER BY Indatetime desc";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("(1=1)");
            strWhere.Append(" AND (IsDelete=0)");
            #region 查询条件
            if (SearchModel.Appstater != -1)
                strWhere.Append(" AND ( IsState=" + SearchModel.Appstater + ")");
            if (!string.IsNullOrEmpty(SearchModel.Acttitle))
            {
                strWhere.Append(" AND ( FieldName like '%" + SearchModel.Acttitle + "%')");
            }
            if (!string.IsNullOrEmpty(SearchModel.USid))
            {
                strWhere.Append(" AND ( Usid='" + SearchModel.USid + "')");
            }
            #endregion

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere.ToString());
                rowsCount = query.First<int>();
                List<dt_ApplicantsBallView> q = rdc.ExecuteQuery<dt_ApplicantsBallView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere.ToString() + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
    }
}
