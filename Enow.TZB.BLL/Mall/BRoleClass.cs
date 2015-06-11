using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BRoleClass
    {
        /// <summary>
        /// 删除商品类别
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(List<string> idlist)
        {
            using (FWDC rdc = new FWDC())
            {
                var idal=idlist;
                var m =(from tmp in rdc.tbl_GoodsRoleClass where idal.Contains(tmp.Id.ToString()) select tmp).ToList();
                if (m.Count>0)
                {
                    for (int i = 0; i < m.Count; i++)
                    {
                        m[i].IsDelete = 1;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 添加商品分类
        /// </summary>
        /// <param name="model"></param>
        public static void Add(tbl_GoodsRoleClass model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_GoodsRoleClass.FirstOrDefault(w => w.Rolename == model.Rolename && w.Type == model.Type);
                if (m!=null)
                {
                    m.IsDelete = 0;
                }
                else
                {
                    rdc.tbl_GoodsRoleClass.InsertOnSubmit(model);
                }
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 修改商品类别
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_GoodsRoleClass model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_GoodsRoleClass.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    if (!string.IsNullOrEmpty(model.Rolename))
                    {
                        m.Rolename = model.Rolename;
                        m.Type = model.Type;

                    }
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 取得商品类别实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_GoodsRoleClass GetModel(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_GoodsRoleClass.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 取得商品类别实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static string GetName(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_GoodsRoleClass.FirstOrDefault(n => n.Id == Id);
                if (model!=null)
                {
                    return model.Rolename;
                }
                return "";
            }
        }
        /// <summary>
        /// 获取商品分类列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_GoodsRoleClass> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, MGoodsClassSearch SearchModel)
        {
            string FieldsList = "*";
            string TableName = " tbl_GoodsRoleClass ";
            string OrderString = " ORDER BY Id desc";
            string strWhere = "(1=1)";
            strWhere += " AND (IsDelete=0)";
            if (!String.IsNullOrWhiteSpace(SearchModel.ClassName))
            {
                strWhere = strWhere + " AND ( Rolename like '%" + SearchModel.ClassName + "%')";
            }

            if (SearchModel.GoodsType!=null)
            {
                strWhere = strWhere + " AND (Type = " + SearchModel.GoodsType + ")";
            }

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_GoodsRoleClass> q = rdc.ExecuteQuery<tbl_GoodsRoleClass>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 获取商品类别列表
        /// </summary>
        /// <param name="Types">一级分类编号</param>
        /// <returns></returns>
        public static List<tbl_GoodsRoleClass> GetTypeList(int Types)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from T in rdc.tbl_GoodsRoleClass
                            where T.Type == Types && T.IsDelete == 0
                            select T;
                return query.Distinct().ToList();
            }
        }
    }
}
