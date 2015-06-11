using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.My
{
    public partial class Notice : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int PageSize = 10;
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Master.Page.Title = "消息中心";
                if (BWebMemberAuth.IsLoginCheck())
                {
                    var model = BWebMemberAuth.GetUserModel();
                    string MemberId = model.Id;
                    InitMember(MemberId);
                }
                else
                {
                    MessageBox.ShowAndRedirect("请登录!", "/Default.aspx");
                }
            }
        }

        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="MemberId"></param>
        private void InitMember(string MemberId)
        {
            var model = BMember.GetModel(MemberId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/My/Update.aspx");
                    return;
                }
                InitList(MemberId);
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "/Register/Default.aspx");
                return;
            }
        }

        /// <summary>
        /// 加载消息列表
        /// </summary>
        /// <param name="MemberId"></param>
        private void InitList(string MemberId)
        {
            int rowCounts = 0;
            PageIndex = UtilsCommons.GetPadingIndex();
            Model.MMesageSearch SearchModel = new Model.MMesageSearch();
            SearchModel.ReceiveId = MemberId;
            var list = BMessage.GetList(ref rowCounts, PageSize, PageIndex, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = PageSize;
                ExportPageInfo1.intRecordCount = rowCounts;
                ExportPageInfo1.CurrencyPage = PageIndex;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }
        }
    }
}