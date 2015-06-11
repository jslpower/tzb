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
    public partial class TeamArticleView : System.Web.UI.Page
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
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitModel(OpenId);


             
                string articleid = Request.QueryString["Id"];
                if (!String.IsNullOrWhiteSpace(articleid))
                {
                    ArticleId = articleid;
                    InitPage(articleid);
                   
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
          
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitModel(string OpenId)
        {
            var MemberModel = BMember.GetModelByOpenId(OpenId);
            if (MemberModel != null)
            {

                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);

            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
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
       


    }
}