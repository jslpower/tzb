using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model.EnumType
{
    public enum 操作符号
    {
        加,
        减
    }
    public enum 会员状态
    {
        审核中,
        通过,
        拒绝
    }
    public enum 归档状态
    {
        在用 = 1,
        归档
    }
    public enum 会员类型
    {
        微信 = 0,
        网站,
        APP
    }
    public enum 头衔
    {
        铁矿石 = 1,
        生铁,
        熟铁,
        钢铁,
        钨铁,
        合金铁,
        超合金铁,
        外星铁
    }
    public enum 积分类型
    {
        获取 = 1,
        消费,
        奖励,
        惩罚,
        退款
    }
}
