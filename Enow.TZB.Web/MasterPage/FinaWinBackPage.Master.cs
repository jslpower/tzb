using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Model;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.MasterPage
{
    public partial class FinaWinBackPage : System.Web.UI.MasterPage
    {
        /// <summary>
        /// 子级菜单编号
        /// </summary>
        protected int MenuId = 0, SubMenuId = 0, ChildMenuId = 0;
        /// <summary>
        /// 员工编号
        /// </summary>
        protected int EmployeeId = 0;
        protected string quickMenu = "", perList = "";
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (StringValidate.IsInteger(Request.QueryString["MId"]))
            {
                MenuId = Utils.GetInt(Utils.GetQueryStringValue("MId"));
            }
            if (StringValidate.IsInteger(Request.QueryString["SMId"]))
            {
                SubMenuId = Convert.ToInt32(Request.QueryString["SMId"]);
            }
            if (StringValidate.IsInteger(Request.QueryString["CId"]))
            {
                ChildMenuId = Convert.ToInt32(Request.QueryString["CId"]);
            }
            if (!Page.IsPostBack)
            {
                //Adpost.Common.License.License.CheckLicense();
                ManageUserAuth.ManageLoginCheck();
                this.ltrCurrencyDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
                InitPosition();
                InitTopMenu();
                InitLeftMenu();
            }
        }
        /// <summary>
        /// 初始化当前位置
        /// </summary>
        private void InitPosition()
        {
            this.ltrPosition.Text = "首页";
            if (MenuId > 0)
            {
                var model = InitMenu.GetMenuInfo(MenuId);
                if (model != null)
                {
                    this.ltrPosition.Text = model.MenuName;
                }
            }
            if (SubMenuId > 0)
            {
                var model = InitMenu.GetSubMenuInfo(SubMenuId);
                if (model != null)
                {
                    this.ltrPosition.Text = this.ltrPosition.Text + " > " + model.ClassName;
                }
            }
            if (ChildMenuId > 0)
            {
                var model = InitMenu.GetChildMenuInfo(ChildMenuId);
                if (model != null)
                {
                    this.ltrPosition.Text = this.ltrPosition.Text + " > " + model.PerName;
                }
            }
        }
        /// <summary>
        /// 加载菜单
        /// </summary>
        private void InitTopMenu()
        {
            //取得登陆用户信息
            ManagerList model = ManageUserAuth.GetManageUserModel();
            if (model != null)
            {
                var UserModel = SysUser.GetModel(model.Id);
                if (UserModel != null)
                {
                    EmployeeId = UserModel.EmployeeId;
                    string UserId = model.Id.ToString();
                    perList = UserModel.PermissionList;
                    quickMenu = UserModel.QuickMenu;
                    ltUserName.Text = UserModel.ContactName;
                    model = null;
                    UserModel = null;
                    List<MenuList> menuList = InitMenu.GetMenuList(UserId, perList);
                    this.rptMenuList.DataSource = menuList;
                    this.rptMenuList.DataBind();
                }
            }
        }
        /// <summary>
        /// 加载左侧菜单
        /// </summary>
        private void InitLeftMenu()
        {
            if (MenuId > 0)
            {
                var model = InitMenu.GetMenuInfo(MenuId);
                if (model != null)
                {
                    this.ltrMenuName.Text = model.MenuName;
                    IList<RoleClass> list = InitMenu.GetSubMenuList(MenuId, perList);
                    this.rptSubMenu.DataSource = list;
                    this.rptSubMenu.DataBind();
                }
            }
            else
            {
                this.ltrMenuName.Text = "快捷菜单";
                if (!String.IsNullOrEmpty(quickMenu))
                {
                    IList<RoleClass> list = InitMenu.GetSubMenuList(quickMenu);
                    this.rptSubMenu.DataSource = list;
                    this.rptSubMenu.DataBind();
                }
            }
        }
        /// <summary>
        /// 加载子级菜单显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitChildMenuList(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptChildMenu = (Repeater)e.Item.FindControl("rptChildMenu");
                if (MenuId > 0)
                {
                    RoleClass model = (RoleClass)e.Item.DataItem;
                    rptChildMenu.DataSource = InitMenu.GetChildMenuList(model.Id, perList, true); ;
                    rptChildMenu.DataBind();
                }
                else
                {
                    RoleClass model = (RoleClass)e.Item.DataItem;
                    rptChildMenu.DataSource = InitMenu.GetChildMenuList(model.Id, quickMenu, true); ;
                    rptChildMenu.DataBind();
                }
            }
        }
    }
}
