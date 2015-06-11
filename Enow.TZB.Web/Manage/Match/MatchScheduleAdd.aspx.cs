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
    /// <summary>
    /// 赛事日程管理
    /// </summary>
    public partial class MatchScheduleAdd : System.Web.UI.Page
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
            if (!IsPostBack) {
                string Id = Utils.GetQueryStringValue("id");
                if (!String.IsNullOrWhiteSpace(Id)) {
                    InitMatch(Id);
                } else {
                    MessageBox.ShowAndReturnBack("未找到您要处理的比赛!");
                    return;
                }
            }
            string dotype = Utils.GetQueryStringValue("dotype").ToLower();
            switch (dotype)
            {
                case "save":
                    SaveData();
                    break;
                default:
                    break;
            }
        }
        #region 赛事初始化
        /// <summary>
        /// 加载赛事信息
        /// </summary>
        /// <param name="Id"></param>
        private void InitMatch(string Id) {
            var model = BMatchGame.GetModel(Id);
            if (model != null) {
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
                InitScheduleList(MatchId,Id);
            } else {
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
        private void InitScheduleList(string MatchId, string GameId) {
            var list = BMatchSchedule.GetList(new MMatchScheduleSearch { 
            MatchId = MatchId,
            GameId = GameId
            });
            if (list != null && list.Count() > 0)
            {
                this.phNoData.Visible = false;
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            else {
                this.phNoData.Visible = true;
            }
        }
        /// <summary>
        /// 加载参赛球队
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        protected string InitTeam(string SelectId)
        {
            StringBuilder sb = new StringBuilder();
            var list = BMatchTeam.GetMatchTeamList(MatchId);
            sb.Append("<option value='0'>请选择参赛球队</option>");
            if (list.Count>0)
            {
                foreach (var lst in list)
                {
                    if (lst.TeamId != SelectId)
                        sb.AppendFormat("<option value='{0}'>{1}</option>", lst.TeamId, lst.TeamName);
                    else
                        sb.AppendFormat("<option value='{0}' selected>{1}</option>", lst.TeamId, lst.TeamName);
                }
            }
            return sb.ToString();
        }
        #endregion
        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            var list = GetScheduleInfo();
            string DelIdList = Utils.GetFormValue("hidDelList");
            if (!String.IsNullOrWhiteSpace(DelIdList)) {
                DelIdList = DelIdList.TrimEnd(',');
                if (!String.IsNullOrWhiteSpace(DelIdList))
                {
                    string[] arrDelId = DelIdList.Split(',');
                    DelIdList = "'" + String.Join("','", arrDelId) + "'";
                    //批量删除
                    BMatchSchedule.BatchDelete(DelIdList);
                }
            }
            bool result = false;
            if (list.Count>0)
            {
                foreach (var lst in list)
                {   
                    /*
                    //判断当前赛程是否已安排当前主客队
                    if (BMatchSchedule.IsExists(lst.MatchId, lst.GameId, lst.HomeTeamId, lst.AwayTeamId))
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "球队"+lst.HomeTeamName+"与"+lst.AwayTeamName+"的"+lst.GameName+"阶段的赛事日程已存在，请在赛程管理-赛事日程管理界面进行编辑!"));
                    }
                     */
                    if (lst.Action == 1)
                    {
                        result = BMatchSchedule.Add(lst.Item);
                    }
                    else {
                        result = BMatchSchedule.Update(lst.Item);
                    }
                }
            }
            if (result)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功!"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "数据异常，请重试!"));
            }
          
        }
        #endregion

        #region 获取异步提交数据的集合
        private List<MMatchScheduleAction> GetScheduleInfo()
        {
            string[] hidScheduleId = Utils.GetFormValues("hidScheduleId");
            string[] OrdinalNumber = Utils.GetFormValues("txtOrdinalNumber");
            string[] StartTime = Utils.GetFormValues("txtStartDate");
            string[] EndTime = Utils.GetFormValues("txtEndDate");
            string[] HomeMatchCode = Utils.GetFormValues("txtHomeMatchCode");
            string[] HomeTeamId = Utils.GetFormValues("ddlHomeTeamId");
            string[] AwayMatchCode = Utils.GetFormValues("txtAwayMatchCode");
            string[] AwayTeamId = Utils.GetFormValues("ddlAwayTeamId");
            string[] TeamGameState = Utils.GetFormValues("cbGameState");
            #region 获取当前操作人信息
            ManagerList Umodel = ManageUserAuth.GetManageUserModel();
            int OperatorId = Umodel.Id;
            string OperatorName = Umodel.ContactName;
            Umodel = null;
            #endregion

            #region 获取赛事相关信息
            string MatchId = "";
            string Id = Utils.GetQueryStringValue("id");
            var MatchGameModel = BMatchGame.GetModel(Id);
            string MatchName = "";
            string GameName = "";
            string GameId = Id;
            string MatchFieldId = "";
            string MatchFieldName = "";
            if (MatchGameModel != null)
            {
                MatchName = MatchGameModel.MatchName;
                MatchId = MatchGameModel.MatchId;
                GameName = MatchGameModel.GameName;
                MatchFieldId = MatchGameModel.MatchFieldId;
                MatchFieldName = MatchGameModel.FieldName;
            }
            else { Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "请求异常，请重试。")); }
            
            #endregion

            if ( OrdinalNumber.Length!=StartTime.Length||StartTime.Length != EndTime.Length
                || EndTime.Length != HomeMatchCode.Length
                || HomeMatchCode.Length != HomeTeamId.Length
                || HomeTeamId.Length != AwayMatchCode.Length
                || AwayMatchCode.Length != AwayTeamId.Length
                || AwayTeamId.Length != TeamGameState.Length)
            {

                Utils.RCWE(UtilsCommons.AjaxReturnJson("-1", "请求异常，请重试。"));
            }
            List<MMatchScheduleAction> list = new List<MMatchScheduleAction>();
            for (int i = 0; i < StartTime.Length; i++)
            {
                var ScheduleActionItem = new MMatchScheduleAction();
                var item = new tbl_MatchSchedule();
                if (hidScheduleId[i] == "0")
                {
                    item.Id = Guid.NewGuid().ToString();
                    ScheduleActionItem.Action = 1;
                }
                else
                {
                    item.Id = hidScheduleId[i];
                    ScheduleActionItem.Action = 2;
                }
                item.OperatorId = OperatorId;
                item.ContactName = OperatorName;
                item.MatchId = MatchId;
                item.MatchName = MatchName;
                item.MatchFieldId = MatchFieldId;
                item.FieldName = MatchFieldName;
                item.GameId = GameId;
                item.GameName = GameName;
                item.GameStartTime = Utils.GetDateTime(StartTime[i]);
                item.GameEndTime = Utils.GetDateTime(EndTime[i]);
                item.HomeTeamId = HomeTeamId[i];
                item.HomeMatchTeamId = "0";
                item.HomeTeamName = "";
                if (HomeTeamId[i] != "0")
                {
                    var HometeamModel = BMatchTeam.GetModel(MatchId, item.HomeTeamId);
                    if (HometeamModel != null)
                    {
                        item.HomeMatchTeamId = HometeamModel.Id;
                        item.HomeTeamName = HometeamModel.TeamName;
                    }
                }
                item.HomeMatchCode = HomeMatchCode[i];
                item.AwayTeamId = AwayTeamId[i];
                item.AwayMatchTeamId = "0";
                item.AwayTeamName = "";
                if (AwayTeamId[i] != "0")
                {
                    var AMatchTeamModel = BMatchTeam.GetModel(MatchId, item.AwayTeamId);
                    if (AMatchTeamModel != null)
                    {
                        item.AwayMatchTeamId = AMatchTeamModel.Id;
                        item.AwayTeamName = AMatchTeamModel.TeamName;
                    }
                }
                item.AwayMatchCode = AwayMatchCode[i];
                item.GameState = Utils.GetInt(TeamGameState[i], 0);
                item.PublishState = 0;
                item.HomeGoals = 0;
                item.HomeOvertimePenaltys = 0;
                item.HomePenaltys = 0;
                item.HomeFouls = 0;
                item.HomeReds = 0;
                item.HomeYellows = 0;
                item.AwayGoals = 0;
                item.AwayOvertimePenaltys = 0;
                item.AwayPenaltys = 0;
                item.AwayFouls = 0;
                item.AwayReds = 0;
                item.AwayYellows = 0;
                item.OrdinalNumber =Utils.GetInt(OrdinalNumber[i]);
                item.ResultOperatorId = 0;
                item.ResultContactName = "";
                item.ResultTime = Utils.GetDateTimeNullable("");
                item.IssueTime = DateTime.Now;
                //判断主客队是否重复
                if (item.HomeTeamId != "0" && item.AwayTeamId!="0")
                {
                    if (item.HomeTeamId == item.AwayTeamId)
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "第" + i + 1 + "条赛事日程主客队不能是同支队伍!"));
                    }
                }
                ScheduleActionItem.Item = item;
                list.Add(ScheduleActionItem);
            }
            return list;
        }
#endregion

    }
    /// <summary>
    /// 排赛管理实体
    /// </summary>
    public class MMatchScheduleAction
    {
        public tbl_MatchSchedule Item { get; set; }
        /// <summary>
        /// 操作 1：新增 2:修改
        /// </summary>
        public int Action { get; set; }
    }
}