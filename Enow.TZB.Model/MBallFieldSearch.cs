using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.Model
{
    /// <summary>
    /// 球场搜索
    /// </summary>
    public class MBallFieldSearch
    {
        private int _FieldType = 0;
        /// <summary>
        /// 场地类型
        /// </summary>
        public int FieldType { get { return _FieldType; } set { _FieldType = value; } }
        private int _CountryId = 0;
        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get { return _CountryId; } set { _CountryId = value; } }
        private int _ProvinceId = 0;
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get { return _ProvinceId; } set { _ProvinceId = value; } }
        private int _CityId = 0;
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get { return _CityId; } set { _CityId = value; } }
        private int _Countyd = 0;
        /// <summary>
        /// 县区编号
        /// </summary>
        public int Countyd { get { return _Countyd; } set { _Countyd = value; } }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 是否所有城市
        /// </summary>
        public bool? IsAllCity { get; set; }
        /// <summary>
        /// 城市限制
        /// </summary>
        public string CityLimitList { get; set; }
    }
    /// <summary>
    /// 场所统计查询
    /// </summary>
    public class MCourStatisticsSearch {
        private int _FieldType = 0;
        /// <summary>
        /// 场地类型
        /// </summary>
        public int FieldTypeId { get { return _FieldType; } set { _FieldType = value; } }
        /// <summary>
        /// 场所编号
        /// </summary>
        public string FieldId { get; set; }
        private int _CountryId = 0;
        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get { return _CountryId; } set { _CountryId = value; } }
        private int _ProvinceId = 0;
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get { return _ProvinceId; } set { _ProvinceId = value; } }
        private int _CityId = 0;
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get { return _CityId; } set { _CityId = value; } }
        private int _AreaId = 0;
        /// <summary>
        /// 县区编号
        /// </summary>
        public int AreaId { get { return _AreaId; } set { _AreaId = value; } }
        /// <summary>
        /// 是否所有城市
        /// </summary>
        public bool? IsAllCity { get; set; }
        /// <summary>
        /// 城市限制
        /// </summary>
        public string CityLimitList { get; set; }
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }
        }
}
