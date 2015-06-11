using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 招聘信息
    /// </summary>
    public class BJob
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Job> GetList(ref int rowsCount, int intPageSize, int CurrencyPage,Model.MjobSearch searchModel)
        {
            string FieldsList = " id,OperatorId,ContactName,JobName,CoutryId,ProvinceId,CityId,StartDate,EndDate,JobNumber,JobRule,isnull(JobType,0) as JobType,JobInfo,jobreward,IsValid,IssueTime";
            string TableName = "tbl_Job";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(1=1)";
            strWhere += "AND (IsDelete=0)";
            if (!string.IsNullOrEmpty(searchModel.JobName))
            {
                strWhere = strWhere + " AND (JobName like '%" + searchModel.JobName + "%')";
            }
            if (searchModel.jobType.HasValue)
            {
                strWhere += " and ( jobType=" + searchModel.jobType + ")";
            }
            if (searchModel.IsValid.HasValue)
            {
                strWhere += " and (IsValid=" + searchModel.IsValid + ")";
            }
            if (searchModel.startDate.HasValue)
            {
                strWhere += " and (StartDate>='" + searchModel.startDate + "')";
            }
            if (searchModel.endDate.HasValue)
            {
                strWhere += " and (EndDate>='" + searchModel.endDate + "')";
            }

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_Job> q = rdc.ExecuteQuery<tbl_Job>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_JobList> GetListView(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MjobSearch searchModel)
        {
            string FieldsList = " Id,OperatorId,ContactName,JobName,CoutryId,CoutryName,ProvinceId,ProvinceName,CityId,CityName,StartDate,EndDate,JobNumber,JobType,IsValid,IssueTime";
            string TableName = "dt_JobList";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(1=1)";
            if (!string.IsNullOrEmpty(searchModel.JobName))
            {
                strWhere = strWhere + " AND (JobName like '%" + searchModel.JobName + "%')";
            }
            if (searchModel.jobType.HasValue)
            {
                strWhere += " and ( jobType=" + searchModel.jobType + ")";
            }
            if (searchModel.IsValid.HasValue)
            {
                strWhere += " and (IsValid=" + searchModel.IsValid + ")";
            }
            if (searchModel.startDate.HasValue)
            {
                strWhere += " and (StartDate<='" + searchModel.startDate + "')";
            }
            if (searchModel.endDate.HasValue)
            {
                strWhere += " and (EndDate>='" + searchModel.endDate + "')";
            }

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_JobList> q = rdc.ExecuteQuery<dt_JobList>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 取得招聘信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_Job GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Job.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 根据ID查询是否是舵主
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool Getdzbool(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_OfferMemberView.FirstOrDefault(n => n.MemberId == Id &&n.Jobtyoe!=0);
                if (model!=null)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 取得招聘信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static dt_JobList GetViewModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_JobList.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 招聘添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Add(tbl_Job model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_Job.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 修改招聘信息
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_Job model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Job.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.JobName = model.JobName;
                    m.CoutryId = model.CoutryId;
                    m.ProvinceId = model.ProvinceId;
                    m.CityId = model.CityId;
                    m.AreaId = model.AreaId;
                    m.StartDate = model.StartDate;
                    m.EndDate = model.EndDate;
                    m.JobNumber = model.JobNumber;
                    m.JobRule = model.JobRule;
                    m.JobInfo = model.JobInfo;
                    m.JobType = model.JobType;
                    m.TypeId = model.TypeId;
                    m.JobReward = model.JobReward;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 状态审核
        /// </summary>
        /// <param name="Id">编号</param>
        /// <param name="IsValid">审核状态</param>
        /// <returns></returns>
        public static bool UpdateValid(string Id,bool IsValid)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Job.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    m.IsValid = IsValid;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 删除招聘信息
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(string id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Job.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    m.IsDelete = 1;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        #region 舵主堂主站长操作
        /// <summary>
        /// 获取舵主堂主站长信息实体
        /// </summary>
        /// <param name="MemberID">舵/堂/站 ID</param>
        /// <returns></returns>
        public static dt_OfferMemberView GetJobdtzModel(string MemberID)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_OfferMemberView.FirstOrDefault(n => n.MemberId == MemberID);
                return model;
            }
        }
        /// <summary>
        /// 获取舵主堂主站长列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_OfferMemberView> GetdtzList(ref int rowsCount, int intPageSize, int CurrencyPage, dt_OfferMemberView searchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_OfferMemberView";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(1=1)";
            strWhere += "AND (OfferState=3 or OfferState=4)";
            if (searchModel.Jobtyoe!=0)
            {
                strWhere += " AND (Jobtyoe ="+searchModel.Jobtyoe+")";
            }
            if (searchModel.CityId > 0)
            {
                strWhere += " AND (CityId =" + searchModel.CityId + ")";
            }
            if (!string.IsNullOrEmpty(searchModel.ContactName))
            {
                strWhere += " AND (ContactName =" + searchModel.ContactName + ")";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_OfferMemberView> q = rdc.ExecuteQuery<dt_OfferMemberView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        #endregion
        

    }     
}
