using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.AboutWar
{
    public partial class BattlefieldList : System.Web.UI.Page
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
            InitMember();
            if (!IsPostBack)
            {
                
                var action = Utils.GetQueryStringValue("ation").ToLower();
                switch (action)
                {
                    case "queren":
                        UpdateState(true);
                        break;
                    case "chongtian":
                        UpdateState(false);
                        break;
                }
                InitList();
            }
        }
        /// <summary>
        /// 修改战报状态
        /// </summary>
        /// <param name="Astate">true确认  false重填</param>
        private void UpdateState(bool Astate)
        {
            string AID = Utils.GetQueryStringValue("AID");
            var btmodel = BTeamMember.GetModel(MemberId);//球队实体
            var awmodel = BAboutWar.GetModel(AID);//约战信息实体
            string msg = "";
            bool retbol= Getqxbol(btmodel, awmodel, ref msg);
            if (retbol)
            {
                if (Astate)
                {
                    awmodel.AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.双方确认;
                }
                else
                {
                    awmodel.AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.战报重填;
                }
                if (BAboutWar.UpdateAboutState(awmodel))
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "修改成功！"));
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "修改失败！"));
                }

            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", msg));
            }
        }
        /// <summary>
        /// 判断当前用户是否有权限审批
        /// </summary>
        /// <param name="btmodel">球队实体</param>
        /// <param name="awmodel">约战信息实体</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        private bool Getqxbol(dt_TeamMemberList btmodel, tbl_AboutWar awmodel,ref string msg)
        {
            bool ret = false;
            if (btmodel==null)
                msg = "请先加入球队！";
            else if (btmodel.RoleType !=(int)Enow.TZB.Model.EnumType.球员角色.队长)
                msg = "不是队长没有审批权限！";
            else if (awmodel == null)
                msg = "约战信息错误！";
            else if (awmodel.GuestID != TeamId)
                msg = "球队不符没有审批权限！";
            else if (awmodel.AboutState != (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.战报待确认)
                msg = "约战状态不符！";
            else
                ret = true;
            return ret;
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
                var btmodel = BTeamMember.GetModel(MemberId);
                if (btmodel != null && btmodel.RoleType == (int)Enow.TZB.Model.EnumType.球员角色.队长)
                {
                    dzqx = true;
                    TeamId = btmodel.TeamId;
                }
                else
                {
                    TeamId = BAboutWar.GetUserTeam(MemberId);
                }
                if (string.IsNullOrEmpty(TeamId))
                {
                    MessageBox.ShowAndRedirect("请先参加球队", "/WX/Team/");
                }
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RedirectUrl);
                return;
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
                if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
                {
                    int.TryParse(Page, out CurrencyPage);
                    if (CurrencyPage < 1)
                    {
                        CurrencyPage = 1;
                    }
                }

                tbl_AboutWar SearchModel = new tbl_AboutWar();
                SearchModel.title = Utils.InputText(txtGoodsName.Text);
                SearchModel.AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.战报待确认;
                SearchModel.MainID = TeamId;
                var list = BAboutWar.GetAboutWarList(ref rowCounts, intPageSize, CurrencyPage, SearchModel);
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
          
           
            //if (list.Count() <= rowCounts)
            //    this.PlaceHolder1.Visible = false;
        }
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            InitList();
        }
    }
}