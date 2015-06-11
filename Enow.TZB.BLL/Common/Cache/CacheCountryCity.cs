using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Utility;


namespace Enow.TZB.BLL
{
    /// <summary>
    /// 国家 省份 城市 区县 存
    /// </summary>
    public class CacheSysBaseCity
    {
        #region 国家
        /// <summary>
        /// 返回国家缓存的消息
        /// </summary>
        /// <returns></returns>
        public static List<tbl_SysCountry> GetCountryList()
        {
            CacheUtility cache = new CacheUtility();
            if (cache.IsExist(CacheNameDefine.ComCountry))
            {
                return (List<tbl_SysCountry>)cache.RetrieveCache(CacheNameDefine.ComCountry);
            }
            else
            {
                using (FWDC rdc = new FWDC())
                {
                    var list = (from m in rdc.tbl_SysCountry
                                orderby m.SequenceId ascending,m.CountryId
                                select m).ToList();
                    cache.SetCache(CacheNameDefine.ComCountry, list);
                    return list;
                }
            }
        }
        /// <summary>
        /// 移除消息队列
        /// </summary>
        public static void CountryClear()
        {
            CacheUtility cache = new CacheUtility();
            cache.RemoveCache(CacheNameDefine.ComCountry);
        }
        #endregion
       
        #region 省份
        /// <summary>
        /// 返回省份缓存的消息
        /// </summary>
        /// <returns></returns>
        public static List<tbl_SysProvince> GetProvinceList()
        {
            CacheUtility cache = new CacheUtility();
            if (cache.IsExist(CacheNameDefine.ComProvince))
            {
                return (List<tbl_SysProvince>)cache.RetrieveCache(CacheNameDefine.ComProvince);
            }
            else
            {
                using (FWDC rdc = new FWDC())
                {
                    var list = (from m in rdc.tbl_SysProvince
                                orderby m.CountryId ascending,m.ProvinceId ascending
                                select m).ToList();
                    cache.SetCache(CacheNameDefine.ComProvince, list);
                    return list;
                }
            }
        }
        /// <summary>
        /// 移除消息队列
        /// </summary>
        public static void ProvinceClear()
        {
            CacheUtility cache = new CacheUtility();
            cache.RemoveCache(CacheNameDefine.ComProvince);
        }
        #endregion

        #region 城市
        /// <summary>
        /// 返回城市缓存的消息
        /// </summary>
        /// <returns></returns>
        public static List<tbl_SysCity> GetCityList()
        {
            CacheUtility cache = new CacheUtility();
            if (cache.IsExist(CacheNameDefine.ComCity))
            {
                return (List<tbl_SysCity>)cache.RetrieveCache(CacheNameDefine.ComCity);
            }
            else
            {
                using (FWDC rdc = new FWDC())
                {
                    var list = (from m in rdc.tbl_SysCity
                                orderby m.ProvinceId ascending,m.CityId ascending
                                select m).ToList();
                    cache.SetCache(CacheNameDefine.ComCity, list);
                    return list;
                }
            }
        }
        /// <summary>
        /// 移除城市
        /// </summary>
        public static void CityClear()
        {
            CacheUtility cache = new CacheUtility();
            cache.RemoveCache(CacheNameDefine.ComCity);
        }
        #endregion

        #region 区县
        /// <summary>
        /// 返回区县缓存的消息
        /// </summary>
        /// <returns></returns>
        public static List<tbl_SysArea> GetCountyAreaList()
        {
            CacheUtility cache = new CacheUtility();
            if (cache.IsExist(CacheNameDefine.ComCountyArea))
            {
                return (List<tbl_SysArea>)cache.RetrieveCache(CacheNameDefine.ComCountyArea);
            }
            else
            {
                using (FWDC rdc = new FWDC())
                {
                    var list = (from m in rdc.tbl_SysArea
                                orderby m.CityId ascending,m.CountyId ascending
                                select m).ToList();
                    cache.SetCache(CacheNameDefine.ComCountyArea, list);
                    return list;
                }
            }
        }
        /// <summary>
        /// 移除区县队列
        /// </summary>
        public static void CountyAreaClear()
        {
            CacheUtility cache = new CacheUtility();
            cache.RemoveCache(CacheNameDefine.ComCountyArea);
        }
        #endregion
    }
}
