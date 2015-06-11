using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
   public class MGoodsSearch
    {

       /// <summary>
       /// 商品名称
       /// </summary>
       public string GoodsName { get; set; }
       /// <summary>
       /// 球场名称
       /// </summary>
       public string BallFieldName { get; set; }
       /// <summary>
       /// 生产商
       /// </summary>
       public string Producer { get; set; }
       /// <summary>
       /// 商品状态
       /// </summary>
       public 商品上架状态? Status { get; set; }
       /// <summary>
       /// 球场ID
       /// </summary>
       public string BallFieldId { get; set; }
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
       /// 大于当前类别ID的商品
       /// </summary>
       public int? MaxThenType { get; set; }
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
