using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
    public class BArticleLeaveWord
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public static void Add(tbl_ArticleLeaveWord model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_ArticleLeaveWord.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        public static List<tbl_ArticleLeaveWord> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, string articleid, MArticleLeaveSearch SearchModel)
        {
            string FieldsList = "*";
            string TableName = "tbl_ArticleLeaveWord";
            string OrderString = " ORDER BY IssueTime desc";
            string strWhere = "(1=1)";
            if (!String.IsNullOrEmpty(articleid))
            {
                strWhere = strWhere + " AND (ArticleId = '" + articleid + "')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.MemberId))
            {
                strWhere = strWhere + " AND (MemberId = '" + StringValidate.CheckSql(SearchModel.MemberId.Trim()) + "')";
            }
            if (SearchModel.IsEnable.HasValue)
            {
                string IsValid = SearchModel.IsEnable.Value ? "1" : "0";
                strWhere = strWhere + " AND (IsEnable = " + IsValid + ")";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.KeyWords))
            {
                strWhere = strWhere + " AND (Title LIKE '%" + StringValidate.CheckSql(SearchModel.KeyWords.Trim()) + "%')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_ArticleLeaveWord> q = rdc.ExecuteQuery<tbl_ArticleLeaveWord>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }

        }
        /// <summary>
        /// 返回回复列表
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        public static List<tbl_ArticleLeaveWord> GetReplyList(string ArticleId)
        {            
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.tbl_ArticleLeaveWord
                            where q.ReplyId == ArticleId && q.IsReply == true && q.IsEnable == true
                            orderby q.IssueTime ascending
                            select q;
                return query.ToList();
            }
           
        }
    }
}
