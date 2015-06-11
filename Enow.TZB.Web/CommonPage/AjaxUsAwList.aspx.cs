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
    public partial class AjaxUsAwList : System.Web.UI.Page
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
        private bool dzqx = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Getusermodel();
                InitList();
            }
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        private void Getusermodel()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel != null)
            {
                string OpenId = AuthModel.OpenId;
                InitMember(OpenId);
            }
            else
            {
                Response.End();
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
        }
        
        #region 约战状态信息
        /// <summary>
        /// 是否发起者
        /// </summary>
        /// <param name="qdteamid">球队ID</param>TeamId
        /// <returns></returns>
        protected string Getyztitle(string qdteamid)
        {
            //发起者
            if (qdteamid == TeamId)
            {
                return "<span class=\"green\">发起的约战</span>";
            }
            else
            {
                return "<span class=\"red\">收到的约战</span>";
            }
        }
        protected string GetAState(string AboutState, string Wtypes, string Wstates)
        {
            string str = "<p>状态：<span style=\"color: red;\">" + (Enow.TZB.Model.EnumType.GathersEnum.约战状态)(Enow.TZB.Utility.Utils.GetInt(AboutState)) + "</span></p>";

            if (Wtypes == "2")
            {
                if (Wstates != ((int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.进行中).ToString() && Wstates != ((int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.终止).ToString())
                {
                    str = "<p>状态：<span style=\"color: red;\">" + (Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态)(Enow.TZB.Utility.Utils.GetInt(Wstates)) + "</span></p>";
                }

            }
            return str;

        }
        #endregion
        #region 处理约战权限
        /// <summary>
        /// 约战操作
        /// </summary>
        /// <param name="qtWtypes">分类 1 主队  2客队</param>
        /// <param name="qtTeamId">球队编号</param>
        /// <param name="qtAboutState">约战状态</param>
        /// <param name="qtWstates">球队约战状态</param>
        /// <returns></returns>
        protected string Gettzqx(string qtWtypes, string qtTeamId, string qtAboutState, string qtWstates)
        {
            string str = "<a href=\"javascript:void(0);\" class=\"basic_btn jjbtn\">拒绝</a><a href=\"javascript:void(0);\"  class=\"basic_ybtn yzbtn\">应战</a>";
            if (!dzqx)
                str = "";
            if (qtAboutState != ((int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.约战中).ToString())
                str = "";
            if (qtWtypes == "1")
                str = "";
            if (TeamId == qtTeamId)
                str = "";
            if (qtWstates != ((int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.待确认).ToString())
                str = "";
            return str;
        }
        #endregion

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

                dt_AboutWarBallTeam SearchModel = new dt_AboutWarBallTeam();
                SearchModel.title = Utils.InputText(title);
                //SearchModel.AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.约战中;
                SearchModel.TeamId = TeamId;
                SearchModel.Wtypes = 2;
                SearchModel.Wstates = -1;
                SearchModel.Atypes = -1;
                var list = BAboutWar.GetList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
                if (list.Count() > 0)
                {
                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                }  
            }
            else
            {
                Response.End();
            }
      
        }
    }
}