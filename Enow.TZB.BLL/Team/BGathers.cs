using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
    public class BGathers
    {
        /// <summary>
        /// 约战
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static void Add(tbl_Gathers model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_Gathers.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }

        public static bool AccepWar(string id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Gathers.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    m.IsAcceptWar = true;
                    rdc.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool IsGatherResult(string id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Gathers.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    m.IsGatherResult = true;
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
        /// 获取约战实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_Gathers GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Gathers.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
       
        /// <summary>
        /// 获取发起的约战列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Gathers> GetList(ref int rowsCount,int intPageSize, int CurrencyPage, MGathersSearch SearchModel)
        {
            string FieldsList = "*";
            string TableName = "tbl_Gathers";
            string OrderString = " ORDER BY WarTime desc";
            string strWhere = "(1=1)";
           
            if (!String.IsNullOrWhiteSpace(SearchModel.MemberId))
            {
                strWhere = strWhere + " AND (MemberId = '" + StringValidate.CheckSql(SearchModel.MemberId.Trim()) + "')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.TeamId))
            {
                strWhere = strWhere + " AND (TeamId = '" + StringValidate.CheckSql(SearchModel.TeamId.Trim()) + "')";

                //strWhere = strWhere + " AND (AcceptTeamId <> '" + StringValidate.CheckSql(SearchModel.TeamId.Trim()) + "')";
            }

            if (!String.IsNullOrWhiteSpace(SearchModel.Name))
            {
                strWhere = strWhere + " AND (AcceptTeamName LIKE '%" + StringValidate.CheckSql(SearchModel.Name.Trim()) + "%')";
            }
            if (SearchModel.IsAcceptWar.HasValue)
            {
                string IsAcceptWar = SearchModel.IsAcceptWar.Value ? "1" : "0";
                strWhere = strWhere + " AND (IsAcceptWar = " + IsAcceptWar + ")";
            }
            if (SearchModel.IsGatherResult.HasValue)
            {
                string IsGatherResult = SearchModel.IsGatherResult.Value ? "1" : "0";
                strWhere = strWhere + " AND (IsGatherResult = " + IsGatherResult + ")";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_Gathers> q = rdc.ExecuteQuery<tbl_Gathers>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 获取收到的约战列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Gathers> GetAcceptList(ref int rowsCount, int intPageSize, int CurrencyPage, MGathersSearch SearchModel)
        {
            string FieldsList = "*";
            string TableName = "tbl_Gathers";
            string OrderString = " ORDER BY WarTime desc";
            string strWhere = "(1=1)";


            if (!String.IsNullOrWhiteSpace(SearchModel.AcceptTeamId))
            {
                strWhere = strWhere + " AND (AcceptTeamId = '" + StringValidate.CheckSql(SearchModel.AcceptTeamId.Trim()) + "')";

                strWhere = strWhere + " AND (TeamId <> '" + StringValidate.CheckSql(SearchModel.AcceptTeamId.Trim()) + "')";
            }

            if (!String.IsNullOrWhiteSpace(SearchModel.Name))
            {
                strWhere = strWhere + " AND (TeamName LIKE '%" + StringValidate.CheckSql(SearchModel.Name.Trim()) + "%')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.AcceptTeamName))
            {
                strWhere = strWhere + " AND (AcceptTeamName = '" + StringValidate.CheckSql(SearchModel.AcceptTeamName.Trim()) + "')";
            }
            if (SearchModel.IsAcceptWar.HasValue)
            {
                string IsAcceptWar = SearchModel.IsAcceptWar.Value ? "1" : "0";
                strWhere = strWhere + " AND (IsAcceptWar = " + IsAcceptWar + ")";
            }
            if (SearchModel.IsGatherResult.HasValue)
            {
                string IsGatherResult = SearchModel.IsGatherResult.Value ? "1" : "0";
                strWhere = strWhere + " AND (IsGatherResult = " + IsGatherResult + ")";
            }
            
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_Gathers> q = rdc.ExecuteQuery<tbl_Gathers>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
    }
}
