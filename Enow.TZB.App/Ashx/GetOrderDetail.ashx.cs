using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Enow.TZB.Utility;
using Enow.TZB.BLL.Order;

namespace Enow.TZB.Web.Ashx
{
    /// <summary>
    /// 取得订单详情
    /// </summary>
    public class GetOrderDetail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string strJoson = "{\"result\":\"0\",\"data\":[]}";
            context.Response.ContentType = "text/plain";
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.Cache.SetNoStore();
            int Id = Utils.GetInt(Utils.GetQueryStringValue("Id"));
            if (Id>0)
            {
                var model = BOrder.GetModelById(Id);
                if (model != null)
                {
                    strJoson = "{\"result\":\"1\",\"data\":" + JsonConvert.SerializeObject(model) + "}";
                }
            }
            context.Response.Write(strJoson);
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