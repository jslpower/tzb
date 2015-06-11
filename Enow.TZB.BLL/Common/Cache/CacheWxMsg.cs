using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weixin.Mp.Sdk.Domain;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 微信消息发送计划
    /// </summary>
    public abstract class CacheWxMsgSchedule
    {
        #region 微信消息缓存
        /// <summary>
        /// 返回缓存的消息
        /// </summary>
        /// <returns></returns>
        public static List<ReceiveMessageBase> GetList()
        {
            CacheUtility cache = new CacheUtility();
            if (cache.IsExist(CacheNameDefine.WXSendSchedule))
            {
                return (List<ReceiveMessageBase>)cache.RetrieveCache(CacheNameDefine.WXSendSchedule);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 加入消息队列
        /// </summary>
        /// <param name="model"></param>
        public static void Add(ReceiveMessageBase model)
        {
            List<ReceiveMessageBase> list = GetList();
            //判断缓存队列中是否存在相同数据
            if (list != null)
            {
                var SModel = list.FirstOrDefault(n => n.FromUserName == model.FromUserName && n.CreateTime == model.CreateTime);
                if (SModel == null)
                {
                    list.Add(model);
                    CacheUtility cache = new CacheUtility();
                    cache.SetCache(CacheNameDefine.WXSendSchedule, list);
                    Weixin.Mp.Sdk.Util.Logger.WriteTxtLog("关注缓存了!!" + model.MessageBody + "===" + model.FromUserName, @"D:\Website\TZB\SU.txt");
                }
                else
                {
                    Weixin.Mp.Sdk.Util.Logger.WriteTxtLog("缓存已经存在!!" + model.MessageBody + "===" + model.FromUserName, "/Log/ErrorAA.txt");
                }
            }
            else {
                list.Add(model);
                CacheUtility cache = new CacheUtility();
                cache.SetCache(CacheNameDefine.WXSendSchedule, list);
                Weixin.Mp.Sdk.Util.Logger.WriteTxtLog("关注缓存了!!" + model.MessageBody + "===" + model.FromUserName, @"D:\Website\TZB\SU.txt");
            }
        }
        /// <summary>
        /// 移除消息队列
        /// </summary>
        public static void Clear()
        {
            CacheUtility cache = new CacheUtility();
            cache.RemoveCache(CacheNameDefine.WXSendSchedule);
        }
        #endregion
    }
}
