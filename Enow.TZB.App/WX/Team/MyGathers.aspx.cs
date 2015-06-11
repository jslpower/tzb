using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Team
{
    public partial class MyGathers : System.Web.UI.Page
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

        string Page ="";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string doType = Request.QueryString["doType"];
            switch (doType)
            {
                case "war": AcceptWar(); break;
            }
            Page = Request.QueryString["Page"];
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            InitMember(OpenId);
            if (!IsPostBack)
            {
                
                BMemberAuth.LoginCheck();
               
                InitList();
                InitAcceptList();
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
        /// 加载我发起的约战列表
        /// </summary>
        private void InitList()
        {
            int rowCounts = 0;
           
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            string TeamId = Model.MCommon.DefaultGuidId;
            var TeamModel = BTeamMember.GetModel(MemberId);
            if (TeamModel != null)
            {
                TeamId = TeamModel.TeamId;
            }
            Model.MGathersSearch SearchModel = new Model.MGathersSearch();
            SearchModel.MemberId = MemberId;
            SearchModel.TeamId = TeamId;
            SearchModel.IsAcceptWar = false;
            SearchModel.IsGatherResult = false;

            if (!String.IsNullOrWhiteSpace(txtTeamName.Text))
            {
                SearchModel.Name = Server.UrlDecode(txtTeamName.Text.Trim());
            }
            var list =BGathers.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            //else
            //{
            //    this.PlaceHolder1.Visible = false;
            //}
        }
        private void AcceptWar()
        {
            string gatherid = Utils.GetQueryStringValue("id");


            int bllRetCode = BGathers.AccepWar(gatherid) == true ? 1 : -99;

            if (bllRetCode == 1)
            {
                //MessageBox.ShowAndRedirect("", "Articles.aspx");
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "应战成功！"));
            }
            else
            {
                //MessageBox.ShowAndRedirect("", "Articles.aspx"); 
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-99", "应战失败！"));
            }
        }
          /// <summary>
        /// 加载我收到的约战列表
        /// </summary>
        private void InitAcceptList()
        {
            int rowCounts = 0;

            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            string TeamId = Model.MCommon.DefaultGuidId;
            var TeamModel = BTeamMember.GetModel(MemberId);
            string AcceptTeamName = "";
            if (TeamModel != null)
            {
                TeamId = TeamModel.TeamId;
                AcceptTeamName=TeamModel.TeamName;
            }
            Model.MGathersSearch SearchModel = new Model.MGathersSearch();
         
            SearchModel.AcceptTeamId = TeamId;
            SearchModel.IsAcceptWar = false;
            SearchModel.IsGatherResult = false;

            SearchModel.AcceptTeamName = AcceptTeamName;
            if (!String.IsNullOrWhiteSpace(txtTeamName.Text))
            {
                SearchModel.Name = Server.UrlDecode(txtTeamName.Text.Trim());
            }

            var acceptList = BGathers.GetAcceptList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (acceptList.Count() > 0)
            {
                this.rptAcceptList.DataSource = acceptList;
                this.rptAcceptList.DataBind();
            }
            //else
            //{
            //    this.PlaceHolder1.Visible = false;
            //}
        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            InitList();
            InitAcceptList();
        }
    }
}