using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BGoodsClass
    {
        /// <summary>
        /// 删除商品类别
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_GoodsClass.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    rdc.tbl_GoodsClass.DeleteOnSubmit(m);
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
        public static void Add(tbl_GoodsClass model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_GoodsClass.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 修改商品类别
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_GoodsClass model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_GoodsClass.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    if (!string.IsNullOrEmpty(model.ClassName))
                    {
                        m.ClassName = model.ClassName;
                      
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
        public static tbl_GoodsClass GetModel(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_GoodsClass.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 获取商品分类列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_GoodsClass> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, MGoodsClassSearch SearchModel)
        {
            string FieldsList = " Id,Type,ClassName";
            string TableName = " tbl_GoodsClass ";
            string OrderString = " ORDER BY Id ASC";
            string strWhere = "(1=1)";
            if (!String.IsNullOrWhiteSpace(SearchModel.ClassName))
            {
                strWhere = strWhere + " AND ( ClassName like '%" + SearchModel.ClassName + "%')";
            }

            if (SearchModel.IsDelete.HasValue)
            {
                string IsValid = SearchModel.IsDelete.Value ? "1" : "0";
                strWhere = strWhere + " AND (IsDelete = " + IsValid + ")";
            }
            
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_GoodsClass> q = rdc.ExecuteQuery<tbl_GoodsClass>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 获取商品类别列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_GoodsClass> GetTypeList()
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from T in rdc.tbl_GoodsClass
                            where T.ParentId==0 && T.Type==1 && T.IsDelete==false
                            select T;
                return query.Distinct().ToList();
            }
        }

        public static List<tbl_GoodsClass> GetStandardList()
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from T in rdc.tbl_GoodsClass
                            where T.ParentId == 1 && T.Type == 2 && T.IsDelete == false
                            select T;
                return query.Distinct().ToList();
            }
        }
    }
}
