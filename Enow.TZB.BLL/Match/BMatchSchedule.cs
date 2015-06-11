using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    public class BMatchSchedule
    {
        /// <summary>
        /// 赛事阶段列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_MatchSchedule> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MMatchScheduleSearch searchModel)
        {
            string FieldsList = " * ";
            string TableName = "tbl_MatchSchedule";
            string OrderString = " ORDER BY MatchId,GameId,MatchFieldId,OrdinalNumber ASC";
            string strWhere = "(IsDeleted=0)";
            if (!string.IsNullOrEmpty(searchModel.MatchId))
            {
                strWhere = strWhere + " AND (MatchId = '" + searchModel.MatchId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.MatchName))
            {
                strWhere = strWhere + " AND (MatchName like '%" + searchModel.MatchName + "%')";
            }
            if (!string.IsNullOrEmpty(searchModel.MatchFieldId))
            {
                strWhere = strWhere + " AND (MatchFieldId = '" + searchModel.MatchFieldId + "')";
            }

            if (!string.IsNullOrEmpty(searchModel.FieldName))
            {
                strWhere = strWhere + " AND (FieldName like '%" + searchModel.FieldName + "%')";
            }
            if (!string.IsNullOrEmpty(searchModel.GameId))
            {
                strWhere = strWhere + " AND (GameId = '" + searchModel.GameId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.GameName))
            {
                strWhere = strWhere + " AND (GameName like '%" + searchModel.GameName + "%')";
            }
            if (!string.IsNullOrEmpty(searchModel.MatchId))
            {
                strWhere = strWhere + " AND (MatchId = '" + searchModel.MatchId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.HomeMatchTeamId))
            {
                strWhere = strWhere + " AND (HomeMatchTeamId = '" + searchModel.HomeMatchTeamId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.HomeTeamId))
            {
                strWhere = strWhere + " AND (HomeTeamId = '" + searchModel.HomeTeamId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.AwayMatchTeamId))
            {
                strWhere = strWhere + " AND (AwayMatchTeamId = '" + searchModel.AwayMatchTeamId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.AwayTeamId))
            {
                strWhere = strWhere + " AND (AwayTeamId = '" + searchModel.AwayTeamId + "')";
            }
            if (searchModel.GameState.HasValue)
            {
                strWhere = strWhere + " AND (GameState=" + (int)searchModel.GameState.Value + ")";
            }
            if (searchModel.PublishState.HasValue)
            {
                strWhere = strWhere + " AND (PublishState=" + (int)searchModel.PublishState.Value + ")";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_MatchSchedule> q = rdc.ExecuteQuery<tbl_MatchSchedule>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 赛事列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_MatchSchedule> GetList(Model.MMatchScheduleSearch searchModel)
        {
            string FieldsList = " * ";
            string TableName = "tbl_MatchSchedule";
            string OrderString = " ORDER BY OrdinalNumber ASC";
            string strWhere = "(IsDeleted=0)";
            if (!string.IsNullOrEmpty(searchModel.MatchId))
            {
                strWhere = strWhere + " AND (MatchId = '" + searchModel.MatchId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.MatchFieldId))
            {
                strWhere = strWhere + " AND (MatchFieldId = '" + searchModel.MatchFieldId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.GameId))
            {
                strWhere = strWhere + " AND (GameId = '" + searchModel.GameId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.MatchId))
            {
                strWhere = strWhere + " AND (MatchId = '" + searchModel.MatchId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.HomeMatchTeamId))
            {
                strWhere = strWhere + " AND (HomeMatchTeamId = '" + searchModel.HomeMatchTeamId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.HomeTeamId))
            {
                strWhere = strWhere + " AND (HomeTeamId = '" + searchModel.HomeTeamId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.AwayMatchTeamId))
            {
                strWhere = strWhere + " AND (AwayMatchTeamId = '" + searchModel.AwayMatchTeamId + "')";
            }
            if (!string.IsNullOrEmpty(searchModel.AwayTeamId))
            {
                strWhere = strWhere + " AND (AwayTeamId = '" + searchModel.AwayTeamId + "')";
            }
            if (searchModel.GameState.HasValue)
            {
                strWhere = strWhere + " AND (GameState=" + (int)searchModel.GameState.Value + ")";
            }
            if (searchModel.PublishState.HasValue)
            {
                strWhere = strWhere + " AND (PublishState=" + (int)searchModel.PublishState.Value + ")";
            }
            using (FWDC rdc = new FWDC())
            {
                List<tbl_MatchSchedule> q = rdc.ExecuteQuery<tbl_MatchSchedule>(@"SELECT " + FieldsList + " FROM  " + TableName + " WHERE " + strWhere + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 添加赛事日程
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_MatchSchedule model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_MatchSchedule.InsertOnSubmit(model);
                    rdc.SubmitChanges();
                    BMatchSchedule.UpdateBallot(model.HomeMatchTeamId);
                    BMatchSchedule.UpdateBallot(model.AwayMatchTeamId);
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 判断球队赛程是否已存在
        /// </summary>
        /// <param name="matchId">赛事ID</param>
        /// <param name="gameId">赛事阶段ID</param>
        /// <param name="homeTeamId">主队ID</param>
        /// <param name="awayTeamId">客队ID</param>
        /// <returns></returns>
        public static bool IsExists(string matchId, string gameId, string homeTeamId, string awayTeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MatchSchedule.FirstOrDefault(n => n.MatchId == matchId && n.GameId == gameId && ((n.HomeTeamId == homeTeamId && n.AwayTeamId == awayTeamId) || (n.AwayTeamId == homeTeamId && n.HomeTeamId == awayTeamId)));
                if (model != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 更新参赛球队抽签状态
        /// </summary>
        /// <param name="ScheduleId"></param>
        /// <param name="IsHomeTeam"></param>
        public static void UpdateBallot(string ScheduleId,bool IsHomeTeam)
        {
            using (FWDC rdc = new FWDC())
            {
                if (IsHomeTeam)
                {
                    //更新主队抽签状态
                    rdc.ExecuteCommand(@"UPDATE tbl_MatchSchedule SET HomeIsBallot=1 WHERE (Id='" + ScheduleId + "')");
                }
                else
                {
                    //更新客队抽签状态
                    rdc.ExecuteCommand(@"UPDATE tbl_MatchSchedule SET AwayIsBallot=1 WHERE (Id='" + ScheduleId + "')");
                }
            }
        }
        /// <summary>
        /// 更新参赛球队抽签状态
        /// </summary>
        /// <param name="MatchTeamId"></param>
        /// <param name="MatchCode"></param>
        /// <returns></returns>
        public static void UpdateBallot(string MatchTeamId)
        {
            using (FWDC rdc = new FWDC())
            {
                //更新主队抽签状态
                rdc.ExecuteCommand(@"UPDATE tbl_MatchSchedule SET HomeIsBallot=1 WHERE (HomeMatchTeamId='" + MatchTeamId + "')");
                //更新客队抽签状态
                rdc.ExecuteCommand(@"UPDATE tbl_MatchSchedule SET AwayIsBallot=1 WHERE (AwayMatchTeamId='" + MatchTeamId + "')");
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ScheduleIdList"></param>
        public static void BatchDelete(string ScheduleIdList)
        {
            using (FWDC rdc = new FWDC())
            {
                if (!String.IsNullOrWhiteSpace(ScheduleIdList))
                {
                    rdc.ExecuteCommand(@"UPDATE tbl_MatchSchedule SET IsDeleted=1 WHERE (Id IN (" + ScheduleIdList + "))");
                }
            }
        }
        /// <summary>
        /// 取得赛事日程实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_MatchSchedule GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MatchSchedule.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }

        /// <summary>
        /// 更新赛事日程
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_MatchSchedule model)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.tbl_MatchSchedule.FirstOrDefault(n => n.Id == model.Id);
                if (model != null)
                {
                    m.Id = model.Id;
                    m.OperatorId = model.OperatorId;
                    m.ContactName = model.ContactName;
                    m.MatchFieldId = model.MatchFieldId;
                    m.FieldName = model.FieldName;
                    m.GameId = model.GameId;
                    m.GameName = model.GameName;
                    m.GameStartTime = model.GameStartTime;
                    m.GameEndTime = model.GameEndTime;
                    m.HomeMatchTeamId = model.HomeMatchTeamId;
                    m.HomeTeamId = model.HomeTeamId;
                    m.HomeTeamName = model.HomeTeamName;
                    m.HomeMatchCode = model.HomeMatchCode;
                    m.AwayMatchTeamId = model.AwayMatchTeamId;
                    m.AwayTeamId = model.AwayTeamId;
                    m.AwayTeamName = model.AwayTeamName;
                    m.AwayMatchCode = model.AwayMatchCode;
                    m.GameState = model.GameState;
                    m.PublishState = model.PublishState;
                    m.OrdinalNumber = model.OrdinalNumber;
                    rdc.SubmitChanges();
                    BMatchSchedule.UpdateBallot(model.HomeMatchTeamId);
                    BMatchSchedule.UpdateBallot(model.AwayMatchTeamId);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 录入战报
        /// </summary>
        /// <param name="Id"></param>
        public static void UpdateScheduleStandings(tbl_MatchSchedule model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.esp_Standings(model.Id, model.PublishState, model.HomeScore, model.HomeGoals, model.HomeFirstGoals, model.HomeSecondGoals, model.HomeOvertimePenaltys, model.HomePenaltys, model.HomeFouls, model.HomeReds, model.HomeYellows, model.AwayScore, model.AwayGoals, model.AwayFirstGoals, model.AwaySecondGoals, model.AwayOvertimePenaltys, model.AwayPenaltys, model.AwayFouls, model.AwayReds, model.AwayYellows, model.ResultOperatorId, model.ResultContactName, model.ResultTime);
                rdc.esp_MatchTeam_Standings(model.HomeMatchTeamId);
                rdc.esp_MatchTeam_Standings(model.AwayMatchTeamId);
            }
        }
    }
}
