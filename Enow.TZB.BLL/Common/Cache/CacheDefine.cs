using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Enow.TZB.Model;
using Enow.TZB.Utility;


namespace Enow.TZB.BLL
{ 
    /// <summary>
    /// 缓存定义
    /// </summary>
    internal abstract class CacheDefine
    {      
        #region 菜单缓存

        #region 一级菜单
        /// <summary>
        /// 缓存一级菜单
        /// </summary>
        /// <returns></returns>
        public static List<MenuList> GetMenuList()
        {
            CacheUtility cache = new CacheUtility();
            if (cache.IsExist(CacheNameDefine.MenuList))
            {
                return (List<MenuList>)cache.RetrieveCache(CacheNameDefine.MenuList);
            }
            else
            {
                using (FWDC rdc = new FWDC())
                {
                    var menu = (from m in rdc.MenuList
                                orderby m.Id ascending
                                select m).ToList();
                    cache.SetCache(CacheNameDefine.MenuList, menu);
                    return menu;
                }
            }
        }
        /// <summary>
        /// 根据权限显示一级菜单
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <param name="PerList">权限</param>
        /// <returns></returns>
        public static List<MenuList> GetMenuList(string UserId, string PerList)
        {
            string cacheName = CacheNameDefine.MenuList + "_" + UserId;
            CacheUtility cache = new CacheUtility();
            if (cache.IsExist(cacheName))
            {
                return (List<MenuList>)cache.RetrieveCache(cacheName);
            }
            else
            {
                using (FWDC rdc = new FWDC())
                {
                    if (!String.IsNullOrEmpty(PerList))
                    {
                        var menu = rdc.ExecuteQuery<MenuList>("SELECT DISTINCT Id,TypeId,MenuName,IcoPath from dt_RoleMenu WHERE (PerId in (" + PerList + "))").ToList();
                        cache.SetCache(cacheName, menu);
                        return menu;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        #region 清理缓存
        /// <summary>
        /// 移除菜单CACHE
        /// </summary>
        public static void RemoveMenuCache()
        {
            CacheUtility cache = new CacheUtility();
            cache.RemoveCache(CacheNameDefine.MenuList);
        }
        #endregion
        #endregion

        #region 二级级菜单
        /// <summary>
        /// 缓存二级菜单
        /// </summary>
        /// <returns></returns>
        public static List<RoleClass> GetSubMenuList()
        {
            CacheUtility cache = new CacheUtility();
            if (cache.IsExist(CacheNameDefine.SubMenuList))
            {
                return (List<RoleClass>)cache.RetrieveCache(CacheNameDefine.SubMenuList);
            }
            else
            {
                using (FWDC rdc = new FWDC())
                {
                    var query = (from m in rdc.RoleClass
                                 orderby m.MenuId ascending, m.Id ascending
                                 select m).ToList();
                    cache.SetCache(CacheNameDefine.SubMenuList, query);
                    return query;
                }
            }
        }
        #region 清理缓存
        /// <summary>
        /// 移除二级菜单CACHE
        /// </summary>
        public static void RemoveSubMenuCache()
        {
            CacheUtility cache = new CacheUtility();
            cache.RemoveCache(CacheNameDefine.SubMenuList);
        }
        #endregion
        #endregion

        #region 子菜单
        /// <summary>
        /// 缓存子菜单
        /// </summary>
        /// <returns></returns>
        public static List<RoleList> GetChildMenuList()
        {
            CacheUtility cache = new CacheUtility();
            if (cache.IsExist(CacheNameDefine.ChildMenuList))
            {
                return (List<RoleList>)cache.RetrieveCache(CacheNameDefine.ChildMenuList);
            }
            else
            {
                using (FWDC rdc = new FWDC())
                {
                    var query = (from m in rdc.RoleList
                                 orderby m.ClassId ascending, m.Id ascending
                                 select m).ToList();
                    cache.SetCache(CacheNameDefine.ChildMenuList, query);
                    return query;
                }
            }
        }

        /// <summary>
        /// 移除二级菜单CACHE
        /// </summary>
        public static void RemoveChildMenuCache()
        {
            CacheUtility cache = new CacheUtility();
            cache.RemoveCache(CacheNameDefine.ChildMenuList);
        }
        #endregion

        #endregion       

    }
}