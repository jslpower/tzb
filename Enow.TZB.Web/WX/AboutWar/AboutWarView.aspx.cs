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
    public partial class AboutWarView : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            var ation = Utils.GetQueryStringValue("ation").ToString();
            switch (ation)
            {
                case "inter":
                    InterModel();
                    break;
            }
            if (!IsPostBack)
            {
                Getusermodel();
                GetModel();
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
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        private string Getusid()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel!=null)
            {
                var model = BMember.GetModelByOpenId(AuthModel.OpenId);
                if (model != null)
                {
                    return model.Id;
                }
            }
            return "";
        }
        /// <summary>
        /// 初始化球队信息
        /// </summary>
        private void GetModel()
        {
            var Id = Utils.GetQueryStringValue("ID");
            var model= BAboutWar.GetModel(Id);
            if (model!=null)
            {
                litTeamName.Text = model.title;

               //主队信息
               var zhuduimodel=BAboutWar.GetQdid(model.MainID);
               if (zhuduimodel != null)
               {
                   litzkdname.Text = "<div>主队：" + zhuduimodel.TeamName+ "</div>";
               }
               //客队信息
               if (!string.IsNullOrEmpty(model.GuestID))
               {
                   //客队信息
                   var keduimodel = BAboutWar.GetQdid(model.MainID);
                   if (keduimodel != null)
                   {
                       litzkdname.Text += "<div>客队：" + keduimodel.TeamName + "</div>";
                   }
               }
               
               litTime.Text = model.AboutTime.ToString();//时间
               Literal1.Text = model.CourtName;//球场
               litPlace.Text = model.Address;//地址
               litzhanshu.Text = model.Afternoon;//战书
               litType.Text = model.Format != 100 ? model.Format + "人制" : "其他";
               litFee.Text = ((Enow.TZB.Model.EnumType.GathersEnum.比赛费用)model.Costnum).ToString();
               litState.Text = ((Enow.TZB.Model.EnumType.GathersEnum.约战状态)model.AboutState).ToString();

            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        
        }
        private void InterModel()
        {
            var AID = Utils.GetQueryStringValue("AID");
            if (!string.IsNullOrEmpty(AID))
            {
                string usid = Getusid();
                var ballmodel = BTeamMember.GetModel(usid);
                if (ballmodel == null)
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请先参加球队！"));
                var model = BAboutWar.GetModel(AID);
                if (model == null)
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "该信息不存在！"));
                if (DateTime.Now> model.AboutTime)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "已过约战时间！"));
                if (model.AboutState!=(int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.约战中)
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "该约战已开始或结束！"));
                if (ballmodel.RoleType != (int)Enow.TZB.Model.EnumType.球员角色.队长)
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "只有队长才能报名！"));
                if (model.MainID == ballmodel.TeamId)
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "主队不需重复报名此约战！"));
                if(BAboutWarReport.Getboolyuezhan(AID, ballmodel.Id))
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "已报名此约战！"));
                var yzmodel = new tbl_AboutWarReport()
                {
                    Inserttime = DateTime.Now,
                    AboutWarID = AID,
                    RIsDelete = 0,
                    TeamId = ballmodel.TeamId,
                    Wtypes = 2,
                    Wid = Guid.NewGuid().ToString(),
                    Wstates = (int)Enow.TZB.Model.EnumType.GathersEnum.主客队约战状态.待确认
                };
                BAboutWarReport.Add(yzmodel);
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "约战成功！等待对方确认!"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "约战信息不正确！"));
            }
        }
    }
}