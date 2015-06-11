using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public class MApplicants
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public long ActivityId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string USid { get; set; }
        /// <summary>
        /// 活动标题
        /// </summary>
        public string Acttitle { get; set; }
        /// <summary>
        /// 活动分类
        /// </summary>
        public int Acttypes { get; set; }
        /// <summary>
        /// 报名状态
        /// </summary>
        public int Appstater { get; set; }
        /// <summary>
        /// 发布目标
        /// </summary>
        public int fbmb { get; set; }
    }
}
