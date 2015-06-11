using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.Model;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Member
{
    /// <summary>
    /// 赛事详情
    /// </summary>
    public partial class MatchDetail : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                string Id = Utils.GetQueryStringValue("Id");
                if (!String.IsNullOrWhiteSpace(Id))
                {
                    InitMatchInfo(Id);
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
        }
       
        /// <summary>
        /// 加载赛事信息
        /// </summary>
        /// <param name="Id"></param>
        private void InitMatchInfo(string Id)
        {
            var MemberMatchModel = BMatchTeamMember.GetMemberMatchModel(Id);
            if (MemberMatchModel != null)
            {
                var model = BMatch.GetModel(MemberMatchModel.MatchId);
                if (model != null)
                {
                    this.MasterOrganizer.Text = model.MasterOrganizer;
                    if (!string.IsNullOrWhiteSpace(model.CoOrganizers))
                    {

                        this.CoOrganizers.Text = "协办方：" + model.CoOrganizers;
                    }
                    else
                    {
                        this.CoOrganizers.Text = "&nbsp;&nbsp;";

                    }
                    if (!string.IsNullOrWhiteSpace(model.Organizer))
                    {
                        this.Organizer.Text = "承办方：" + model.Organizer;
                    }
                    else
                    {
                        this.CoOrganizers.Text = "&nbsp;&nbsp;";
                    }
                    if (!string.IsNullOrWhiteSpace(model.Sponsors))
                    {
                        this.Sponsors.Text = "赞助方：" + model.Sponsors;
                    }
                    else
                    {
                        this.Sponsors.Text = "&nbsp;&nbsp";
                    }
                    this.ltrSignUpTime.Text = model.SignBeginDate.ToString("yyyy-MM-dd") + " 至 " + model.SignEndDate.ToString("yyyy-MM-dd");
                    this.BreakTime.Text = model.BreakTime.ToString() + " 分钟";
                    this.SignUpNumber.Text = model.SignUpNumber.ToString() + "/" + model.TeamNumber.ToString();
                    this.TotalTime.Text = model.TotalTime.ToString() + " 分钟";
                    this.BayMax.Text = model.BayMax.ToString() + " 人";
                    this.BayMin.Text = model.BayMin.ToString();
                    this.MaxAge.Text = model.MaxAge.ToString() + " 岁";
                    this.MinAge.Text = model.MinAge.ToString();
                    this.Remark.Text = model.Remark;
                    this.ltrTeamNumber.Text = MemberMatchModel.JoinNumber + "/" + model.PlayersMax.ToString();
                    this.PlayersMax.Text = model.PlayersMin.ToString() + "-" + model.PlayersMax.ToString() + " 人";
                    InitList(MemberMatchModel.MatchId, MemberMatchModel.MatchTeamId);
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
        #region 加载成员列表
        /// <summary>
        /// 加载成员列表
        /// </summary>
        /// <param name="TeamId"></param>
        private void InitList(string MatchId, string MatchTeamId)
        {
            var list = BMatchTeamMember.GetListByMatchId(MatchId, MatchTeamId);
            if (list != null)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }
        #endregion
    }
}