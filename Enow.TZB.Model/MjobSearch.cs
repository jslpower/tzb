using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
   public class MjobSearch
    {
       /// <summary>
       /// 职位名称
       /// </summary>
       public string JobName { get; set; }
       /// <summary>
       /// 职位类别
       /// </summary>
       public int? jobType { get; set; }
       /// <summary>
       /// 是否前台发布 0：否 1：是
       /// </summary>
       public int? IsValid { get; set; }
       /// <summary>
       /// 开始日期
       /// </summary>
       public DateTime? startDate { get; set; }
       /// <summary>
       /// 结束日期
       /// 
       /// </summary>
       public DateTime? endDate { get; set; }
    }

    /// <summary>
    /// 应聘查询实体
    /// </summary>
  public class MOfferSearch
   {
       /// <summary>
       /// tbl_offer表Id
       /// </summary>
       public string OfferId { get; set; }
       /// <summary>
       /// 工作名称
       /// </summary>
       public string JobName { get; set; }
       /// <summary>
       /// 用户名
       /// </summary>
       public string UserName { get; set; }
       /// <summary>
       /// 姓名
       /// </summary>
       public string ContactName { get; set; }
       /// <summary>
       /// 申请开始日期
       /// </summary>
       public DateTime? StartIssueTime { get; set; }
      /// <summary>
      /// 申请结束日期
      /// </summary>
       public DateTime? EndIssueTime { get; set; }
      /// <summary>
      /// 应聘状态
      /// </summary>
       public int OfferState { get; set; }
      /// <summary>
      /// 应聘人电话
      /// </summary>
       public string Mobile { get; set; }
      /// <summary>
      /// 国家ID
      /// </summary>
       public int CountryId { get; set; }
      /// <summary>
      /// 省份ID
      /// </summary>
       public int ProvinceId { get; set; }
      /// <summary>
      /// 城市ID
      /// </summary>
       public int CityId { get; set; }
      /// <summary>
      /// 区县ID
      /// </summary>
       public int AreaId { get; set; }
   }
}
