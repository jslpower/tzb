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
    /// <summary>
    /// 战报录入
    /// </summary>
    public partial class MatchScheduleResultAdd : System.Web.UI.Page
    {
        protected string HomeTeamMemberList = "", AwayTeamMemberList = "";
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("dotype");
            if (dotype=="save") {
                SaveResult();
                return;
            }
            if (!IsPostBack)
            {
                ManageUserAuth.ManageIsLoginCheck();
                InitModel();
            }
        }
        #region 初始化赛事信息
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
                    ltrGameName.Text = model.GameName;
                    ltrFieldName.Text = model.FieldName;

                    ltrMatchTime.Text = model.GameStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "-" + model.GameEndTime.ToString("HH:mm:ss");
                    ltrHomeTeamName.Text = model.HomeTeamName + "(" + model.HomeMatchCode + ")";
                    ltrAwayTeamName.Text = model.AwayTeamName + "(" + model.AwayMatchCode + ")";
                    HomeTeamMemberList = MatchMemberList(model.MatchId, model.HomeMatchTeamId, "");
                    AwayTeamMemberList = MatchMemberList(model.MatchId, model.AwayMatchTeamId, "");
                    if (model.PublishState == 1)
                        this.ltrPublishTarget.Text = "<input id=\"cphMain_radPublishTarget_1\" type=\"radio\" name=\"radPublishTarget\" value=\"0\" /><label for=\"cphMain_radPublishTarget_1\">未发布</label><input id=\"cphMain_radPublishTarget_0\" type=\"radio\" name=\"radPublishTarget\" value=\"1\" checked=\"checked\" /><label for=\"cphMain_radPublishTarget_0\">已发布</label>";
                    else
                        this.ltrPublishTarget.Text = "<input id=\"cphMain_radPublishTarget_1\" type=\"radio\" name=\"radPublishTarget\" value=\"0\" checked=\"checked\" /><label for=\"cphMain_radPublishTarget_1\">未发布</label><input id=\"cphMain_radPublishTarget_0\" type=\"radio\" name=\"radPublishTarget\" value=\"1\" /><label for=\"cphMain_radPublishTarget_0\">已发布</label>";
                    this.txtHomeScore.Text = model.HomeScore.ToString();
                    this.txtHomeGoals.Text = model.HomeGoals.ToString();
                    this.txtHomeFirstGoals.Text = model.HomeFirstGoals.ToString();
                    this.txtHomeSecondGoals.Text = model.HomeSecondGoals.ToString();
                    this.txtHomeOvertimePenaltys.Text = model.HomeOvertimePenaltys.ToString();
                    this.txtHomePenaltys.Text = model.HomePenaltys.ToString();
                    this.txtHomeFouls.Text = model.HomeFouls.ToString();
                    this.txtHomeReds.Text = model.HomeReds.ToString();
                    this.txtHomeYellows.Text = model.HomeYellows.ToString();
                    this.txtAwayScore.Text = model.AwayScore.ToString();
                    this.txtAwayGoals.Text = model.AwayGoals.ToString();
                    this.txtAwayFirstGoals.Text = model.AwayFirstGoals.ToString();
                    this.txtAwaySecondGoals.Text = model.AwaySecondGoals.ToString();
                    this.txtAwayOvertimePenaltys.Text = model.AwayOvertimePenaltys.ToString();
                    this.txtAwayPenaltys.Text = model.AwayPenaltys.ToString();
                    this.txtAwayFouls.Text = model.AwayFouls.ToString();
                    this.txtAwayReds.Text = model.AwayReds.ToString();
                    this.txtAwayYellows.Text = model.AwayYellows.ToString();
                    var HomeList = BMatchMemberTechnical.GetList(Id,model.HomeMatchTeamId);
                    if (HomeList != null && HomeList.Count() > 0)
                    {
                        this.phNoData.Visible = false;
                        this.rptList.DataSource = HomeList;
                        this.rptList.DataBind();
                    }
                    else {
                        this.phNoData.Visible = true;
                    }
                    var AwayList = BMatchMemberTechnical.GetList(Id, model.AwayMatchTeamId);
                    if (AwayList != null && AwayList.Count() > 0)
                    {
                        this.phNoData2.Visible = false;
                        this.rptList2.DataSource = AwayList;
                        this.rptList2.DataBind();
                    }
                    else
                    {
                        this.phNoData2.Visible = true;
                    }
                    var ReportModel = BMatchSchedultReport.GetModel(Id);
                    if (ReportModel != null) {
                        this.txtTitle.Text = ReportModel.Title;
                        this.txtContent.Text = ReportModel.ContentInfo;
                    }
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
        #endregion
        #region 初始化参赛队员信息
        /// <summary>
        /// 加载参赛队员信息
        /// </summary>
        /// <param name="MatchId"></param>
        /// <param name="TeamId"></param>
        /// <param name="TeamMemberIdSelect"></param>
        /// <returns></returns>
        protected string MatchMemberList(string MatchId, string MatchTeamId, string TeamMemberIdSelect)
        {
            string str = "";
            var list = BMatchTeamMember.GetListByMatchId(MatchId, MatchTeamId);
            if (list != null && list.Count() > 0) {
                foreach (var m in list)
                {
                    if (TeamMemberIdSelect == m.TeamMemberId)
                        str += "<option value=\"" + MatchId + "," + MatchTeamId + "," + m.TeamMemberId + "," + m.MemberId + "\" selected>" + m.ContactName + "(" + m.JerseyNumber + "号)";
                    else
                        str += "<option value=\"" + MatchId + "," + MatchTeamId + "," + m.TeamMemberId + "," + m.MemberId + "\">" + m.ContactName + "(" + m.JerseyNumber + "号)";
                }
            }
            return str;
        }
        /// <summary>
        /// 进球类型绑定
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        protected string TechnicalOptionList(string TypeId)
        {
            string str = Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(Model.EnumType.进球类型)), TypeId);
            return str;
        }
        #endregion
        #region 保存比赛结果u
        /// <summary>
        /// 保存比赛结果
        /// </summary>
        private void SaveResult()
        {
            string Id = Utils.GetQueryStringValue("id");
            if (!string.IsNullOrWhiteSpace(Id))
            {
                int PublishTarget = Utils.GetInt(Utils.GetFormValue("radPublishTarget"));
                int HomeScore = Utils.GetInt(Utils.GetFormValue(txtHomeScore.UniqueID));
                int HomeGoals = Utils.GetInt(Utils.GetFormValue(txtHomeGoals.UniqueID));
                int HomeFirstGoals = Utils.GetInt(Utils.GetFormValue(txtHomeFirstGoals.UniqueID));
                int HomeSecondGoals = Utils.GetInt(Utils.GetFormValue(txtHomeSecondGoals.UniqueID));
                int HomeOvertimePenaltys = Utils.GetInt(Utils.GetFormValue(txtHomeOvertimePenaltys.UniqueID));
                int HomePenaltys = Utils.GetInt(Utils.GetFormValue(txtHomePenaltys.UniqueID));
                int HomeFouls = Utils.GetInt(Utils.GetFormValue(txtHomeFouls.UniqueID));
                int HomeReds = Utils.GetInt(Utils.GetFormValue(txtHomeReds.UniqueID));
                int HomeYellows = Utils.GetInt(Utils.GetFormValue(txtHomeYellows.UniqueID));
                int AwayScore = Utils.GetInt(Utils.GetFormValue(txtAwayScore.UniqueID));
                int AwayGoals = Utils.GetInt(Utils.GetFormValue(txtAwayGoals.UniqueID));
                int AwayFirstGoals = Utils.GetInt(Utils.GetFormValue(txtAwayFirstGoals.UniqueID));
                int AwaySecondGoals = Utils.GetInt(Utils.GetFormValue(txtAwaySecondGoals.UniqueID));
                int AwayOvertimePenaltys = Utils.GetInt(Utils.GetFormValue(txtAwayOvertimePenaltys.UniqueID));
                int AwayPenaltys = Utils.GetInt(Utils.GetFormValue(txtAwayPenaltys.UniqueID));
                int AwayFouls = Utils.GetInt(Utils.GetFormValue(txtAwayFouls.UniqueID));
                int AwayReds = Utils.GetInt(Utils.GetFormValue(txtAwayReds.UniqueID));
                int AwayYellows = Utils.GetInt(Utils.GetFormValue(txtAwayYellows.UniqueID));
                #region 个人战绩
                string[] HomeTypeId = Utils.GetFormValues("ddlHomeTypeId");
                string[] HomeTeamMember = Utils.GetFormValues("ddlHomeTeamMember");
                string[] HomeTechnicals = Utils.GetFormValues("txtHomeTechnicals");
                string[] AwayTypeId = Utils.GetFormValues("ddlAwayTypeId");
                string[] AwayTeamMember = Utils.GetFormValues("ddlAwayTeamMember");
                string[] AwayTechnicals = Utils.GetFormValues("txtAwayTechnicals");

                #endregion
                #region 战报内容
                string Title = Utils.GetFormValue(txtTitle.UniqueID);
                string ContentInfo = Utils.GetFormEditorValue(txtContent.UniqueID);
                #endregion
                #region 用户信息
                var UserModel = ManageUserAuth.GetManageUserModel();
                int OperatorId = UserModel.Id;
                string Contactname = UserModel.ContactName;
                UserModel = null;
                #endregion
                 var model = BMatchSchedule.GetModel(Id);
                 if (model != null)
                 {
                     #region 更新赛程战报
                     model.PublishState = PublishTarget;
                     model.HomeScore = HomeScore;
                     model.HomeGoals = HomeGoals;
                     model.HomeFirstGoals = HomeFirstGoals;
                     model.HomeSecondGoals = HomeSecondGoals;
                     model.HomeOvertimePenaltys = HomeOvertimePenaltys;
                     model.HomePenaltys = HomePenaltys;
                     model.HomeFouls = HomeFouls;
                     model.HomeReds = HomeReds;
                     model.HomeYellows = HomeYellows;
                     model.AwayScore = AwayScore;
                     model.AwayGoals = AwayGoals;
                     model.AwayFirstGoals = AwayFirstGoals;
                     model.AwaySecondGoals = AwaySecondGoals;
                     model.AwayOvertimePenaltys = AwayOvertimePenaltys;
                     model.AwayPenaltys = AwayPenaltys;
                     model.AwayFouls = AwayFouls;
                     model.AwayReds = AwayReds;
                     model.AwayYellows = AwayYellows;
                     model.ResultOperatorId = OperatorId;
                     model.ResultContactName = Contactname;
                     model.ResultTime = DateTime.Now;
                     //更新赛程战绩
                     BMatchSchedule.UpdateScheduleStandings(model);
                    #endregion

                     #region 更新个人战报
                     List<tbl_MatchMemberTechnical> MTList = new List<tbl_MatchMemberTechnical>();
                     #region 主队个人
                     if (HomeTypeId != null && HomeTypeId.Length > 0)
                     {
                         for (int i = 0; i < HomeTypeId.Length; i++)
                         {
                             if (HomeTypeId[i] != "0")
                             {
                                 string[] MemeberInfo = HomeTeamMember[i].Split(',');
                                 if (MemeberInfo.Length != 4)
                                 {

                                     Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "异常数据!"));
                                     return;
                                 }
                                 else
                                 {
                                     string MemberId = MemeberInfo[3];
                                     string MemberName = "";
                                     var MemberModel = BMember.GetModel(MemberId);
                                     if (MemberModel != null)
                                     {
                                         MemberName = MemberModel.ContactName;
                                     }
                                     else
                                     {
                                         Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "队员用户不存在!"));
                                         return;
                                     }
                                     MemberModel = null;
                                     MTList.Add(new tbl_MatchMemberTechnical
                                     {
                                         Id = System.Guid.NewGuid().ToString(),
                                         OperatorId = OperatorId,
                                         ContactName = Contactname,
                                         MatchId = MemeberInfo[0],
                                         MatchTeamId = MemeberInfo[1],
                                         MatchTeamMemberId = MemeberInfo[2],
                                         MemberId = MemberId,
                                         MemberName = MemberName,
                                         ScheduleId = Id,
                                         TypeId = Utils.GetInt(HomeTypeId[i]),
                                         Technicals = Utils.GetInt(HomeTechnicals[i]),
                                         IssueTime = DateTime.Now
                                     });
                                 }

                             }
                         }
                             
                     }
