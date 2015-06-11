using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public class MOrderSearch
    {
        /// <summary>
        /// 球场ID
        /// </summary>
        public string BallFieldId { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int? OperatorId { get; set; }

        /// <summary>
        /// 操作日期--开始
        /// </summary>
        public DateTime? OperatorBeginTime { get; set; }

        /// <summary>
        /// 操作日期-结束
        /// </summary>
        public DateTime? OperatorEndTime { get; set; }
        /// <summary>
        /// 收款类型
        /// </summary>
        public 收款类型? PayType { get; set; }
        /// <summary>
        /// 收款状态
        /// </summary>
        public 收款状态? PayStatus { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string JiaoYiHao { get; set; }
    }

    public class MSummarySearch
    {
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int? OperatorId { get; set; }
        /// <summary>
        /// 球场ID
        /// </summary>
        public string BallFieldId { get; set; }
        /// <summary>
        /// 操作日期--开始
        /// </summary>
        public DateTime? IssueBeginTime { get; set; }

        /// <summary>
        /// 操作日期-结束
        /// </summary>
        public DateTime? IssueEndTime { get; set; }
    }
}
