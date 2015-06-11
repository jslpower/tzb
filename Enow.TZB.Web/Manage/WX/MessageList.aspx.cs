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
    /// 微信消息列表
    /// </summary>
    public partial class MessageList : System.Web.UI.Page
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
            var list = BWeixinMsg.GetList(ref rowCounts, intPageSize, CurrencyPage);
            if (rowCounts > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = intPageSize;
                ExportPageInfo1.intRecordCount = rowCounts;
                ExportPageInfo1.CurrencyPage = CurrencyPage;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }
        }
        /// <summary>
        /// 留言内容
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="TextMsg">文本内容</param>
        /// <param name="MediaPath">非文本路径</param>
        /// <param name="Format">视频格式</param>
        /// <param name="Recognition">视频编码</param>
        /// <returns></returns>
        protected string GetMsg(int type, string TextMsg, string MediaPath, string Format, string Recognition)
        {
            string str = "";
            switch ((MsgType)type)
            {
                case MsgType.Text:
                    str = TextMsg;
                    break;
                case MsgType.Image:
                    str = "<a href=\"" + MediaPath + "\" target=\"_blank\"><img src=\"" + MediaPath + "\" width=\"100px\" height=\"100px\" border=\"0\" /></a>";
                    break;
                case MsgType.Voice:
                case MsgType.VoiceResult:
                    str = "<a href=\"" + MediaPath + "\" target=\"_blank\">【语音】下载播放</a>";
                    break;
                case MsgType.Video:
                    str = "<a href=\"" + MediaPath + "\" target=\"_blank\">【视频】下载播放</a>";
                    break;
            }
            return str;
        }
    }
}