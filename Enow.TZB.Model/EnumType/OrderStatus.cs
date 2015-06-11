using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public enum 商城订单状态
    {
        未支付 = 1,
        已支付,
        无效,
        退款,
        待审核,
        审核通过,
        审核无效,
        待发货,
        已发货
    }
    public enum 商城支付方式
    {
        微信支付 = 1,
        APP支付
    }
}
