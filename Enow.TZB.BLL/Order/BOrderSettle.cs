using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 场所结算表
    /// </summary>
    public class BOrderSettle
    {
        /// <summary>
        /// 新增结算信息
        /// </summary>
        /// <param name="model"></param>
        public static void Add(tbl_OrderSettle model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = GetModel(model.FieldId, model.SettleYear, model.SettleMonth);
                if (m == null)
                {
                    rdc.tbl_OrderSettle.InsertOnSubmit(model);
                    rdc.SubmitChanges();
                }
            }
        }
        #region 取得球场结算金额
        /// <summary>
        /// 取得球场结算金额
        /// </summary>
        /// <param name="FieldId">球场编号</param>
        /// <param name="Year">结算年份</param>
        /// <param name="Month">结算月份</param>
        /// <returns></returns>
        public static decimal GetFieldSettleMoney(string FieldId, int Year, int Month) {
            using (FWDC rdc = new FWDC())
            {
                string StartDate = Year.ToString() + "-" + Month.ToString() + "-1 00:00:00";
                string EndDate = DateTime.Parse(Year.ToString() + "-" + Month.ToString() + "-1 00:00:00").AddMonths(1).ToString("yyyy-MM-dd HH:mm:ss");
                decimal Count = 0;
                // 刷卡收款
                var countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + FieldId + "') AND TypeId > 1 and (paytype IN (" + (int)Model.收款类型.借记卡 + "," + (int)Model.收款类型.信用卡 + "," + (int)Model.收款类型.工商银行卡 + ")) and State=" + (int)Model.订单状态.已付款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                Count = countQuery.First<decimal>();
                decimal CardReceipts = Count;
                // 刷卡退款
                countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + FieldId + "') AND TypeId > 1 and (paytype IN (" + (int)Model.收款类型.借记卡 + "," + (int)Model.收款类型.信用卡 + "," + (int)Model.收款类型.工商银行卡 + ")) and State=" + (int)Model.订单状态.已退款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                Count = countQuery.First<decimal>();
                decimal CardRefund = Count;
                //现金充值
                countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + FieldId + "') AND TypeId = 1 and paytype = " + (int)Model.收款类型.现金 + " and State=" + (int)Model.订单状态.已付款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                Count = countQuery.First<decimal>();
                decimal CashRecharge = Count;
                //铁丝币收款
                countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + FieldId + "') AND TypeId > 1 and paytype = " + (int)Model.收款类型.铁丝卡 + " and State=" + (int)Model.订单状态.已付款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                Count = countQuery.First<decimal>();
                decimal TieSiCardReceipts = Count;
                //铁丝币退款
                countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + FieldId + "') AND TypeId > 1 and paytype = " + (int)Model.收款类型.铁丝卡 + " and State=" + (int)Model.订单状态.已退款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                Count = countQuery.First<decimal>();
                decimal TieSiCardRefund = Count;
                decimal SettleMoney = CardReceipts + TieSiCardReceipts - CardRefund - TieSiCardRefund - CashRecharge;
                return SettleMoney;
            }
        }
        #endregion
        /// <summary>
        /// 查询场所结算信息
        /// </summary>
        /// <param name="FieldId"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static tbl_OrderSettle GetModel(string FieldId,int Year,int Month)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_OrderSettle.FirstOrDefault(n => n.FieldId == FieldId && n.SettleYear == Year && n.SettleMonth == Month);
                return model;
            }
        }
    }
}
