using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Match
{
    public partial class ScheduleDetail : System.Web.UI.Page
    {
        /// <summary>
        /// 赛事阶段
        /// </summary>
        protected string GameList = "";
        /// <summary>
        /// 参赛球队
        /// </summary>
        protected string MatchTeamList = "";
        protected string MatchId = "";
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Id = Utils.GetQueryStringValue("id");
                if (!String.IsNullOrWhiteSpace(Id))
                {
                    InitMatch(Id);
                }
                else
                {
                    MessageBox.ShowAndReturnBack("未找到您要处理的比赛!");
                    return;
                }
            }
        }
        #region 赛事初始化
        /// <summary>
        /// 加载赛事信息
        /// </summary>
        /// <param name="Id"></param>
        private void InitMatch(string Id)
        {
            var model = BMatchGame.GetModel(Id);
            if (model != null)
            {
                this.ltrGameName.Text = model.GameName;
                var MatchModel = BMatch.GetModel(model.MatchId);
                if (MatchModel != null)
                {
                    MatchId = model.MatchId;
                    this.ltrMatchName.Text = MatchModel.MatchName;
                    this.ltrMatchTime.Text = MatchModel.BeginDate.ToString("yyyy-MM-dd HH:mm:ss") + "到" + MatchModel.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    MessageBox.ShowAndReturnBack("未找到您要处理的比赛!");
                    return;
                }
                InitScheduleList(MatchId, Id);
            }
            else
            {
                MessageBox.ShowAndReturnBack("未找到您要处理的比赛!");
                return;
            }
        }
        /// <summary>
        /// 加载排赛列表
        /// </summary>
        /// <param name="MatchId"></param>
        /// <param name="GameId"></param>
        /// <returns></returns>
        private void InitScheduleList(string MatchId, string GameId)
        {
            var list = BMatchSchedule.GetList(new MMatchScheduleSearch
            {
                MatchId = MatchId,
                GameId = GameId
            });
            if (list != null && list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }
        #endregion
    }
}