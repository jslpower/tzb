using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Ashx
{
    /// <summary>
    /// 获取场所类型及场所
    /// </summary>
    public class GetCourtList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string getType = Utils.GetQueryStringValue("get");
            StringBuilder sb = new StringBuilder();
            int gID = Utils.GetInt(Utils.GetQueryStringValue("gid"));//国家
            int pID = Utils.GetInt(Utils.GetQueryStringValue("pid"));//省份
            switch (getType)
            {
                case "g":

                    var gList = Enow.TZB.BLL.BBallField.GetFieldTypeList();
                    if (gList != null && gList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < gList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + gList[i].Id.ToString() + "\",\"name\":\"" + gList[i].FieldType + "\"},");
                        }
                        if (sb.Length > 1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append("]}");
                    }
                    else
                    {
                        sb.Append("{\"list\":[]}");
                    }
                    break;
                case "p":

                    var pList = Enow.TZB.BLL.BBallField.GetCourtList(gID);

                    if (pList != null && pList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < pList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + pList[i].Id.ToString() + "\",\"name\":\"" + pList[i].FieldName + "\"},");
                        }
                        if (sb.Length > 1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append("]}");
                    }
                    else
                    {
                        sb.Append("{\"list\":[]}");
                    }

                    break;
                default: break;
            }

            context.Response.Write(sb.ToString());
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