#endregion
                     #region 客队个人
                     if (AwayTypeId != null && AwayTypeId.Length > 0)
                     {
                         for (int i = 0; i < AwayTypeId.Length; i++)
                         {
                             if (AwayTypeId[i] != "0")
                             {
                                 string[] MemeberInfo = AwayTeamMember[i].Split(',');
                                 if (MemeberInfo.Length != 4)
                                 {

                                     Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "异常数据!"));
                                     return;
                                 }
                                 else
                                 {
                                     string MemberId = MemeberInfo[3];
                                     string MemberName = "";
                                     var MemberModel = BMember.GetModel(MemberId);
                                     if (MemberModel != null)
                                     {
                                         MemberName = MemberModel.ContactName;
                                     }
                                     else
                                     {
                                         Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "队员用户不存在!"));
                                         return;
                                     }
                                     MemberModel = null;
                                     MTList.Add(new tbl_MatchMemberTechnical
                                     {
                                         Id = System.Guid.NewGuid().ToString(),
                                         OperatorId = OperatorId,
                                         ContactName = Contactname,
                                         MatchId = MemeberInfo[0],
                                         MatchTeamId = MemeberInfo[1],
                                         MatchTeamMemberId = MemeberInfo[2],
                                         MemberId = MemberId,
                                         MemberName = MemberName,
                                         ScheduleId = Id,
                                         TypeId = Utils.GetInt(AwayTypeId[i]),
                                         Technicals = Utils.GetInt(AwayTechnicals[i]),
                                         IssueTime = DateTime.Now
                                     });
                                 }

                             }
                         }

                     }
                     #endregion
                     //删掉赛程个人战报
                     BMatchMemberTechnical.DelSchedule(Id);
                     //保存赛程个人战报
                     BMatchMemberTechnical.Add(MTList);
                     #endregion
                     #region 添加战报文章
                     BMatchSchedultReport.Add(new tbl_MatchSchedultReport { 
                        Id = System.Guid.NewGuid().ToString(),
                        OperatorId= OperatorId,
                        OperatorName = Contactname,
                        ScheduleId = Id,
                        PhotoUrl = "",
                        titleSulg = "",
                        Title = Title,
                        ContentInfo = ContentInfo,
                        IssueTime = DateTime.Now
                     });
                     #endregion
                     Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "战报保存成功！"));
                 }
                 else {
                     Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "未找到您要操作的对象"));
                 }       
            }
            else {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "未找到您要操作的对象"));
            }
        }
        #endregion
    }
}