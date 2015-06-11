using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Member
{
    /// <summary>
    /// 我的日志列表页
    /// </summary>
    public partial class TeamArticles : System.Web.UI.Page
    {
        protected string dhurl = "";
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
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Request.QueryString["doType"];
            switch (doType)
            {

                case "greet": Greet(); break;

                default: break;
            }
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitMember(OpenId);
                InitLogList();
            }
        }

        private void Greet()
        {
            string s = Utils.GetQueryStringValue("id");


            int bllRetCode = BArticle.UpdateView(s, TZB.Model.发布对象.点赞) == true ? 1 : -99;

            if (bllRetCode == 1)
            {
                //MessageBox.ShowAndRedirect("", "Articles.aspx");
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "成功！"));
            }
            else
            {
                //MessageBox.ShowAndRedirect("", "Articles.aspx"); 
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-99", "失败！"));
            }

        }

        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember(string OpenId)
        {
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 加载我的日志列表
        /// </summary>
        private void InitLogList()
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
            string TeamId = Model.MCommon.DefaultGuidId;
            var teamid = Utils.GetQueryStringValue("TeamId");
            if (!string.IsNullOrEmpty(teamid))
            {
                TeamId = teamid;
                UserHome1.Userhometitle = "球队信息";
                dhurl = "?TeamId=" + teamid;
            }
            else
            {
                var TeamModel = BTeamMember.GetModel(MemberId);
                if (TeamModel != null)
                {
                    TeamId = TeamModel.TeamId;
                }
                UserHome1.Userhometitle = "我的球队";
            }
           
            Model.MMemberArticleSearch SearchModel = new Model.MMemberArticleSearch();
            //SearchModel.MemberId = MemberId;
            SearchModel.TeamId = TeamId;
            SearchModel.IsEnable = true;
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                SearchModel.KeyWords = Server.UrlDecode(KeyWord.Trim());
            }
            SearchModel.TypeId = 0;
            var list = BArticle.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptLogList.DataSource = list;
                this.rptLogList.DataBind();
            }
            else
            {
                this.PlaceHolder1.Visible = false;
            }
        }
    }
}