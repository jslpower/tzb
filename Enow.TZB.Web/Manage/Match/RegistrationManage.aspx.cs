using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using System.Data;

namespace Enow.TZB.Web.Manage.Match
{
    public partial class RegistrationManage : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            if (!String.IsNullOrWhiteSpace(doType))
            {
                switch (doType)
                {
                    case "StateBack": StateBack(); break;
                    default: break;
                }
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();               
                InitList();
            }
        }
        /// <summary>
        /// 退回待审状态
        /// </summary>
        private void StateBack()
        {
            bool bllRetCode = false;
            var UserModel = ManageUserAuth.GetManageUserModel();
            int userId = UserModel.Id;
            string contractName = UserModel.ContactName;
            UserModel = null;
            string id = Utils.GetQueryStringValue("Id");
            if (string.IsNullOrWhiteSpace(id))
            {

                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                return;
            }
            #region 获取报名球队实体
            var model = BMatchTeam.GetModel(id);
            int number = model.OrdinalNumber + 1;

            if (model != null)
            {
                bllRetCode = BMatchTeam.UpdateValid(id, Model.EnumType.参赛审核状态.资格审核中, userId, contractName, number);
                //球队退回初审审核通过，赛事表已参加球队数量减一
                BMatch.SubtractSignNumber(model.MatchId);
            }
            #endregion

            if (bllRetCode)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(18)));
                return;
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
                return;
            }
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        private void InitList()
        {
            int rowsCount = 0;
            string Page = Request.QueryString["Page"];
            int State = Utils.GetInt(Utils.GetQueryStringValue("State"), -1);
            string keyword = Utils.GetQueryStringValue("KeyWord");
            string matchName = Utils.GetQueryStringValue("MatchName");
            DateTime? StartDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("StartDate"));
            DateTime? EndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("EndDate"));
            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                CurrencyPage = int.Parse(Page);
                if (CurrencyPage < 1)
                    CurrencyPage = 1;
            }

            MMatchTeamSearch searchModel = new MMatchTeamSearch();
            if (State != -1)
            {
                searchModel.State = (Model.EnumType.参赛审核状态)State;
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                searchModel.TeamName = keyword;
            }
            if (!string.IsNullOrWhiteSpace(matchName))
            {
                searchModel.MatchName = matchName;
            }
            if (StartDate.HasValue)
            {
                searchModel.StartDate = StartDate;
            }
            if (EndDate.HasValue)
            {
                searchModel.EndDate = EndDate;
            }
            List<dt_MatchTeam> list = BMatchTeam.GetList(ref rowsCount, intPageSize, CurrencyPage, searchModel);
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




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int State = Utils.GetInt(Utils.GetFormValue("State"), -1);
            string KeyWord = this.txtKeyWord.Text;
            string MatchName = this.txtMatch.Text;
            string StartDate = this.txtStartDate.Text;
            string EndDate = this.txtEndDate.Text;
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&State=" + State + "&MatchName=" + Server.UrlEncode(MatchName) + "&KeyWord=" + Server.UrlEncode(KeyWord)+"&StartDate="+StartDate+"&EndDate="+EndDate, true);
        }


        #region 导出Excel
        protected void btnExport_Click(object sender, EventArgs e)
        {
            int rowsCount = 0;
          
            int State = Utils.GetInt(Utils.GetQueryStringValue("State"), -1);
            string keyword = Utils.GetQueryStringValue("KeyWord");
            string matchName = Utils.GetQueryStringValue("MatchName");
           
            MMatchTeamSearch searchModel = new MMatchTeamSearch();
            if (State != -1)
            {
                searchModel.State = (Model.EnumType.参赛审核状态)State;
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                searchModel.TeamName = keyword;
            }
            if (!string.IsNullOrWhiteSpace(matchName))
            {
                searchModel.MatchName = matchName;
            }
            List<dt_MatchTeam> list = BMatchTeam.GetList(ref rowsCount, 9999, 1, searchModel);
            if (rowsCount > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("球队名称", typeof(System.String));
                dt.Columns.Add("创始人姓名", typeof(System.String));
                dt.Columns.Add("参赛人数", typeof(System.Int32));
                dt.Columns.Add("地域", typeof(System.String));
                dt.Columns.Add("赛事名称", typeof(System.String));
                dt.Columns.Add("球场名称", typeof(System.String));
                dt.Columns.Add("球场地址", typeof(System.String));
                dt.Columns.Add("报名状态", typeof(System.String));
                dt.Columns.Add("支付方式", typeof(System.String));
                dt.Columns.Add("支付状态", typeof(System.String));
                dt.Columns.Add("申请时间", typeof(System.String));
                dt.Columns.Add("审核时间", typeof(System.String));

                foreach (var lst in list)
                {
                    DataRow dr =dt.NewRow();
                    dr[0] = lst.TeamName;
                    dr[1] = lst.TeamOwner;
                    dr[2] = lst.JoinNumber;
                    dr[3] = lst.CountryName + "-" + lst.ProvinceName + "-" + lst.CityName + "-" + lst.AreaName;
                    dr[4] = lst.MatchName;
                    dr[5] = lst.FieldName;
                    dr[6] = lst.FieldAddress;
                    dr[7] = (Model.EnumType.参赛审核状态)lst.State;
                    dr[8] = (Model.EnumType.支付方式)lst.PayType;
                    dr[9] = lst.IsPayed ? "已支付" : "未支付";
                    dr[10] = lst.IssueTime.ToString("yyyy-MM-dd");
                    dr[11] = DateTime.Parse(lst.LastCheckTime.ToString()).ToString("yyyy-MM-dd");
                    dt.Rows.Add(dr);
                }
                NPOIHelper.TableToExcelForXLSAny(dt, "赛事报名报表");


            }

            else
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", "暂无满足条件的数据"));
                Response.End();
            }
        }

        #endregion
    }
}