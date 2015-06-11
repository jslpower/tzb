using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.Ashx
{
    /// <summary>
    /// 消息设为已读
    /// </summary>
    public class MessageRead : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Id = Utils.GetQueryStringValue("Id");
            context.Response.ContentType = "text/plain";
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.Cache.SetNoStore();
            if (!String.IsNullOrWhiteSpace(Id))
            {
                var bln = BMessage.UpdateReadState(Id);
                if (bln)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", ""));
                }
                else {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", ""));
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}