using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public class MallorderQuery
    {
        /// <summary>
        /// 发布会员ID
        /// </summary>
        public string GoodsMemberId { get; set; }
        /// <summary>
        /// 所属会员ID
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 订单状态，1:未支付，2：已支付，3：无效，4：退订
        /// </summary>
        public int PayStatus { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int zffs { get; set; }

        public bool? IsDelete { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
