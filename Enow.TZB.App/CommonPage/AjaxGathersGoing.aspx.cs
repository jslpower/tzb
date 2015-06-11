using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.CommonPage
{
    public partial class AjaxGathersGoing : System.Web.UI.Page
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
        /// <summary>
        /// 球队编号
        /// </summary>
        protected string TeamId = "";
        /// <summary>
        /// 判断是否队长
        /// </summary>
        protected bool dzqx = false;
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
        /// <param name="OpenId"></param>
        private void InitMember()
        {
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    Response.End();
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
                var btmodel = BTeamMember.GetModel(MemberId);
                if (btmodel != null && btmodel.RoleType == (int)Enow.TZB.Model.EnumType.球员角色.队长)
                {
                    dzqx = true;
                    TeamId = btmodel.Id;
                }
                else
                {
                    TeamId = BAboutWar.GetUserTeam(MemberId);
                }
            }
            else
            {
                Response.End();
            }
        }
        /// <summary>
        /// 加载商品列表
        /// </summary>
        private void InitList()
        {
            if (!string.IsNullOrEmpty(TeamId))
            {
                int rowCounts = 0;
                string Page = Request.QueryString["Page"];
                string title = Utils.GetQueryStringValue("title");
                if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
                {
                    int.TryParse(Page, out CurrencyPage);
                    if (CurrencyPage < 1)
                    {
                        CurrencyPage = 1;
                    }
                }

                tbl_AboutWar SearchModel = new tbl_AboutWar();
                SearchModel.title = Utils.InputText(title);
                SearchModel.AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.进行中;
                SearchModel.MainID = TeamId;
                var list = BAboutWar.GetAboutWarList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
                if (list.Count() > 0)
                {
                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                }
            }
           
            //if (list.Count() <= rowCounts)
            //    this.PlaceHolder1.Visible = false;
        }
    }
}