using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 求职管理
    /// </summary>
    public class BOffer
    {
        /// <summary>
        /// 获取求职列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_JobOffer> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MOfferSearch searchModel)
        {
            string FieldsList = " Id, JobName,UserName,MobilePhone,ContactName,PersonalId,Address,Email,WorkYear,BallYear,Specialty,BMGY, ApplyInfo,OfferState,IssueTime,";
            FieldsList += " CountryId, CountryName, ProvinceName, ProvinceId, CityId, CityName, AreaId,AreaName ";
            string TableName = "dt_JobOffer";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(1=1)";
            if (!string.IsNullOrEmpty(searchModel.JobName))
            {
                strWhere = strWhere + " AND (JobName like '%" + searchModel.JobName + "%')";
            }
            if (!string.IsNullOrEmpty(searchModel.OfferId))
            {
                strWhere += " and (Id='" + searchModel.OfferId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.UserName))
            {
                strWhere += " and ContactName like '%" + searchModel.UserName + "%'";
            }
            if (searchModel.StartIssueTime.HasValue)
            {
                strWhere += " and IssueTime>='" + searchModel.StartIssueTime + "'";
            }
            if (searchModel.EndIssueTime.HasValue)
            {
                strWhere += " and IssueTime<='" + searchModel.EndIssueTime + "'";
            }
            if (searchModel.CountryId>0)
            {
                strWhere += " and CountryId=" + searchModel.CountryId + "";
            }
            if (searchModel.ProvinceId>0)
            {
                strWhere += " and ProvinceId=" + searchModel.ProvinceId + "";
            }
            if (searchModel.CityId>0)
            {
                strWhere += " and CityId=" + searchModel.CityId + "";
            }
            if (searchModel.AreaId>0)
            {
                strWhere += " and AreaId=" + searchModel.AreaId + "";
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            {
                strWhere+=" and MobilePhone like '%"+searchModel.Mobile+"%'";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_JobOffer> q = rdc.ExecuteQuery<dt_JobOffer>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 是否报名
        /// </summary>
        /// <param name="JobId">职位编号</param>
        /// <param name="MemberId">会员编号</param>
        /// <returns></returns>
        public static bool IsSignUp(string JobId, string MemberId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Offer.FirstOrDefault(n => n.JobId == JobId && n.MemberId == MemberId);
                if (model != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 取得求职信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_Offer GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Offer.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 求职添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Add(tbl_Offer model)
        {
            using (FWDC rdc = new FWDC())
            {
                var JobModel = BJob.GetModel(model.JobId);
                if (JobModel != null)
                {
                    string JobName = JobModel.JobName;
                    JobModel = null;
                    rdc.tbl_Offer.InsertOnSubmit(model);
                    rdc.SubmitChanges();
                    #region 写入消息中心
                    #region 获取会员姓名
                    string ReceiveName = "";
                    var MemberModel = BMember.GetModel(model.MemberId);
                    if (MemberModel != null)
                    {
                        ReceiveName = MemberModel.ContactName;
                    }
                    MemberModel = null;
                    #endregion
                    //写入会员消息
                    BMessage.Add(new tbl_Message
                    {
                        Id = System.Guid.NewGuid().ToString(),
                        TypeId = (int)Model.EnumType.消息类型.系统消息,
                        SendId = "0",
                        SendName = "铁子帮",
                        SendTime = DateTime.Now,
                        ReceiveId = model.MemberId,
                        ReceiveName = ReceiveName,
                        MasterMsgId = "0",
                        MsgTitle = string.Format(CacheSysMsg.GetMsg(67), JobName),
                        MsgInfo = string.Format(CacheSysMsg.GetMsg(68), JobName),
                        IsRead = false,
                        IssueTime = DateTime.Now
                    });
                    #endregion
                }
            }
        }
        /// <summary>
        /// 状态审核
        /// </summary>
        /// <param name="Id">编号</param>
        /// <param name="IsValid">审核状态</param>
        /// <returns></returns>
        public static bool UpdateState(string Id, Model.EnumType.应聘状态 OfferState)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Offer.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    m.OfferState = (int)OfferState;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
    }
}
