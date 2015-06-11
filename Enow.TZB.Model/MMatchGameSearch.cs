using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 赛事阶段搜索
    /// </summary>
    public class MMatchGameSearch
    {
        /// <summary>
        /// 赛事名称
        /// </summary>
        public string MatchName { get; set; }
        /// <summary>
        /// 阶段名称
        /// </summary>
        public string GameName { get; set; }
        /// <summary>
        /// 球场名称
        /// </summary>
        public string FieldName { get; set; }
    }
}
