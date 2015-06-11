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
    public partial class VoteInfo : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            var ation = Utils.GetQueryStringValue("ation").ToLower();
            switch (ation)
            {
                    case "saveadd":
                    Addtoupiao();
                    break;
            }
            if (!IsPostBack)
            {
                Getusermodel();
                InitLoad();
            }
        }
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
        private void InitLoad()
        {
            var id = Utils.GetQueryStringValue("Vid");
            string strmsg = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                var model = BVote.GetModel(id);
                if (model!=null&&model.VRelease==((int)Model.发布对象.微信))
                {
                    ltrGoodsName.Text = model.Vtitle;
                    litltxt.Text = model.Remarks;
                    rptList.DataSource = BVoteOption.GettypList(model.Vid);
                    rptList.DataBind();
                }
                else
                {
                    strmsg = "投票信息编号不正确！请重新选择！";
                }
            }
            else
            {
                //未找到您的用户信息!
                strmsg = "该投票信息不存在！请重新选择！";
            }
            if (!string.IsNullOrEmpty(strmsg))
            {
                MessageBox.ShowAndRedirect(strmsg, "Defaut.aspx");
                return;
            }
        }
        /// <summary>
        /// 添加投票信息
        /// </summary>
        private void Addtoupiao()
        {
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel != null)
            {
                var model = BMember.GetModelByOpenId(AuthModel.OpenId);
                if (model!=null)
                {
                    MemberId = model.Id;
                }
                
            }
            var VoteId = Utils.GetQueryStringValue("VoteID");
            var OPid = Utils.GetQueryStringValue("OPid");
            
            if (string.IsNullOrEmpty(VoteId))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "当前投票信息不正确！"));
            }
            if (string.IsNullOrEmpty(OPid))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "该选项不存在！"));
            }
            //投票信息实体
            var Votemodel = BVote.GetModel(VoteId);
            if (Votemodel == null && Votemodel.VRelease!=((int)Model.发布对象.微信))
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "该投票信息不存在！"));
            if (Votemodel.ExpireTime<DateTime.Now)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "该投票已过期！"));
            //用户投票实体
            var VoteUser = new tbl_VoteUser()
            {
                Uid = Guid.NewGuid().ToString(),
                VoteInfoId = VoteId,
                VoteOptionId = OPid,
                UserId = MemberId,
                UsVoteIsDelete = 0,
                AwardsNum = 0,
                UIndatetime = DateTime.Now
            };
            bool retbol = false;//成功与否返回值
            //判断当前投票类型
            if (Votemodel.Vtype == 1)//普通投票
            {
                retbol = true;
            }
            else if (Votemodel.Vtype == 2  && !string.IsNullOrEmpty(MemberId))//铁丝投票
            {
                 retbol = BVoteUser.GetVoteBool(VoteId, MemberId);
            }
            else if (Votemodel.Vtype == 3 && !string.IsNullOrEmpty(MemberId))//主体投票
            {
                bool cansaibool = false;
                if (Votemodel.SubjectType==1)
                {
                   var appuser=BApplicants.GetUsbool(Votemodel.MatchId, MemberId);
                   if (appuser!=null&&appuser.IsState==((int)Enow.TZB.Model.EnumType.ApplicantsStartEnum.已通过))
                   {
                       retbol = true;  
                   }
                }
                else
                {
                    cansaibool = BMatchTeamMember.GetMemberMatchModelBool(MemberId, Votemodel.MatchId);
                }
                if (cansaibool==false)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "未参与比赛或活动无法参与投票！"));
                }
                retbol = BVoteUser.GetVoteBool(VoteId, MemberId);
            }
            if (retbol)
            {
                if (!BVoteOption.UpdateNum(OPid))
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "该选项不存在！"));
                }
                if (Votemodel.Vtype != 1)
                {
                    BVoteUser.Add(VoteUser);
                }
                
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "投票成功！"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "您已投过票！"));
            }
        }
    }
}