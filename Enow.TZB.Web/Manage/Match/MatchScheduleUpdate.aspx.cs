using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Match
{
    public partial class MatchScheduleUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageIsLoginCheck();
                InitModel();
            }
        }

        /// <summary>
        /// 加载赛事日程信息
        /// </summary>
        private void InitModel()
        {
            string Id = Utils.GetQueryStringValue("Id");
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var model = BMatchSchedule.GetModel(Id);
                if (model != null)
                {
                    InitMatchTeam(model.MatchId);
                    InitGame(model.MatchId);
                    for (int i = 0; i < ddlHomeTeamId.Items.Count; i++)
                    {
                        if (ddlHomeTeamId.Items[i].Value == model.HomeTeamId)
                            ddlHomeTeamId.Items[i].Selected = true;
                    }
                    for (int i = 0; i < ddlAwayTeamId.Items.Count; i++)
                    {
                        if (ddlAwayTeamId.Items[i].Value == model.AwayTeamId)
                            ddlAwayTeamId.Items[i].Selected = true;
                    }
                    ddlGameId.Items.FindByValue(model.GameId).Selected = true;
                    txtOrdinalNumber.Text = model.OrdinalNumber.ToString();
                    txtStartTime.Text = model.GameStartTime.ToString("yyyy-MM-dd HH:mm:ss");
                    txtEndTime.Text = model.GameEndTime.ToString("yyyy-MM-dd HH:mm:ss");
                    txtHomeMatchCode.Text = model.HomeMatchCode;
                    txtAwayMatchCode.Text = model.AwayMatchCode;
                    radGameState.Items.FindByValue(model.GameState.ToString()).Selected = true;
                    radPublishTarget.Items.FindByValue(model.PublishState.ToString()).Selected = true;
                   
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                return;
            }
        }

        /// <summary>
        /// 绑定参赛主客队
        /// </summary>
        /// <param name="MatchId"></param>
        private void InitMatchTeam(string MatchId)
        {
            var list = BMatchTeam.GetMatchTeamList(MatchId);
            if (list.Count>0)
            {
                ddlHomeTeamId.DataSource = list;
                ddlHomeTeamId.DataTextField = "TeamName";
                ddlHomeTeamId.DataValueField = "TeamId";
                ddlHomeTeamId.DataBind();
                ddlHomeTeamId.Items.Insert(0, new ListItem("请选择参赛队伍", "0"));
                ddlAwayTeamId.DataSource = list;
                ddlAwayTeamId.DataTextField = "TeamName";
                ddlAwayTeamId.DataValueField = "TeamId";
                ddlAwayTeamId.DataBind();
                ddlAwayTeamId.Items.Insert(0, new ListItem("请选择参赛队伍", "0"));
            }
        }

        /// <summary>
        /// 绑定赛事阶段
        /// </summary>
        /// <param name="MatchId"></param>
        private void InitGame(string MatchId)
        {
            var list = BMatchGame.GetList(MatchId);
            if (list.Count>0)
            {
                ddlGameId.DataSource = list;
                ddlGameId.DataTextField = "GameName";
                ddlGameId.DataValueField = "Id";
                ddlGameId.DataBind();
            }
        }

       /// <summary>
       /// 保存数据
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            #region 取值
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;
            string name = UserModel.ContactName;
            string Id = Utils.GetQueryStringValue("Id");
            var sModel = BMatchSchedule.GetModel(Id);
            string MatchId = sModel.MatchId;
            string GameId = Utils.GetFormValue(ddlGameId.UniqueID);
            string HomeTeamId = Utils.GetFormValue(ddlHomeTeamId.UniqueID);
            string AwayTeamId = Utils.GetFormValue(ddlAwayTeamId.UniqueID);
            int OridnalNumber = Utils.GetInt(Utils.GetFormValue(txtOrdinalNumber.UniqueID));
            DateTime StartDate = Utils.GetDateTime(Utils.GetFormValue(txtStartTime.UniqueID));
            DateTime EndTime = Utils.GetDateTime(Utils.GetFormValue(txtEndTime.UniqueID));
            string HomeMatchCode = Utils.GetFormValue(txtHomeMatchCode.UniqueID);
            string AwayMatchCode = Utils.GetFormValue(txtAwayMatchCode.UniqueID);
            int GameState = Utils.GetInt(Utils.GetFormValue(radGameState.UniqueID));
            int PublishTarget = Utils.GetInt(Utils.GetFormValue(radPublishTarget.UniqueID));
            string GameName = "";
            string MatchFieldId = "";
            string MatchFieldName = "";
            string HomeMatchTeamName = "";
            string AwayMatchTeamName = "";
            string HomeMatchTeamId = "0";
            string AwayMatchTeamId = "0";
            string MatchCode = "";
            if (HomeTeamId != "0")
            {
                var HometeamModel = BMatchTeam.GetModel(MatchId, HomeTeamId);
                if (HometeamModel != null)
                {
                    HomeMatchTeamId = HometeamModel.Id;
                    HomeMatchTeamName = HometeamModel.TeamName;
                    if (HometeamModel.IsBallot) {
                        BMatchTeamCode.UpdateBallot(HomeMatchTeamId, ref MatchCode);
                    }
                }
            }
            if (AwayTeamId != "0")
            {
                var AwayTeamModel = BMatchTeam.GetModel(MatchId, AwayTeamId);
                if (AwayTeamModel != null)
                {
                    AwayMatchTeamId = AwayTeamModel.Id;
                    AwayMatchTeamName = AwayTeamModel.TeamName;
                    if (AwayTeamModel.IsBallot)
                    {
                        BMatchTeamCode.UpdateBallot(AwayMatchTeamId, ref MatchCode);
                    }
                }
            }
            var GameModel = BMatchGame.GetModel(GameId);
      
            if (GameModel != null)
            {
                GameName = GameModel.GameName;
                MatchFieldId = GameModel.MatchFieldId;
                MatchFieldName = GameModel.FieldName;
            }
            #endregion

            #region 判断

            if (HomeTeamId!="0" &&AwayTeamId!="0"&& HomeTeamId == AwayTeamId)
            {
                MessageBox.ShowAndReload("主客队不能重复！");
                return;
            }

            bool isResult = BMatchSchedule.Update(new tbl_MatchSchedule {
                Id=Id,
                OperatorId=UserId,
                ContactName=name,
                MatchFieldId=MatchFieldId,
                FieldName=MatchFieldName,
                GameId=GameId,
                GameName=GameName,
                GameStartTime=StartDate,
                GameEndTime=EndTime,
                HomeMatchTeamId=HomeMatchTeamId,
                HomeTeamId=HomeTeamId,
                HomeTeamName=HomeMatchTeamName,
                HomeMatchCode=HomeMatchCode,
                AwayMatchTeamId=AwayMatchTeamId,
                AwayTeamId=AwayTeamId,
                AwayTeamName=AwayMatchTeamName,
                AwayMatchCode=AwayMatchCode,
                GameState=GameState,
                PublishState=PublishTarget,
                OrdinalNumber = OridnalNumber
            
            });
            if (isResult)
            {
                MessageBox.ShowAndRedirect("操作成功", "Schedule.aspx");

            }
            else
            {
                MessageBox.ShowAndReload("操作失败");
                return;
            }
            #endregion 

        }
        
    }


}