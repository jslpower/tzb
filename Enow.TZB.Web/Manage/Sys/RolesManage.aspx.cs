using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Sys
{
    public partial class RolesManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");

            switch (doType)
            {
                case "delete": DeleteData(); break;
                default: break;
            }
            #endregion
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginUrlCheck();
                InitList();
            }
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitList()
        {
            string KeyWord = Utils.GetQueryStringValue("KeyWord");
            if (!String.IsNullOrEmpty(KeyWord))
                KeyWord = Server.UrlDecode(KeyWord);
            List<tbl_UserRole> ResultList = UserRole.GetList(KeyWord);
            if (ResultList.Count() > 0)
            {
                this.phNoData.Visible = false;
                this.rptList.DataSource = ResultList;
                this.rptList.DataBind();
            }
            else
            {
                this.phNoData.Visible = true;
            }
        }
        #region 批量删除
        /// <summary>
        /// 删除操作
        /// </summary>
        private void DeleteData()
        {
            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            if (StringValidate.IsInteger(s) == false) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息！"));
            int bllRetCode = UserRole.Delete(Convert.ToInt32(s));

            if (bllRetCode == 1) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：已成功删除！"));
            else if (bllRetCode == -99) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));
        }
        #endregion
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string KeyWord = this.txtKeyWord.Text;
            string uri = UtilsCommons.GetMenuUri(Request.ServerVariables["SCRIPT_NAME"]);
            Response.Redirect(uri + "&KeyWord=" + Server.UrlEncode(KeyWord), true);
        }
    }
}