using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Enow.TZB.BLL;

namespace Enow.TZB.WebForm.ashx
{
    /// <summary>
    /// 取得球场信息
    /// </summary>
    public class GetFieldInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string strJoson = "{\"IsResult\":\"0\",\"Data\":{}}";
            string Id = context.Request.QueryString["Id"];
            context.Response.ContentType = "text/plain";
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.Cache.SetNoStore();
            if (!String.IsNullOrWhiteSpace(Id))
            {
                var model = BBallField.GetDynmaicModel(Id);
                if (model != null)
                {
                    strJoson = "{\"IsResult\":\"1\",\"Data\":" + JsonConvert.SerializeObject(model) + "}";
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