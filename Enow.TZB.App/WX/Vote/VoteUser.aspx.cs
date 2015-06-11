using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Vote
{
    public partial class VoteUser : System.Web.UI.Page
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
        public string flTitle = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Getusermodel();
                InitList();
            }
        }
        private void Getusermodel()
        {
            BMemberApp.LoginCheck();
            InitMember();
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
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
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 加载商品列表
        /// </summary>
        private void InitList()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            int types = Utils.GetInt(Utils.GetQueryStringValue("types"), 1);
            types = types > 2 ? 1 : types;
            flTitle = types == 1 ? "用户投票信息" : "用户中奖信息";
            UserHome1.Userhometitle = flTitle;
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            Model.MVoteQuery SearchModel = new Model.MVoteQuery();
            SearchModel.title = Utils.GetFormValue(txtGoodsName.UniqueID);
            SearchModel.UserID = MemberId;
            SearchModel.ColumnID = types;
            SearchModel.Release = (int)Model.发布对象.APP;
            var list = BVoteUser.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            else
            {
                PlaceHolder1.Visible = false;
            }
            if (list.Count() <= 0)
            {
                if (types == 1)
                {
                    MessageBox.ShowAndRedirect("当前没有投票信息", "Defaut.aspx");
                }
                else
                {
                    MessageBox.ShowAndRedirect("当前没有中奖信息", "LotteryList.aspx");
                }
            }
        }
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            InitList();
        }
    }
}