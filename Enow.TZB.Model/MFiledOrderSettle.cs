using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 场所结算统计
    /// </summary>
    public class MFiledOrderSettle
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 场所类型
        /// </summary>
        public int FieldTypeId { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string FieldTypeName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        /// <summary>
        /// 场所名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 结算日期
        /// </summary>
        public string SettleDate { get; set; }
        /// <summary>
        /// 现金收款
        /// </summary>
        public decimal CashReceipts { get; set; }
        /// <summary>
        /// 现金退款
        /// </summary>
        public decimal CashRefund { get; set; }
        /// <summary>
        /// 刷卡收款
        /// </summary>
        public decimal CardReceipts { get; set; }
        /// <summary>
        /// 刷卡退款
        /// </summary>
        public decimal CardRefund { get; set; }
        /// <summary>
        /// 现金充值
        /// </summary>
        public decimal CashRecharge { get; set; }        
        /// <summary>
        /// 刷卡充值
        /// </summary>
        public decimal CardRecharge { get; set; }
        /// <summary>
        /// 铁丝币收款
        /// </summary>
        public decimal TieSiCardReceipts { get; set; }
        /// <summary>
        /// 铁丝币退款
        /// </summary>
        public decimal TieSiCardRefund { get; set; }
        /// <summary>
        /// 结算金额=刷卡收款+铁丝币收款-铁丝币退款-刷卡退款-现金充值 结算给场所的钱
        /// </summary>
        public decimal SettleMoney { get; set; }
        public bool IsSettle { get; set; }
        public int SettleOperatorId { get; set; }
        public string SettleName { get; set; }
        public DateTime? SettleTime { get; set; }
    }
}
