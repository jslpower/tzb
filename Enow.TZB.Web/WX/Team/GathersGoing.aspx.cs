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
    public partial class GathersGoing : System.Web.UI.Page
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
            Page = Request.QueryString["Page"];

            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            InitMember(OpenId);

            if (!IsPostBack)
            {
              
                BMemberAuth.LoginCheck();
              
               
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
                AcceptTeamName = TeamModel.TeamName;
            }
            Model.MGathersSearch SearchModel = new Model.MGathersSearch();

            SearchModel.AcceptTeamId = TeamId;
            SearchModel.IsAcceptWar = true;
            SearchModel.IsGatherResult = false;
            SearchModel.AcceptTeamName = AcceptTeamName;
            if (!String.IsNullOrWhiteSpace(txtTeamName.Text))
            {
                SearchModel.Name = Server.UrlDecode(txtTeamName.Text.Trim());
            }
            var list = BGathers.GetAcceptList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptAcceptList.DataSource = list;
                this.rptAcceptList.DataBind();
            }
            //else
            //{
            //    this.PlaceHolder1.Visible = false;
            //}
        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            InitAcceptList();
        }
    }
}