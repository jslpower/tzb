using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.Manage
{
    public partial class QuickMenuSet : System.Web.UI.Page
    {
        private string quickMenu = null;
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitUserInfo();
            }
        }
        /// <summary>
        /// 错误提醒
        /// </summary>
        private void ErrMsg()
        {
            MessageBox.ResponseScript("alert('未找到您要授权的用户信息!');parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();");
            return;
        }
        /// <summary>
        /// 加载角色信息
        /// </summary>
        private void InitUserInfo()
        {
            //取得登陆用户信息
            ManagerList ManageModel = ManageUserAuth.GetManageUserModel();
            if (ManageModel != null)
            {
                var model = SysUser.GetModel(ManageModel.Id);
                if (model != null)
                {
                    quickMenu = model.QuickMenu;
                    InitPerList(model.Id, model.PermissionList);
                }
                else { ErrMsg(); }
            }
            else { ErrMsg(); }
        }
        #region 绑定权限
        /// <summary>
        /// 加载管理员权限列表
        /// </summary>
        private void InitPerList(int UserId, string MenuList)
        {
            List<MenuList> menuList = InitMenu.GetMenuList(UserId.ToString(), MenuList);
            this.dlOperatorPermission.DataSource = menuList;
            this.dlOperatorPermission.DataBind();

        }
        /// <summary>
        /// 显示权限内容
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string ShowOP(string ID, int ClassID)
        {
            string tbName = "child" + ClassID.ToString();
            string ReturnVal = "";
            ReturnVal = "<table width=\"100%\" bgcolor=\"#FFFFFF\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" id='" + tbName + "'>";
            List<RoleList> Roles = InitMenu.GetChildMenuList(ClassID, true);
            if (Roles.Count > 0)
            {
                foreach (RoleList model in Roles)
                {
                    if (quickMenu != null && quickMenu.Length > 0)
                    {
                        string tmpChecked = quickMenu.Contains(model.Id.ToString()) ? " checked" : "";
                        ReturnVal += "<tr>";
                        ReturnVal += "<td width=\"25\" height=\"23\" align=\"center\"><input name=\"PermissionID\" type=\"checkbox\" id=\"PermissionID_" + ID + "\" value=\"" + model.Id.ToString() + "\"" + tmpChecked + "></td><td>" + model.PerName + "</td>";
                        ReturnVal += "</tr>";
                    }
                    else
                    {
                        ReturnVal += "<tr>";
                        ReturnVal += "<td width=\"25\" height=\"23\" align=\"center\"><input name=\"PermissionID\" type=\"checkbox\" id=\"PermissionID_" + ID + "\" value=\"" + model.Id.ToString() + "\"></td><td>" + model.PerName + "</td>";
                        ReturnVal += "</tr>";
                    }
                }
            }
            ReturnVal += "</table>";
            return ReturnVal;
        }
        /// <summary>
        /// 绑定子类权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dlOperatorPermission_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                int intPerCategoryID = 0;
                System.Web.UI.WebControls.DataList _dlPermissionClass = (System.Web.UI.WebControls.DataList)e.Item.FindControl("dlPermissionClass");
                MenuList _drv = (MenuList)e.Item.DataItem;
                intPerCategoryID = _drv.Id;
                _drv = null;
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _dlPermissionClass.DataSource = InitMenu.GetSubMenuList(intPerCategoryID);
                    _dlPermissionClass.DataBind();
                }
            }
        }
        #endregion
        /// <summary>
        /// 快捷菜单保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            //取得登陆用户信息
            ManagerList model = ManageUserAuth.GetManageUserModel();
            string PerList = Request.Form["PermissionID"];
            SysUser.UpdateQuickMenu(new ManagerList
            {
                Id = model.Id,
                QuickMenu = PerList
            });
            MessageBox.ShowAndRedirect("快捷菜单设置成功！", "QuickMenuSet.aspx");
            return;
        }
    }
}