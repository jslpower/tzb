using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model.EnumType
{
    public enum 球员位置
    {
        前锋,
        前卫,
        后卫,
        门将,
        足球宝贝,
        球迷
    }
    public enum 球员角色
    {
        队长 = 1,
        队员,
        足球宝贝,
        领队,
        教练,
        球迷
    }
    public enum 球队徽章
    {
        铁丝坚之队 = 1,
        铁丝强之队,
        铁丝如之队,
        铁丝铁之队,
        铁丝牙之队,
        铁丝刃之队,
        铁丝锋之队,
        铁丝芒之队
    }
    public enum 球队审核状态
    {
        审核中 = 1,
        初审通过,
        初审拒绝,
        终审通过,
        终审拒绝,
        解散申请,
        解散通过,
        解散拒绝
    }
    public enum 球员审核状态
    {
        审核中 = 1,
        审核通过,
        拒绝,
        踢除,
        退出申请,
        同意退出
    }
    public enum 球队审核不通过原因
    {
        球队信息填写不符合规范 = 1,
        球队图片不符合规范,
        球队名称重复
    }
}
