using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Videocode
{
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
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("dotype");
            switch (doType)
            {
                case "delete": DeleteArticle(); break;
                default: break;
            }
            if (!IsPostBack)
            {
                
                Getquanxian();
                Gettypevlue();
                InitPage();

            }

        }
        /// <summary>
        /// 权限验证
        /// </summary>
        private void Getquanxian()
        {
            ManageUserAuth.ManageLoginCheck();
            phadd.Visible= ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.码生成);
            phUpdate.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.码修改);
            phdelete.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.码删除);
        }
        /// <summary>
        /// 初始化分类
        /// </summary>
        private void Gettypevlue()
        {
            var list = EnumObj.GetList(typeof(Model.EnumType.VideoCodeEnum));
            if (list != null)
            {
                list.Insert(0, new EnumObj { Value = "0", Text = "全部" });
            }
            droptype.DataSource = list;
            droptype.DataBind();
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        private void InitPage()
        {
            McodesQuery codequery = new McodesQuery();
            #region 查询实体
            var Title = Utils.GetQueryStringValue("Title");
            var types = Utils.GetInt(Utils.GetQueryStringValue("types"),0);
            var states = Utils.GetInt(Utils.GetQueryStringValue("states"), -1);
            codequery.Codestate = states;
            ddlIsValid.Value = states.ToString();
            codequery.Codetype = types;
            droptype.SelectedValue = types.ToString();
            codequery.Codenum = Title;
            txtTitle.Value = Title;
            #endregion


            int rowCounts = 0;
            string Page = Request.QueryString["Page"];

            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            var list = Bcode.GetList(ref rowCounts, intPageSize, CurrencyPage, codequery);
            if (rowCounts > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = intPageSize;
                ExportPageInfo1.intRecordCount = rowCounts;
                ExportPageInfo1.CurrencyPage = CurrencyPage;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        protected void DeleteArticle()
        {
            var id = Utils.GetStringArray(Utils.GetQueryStringValue("id"), ",");
            if (id.Count <= 0)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息1！"));
            }
            bool bllRetCode = Bcode.Delete(id);
            if (bllRetCode)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：已成功删除！"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", CacheSysMsg.GetMsg(31)));
            }
        }
        
    }
}