using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
   
    /// <summary>
    ///  我的约战搜索
    /// </summary>
    public class MGathersSearch
    {
       
        /// <summary>
        /// 城市
        /// </summary>
        public int  City { get; set; }
        /// <summary>
        /// 发起约战的球队
        /// </summary>
        public string TeamId { get; set; }
        /// <summary>
        /// 发起约战的人
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 球队名称
        /// </summary>
        public string Name { get; set; }

        public bool? IsAcceptWar { get; set; }

        /// <summary>
        /// 应战方球队名称
        /// </summary>
        public string AcceptTeamName { get; set; }
        /// <summary>
        /// 应战方球队编号
        /// </summary>
        public string AcceptTeamId { get; set; }
        /// <summary>
        /// 是否发出战报
        /// </summary>
        public bool? IsGatherResult { get; set; }
    }
}
