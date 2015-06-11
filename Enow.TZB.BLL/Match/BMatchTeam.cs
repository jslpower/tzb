using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.SMS;

namespace Enow.TZB.BLL
{
    #region 参赛球队
    /// <summary>
    /// 参赛球队
    /// </summary>
    public class BMatchTeam
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_MatchTeam> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MMatchTeamSearch searchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_MatchTeam";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(1=1)";
            if (!string.IsNullOrEmpty(searchModel.MatchName))
            {
                strWhere = strWhere + " AND (MatchName like '%" + searchModel.MatchName + "%')";
            }
            if (!string.IsNullOrEmpty(searchModel.TeamName))
            {
                strWhere = strWhere + " AND (TeamName like '%" + searchModel.TeamName + "%')";
            }
            if (searchModel.TypeId.HasValue)
            {
                strWhere += " and (TypeId=" + (int)searchModel.TypeId + ")";
            }
            if (searchModel.State.HasValue)
            {
                strWhere += " and (State=" + (int)searchModel.State.Value + ")";
            }
            if (searchModel.StartDate.HasValue)
            {
                strWhere += " and IssueTime>='" + searchModel.StartDate + "'";
            }
            if (searchModel.EndDate.HasValue)
            {
                strWhere += " and IssueTime<='" + searchModel.EndDate + "'";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_MatchTeam> q = rdc.ExecuteQuery<dt_MatchTeam>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_MatchTeam> GetList(string IdList)
        {
            string FieldsList = "Id,MatchId,TeamId,TeamName,TeamOwner,DepositMoney,DepositOverage,State";
            string TableName = "tbl_MatchTeam";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(State > "+ (int)Model.EnumType.参赛审核状态.报名费确认中 +")";
            if (!string.IsNullOrEmpty(IdList))
            {
                strWhere = strWhere + " AND (ID IN (" + IdList + "))";
            }
            using (FWDC rdc = new FWDC())
            {
                List<dt_MatchTeam> q = rdc.ExecuteQuery<dt_MatchTeam>(@"SELECT " + FieldsList + " FROM " + TableName + " WHERE " + strWhere + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 取得参赛球队列表
        /// </summary>
        /// <param name="MatchId">赛事ID</param>
        /// <param name="State">参赛状态</param>
        /// <returns></returns>
        public static List<dt_MatchTeamInfo> GetMatchTeamList(string MatchId)
        {

            using (FWDC rdc=new FWDC())
            {
                var query = from q in rdc.dt_MatchTeamInfo
                            where q.MatchId == MatchId && q.State > (int)Model.EnumType.参赛审核状态.报名费确认中
                            select q;
                return query.ToList();

                        
            }
        }
        /// <summary>
        /// 判断是否参赛
        /// </summary>
        /// <param name="MatchId">赛事编号</param>
        /// <param name="TeamId">队伍编号</param>
        /// <returns>已参加 true 未参加 false</returns>
        public static bool IsExists(string MatchId, string TeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MatchTeam.FirstOrDefault(n => n.MatchId == MatchId && n.TeamId == TeamId);
                if (m != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 取得信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static dt_MatchTeam GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_MatchTeam.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }

        /// <summary>
        /// 取得参赛球队的实体信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_MatchTeam GetTeamModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MatchTeam.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 根据赛事编号和球队编号获取比赛信息
        /// </summary>
        /// <param name="matchid"></param>
        /// <param name="teamid"></param>
        /// <returns></returns>
        public static dt_MatchTeam GetModel(string matchid,string teamid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_MatchTeam.FirstOrDefault(n => n.MatchId == matchid && n.TeamId == teamid);
                return model;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Add(tbl_MatchTeam model)
        {
            using (FWDC rdc = new FWDC())
            {
                var MatchModel = BMatch.GetModel(model.MatchId);
                if (MatchModel != null)
                {
                    string MatchName = MatchModel.MatchName;
                    MatchModel = null;
                    var TeamModel = BTeam.GetModel(model.TeamId);
                    if (TeamModel != null)
                    {
                        rdc.tbl_MatchTeam.InsertOnSubmit(model);
                        rdc.SubmitChanges();
                        #region 写入消息中心
                        //写入会员消息
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
                            MsgTitle = string.Format(CacheSysMsg.GetMsg(69), MatchName),
                            MsgInfo = string.Format(CacheSysMsg.GetMsg(70), MatchName),
                            IsRead = false,
                            IssueTime = DateTime.Now
                        });
                        #endregion
                    }
                }
            }
        }
        
        /// <summary>
        /// 报名球队状态审核
        /// </summary>
        /// <param name="Id">参赛球队ID</param>
        /// <param name="State">审核状态</param>
        /// <param name="UserId">审核人ID</param>
        /// <param name="UserName">审核人姓名</param>
        /// <param name="OrdinalNumber">队伍序数</param>
        /// <returns></returns>
        public static bool UpdateValid(string Id, Model.EnumType.参赛审核状态 State, int UserId, string UserName, int OrdinalNumber)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MatchTeam.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    switch (State)
                    {
                        case Model.EnumType.参赛审核状态.资格审核通过:
                            m.State = (int)State;
                            m.CheckId = UserId;
                            m.CheckName = UserName;
                            m.LastCheckTime = DateTime.Now;
                            m.OrdinalNumber = OrdinalNumber;
                            rdc.SubmitChanges();
                            
                            #region 写入消息中心
                            //取得球队队长信息
                            var tm = BTeamMember.GetModelByTeamId(m.TeamId);
                            if (tm != null)
                            {
                                //发送短信
                                Enow.TZB.SMS.SMSClass.Send(tm.MobilePhone, string.Format(CacheSysMsg.GetMsg(117), m.TeamName));
                                //写消息中心
                                BMessage.Add(new tbl_Message
                                {
                                    Id = System.Guid.NewGuid().ToString(),
                                    TypeId = (int)Model.EnumType.消息类型.系统消息,
                                    SendId = "0",
                                    SendName = "铁子帮",
                                    SendTime = DateTime.Now,
                                    ReceiveId = tm.MemberId,
                                    ReceiveName = tm.ContactName,
                                    MasterMsgId = "0",
                                    MsgTitle = string.Format(CacheSysMsg.GetMsg(116), m.TeamName),
                                    MsgInfo = string.Format(CacheSysMsg.GetMsg(117), m.TeamName),
                                    IsRead = false,
                                    IssueTime = DateTime.Now
                                });
                            }
                            #endregion
                            break;
                        case Model.EnumType.参赛审核状态.资格审核拒绝:
                            m.State = (int)State;
                            m.CheckId = UserId;
                            m.CheckName = UserName;
                            m.LastCheckTime = DateTime.Now;
                            rdc.SubmitChanges();
                            #region 写入消息中心
                            //取得球队队长信息
                            var tma = BTeamMember.GetModelByTeamId(m.TeamId);
                            if (tma != null)
                            {
                                BMessage.Add(new tbl_Message
                                {
                                    Id = System.Guid.NewGuid().ToString(),
                                    TypeId = (int)Model.EnumType.消息类型.系统消息,
                                    SendId = "0",
                                    SendName = "铁子帮",
                                    SendTime = DateTime.Now,
                                    ReceiveId = tma.MemberId,
                                    ReceiveName = tma.ContactName,
                                    MasterMsgId = "0",
                                    MsgTitle = string.Format(CacheSysMsg.GetMsg(118), m.TeamName),
                                    MsgInfo = string.Format(CacheSysMsg.GetMsg(119), m.TeamName),
                                    IsRead = false,
                                    IssueTime = DateTime.Now
                                });
                            }
                            #endregion
                            break;
                        case Model.EnumType.参赛审核状态.报名费确认中:
                            m.State = (int)State;
                            rdc.SubmitChanges();
                           
                            #region 写入消息中心
                            /*
                            //取得球队队长信息
                            var tma3 = BTeamMember.GetModelByTeamId(m.TeamId);
                            if (tma3 != null)
                            {
                                var MatchModel = BMatch.GetModel(m.MatchId);
                                if (MatchModel != null)
                                {
                                    BMessage.Add(new tbl_Message
                                    {
                                        Id = System.Guid.NewGuid().ToString(),
                                        TypeId = (int)Model.EnumType.消息类型.系统消息,
                                        SendId = "0",
                                        SendName = "铁子帮",
                                        SendTime = DateTime.Now,
                                        ReceiveId = tma3.MemberId,
                                        ReceiveName = tma3.ContactName,
                                        MasterMsgId = "0",
                                        MsgTitle = string.Format(CacheSysMsg.GetMsg(126), m.TeamName, (MatchModel.RegistrationFee + MatchModel.EarnestMoney).ToString("F2")),
                                        MsgInfo = string.Format(CacheSysMsg.GetMsg(127), m.TeamName, (MatchModel.RegistrationFee + MatchModel.EarnestMoney).ToString("F2")),
                                        IsRead = false,
                                        IssueTime = DateTime.Now
                                    });
                                }
                            }
                             * */
                            #endregion

                            break;
                        case Model.EnumType.参赛审核状态.已获参赛权:
                            m.State = (int)State;
                            rdc.SubmitChanges();
                            #region 写入消息中心
                            //取得球队队长信息
                            var tma2 = BTeamMember.GetModelByTeamId(m.TeamId);
                            if (tma2 != null)
                            {                                
                                //写消息中心
                                var MatchModel = BMatch.GetModel(m.MatchId);
                                if (MatchModel != null)
                                {
                                    //发送短信
                                    Enow.TZB.SMS.SMSClass.Send(tma2.MobilePhone, string.Format(CacheSysMsg.GetMsg(123), m.TeamName, (MatchModel.RegistrationFee + MatchModel.EarnestMoney).ToString("F2")));
                                    //发送短信结束
                                    BMessage.Add(new tbl_Message
                                    {
                                        Id = System.Guid.NewGuid().ToString(),
                                        TypeId = (int)Model.EnumType.消息类型.系统消息,
                                        SendId = "0",
                                        SendName = "铁子帮",
                                        SendTime = DateTime.Now,
                                        ReceiveId = tma2.MemberId,
                                        ReceiveName = tma2.ContactName,
                                        MasterMsgId = "0",
                                        MsgTitle = string.Format(CacheSysMsg.GetMsg(122), m.TeamName, (MatchModel.RegistrationFee + MatchModel.EarnestMoney).ToString("F2")),
                                        MsgInfo = string.Format(CacheSysMsg.GetMsg(123), m.TeamName, (MatchModel.RegistrationFee+MatchModel.EarnestMoney).ToString("F2")),
                                        IsRead = false,
                                        IssueTime = DateTime.Now
                                    });
                                }
                            }
                            #endregion
                            break;
                        case Model.EnumType.参赛审核状态.资格审核中:
                        case Model.EnumType.参赛审核状态.已抽签:
                            m.State = (int)State;
                            rdc.SubmitChanges();                            
                            break;
                    }
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 更新参赛审核信息及人数
        /// </summary>
        /// <param name="Id">参赛编号</param>
        /// <param name="State">参赛状态</param>
        /// <param name="JoinNumer">人数</param>
        /// <param name="FieldId">球场编号</param>
        /// <returns></returns>
        public static bool Update(string Id, Model.EnumType.参赛审核状态 State,int JoinNumer,string FieldId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MatchTeam.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    var FieldModel = BBallField.GetModel(FieldId);
                    if (FieldModel != null)
                    {
                        m.MatchFieldId = FieldId;
                        m.FieldName = FieldModel.FieldName;
                        m.FieldAddress = FieldModel.Address;
                    }
                    m.JoinNumber = JoinNumer;
                    m.State = (int)State;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 更新保证金
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Operation"></param>
        /// <param name="DepositMoney"></param>
        /// <returns></returns>
        public static bool UpdateDepositMoney(string Id, Model.EnumType.操作符号 Operation, decimal DepositMoney)
        {
            using (FWDC rdc = new FWDC())
            {
                decimal DepositOverage = 0;
                var m = rdc.tbl_MatchTeam.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    switch (Operation)
                    {
                        case Model.EnumType.操作符号.加:
                            m.DepositOverage = m.DepositOverage - DepositMoney;
                            rdc.SubmitChanges();
                            break;
                        case Model.EnumType.操作符号.减:
                            DepositOverage = m.DepositMoney - m.DepositOverage-DepositMoney;//余额
                            m.DepositOverage = m.DepositOverage + DepositMoney;//扣除数累计
                            rdc.SubmitChanges();                            
                            #region 写入消息中心
                            //取得球队队长信息
                            var tma = BTeamMember.GetModelByTeamId(m.TeamId);
                            if (tma != null)
                            {                                
                                var MatchModel = BMatch.GetModel(m.MatchId);
                                if (MatchModel != null)
                                {
                                    #region 写流水
                                    //增加队长铁丝币
                                    BMember.UpdateCurrencyNumber(tma.MemberId, Model.EnumType.操作符号.加, DepositMoney);
                                    //写流水
                                    BMemberWallet.Add(new tbl_MemberWallet
                                    {
                                        Id = System.Guid.NewGuid().ToString(),
                                        TradeNumber = Id,
                                        UserId = 0,
                                        UserContactName = "",
                                        TypeId = (int)Model.EnumType.财务流水类型.保证金分配,
                                        MemberId = tma.MemberId,
                                        ContactName = tma.ContactName,
                                        TradeMoney = DepositMoney,
                                        IsPayed = '1',
                                        PayMemberId = tma.MemberId,
                                        PayContactName = tma.ContactName,
                                        Remark = string.Format(CacheSysMsg.GetMsg(136), MatchModel.MatchName, m.DepositMoney.ToString("F2"), DepositMoney.ToString("F2"), DepositOverage.ToString("F2")),
                                        BindId = Id,
                                        IssueTime = DateTime.Now
                                    });
                                    #endregion
                                    #region 写消息
                                    BMessage.Add(new tbl_Message
                                    {
                                        Id = System.Guid.NewGuid().ToString(),
                                        TypeId = (int)Model.EnumType.消息类型.系统消息,
                                        SendId = "0",
                                        SendName = "铁子帮",
                                        SendTime = DateTime.Now,
                                        ReceiveId = tma.MemberId,
                                        ReceiveName = tma.ContactName,
                                        MasterMsgId = "0",
                                        MsgTitle = CacheSysMsg.GetMsg(135),
                                        MsgInfo = string.Format(CacheSysMsg.GetMsg(136), MatchModel.MatchName, m.DepositMoney.ToString("F2"), DepositMoney.ToString("F2"), DepositOverage.ToString("F2")),
                                        IsRead = false,
                                        IssueTime = DateTime.Now
                                    });
                                    #endregion
                                    #region 发送短信
                                    var MemberModel = BMember.GetModel(tma.MemberId);
                                    if (MemberModel != null)
                                    {
                                        if (MemberModel.CountryId == 1 && MemberModel.ProvinceId != 190 && MemberModel.ProvinceId != 191 && MemberModel.ProvinceId != 988)
                                        {

                                            //发送短信
                                            BSMS.Send(MemberModel.MobilePhone, string.Format(CacheSysMsg.GetMsg(136), MatchModel.MatchName, m.DepositMoney.ToString("F2"), DepositMoney.ToString("F2"), DepositOverage.ToString("F2")));
                                        }
                                    }
                                    #endregion
                                }
                            }
                            #endregion
                            
                            break;
                    }
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 更新支付确认信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="PayType"></param>
        /// <param name="IsPayed"></param>
        /// <returns></returns>
        public static bool UpdatePayInfo(string Id, Model.EnumType.支付方式 PayType, bool IsPayed)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MatchTeam.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    switch (PayType)
                    {
                        case Model.EnumType.支付方式.线下支付:
                            m.PayType = (int)PayType;
                            m.IsPayed = IsPayed;
                            m.PayedTime = DateTime.Now;
                            rdc.SubmitChanges();
                            #region 写入消息中心
                            //取得球队队长信息
                            var tma1 = BTeamMember.GetModelByTeamId(m.TeamId);
                            if (tma1 != null)
                            {
                                var MatchModel = BMatch.GetModel(m.MatchId);
                                if (MatchModel != null)
                                {
                                    BMessage.Add(new tbl_Message
                                    {
                                        Id = System.Guid.NewGuid().ToString(),
                                        TypeId = (int)Model.EnumType.消息类型.系统消息,
                                        SendId = "0",
                                        SendName = "铁子帮",
                                        SendTime = DateTime.Now,
                                        ReceiveId = tma1.MemberId,
                                        ReceiveName = tma1.ContactName,
                                        MasterMsgId = "0",
                                        MsgTitle = string.Format(CacheSysMsg.GetMsg(120), m.TeamName, (MatchModel.RegistrationFee + MatchModel.EarnestMoney).ToString("F2")),
                                        MsgInfo = string.Format(CacheSysMsg.GetMsg(121), m.TeamName, (MatchModel.RegistrationFee + MatchModel.EarnestMoney).ToString("F2")),
                                        IsRead = false,
                                        IssueTime = DateTime.Now
                                    });
                                }
                            }
                            #endregion
                            break;
                        case Model.EnumType.支付方式.微信支付:
                        case Model.EnumType.支付方式.支付宝支付:
                            m.PayType = (int)PayType;
                            m.IsPayed = IsPayed;
                            m.PayedTime = DateTime.Now;
                            rdc.SubmitChanges();
                            #region 写入消息中心
                            //取得球队队长信息
                            var tma = BTeamMember.GetModelByTeamId(m.TeamId);
                            if (tma != null)
                            {
                                var MatchModel = BMatch.GetModel(m.MatchId);
                                if(MatchModel!=null){
                                BMessage.Add(new tbl_Message
                                {
                                    Id = System.Guid.NewGuid().ToString(),
                                    TypeId = (int)Model.EnumType.消息类型.系统消息,
                                    SendId = "0",
                                    SendName = "铁子帮",
                                    SendTime = DateTime.Now,
                                    ReceiveId = tma.MemberId,
                                    ReceiveName = tma.ContactName,
                                    MasterMsgId = "0",
                                    MsgTitle = string.Format(CacheSysMsg.GetMsg(120), m.TeamName, (MatchModel.RegistrationFee + MatchModel.EarnestMoney).ToString("F2")),
                                    MsgInfo = string.Format(CacheSysMsg.GetMsg(121), m.TeamName, (MatchModel.RegistrationFee + MatchModel.EarnestMoney).ToString("F2")),
                                    IsRead = false,
                                    IssueTime = DateTime.Now
                                });
                                }
                            }
                            #endregion
                            break;
                    }
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public static bool Delete(string id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MatchTeam.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    rdc.tbl_MatchTeam.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

    }
    #endregion
    #region 参赛球队成员
    /// <summary>
    /// 参赛球队
    /// </summary>
    public class BMatchTeamMember
    {
        /// <summary>
        /// 获取会员参加赛事列表
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="intPageSize"></param>
        /// <param name="CurrencyPage"></param>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public static List<dt_MemberMatch> GetList(ref int rowsCount, int intPageSize, int CurrencyPage,string MemberId)
        {
            string FieldsList = "*";
            string TableName = "dt_MemberMatch";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(MemberId='" + MemberId + "')";

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_MemberMatch> q = rdc.ExecuteQuery<dt_MemberMatch>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 根据参赛人员信息编号取得参赛信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static dt_MemberMatch GetMemberMatchModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_MemberMatch.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 查询是否参加了比赛
        /// </summary>
        /// <param name="UsId">用户ID</param>
        /// <param name="MatchId">赛事ID</param>
        /// <returns></returns>
        public static bool GetMemberMatchModelBool(string UsId, string MatchId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from tmp in rdc.dt_MatchTeamMember where tmp.MatchId == MatchId && tmp.MemberId == UsId && tmp.State == ((int)Enow.TZB.Model.EnumType.参赛审核状态.已获参赛权) select tmp).ToList();
                if (model.Count>0)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 获取参赛队伍成员列表
        /// </summary>
        /// <param name="MatchId"></param>
        /// <param name="TeamId"></param>
        /// <returns></returns>
        public static List<tbl_MatchTeamMember> GetList(string TeamId)
        {     
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.tbl_MatchTeamMember
                            where q.MatchTeamId == TeamId
                            orderby q.RoleType ascending, q.IssueTime ascending
                            select q;
                return query.ToList();
            }
        }        
        /// <summary>
        /// 批量添加参赛成员
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static void Add(List<tbl_MatchTeamMember> list)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_MatchTeamMember.InsertAllOnSubmit(list);
                rdc.SubmitChanges();                
            }
        }
        /// <summary>
        /// 根据参赛队伍编号删除
        /// </summary>
        /// <param name="TeamId">参赛队伍编号</param>
        /// <returns></returns>
        public static bool Delete(string TeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                var list = from n in rdc.tbl_MatchTeamMember
                           where n.MatchTeamId == TeamId
                           select n;
                if (list != null)
                {
                    rdc.tbl_MatchTeamMember.DeleteAllOnSubmit(list);
                    rdc.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 参赛球队列表
        /// </summary>
        /// <param name="MatchId"></param>
        /// <param name="TeamId"></param>
        /// <returns></returns>
        public static List<dt_MatchTeamMember> GetListByMatchId(string MatchId, string MatchTeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.dt_MatchTeamMember
                            where q.MatchTeamId == MatchTeamId && q.MatchId == MatchId
                            orderby q.RoleType ascending, q.OrdinalNumber ascending
                            select q;
                return query.ToList();
            }
        }
    }
 
    #endregion
}
