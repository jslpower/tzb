using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.Ashx
{
    /// <summary>
    /// GetSubMenu 的摘要说明
    /// </summary>
    public class GetSubMenu : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string MenuId = context.Request.QueryString["MenuId"];
            string SubMenuId = context.Request.QueryString["SubMenuId"];
            context.Response.ContentType = "text/plain";
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.Cache.SetNoStore();

            context.Response.Write(InitMenu.GetSubMenuStr(MenuId, SubMenuId));
            context.Response.End();
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