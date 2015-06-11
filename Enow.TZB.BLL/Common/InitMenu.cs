using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 加载菜单
    /// </summary>
    public class InitMenu
    {
        #region 菜单管理
        /// <summary>
        /// 一级菜单
        /// </summary>
        /// <returns></returns>
        public static List<MenuList> GetMenuList()
        {
            return CacheDefine.GetMenuList();
        }
        /// <summary>
        /// 一级菜单
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <param name="PerList">权限</param>
        /// <returns></returns>
        public static List<MenuList> GetMenuList(string UserId, string PerList)
        {
            return CacheDefine.GetMenuList(UserId, PerList);
        }
        /// <summary>
        /// 取得菜单信息
        /// </summary>
        /// <param name="MenuId">菜单编号</param>
        /// <returns></returns>
        public static MenuList GetMenuInfo(int menuId)
        {
            using (FWDC rdc = new FWDC())
            {
                var menuModel = rdc.MenuList.FirstOrDefault(n => n.Id == menuId);
                if (menuModel != null)
                {
                    return menuModel;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 取得二级菜单信息
        /// </summary>
        /// <param name="MenuId">菜单编号</param>
        /// <returns></returns>
        public static RoleClass GetSubMenuInfo(int MenuId)
        {
            using (FWDC rdc = new FWDC())
            {
                var menuModel = rdc.RoleClass.FirstOrDefault(n => n.Id == MenuId);
                if (menuModel != null)
                {
                    return menuModel;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 取得根级菜单信息
        /// </summary>
        /// <param name="MenuId">根级菜单编号</param>
        /// <returns></returns>
        public static RoleList GetChildMenuInfo(int ChildMenuId)
        {
            using (FWDC rdc = new FWDC())
            {
                var menuModel = rdc.RoleList.FirstOrDefault(n => n.Id == ChildMenuId);
                if (menuModel != null)
                {
                    return menuModel;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 二级菜单
        /// </summary>
        /// <param name="MenuId">一级菜单编号</param>
        /// <returns></returns>
        public static List<RoleClass> GetSubMenuList(int MenuId)
        {
            var SubMenu = CacheDefine.GetSubMenuList();
            var query = (from m in SubMenu
                         where m.MenuId == MenuId
                         orderby m.Id
                         select m).ToList();
            return query;
        }
        /// <summary>
        /// 二级菜单
        /// </summary>
        /// <param name="MenuId">一级菜单编号</param>
        /// <returns></returns>
        public static List<RoleClass> GetSubMenuList(int MenuId, string PerList)
        {
            using (FWDC rdc = new FWDC())
            {
                if (!String.IsNullOrEmpty(PerList))
                {
                    var menu = rdc.ExecuteQuery<RoleClass>("SELECT DISTINCT Id, MenuId, ClassName,IcoPath from dt_RoleClass WHERE (MenuId = " + MenuId + ") AND (PerId in (" + PerList + "))").ToList();
                    return menu;
                }
                else
                {
                    var menu = rdc.ExecuteQuery<RoleClass>("SELECT DISTINCT Id, MenuId, ClassName,IcoPath from dt_RoleClass WHERE (MenuId = " + MenuId + ")").ToList();
                    return menu;
                }
            }
        }
        /// <summary>
        /// 二级菜单
        /// </summary>
        /// <param name="MenuId">一级菜单编号</param>
        /// <returns></returns>
        public static List<RoleClass> GetSubMenuList(string PerList)
        {
            using (FWDC rdc = new FWDC())
            {
                var menu = rdc.ExecuteQuery<RoleClass>("SELECT DISTINCT Id, MenuId, ClassName,IcoPath from dt_RoleClass WHERE (PerId in (" + PerList + "))").ToList();
                return menu;
            }
        }
        /// <summary>
        /// 子级菜单
        /// </summary>
        /// <param name="SubMenuId">二级菜单编号</param>
        /// <returns></returns>
        public static List<RoleListView> GetChildMenuList(string PerList, bool IsMenu)
        {
            using (FWDC rdc = new FWDC())
            {
                if (IsMenu)//作为菜单显示
                {
                    var menu = rdc.ExecuteQuery<RoleListView>("SELECT Id, MenuId, MenuName, ClassId, ClassName, PerName, Url, IsMenuView from dt_RoleList WHERE (IsMenuView = 1) AND (Id in (" + PerList + "))").ToList();
                    return menu;
                }
                else
                {
                    var menu = rdc.ExecuteQuery<RoleListView>("SELECT Id, MenuId, MenuName, ClassId, ClassName, PerName, Url, IsMenuView from dt_RoleList WHERE (Id in (" + PerList + "))").ToList();
                    return menu;
                }
            }
        }
        /// <summary>
        /// 子级菜单
        /// </summary>
        /// <param name="SubMenuId">二级菜单编号</param>
        /// <returns></returns>
        public static List<RoleListView> GetChildMenuList(int SubMenuId, string PerList, bool IsMenu)
        {
            using (FWDC rdc = new FWDC())
            {
                if (IsMenu)//作为菜单显示
                {
                    var menu = rdc.ExecuteQuery<RoleListView>("SELECT Id, MenuId, MenuName, ClassId, ClassName, PerName, Url, IsMenuView from dt_RoleList WHERE (ClassId = " + SubMenuId + ") AND (IsMenuView = 1) AND (Id in (" + PerList + "))").ToList();
                    return menu;
                }
                else
                {
                    var menu = rdc.ExecuteQuery<RoleListView>("SELECT Id, MenuId, MenuName, ClassId, ClassName, PerName, Url, IsMenuView from dt_RoleList WHERE (ClassId = " + SubMenuId + ") AND (Id in (" + PerList + "))").ToList();
                    return menu;
                }
            }
        }
        /// <summary>
        /// 子级菜单
        /// </summary>
        /// <param name="SubMenuId">二级菜单编号</param>
        /// <returns></returns>
        public static List<RoleList> GetChildMenuList(int SubMenuId, string[] PerList, bool IsMenu)
        {
            List<RoleList> roles = new List<RoleList>();
            var childMenu = GetChildMenuList(SubMenuId, IsMenu);
            foreach (var model in childMenu)
            {
                if (PerList.Contains(model.Id.ToString()))
                {
                    roles.Add(model);
                }
            }
            return roles;
        }
        /// <summary>
        /// 子级菜单
        /// </summary>
        /// <param name="SubMenuId">二级菜单编号</param>
        /// <returns></returns>
        public static List<RoleList> GetChildMenuList(int SubMenuId)
        {
            return GetChildMenuList(SubMenuId, true);
        }
        /// <summary>
        /// 子级菜单
        /// </summary>
        /// <param name="SubMenuId">二级菜单编号</param>
        /// <returns></returns>
        public static List<RoleList> GetChildMenuList(int SubMenuId, bool IsMenu)
        {
            if (IsMenu)//作为菜单显示
            {
                var ChildMenu = CacheDefine.GetChildMenuList();
                var query = (from m in ChildMenu
                             where m.ClassId == SubMenuId && m.IsMenuView == '1'
                             orderby m.Id
                             select m).ToList();
                return query;
            }
            else
            {//全部显示
                var ChildMenu = CacheDefine.GetChildMenuList();
                var query = (from m in ChildMenu
                             where m.ClassId == SubMenuId
                             orderby m.Id
                             select m).ToList();
                return query;
            }
        }
        /// <summary>
        /// 根据URL查询子菜单编号
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        public static int GetChildMenuId(string url)
        {
            int Id = 0;
            var ChildMenu = CacheDefine.GetChildMenuList();
            var query = (from m in ChildMenu
                         where m.Url == url
                         orderby m.Id
                         select m).FirstOrDefault();
            if (query != null)
                Id = query.Id;
            return Id;
        }
#endregion
        #region 取得子菜单信息
        /// <summary>
        /// 取得子菜单信息
        /// </summary>
        /// <param name="MenuId">大菜单</param>
        /// <param name="SubMenuId">子菜单</param>
        /// <returns></returns>
        public static string GetSubMenuStr(string MenuId, string SubMenuId)
        {
            string tmpStr = "";
            if (!String.IsNullOrEmpty(MenuId) && StringValidate.IsInteger(MenuId) == true)
            {
                int menuId = Convert.ToInt32(MenuId);
                //取得登陆用户信息
                ManagerList model = ManageUserAuth.GetManageUserModel();
                if (model != null)
                {
                    string UserId = model.Id.ToString();
                    string perList = model.PermissionList;
                    string strPerList = model.PermissionList;
                    model = null;
                    using (FWDC rdc = new FWDC())
                    {
                        var menuModel = rdc.MenuList.FirstOrDefault(n => n.Id == menuId);
                        if (menuModel != null)
                        {
                            tmpStr = tmpStr + "<div class=\"menubox\"><h3>" + menuModel.MenuName + "</h3><ul>";
                            menuModel = null;
                            IList<RoleClass> list = InitMenu.GetSubMenuList(menuId, perList);
                            foreach (var rcModel in list)
                            {
                                if (rcModel.Id == 26 || rcModel.Id == 27)
                                {
                                }
                                else
                                {
                                    tmpStr = tmpStr + "<li><a href=\"javascript:void();\"><s class=\"" + rcModel.IcoPath + "\"></s>" + rcModel.ClassName + "</a>";
                                    IList<RoleListView> roleList = InitMenu.GetChildMenuList(rcModel.Id, strPerList, true);
                                    if (roleList.Count() > 0)
                                    {
                                        tmpStr = tmpStr + "<div class=\"sub-menu\">";
                                        foreach (var role in roleList)
                                        {
                                            tmpStr = tmpStr + "<a href=\"" + role.Url + "?MId=" + role.MenuId + "&SMId=" + role.ClassId + "&CId=" + role.Id + "\">" + role.PerName + "</a>";
                                        }
                                        tmpStr = tmpStr + "</div>";
                                    }
                                    roleList = null;
                                }
                            }
                            tmpStr = tmpStr + "</ul><div class=\"botbg\"></div></div>";
                        }
                    }
                    tmpStr = tmpStr + "<div class=\"menubox\"><h3>个人助理</h3><ul><li><a href=\"/Manage/ChangePwd.aspx\"><s class=\"mmgl\"></s>密码管理</a></li>";
                    tmpStr = tmpStr + "<li><a href=\"/Manage/QuickMenuSet.aspx\"><s class=\"xxgl\"></s>快捷菜单设置</a></li><li><a href=\"javascript:logout()\"><s class=\"exit\"></s>安全退出</a></li></ul><div class=\"botbg\"></div></div>";
                    return tmpStr;
                }
                else {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
#endregion
    }
}