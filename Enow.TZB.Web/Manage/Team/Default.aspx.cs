using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using Enow.TZB.Model;
using Enow.TZB.SMS;
using System.Data;
namespace Enow.TZB.Web.Manage.Team
{
    /// <summary>
    /// 球队管理
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        /// <summary>
        /// 省市联动下拉框初始化
        /// </summary>
        protected string CId = "", PId = "", CSId = "", AId = "";
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");

            switch (doType)
            {
                case "StateBack": StateBack(); break;
                case "Disabled": DisabledData(1); break;
                case "Enable": EnableData(1); break;
                case "Disabled2": DisabledData(2); break;
                case "Enable2": EnableData(2); break;
                case "Disband": Disband(); break;
                case "NoDisband": NoDisband(); break;
                default: break;
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();
                if (ManageUserAuth.CheckAdminAuth((int)RoleEnum.球队初审) == true)
                {
                    this.PhDZCheck.Visible = true;
                }
                if (ManageUserAuth.CheckAdminAuth((int)RoleEnum.球队终审) == true)
                {
                    this.phEndCheck.Visible = true;
                }
                if (ManageUserAuth.CheckAdminAuth((int)RoleEnum.解散审批) == true)
                {
                    this.phDisband.Visible = true;
                }
                InitList();
            }
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitList()
        {

            Model.MBallTeamSearch SearchModel = new Model.MBallTeamSearch();
            int rowsCount = 0;
            string Page = Request.QueryString["Page"];
            int State = Utils.GetInt(Utils.GetQueryStringValue("State"), -1);
            string keyword = Utils.GetQueryStringValue("KeyWord");
            string TeamOwner = Utils.GetQueryStringValue("TeamOwner");
            string MobileNo = Utils.GetQueryStringValue("MobileNo");
            int IsJoinMatch = Utils.GetInt(Utils.GetQueryStringValue("IsJoinMatch"), -1);
            int IsTeamer = Utils.GetInt(Utils.GetQueryStringValue("IsTeamer"), -1);
            int CountryId = Utils.GetInt(Utils.GetQueryStringValue("CountryId"), 0);
            int ProvinceId = Utils.GetInt(Utils.GetQueryStringValue("ProvinceId"), 0);
            int CityId = Utils.GetInt(Utils.GetQueryStringValue("CityId"), 0);
            int AreaId = Utils.GetInt(Utils.GetQueryStringValue("AreaId"), 0);
            DateTime? IssueBeginTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("IssueBeginTime"));
            DateTime? IssueEndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("IssueEndTime"));
            DateTime? CheckBeginTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("CheckBeginTime"));
            DateTime? CheckEndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("CheckEndTime"));
            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                CurrencyPage = int.Parse(Page);
                if (CurrencyPage < 1)
                    CurrencyPage = 1;
            }
            #region 赋值
            if (CountryId > 0)
            {
                CId = CountryId.ToString();
                SearchModel.CountryId = CountryId;
            }
            if (ProvinceId > 0)
            {
                PId = CountryId.ToString();
                SearchModel.ProvinceId = ProvinceId;
            }
            if (CityId > 0)
            {
                CSId = CityId.ToString();
                SearchModel.CityId = CityId;
            }
            if (AreaId > 0)
            {
                AId = AreaId.ToString();
                SearchModel.AreaId = AreaId;
            }
            #region 城市权限控制
            var ManageModel = ManageUserAuth.GetManageUserModel();
            if (ManageModel != null)
            {
                SearchModel.IsAllCity = ManageModel.IsAllCity;
                SearchModel.CityLimitList = ManageModel.CityList;
            }
            ManageModel = null;
            #endregion
            if (!string.IsNullOrWhiteSpace(TeamOwner))
            {
                this.txtTeamOwner.Text = TeamOwner;
                SearchModel.TeamOwner = TeamOwner;
            }
            if (!string.IsNullOrWhiteSpace(MobileNo))
            {
                this.txtOwnerMobile.Text = MobileNo;
                SearchModel.Mobile = MobileNo;
            }
            ////if (IsJoinMatch != -1)
            ////{
            ////    this.ddlJoinMatch.Items.FindByValue(IsJoinMatch.ToString()).Selected = true;
            ////}
            if (IsJoinMatch > 0)
            {
                if (IsJoinMatch == 1)
                {
                    SearchModel.JoinMatch = false;
                }
                else
                {
                    SearchModel.JoinMatch = true;
                }
            }
            if (IsTeamer > 0)
            {
                if (IsTeamer == 1)
                {
                    SearchModel.IsTeamer = false;
                }
                else
                {
                    SearchModel.IsTeamer = true;
                }
            }
            if (IssueBeginTime.HasValue)
            {
                this.txtIBeginDate.Text = IssueBeginTime.Value.ToString("yyyy-MM-dd");
                SearchModel.IssueBeginTime = IssueBeginTime;
            }
            if (IssueEndTime.HasValue)
            {
                this.txtIEndDate.Text = IssueEndTime.Value.ToString("yyyy-MM-dd");
                SearchModel.IssueEndTime = IssueEndTime;
            }
            if (CheckBeginTime.HasValue)
            {
                this.txtCBeginDate.Text = CheckBeginTime.Value.ToString("yyyy-MM-dd");
                SearchModel.CheckBeginTime = CheckBeginTime;
            }
            if (CheckEndTime.HasValue)
            {
                this.txtCEndDate.Text = CheckEndTime.Value.ToString("yyyy-MM-dd");
                SearchModel.CheckEndTime = CheckEndTime;
            }
            #endregion
            if (State != -1)
            {
                SearchModel.State = (Model.EnumType.球队审核状态)State;
            }
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                this.txtKeyWord.Text = keyword.Trim();
                SearchModel.KeyWord = keyword.Trim();
            }
            var list = BTeam.GetListView(ref rowsCount, intPageSize, CurrencyPage, SearchModel);
            if (rowsCount > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                this.ExportPageInfo1.LinkType = 3;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = rowsCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;

            }
        }
        #region 禁用/启用
        /// <summary>
        /// 退回待审状态
        /// </summary>
        private void StateBack()
        {
            string TeamCreateName = "";
            string TeamCreateMobilePhone = "";
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;
            string ContactName = UserModel.ContactName;
            UserModel = null;
            string s = Utils.GetQueryStringValue("id");
            string[] ids = s.Split(',');
            foreach (var IdStr in ids)
            {
                if (string.IsNullOrEmpty(IdStr))
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                    return;
                }
                #region 取得球队信息
                var model = BTeam.GetViewModel(IdStr);
                if (model != null)
                {
                    TeamCreateName = model.ContactName;
                    TeamCreateMobilePhone = model.MobilePhone;
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                    return;
                }
                #endregion
                bllRetCode = BTeam.UpdateState(IdStr, UserId, ContactName, Model.EnumType.球队审核状态.审核中, "", 0, "");
            }
            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
        }
        /// <summary>
        /// 启用操作
        /// </summary>
        private void EnableData(int TypeId)
        {
            string TeamCreateName = "";
            string TeamCreateMobilePhone = "";
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;
            string ContactName = UserModel.ContactName;
            UserModel = null;
            string s = Utils.GetQueryStringValue("id");
            string[] ids = s.Split(',');
            foreach (var IdStr in ids)
            {
                if (string.IsNullOrEmpty(IdStr))
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                    return;
                }
                #region 取得球队信息
                var model = BTeam.GetViewModel(IdStr);
                if (model != null)
                {
                    TeamCreateName = model.ContactName;
                    TeamCreateMobilePhone = model.MobilePhone;
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                    return;
                }
                #endregion
                switch (TypeId)
                {
                    case 1:
                        bllRetCode = BTeam.UpdateState(IdStr, UserId, ContactName, Model.EnumType.球队审核状态.初审通过, "", 0, "");
                        break;
                    case 2:
                        bllRetCode = BTeam.UpdateState(IdStr, UserId, ContactName, Model.EnumType.球队审核状态.终审通过, "", 0, "");
                        //发送短信
                       // BSMS.Send(TeamCreateMobilePhone, CacheSysMsg.GetMsg(26));
                        break;
                }
            }
            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
        }
        /// <summary>
        /// 禁用操作
        /// </summary>
        private void DisabledData(int TypeId)
        {
            string TeamCreateName = "";
            string TeamCreateMobilePhone = "";
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;
            string ContactName = UserModel.ContactName;
            UserModel = null;
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                return;
            }
            #region 取得球队信息
            var model = BTeam.GetViewModel(s);
            if (model != null)
            {
                TeamCreateName = model.ContactName;
                TeamCreateMobilePhone = model.MobilePhone;
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                return;
            }
            #endregion
            switch (TypeId)
            {
                case 1:
                    bllRetCode = BTeam.UpdateState(s, UserId, ContactName, Model.EnumType.球队审核状态.初审拒绝, "", 0, "");
                    break;
                case 2:
                    bllRetCode = BTeam.UpdateState(s, UserId, ContactName, Model.EnumType.球队审核状态.终审拒绝, "", 0, "");
                    //发送短信
                    //SMSClass.Send(TeamCreateMobilePhone, CacheSysMsg.GetMsg(28));
                    break;
            }
            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
        }
        /// <summary>
        /// 解散操作
        /// </summary>
        private void Disband()
        {
            string TeamCreateName = "";
            string TeamCreateMobilePhone = "";
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;
            string ContactName = UserModel.ContactName;
            UserModel = null;
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                return;
            }
            #region 取得球队信息
            var model = BTeam.GetViewModel(s);
            if (model != null)
            {
                TeamCreateName = model.ContactName;
                TeamCreateMobilePhone = model.MobilePhone;
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                return;
            }
            #endregion
            bllRetCode = BTeam.UpdateState(s, UserId, ContactName, Model.EnumType.球队审核状态.解散通过, "", 0, "");
            /*
            //发送短信
            SMSClass.Send(TeamCreateMobilePhone, CacheSysMsg.GetMsg(29));
             * */
            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
        }
        /// <summary>
        /// 拒绝解散操作
        /// </summary>
        private void NoDisband()
        {
            string TeamCreateName = "";
            string TeamCreateMobilePhone = "";
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int UserId = UserModel.Id;
            string ContactName = UserModel.ContactName;
            UserModel = null;
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                return;
            }
            #region 取得球队信息
            var model = BTeam.GetViewModel(s);
            if (model != null)
            {
                TeamCreateName = model.ContactName;
                TeamCreateMobilePhone = model.MobilePhone;
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                return;
            }
            #endregion
            bllRetCode = BTeam.UpdateState(s, UserId, ContactName, Model.EnumType.球队审核状态.解散拒绝, "", 0, "");
            if (bllRetCode)
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", CacheSysMsg.GetMsg(18)));
            else
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
        }
        #endregion
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int State = Utils.GetInt(Utils.GetFormValue("State"), -1);
            string KeyWord = this.txtKeyWord.Text;
            string TeamOwner = Utils.GetFormValue(txtTeamOwner.UniqueID);
            string MobileNo = Utils.GetFormValue(txtOwnerMobile.UniqueID);
            int IsJoinMatch = Utils.GetInt(Utils.GetFormValue("ddlJoinMatch"), -1);
            int IsTeamer = Utils.GetInt(Utils.GetFormValue("ddlTeamer"), -1);
            int CountryId = Utils.GetInt(Utils.GetFormValue("ddlCountry"), 0);
            int ProvinceId = Utils.GetInt(Utils.GetFormValue("ddlProvince"), 0);
            int CityId = Utils.GetInt(Utils.GetFormValue("ddlCity"), 0);
            int AreaId = Utils.GetInt(Utils.GetFormValue("ddlArea"), 0);
            DateTime? IssueBeginTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtIBeginDate.UniqueID));
            DateTime? IssueEndTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtIEndDate.UniqueID));
            DateTime? CheckBeginTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtCBeginDate.UniqueID));
            DateTime? CheckEndTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtCEndDate.UniqueID));
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&State=" + State + "&KeyWord=" + Server.UrlEncode(KeyWord) + "&TeamOwner=" + Server.UrlEncode(TeamOwner) + "&MobileNo=" + MobileNo + "&IsJoinMatch=" + IsJoinMatch + "&IsTeamer=" + IsTeamer + "&CountryId=" + CountryId + "&ProvinceId=" + ProvinceId + "&CityId=" + CityId + "&AreaId=" + AreaId + "&IssueBeginTime=" + IssueBeginTime + "&IssueEndTime=" + IssueEndTime + "&CheckBeginTime=" + CheckBeginTime + "&CheckEndTime=" + CheckEndTime, true);
        }


        #region 导出Excel

        protected void btnExport_Click(object sender, EventArgs e)
        {
            int rowsCount = 0;
            int State = Utils.GetInt(Utils.GetQueryStringValue("State"), -1);
            string keyword = Utils.GetQueryStringValue("KeyWord");

            Model.MBallTeamSearch SearchModel = new Model.MBallTeamSearch();
            if (State != -1)
            {
                SearchModel.State = (Model.EnumType.球队审核状态)State;
            }
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                SearchModel.KeyWord = keyword.Trim();
            }
            var list = BTeam.GetListView(ref rowsCount, 9999, 1, SearchModel);
            if (rowsCount > 0)
            {
                int Index = 1;
                DataTable dt = new DataTable();
                dt.Columns.Add("编号", typeof(System.Int32));
                dt.Columns.Add("球队名称", typeof(System.String));
                dt.Columns.Add("地域", typeof(System.String));
                dt.Columns.Add("队长姓名", typeof(System.String));
                dt.Columns.Add("队长手机号", typeof(System.String));
                dt.Columns.Add("审核状态", typeof(System.String));
                dt.Columns.Add("参加比赛数", typeof(System.Int32));
                dt.Columns.Add("荣誉值", typeof(System.Int32));
                dt.Columns.Add("队员数", typeof(System.Int32));
                dt.Columns.Add("申请时间", typeof(System.String));
                dt.Columns.Add("审核时间", typeof(System.String));

                foreach (var lst in list)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = Index++;
                    dr[1] = lst.TeamName;
                    dr[2] = lst.CountryName + "-" + lst.ProvinceName + "-" + lst.CityName + "-" + lst.AreaName;
                    dr[3] = lst.ContactName;
                    dr[4] = lst.MobilePhone;
                    dr[5] = (Model.EnumType.球队审核状态)lst.State;
                    dr[6] = GetMatchCount(lst.Id);
                    dr[7] = lst.HonorNumber;
                    dr[8] = GetTeamerCount(lst.Id);
                    dr[9] = lst.IssueTime.ToString("yyyy-MM-dd");
                    dr[10] = DateTime.Parse(lst.IssueTime.ToString()).ToString("yyyy-MM-dd");
                    dt.Rows.Add(dr);
                }

                NPOIHelper.TableToExcelForXLSAny(dt, "球队信息列表");
            }
            else
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", "暂无满足条件的数据"));
                Response.End();
            }
        #endregion

        }

        /// <summary>
        /// 获取球队参赛次数
        /// </summary>
        /// <param name="TeamId"></param>
        /// <returns></returns>
        protected int GetMatchCount(string TeamId)
        {
            return BTeam.GetMatchCount(TeamId);
        }

        /// <summary>
        /// 获取球队队员数量
        /// </summary>
        /// <param name="TeamId"></param>
        /// <returns></returns>
        protected int GetTeamerCount(string TeamId)
        {
            return BTeam.GetTeamerCount(TeamId);
        }

    }
}