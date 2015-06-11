using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL.Order
{

    public class BOrder
    {

        /// <summary>
        /// 订单交易
        /// </summary>
        /// <param name="info">订单实体</param>

        /// <returns>1：操作成功，0：交易失败，-1：铁丝币余额不足</returns>
        public static int AddOrder(Model.MOrderInfo info)
        {
            using (FWDC rdc = new FWDC())
            {
                int num = GetOrderCount() + 1;
                info.OrderId = Guid.NewGuid().ToString();
                info.JiaoYiHao = DateTime.Now.ToString("yyyyMMdd") + num.ToString().PadLeft(5, '0');
                tbl_Order model = new tbl_Order();
                model.OrderId = info.OrderId;
                model.JiaoYiHao = info.JiaoYiHao;
                model.JinE = info.JinE;
                model.ZheKou = info.ZheKou;
                model.ActualMoney = info.ActualMoney;
                model.PayType = (int)info.PayType;
                model.PayStatus = (int)info.PayStatus;
                model.MemberId = info.MemberId;
                model.OperatorId = info.OperatorId;
                model.OperatorName = info.OperatorName;
                model.FieldId = info.FieldId;
                model.OperatorTime = DateTime.Now;
                //判断是否会员
                if (BMember.isExistsId(info.MemberId) && info.PayType == Model.收款类型.铁丝卡)
                {

                    var Mmodel = BMember.GetModel(info.MemberId);
                    model.CustomerName = Mmodel.ContactName;
                    //支付金额大于铁丝币，交易失败，返回-1；
                    if (Mmodel.CurrencyNumber < info.JinE)
                    {
                        return -1;
                    }
                    else
                    {

                        //当前消费铁丝币
                        info.CurrencyNumber = info.JinE;
                        //铁丝币余额
                        info.TotalCurrencyNumber = Mmodel.CurrencyNumber - info.JinE;
                        //计算本次积分，积分只取整数部分
                        info.Point = Convert.ToInt32(decimal.Truncate(info.ActualMoney).ToString()) / 10;
                        //总积分
                        info.TotalPoint = info.Point + Mmodel.IntegrationNumber;

                        if (info.Point > 0)
                        {
                            //更新会员消费积分
                            BMember.UpdateIntegrationNumber(Mmodel.Id, Model.EnumType.操作符号.加, info.Point);
                            #region 新增会员积分记录
                            BMemberIntegration.Add(new tbl_MemberIntegration
                            {
                                Id = Guid.NewGuid().ToString(),
                                TypeId = (int)Model.EnumType.积分类型.获取,
                                MemberId = Mmodel.Id,
                                UserName = Mmodel.UserName,
                                ContactName = Mmodel.ContactName,
                                OrderId = info.OrderId,
                                IntegrationNumber = info.Point,
                                Remark = string.Format(CacheSysMsg.GetMsg(143), info.JiaoYiHao, info.JinE, info.Point),
                                IssueTime = DateTime.Now
                            });
                            #endregion
                        }
                        //消费写入会员充值流水
                        string Id = Guid.NewGuid().ToString();
                        BMemberWallet.Add(new tbl_MemberWallet
                        {
                            Id = Id,
                            TradeNumber = info.JiaoYiHao,
                            UserId = info.OperatorId,
                            UserContactName = info.OperatorName,
                            TypeId = (int)Model.EnumType.财务流水类型.线下消费,
                            MemberId = info.MemberId,
                            ContactName = model.CustomerName,
                            TradeMoney = info.JinE,
                            IsPayed = '0',
                            PayMemberId = info.MemberId,
                            Remark = string.Format(CacheSysMsg.GetMsg(137), info.JiaoYiHao, info.CurrencyNumber, info.TotalCurrencyNumber),
                            PayContactName = info.CustomerName,
                            IssueTime = DateTime.Now
                        });
                        //更新支付状态，扣除铁丝币并且写入消息中心
                        BMemberWallet.UpdatePayState(info.JiaoYiHao);

                    }
                }
                else
                {
                    model.MemberId = "0";
                    model.CustomerName = "散客";
                }
                //写入订单表
                rdc.tbl_Order.InsertOnSubmit(model);
                rdc.SubmitChanges();

                //写入订单明细表
                if (info.Mingxi.Count > 0 && info.Mingxi != null)
                {
                    tbl_OrderMingXi Omodel = new tbl_OrderMingXi();
                    tbl_Goods Gmodel = new tbl_Goods();

                    foreach (var item in info.Mingxi)
                    {
                        Gmodel = BGoods.GetModel(item.GoodsId);
                        Omodel = new tbl_OrderMingXi();
                        Omodel.OrderId = info.OrderId;
                        Omodel.GoodsId = item.GoodsId;
                        Omodel.Amount = item.Amount;
                        Omodel.Price = item.Price;
                        Omodel.GoodsName = Gmodel.GoodsName;
                        Omodel.GoodUnit = Gmodel.Unit;
                        Omodel.TypeId = Gmodel.TypeId;
                        Omodel.CPrice = item.Cprice;
                        Omodel.JinE = item.JinE;
                        Omodel.IssueTime = DateTime.Now;
                        Omodel.State = (int)item.State;
                        rdc.tbl_OrderMingXi.InsertOnSubmit(Omodel);
                        rdc.SubmitChanges();
                        //修改库存数量
                        BGoods.UpdateStock(item.GoodsId, Model.库存操作符号.减, item.Amount);
                    }

                    return 1;

                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 会员充值
        /// </summary>
        /// <param name="Info">订单交易实体</param>
        /// <param name="CurrencyNumber">充值铁丝币</param>
        /// <param name="CurrencyCount">累计铁丝币</param>
        /// <returns>-1:会员不存在，0：操作失败，1：操作成功</returns>
        public static int MemberChongzhi(Model.MOrderInfo Info)
        {
            using (FWDC rdc = new FWDC())
            {

                int num = GetOrderCount() + 1;
                Info.OrderId = Guid.NewGuid().ToString();
                Info.JiaoYiHao = DateTime.Now.ToString("yyyyMMdd") + num.ToString().PadLeft(5, '0');
                tbl_Order model = new tbl_Order();
                model.OrderId = Info.OrderId;
                model.JiaoYiHao = Info.JiaoYiHao;
                model.JinE = Info.JinE;
                model.ZheKou = Info.ZheKou;
                model.ActualMoney = Info.ActualMoney;
                model.PayType = (int)Info.PayType;
                model.PayStatus = (int)Info.PayStatus;
                model.MemberId = Info.MemberId;
                model.OperatorId = Info.OperatorId;
                model.OperatorName = Info.OperatorName;
                model.FieldId = Info.FieldId;
                model.OperatorTime = DateTime.Now;
                BMember m = new BMember();
                if (m.IsExistsPhone(Info.Mobile))
                {
                    var Mmodel = m.GetModelByPhone(Info.Mobile);
                    model.MemberId = Mmodel.Id;
                    model.CustomerName = Mmodel.ContactName;
                    //消费写入会员充值流水
                    string Id = Guid.NewGuid().ToString();
                    BMemberWallet.Add(new tbl_MemberWallet
                    {
                        Id = Id,
                        TradeNumber = Info.JiaoYiHao,
                        UserId = Info.OperatorId,
                        UserContactName = Info.OperatorName,
                        TypeId = (int)Model.EnumType.财务流水类型.线下充值,
                        MemberId = Mmodel.Id,
                        ContactName = model.CustomerName,
                        TradeMoney = Info.JinE,
                        IsPayed = '0',
                        Remark = string.Format(CacheSysMsg.GetMsg(72), Info.JinE),
                        PayMemberId = Mmodel.Id,
                        PayContactName = model.CustomerName,
                        IssueTime = DateTime.Now
                    });
                    //更新支付状态，添加铁丝币并且写入消息中心
                    BMemberWallet.UpdatePayState(Info.JiaoYiHao);
                    //铁丝币
                    Info.CurrencyNumber = Info.JinE;
                    //铁丝币累计
                    Info.TotalCurrencyNumber = Mmodel.CurrencyNumber + Info.JinE;
                }
                else
                {
                    return -1;
                }
                //写入订单表
                rdc.tbl_Order.InsertOnSubmit(model);
                rdc.SubmitChanges();

                //写入订单明细表
                if (Info.Mingxi.Count > 0 && Info.Mingxi != null)
                {
                    tbl_OrderMingXi Omodel = new tbl_OrderMingXi();
                    tbl_Goods Gmodel = new tbl_Goods();
                    foreach (var item in Info.Mingxi)
                    {
                        Gmodel = BGoods.GetChongzhiCardModel(Info.FieldId);
                        Omodel = new tbl_OrderMingXi();
                        Omodel.OrderId = Info.OrderId;
                        Omodel.GoodsId = item.GoodsId;
                        Omodel.Amount = item.Amount;
                        Omodel.Price = item.Price;
                        Omodel.CPrice = item.Cprice;
                        Omodel.GoodsName = Gmodel.GoodsName;
                        Omodel.GoodUnit = Gmodel.Unit;
                        Omodel.TypeId = Gmodel.TypeId;
                        Omodel.JinE = item.JinE;
                        Omodel.IssueTime = DateTime.Now;
                        Omodel.State = (int)item.State;
                        rdc.tbl_OrderMingXi.InsertOnSubmit(Omodel);
                        rdc.SubmitChanges();
                    }

                    return 1;

                }
                else
                {
                    return 0;
                }
            }

        }
        /// <summary>
        /// 订单明细退款
        /// </summary>
        /// <param name="Ids">订单明细编号集字符串</param>
        /// <returns>1:处理成功 0:订单明细编号空值 -1:订单明细编号不正确 -2：找不到相关订单明细内容 -3:未找到相应订单内容 -4:未找到退款用户</returns>
        public static int OrderDetailRefund(string Ids, int OperatorId, string ContactName, ref List<Model.MRefundResponseInfo> ResponseInfo)
        {
            using (FWDC rdc = new FWDC())
            {
                int rv = 0;
                if (!String.IsNullOrWhiteSpace(Ids))
                {
                    string JiaoYiHao = "";
                    int TotalPoint = 0;
                    decimal TotalMoney = 0;
                    decimal ZheKou = 0;
                    string[] Id = Ids.Split(',');
                    List<Model.RefundProduct> plist = new List<Model.RefundProduct>();
                    foreach (string s in Id)
                    {
                        int intId = Utility.Utils.GetInt(s);
                        if (intId > 0)
                        {
                            var model = GetModelById(Convert.ToInt32(intId));
                            if (model != null)
                            {
                                plist.Add(new Model.RefundProduct
                                {
                                    Id = model.ID,
                                    TypeId = model.TypeId,
                                    GoodsId = model.GoodsId,
                                    GoodsName = model.GoodsName,
                                    GoodUnit = model.Unit,
                                    Amount = model.Amount,
                                    Price = model.Price,
                                    Cprice = model.CPrice,
                                    JinE = model.JinE.Value,
                                    State = model.State
                                });
                                string OrderId = model.OrderId;
                                #region 退订单总金额及铁丝币及积分
                                var OrderModel = GetOrderModel(OrderId);
                                if (OrderModel != null)
                                {
                                    JiaoYiHao = OrderModel.JiaoYiHao;
                                    ZheKou = OrderModel.ZheKou;
                                    #region 退款
                                    //退款金额返还到铁丝卡
                                    if (OrderModel.PayType == (int)Model.收款类型.铁丝卡 && BMember.isExistsId(OrderModel.MemberId))
                                    {
                                        #region 铁丝退款
                                        var Mmodel = BMember.GetModel(OrderModel.MemberId);
                                        if (Mmodel != null)
                                        {
                                            //计算总退款额
                                            TotalMoney = TotalMoney + model.JinE.Value;
                                            //计算本次积分
                                            int Point = Convert.ToInt32(model.JinE / 10);
                                            //计算会员应更新总积分
                                            TotalPoint = Mmodel.IntegrationNumber - Point;
                                            if (TotalPoint < 0)
                                                TotalPoint = 0;
                                            if (Point > 0)
                                            {
                                                //更新会员消费积分
                                                BMember.UpdateIntegrationNumber(Mmodel.Id, Model.EnumType.操作符号.减, Point);
                                                #region 新增会员积分记录
                                                BMemberIntegration.Add(new tbl_MemberIntegration
                                                {
                                                    Id = Guid.NewGuid().ToString(),
                                                    TypeId = (int)Model.EnumType.积分类型.退款,
                                                    MemberId = Mmodel.Id,
                                                    UserName = Mmodel.UserName,
                                                    ContactName = Mmodel.ContactName,
                                                    OrderId = OrderModel.OrderId,
                                                    IntegrationNumber = Point,
                                                    Remark = string.Format(CacheSysMsg.GetMsg(144), model.JiaoYiHao, (decimal)model.JinE, Point),
                                                    IssueTime = DateTime.Now
                                                });
                                                #endregion
                                            }
                                            #region 写消费流水
                                            //消费写入会员退款流水
                                            string TradeNumber = System.Guid.NewGuid().ToString();
                                            BMemberWallet.Add(new tbl_MemberWallet
                                            {
                                                Id = System.Guid.NewGuid().ToString(),
                                                TradeNumber = TradeNumber,
                                                UserId = OperatorId,
                                                UserContactName = ContactName,
                                                TypeId = (int)Model.EnumType.财务流水类型.消费退款,
                                                MemberId = OrderModel.MemberId,
                                                ContactName = OrderModel.CustomerName,
                                                TradeMoney = model.JinE.Value,
                                                IsPayed = '0',
                                                Remark = string.Format(CacheSysMsg.GetMsg(138), OrderModel.JiaoYiHao, Convert.ToString(model.GoodsName), model.JinE.Value.ToString("F2"), model.JinE),
                                                PayMemberId = OrderModel.MemberId,
                                                PayContactName = OrderModel.CustomerName,
                                                IssueTime = DateTime.Now
                                            });
                                            #endregion
                                            //更新支付状态，增加铁丝币并且写入消息中心
                                            BMemberWallet.UpdatePayState(TradeNumber);
                                            rv = 1;
                                        }
                                        else
                                        {
                                            //计算总退款额
                                            TotalMoney = TotalMoney + model.JinE.Value;
                                            rv = -4;
                                            return rv;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        //非会员退款
                                        rv = 1;
                                    }

                                    #endregion
                                    #region 退订单明细
                                    int GoodsId = model.GoodsId;
                                    int Amount = model.Amount;
                                    UpdateOrderDetailState(intId, Model.订单状态.已退款);
                                    //更新订单表收款金额
                                    UpdateOrderMoney(OrderId, model.JinE.Value);
                                    //更新商品库存数量
                                    BGoods.UpdateStock(GoodsId, Model.库存操作符号.加, Amount);
                                    #endregion
                                    rv = 1;
                                }
                                else
                                {
                                    rv = -3;
                                    return rv;
                                }
                                #endregion
                            }
                            else
                            {
                                rv = -2;
                                return rv;
                            }
                        }
                        else
                        {
                            rv = -1;
                            return rv;
                        }
                    }
                    #region 构造返回数据
                    ResponseInfo.Add(new Model.MRefundResponseInfo
                    {
                        shoukuanrenid = OperatorId.ToString().PadLeft(4, '0'),
                        jinfen = TotalPoint,
                        liushuihao = JiaoYiHao,
                        shishoutiesijine = TotalMoney,
                        shoukuanshijian = DateTime.Now,
                        yingshoujine = TotalMoney,
                        zhekou = ZheKou,
                        zongjifen = TotalPoint,
                        ProductList = plist
                    });
                    #endregion
                }
                else
                {
                    return rv;
                }
                return rv;
            }
        }

        /// <summary>
        /// 构造订单实体
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <returns></returns>
        public static Model.MOrderInfo GetOrderInfoModel(string OrderId)
        {
            using (FWDC rdc = new FWDC())
            {
                Model.MOrderInfo query = (Model.MOrderInfo)(from q in rdc.dt_OrderView
                                                            where q.OrderId == OrderId
                                                            orderby q.IssueTime descending
                                                            select new Model.MOrderInfo
                                                            {
                                                                OrderId = q.OrderId,
                                                                JiaoYiHao = q.JiaoYiHao,
                                                                JinE = (decimal)q.JinE,
                                                                ZheKou = (decimal)q.ZheKou,
                                                                ActualMoney = (decimal)q.ActualMoney,
                                                                PayType = (Model.收款类型)q.PayType,
                                                                PayStatus = (Model.收款状态)q.PayStatus,
                                                                MemberId = q.MemberId,
                                                                CustomerName = q.CustomerName,
                                                                OperatorId = (int)q.OperatorId,
                                                                OperatorName = q.OperatorName,
                                                                FieldId = q.FieldId,
                                                                OperatorTime = (DateTime)q.OperatorTime,
                                                                Mingxi = GetMingxiList(q.OrderId)
                                                            });
                return query;
            }
        }

        /// <summary>
        /// 构造商品明细列表
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <returns></returns>
        public static List<Model.MorderMingxi> GetMingxiList(string OrderId)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.dt_OrderView
                            where q.OrderId == OrderId
                            orderby q.ID ascending
                            select new Model.MorderMingxi { Id = q.ID, OrderId = q.OrderId, GoodsId = (int)q.GoodsId, GoodsName = q.GoodsName, Unit = q.Unit, Price = q.Price, Cprice = q.CPrice, JinE = (decimal)q.JinE, IssueTime = q.IssueTime, State = (Model.订单状态)q.State };
                return query.ToList();
            }
        }


        /// <summary>
        /// 商品退款，更新订单表的合计金额和收款金额
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="jinE"></param>
        /// <returns></returns>
        public static bool UpdateOrderMoney(string OrderId, decimal jinE)
        {
            using (FWDC rdc = new FWDC())
            {

                var m = rdc.tbl_Order.FirstOrDefault(n => n.OrderId == OrderId);
                if (m != null)
                {
                    m.JinE = m.JinE - jinE;
                    m.ActualMoney = m.ActualMoney - jinE;
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
        /// 历史订单
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="intPageSize"></param>
        /// <param name="CurrencyPage"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static List<tbl_Order> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MOrderSearch searchModel)
        {
            string FieldsList = " *";
            string TableName = "tbl_order";
            string OrderString = " ORDER BY OperatorTime Desc";
            string strWhere = "(1=1)";
            if (!string.IsNullOrWhiteSpace(searchModel.BallFieldId))
            {
                strWhere += " and FieldId ='" + searchModel.BallFieldId + "'";
            }
            if (searchModel.OperatorId.HasValue)
            {
                strWhere += " and OperatorId=" + searchModel.OperatorId + "";
            }
            if (searchModel.OperatorBeginTime.HasValue)
            {
                strWhere += " and OperatorTime>='" + Convert.ToDateTime(searchModel.OperatorBeginTime).ToString("yyyy-MM-dd") + " 00:00:00'";
            }
            if (searchModel.OperatorEndTime.HasValue)
            {
                strWhere += " and OperatorTime<='" + Convert.ToDateTime(searchModel.OperatorBeginTime).ToString("yyyy-MM-dd") + " 23:59:59'";
            }
            if (!string.IsNullOrWhiteSpace(searchModel.JiaoYiHao))
            {
                strWhere += " and JiaoYiHao like '%" + searchModel.JiaoYiHao + "%'";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_Order> q = rdc.ExecuteQuery<tbl_Order>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 根据订单ID获取订单明细列表
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public static List<dt_OrderView> GetOrderDetail(string OrderId)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.dt_OrderView
                            where q.OrderId == OrderId
                            orderby q.IssueTime ascending
                            select q;
                return query.ToList();


            }
        }
        /// <summary>
        /// 获取订单实体
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public static tbl_Order GetOrderModel(string OrderId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Order.FirstOrDefault(n => n.OrderId == OrderId);
                return model;
            }
        }
        /// <summary>
        /// 获取订单明细实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_OrderMingXi GetDetailById(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_OrderMingXi.FirstOrDefault(n => n.ID == Id);
                return model;
            }
        }
        /// <summary>
        /// 更新订单明细状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public static bool UpdateOrderDetailState(int Id, Model.订单状态 State)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_OrderMingXi.FirstOrDefault(n => n.ID == Id);
                if (model != null)
                {
                    model.State = (int)State;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 获取订单明细实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static dt_OrderView GetModelById(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_OrderView.FirstOrDefault(n => n.ID == Id);
                return model;
            }
        }

        /// <summary>
        /// 收银流水
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static List<dt_OrderView> GetCashierFlow(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MSummarySearch searchModel)
        {
            string FieldsList = " * ";
            string TableName = " dt_OrderView ";
            string OrderString = " ORDER BY IssueTime Desc ";

            string strWhere = "(1=1)";
            if (!string.IsNullOrWhiteSpace(searchModel.BallFieldId))
            {
                strWhere += " and FieldId ='" + searchModel.BallFieldId + "'";
            }
            if (searchModel.OperatorId.HasValue)
            {
                strWhere += " and OperatorId=" + searchModel.OperatorId + "";
            }
            if (searchModel.IssueBeginTime.HasValue)
            {
                strWhere += " and IssueTime>='" + Convert.ToDateTime(searchModel.IssueBeginTime).ToString("yyyy-MM-dd") + " 00:00:00'";
            }
            if (searchModel.IssueEndTime.HasValue)
            {
                strWhere += " and IssueTime<='" + Convert.ToDateTime(searchModel.IssueEndTime).ToString("yyyy-MM-dd") + " 23:59:59'";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) From (SELECT " + FieldsList + " FROM " + TableName + " WHERE " + strWhere + " )Table_StocPage ");
                rowsCount = query.First<int>();
                List<dt_OrderView> q = rdc.ExecuteQuery<dt_OrderView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 收银流水
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static List<dt_OrderView> GetCashierFlow(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MCourStatisticsSearch searchModel)
        {
            string FieldsList = " * ";
            string TableName = " dt_OrderView ";
            string OrderString = " ORDER BY IssueTime Desc ";
            string strWhere = "(1=1)";
            #region 查询条件
            if (searchModel.FieldTypeId > 0)
            {
                strWhere = strWhere + " AND (FieldTypeId = " + searchModel.FieldTypeId + ")";
            }
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
                strWhere = strWhere + " AND (CountyId = " + searchModel.AreaId + ")";
            }
            #region 城市权限控制
            if (searchModel.IsAllCity == false)
            {
                if (!String.IsNullOrWhiteSpace(searchModel.CityLimitList))
                {
                    strWhere += " AND (CountyId IN (" + searchModel.CityLimitList + "))";
                }
                else { strWhere += " AND (CountyId = 0)"; }
            }
            #endregion
            #endregion
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) From (SELECT " + FieldsList + " FROM " + TableName + " WHERE " + strWhere + " )Table_StocPage ");
                rowsCount = query.First<int>();
                List<dt_OrderView> q = rdc.ExecuteQuery<dt_OrderView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }


        /// <summary>
        /// 获取当天交易单数
        /// </summary>
        /// <returns></returns>
        public static int GetOrderCount()
        {
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM tbl_Order WHERE OperatorTime>= '" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00' ");
                return query.First<int>();
            }
        }



    }


}
