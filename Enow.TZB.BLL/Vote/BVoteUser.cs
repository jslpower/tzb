using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BVoteUser
    {
        /// <summary>
        /// 添加用户投票信息
        /// </summary>
        /// <param name="model">添加实体</param>
        public static bool Add(tbl_VoteUser model)
        {
            using (FWDC rdc = new FWDC())
            {

                rdc.tbl_VoteUser.InsertOnSubmit(model);
                rdc.SubmitChanges();
                return true;

            }
        }
        /// <summary>
        /// 删除投票信息
        /// </summary>
        /// <param name="DelId">要删除的ID</param>
        /// <returns></returns>
        public static bool Delete(string DelId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_VoteUser.FirstOrDefault(w => w.Uid == DelId);
                if (m != null)
                {
                    m.UsVoteIsDelete = 1;
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 根据抽奖信息ID查询抽奖信息
        /// </summary>
        /// <param name="VoteId">抽奖信息ID</param>
        /// <returns></returns>
        public static List<int> GetVotelistCJ(string VoteId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = (from tmp in rdc.tbl_VoteUser where tmp.VoteInfoId == VoteId select tmp.AwardsNum).ToList();
                if (m.Count>0)
                {
                    return m;
                }
              
                return null;
            }
        }
        /// <summary>
        /// 查询用户投票是否已存在
        /// </summary>
        /// <param name="VoteId">要查询的投票选项ID</param>
        /// <param name="UserID">要查询用户ID</param>
        /// <returns></returns>
        public static bool GetVoteBool(string VoteId,string UserID)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_VoteUser.FirstOrDefault(w =>w.VoteInfoId==VoteId&& w.UserId == UserID);
                if (m != null)
                {
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// 获取码分类列表(投票抽奖表  投票奖项表  投票中奖记录表 用户表 连表查询)
        /// </summary>
        /// <returns></returns>
        public static List<dt_VoteUserView> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, MVoteQuery SearchModel)
        {
            string FieldsList = "*";
            string TableName = " dt_VoteUserView ";
            string OrderString = " ORDER BY UIndatetime desc";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("(1=1)");
            strWhere.Append(" AND (IsDelete=0)");
            strWhere.Append(" AND (UsVoteIsDelete=0)");
            strWhere.Append(" AND (OptIsDelete=0)");
            #region 查询条件
            if (SearchModel.Release!=null)
            {
                 strWhere.Append(" AND ( VRelease = " + SearchModel.Release + ")");
            }
            if (!string.IsNullOrEmpty(SearchModel.UserID))
            {
                strWhere.Append(" AND ( UserId = '" + SearchModel.UserID + "')");
            }
            if (!string.IsNullOrEmpty(SearchModel.title))
            {
                strWhere.Append(" AND ( Vtitle like '%" + SearchModel.title + "%')");
            }
            if (!string.IsNullOrEmpty(SearchModel.Vid))
            {
                strWhere.Append(" AND ( Vid = '" + SearchModel.Vid + "')");
            }
            if (SearchModel.ColumnID!=null)
            {
                strWhere.Append(" AND ( ColumnID = " + SearchModel.ColumnID + ")");
            }
            if (SearchModel.AwardsNum!=null)
            {
                strWhere.Append(" AND ( AwardsNum != 0)");
            }
            #endregion

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere.ToString());
                rowsCount = query.First<int>();
                List<dt_VoteUserView> q = rdc.ExecuteQuery<dt_VoteUserView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere.ToString() + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
    }
}
