using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 关注/点赞信息操作
    /// </summary>
    public class BOfferpat
    {
        /// <summary>
        /// 添加关注/点赞信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public static void Add(tbl_Offerpat model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_Offerpat.InsertOnSubmit(model);
                    rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 删除关注/点赞信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public static bool Delete(string id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Offerpat.FirstOrDefault(w=>w.Id==id);
                if (m != null)
                {
                    rdc.tbl_Offerpat.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
                
            }
        }
        /// <summary>
        /// 查询是否已关注点赞等等
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public static tbl_Offerpat Getmodelbool(tbl_Offerpat model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Offerpat.FirstOrDefault(tmp => tmp.Pattype == model.Pattype && tmp.PatId == model.PatId && tmp.MemberId == model.MemberId);
                if (m!=null)
                {
                    return m;
                }
                return null;
            }
        }
        /// <summary>
        /// 根据用户编号 关注编号查询是否关注
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>true:不为空  false:为空</returns>
        public static bool GetMPidbool(string patid,string memberid)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Offerpat.FirstOrDefault(tmp =>tmp.PatId == patid && tmp.MemberId == memberid);
                if (m != null)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 获取用户关注/点赞集合
        /// </summary>
        /// <param name="Usid">用户编号</param>
        /// <param name="types">分类 1 关注 2点赞</param>
        /// <returns></returns>
        public static List<string> GetStrlist(string Usid,int types)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = (from tmp in rdc.tbl_Offerpat where tmp.MemberId == Usid && tmp.Pattype == types select tmp.PatId).ToList();
                if (m.Count>0)
                {
                    return m;
                }
                return null;
            }
        }
        /// <summary>
        /// 根据用户ID获取关注列表  （关注舵主 堂主）
        /// </summary>
        /// <returns></returns>
        public static List<dt_PatOfferMemberView> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, dt_PatOfferMemberView searchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_PatOfferMemberView";
            string OrderString = " ORDER BY Inserttime Desc";
            string strWhere = "(1=1)";
            strWhere += "AND ( Jobtyoe!=0)";
            if (!string.IsNullOrEmpty(searchModel.OfferpatMemberId))
            {
                strWhere += "AND ( OfferpatMemberId='" + searchModel.OfferpatMemberId + "')";
            }
            if (searchModel.Jobtyoe!=-1)
            {
                strWhere += "AND ( Jobtyoe=" + searchModel.Jobtyoe + ")";
            }
            if (searchModel.Pattype!=-1)
            {
                strWhere += "AND ( Pattype=" + searchModel.Pattype + ")";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_PatOfferMemberView> q = rdc.ExecuteQuery<dt_PatOfferMemberView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 获取关注列表  （球队）
        /// </summary>
        /// <returns></returns>
        public static List<dt_OfferpatBallTeamView> GetqdList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MBallTeamSearch searchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_OfferpatBallTeamView";
            string OrderString = " ORDER BY State ASC,IssueTime Desc";
            string strWhere = "(1=1)";
            #region 查询条件
            if (searchModel.CountryId > 0)
            {
                strWhere = strWhere + " AND (CountryId = " + searchModel.CountryId + ")";
            }
            if (searchModel.ProvinceId > 0)
            {
                strWhere = strWhere + " AND (ProvinceId = " + searchModel.ProvinceId + ")";
            }
            if (searchModel.CityId > 0)
            {
                strWhere = strWhere + " AND (CityId = " + searchModel.CityId + ")";
            }
            if (searchModel.State.HasValue)
            {
                strWhere = strWhere + " AND (State = " + (int)searchModel.State.Value + ")";
            }
            if (searchModel.KeyWord != null)
            {
                strWhere = strWhere + " AND (TeamName like '%" + searchModel.KeyWord + "%')";
            }
            if (!string.IsNullOrEmpty(searchModel.OfferpatMemberID))
            {
                 strWhere = strWhere + " AND (OfferpatMemberId = '" + searchModel.OfferpatMemberID + "')";
            }
            #endregion
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_OfferpatBallTeamView> q = rdc.ExecuteQuery<dt_OfferpatBallTeamView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
    }
}
