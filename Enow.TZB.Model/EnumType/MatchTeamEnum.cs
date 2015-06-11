using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model.EnumType
{
    public enum 参赛审核状态
    {
        资格审核中,
        资格审核通过,
        资格审核拒绝,
        报名费确认中,
        已获参赛权,
        已抽签
    }
    public enum 赛制
    {
        循环赛,
        淘汰赛,
        小组赛加淘汰赛
    }
    public enum 赛事类型
    {
        城市赛事,
        全省赛事,
        全国赛事,
        全球赛事
    }
    public enum 支付方式
    {
        未支付,
        线下支付,
        微信支付,
        支付宝支付
    }
    public enum 赛程状态
    {
        不发布,
        发布
    }
    public enum 结果发布状态
    {
        不发布,
        发布
    }
    public enum 进球类型
    {
        上半场进球=1,
        上半场点球,
        下半场进球,
        下半场点球,
        加时进球,
        上半场犯规,
        上半场红牌,
        上半场黄牌,
        下半场犯规,
        下半场红牌,
        下半场黄牌
    }
}
