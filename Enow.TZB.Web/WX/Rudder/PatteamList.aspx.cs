using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Rudder
{
    public partial class PatteamList : System.Web.UI.Page
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
        /// <summary>
        /// 会员关注列表
        /// </summary>
        private List<string> strlist = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Getgzlist();
            if (!IsPostBack)
            {
                Gethuiyuan();
                InitList();
            }
        }
        #region 会员关注列表信息
        /// <summary>
        /// 获取用户关注列表
        /// </summary>
        private void Getgzlist()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            var model = BMember.GetModelByOpenId(AuthModel.OpenId);
            strlist = BOfferpat.GetStrlist(model.Id, 3);
        }
        /// <summary>
        /// 查询是否已关注
        /// </summary>
        /// <param name="Usid"></param>
        /// <returns></returns>
        protected string Selgzyf(string Usid)
        {
            if (strlist != null && strlist.Contains(Usid))
            {
                return "取消关注";
            }
            return "关注";

        }
        #endregion
        public void Gethuiyuan()
        {
            BMemberAuth.LoginCheck();
            var AuthModel = BMemberAuth.GetUserModel();
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
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberID = model.Id;
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
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
                txtKeyWord.Value = Server.UrlDecode(KeyWord.Trim());
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
            else
            {
                this.PlaceHolder1.Visible = false;
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string KeyWords = Utils.GetFormValue("txtKeyWord");
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&KeyWord=" + Server.UrlEncode(KeyWords.Trim()), true);
        }        
    }
}