using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public class MMatchTeamSearch
    {
        /// <summary>
        /// 赛制
        /// </summary>
        public Model.EnumType.赛制? TypeId { get; set; }
        /// <summary>
        /// 赛事名称
        /// </summary>
        public string MatchName { get; set; }
        /// <summary>
        /// 参赛队伍名称
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public Model.EnumType.参赛审核状态? State { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
