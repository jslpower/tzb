using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model.EnumType
{
    /// <summary>
    /// 活动分类枚举
    /// </summary>
    public enum ActivityEnum
    {
        场地=1,
        培训,
        聚会,
        见面会
    }
    /// <summary>
    /// 活动报名状态枚举
    /// </summary>
    public enum ApplicantsStartEnum
    {
        申请中 = 0,
        已通过,
        未通过
    }
    /// <summary>
    /// 发布目标枚举
    /// </summary>
    public enum ReleaseEnum
    {
        网站 = 1,
        微信,
        APP,
    }
}
