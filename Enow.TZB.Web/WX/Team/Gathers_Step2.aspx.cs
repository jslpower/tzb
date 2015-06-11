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
    public partial class Gathers_Step2 : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected Model.EnumType.球员角色 MemberRole = Model.EnumType.球员角色.队员;
        protected Model.EnumType.球队审核状态 TeamState = Model.EnumType.球队审核状态.审核中;
        protected int MeberSQWZ = 0;
        string TeamId = Utils.GetQueryStringValue("teamid");
        string memberid = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitModel(OpenId);

            }
            memberid = hdMemberId.Value;
        }
        /// <summary>
        /// 加载球队基本信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitModel(string OpenId)
        {
            var MemberModel = BMember.GetModelByOpenId(OpenId);
            if (MemberModel != null)
            {
                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                MemberId = MemberModel.Id;
                string TeamId = "";
                //查询自己所在的球队
                var TeamModel = BTeamMember.GetModel(MemberId);
                if (TeamModel != null)
                {
                    TeamId = TeamModel.TeamId;
                   
                    var model = BTeam.GetModel(TeamId);
                    if (model != null)
                    {
                        string ErrMsg = "";
                        string Url = "";
                        TeamState = (Model.EnumType.球队审核状态)model.State;
                        switch (model.State)
                        {
                            case (int)Model.EnumType.球队审核状态.审核中:
                            case (int)Model.EnumType.球队审核状态.初审通过:
                                Url = "/WX/Member/Default.aspx";
                                ErrMsg = CacheSysMsg.GetMsg(5);
                                break;
                            case (int)Model.EnumType.球队审核状态.初审拒绝:
                            case (int)Model.EnumType.球队审核状态.终审拒绝:
                                Url = "/WX/Team/TeamUpdate.aspx";
                                ErrMsg = CacheSysMsg.GetMsg(46);
                                break;
                            case (int)Model.EnumType.球队审核状态.解散申请:
                                Url = "/WX/Member/Default.aspx";
                                ErrMsg = CacheSysMsg.GetMsg(45);
                                break;
                            case (int)Model.EnumType.球队审核状态.解散通过:
                                Url = "/WX/Member/Default.aspx";
                                ErrMsg = CacheSysMsg.GetMsg(7);
                                break;
                           
                        }
                        if (!String.IsNullOrWhiteSpace(ErrMsg))
                        {
                            MessageBox.ShowAndRedirect(ErrMsg, Url);
                            return;
                        }
                        InitList(TeamId);

                        
                    }
                    else
                    {
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(6));
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(7), "Default.aspx");
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 加载成员列表
        /// </summary>
        /// <param name="TeamId"></param>
        private void InitList(string TeamId)
        {
            var list = BTeamMember.GetList(TeamId);
            if (list != null)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }
        /// <summary>
        /// 绑定球员列表操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitOperation(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = (dt_TeamMember)e.Item.DataItem;
                Literal ltrOperation = (Literal)e.Item.FindControl("ltrOperation");
                if (TeamState == Model.EnumType.球队审核状态.终审通过)
                {
                    if (row.RoleType == (int)Model.EnumType.球员角色.队长)
                    {
                        //如果是队长，不可选
                        ltrOperation.Text = " <div class=\"Rbtn padd01\"><a href=\"#\" data="+row.MemberId+" class=\"green\">必战 </a>" +
            "</div>";


                    }
                    else
                    {
                        ltrOperation.Text = " <div class=\"Rbtn padd01\"> <a href=\"#\" data=" + row.MemberId + "  class=\"gray\">未出战</a>" +
                "</div>";
                 
                    }
                   
                }
               
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string gatherid = Utils.GetQueryStringValue("gatherid");
            if(!string.IsNullOrEmpty(gatherid) )
            {
                
                string[] members = null;
                if (!string.IsNullOrEmpty(memberid))
                {
                    members = memberid.Remove(memberid.Length - 1, 1).Split(',');
                }
                else
                {
                    MessageBox.ShowAndRedirect("请选择约战队员！", "Gathers_Step2.aspx?gatherid=" + gatherid + "&teamid=" + TeamId + "");
                    return;
                }
                List<tbl_GathersMember> gathersMembers=new List<tbl_GathersMember> ();
                for (int i = 0; i < members.Length;i++ )
                {
                    dt_TeamMember model = BTeamMember2.GetTeamMemberModel(TeamId, members[i]);
                    if(model!=null)

                    {
                        gathersMembers.Add(
                        new tbl_GathersMember
                        {
                            Id = Guid.NewGuid().ToString(),
                            TeamId = TeamId,
                            MemberId = members[i],
                            GatherId = gatherid,
                            MemberName=model.ContactName,
                            RoleType = model.RoleType,
                            DNQYHM=model.DNQYHM,
                            DNWZ = model.DNWZ,
                            IssueTime=model.IssueTime,
                            JRYS = model.JRYS


                        }
                        );
                    }
                
                }
                BGathersMember.Add(gathersMembers);
                MessageBox.ShowAndRedirect("球队约战成功！", "MyGathers.aspx");
          }
          else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8));

            }
        }
    }


}