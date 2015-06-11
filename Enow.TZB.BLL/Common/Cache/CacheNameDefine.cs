using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Enow.TZB.BLL
{
    internal abstract class CacheNameDefine
    {
        /// <summary>
        /// 一级菜单缓存名称
        /// </summary>
        public static string MenuList = "MenuList";        
        /// <summary>
        /// 二级菜单缓存名称
        /// </summary>
        public static string SubMenuList = "SubMenuList";
        /// <summary>
        /// 三级菜单缓存名称
        /// </summary>
        public static string ChildMenuList = "ChildMenuList";
        /// <summary>
        /// 系统信息列表缓存名称
        /// </summary>
        public static string SystemInfo = "SystemInfo";
        /// <summary>
        /// 国家 COUNTRY
        /// </summary>
        public static string ComCountry = "CacheCountryList";
        /// <summary>
        /// 省份
        /// </summary>
        public static string ComProvince = "CacheProvinceList";
        /// <summary>
        /// 城市
        /// </summary>
        public static string ComCity = "CacheCityList";
        /// <summary>
        /// 区县
        /// </summary>
        public static string ComCountyArea = "CacheCountyAreaList";
        /// <summary>
        /// 国家 COUNTRY
        /// </summary>
        public static string ComWxCountry = "CacheWxCountryList";
        /// <summary>
        /// 省份
        /// </summary>
        public static string ComWxProvince = "CacheWxProvinceList";
        /// <summary>
        /// 城市
        /// </summary>
        public static string ComWxCity = "CacheWxCityList";
        /// <summary>
        /// 微信发送计划表
        /// </summary>
        public static string WXSendSchedule = "WxSendSchedule";
        /// <summary>
        /// 系统消息列表
        /// </summary>
        public static string SysMsg = "SysMsg";
        
    }
}