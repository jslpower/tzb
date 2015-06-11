using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
   public class ArticleClass
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_SysType> GetList(ref int rowsCount, int intPageSize, int CurrencyPage,MarticleTypeSeach SearchModel)
        {
            string FieldsList = " ID,TypeId,TypeName,TypePhoto,Remark";
            string TableName = " tbl_SysType ";
            string OrderString = " ORDER BY Id ASC";
            string strWhere = "(1=1)";
           
            if (!String.IsNullOrWhiteSpace(SearchModel.TypeName))
            {
                strWhere = strWhere + " AND ( TypeName '%" + SearchModel.TypeName + "%')";
            }
            if (SearchModel.TypeId.HasValue)
            {
                strWhere += " and ( TypeId=" + SearchModel.TypeId + ")";
            }
            if (SearchModel.id.HasValue)
            {
                strWhere += " and ( id=" + SearchModel.id + ")";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_SysType> q = rdc.ExecuteQuery<tbl_SysType>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public static void Add(tbl_SysType model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_SysType.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 取得资讯类别实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_SysType GetModel(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SysType.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }

        /// <summary>
        /// 修改资讯类别
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_SysType model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SysType.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    if (!string.IsNullOrEmpty(model.TypeName))
                    {
                        m.TypeName = model.TypeName;
                        m.TypePhoto = model.TypePhoto;
                        m.Remark = m.Remark;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 删除资讯类别
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SysType.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    rdc.tbl_SysType.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
    }
}
