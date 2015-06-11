using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    #region 订单支付信息实体
    public class MOrderInfo
    {
        public MOrderInfo()
        {
        
        }

        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        public string JiaoYiHao { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal ZheKou { get; set; }
        /// <summary>
        /// 实收款
        /// </summary>
        public decimal ActualMoney { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public 收款类型 PayType { get; set; }
        /// <summary>
        /// 收款状态
        /// </summary>
        public 收款状态 PayStatus { get; set; }
        /// <summary>
        /// 会员ID，非会员为"0"
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 客户姓名（非会员显示“散客”）
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatorId { get; set; }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 球场编号
        /// </summary>
        public string FieldId { get; set; }
       /// <summary>
       /// 操作日期
       /// </summary>
        public DateTime OperatorTime { get; set; }
        /// <summary>
        /// 会员手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 订单明细
        /// </summary>
        public IList<MorderMingxi> Mingxi { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Point { get; set; }
        /// <summary>
        /// 累计积分
        /// </summary>
        public int TotalPoint { get; set; }
        /// <summary>
        /// 铁丝币
        /// </summary>
        public decimal CurrencyNumber { get; set; }
        /// <summary>
        /// 铁丝币余额
        /// </summary>
        public decimal TotalCurrencyNumber { get; set; }
    }
    #endregion

    #region 订单明细实体

    public class MorderMingxi{

        public MorderMingxi()
        { 
        }

        /// <summary>
        /// 订单明细表自增ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商品类别
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 市场价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 铁丝价
        /// </summary>
        public decimal Cprice { get; set; }
        /// <summary>
        /// 小计金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 订单交易状态(0:已支付，1：已退款)
        /// </summary>
        public Model.订单状态 State { get; set; }

    }
    #endregion
}
