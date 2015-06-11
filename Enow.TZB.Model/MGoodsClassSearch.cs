using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public class MGoodsClassSearch
    {
        /// <summary>
        /// 商品类别名称
        /// </summary>
        public string ClassName { get; set; }

        public bool? IsDelete { get; set; }
        /// <summary>
        /// 一级商品分类
        /// </summary>
        public int? GoodsType { get; set; }
        /// <summary>
        /// 二级商品分类
        /// </summary>
        public int GoodsroleType { get; set; }
    }
}
