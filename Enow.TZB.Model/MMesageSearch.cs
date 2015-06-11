using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public class MMesageSearch
    {
        public Model.EnumType.消息类型? TypeId { get; set; }
        /// <summary>
        /// 发送人编号
        /// </summary>
        public string SendId { get; set; }
        /// <summary>
        /// 接收人编号
        /// </summary>
        public string ReceiveId { get; set; }
        /// <summary>
        /// 是否阅读 如为null 显示全部
        /// </summary>
        public bool? IsRead { get; set; }
    }
}
