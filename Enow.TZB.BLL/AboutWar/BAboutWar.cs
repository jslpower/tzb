using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BAboutWar
    {
        /// <summary>
        /// 根据会员ID查询球队信息
        /// </summary>
        /// <param name="usid">队长ID</param>
        /// <returns></returns>
        public static tbl_BallTeam Getduizhang(string usid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_BallTeam.FirstOrDefault(n => n.MemberId == usid);
                return model;
            }
        }
        /// <summary>
        /// 根据球队ID查询详细信息
        /// </summary>
        /// <param name="Id">球队ID</param>
        /// <returns></returns>
        public static tbl_BallTeam GetQdid(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_BallTeam.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">实体</param>
        public static bool Add(tbl_AboutWar model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_AboutWar.InsertOnSubmit(model);
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
        /// <param name="listid">ID列表</param>
        /// <returns></returns>
        public static bool Delete(List<string> listid)
        {
            using (FWDC rdc = new FWDC())
            {
                var liid = listid;
                var m = (from tmp in rdc.tbl_AboutWar where liid.Contains(tmp.Aid.ToString()) select tmp).ToList();
                if (m.Count > 0)
                {
                    for (int i = 0; i < m.Count; i++)
                    {
                        m[i].AIsDelete = 1;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 绑定客队编号 名称
        /// </summary>
        /// <param name="KdName">客队名称</param>
        /// <param name="KdId">客队ID</param>
        /// <param name="AID">约战编号</param>
        /// <returns></returns>
        public static bool Updatekdid(string KdName,string KdId,string AID)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_AboutWar.FirstOrDefault(n => n.Aid == AID);
                if (model!=null)
                {
                    model.GuestID = KdId;
                    model.GuestName = KdName;
                    model.AboutState = (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.进行中;
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 修改对于约战状态
        /// </summary>
        /// <param name="m">实体</param>
        /// <returns></returns>
        public static bool UpdateAboutState(tbl_AboutWar m)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_AboutWar.FirstOrDefault(n => n.Aid == m.Aid);
                if (model != null)
                {
                    model.AboutState = m.AboutState;
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 修改战报
        /// </summary>
        /// <param name="AID">约战编号</param>
        /// <returns></returns>
        public static bool Update(tbl_AboutWar tmp)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_AboutWar.FirstOrDefault(n => n.Aid == tmp.Aid);
                if (model != null)
                {
                    model.MainSnum = tmp.MainSnum;
                    model.MainXnum = tmp.MainXnum;
                    model.GuestSnum = tmp.GuestSnum;
                    model.GuestXnum = tmp.GuestXnum;
                    model.AWContent = tmp.AWContent;
                    model.Uptime = tmp.Uptime;
                    model.AboutState = tmp.AboutState;
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns></returns>
        public static tbl_AboutWar GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_AboutWar.FirstOrDefault(n => n.Aid == Id);
                return model;
            }
        }
        /// <summary>
        /// 查询会员所属球队
        /// </summary>
        /// <param name="usid">会员ID</param>
        /// <returns></returns>
        public static string GetUserTeam(string usid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.dt_TeamMemberList.FirstOrDefault(n => n.MemberId == usid);
                if (model!=null)
                {
                    return model.TeamId;
                }
                return "";
            }
        }
        /// <summary>
        /// 根据查询条件获取分页列表信息 (约战表  主场球队表)  状态:约战中
        /// </summary>
        /// <returns></returns>
        public static List<dt_AboutWarBallTeam> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, dt_AboutWarBallTeam SearchModel)
        {
            string FieldsList = "*";
            string TableName = " dt_AboutWarBallTeam ";
            string OrderString = " ORDER BY Releasetime desc";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("(1=1)");
            strWhere.Append(" AND (AIsDelete=0)");
            #region 查询条件
            if (!string.IsNullOrEmpty(SearchModel.title))
                strWhere.Append(" AND ( MainName like '%" + SearchModel.title + "%' OR GuestName like '%" + SearchModel.title + "%')");
            if (SearchModel.AboutState!=0)
            {
                strWhere.Append(" AND ( AboutState="+SearchModel.AboutState+")");
            }
            if (!string.IsNullOrEmpty(SearchModel.TeamId))
            {
                strWhere.Append(" AND ( MainID='" + SearchModel.TeamId + "' OR GuestID='" + SearchModel.TeamId + "' OR TeamId='" + SearchModel.TeamId + "') ");
            }
            if (SearchModel.Wtypes!=-1)
            {
                strWhere.Append(" AND ( Wtypes=" + SearchModel.Wtypes + ")");
            }
            if (SearchModel.Wstates != -1)
            {
                strWhere.Append(" AND ( Wstates=" + SearchModel.Wstates + ")");
            }
            if (SearchModel.Atypes != -1)
            {
                strWhere.Append(" AND ( Atypes=" + SearchModel.Atypes + ")");
            }
            if (SearchModel.AwcityId > 0)
            {
                strWhere.Append(" AND ( AwcityId=" + SearchModel.AwcityId + ")");
            }
            #endregion

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere.ToString());
                rowsCount = query.First<int>();
                List<dt_AboutWarBallTeam> q = rdc.ExecuteQuery<dt_AboutWarBallTeam>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere.ToString() + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }

        }
        /// <summary>
        /// 根据查询条件获取分页列表信息 (约战表)  
        /// </summary>
        /// <returns></returns>
        public static List<tbl_AboutWar> GetAboutWarList(ref int rowsCount, int intPageSize, int CurrencyPage, tbl_AboutWar SearchModel)
        {
            string FieldsList = "*";
            string TableName = " tbl_AboutWar ";
            string OrderString = " ORDER BY AboutTime desc";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("(1=1)");
            strWhere.Append(" AND (AIsDelete=0)");
            #region 查询条件
            if (!string.IsNullOrEmpty(SearchModel.title))
                strWhere.Append(" AND ( title = '" + SearchModel.title + "')");
            if (SearchModel.AboutState != 0)
            {
                if (SearchModel.AboutState==(int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.战报待确认)
                {
                    strWhere.Append(" AND ( AboutState=" + (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.战报待确认 + " OR AboutState=" + (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.战报重填 + " OR AboutState=" + (int)Enow.TZB.Model.EnumType.GathersEnum.约战状态.双方确认 + ")");
                }
                else
                {
                    strWhere.Append(" AND ( AboutState=" + SearchModel.AboutState + ")");
                }
                
            }
            if (!string.IsNullOrEmpty(SearchModel.MainID))
            {
                strWhere.Append(" AND ( MainID='" + SearchModel.MainID + "' OR GuestID='" + SearchModel.MainID + "') ");
            }
            #endregion

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere.ToString());
                rowsCount = query.First<int>();
                List<tbl_AboutWar> q = rdc.ExecuteQuery<tbl_AboutWar>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere.ToString() + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }

        }

        
    }
}
