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

namespace Enow.TZB.Web.Manage.Vote
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
                InitPage();

            }
        }
        /// <summary>
        /// 权限验证
        /// </summary>
        private void Getquanxian()
        {
            ManageUserAuth.ManageLoginCheck();
            phadd.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.投票信息新增);
            phUpdate.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.投票信息修改);
            phdelete.Visible = ManageUserAuth.CheckAdminAuth((int)Model.RoleEnum.投票信息删除);
            //发布至
            var fblist = EnumObj.GetList(typeof(Model.EnumType.ReleaseEnum));
            if (fblist != null)
            {
                fblist.Insert(0, new EnumObj { Value = "-1", Text = "全部" });
            }
            dropfbmb.DataSource = fblist;
            dropfbmb.DataBind();
            //分类
            var lxlist = EnumObj.GetList(typeof(Model.EnumType.VoteEnum));
            if (lxlist != null)
            {
                lxlist.RemoveAt(0);
                lxlist.Insert(0, new EnumObj { Value = "-1", Text = "全部" });
            }
            droptypes.DataSource = lxlist;
            droptypes.DataBind();
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        private void InitPage()
        {
            MVoteQuery codequery = new MVoteQuery();
            #region 查询实体
            var Title = Utils.GetQueryStringValue("Title");
            var types = Utils.GetInt(Utils.GetQueryStringValue("types"), -1);
            var fbmb = Utils.GetInt(Utils.GetQueryStringValue("fbmb"), -1);
            //分类
            if (types!=-1)
            {
                codequery.types = types;
                droptypes.SelectedValue = types.ToString();
            }
            
            //标题
            codequery.title = Title;
            txtTitle.Value = Title;
            if (fbmb!=-1)
            {
                codequery.Release = fbmb;
                dropfbmb.SelectedValue = fbmb.ToString();
            }
            codequery.ColumnID = 1;
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
            var list =BVote.GetList(ref rowCounts, intPageSize, CurrencyPage, codequery);
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
        /// 删除
        /// </summary>
        protected void DeleteArticle()
        {
            var id = Utils.GetStringArray(Utils.GetQueryStringValue("id"), ",");
            if (id.Count <= 0)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的信息1！"));
            }
            bool bllRetCode = BActivity.Delete(id);
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