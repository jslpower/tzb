using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using Newtonsoft.Json;

namespace Enow.TZB.Web.Ashx
{
    /// <summary>
    /// GetFieldListByCity 的摘要说明
    /// </summary>
    public class GetFieldListByCity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string strJoson = "{\"list\":[]}";
            int CityId = Utils.GetInt(Utils.GetQueryStringValue("CityId"));
            context.Response.ContentType = "text/plain";
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.Cache.SetNoStore();
            if (CityId>0)
            {
                var list = BBallField.GetList(CityId);
                if (list.Count()>0)
                {
                    strJoson = "{\"list\":" + JsonConvert.SerializeObject(list) + "}";
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