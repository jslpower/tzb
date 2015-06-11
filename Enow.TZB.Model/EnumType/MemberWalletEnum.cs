using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model.EnumType
{
    public enum 财务流水类型
    {
        充值=0,
        代充,
        线下充值,
        消费,
        代消费,
        线下消费,
        比赛保证金,
        保证金分配,
        消费退款,
        转账支出,
        转账收入,
        支付
    }
    public enum 付款状态
    {
        未付,
        已付
    }
    public enum 密码有效情况
    {
        失效,
        有效
    }
}
