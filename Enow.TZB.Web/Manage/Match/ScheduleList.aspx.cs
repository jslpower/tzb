using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Match
{
    public partial class ScheduleList : System.Web.UI.Page
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
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();
                InitPage();

                string doType = Utils.GetQueryStringValue("doType").ToLower();
                switch (doType)
                {
                    case "delete":
                        DelGame();
                        break;
                    default:
                        break;
                }

            }
        }

        /// <summary>
        /// 删除赛事阶段
        /// </summary>
        private void DelGame()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            int bllRetCode = BMatchGame.Delete(s) == true ? 1 : -99;

            if (bllRetCode == 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：已成功删除！"));
            else if (bllRetCode == -99) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));

        }

        private void InitPage()
        {
            #region 查询实体
            MMatchGameSearch SearchModel = new MMatchGameSearch();

            SearchModel.MatchName = Utils.GetQueryStringValue("MatchName");
            SearchModel.GameName = Utils.GetQueryStringValue("GameName");
            SearchModel.FieldName = Utils.GetQueryStringValue("FieldName");
            #endregion

            int Rowcount = 0;
            string Page = Request.QueryString["Page"];
            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                CurrencyPage = int.Parse(Page);
                if (CurrencyPage < 1)
                    CurrencyPage = 1;
            }

            List<tbl_MatchGame> list = BMatchGame.GetList(ref Rowcount, intPageSize, CurrencyPage, SearchModel);
            if (Rowcount > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = intPageSize;
                ExportPageInfo1.intRecordCount = Rowcount;
                ExportPageInfo1.CurrencyPage = CurrencyPage;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }
        }
    }
}