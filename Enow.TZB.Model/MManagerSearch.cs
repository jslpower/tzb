using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 用户搜索实体
    /// </summary>
    public class MManagerSearch
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string KeyWord { get; set; }
        private bool _IsCashier = false;
        /// <summary>
        /// 是否收银员
        /// </summary>
        public bool IsCashier { get { return _IsCashier; } set { _IsCashier = value; } }
        /// <summary>
        /// 是否所有城市
        /// </summary>
        public bool? IsAllCity { get; set; }
        /// <summary>
        /// 城市限制
        /// </summary>
        public string CityLimitList { get; set; }

    }
}
