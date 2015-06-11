using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    public class MMallGoodsSearch
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
       
        /// <summary>
        /// 生产商
        /// </summary>
        public string Producer { get; set; }
        /// <summary>
        /// 商品状态
        /// </summary>
        public 商品销售状态? Status { get; set; }
       
        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime? IssueBeginTime { get; set; }
        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime? IssueEndTime { get; set; }
        /// <summary>
        /// 商品类别
        /// </summary>
        public int? TypeId { get; set; }

        /// <summary>
        /// 二级商品分类
        /// </summary>
        public int? Roleype { get; set; }
        /// <summary>
        /// 是否精品商品
        /// </summary>
        public bool? IsGood { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? IsDelete { get; set; }
       
       
    }
}
