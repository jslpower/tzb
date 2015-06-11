using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 系统消息缓存
    /// </summary>
    public class CacheSysMsg
    {
        /// <summary>
        /// 返回缓存的消息
        /// </summary>
        /// <returns></returns>
        public static List<tbl_SysMsg> GetList()
        {
            CacheUtility cache = new CacheUtility();
            if (cache.IsExist(CacheNameDefine.SysMsg))
            {
                return (List<tbl_SysMsg>)cache.RetrieveCache(CacheNameDefine.SysMsg);
            }
            else
            {
                using (FWDC rdc = new FWDC())
                {
                    var list = (from m in rdc.tbl_SysMsg
                                orderby m.Id ascending
                                select m).ToList();
                    cache.SetCache(CacheNameDefine.SysMsg, list);
                    return list;
                }
            }
        }
        /// <summary>
        /// 返回消息
        /// </summary>
        /// <param name="MsgId">消息编号</param>
        /// <returns></returns>
        public static string GetMsg(int MsgId)
        {
            var list = GetList();
            if (list.Count() > 0)
            {
                var model = list.FirstOrDefault(n => n.Id == MsgId);
                if (model != null)
                {
                    return model.MsgInfo;
                }
                else { return ""; }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 移除消息队列
        /// </summary>
        public static void Clear()
        {
            CacheUtility cache = new CacheUtility();
            cache.RemoveCache(CacheNameDefine.SysMsg);
        }
    }
}
