using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 收银流水表实体
    /// </summary>
  public class MCashierSummary
    {
      /// <summary>
      /// 商品ID
      /// </summary>
      public int GoodsId { get; set; }
      /// <summary>
      /// 商品名称
      /// </summary>
      public string GoodsName { get; set; }
      /// <summary>
      /// 现金交易汇总
      /// </summary>
      public decimal CashSum { get; set; }
      /// <summary>
      /// 现金交易数量
      /// </summary>
      public int CashCount { get; set; }
      /// <summary>
      /// 铁丝卡交易汇总
      /// </summary>
      public decimal TiesiSum { get; set; }
      /// <summary>
      /// 铁丝卡交易数量
      /// </summary>
      public int TiesiCount { get; set; }
      /// <summary>
      /// 广发卡交易汇总
      /// </summary>
      public decimal CreditSum { get; set; }
      /// <summary>
      /// 广发卡交易数量
      /// </summary>
      public int CreditCount { get; set; }
      /// <summary>
      /// 总交易数量
      /// </summary>
      public int TotalCount { get; set; }
      /// <summary>
      /// 应收款
      /// </summary>
      public decimal YinshouJinE { get; set; }
      /// <summary>
      /// 实收款
      /// </summary>
      public decimal ShishouJinE { get; set; }

    }
}
