using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.SMS;

namespace Enow.TZB.BLL
{
    #region 会员管理
    /// <summary>
    /// 会员管理
    /// </summary>
    public class BMember
    {
        /// <summary>
        /// 获取列表（带 舵主堂主标识）
        /// </summary>
        /// <returns></returns>
        public static List<dt_MembertdView> GetjobList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MMemberSearch searchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_MembertdView";
            string OrderString = " ORDER BY IssueTime Desc";
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
                strWhere += " AND ( AreaId=" + searchModel.AreaId + ")";
            }
            #region 城市权限控制
            if (searchModel.IsAllCity == false)
            {
                if (!String.IsNullOrWhiteSpace(searchModel.CityLimitList))
                {
                    strWhere += " AND ( AreaId IN (" + searchModel.CityLimitList + "))";
                }
                else { strWhere += " AND (AreaId = 0)"; }
            }
            #endregion
            if (!string.IsNullOrWhiteSpace(searchModel.MobileNo))
            {
                strWhere += " and (MobilePhone='" + searchModel.MobileNo + "' )";
            }
            if (!string.IsNullOrWhiteSpace(searchModel.NickName))
            {
                strWhere += " and (NickName='" + searchModel.NickName + "')";
            }
            if (searchModel.IssueBeginTime.HasValue)
            {
                strWhere += " and (IssueTime>='" + searchModel.IssueBeginTime + "')";
            }
            if (searchModel.IssueEndTime.HasValue)
            {
                strWhere += " and ( IssueTime<='" + searchModel.IssueEndTime + "')";
            }
            if (searchModel.CheckBeginTime.HasValue)
            {
                strWhere += " and (CheckTime>='" + searchModel.CheckBeginTime + "')";
            }
            if (searchModel.CheckEndTime.HasValue)
            {
                strWhere += " and (CheckTime<='" + searchModel.CheckEndTime + "')";
            }
            if (searchModel.State.HasValue)
            {
                strWhere = strWhere + " AND (State = " + (int)searchModel.State.Value + ")";
            }
            if (searchModel.ContractName != null)
            {
                strWhere = strWhere + " AND (ContactName like '%" + searchModel.ContractName.Trim() + "%')";
            }
            if (searchModel.IsJoinTeam.HasValue)
            {
                if (searchModel.IsJoinTeam == true)
                {
                    strWhere += " and  (Id in  (select MemberId from tbl_BallTeam))";
                }
                else
                {
                    strWhere += " and  (Id not in  (select MemberId from tbl_BallTeam))";
                }
            }
            if (searchModel.IsJoinMatch.HasValue)
            {
                if (searchModel.IsJoinMatch == true)
                {
                    strWhere += " and  (Id in  (select MemberId from dt_MatchTeamMember))";
                }
                else
                {
                    strWhere += " and  (Id not in  (select MemberId from dt_MatchTeamMember))";
                }
            }
            if (searchModel.IsHasPhoto.HasValue)
            {
                if (searchModel.IsHasPhoto == true)
                {
                    strWhere += " and ( HeadPhoto !='' and HeadPhoto is not null)";
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
                List<dt_MembertdView> q = rdc.ExecuteQuery<dt_MembertdView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_MemberView> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MMemberSearch searchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_MemberView";
            string OrderString = " ORDER BY IssueTime Desc";
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
                strWhere += " AND ( AreaId=" + searchModel.AreaId + ")";
            }
            #region 城市权限控制
            if (searchModel.IsAllCity == false) {
                if (!String.IsNullOrWhiteSpace(searchModel.CityLimitList))
                {
                    strWhere += " AND ( AreaId IN (" + searchModel.CityLimitList + "))";
                }
                else { strWhere += " AND (AreaId = 0)"; }
            }
            #endregion
            if (!string.IsNullOrWhiteSpace(searchModel.MobileNo))
            {
                strWhere += " and (MobilePhone='" + searchModel.MobileNo + "' )";
            }
            if (!string.IsNullOrWhiteSpace(searchModel.NickName))
            {
                strWhere += " and (NickName='" + searchModel.NickName + "')";
            }
            if (searchModel.IssueBeginTime.HasValue)
            {
                strWhere += " and (IssueTime>='" + searchModel.IssueBeginTime + "')";
            }
            if (searchModel.IssueEndTime.HasValue)
            {
                strWhere += " and ( IssueTime<='" + searchModel.IssueEndTime + "')";
            }
            if (searchModel.CheckBeginTime.HasValue)
            {
                strWhere += " and (CheckTime>='" + searchModel.CheckBeginTime + "')";
            }
            if (searchModel.CheckEndTime.HasValue)
            {
                strWhere += " and (CheckTime<='" + searchModel.CheckEndTime + "')";
            }
            if (searchModel.State.HasValue)
            {
                strWhere = strWhere + " AND (State = " + (int)searchModel.State.Value + ")";
            }
            if (searchModel.ContractName != null)
            {
                strWhere = strWhere + " AND (ContactName like '%" + searchModel.ContractName.Trim() + "%')";
            }
            if (searchModel.IsJoinTeam.HasValue)
            {
                if (searchModel.IsJoinTeam == true)
                {
                    strWhere += " and  (Id in  (select MemberId from tbl_BallTeam))";
                }
                else
                {
                    strWhere += " and  (Id not in  (select MemberId from tbl_BallTeam))";
                }
            }
            if (searchModel.IsJoinMatch.HasValue)
            {
                if (searchModel.IsJoinMatch == true)
                {
                    strWhere += " and  (Id in  (select MemberId from dt_MatchTeamMember))";
                }
                else
                {
                    strWhere += " and  (Id not in  (select MemberId from dt_MatchTeamMember))";
                }
            }
            if (searchModel.IsHasPhoto.HasValue)
            {
                if (searchModel.IsHasPhoto==true)
                {
                    strWhere += " and ( HeadPhoto !='' and HeadPhoto is not null)";
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
                List<dt_MemberView> q = rdc.ExecuteQuery<dt_MemberView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 取得实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_Member GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Member.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 根据身份证取得用户信息
        /// </summary>
        /// <param name="PesonalId"></param>
        /// <returns></returns>
        public static tbl_Member GetPesonalModel(string PesonalId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Member.FirstOrDefault(n => n.PersonalId == PesonalId);
                return model;
            }
        }
        /// <summary>
        /// 判断用户身份证号码是否重复
        /// </summary>
        /// <param name="MemberId">过滤的会员编号</param>
        /// <param name="PesonalId">身份证号码</param>
        /// <returns>存在同样的身份证返回 ture 不存在返回 false</returns>
        public static bool GetPesonalModel(string MemberId, string PesonalId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Member.FirstOrDefault(n => n.Id != MemberId && n.PersonalId == PesonalId);
                if (model != null)
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
        /// 根据手机号码取得用户实体
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public tbl_Member GetModelByPhone(string mobile)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.MobilePhone == mobile);
                return m;
            }
        }
        /// <summary>
        /// 根据OpenId取得会员实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_Member GetModelByOpenId(string OpenId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = BMemberWeiXin.GetModel(OpenId); ;
                if (model != null)
                {
                    var MemberModel = rdc.tbl_Member.FirstOrDefault(n => n.Id == model.MemberId);
                    if (MemberModel != null) { return MemberModel; } else { return null; }
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Add(tbl_Member model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_Member.InsertOnSubmit(model);
                rdc.SubmitChanges();
                #region 写入消息中心
                BMessage.Add(new tbl_Message
                {
                    Id = System.Guid.NewGuid().ToString(),
                    TypeId = (int)Model.EnumType.消息类型.系统消息,
                    SendId = "0",
                    SendName = "铁子帮",
                    SendTime = DateTime.Now,
                    ReceiveId = model.Id,
                    ReceiveName = model.ContactName,
                    MasterMsgId = "0",
                    MsgTitle = CacheSysMsg.GetMsg(105),
                    MsgInfo = CacheSysMsg.GetMsg(106),
                    IsRead = false,
                    IssueTime = DateTime.Now
                });
                #endregion
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_Member model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.CountryId = model.CountryId;
                    m.CountryName = model.CountryName;
                    m.ProvinceId = model.ProvinceId;
                    m.ProvinceName = model.ProvinceName;
                    m.CityId = model.CityId;
                    m.CityName = model.CityName;
                    m.AreaId = model.AreaId;
                    m.AreaName = model.AreaName;
                    if (!String.IsNullOrWhiteSpace(model.Password))
                        m.Password = model.Password;
                    m.MobilePhone = model.MobilePhone;
                    m.Email = model.Email;
                    m.ContactName = model.ContactName;
                    m.PersonalId = model.PersonalId;
                    m.Address = model.Address;
                    if (!string.IsNullOrWhiteSpace(model.MemberPhoto))
                    {
                        m.MemberPhoto = model.MemberPhoto;
                    }                   
                    m.LastModifyTime = DateTime.Now;
                    rdc.SubmitChanges();
                    BMemberWeiXin.UpdateNickNameAndPhoto(model.Id, model.NickName, model.MemberPhoto);
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public static bool ChangePassword(string memberId, string Password)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Id == memberId);
                if (m != null)
                {
                    m.Password = Password;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(string Id, Model.EnumType.会员状态 State)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    if (m.State == (int)Model.EnumType.会员状态.审核中)
                    {
                        #region 写入消息中心
                        BMessage.Add(new tbl_Message
                        {
                            Id = System.Guid.NewGuid().ToString(),
                            TypeId = (int)Model.EnumType.消息类型.系统消息,
                            SendId = "0",
                            SendName = "铁子帮",
                            SendTime = DateTime.Now,
                            ReceiveId = Id,
                            ReceiveName = m.ContactName,
                            MasterMsgId = "0",
                            MsgTitle = CacheSysMsg.GetMsg(107),
                            MsgInfo = CacheSysMsg.GetMsg(108),
                            IsRead = false,
                            IssueTime = DateTime.Now
                        });
                        #endregion
                    }
                    m.State = (int)State;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改会员职位
        /// </summary>
        /// <param name="Id">会员编号</param>
        /// <param name="jobstate">审批状态</param>
        /// <param name="State">职位信息</param>
        /// <param name="msgtitle">消息标题</param>
        /// <param name="msginfo">消息内容</param>
        /// <returns></returns>
        public static bool UpdateZw(string Id, int jobstate, Model.EnumType.JobType State, string msgtitle, string msginfo)
        {
            using (FWDC rdc = new FWDC())
            {
                
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    #region 写入消息中心
                    BMessage.Add(new tbl_Message
                    {
                        Id = System.Guid.NewGuid().ToString(),
                        TypeId = (int)Model.EnumType.消息类型.系统消息,
                        SendId = "0",
                        SendName = "铁子帮",
                        SendTime = DateTime.Now,
                        ReceiveId = Id,
                        ReceiveName = m.ContactName,
                        MasterMsgId = "0",
                        MsgTitle = msgtitle,
                        MsgInfo = msginfo,
                        IsRead = false,
                        IssueTime = DateTime.Now
                    });
                    #endregion
                    if (jobstate == 3 || jobstate == 4)//试用 正式
                    {
                        m.Jobtyoe = (int)State;
                    }
                    else
                    {
                        m.Jobtyoe = 0;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 判断手机号码是否已存在
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public bool IsExistsPhone(string mobile)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.MobilePhone == mobile);
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
        /// 判断用户名是否已存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool IsExistsName(string UserName)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.UserName == UserName);
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
        /// 判断用户ID是否存在
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool isExistsId(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Id == Id);
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
        /// 更新会员头衔
        /// </summary>
        /// <param name="Id">编号</param>
        /// <param name="Title">头衔</param>
        /// <returns></returns>
        public static bool UpdateTitle(string Id, string Title)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    m.Title = Title;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 更新会员积分
        /// </summary>
        /// <param name="Id">编号</param>
        /// <param name="IntegrationNumber">积分</param>
        /// <returns></returns>
        public static bool UpdateIntegrationNumber(string Id, Model.EnumType.操作符号 Operation, int IntegrationNumber)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    switch (Operation)
                    {
                        case Model.EnumType.操作符号.加:
                            m.IntegrationNumber = m.IntegrationNumber + IntegrationNumber;
                            rdc.SubmitChanges();
                            break;
                        case Model.EnumType.操作符号.减:
                            m.IntegrationNumber = m.IntegrationNumber - IntegrationNumber;
                            rdc.SubmitChanges();
                            break;
                    }
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改微信 OpenId
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public static bool UpdateWxOpenId(string Id, string OpenId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    if (!String.IsNullOrWhiteSpace(OpenId))
                    {
                        m.OpenId = OpenId;
                        rdc.SubmitChanges();
                    }
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改头像 昵称
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="NickName"></param>
        /// <param name="MemberPhoto"></param>
        /// <returns></returns>
        public static bool UpdateNickNameAndPhoto(string Id,string NickName,string MemberPhoto)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    if (!String.IsNullOrWhiteSpace(NickName))
                        m.NickName = NickName;
                    if (!String.IsNullOrWhiteSpace(MemberPhoto))
                        m.MemberPhoto = MemberPhoto;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 更新会员铁丝币
        /// </summary>
        /// <param name="Id">编号</param>
        /// <param name="CurrencyNumber">铁丝币</param>
        /// <returns></returns>
        public static bool UpdateCurrencyNumber(string Id, Model.EnumType.操作符号 Operation, decimal CurrencyNumber)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    switch (Operation)
                    {
                        case Model.EnumType.操作符号.加:
                            m.CurrencyNumber = m.CurrencyNumber + CurrencyNumber;
                            rdc.SubmitChanges();
                            break;
                        case Model.EnumType.操作符号.减:
                            m.CurrencyNumber = m.CurrencyNumber - CurrencyNumber;
                            rdc.SubmitChanges();
                            break;
                    }
                    return true;
                }
                else { return false; }
            }
        }


        /// <summary>
        /// 抓取微信信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool CrawWxData(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                bool ReturnVal = false;
                string[] IdList = Id.Split(',');
                foreach (var strId in IdList)
                {
                    var m = rdc.tbl_MemberWeiXin.FirstOrDefault(n => n.MemberId == strId);
                    if (m != null)
                    {
                        string OpenId = m.OpenId;
                        if (!String.IsNullOrWhiteSpace(OpenId))
                        {
                            //取得微信用户信息
                            var WeiXinModel = BWeiXin.GetUserInfo(OpenId);
                            if (WeiXinModel != null)
                            {
                                string NickName = WeiXinModel.NickName;
                                string HeadPhoto = WeiXinModel.HeadImgUrl.Replace("\\/", "/");
                                BMemberWeiXin.Update(new tbl_MemberWeiXin
                                {
                                    Id = m.Id,
                                    MemberId = strId,
                                    OpenId = OpenId,
                                    NickName = NickName,
                                    HeadPhoto = HeadPhoto,
                                    IssueTime = DateTime.Now
                                });
                                //修改用户表的头像及昵称
                                BMember.UpdateNickNameAndPhoto(strId, NickName, HeadPhoto);
                                ReturnVal = true;
                            }
                            else
                            {
                                ReturnVal = false;
                            }
                        }
                        else
                        {
                            ReturnVal = false;
                        }
                    }
                    else
                    {
                        ReturnVal = false;
                    }
                }
                return ReturnVal;
            }
        }
        /// <summary>
        /// 状态审核
        /// </summary>
        /// <param name="Id">编号</param>
        /// <param name="IsValid">审核状态</param>
        /// <returns></returns>
        public static bool UpdateState(string Id, Model.EnumType.会员状态 State)
        {
            using (FWDC rdc = new FWDC())
            {
                /*
                int count = rdc.ExecuteCommand(@"UPDATE tbl_Member SET State = " + (int)State + " where Id in (" + Id + ")");
                return count > 0 ? true : false;
                 */
                bool ReturnVal = false;
                string[] IdList = Id.Split(',');
                foreach (var strId in IdList)
                {
                    var m = rdc.tbl_Member.FirstOrDefault(n => n.Id == strId);
                    if (m != null)
                    {
                        switch (State)
                        {
                            case Model.EnumType.会员状态.审核中:
                                m.State = (int)State;
                                rdc.SubmitChanges();
                                ReturnVal = true;
                                break;
                            case Model.EnumType.会员状态.通过:
                                m.State = (int)State;
                                rdc.SubmitChanges();
                                if (m.CountryId == 1 && m.ProvinceId != 190 && m.ProvinceId != 191 && m.ProvinceId != 988)
                                {
                                    //发送短信
                                    BSMS.Send(m.MobilePhone, CacheSysMsg.GetMsg(27));
                                }
                                //发送客服消息
                                #region 写入消息中心
                                BMessage.Add(new tbl_Message
                                {
                                    Id = System.Guid.NewGuid().ToString(),
                                    TypeId = (int)Model.EnumType.消息类型.系统消息,
                                    SendId = "0",
                                    SendName = "铁子帮",
                                    SendTime = DateTime.Now,
                                    ReceiveId = m.Id,
                                    ReceiveName = m.ContactName,
                                    MasterMsgId = "0",
                                    MsgTitle = CacheSysMsg.GetMsg(109),
                                    MsgInfo = CacheSysMsg.GetMsg(110),
                                    IsRead = false,
                                    IssueTime = DateTime.Now
                                });
                                #endregion
                                ReturnVal = true;
                                break;
                            case Model.EnumType.会员状态.拒绝:
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
                                    ReceiveId = m.Id,
                                    ReceiveName = m.ContactName,
                                    MasterMsgId = "0",
                                    MsgTitle = CacheSysMsg.GetMsg(111),
                                    MsgInfo = CacheSysMsg.GetMsg(112),
                                    IsRead = false,
                                    IssueTime = DateTime.Now
                                });
                                #endregion
                                ReturnVal = true;
                                break;
                        }
                    }
                    else { ReturnVal = false; }
                }
                return ReturnVal;
            }
        }
        #region 根据用户状态返回信息
        /// <summary>
        /// 认证状态检测
        /// </summary>
        public static void StateCheck(Model.EnumType.会员状态 State)
        {
            StateCheck(State, "/WX/Member/Default.aspx");
        }
        /// <summary>
        /// 认证状态检测
        /// </summary>
        public static void StateCheck(Model.EnumType.会员状态 State,string Url)
        {
            string Msg = "";
            switch (State)
            {
                case Model.EnumType.会员状态.审核中:
                    Msg = CacheSysMsg.GetMsg(36);
                    break;
                case Model.EnumType.会员状态.拒绝:
                    Msg = CacheSysMsg.GetMsg(44);
                    break;
            }
            if (!String.IsNullOrWhiteSpace(Msg))
            {
                Enow.TZB.Utility.MessageBox.ShowAndRedirect(Msg, Url);
                return;
            }
        }
        #endregion
    }
    #endregion
    #region 会员 微信信息管理
    public class BMemberWeiXin
    {
        /// <summary>
        /// 判断用户ID是否已绑定微信
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns>存在:true 不存在:false</returns>
        public static bool IsExists(string MemberId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MemberWeiXin.FirstOrDefault(n => n.MemberId == MemberId);
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
        /// 取得实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_MemberWeiXin GetModel(string OpenId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberWeiXin.FirstOrDefault(n => n.OpenId == OpenId);
                return model;
            }
        }
        public static string  GetMemberHeadPhoto(string MemberId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberWeiXin.FirstOrDefault(n => n.MemberId == MemberId);
                return model.HeadPhoto ;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Add(tbl_MemberWeiXin model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_MemberWeiXin.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 修改微信绑定表，绑定微信OpenId
        /// </summary>
        /// <param name="MemberId"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public static bool UpdateOpenId(string MemberId,string OpenId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MemberWeiXin.FirstOrDefault(n => n.MemberId == MemberId);
                if (m != null)
                {
                    m.OpenId = OpenId;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_MemberWeiXin model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MemberWeiXin.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    if (!String.IsNullOrWhiteSpace(model.NickName))
                        m.NickName = model.NickName;
                    if (!String.IsNullOrWhiteSpace(model.HeadPhoto))
                        m.HeadPhoto = model.HeadPhoto;
                    m.IssueTime = model.IssueTime;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改头像 昵称
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="NickName"></param>
        /// <param name="MemberPhoto"></param>
        /// <returns></returns>
        public static bool UpdateNickNameAndPhoto(string Id, String NickName, string MemberPhoto)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MemberWeiXin.FirstOrDefault(n => n.MemberId == Id);
                if (m != null)
                {
                    if (!String.IsNullOrWhiteSpace(NickName))
                        m.NickName = NickName;
                    if (!String.IsNullOrWhiteSpace(MemberPhoto))
                        m.HeadPhoto = MemberPhoto;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
    }
    #endregion
    #region 会员兴趣管理
    /// <summary>
    /// 会员兴趣管理
    /// </summary>
    public class BMemberInterest
    {
        /// <summary>
        /// 取得会员的兴趣列表
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public static List<tbl_MemberInterest> GetList(string MemberId)
        {
            using (FWDC rdc = new FWDC())
            {
                var query =
                    from q in rdc.tbl_MemberInterest
                    where q.MemberId == MemberId
                    orderby q.State ascending, q.IssueTime ascending
                    select q;
                return query.ToList();
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void Add(tbl_MemberInterest model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_MemberInterest.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 取得实体
        /// </summary>
        /// <param name="MemberId">会员编号</param>
        /// <returns></returns>
        public static tbl_MemberInterest GetModel(string MemberId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberInterest.FirstOrDefault(n => n.MemberId == MemberId && n.State == (int)Model.EnumType.归档状态.在用);
                return model;
            }
        }
        /// <summary>
        /// 修改兴趣
        /// </summary>
        /// <param name="Model"></param>
        public static void Update(tbl_MemberInterest Model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.esp_MemberInterest_Update(Model.MemberId, Model.CSWZ, Model.CYQYH, Model.CYZBPP, Model.MZTQS, Model.GZQD);
            }
        }
    }
    #endregion
    #region 会员 积分记录管理
    public class BMemberIntegration
    {

        /// <summary>
        /// 新增会员积分记录
        /// </summary>
        /// <param name="model">会员积分表实体</param>
        /// <returns>true：操作成功，false：操作失败</returns>
        public static bool Add(tbl_MemberIntegration model)
        {
            using (FWDC rdc=new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_MemberIntegration.InsertOnSubmit(model);
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
    #region 会员 荣誉记录管理

    /// <summary>
    /// 会员荣誉管理
    /// </summary>
    public class BMemberHonor
    {
        /// <summary>
        /// 新增荣誉积分记录
        /// </summary>
        /// <param name="model">会员荣誉表实体</param>
        /// <returns>true：操作成功，false：操作失败</returns>
        public static bool Add(tbl_MemberHonor model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_MemberHonor.InsertOnSubmit(model);
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
        /// 根据荣誉取得会员头衔
        /// </summary>
        /// <param name="HonorNumber"></param>
        /// <returns></returns>
        public static Model.EnumType.头衔 GetTitleEnum(int HonorNumber)
        {
            Model.EnumType.头衔 title = Model.EnumType.头衔.铁矿石;
            if (HonorNumber >= 50 && HonorNumber < 200)
            {
                title = Model.EnumType.头衔.生铁;
            }
            if (HonorNumber >= 200 && HonorNumber < 500)
            {
                title = Model.EnumType.头衔.熟铁;
            }
            if (HonorNumber >= 500 && HonorNumber < 1000)
            {
                title = Model.EnumType.头衔.钢铁;
            }
            if (HonorNumber >= 1000 && HonorNumber < 2000)
            {
                title = Model.EnumType.头衔.钨铁;
            }
            if (HonorNumber >= 2000 && HonorNumber < 5000)
            {
                title = Model.EnumType.头衔.合金铁;
            }
            if (HonorNumber >= 5000)
            {
                title = Model.EnumType.头衔.超合金铁;
            }
            //区域中月增加个人荣誉值最者取代前获取头衔人 = 外星铁
            return title;
        }
        /// <summary>
        /// 根据荣誉取得会员头衔
        /// </summary>
        /// <param name="HonorNumber"></param>
        /// <returns></returns>
        public static string GetTitle(int HonorNumber)
        {
            //string title = "头衔：<span><img src=\"/WX/images/touxian/tx_01.png\"/></span>";
            string title = "头衔：<img src=\"/WX/images/touxian/tx_01.png\"/>";
            if (HonorNumber >= 50 && HonorNumber < 200)
            {
                //title = "头衔：<span><img src=\"/WX/images/touxian/tx_02.png\"/></span>";
                title = "头衔：<img src=\"/WX/images/touxian/tx_02.png\"/>";
            }
            if (HonorNumber >= 200 && HonorNumber < 500)
            {
                //title = "头衔：<span><img src=\"/WX/images/touxian/tx_03.png\"/></span>";
                title = "头衔：<img src=\"/WX/images/touxian/tx_03.png\"/>";
            }
            if (HonorNumber >= 500 && HonorNumber < 1000)
            {
                //title = "头衔：<span><img src=\"/WX/images/touxian/tx_04.png\"/></span>";
                title = "头衔：<img src=\"/WX/images/touxian/tx_04.png\"/>";
            }
            if (HonorNumber >= 1000 && HonorNumber < 2000)
            {
                //title = "头衔：<span><img src=\"/WX/images/touxian/tx_05.png\"/></span>";
                title = "头衔：<img src=\"/WX/images/touxian/tx_05.png\"/>";
            }
            if (HonorNumber >= 2000 && HonorNumber < 5000)
            {
                //title = "头衔：<span><img src=\"/WX/images/touxian/tx_06.png\"/></span>";
                title = "头衔：<img src=\"/WX/images/touxian/tx_06.png\"/>";
            }
            if (HonorNumber >= 5000)
            {
                //title = "头衔：<span><img src=\"/WX/images/touxian/tx_07.png\"/></span>";
                title = "头衔：<img src=\"/WX/images/touxian/tx_07.png\"/>";
            }
            //区域中月增加个人荣誉值最者取代前获取头衔人 = 外星铁
            return title;
        }
        /// <summary>
        /// 更新会员荣誉
        /// </summary>
        /// <param name="Id">编号</param>
        /// <param name="HonorNumber">荣誉</param>
        /// <returns></returns>
        public static bool UpdateHonorNumber(string Id, Model.EnumType.操作符号 Operation, int HonorNumber)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Member.FirstOrDefault(n => n.Id == Id);
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
    }
    #endregion
}
