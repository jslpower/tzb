using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public class MVoteQuery
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 投票类型
        /// </summary>
        public int? types { get; set; }
        /// <summary>
        /// 发布目标
        /// </summary>
        public int? Release { get; set; }
        /// <summary>
        /// 栏目编号 1 投票  2抽奖
        /// </summary>
        public int? ColumnID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 投票/中奖信息ID
        /// </summary>
        public string Vid { get; set; }
        /// <summary>
        /// 中奖编号 0:只显示获奖信息
        /// </summary>
        public int? AwardsNum
        {
            get;
            set;
        }
    }
}
