using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    #region 球队
    public class BTeam
    {
        public static bool IsCreated(string TeamName)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_BallTeam.FirstOrDefault(n => n.TeamName == TeamName);
                if (model != null)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 根据球队的日志更新时间获取排序后的列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_BallTeamTimeView> GetTeamTimeList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MBallTeamSearch searchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_BallTeamTimeView";
            string OrderString = " ORDER BY tjtime Desc";
            string strWhere = "(1=1)";
            #region 查询条件
            if (searchModel.CountryId > 0)
            {
                strWhere = strWhere + " AND (CountryId = " + searchModel.CountryId + ")";
            }
            if (searchModel.ProvinceId > 0)
            {
                strWhere = strWhere + " AND (ProvinceId = " + searchModel.ProvinceId + ")";
            }
            if (searchModel.CityId > 0)
            {
                strWhere = strWhere + " AND (CityId = " + searchModel.CityId + ")";
            }
            if (searchModel.State.HasValue)
            {
                strWhere = strWhere + " AND (State = " + (int)searchModel.State.Value + ")";
            }
            if (searchModel.KeyWord != null)
            {
                strWhere = strWhere + " AND (TeamName like '%" + searchModel.KeyWord + "%')";
            }
            if (searchModel.CityId > 0)
            {
                strWhere = strWhere + " AND ( CityId = " + searchModel.CityId + ")";
            }
            #endregion
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_BallTeamTimeView> q = rdc.ExecuteQuery<dt_BallTeamTimeView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_BallTeam> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MBallTeamSearch searchModel)
        {
            string FieldsList = "*";
            string TableName = "tbl_BallTeam";
            string OrderString = " ORDER BY State ASC,IssueTime Desc";
            string strWhere = "(1=1)";
            #region 查询条件
            if (searchModel.CountryId > 0)
            {
                strWhere = strWhere + " AND (CountryId = " + searchModel.CountryId + ")";
            }
            if (searchModel.ProvinceId > 0)
            {
                strWhere = strWhere + " AND (ProvinceId = " + searchModel.ProvinceId + ")";
            }
            if (searchModel.CityId > 0)
            {
                strWhere = strWhere + " AND (CityId = " + searchModel.CityId + ")";
            }
            if (searchModel.State.HasValue)
            {
                strWhere = strWhere + " AND (State = " + (int)searchModel.State.Value + ")";
            }
            if (searchModel.KeyWord != null)
            {
                strWhere = strWhere + " AND (TeamName like '%" + searchModel.KeyWord + "%')";
            }
            if (searchModel.CityId > 0)
            {
                strWhere = strWhere + " AND ( CityId = " + searchModel.CityId + ")";
            }
            #endregion
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_BallTeam> q = rdc.ExecuteQuery<tbl_BallTeam>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_BallTeam> GetListView(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MBallTeamSearch searchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_BallTeam";
            string OrderString = " ORDER BY IssueTime Desc, State ASC";
            string strWhere = "(1=1)";
            #region 查询条件
            if (searchModel.CountryId > 0)
            {
                strWhere = strWhere + " AND (CountryId = " + searchModel.CountryId + ")";
            }
            if (searchModel.ProvinceId > 0)
            {
                strWhere = strWhere + " AND (ProvinceId = " + searchModel.ProvinceId + ")";
            }
            if (searchModel.CityId > 0)
            {
                strWhere = strWhere + " AND (CityId = " + searchModel.CityId + ")";
            }
            if (searchModel.AreaId > 0)
            {
                strWhere += " and (AreaId=" + searchModel.AreaId + ")";
            }
            #region 城市权限控制
            if (searchModel.IsAllCity == false)
            {
                if (!String.IsNullOrWhiteSpace(searchModel.CityLimitList))
                {
                    strWhere += " AND (AreaId IN (" + searchModel.CityLimitList + "))";
                }
                else { strWhere += " AND (AreaId = 0)"; }
            }
            #endregion
            if (searchModel.State.HasValue)
            {
                strWhere = strWhere + " AND (State = " + (int)searchModel.State.Value + ")";
            }
            if (searchModel.KeyWord != null)
            {
                strWhere = strWhere + " AND (TeamName like '%" + searchModel.KeyWord + "%')";
            }
            if (!string.IsNullOrWhiteSpace(searchModel.TeamOwner))
            {
                strWhere += " and (ContactName like '%" + searchModel.TeamOwner + "%')";
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            {
                strWhere += " and ( MobilePhone like '%" + searchModel.Mobile + "%')";
            }
            if (searchModel.IssueBeginTime.HasValue)
            {
                strWhere += " and ( IssueTime>='" + searchModel.IssueBeginTime + "')";
            }
            if (searchModel.IssueEndTime.HasValue)
            {
                strWhere += " and (IssueTime<='" + searchModel.IssueEndTime + "')";
            }
            if (searchModel.CheckBeginTime.HasValue)
            {
                strWhere += " and (CheckTime>='" + searchModel.CheckBeginTime + "')";
            }
            if (searchModel.CheckEndTime.HasValue)
            {
                strWhere += " and (CheckTime<='" + searchModel.CheckEndTime + "')";
            }
            if (searchModel.IsTeamer.HasValue)
            {
                if (searchModel.IsTeamer == true)
                {
                    strWhere += " and ( Id in (select teamId from tbl_teammember where roleType=" + (int)Model.EnumType.球员角色.队员 + "))";
                }
                else
                {
                    strWhere += " and ( Id not in (select teamId from tbl_teammember where roleType=" + (int)Model.EnumType.球员角色.队员 + "))";
                }
            }

            if (searchModel.JoinMatch.HasValue)
            {
                if (searchModel.JoinMatch == true)
                {
                    strWhere += " and ( Id in (select TeamId from tbl_MatchTeam))";
                }
                else
                {
                    strWhere += " and ( Id not in (select TeamId from tbl_MatchTeam))";
                }
            }
          
            #endregion
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_BallTeam> q = rdc.ExecuteQuery<dt_BallTeam>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 获取参加比赛的次数
        /// </summary>
        /// <param name="TeamId">球队ID</param>
        /// <returns></returns>
        public static int GetMatchCount(string TeamId)
        {
            using (FWDC rdc=new FWDC())
            {
                int Count = rdc.tbl_MatchTeam.Count(n => n.TeamId == TeamId && n.State==(int)Model.EnumType.参赛审核状态.已获参赛权);
                return Count;
            }
        }

        /// <summary>
        /// 获取球队队员数
        /// </summary>
        /// <param name="TeamId"></param>
        /// <returns></returns>
        public static int GetTeamerCount(string TeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                int Count = rdc.tbl_TeamMember.Count(n => n.TeamId == TeamId);
                return Count;
            }
        }
        /// <summary>
        /// 取得实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_BallTeam GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_BallTeam.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 取得球队视图实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static dt_BallTeam GetViewModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_BallTeam.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Add(tbl_BallTeam model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_BallTeam.InsertOnSubmit(model);
                rdc.SubmitChanges();
                #region 写入消息中心
                BMessage.Add(new tbl_Message
                {
                    Id = System.Guid.NewGuid().ToString(),
                    TypeId = (int)Model.EnumType.消息类型.系统消息,
                    SendId = "0",
                    SendName = "铁子帮",
                    SendTime = DateTime.Now,
                    ReceiveId = model.MemberId,
                    ReceiveName = model.MemberName,
                    MasterMsgId = "0",
                    MsgTitle = CacheSysMsg.GetMsg(73),
                    MsgInfo = CacheSysMsg.GetMsg(74),
                    IsRead = false,
                    IssueTime=DateTime.Now
                });
                #endregion
            }
        }
        /// <summary>
        /// 批量添加球队
        /// </summary>
        /// <param name="list"></param>
        public static string  Import(IEnumerable<tbl_TeamImportTemplate> list,int CountryId,string CountryName,int ProvinceId,string ProvinceName,int CityId,string CityName,int AreaId,string AreaName)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_TeamImportTemplate.InsertAllOnSubmit(list);
                rdc.SubmitChanges();
                string strRv = "";
                rdc.esp_TeamExcelImport(CountryId,CountryName,ProvinceId,ProvinceName,CityId,CityName,AreaId,AreaName,ref strRv);
                return strRv;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_BallTeam model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_BallTeam.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.TeamName = model.TeamName;
                    if (model.CountryId > 0)
                    {
                        m.CountryId = model.CountryId;
                    }
                    if (!string.IsNullOrWhiteSpace(model.CountryName))
                    {
                        m.CountryName = model.CountryName;
                    }
                    if (model.ProvinceId>0)
                    {
                        m.ProvinceId = model.ProvinceId;
                    }
                    if (!string.IsNullOrWhiteSpace(model.ProvinceName))
                    {
                        m.ProvinceName = model.ProvinceName;
                    }
                    if (model.CityId>0)
                    {
                        m.CityId = model.CityId;
                    }
                    if (!string.IsNullOrWhiteSpace(model.CityName))
                    {
                        m.CityName = model.CityName;
                    }
                    if (model.AreaId>0)
                    {
                        m.AreaId = model.AreaId;
                    }
                    if (!string.IsNullOrWhiteSpace(model.AreaName))
                    {
                        m.AreaName = model.AreaName;
                    }
                    
                    if (!String.IsNullOrWhiteSpace(model.TeamPhoto))
                    {
                        m.TeamPhoto = model.TeamPhoto;
                    }
                    m.TeamInfo = model.TeamInfo;
                    m.State = (int)model.State;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 更新荣誉
        /// </summary>
        /// <param name="Id">编号</param>
        /// <param name="HonorNumber">荣誉</param>
        /// <returns></returns>
        public static bool UpdateHonorNumber(string Id, Model.EnumType.操作符号 Operation, int HonorNumber)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_BallTeam.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    switch (Operation)
                    {
                        case Model.EnumType.操作符号.加:
                            m.HonorNumber = m.HonorNumber + HonorNumber;
                            rdc.SubmitChanges();
                            break;
                        case Model.EnumType.操作符号.减:
                            m.HonorNumber = m.HonorNumber - HonorNumber;
                            rdc.SubmitChanges();
                            break;
                    }
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 更新保证金总数
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Operation"></param>
        /// <param name="DepositMoney"></param>
        /// <returns></returns>
        public static bool UpdateDepositMoney(string Id, Model.EnumType.操作符号 Operation, decimal DepositMoney)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_BallTeam.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    switch (Operation)
                    {
                        case Model.EnumType.操作符号.加:
                            m.DepositTotalMoney = m.DepositTotalMoney + DepositMoney;
                            rdc.SubmitChanges();
                            break;
                        case Model.EnumType.操作符号.减:
                            m.DepositTotalMoney = m.DepositTotalMoney - DepositMoney;
                            rdc.SubmitChanges();
                            break;
                    }
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 状态审核
        /// </summary>
        /// <param name="Id">球队编号</param>
        /// <param name="UserId">审核人ID</param>
        /// <param name="UserName">审核人姓名</param>
        /// <param name="State">审核状态</param>
        /// <param name="DisbandReason">申请解散理由</param>
        /// <param name="RefuseId">初审/终审拒绝理由ID</param>
        /// <param name="DisbandRefuseReason">拒绝解散理由</param>
        /// <returns></returns>
        public static bool UpdateState(string Id,int UserId,string UserName, Model.EnumType.球队审核状态 State,string DisbandReason,int RefuseId,string DisbandRefuseReason)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_BallTeam.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    switch (State)
                    {    
                        case Model.EnumType.球队审核状态.审核中:
                            m.CheckId = UserId;
                            m.CheckName = UserName;
                            m.CheckTime = DateTime.Now;
                            m.State = (int)State;
                            //更新队长审核标识
                            //BTeamMember.MemberUpdateState(m.MemberId, Model.EnumType.球员审核状态.审核通过);
                            rdc.SubmitChanges();
                            break;
                        case Model.EnumType.球队审核状态.初审通过:
                            m.CheckId = UserId;
                            m.CheckName = UserName;
                            m.CheckTime = DateTime.Now;
                            m.State = (int)State;
                            //更新队长审核标识
                            BTeamMember.MemberUpdateState(m.MemberId, Model.EnumType.球员审核状态.审核通过);
                            rdc.SubmitChanges();
                            #region 写入消息中心
                            BMessage.Add(new tbl_Message
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                TypeId = (int)Model.EnumType.消息类型.系统消息,
                                SendId = "0",
                                SendName = "铁子帮",
                                SendTime = DateTime.Now,
                                ReceiveId = m.MemberId,
                                ReceiveName = m.MemberName,
                                MasterMsgId = "0",
                                MsgTitle = string.Format(CacheSysMsg.GetMsg(75), m.TeamName),
                                MsgInfo = string.Format(CacheSysMsg.GetMsg(76), m.TeamName),
                                IsRead = false,
                                IssueTime=DateTime.Now
                            });
                            #endregion
                            break;                            
                        case Model.EnumType.球队审核状态.初审拒绝:
                            m.CheckId = UserId;
                            m.CheckName = UserName;
                            m.CheckTime = DateTime.Now;
                            m.State = (int)State;
                            m.Refuse_check_reason = RefuseId;
                            rdc.SubmitChanges();
                            #region 写入消息中心
                            BMessage.Add(new tbl_Message
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                TypeId = (int)Model.EnumType.消息类型.系统消息,
                                SendId = "0",
                                SendName = "铁子帮",
                                SendTime = DateTime.Now,
                                ReceiveId = m.MemberId,
                                ReceiveName = m.MemberName,
                                MasterMsgId = "0",
                                MsgTitle = string.Format(CacheSysMsg.GetMsg(77), m.TeamName),
                                MsgInfo = string.Format("您申请组建的{0}队未通过初审，未通过的原因:{1},请重新填写您想组建球队的资料。",m.TeamName,((Model.EnumType.球队审核不通过原因)RefuseId).ToString()),
                                IsRead = false,
                                IssueTime = DateTime.Now
                            });
                            #endregion
                            break;
                        case Model.EnumType.球队审核状态.终审通过:
                            m.EndCheckId = UserId;
                            m.EndCheckName = UserName;
                            m.EndCheckTime = DateTime.Now;
                            m.State = (int)State;
                            rdc.SubmitChanges();
                            #region 写入消息中心
                            BMessage.Add(new tbl_Message
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                TypeId = (int)Model.EnumType.消息类型.系统消息,
                                SendId = "0",
                                SendName = "铁子帮",
                                SendTime = DateTime.Now,
                                ReceiveId = m.MemberId,
                                ReceiveName = m.MemberName,
                                MasterMsgId = "0",
                                MsgTitle = string.Format(CacheSysMsg.GetMsg(79), m.TeamName),
                                MsgInfo = string.Format(CacheSysMsg.GetMsg(80), m.TeamName),
                                IsRead = false,
                                IssueTime = DateTime.Now
                            });
                            #endregion
                            break;
                        case Model.EnumType.球队审核状态.终审拒绝:
                            m.EndCheckId = UserId;
                            m.EndCheckName = UserName;
                            m.EndCheckTime = DateTime.Now;
                            m.State = (int)State;
                            m.Refuse_encheck_reason = RefuseId;
                            rdc.SubmitChanges();
                            #region 写入消息中心
                            BMessage.Add(new tbl_Message
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                TypeId = (int)Model.EnumType.消息类型.系统消息,
                                SendId = "0",
                                SendName = "铁子帮",
                                SendTime = DateTime.Now,
                                ReceiveId = m.MemberId,
                                ReceiveName = m.MemberName,
                                MasterMsgId = "0",
                                MsgTitle = string.Format(CacheSysMsg.GetMsg(81), m.TeamName),
                                MsgInfo = string.Format("您申请组建的{0}队未通过终审，未通过的原因:{1}，请重新填写您想组建球队的资料。", m.TeamName, ((Model.EnumType.球队审核不通过原因)RefuseId).ToString()),
                                IsRead = false,
                                IssueTime = DateTime.Now
                            });
                            #endregion
                            break;
                        case Model.EnumType.球队审核状态.解散申请:
                            m.RequestTime = DateTime.Now;
                            m.DisbandReason = DisbandReason;
                            m.State = (int)State;
                            rdc.SubmitChanges();
                            #region 写入消息中心
                            //创建人消息
                            BMessage.Add(new tbl_Message
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                TypeId = (int)Model.EnumType.消息类型.系统消息,
                                SendId = "0",
                                SendName = "铁子帮",
                                SendTime = DateTime.Now,
                                ReceiveId = m.MemberId,
                                ReceiveName = m.MemberName,
                                MasterMsgId = "0",
                                MsgTitle = string.Format(CacheSysMsg.GetMsg(83), m.TeamName),
                                MsgInfo = string.Format(CacheSysMsg.GetMsg(84), m.TeamName),
                                IsRead = false,
                                IssueTime = DateTime.Now
                            });
                            //所有成员发消息
                            var list = BTeamMember.GetList(Id);
                            if (list != null) {
                                foreach (var TMModel in list) {
                                    if(TMModel.RoleType != (int)Model.EnumType.球员角色.队长){
                                    BMessage.Add(new tbl_Message
                                    {
                                        Id = System.Guid.NewGuid().ToString(),
                                        TypeId = (int)Model.EnumType.消息类型.系统消息,
                                        SendId = "0",
                                        SendName = "铁子帮",
                                        SendTime = DateTime.Now,
                                        ReceiveId = TMModel.MemberId,
                                        ReceiveName = TMModel.ContactName,
                                        MasterMsgId = "0",
                                        MsgTitle = string.Format(CacheSysMsg.GetMsg(85), m.TeamName),
                                        MsgInfo = string.Format(CacheSysMsg.GetMsg(86), m.TeamName),
                                        IsRead = false,
                                        IssueTime = DateTime.Now
                                    });
                                    }
                                }
                            }
                            #endregion
                            break;
                        case Model.EnumType.球队审核状态.解散通过:
                            m.RequestCheckId = UserId;
                            m.RequestCheckName = UserName;
                            m.RequestCheckTime = DateTime.Now;
                            m.State = (int)State;
                            rdc.SubmitChanges();
                            #region 写入消息中心
                            //队长消息
                            BMessage.Add(new tbl_Message
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                TypeId = (int)Model.EnumType.消息类型.系统消息,
                                SendId = "0",
                                SendName = "铁子帮",
                                SendTime = DateTime.Now,
                                ReceiveId = m.MemberId,
                                ReceiveName = m.MemberName,
                                MasterMsgId = "0",
                                MsgTitle = string.Format(CacheSysMsg.GetMsg(87), m.TeamName),
                                MsgInfo = string.Format(CacheSysMsg.GetMsg(88), m.TeamName),
                                IsRead = false,
                                IssueTime = DateTime.Now
                            });
                            //所有成员发消息
                            var list2 = BTeamMember.GetList(Id);
                            if (list2 != null) {
                                foreach (var TMModel in list2) {
                                    if(TMModel.RoleType != (int)Model.EnumType.球员角色.队长){
                                    BMessage.Add(new tbl_Message
                                    {
                                        Id = System.Guid.NewGuid().ToString(),
                                        TypeId = (int)Model.EnumType.消息类型.系统消息,
                                        SendId = "0",
                                        SendName = "铁子帮",
                                        SendTime = DateTime.Now,
                                        ReceiveId = TMModel.MemberId,
                                        ReceiveName = TMModel.ContactName,
                                        MasterMsgId = "0",
                                        MsgTitle = string.Format(CacheSysMsg.GetMsg(89), m.TeamName),
                                        MsgInfo = string.Format(CacheSysMsg.GetMsg(90), m.TeamName),
                                        IsRead = false,
                                        IssueTime = DateTime.Now
                                    });
                                    }
                                }
                            }
                            #endregion
                            break;
                        case Model.EnumType.球队审核状态.解散拒绝:
                            m.EndCheckId = UserId;
                            m.EndCheckName = UserName;
                            m.EndCheckTime = DateTime.Now;
                            m.State = (int)Model.EnumType.球队审核状态.解散拒绝;
                           m.Refuse_disband_reason = DisbandRefuseReason;
                            rdc.SubmitChanges();
                            #region 写入消息中心
                            BMessage.Add(new tbl_Message
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                TypeId = (int)Model.EnumType.消息类型.系统消息,
                                SendId = "0",
                                SendName = "铁子帮",
                                SendTime = DateTime.Now,
                                ReceiveId = m.MemberId,
                                ReceiveName = m.MemberName,
                                MasterMsgId = "0",
                                MsgTitle = string.Format(CacheSysMsg.GetMsg(163), m.TeamName),
                                MsgInfo = string.Format("您申请解散的{0}队被拒绝,拒绝的原因是:{1}！",m.TeamName,DisbandRefuseReason),
                                IsRead = false,
                                IssueTime = DateTime.Now
                            });
                            #endregion
                            break;
                    }                    
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 球队队长转让
        /// </summary>
        /// <param name="Id">球队ID</param>
        /// <param name="MemberId">转让人ID</param>
        /// <param name="MemberName">转让人姓名</param>
        /// <returns></returns>
        public static bool CaptainTransfer(string Id, string MemberId, string MemberName)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.tbl_BallTeam.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    #region 写入消息中心
                    //队长消息
                    BMessage.Add(new tbl_Message
                    {
                        Id = System.Guid.NewGuid().ToString(),
                        TypeId = (int)Model.EnumType.消息类型.系统消息,
                        SendId = "0",
                        SendName = "铁子帮",
                        SendTime = DateTime.Now,
                        ReceiveId = m.MemberId,
                        ReceiveName = m.MemberName,
                        MasterMsgId = "0",
                        MsgTitle = string.Format("您已成功将"+m.TeamName+"队的队长身份转让给"+MemberName+""),
                        MsgInfo = string.Format("您已成功将" + m.TeamName + "队的队长身份转让给" + MemberName + ""),
                        IsRead = false,
                        IssueTime = DateTime.Now
                    });
                    //所有成员发消息
                    var list2 = BTeamMember.GetList(Id);
                    if (list2 != null)
                    {
                        foreach (var TMModel in list2)
                        {
                            if (TMModel.RoleType != (int)Model.EnumType.球员角色.队长 &&TMModel.MemberId!=MemberId)
                            {
                                BMessage.Add(new tbl_Message
                                {
                                    Id = System.Guid.NewGuid().ToString(),
                                    TypeId = (int)Model.EnumType.消息类型.系统消息,
                                    SendId = "0",
                                    SendName = "铁子帮",
                                    SendTime = DateTime.Now,
                                    ReceiveId = TMModel.MemberId,
                                    ReceiveName = TMModel.ContactName,
                                    MasterMsgId = "0",
                                    MsgTitle = string.Format(""+m.MemberName+"已成功将队长身份转让给"+MemberName+","+MemberName+"已成为"+m.TeamName+"队队长!"),
                                    MsgInfo = string.Format("" + m.MemberName + "已成功将队长身份转让给" + MemberName + "," + MemberName + "已成为" + m.TeamName + "队队长!"),
                                    IsRead = false,
                                    IssueTime = DateTime.Now
                                });
                            }
                        }
                    }
                    //新队长消息
                    BMessage.Add(new tbl_Message
                    {
                        Id = System.Guid.NewGuid().ToString(),
                        TypeId = (int)Model.EnumType.消息类型.系统消息,
                        SendId = "0",
                        SendName = "铁子帮",
                        SendTime = DateTime.Now,
                        ReceiveId = MemberId,
                        ReceiveName = MemberName,
                        MasterMsgId = "0",
                        MsgTitle = string.Format("您已成为" + m.TeamName + "队的队长"),
                        MsgInfo = string.Format("您已成为" + m.TeamName + "队的队长"),
                        IsRead = false,
                        IssueTime = DateTime.Now
                    });
                    #endregion
                    m.MemberId = MemberId;
                    m.MemberName = MemberName;
                    rdc.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
    #endregion
    #region 球队成员
    /// <summary>
    /// 求职管理
    /// </summary>
    public class BTeamMember
    {
        /// <summary>
        /// 取得球队成员列表
        /// </summary>
        /// <param name="TeamUsId"></param>
        /// <returns></returns>
        public static dt_TeamMember GetTeamUsModel(string TeamUsId)
        {
            using (FWDC rdc = new FWDC())
            {
                var q = (from query in rdc.dt_TeamMember
                        where query.MemberId == TeamUsId && query.State != (int)Model.EnumType.球员审核状态.拒绝 && query.State != (int)Model.EnumType.球员审核状态.踢除 && query.State != (int)Model.EnumType.球员审核状态.同意退出
                        orderby query.RoleType ascending, query.IssueTime ascending
                        select query).ToList();
                if (q.Count>0)
                {
                    return q[0];
                }
                return null;
            }
        }
        /// <summary>
        /// 取得球队成员列表
        /// </summary>
        /// <param name="TeamId"></param>
        /// <returns></returns>
        public static List<dt_TeamMember> GetList(string TeamId)
        {            
            using (FWDC rdc = new FWDC())
            {
                var q = from query in rdc.dt_TeamMember
                        where query.TeamId == TeamId && query.State != (int)Model.EnumType.球员审核状态.拒绝 && query.State != (int)Model.EnumType.球员审核状态.踢除 && query.State != (int)Model.EnumType.球员审核状态.同意退出
                        orderby query.RoleType ascending, query.IssueTime ascending
                        select query;
                return q.ToList();
            }
        }
        /// <summary>
        /// 取得球队已审成员列表
        /// </summary>
        /// <param name="TeamId"></param>
        /// <returns></returns>
        public static List<dt_TeamMember> GetCheckList(string TeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                var q = from query in rdc.dt_TeamMember
                        where query.TeamId == TeamId && query.State == (int)Model.EnumType.球员审核状态.审核通过 && query.RoleType < (int)Model.EnumType.球员角色.球迷
                        orderby query.RoleType ascending, query.IssueTime ascending
                        select query;
                return q.ToList();
            }
        }
        /// <summary>
        /// 根据会员编号取得退出审核的信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_TeamMember GetModelNoState(string MemberId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_TeamMember.FirstOrDefault(n => n.MemberId == MemberId && n.State == (int)Model.EnumType.球员审核状态.退出申请);
                return model;
            }
        }
        /// <summary>
        /// 根据队伍编号取队长信息
        /// </summary>
        /// <param name="TeamId"></param>
        /// <returns></returns>
        public static dt_TeamMember GetModelByTeamId(string TeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_TeamMember.FirstOrDefault(n => n.TeamId == TeamId && n.RoleType == (int)Model.EnumType.球员角色.队长 && n.State == (int)Model.EnumType.球员审核状态.审核通过);
                return model;
            }
        }
        /// <summary>
        /// 取得信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static dt_TeamMemberList GetModel(string MemberId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_TeamMemberList.FirstOrDefault(n => n.MemberId == MemberId && n.TeamState < (int)Model.EnumType.球队审核状态.解散通过 && (n.State < (int)Model.EnumType.球员审核状态.拒绝||n.State==(int)Model.EnumType.球员审核状态.退出申请));
                return model;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Add(tbl_TeamMember model)
        {
            using (FWDC rdc = new FWDC())
            {
                var TeamModel = BTeam.GetModel(model.TeamId);
                if (TeamModel != null)
                {
                    rdc.tbl_TeamMember.InsertOnSubmit(model);
                    rdc.SubmitChanges();
                    if (model.RoleType != (int)Model.EnumType.球员角色.队长)
                    {
                        string ReceiveName = "";
                        var MemberModel = BMember.GetModel(model.MemberId);
                        if (MemberModel != null)
                        {
                            ReceiveName = MemberModel.ContactName;
                        }
                        MemberModel = null;
                        #region 写入消息中心
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
                            MsgTitle = string.Format(CacheSysMsg.GetMsg(91), TeamModel.TeamName),
                            MsgInfo = string.Format(CacheSysMsg.GetMsg(92), TeamModel.TeamName),
                            IsRead = false,
                            IssueTime = DateTime.Now
                        });
                        //写入队长消息
                        BMessage.Add(new tbl_Message
                        {
                            Id = System.Guid.NewGuid().ToString(),
                            TypeId = (int)Model.EnumType.消息类型.系统消息,
                            SendId = "0",
                            SendName = "铁子帮",
                            SendTime = DateTime.Now,
                            ReceiveId = TeamModel.MemberId,
                            ReceiveName = TeamModel.MemberName,
                            MasterMsgId = "0",
                            MsgTitle = string.Format(CacheSysMsg.GetMsg(93), TeamModel.TeamName, ReceiveName),
                            MsgInfo = string.Format(CacheSysMsg.GetMsg(94), TeamModel.TeamName, ReceiveName),
                            IsRead = false,
                            IssueTime = DateTime.Now
                        });
                        #endregion
                    }
                }
            }
        }
        /// <summary>
        /// 修改基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateBaseInfo(tbl_TeamMember model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_TeamMember.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.DNWZ = model.DNWZ;
                    m.DNQYHM = model.DNQYHM;
                    m.LastCheckTime = DateTime.Now;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_TeamMember model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_TeamMember.FirstOrDefault(n => n.Id == model.Id);
                 if (m != null)
                 {
                     #region 获取球队成员姓名
                     string ReceiveName = "";
                     var MemberModel = BMember.GetModel(model.MemberId);
                     if (MemberModel != null)
                     {
                         ReceiveName = MemberModel.ContactName;
                     }
                     MemberModel = null;
                     #endregion
                     var TeamModel = BTeam.GetModel(m.TeamId);
                     if (TeamModel != null)
                     {
                         m.State = model.State;
                         m.DNWZ = model.DNWZ;
                         m.DNQYHM = model.DNQYHM;
                         m.LastCheckTime = DateTime.Now;
                         rdc.SubmitChanges();
                         switch (model.State.Value)
                         {
                             case (int)Model.EnumType.球员审核状态.审核通过:
                                 #region 写入消息中心
                                 //写入会员消息
                                 BMessage.Add(new tbl_Message
                                 {
                                     Id = System.Guid.NewGuid().ToString(),
                                     TypeId = (int)Model.EnumType.消息类型.系统消息,
                                     SendId = "0",
                                     SendName = "铁子帮",
                                     SendTime = DateTime.Now,
                                     ReceiveId = m.MemberId,
                                     ReceiveName = ReceiveName,
                                     MasterMsgId = "0",
                                     MsgTitle = string.Format(CacheSysMsg.GetMsg(95), TeamModel.TeamName),
                                     MsgInfo = string.Format(CacheSysMsg.GetMsg(96), TeamModel.TeamName),
                                     IsRead = false,
                                     IssueTime = DateTime.Now
                                 });
                                 #endregion
                                 break;
                             case (int)Model.EnumType.球员审核状态.拒绝:
                                 #region 写入消息中心
                                 //写入会员消息
                                 BMessage.Add(new tbl_Message
                                 {
                                     Id = System.Guid.NewGuid().ToString(),
                                     TypeId = (int)Model.EnumType.消息类型.系统消息,
                                     SendId = "0",
                                     SendName = "铁子帮",
                                     SendTime = DateTime.Now,
                                     ReceiveId = m.MemberId,
                                     ReceiveName = ReceiveName,
                                     MasterMsgId = "0",
                                     MsgTitle = string.Format(CacheSysMsg.GetMsg(113), TeamModel.TeamName),
                                     MsgInfo = string.Format(CacheSysMsg.GetMsg(115), TeamModel.TeamName),
                                     IsRead = false,
                                     IssueTime = DateTime.Now
                                 });
                                 #endregion
                                 break;
                         }
                         return true;
                     }
                     else
                     {
                         return false;
                     }
                 }
                 else { return false; }
            }
        }
        /// <summary>
        /// 状态审核
        /// </summary>
        /// <param name="Id">编号</param>
        /// <param name="State">审核状态</param>
        /// <returns></returns>
        public static bool MemberUpdateState(string MemberId, Model.EnumType.球员审核状态 State)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_TeamMember.FirstOrDefault(n => n.MemberId == MemberId);
                if (m != null)
                {
                    #region 获取球队成员姓名
                    string ReceiveName = "";
                    var MemberModel = BMember.GetModel(m.MemberId);
                    if (MemberModel != null)
                    {
                        ReceiveName = MemberModel.ContactName;
                    }
                    MemberModel = null;
                    #endregion
                    var TeamModel = BTeam.GetModel(m.TeamId);
                    if (TeamModel != null)
                    {
                        switch (State)
                        {
                            case Model.EnumType.球员审核状态.审核中:
                            case Model.EnumType.球员审核状态.审核通过:
                            case Model.EnumType.球员审核状态.拒绝:
                                m.State = (int)State;
                                m.LastCheckTime = DateTime.Now;
                                break;
                            case Model.EnumType.球员审核状态.踢除:
                                m.State = (int)State;
                                m.LastCheckTime = DateTime.Now;
                                break;
                            case Model.EnumType.球员审核状态.退出申请:
                                m.State = (int)State;
                                m.RequestTime = DateTime.Now;
                                break;
                            case Model.EnumType.球员审核状态.同意退出:
                                m.State = (int)State;
                                m.RequestCheckTime = DateTime.Now;
                                break;
                        }
                        rdc.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 状态审核
        /// </summary>
        /// <param name="Id">编号</param>
        /// <param name="State">审核状态</param>
        /// <returns></returns>
        public static bool UpdateState(string Id, Model.EnumType.球员审核状态 State)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_TeamMember.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    #region 获取球队成员姓名
                    string ReceiveName = "";
                    var MemberModel = BMember.GetModel(m.MemberId);
                    if (MemberModel != null)
                    {
                        ReceiveName = MemberModel.ContactName;
                    }
                    MemberModel = null;
                    #endregion
                    var TeamModel = BTeam.GetModel(m.TeamId);
                    if (TeamModel != null)
                    {
                        switch (State)
                        {
                            case Model.EnumType.球员审核状态.审核中:
                            case Model.EnumType.球员审核状态.审核通过:
                            case Model.EnumType.球员审核状态.拒绝:
                                m.State = (int)State;
                                m.LastCheckTime = DateTime.Now;
                                break;
                            case Model.EnumType.球员审核状态.踢除:
                                m.State = (int)State;
                                m.LastCheckTime = DateTime.Now;
                                #region 写入消息中心
                                //写入会员消息
                                BMessage.Add(new tbl_Message
                                {
                                    Id = System.Guid.NewGuid().ToString(),
                                    TypeId = (int)Model.EnumType.消息类型.系统消息,
                                    SendId = "0",
                                    SendName = "铁子帮",
                                    SendTime = DateTime.Now,
                                    ReceiveId = m.MemberId,
                                    ReceiveName = ReceiveName,
                                    MasterMsgId = "0",
                                    MsgTitle = string.Format(CacheSysMsg.GetMsg(97), TeamModel.TeamName),
                                    MsgInfo = string.Format(CacheSysMsg.GetMsg(98), TeamModel.TeamName),
                                    IsRead = false,
                                    IssueTime = DateTime.Now
                                });
                                #endregion
                                break;
                            case Model.EnumType.球员审核状态.退出申请:
                                m.State = (int)State;
                                m.RequestTime = DateTime.Now;
                                #region 写入消息中心
                                //写入会员消息
                                BMessage.Add(new tbl_Message
                                {
                                    Id = System.Guid.NewGuid().ToString(),
                                    TypeId = (int)Model.EnumType.消息类型.系统消息,
                                    SendId = "0",
                                    SendName = "铁子帮",
                                    SendTime = DateTime.Now,
                                    ReceiveId = m.MemberId,
                                    ReceiveName = ReceiveName,
                                    MasterMsgId = "0",
                                    MsgTitle = string.Format(CacheSysMsg.GetMsg(99), TeamModel.TeamName),
                                    MsgInfo = string.Format(CacheSysMsg.GetMsg(100), TeamModel.TeamName),
                                    IsRead = false,
                                    IssueTime = DateTime.Now
                                });
                                //写入队长消息
                                BMessage.Add(new tbl_Message
                                {
                                    Id = System.Guid.NewGuid().ToString(),
                                    TypeId = (int)Model.EnumType.消息类型.系统消息,
                                    SendId = "0",
                                    SendName = "铁子帮",
                                    SendTime = DateTime.Now,
                                    ReceiveId = TeamModel.MemberId,
                                    ReceiveName = TeamModel.MemberName,
                                    MasterMsgId = "0",
                                    MsgTitle = string.Format(CacheSysMsg.GetMsg(101), TeamModel.TeamName, ReceiveName),
                                    MsgInfo = string.Format(CacheSysMsg.GetMsg(102), TeamModel.TeamName, ReceiveName),
                                    IsRead = false,
                                    IssueTime = DateTime.Now
                                });
                                #endregion
                                break;
                            case Model.EnumType.球员审核状态.同意退出:
                                m.State = (int)State;
                                m.RequestCheckTime = DateTime.Now;
                                #region 写入消息中心
                                //写入会员消息
                                BMessage.Add(new tbl_Message
                                {
                                    Id = System.Guid.NewGuid().ToString(),
                                    TypeId = (int)Model.EnumType.消息类型.系统消息,
                                    SendId = "0",
                                    SendName = "铁子帮",
                                    SendTime = DateTime.Now,
                                    ReceiveId = m.MemberId,
                                    ReceiveName = ReceiveName,
                                    MasterMsgId = "0",
                                    MsgTitle = string.Format(CacheSysMsg.GetMsg(103), TeamModel.TeamName),
                                    MsgInfo = string.Format(CacheSysMsg.GetMsg(104), TeamModel.TeamName),
                                    IsRead = false,
                                    IssueTime = DateTime.Now
                                });
                                #endregion
                                break;
                        }
                        rdc.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 球员角色转换
        /// </summary>
        /// <param name="TeamId"></param>
        /// <param name="MemberId"></param>
        /// <param name="RoleType"></param>
        /// <returns></returns>
        public static bool UpdateRoleType(string TeamId, string MemberId, Model.EnumType.球员角色 RoleType)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.tbl_TeamMember.FirstOrDefault(n => n.TeamId == TeamId && n.MemberId == MemberId && n.State == (int)Model.EnumType.球员审核状态.审核通过);
                if (m != null)
                {
                    m.RoleType = (int)RoleType;
                    rdc.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
    #endregion
}
