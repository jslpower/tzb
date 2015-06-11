using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Member
{
    public partial class ActivityList : System.Web.UI.Page
    {
        public string Aptitle = "";
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitMember();
                InitList();
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        private void InitMember()
        {
            var model = BMemberApp.GetUserModel();
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
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
        /// <summary>
        /// 加载商品列表
        /// </summary>
        private void InitList()
        {
            if (string.IsNullOrEmpty(MemberId))
            {
                 //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            int typeid = 2;
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("types")))
            {

            }
            else if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("type")))
            {
                typeid = Utils.GetInt(Utils.GetQueryStringValue("type"), 2);
            }
            if (typeid <= 4)
            {
                Aptitle = ((Enow.TZB.Model.EnumType.ActivityEnum)(typeid)).ToString();
                UserHome1.Userhometitle = Aptitle;
            }
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            Model.MApplicants SearchModel = new Model.MApplicants();
            SearchModel.Acttypes = typeid;
            SearchModel.Acttitle = Utils.InputText(txtGoodsName.Text);
            SearchModel.USid = MemberId;
            SearchModel.ActivityId = -1;
            SearchModel.fbmb = (int)Enow.TZB.Model.EnumType.ReleaseEnum.微信;
            var list =BApplicants.GetAcpUserList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            else
            {
                PlaceHolder1.Visible = false;
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect("当前未预约或报名活动!请先预约或报名!", ("TZGathering.aspx?type=" + typeid));
                return;
            }
        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            InitList();
        }
    }
}