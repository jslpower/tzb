using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using System.Web.UI.HtmlControls;

namespace Enow.TZB.Web.WX.Member
{
    public partial class ArticleView : System.Web.UI.Page
    {

        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WxPageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected string ArticleId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitModel();


                string doType = Request.QueryString["doType"];
                switch (doType)
                {

                    case "greetfast": GreetFast(); break;
                    default: break;
                }
                string articleid = Request.QueryString["id"];
                if (!String.IsNullOrWhiteSpace(articleid))
                {
                    ArticleId = articleid;
                    InitPage(articleid);
                    InitLeaveWordList(articleid);
                }
                else
                {
                    //未找到您要查看的信息！
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
          
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        private void InitModel()
        {
            var MemberModel = BMemberApp.GetUserModel();
            if (MemberModel != null)
            {

                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);

            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
     
        private void GreetFast()
        {
            string s = Request.QueryString["id"];


            int bllRetCode = BArticle.UpdateView(s, TZB.Model.发布对象.点赞) == true ? 1 : -99;

            if (bllRetCode == 1) {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "成功！")); 
            }
            else
            {
                //MessageBox.ShowAndRedirect("", "ArticleView.aspx");
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-99", "失败！"));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleid"></param>
        private void InitPage(string articleid)
        {
            var model = BArticle.GetModel(articleid);
            if (model != null)
            {
                ltrTitle.Text = model.ArticleTitle;
                ltrIssueTime.Text = model.IssueTime.ToString();
                if (!String.IsNullOrEmpty(model.ArticlePhoto)) {
                    this.ltrImg.Text = "<img src=\"" + model.ArticlePhoto+   "\" width=\"100%\" />";
                }
                ltrContent.Text = model.ArticleInfo;

            }
        }
        protected string GetMemberHeadPhoto(string memberid)
        {
            return BMemberWeiXin.GetMemberHeadPhoto(memberid);
        }
         /// <summary>
        /// 加载回复列表
        /// </summary>
        private void InitReplyList( Repeater rptReplyList, string wid)
        {
            var list = BArticleLeaveWord.GetReplyList(wid);
            if (list.Count() > 0)
            {
                rptReplyList.DataSource = list;
                rptReplyList.DataBind();
            }
        }
        /// <summary>
        /// 加载留言列表
        /// </summary>
        private void InitLeaveWordList(string articleid )
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
            Model.MArticleLeaveSearch SearchModel = new Model.MArticleLeaveSearch();
            SearchModel.MemberId = MemberId;
            SearchModel.IsEnable = true;
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                SearchModel.KeyWords = Server.UrlDecode(KeyWord.Trim());
            }

            var list = BArticleLeaveWord.GetList(ref rowCounts, intPageSize, CurrencyPage, articleid, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            this.rptList.DataSource = list;
            this.rptList.DataBind();
        }

        protected void ReplyDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                Repeater rptReplyList = (Repeater)e.Item.FindControl("rptReplyList");
                tbl_ArticleLeaveWord row = (tbl_ArticleLeaveWord)e.Item.DataItem;
                var ArticleId = row.Id;
                row = null;
                InitReplyList(rptReplyList,ArticleId);
            }
        }

    }
}