using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 文章查询实体
    /// </summary>
    public class MArticleSearch
    {
        /// <summary>
        /// 文章类别
        /// </summary>
        public int ClassId { get; set; }
        /// <summary>
        /// 发布对象
        /// </summary>
        public 发布对象? PublicTarget { get; set; }
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool? IsValid { get; set; }
        /// <summary>
        /// 是否显示图片
        /// </summary>
        public bool? ShowPhoto { get; set; }
    }

    /// <summary>
    /// 资讯类别查询实体
    /// </summary>
    public class MarticleTypeSeach
    {

        public int? id { get; set; }
        /// <summary>
        /// 资讯类别ID
        /// </summary>
        public int? TypeId { get; set; }
        /// <summary>
        /// 资讯类别名称
        /// </summary>
        public string TypeName { get; set; }
    }
}
