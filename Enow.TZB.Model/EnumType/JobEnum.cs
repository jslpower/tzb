using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model.EnumType
{
    public enum 招聘类型
    { 
        招聘=0,
        招募
    }
    public enum 应聘状态
    {
        已投递=0,
        婉拒,
        面试,
        试用,
        正式,
        离职,
        开除
    }

    public enum JobType
    {
        会员=0,
        舵主,
        堂主,
        站长
    }
    /// <summary>
    /// 关注点赞分类 1人员关注  2日志点赞 3 球队关注
    /// </summary>
    public enum GuanZhuenum
    {
        人员关注 = 1,
        点赞,
        球队关注
    }
}
