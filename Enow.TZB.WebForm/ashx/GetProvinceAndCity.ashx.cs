using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.TZB.Utility;
using System.Text;

namespace Web.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2012-11-20
    /// 说明：处理国家，省份，城市，县区
    public class GetProvinceAndCity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string getType = Utils.GetQueryStringValue("get");
            StringBuilder sb = new StringBuilder();
            int gID = Utils.GetInt(Utils.GetQueryStringValue("gid"));//国家ID
            int pID = Utils.GetInt(Utils.GetQueryStringValue("pid"));//省份ID
            int cID = Utils.GetInt(Utils.GetQueryStringValue("cid"));//城市ID
            int xID = Utils.GetInt(Utils.GetQueryStringValue("xid"));//区县ID
            switch (getType)
            {
                case "g":

                    IList<Enow.TZB.BLL.tbl_SysCountry> gList = Enow.TZB.BLL.BMSysProvince.GetCountryList();
                    if (gList != null && gList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < gList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + gList[i].CountryId.ToString() + "\",\"name\":\"" + gList[i].Name + "\"},");
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

                    IList<Enow.TZB.BLL.tbl_SysProvince> pList = Enow.TZB.BLL.BMSysProvince.GetProvinceList(new Enow.TZB.Model.MProvinceSearch { CountryId=gID});

                    if (pList != null && pList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < pList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + pList[i].ProvinceId.ToString() + "\",\"name\":\"" + pList[i].Name + "\"},");
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
                case "c":

                    IList<Enow.TZB.BLL.tbl_SysCity> cList = Enow.TZB.BLL.BMSysProvince.GetCityList(new Enow.TZB.Model.MCitySearch { ProvinceId = pID });
                    if (cList != null && cList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < cList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + cList[i].CityId.ToString() + "\",\"name\":\"" + cList[i].Name + "\"},");
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

                case "x":
                    IList<Enow.TZB.BLL.tbl_SysArea> xList =Enow.TZB.BLL.BMSysProvince.GetAreaList(new Enow.TZB.Model.MAreaSearch{CityId=cID});
                    if (xList != null && xList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < xList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + xList[i].CountyId.ToString() + "\",\"name\":\"" + xList[i].Name + "\"},");
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
