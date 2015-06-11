using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 会员日志留言查询实体
    /// </summary>
    public class MArticleLeaveSearch
    {
     
        /// <summary>
        /// 留言人
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? IsEnable { get; set; }
        /// <summary>
        /// 是否回复
        /// </summary>
        public bool? IsReply { get; set; }
    }
}
