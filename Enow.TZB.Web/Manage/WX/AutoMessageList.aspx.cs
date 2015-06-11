using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;
using Weixin.Mp.Sdk.Domain;

namespace Enow.TZB.Web.Manage.WX
{
    /// <summary>
    /// 自动回复管理
    /// </summary>
    public partial class AutoMessageList : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("dotype");
            if (dotype == "delete")
            {
                Utils.RCWE(Delete());
                return;
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitPage();
            }
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        private void InitPage()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];

            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            var list = BWeixinAutoMsg.GetList(ref rowCounts, intPageSize, CurrencyPage);
            if (rowCounts > 0)
            {
                this.phNoData.Visible = false;
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = intPageSize;
                ExportPageInfo1.intRecordCount = rowCounts;
                ExportPageInfo1.CurrencyPage = CurrencyPage;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }
            else {
                this.phNoData.Visible = true;
            }
        }
        /// <summary>
        /// 回复内容
        /// </summary>
        /// <param name="ReplyType"></param>
        /// <param name="ReplyInfo"></param>
        /// <returns></returns>
        protected string ReplyText(Model.EnumType.微信回复类型 ReplyType, string ReplyInfo) {
            string str = "";
            switch (ReplyType) { 
                case Model.EnumType.微信回复类型.图文消息:
                    var list = BWeixinAutoMsg.GetNewsList(ReplyInfo);
                    System.Text.StringBuilder tmpStr = new System.Text.StringBuilder();
                        tmpStr.Append("<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tbody><tr><th>标题</th><th>描述</th><th>图片</th><th>链接网址</th></tr>");
                    if (list != null) {
                        foreach (var model in list) {
                            tmpStr.Append("<tr><td>" + model.Title + "</td><td>" + model.Description + "</td><td><img src=\"" + model.PicUrl + "\" width=\"80px\" height=\"80px\" border=\"0\"></td><td>" + model.Url + "</td></tr>");
                        }
                    }
                    tmpStr.Append("</table>");
                    str = tmpStr.ToString();
                    break;
                default:
                    str = ReplyInfo;
                    break;
            }
            return str;
        }        
        /// <summary>
        /// 删除
        /// </summary>
        private string Delete()
        {
            string id = Utils.GetQueryStringValue("id");
            if (!String.IsNullOrEmpty(id))
            {
                bool value = BWeixinAutoMsg.Delete(id);
                if (value)
                    return UtilsCommons.AjaxReturnJson("1", "删除成功,页面跳转。。。");
                else
                    return UtilsCommons.AjaxReturnJson("0", "删除失败,请重新删除！");
            }
            else { return UtilsCommons.AjaxReturnJson("0", "请选择要删除的信息！"); }
        }
    }
}