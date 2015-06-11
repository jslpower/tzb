using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 赛事信息查询实体
    /// </summary>
  public  class MMatch
    {
      /// <summary>
      /// 比赛名称
      /// </summary>
      public string MatchName { get; set; }
      /// <summary>
      /// 比赛开始时间
      /// </summary>
      public DateTime? BeginDate { get; set; }
      /// <summary>
      /// 结束时间
      /// </summary>
      public DateTime? EndDate { get; set; }
      /// <summary>
      /// 比赛城市
      /// </summary>
      public int cityid { get; set; }
    }
}
