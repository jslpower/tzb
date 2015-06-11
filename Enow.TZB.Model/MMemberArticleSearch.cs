using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 会员日志查询实体
    /// </summary>
    public class MMemberArticleSearch
    {
        private int _TypeId = 0;
        /// <summary>
        /// 文章类别
        /// </summary>
        public int TypeId { get { return _TypeId; } set { _TypeId = value; } }
        /// <summary>
        /// 日志发布人所在的球队
        /// </summary>
        public string TeamId { get; set; }
        /// <summary>
        /// 日志发布人
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool? IsEnable { get; set; }
    }
}
