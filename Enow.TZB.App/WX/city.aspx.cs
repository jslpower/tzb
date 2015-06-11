using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;
using Newtonsoft.Json;
using System.Text;

namespace Enow.TZB.Web.WX
{
    public partial class city : System.Web.UI.Page
    {
        protected string fhtxt = "";
        protected string action = "";
        protected string fhdz = "";
        protected string url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Geturl();
            if (!IsPostBack)
            {
                Getcitylist();
            }
        }
        private void Getcitylist()
        {
            var cityname = Utils.GetQueryStringValue("CityName");
            if (!string.IsNullOrEmpty(cityname))
            {
                rptList.DataSource = BCity.Getnamesel(cityname);
                rptList.DataBind();
            }
         
        }
        /// <summary>
        /// 组织返回URL
        /// </summary>
        private void Geturl()
        {
            action = Utils.GetQueryStringValue("action");
            fhdz = Utils.GetQueryStringValue("fhdz");
            url = "/WX/" + action + "/" + fhdz + ".aspx";
        }
        /// <summary>
        /// 序列化
        /// </summary>
        private void Getjson()
        {
            var cty = BCity.Getctyszm();
            cty.Add(new tbl_SysCity() { JP = "X", ProvinceId = 988, Name = "香港特别行政区", CityId = 29383 });
            cty.Add(new tbl_SysCity() { JP = "T", ProvinceId = 989, Name = "天津市", CityId = 29383 });
            cty.Add(new tbl_SysCity() { JP = "Y", ProvinceId = 990, Name = "银川", CityId = 29383 });
            string[] str = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            StringBuilder strlist = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                strlist.Append("<li class=\"city_li\">");
                strlist.Append("<div class=\"city_group_title\">");
                strlist.Append(str[i]);
                strlist.Append("</div>");
                strlist.Append("<ul class=\"city_group_box\">");
                foreach (var item in cty)
                {
                    if (item.JP==str[i])
                    {
                        strlist.Append("<li class=\"tiaozhan\" data-cid=\""+item.CityId+"\">"+item.Name+"</li>");
                     
                    }
                }
                strlist.Append("</ul>");
                strlist.Append("</li>");
            }
            fhtxt = strlist.ToString();
        }
        private void UpdateSZM()
        {
            var cty =BCity.Getctyszm();
            for (int i = 0; i < cty.Count; i++)
            {
                cty[i].JP = BCity.GetPYChar(cty[i].Name);
            }
            BCity.Ipdatectyname(cty);
        }
    }
}