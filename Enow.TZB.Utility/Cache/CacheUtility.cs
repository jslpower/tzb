using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web;

namespace Enow.TZB.Utility
{
    /// <summary>
    /// .net 默认缓存管理类
    /// </summary>
    public class CacheUtility
    {
        #region 变量定义
        /// <summary>
        /// 缓存基类
        /// </summary>
        protected static volatile System.Web.Caching.Cache webCache = System.Web.HttpRuntime.Cache;
        /// <summary>
        /// 默认缓存存活期为-1分钟 不设置超时
        /// </summary>
        protected int _intTimeOut = -1;
        #endregion

        #region 方法
        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey">缓存Key</param>
        /// <returns>存在:True 不存在:False</returns>
        public bool IsExist(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            if (objCache.Get(cacheKey) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey">缓存Key</param>
        /// <param name="objPackage">缓存内容</param>
        public void SetCache(string cacheKey, object objPackage)
        {
            if (cacheKey == null || cacheKey.Length == 0 || objPackage == null)
            {
                return;
            }
            //移除缓存回发事件
            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);
            if (TimeOut == -1)
            {
                webCache.Insert(cacheKey, objPackage, null, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, callBack);
            }
            else
            {
                webCache.Insert(cacheKey, objPackage, null, DateTime.Now.AddMinutes(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
            }
        }

        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="cacheKey">对象的键值</param>
        /// <param name="objPackage">缓存的对象</param>
        /// <param name="files">监视的路径文件</param>
        public void SetCacheWithFileChange(string cacheKey, object objPackage, string[] files)
        {
            if (cacheKey == null || cacheKey.Length == 0 || objPackage == null)
            {
                return;
            }
            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);
            CacheDependency dep = new CacheDependency(files, DateTime.Now);
            webCache.Insert(cacheKey, objPackage, dep, System.DateTime.Now.AddHours(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
        }

        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="cacheKey">对象的键值</param>
        /// <param name="objPackage">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        public void SetCacheWithDepend(string cacheKey, object objPackage, string[] dependKey)
        {
            if (cacheKey == null || cacheKey.Length == 0 || objPackage == null)
            {
                return;
            }
            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);
            CacheDependency dep = new CacheDependency(null, dependKey, DateTime.Now);
            webCache.Insert(cacheKey, objPackage, dep, System.DateTime.Now.AddMinutes(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, callBack);
        }

        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey">Key</param>
        /// <returns></returns>
        public object RetrieveCache(string cacheKey)
        {
            if (cacheKey == null || cacheKey.Length == 0)
            {
                return null;
            }
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// 移除指定CacheKey的值
        /// </summary>
        /// <param name="cacheKey">缓存Key</param>
        public void RemoveCache(string cacheKey)
        {
            if (cacheKey == null || cacheKey.Length == 0)
            {
                return;
            }
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(cacheKey);
        }

        /// <summary>
        /// 到期时间,单位：分钟
        /// </summary>
        public int TimeOut
        {
            set { _intTimeOut = value > 0 ? value : -1; }
            get { return _intTimeOut > 0 ? _intTimeOut : -1; }
        }

        #region 移除缓存回发事件
        /// <summary>
        /// 建立回调委托的一个实例
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="reason"></param>
        private void onRemove(string key, object val, CacheItemRemovedReason reason)
        {
            switch (reason)
            {
                //缓存依赖项被修改
                case CacheItemRemovedReason.DependencyChanged:
                    break;
                //缓存过期
                case CacheItemRemovedReason.Expired:
                    break;
                //缓存被移除
                case CacheItemRemovedReason.Removed:
                    break;
                //系统释放资源
                case CacheItemRemovedReason.Underused:
                    break;
                //其它情况
                default: break;
            }
        }
        #endregion

        #endregion
    }
}
