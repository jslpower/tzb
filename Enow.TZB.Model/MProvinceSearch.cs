using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 国家查询实体
    /// </summary>
    public class MCountrySearch
    {
        /// <summary>
        /// 中文名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnName { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        public string JP { get; set; }
        /// <summary>
        /// 全拼
        /// </summary>
        public string QP { get; set; }
    }

    /// <summary>
    /// 省份查询实体
    /// </summary>
   public class MProvinceSearch
    {
       /// <summary>
       /// 国家ID
       /// </summary>
       public int? CountryId { get; set; }
       /// <summary>
       /// 中文名称
       /// </summary>
       public string Name { get; set; }
       /// <summary>
       /// 英文名称
       /// </summary>
       public string EnName { get; set; }
       /// <summary>
       /// 简拼
       /// </summary>
       public string JP { get; set; }
       /// <summary>
       /// 全拼
       /// </summary>
       public string QP { get; set; }
    }

    /// <summary>
    /// 城市查询实体
    /// </summary>
   public class MCitySearch
   {
       /// <summary>
       /// 省份ID
       /// </summary>
       public int? ProvinceId { get; set; }
       /// <summary>
       /// 中文名称
       /// </summary>
       public string Name { get; set; }
       /// <summary>
       /// 英文名称
       /// </summary>
       public string EnName { get; set; }
       /// <summary>
       /// 简拼
       /// </summary>
       public string JP { get; set; }
       /// <summary>
       /// 全拼
       /// </summary>
       public string QP { get; set; }
   }

    /// <summary>
    /// 区县查询实体
    /// </summary>
   public class MAreaSearch
   {
       /// <summary>
       /// 城市ID
       /// </summary>
       public int? CityId { get; set; }
       /// <summary>
       /// 中文名称
       /// </summary>
       public string Name { get; set; }
       /// <summary>
       /// 英文名称
       /// </summary>
       public string EnName { get; set; }
       /// <summary>
       /// 简拼
       /// </summary>
       public string JP { get; set; }
       /// <summary>
       /// 全拼
       /// </summary>
       public string QP { get; set; }
   }
}
