using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public class MActivity
    {
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int types { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? Sttime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? Edtime { get; set; }
        /// <summary>
        /// 发布目标
        /// </summary>
        public int fbmb { get; set; }
    }
}
