using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    public class BMatchGame
    {
        /// <summary>
        /// 赛事阶段列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_MatchGame> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MMatchGameSearch searchModel)
        {
            string FieldsList = " * ";
            string TableName = "tbl_MatchGame";
            string OrderString = " ORDER BY MatchId,IssueTime ASC";
            string strWhere = "(1=1)";
            if (!string.IsNullOrEmpty(searchModel.MatchName))
            {
                strWhere = strWhere + " AND (MatchName like '%" + searchModel.MatchName + "%')";
            }
            if (!string.IsNullOrEmpty(searchModel.GameName))
            {
                strWhere = strWhere + " AND (GameName like '%" + searchModel.GameName + "%')";
            }
            if (!string.IsNullOrEmpty(searchModel.FieldName))
            {
                strWhere = strWhere + " AND (FieldName like '%" + searchModel.FieldName + "%')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_MatchGame> q = rdc.ExecuteQuery<tbl_MatchGame>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 赛事阶段实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_MatchGame GetModel(string Id)
        {
            using (FWDC rdc=new FWDC())
            {
                var model = rdc.tbl_MatchGame.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 返回比赛赛程
        /// </summary>
        /// <param name="MatchId"></param>
        /// <returns></returns>
        public static List<tbl_MatchGame> GetList(string MatchId)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.tbl_MatchGame
                            where q.MatchId == MatchId
                            orderby q.IssueTime ascending
                            select q;
                return query.ToList();
            }
        }
        /// <summary>
        /// 添加赛事阶段
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_MatchGame model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_MatchGame.InsertOnSubmit(model);
                    rdc.SubmitChanges();
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 修改赛事阶段信息
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_MatchGame model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MatchGame.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.MatchFieldId = model.MatchFieldId;
                    m.FieldName = model.FieldName;
                    m.GameName = model.GameName;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 删除赛事阶段信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(string id)
        {
            using (FWDC rdc=new FWDC())
            {
                 var m = rdc.tbl_MatchGame.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    rdc.tbl_MatchGame.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

    }
}
