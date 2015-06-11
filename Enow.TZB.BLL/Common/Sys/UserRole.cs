using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.Web;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_UserRole> GetList(string KeyWord)
        {
            using (FWDC rdc = new FWDC())
            {
                if (String.IsNullOrEmpty(KeyWord))
                {
                    var list = rdc.tbl_UserRole.ToList();
                    return list;
                }
                else
                {
                    var list = (from q in rdc.tbl_UserRole
                                where SqlMethods.Like(q.RoleName, "%" + StringValidate.CheckSql(KeyWord.Trim()) + "%")
                                select q).ToList();
                    return list;
                }
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        public static void Add(tbl_UserRole model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_UserRole.InsertOnSubmit(model);
                rdc.SubmitChanges();
                CacheDefine.RemoveMenuCache();
                CacheDefine.RemoveSubMenuCache();
                CacheDefine.RemoveChildMenuCache();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public static void Edit(tbl_UserRole model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.esp_UserRole_Update(model.Id, model.RoleName, model.PerList, model.Remark);
                var List = (from n in rdc.ManagerList
                                  where n.RoleId == model.Id
                                  select n).ToList();
                if (List != null)
                {
                    CacheUtility cache = new CacheUtility();
                    foreach (ManagerList item in List)
                    {
                        cache.RemoveCache(CacheNameDefine.MenuList + "_" + item.Id);
                    }
                }//去除权限组下用户的缓存
                CacheDefine.RemoveMenuCache();
                CacheDefine.RemoveSubMenuCache();
                CacheDefine.RemoveChildMenuCache();
            }
        }
        /// <summary>
        /// 取得职位信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_UserRole GetModel(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_UserRole.FirstOrDefault(n => n.Id == Id);
                if (model != null)
                    return model;
                else
                    return null;
            }
        }
        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static int Delete(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                int sqlExceptionCode = 0;
                try
                {
                    var model = rdc.tbl_UserRole.FirstOrDefault(n => n.Id == Id);
                    if (model != null)
                    {
                        rdc.tbl_UserRole.DeleteOnSubmit(model);
                        rdc.SubmitChanges();
                        sqlExceptionCode = 1;
                    }
                    else
                    {
                        sqlExceptionCode = -99;
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    sqlExceptionCode = 0 - e.Number;
                }

                return sqlExceptionCode;
            }
        }
    }
}