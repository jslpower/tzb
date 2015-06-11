using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BMallOrders
    {
        /// <summary>
        /// 根据订单ID填写物流编号
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static bool UpdateLogisticsState(string orderid, string wuliuID)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MallOrder.FirstOrDefault(n => n.OrderId == orderid && n.PayType == ((int)Model.商城订单状态.已支付));
                if (m != null)
                {
                    m.LogisticsId = wuliuID;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 根据订单ID修改支付状态
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static bool UpdatePayState(string orderid)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MallOrder.FirstOrDefault(n => n.OrderId == orderid);
                if (m != null)
                {
                    m.PayStatus = (int)Model.商城订单状态.已支付;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        public static bool UpdateState(string orderId, int UserId, string UserName,Enow.TZB.Model.商城订单状态 State)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MallOrder.FirstOrDefault(n => n.OrderId == orderId);
                if (m != null)
                {
                    switch (State)
                    {

                        case Model.商城订单状态.未支付:
                            m.CheckId = UserId;
                            m.CheckName = UserName;
                            m.ReviewTime = DateTime.Now;
                            m.PayStatus= (int)State;
                           
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
                                MsgTitle = string.Format(CacheSysMsg.GetMsg(166), m.OrderNo),
                                MsgInfo = string.Format(CacheSysMsg.GetMsg(167), m.OrderNo),
                                IsRead = false,
                                IssueTime = DateTime.Now
                            });
                            #endregion
                            break;
                        case Model.商城订单状态.退款:
                            m.CheckId = UserId;
                            m.CheckName = UserName;
                            m.ReviewTime = DateTime.Now;
                            m.PayStatus = (int)State;

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
                                MsgTitle = string.Format(CacheSysMsg.GetMsg(166), m.OrderNo),
                                MsgInfo = string.Format(CacheSysMsg.GetMsg(167), m.OrderNo),
                                IsRead = false,
                                IssueTime = DateTime.Now
                            });
                            #endregion
                            break;
                        case Model.商城订单状态.审核无效:
                            m.CheckId = UserId;
                            m.CheckName = UserName;
                            m.ReviewTime = DateTime.Now;
                            m.PayStatus = (int)State;
                         
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
                                MsgTitle = string.Format("您提交的订单{0}无效！", m.OrderNo),
                                MsgInfo = string.Format("您提交的订单{0}审核未通过，无效！",m.OrderNo),
                                IsRead = false,
                                IssueTime = DateTime.Now
                            });
                            #endregion
                            break;
                        case Model.商城订单状态.已发货:

                            m.PayStatus = (int)State;
                         
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
                                MsgTitle = string.Format("您提交的订单{0}已发货！", m.OrderNo),
                                MsgInfo = string.Format("您提交的订单{0}已发货！", m.OrderNo),
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
        /// 修改订单下所有商品的销售信息
        /// </summary>
        /// <param name="model"></param>
        public static void UpdateGoodsStockNum(string orderid)
        {
            using (FWDC rdc = new FWDC())
            {
                //商品编号集合
                 var charmodel = (from tmp in rdc.tbl_ShoppingChart where tmp.OrderId == orderid select tmp).ToList();
                 if (charmodel.Count>0)
                 {
                     foreach (var model in charmodel)
                     {
                         BMallGoods.UpdateSellNum(new tbl_MallGoods
                         {
                             Id = model.GoodsId,
                             SellNum = model.ShoppingNum
                         }); 
                     }
                     
                 }
                    

            }
        }
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="model"></param>
        public static bool Add(tbl_MallOrder model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_MallOrder.InsertOnSubmit(model);
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
        /// 创建订单
        /// </summary>
        /// <param name="model"></param>
        public static tbl_MallOrder Getordermodel(string Usid,string OrderId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MallOrder.Where(w => w.MemberId == Usid && w.OrderId == OrderId).SingleOrDefault();
                if (m != null)
                {
                    return m;
                }
                return null;

            }
        }
        /// <summary>
        /// 根据义卖商品创建用户编号和订单编号查询订单详情
        /// </summary>
        /// <param name="model"></param>
        public static dt_BazaarOrderView GetBazaarOrderViewmodel(string Usid, string OrderId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.dt_BazaarOrderView.Where(w => w.GoodsMemberId == Usid && w.OrderId == OrderId).SingleOrDefault();
                if (m != null)
                {
                    return m;
                }
                return null;

            }
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <param name="MemberId">用户编号</param>
        public static bool Delmodel(string OrderId, string MemberId)
        {
            using (FWDC rdc = new FWDC())
            {
                var temp = rdc.tbl_MallOrder.Where(w => w.OrderId == OrderId && w.MemberId == MemberId).SingleOrDefault();
                if (temp != null)
                {
                    //rdc.tbl_MallOrder.DeleteOnSubmit(temp);
                    temp.IsDelete = true;
                    rdc.SubmitChanges();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取商品分类列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_MallOrder> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, MallorderQuery SearchModel)
        {
            string FieldsList = "*";
            string TableName = " tbl_MallOrder ";
            string OrderString = " ORDER BY CreatTime desc";
            string strWhere = "(1=1)";
            if(SearchModel.IsDelete.HasValue)
            {
                string IsValid = SearchModel.IsDelete.Value ? "1" : "0";
                strWhere = strWhere + " AND (IsDelete = " + IsValid + ")";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.MemberId))
            {
                strWhere = strWhere + " AND ( MemberId ='" + SearchModel.MemberId + "')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.OrderNo))
            {
                strWhere = strWhere + " AND ( OrderNo like '%" + SearchModel.OrderNo + "%')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.StartDate))
            {
                strWhere = strWhere + " AND ( CONVERT(varchar(100), CreatTime, 23) >= '" + SearchModel.StartDate + "')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.EndDate))
            {
                strWhere = strWhere + " AND ( CONVERT(varchar(100), CreatTime, 23) <= '" + SearchModel.EndDate + "')";
            }

            if (SearchModel.PayStatus>0)
            {
                strWhere = strWhere + " AND ( PayStatus =" + SearchModel.PayStatus + ")"; 
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_MallOrder> q = rdc.ExecuteQuery<tbl_MallOrder>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 用户义卖商品订单分页列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_BazaarOrderView> GetBazaarOrderList(ref int rowsCount, int intPageSize, int CurrencyPage, MallorderQuery SearchModel)
        {
            string FieldsList = "*";
            string TableName = " dt_BazaarOrderView ";
            string OrderString = " ORDER BY CreatTime desc";
            string strWhere = "(1=1)";
            //strWhere += " AND (IsDelete = 0)";
            if (!String.IsNullOrWhiteSpace(SearchModel.GoodsMemberId))
            {
                strWhere += " AND ( GoodsMemberId ='" + SearchModel.GoodsMemberId + "')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.OrderNo))
            {
                strWhere +=" AND ( OrderNo like '%" + SearchModel.OrderNo + "%')";
            }
            if (SearchModel.zffs>0)
            {
                strWhere += " AND ( PayType=" + SearchModel.zffs + ")";
            }
            if (SearchModel.PayStatus == ((int)Enow.TZB.Model.商城订单状态.已支付))
            {
                strWhere += " AND ( PayStatus=" + (int)Enow.TZB.Model.商城订单状态.已支付 + ")";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_BazaarOrderView> q = rdc.ExecuteQuery<dt_BazaarOrderView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
    }
}
