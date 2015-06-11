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
    /// <summary>
    /// 角色添加
    /// </summary>
    public partial class RoleAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitPerList();
            }
        }
        #region 绑定权限
        /// <summary>
        /// 加载管理员权限列表
        /// </summary>
        private void InitPerList()
        {
            this.dlOperatorPermission.DataSource = InitMenu.GetMenuList();
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
            List<RoleList> Roles = InitMenu.GetChildMenuList(ClassID, false);
            if (Roles.Count > 0)
            {
                foreach (RoleList model in Roles)
                {
                    ReturnVal += "<tr>";
                    ReturnVal += "<td width=\"25\" height=\"23\" align=\"left\"><input name=\"PermissionID\" type=\"checkbox\" id=\"PermissionID_" + ID + "\" value=\"" + model.Id.ToString() + "\"></td><td>" + model.PerName + "</td>";
                    ReturnVal += "</tr>";
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
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string RoleName = this.txtRoleName.Text;
                string Remark = this.txtRemark.Text;
                string PerList = Request.Form["PermissionID"];
                UserRole.Add(new tbl_UserRole
                {
                    RoleName = RoleName,
                    Remark = Remark,
                    PerList = PerList
                });
                MessageBox.ShowAndParentReload("添加成功！");
                return;
            }

        }
    }
}