using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.CommonPage
{
	public partial class AjaxPatteamList : System.Web.UI.Page
	{
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WxPageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;

        //图片裁剪后保存的文件夹
        protected const string DIRPATH = "/ufiles/";
        private string MemberID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Gethuiyuan();
                InitList();
            }
        }
        public void Gethuiyuan()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel==null)
            {
                Response.End();
            }
            string OpenId = AuthModel.OpenId;
            InitMember(OpenId);
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
                    Response.End();
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberID = model.Id;
            }
            else
            {
                Response.End();
            }
        }
        /// <summary>
        /// 加载球队列表
        /// </summary>
        private void InitList()
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
            Model.MBallTeamSearch SearchModel = new Model.MBallTeamSearch();
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            if (!String.IsNullOrWhiteSpace(KeyWord))
            {
                SearchModel.KeyWord = Server.UrlDecode(KeyWord.Trim());
            }
            SearchModel.State = Model.EnumType.球队审核状态.终审通过;
            SearchModel.OfferpatMemberID = MemberID;
            var list = BOfferpat.GetqdList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }
	}
}