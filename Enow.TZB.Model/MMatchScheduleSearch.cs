using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 赛程搜索条件
    /// </summary>
    public class MMatchScheduleSearch
    {
        /// <summary>
        /// 赛事编号
        /// </summary>
        public string MatchId { get; set; }
        /// <summary>
        /// 赛事名称
        /// </summary>
        public string MatchName { get; set; }
        /// <summary>
        /// 赛事球场编号
        /// </summary>
        public string MatchFieldId { get; set; }
        /// <summary>
        /// 球场名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 赛程编号
        /// </summary>
        public string GameId { get; set; }
        /// <summary>
        /// 阶段名称
        /// </summary>
        public string GameName { get; set; }
        /// <summary>
        /// 主球队参赛编号
        /// </summary>
        public string HomeMatchTeamId { get; set; }
        /// <summary>
        /// 主球队编号
        /// </summary>
        public string HomeTeamId { get; set; }
        /// <summary>
        /// 客球队参赛编号
        /// </summary>
        public string AwayMatchTeamId { get; set; }
        /// <summary>
        /// 客球队编号
        /// </summary>
        public string AwayTeamId { get; set; }
        /// <summary>
        /// 赛程状态
        /// </summary>
        public Model.EnumType.赛程状态? GameState { get; set; }
        /// <summary>
        /// 结果发布状态
        /// </summary>
        public Model.EnumType.结果发布状态? PublishState { get; set; }
    }
}
