using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
    #region 充值消费
    /// <summary>
    /// 财务流水
    /// </summary>
    public class BMemberWallet
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_MemberWallet> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MWalletSearch SearchModel)
        {
            string FieldsList = "*";
            string TableName = "tbl_MemberWallet";
            string OrderString = " ORDER BY IssueTime desc";
            string strWhere = "(1=1)";
            #region 城市权限控制
            if (SearchModel.IsAllCity == false)
            {
                if (!String.IsNullOrWhiteSpace(SearchModel.CityLimitList))
                {
                    strWhere += " AND (AreaId IN (" + SearchModel.CityLimitList + "))";
                }
                else { strWhere += " AND (AreaId = 0)"; }
            }
            #endregion
            if (SearchModel.TypeId.HasValue)
            {
                strWhere = strWhere + " AND (TypeId = " + (int)SearchModel.TypeId.Value + ")";
            }
            if (SearchModel.IsPay.HasValue)
            {
                string IsPay = "0";
                if (SearchModel.IsPay.Value == Model.EnumType.付款状态.已付)
                    IsPay = "1";
                strWhere = strWhere + " AND (IsPayed = " + IsPay + ")";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.MemberId))
            {
                strWhere = strWhere + " AND (MemberId = '" + SearchModel.MemberId.Trim() + "')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.TradeNumber))
            {
                strWhere = strWhere + " AND (TradeNumber LIKE '%" + SearchModel.TradeNumber.Trim() + "%')";
            }
            if (SearchModel.BeginDate.HasValue)
            {
                strWhere += " and (IssueTime>='" + SearchModel.BeginDate + "')";
            }
            if (SearchModel.EndDate.HasValue)
            {
                strWhere += " and (IssueTime<='" + SearchModel.EndDate + "')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_MemberWallet> q = rdc.ExecuteQuery<tbl_MemberWallet>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_MemberWallet> GetViewList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MWalletViewSearch SearchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_MemberWallet";
            string OrderString = " ORDER BY IssueTime desc";
            string strWhere = "(1=1)";

            if (SearchModel.TypeId.HasValue)
            {
                strWhere = strWhere + " AND (TypeId = " + (int)SearchModel.TypeId.Value + ")";
            }
            else {
                if (SearchModel.PerClass.HasValue)
                {
                    switch (SearchModel.PerClass.Value)
                    {
                        case Model.EnumType.财务流水类型.充值:
                            strWhere = strWhere + " AND (TypeId IN (" + (int)Model.EnumType.财务流水类型.充值 + "," + (int)Model.EnumType.财务流水类型.代充 + "," + (int)Model.EnumType.财务流水类型.线下充值 + "," + (int)Model.EnumType.财务流水类型.转账收入 + "))";
                            break;
                        case Model.EnumType.财务流水类型.消费:
                            strWhere = strWhere + " AND (TypeId IN (" + (int)Model.EnumType.财务流水类型.消费 + "," + (int)Model.EnumType.财务流水类型.代消费 + "," + (int)Model.EnumType.财务流水类型.线下消费 + "," + (int)Model.EnumType.财务流水类型.转账支出+ "))";
                            break;
                        case Model.EnumType.财务流水类型.消费退款:
                            strWhere = strWhere + " AND (TypeId ="+(int)Model.EnumType.财务流水类型.消费退款+")";
                            break;
                        case Model.EnumType.财务流水类型.比赛保证金:
                            strWhere = strWhere + " AND (TypeId =" + (int)Model.EnumType.财务流水类型.保证金分配 + ")";
                            break;
                    }
                }
            }
            if (SearchModel.IsPay.HasValue)
            {
                string IsPay = "0";
                if (SearchModel.IsPay.Value == Model.EnumType.付款状态.已付)
                    IsPay = "1";
                strWhere = strWhere + " AND (IsPayed = " + IsPay + ")";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.MemberId))
            {
                strWhere = strWhere + " AND (MemberId = '" + SearchModel.MemberId.Trim() + "')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.TradeNumber))
            {
                strWhere = strWhere + " AND (TradeNumber LIKE '%" + SearchModel.TradeNumber.Trim() + "%')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.ContactName))
            {
                strWhere = strWhere + " AND (ContactName LIKE '%" + SearchModel.ContactName.Trim() + "%')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.NickName))
            {
                strWhere = strWhere + " AND (NickName LIKE '%" + SearchModel.NickName.Trim() + "%')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.MobilePhone))
            {
                strWhere = strWhere + " AND (MobilePhone LIKE '%" + SearchModel.MobilePhone.Trim() + "%')";
            }
            if (SearchModel.BeginDate.HasValue)
            {
                strWhere += " and (IssueTime>='" + SearchModel.BeginDate + "')";
            }
            if (SearchModel.EndDate.HasValue)
            {
                strWhere += " and (IssueTime<='" + SearchModel.EndDate + "')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_MemberWallet> q = rdc.ExecuteQuery<dt_MemberWallet>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        #region 球队保证金流水列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_MatchWallet> GetViewList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MTeamWalletViewSearch SearchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_MatchWallet";
            string OrderString = " ORDER BY IssueTime desc";
            string strWhere = "(IsPayed=1)";
            #region 城市权限控制
            if (SearchModel.IsAllCity == false)
            {
                if (!String.IsNullOrWhiteSpace(SearchModel.CityLimitList))
                {
                    strWhere += " AND (AreaId IN (" + SearchModel.CityLimitList + "))";
                }
                else { strWhere += " AND (AreaId = 0)"; }
            }
            #endregion
            if (!String.IsNullOrWhiteSpace(SearchModel.ContactName))
            {
                strWhere = strWhere + " AND (PayContactName LIKE '%" + StringValidate.CheckSql(SearchModel.ContactName.Trim()) + "%')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.TeamName))
            {
                strWhere = strWhere + " AND (TeamName LIKE '%" + StringValidate.CheckSql(SearchModel.TeamName.Trim()) + "%')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.MatchName))
            {
                strWhere = strWhere + " AND (MatchName LIKE '%" + StringValidate.CheckSql(SearchModel.MatchName.Trim()) + "%')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_MatchWallet> q = rdc.ExecuteQuery<dt_MatchWallet>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        #endregion
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public static void Add(tbl_MemberWallet model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_MemberWallet.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 取得实体
        /// </summary>
        /// <param name="TradeNumber"></param>
        /// <returns></returns>
        public static tbl_MemberWallet GetModelById(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberWallet.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 返回是否存在支付信息
        /// </summary>
        /// <param name="MatchTeamId"></param>
        /// <returns>存在 true 不存在 false</returns>
        public static tbl_MemberWallet GetExistsModel(string MatchTeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberWallet.FirstOrDefault(n => n.BindId == MatchTeamId);
                return model;
            }
        }
        /// <summary>
        /// 取得实体
        /// </summary>
        /// <param name="TradeNumber"></param>
        /// <returns></returns>
        public static tbl_MemberWallet GetModel(string TradeNumber)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberWallet.FirstOrDefault(n => n.TradeNumber == TradeNumber);
                return model;
            }
        }
        /// <summary>
        /// 更新流水号
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="NewTradeNumber"></param>
        /// <returns></returns>
        public static bool UpdateTradeNumber(string Id, string TradeNumber)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberWallet.FirstOrDefault(n => n.Id == Id && n.IsPayed=='0');
                if (model != null)
                {
                    model.TradeNumber = TradeNumber;
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
        /// 更新支付状态 为已支付
        /// </summary>
        /// <param name="Id"></param>
        public static bool UpdatePayStateByBindId(string BindId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberWallet.FirstOrDefault(n => n.BindId == BindId && n.IsPayed == '0');
                if (model != null)
                {
                    model.IsPayed = '1';
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
        /// 更新支付状态 为已支付
        /// </summary>
        /// <param name="Id"></param>
        public static bool UpdatePayState(string TradeNumber)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberWallet.FirstOrDefault(n => n.TradeNumber == TradeNumber && n.IsPayed == '0');
                if (model != null)
                {
                    string MsgTitle = "";
                    string MsgInfo = "";
                    model.IsPayed = '1';
                    rdc.SubmitChanges();
                    //更改个人总铁丝币
                    switch (model.TypeId)
                    {
                        case (int)Model.EnumType.财务流水类型.充值:
                        case (int)Model.EnumType.财务流水类型.代充:
                        case (int)Model.EnumType.财务流水类型.线下充值:
                            BMember.UpdateCurrencyNumber(model.MemberId, Model.EnumType.操作符号.加, model.TradeMoney);
                            MsgTitle = string.Format(CacheSysMsg.GetMsg(71), model.TradeMoney.ToString("F2"));
                            MsgInfo = string.Format(CacheSysMsg.GetMsg(72), model.TradeMoney.ToString("F2"));
                            break;
                        case (int)Model.EnumType.财务流水类型.比赛保证金:
                            //更改比赛支付状态
                            break;
                        case (int)Model.EnumType.财务流水类型.线下消费:
                            BMember.UpdateCurrencyNumber(model.MemberId, Model.EnumType.操作符号.减, model.TradeMoney);
                            MsgTitle = string.Format(CacheSysMsg.GetMsg(139), model.TradeMoney.ToString("F2"));
                        MsgInfo = string.Format(CacheSysMsg.GetMsg(141), model.TradeMoney.ToString("F2"));
                            break;
                        case (int)Model.EnumType.财务流水类型.消费退款:
                            BMember.UpdateCurrencyNumber(model.MemberId, Model.EnumType.操作符号.加, model.TradeMoney);
                             MsgTitle = string.Format(CacheSysMsg.GetMsg(140), model.TradeMoney.ToString("F2"));
                             MsgInfo = string.Format(CacheSysMsg.GetMsg(142), model.TradeMoney.ToString("F2"));
                            break;
                        case (int)Model.EnumType.财务流水类型.转账支出:
                            BMember.UpdateCurrencyNumber(model.MemberId, Model.EnumType.操作符号.减, model.TradeMoney);
                            MsgTitle = string.Format(CacheSysMsg.GetMsg(150));
                            MsgInfo = string.Format(CacheSysMsg.GetMsg(151), model.IssueTime.ToString("yyyy-MM-dd HH:mm:ss"), model.PayContactName, model.TradeMoney.ToString("F2"));
                            break;
                        case (int)Model.EnumType.财务流水类型.转账收入:
                            BMember.UpdateCurrencyNumber(model.MemberId, Model.EnumType.操作符号.加, model.TradeMoney);
                            MsgTitle = string.Format(CacheSysMsg.GetMsg(152));
                            MsgInfo = string.Format(CacheSysMsg.GetMsg(153), model.PayContactName, model.IssueTime.ToString("yyyy-MM-dd HH:mm:ss"), model.TradeMoney.ToString("F2"));
                            break;
                    }
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
                        MsgTitle = MsgTitle,
                        MsgInfo =MsgInfo,
                        IsRead = false,
                        IssueTime = DateTime.Now
                    });
                    #endregion
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #region 构造支付签名
        private const string _Key = "12$#@!#@5tr%u8wsfr543$,23ve7w%$#";
        private const string _IV = "!54~1)e74&%3+-q#";
        /// <summary>
        /// 支付签名
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static string Sign(string Id)
        {
            string rv = "";
            if (!String.IsNullOrWhiteSpace(Id))
            {

                /*
                HashCrypto CrypTo = new HashCrypto();
                CrypTo.Key = _Key;
                CrypTo.IV = _IV;
                rv = CrypTo.DESEncrypt(Id);
                CrypTo.Dispose();
                 * */
                rv = BSignCode.AddSignCode(Id);

            }
            return rv;
        }
        /// <summary>
        /// 签名解密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UnSign(string s)
        {
            string rv = "-1";
            if (!String.IsNullOrWhiteSpace(s))
            {
                try
                {
                    /*
                    HashCrypto CrypTo = new HashCrypto();
                    CrypTo.Key = _Key;
                    CrypTo.IV = _IV;
                    rv = CrypTo.DeDESEncrypt(s);
                    CrypTo.Dispose();
                     * */
                    rv = BSignCode.GetCodeId(s);
                }
                catch
                {
                    rv = "-1";
                }
            }
            return rv;
        }
        #endregion
    }    
    #endregion
    #region 支付密码
    /// <summary>
    /// 支付密码
    /// </summary>
    public class BMemeberPayPassword {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MemeberId"></param>
        /// <returns></returns>
        public static List<tbl_MemberPayPassword> GetList(string MemeberId)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.tbl_MemberPayPassword
                            where q.MemberId == MemeberId
                            orderby q.IssueTime descending
                            select q;
                return query.ToList();
            }
        }
        /// <summary>
        /// 是否有支付密码
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns>有：true 无:false</returns>
        public static bool IsExist(string MemberId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberPayPassword.FirstOrDefault(n => n.MemberId == MemberId && n.IsEnable == true);
                if (model != null)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public static void Add(tbl_MemberPayPassword model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_MemberPayPassword.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }

        /// <summary>
        /// 取得实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_MemberPayPassword GetModel(string MemberId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberPayPassword.FirstOrDefault(n => n.MemberId == MemberId && n.IsEnable == true);
                return model;
            }
        }
        /// <summary>
        /// 修改支付密码
        /// </summary>
        /// <param name="MemberId"></param>
        /// <param name="PayPassword"></param>
        /// <returns></returns>
        public static bool Update(string MemberId,string PayPassword)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MemberPayPassword.FirstOrDefault(n => n.MemberId == MemberId && n.IsEnable == true);
                if (model != null)
                {
                    model.IsEnable = false;
                    rdc.SubmitChanges();
                    Add(new tbl_MemberPayPassword
                    {
                        Id = System.Guid.NewGuid().ToString(),
                        MemberId = MemberId,
                        PayPassword = PayPassword,
                        IsEnable = true,
                        IssueTime = DateTime.Now
                    });
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
