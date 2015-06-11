using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 日志
    /// </summary>
    public class EventLog
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="intPageSize"></param>
        /// <param name="Page"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static List<tbl_EventLog> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.EnumType.EventType TypeId)
        {
            string FieldsList = "Id,TypeId,OperatorId,OperatorName,EventTitle,Ip,IssueTime";//字段
            string TableName = "tbl_EventLog";//表名
            string OrderString = " ORDER BY IssueTime DESC";//排序
            string strWhere = "(TypeId=" + (int)TypeId + ")";
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_EventLog> q = rdc.ExecuteQuery<tbl_EventLog>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="model"></param>
        public  static void Add(tbl_EventLog model)
        {
            using(FWDC rdc = new FWDC())
            {
                rdc.esp_EventLog_ADD(System.Guid.NewGuid().ToString(), model.TypeId, model.OperatorId, model.OperatorName, model.EventTitle, model.EventInfo, model.Ip, DateTime.Now);
            }
        }
    }
}