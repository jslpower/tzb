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
    public partial class GathersResult : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Getusermodel();
                Initload();
            }
        }
        protected void Initload()
        {
            string AID = Utils.GetQueryStringValue("AID");
            var model = BAboutWar.GetModel(AID);//约战信息实体
            if (model!=null)
            {
                //判断是否有权限查看或填写战报
                if (model.MainID != TeamId && model.GuestID != TeamId)
                {
                  Response.Redirect("GathersGoing.aspx");  
                }
                if (model.AboutState != (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.进行中 && model.AboutState != (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.战报重填 && model.AboutState != (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.战报待确认)
                {
                    Response.Redirect("GathersGoing.aspx"); 
                }
                txtZTeam.Text = model.MainName;
                txtZTeam.Enabled = false;
                txtCTeam.Text = model.GuestName;
                txtCTeam.Enabled = false;
                txtZUp.Text = model.MainSnum;
                txtZDown.Text = model.MainXnum;
                txtCUp.Text = model.GuestSnum;
                txtCDown.Text = model.GuestXnum;
                txtGatherInfo.Text = model.AWContent;
                string ation = Utils.GetQueryStringValue("dotype");
                if (ation=="sel")
                {
                    txtZUp.Enabled = false;
                    txtZDown.Enabled = false;
                    txtCUp.Enabled = false;
                    txtCDown.Enabled = false;
                    txtGatherInfo.Enabled = false;
                    btnSave.Visible = false;
                    litbtn.Text = "<a href=\"javascript:void(0);\" class=\"basic_btn qrbtn\">确认</a><a href=\"javascript:void(0);\" class=\"basic_ybtn ctbtn\" style=\"margin-top: 10px;\">重写战报</a>";
                }
                
            }
            else
            {
                Response.Redirect("GathersGoing.aspx");
            }
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        private void Getusermodel()
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
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
                TeamId = BAboutWar.GetUserTeam(MemberId);
                if (string.IsNullOrEmpty(TeamId))
                {
                    MessageBox.ShowAndRedirect("请先参加球队", "/WX/Member/");
                    return;
                }
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 保存战报信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string AID = Utils.GetQueryStringValue("AID");//约战编号
            if (!string.IsNullOrEmpty(AID))
            {
                Getusermodel();
                var ballmodel = BTeamMember.GetModel(MemberId);//球队实体
                if (ballmodel==null)
                {
                   MessageBox.ShowAndRedirect("请先加入球队！", "/WX/Member/");
                    return; 
                }
                if (ballmodel.RoleType !=(int)Enow.TZB.Model.EnumType.球员角色.队长)
                {
                    MessageBox.ShowAndRedirect("只有队长才能填写战报！", "GathersGoing.aspx");
                    return;
                }
                var model = BAboutWar.GetModel(AID);//约战信息实体
                if (model == null)
                {
                    MessageBox.ShowAndRedirect("约战信息错误！", "GathersGoing.aspx");
                    return;
                }

                if (model.AboutState != (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.进行中 && model.AboutState != (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.战报重填)
                {
                    MessageBox.ShowAndRedirect("约战信息不正确！", "GathersGoing.aspx");
                    return;
                }
                if (model.Zbstate == 2)
                {
                    MessageBox.ShowAndRedirect("战报已确认不能重复填写！", "GathersGoing.aspx");
                    return;
                }
                model.Uptime = DateTime.Now;
                model.MainSnum = Utils.GetFormValue(txtZUp.UniqueID);
                model.MainXnum = Utils.GetFormValue(txtZDown.UniqueID);
                model.GuestSnum = Utils.GetFormValue(txtCUp.UniqueID);
                model.GuestXnum = Utils.GetFormValue(txtCDown.UniqueID);
                model.AWContent = Utils.GetFormValue(txtGatherInfo.UniqueID);
                model.AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.战报待确认;
                if (BAboutWar.Update(model))
                {
                    MessageBox.ShowAndRedirect("填写成功！", "BattlefieldList.aspx");
                }
                else
                {
                    MessageBox.ShowAndRedirect("填写失败！", "GathersGoing.aspx");
                }
            }
            else
            {
                MessageBox.ShowAndRedirect("约战信息失败！", "GathersGoing.aspx");
                return;
            }
        }
    }
}