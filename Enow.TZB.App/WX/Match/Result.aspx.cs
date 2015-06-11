#region 命名空间
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.Model;
using Enow.TZB.BLL;
#endregion

namespace Enow.TZB.Web.WX.Match
{
    /// <summary>
    /// 赛事战报
    /// </summary>
    public partial class Result : System.Web.UI.Page
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
                string ScheduleId = Utils.GetQueryStringValue("ScheduleId");
                if (!String.IsNullOrWhiteSpace(Id) && !String.IsNullOrWhiteSpace(ScheduleId))
                {
                    InitMatchInfo(ScheduleId);
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
        /// <param name="ScheduleId"></param>
        private void InitMatchInfo(string ScheduleId)
        {
            var model = BMatchSchedule.GetModel(ScheduleId);
            if (model != null)
            {
                this.ltrMatchName.Text = model.MatchName;
                this.ltrGameName.Text = model.GameName;
                this.ltrMatchScheduleInfo.Text = model.OrdinalNumber.ToString() + "场 " + model.GameStartTime.ToString("yyyy年MM月dd日 HH:mm") + "-" + model.GameEndTime.ToString("HH:mm") + "【" + model.FieldName + "】";
                this.ltrHomeTeam.Text = model.HomeTeamName;
                this.ltrAwayTeam.Text = model.AwayTeamName;
                this.ltrHomeGolas.Text = model.HomeGoals.ToString();
                this.ltrAwayGolas.Text = model.AwayGoals.ToString();
                this.ltrHomeFirstGoals.Text = model.HomeFirstGoals.ToString();
                this.ltrAwayFirstGoals.Text = model.AwayFirstGoals.ToString();
                this.ltrHomeFirstGoals.Text = model.HomeFirstGoals.ToString();
                this.ltrAwayFirstGoals.Text = model.AwayFirstGoals.ToString();
                this.ltrHomeSecondGoals.Text = model.HomeSecondGoals.ToString();
                this.ltrAwaySecondGoals.Text = model.AwaySecondGoals.ToString();
                this.txtHomeOvertimePenaltys.Text = model.HomeOvertimePenaltys.ToString();
                this.txtHomePenaltys.Text = model.HomePenaltys.ToString();
                this.txtHomeFouls.Text = model.HomeFouls.ToString();
                this.txtHomeReds.Text = model.HomeReds.ToString();
                this.txtHomeYellows.Text = model.HomeYellows.ToString();
                this.txtAwayOvertimePenaltys.Text = model.AwayOvertimePenaltys.ToString();
                this.txtAwayPenaltys.Text = model.AwayPenaltys.ToString();
                this.txtAwayFouls.Text = model.AwayFouls.ToString();
                this.txtAwayReds.Text = model.AwayReds.ToString();
                this.txtAwayYellows.Text = model.AwayYellows.ToString();
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
            }
        }
    }
}