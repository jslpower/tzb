using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.Ashx
{
    /// <summary>
    /// 生成二维码图片
    /// </summary>
    public class GetQrCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.Cache.SetNoStore();
            string QCode = context.Request.QueryString["QCode"];
            QrCode.CreateZxingCodeResponse(QCode, 600, 600);
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