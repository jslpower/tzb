using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 消息中心
    /// </summary>
    public class BMessage
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Message> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MMesageSearch SearchModel)
        {
            string FieldsList = "*";
            string TableName = "tbl_Message";
            string OrderString = " ORDER BY IssueTime desc";
            string strWhere = "(MasterMsgId='0')";
            if (SearchModel.TypeId.HasValue)
            {
                strWhere = strWhere + " AND (TypeId = " + (int)SearchModel.TypeId.Value + ")";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.SendId))
            {
                strWhere = strWhere + " AND (SendId = '" + SearchModel.SendId.Trim()+ "')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.ReceiveId))
            {
                strWhere = strWhere + " AND (ReceiveId = '" + SearchModel.ReceiveId.Trim() + "')";
            }
            if (SearchModel.IsRead.HasValue)
            {
                string IsRead = "0";
                if (SearchModel.IsRead.Value)
                    IsRead = "1";
                strWhere = strWhere + " AND (IsRead = " + IsRead + ")";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_Message> q = rdc.ExecuteQuery<tbl_Message>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 未读消息统计
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public static int MsgCount(string MemberId) {
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM tbl_Message WHERE (ReceiveId = '" + MemberId + "') AND (IsRead = 0)");
                int rowsCount = query.First<int>();
                return rowsCount;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public static void Add(tbl_Message model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_Message.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }

        /// <summary>
        /// 取得实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_Message GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Message.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 将消息设为已读
        /// </summary>
        /// <param name="Id"></param>
        public static bool UpdateReadState(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Message.FirstOrDefault(n => n.Id == Id);
                if (model != null)
                {
                    model.IsRead = true;
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
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool Delete(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Message.FirstOrDefault(n => n.Id == Id);
                if (m != null)
                {
                    rdc.tbl_Message.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
    }
}
