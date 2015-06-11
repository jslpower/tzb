using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public enum 商品上架状态
    {
        上架 = 1,
        下架
    }
    public enum 商品销售状态
    {
        停售 = 0,
        在售,
    
    }
    public enum 义卖商品销售状态
    {
        停售 = 0,
        在售,
        待审核,

    }

    public enum 收款类型
    {
        现金 = 0,
        借记卡,
        信用卡,
        铁丝卡,
        工商银行卡
    }

    public enum 收款状态
    {
        未支付 = 0,
        已支付,
        已充值

    }

    public enum 库存操作符号
    {
        加,
        减
    }

    public enum 订单状态
    {
        已付款 = 0,
        已退款
    }
}
