using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 收款异步提交返回信息业务实体
    /// </summary>
    public class MRefundResponseInfo
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string liushuihao { get; set; }
        /// <summary>
        /// 收款人编号
        /// </summary>
        public string shoukuanrenid { get; set; }
        /// <summary>
        /// 收款时间
        /// </summary>
        public DateTime shoukuanshijian { get; set; }
        /// <summary>
        /// 收款时间
        /// </summary>
        public string shoukuanshijian1 { get { return shoukuanshijian.ToString("yyyy-MM-dd HH:mm"); } }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal yingshoujine { get; set; }
        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal shishoujine { get; set; }
        /// <summary>
        /// 实收铁丝金额
        /// </summary>
        public decimal shishoutiesijine { get; set; }
        /// <summary>
        /// 本次积分
        /// </summary>
        public int jinfen { get; set; }
        /// <summary>
        /// 总积分
        /// </summary>
        public int zongjifen { get; set; }
        /// <summary>
        /// 收款折扣
        /// </summary>
        public decimal zhekou { get; set; }
        /// <summary>
        /// 商品金额
        /// </summary>
        public decimal shangpinjine { get; set; }
        public List<RefundProduct> ProductList { get; set; }
    }
    /// <summary>
    /// 退款商品实体
    /// </summary>
    public class RefundProduct
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int GoodsId { get; set; }
        public string GoodsName { get; set; }
        public string GoodUnit { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Cprice { get; set; }
        public decimal JinE { get; set; }
        public int State { get; set; }
    }
}
