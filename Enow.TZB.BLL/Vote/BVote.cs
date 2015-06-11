using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BVote
    {
        /// <summary>
        /// 添加投票信息
        /// </summary>
        /// <param name="model">添加实体</param>
        public static bool Add(tbl_Vote model)
        {
            using (FWDC rdc = new FWDC())
            {

                rdc.tbl_Vote.InsertOnSubmit(model);
                rdc.SubmitChanges();
                return true;

            } 
        }
        /// <summary>
        /// 删除投票信息
        /// </summary>
        /// <param name="listid">要删除的ID集合</param>
        /// <returns></returns>
        public static bool Delete(List<string> listid)
        {
            using (FWDC rdc = new FWDC())
            {
                var liid = listid;
                var m = (from tmp in rdc.tbl_Vote where liid.Contains(tmp.Vid) select tmp).ToList();
                if (m.Count > 0)
                {
                    for (int i = 0; i < m.Count; i++)
                    {
                        m[i].IsDelete = 1;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 修改活动
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_Vote model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Vote.FirstOrDefault(n => n.Vid==model.Vid);
                if (m != null)
                {
                    m.Vtitle = model.Vtitle;//标题
                    m.VRelease = model.VRelease;//发布目标
                    m.Vtype = model.Vtype;//投票类型
                    m.ExpireTime = model.ExpireTime;//到期时间
                    m.Remarks = model.Remarks;//备注
                    m.SponsorId = model.SponsorId;//发起人ID
                    m.SponsorName = model.SponsorName;//发起人名称
                    m.LaunchTime = model.LaunchTime;//发起时间
                    m.MatchId = model.MatchId;//活动赛事ID
                    m.MatchName = model.MatchName;//
                    m.ColumnID = model.ColumnID;//栏目 1 投票 2抽奖
                    m.SubjectType = model.SubjectType;//主体投票分类 1 活动  2 赛事
                    m.EstimateNum = model.EstimateNum;
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 根据ID获取投票实体
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns></returns>
        public static tbl_Vote GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Vote.FirstOrDefault(n => n.Vid == Id);
                return model;
            }
        }
        /// <summary>
        /// 根据标题获取投票实体
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public static tbl_Vote GetModelTitle(string title)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Vote.FirstOrDefault(n => n.Vtitle == title);
                if (model!=null)
                {
                    return model;
                }
                return null;
            }
        }
        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Vote> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, MVoteQuery SearchModel)
        {
            string FieldsList = "*";
            string TableName = " tbl_Vote ";
            string OrderString = " ORDER BY LaunchTime desc";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("(1=1)");
            strWhere.Append(" AND (IsDelete=0)");
            #region 查询条件
            if (SearchModel.ColumnID != null)
                strWhere.Append(" AND ( ColumnID=" + SearchModel.ColumnID + ")");
            if (SearchModel.Release != null)
                strWhere.Append(" AND ( VRelease=" + SearchModel.Release + ")");
            if (SearchModel.types != null)
                strWhere.Append(" AND ( Vtype=" + SearchModel.types + ")");
            if (!string.IsNullOrEmpty(SearchModel.title))
            {
                strWhere.Append(" AND ( Vtitle like '%" + SearchModel.title + "%')");
            }
            #endregion

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere.ToString());
                rowsCount = query.First<int>();
                List<tbl_Vote> q = rdc.ExecuteQuery<tbl_Vote>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere.ToString() + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
    }
}
