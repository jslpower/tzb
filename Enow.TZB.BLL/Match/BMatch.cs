using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    public class BMatch
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_Match model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_Match.InsertOnSubmit(model);
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
        /// 修改赛事信息
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_Match model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Match.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.MatchAreaType = model.MatchAreaType;
                    m.TypeId = model.TypeId;
                    m.CountryId = model.CountryId;
                    m.CountryName = model.CountryName;
                    m.ProvinceId = model.ProvinceId;
                    m.ProvinceName = model.ProvinceName;
                    m.CityId = model.CityId;
                    m.CityName = model.CityName;
                    m.AreaId = model.AreaId;
                    m.AreaName = model.AreaName;
                    m.MatchName = model.MatchName;
                    m.MatchPhoto = model.MatchPhoto;
                    m.IsCityLimit = model.IsCityLimit;
                    m.SignBeginDate = model.SignBeginDate;
                    m.SignEndDate = model.SignEndDate;
                    m.BeginDate = model.BeginDate;
                    m.EndDate = model.EndDate;
                    m.RegistrationFee = model.RegistrationFee;
                    m.EarnestMoney = model.EarnestMoney;
                    m.MasterOrganizer = model.MasterOrganizer;
                    m.CoOrganizers = model.CoOrganizers;
                    m.Organizer = model.Organizer;
                    m.Sponsors = model.Sponsors;
                    m.TeamNumber = model.TeamNumber;
                    m.PlayersMin = model.PlayersMin;
                    m.PlayersMax = model.PlayersMax;
                    m.BayMin = model.BayMin;
                    m.BayMax = model.BayMax;
                    m.TotalTime = model.TotalTime;
                    m.BreakTime = model.BreakTime;
                    m.MinAge = model.MinAge;
                    m.MaxAge = model.MaxAge;
                    m.Remark = model.Remark;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 删除赛事信息
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(string id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Match.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    m.IsDelete = true;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 获取赛事列表(无分页)
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Match> GetwfyList()
        {
            using (FWDC rdc = new FWDC())
            {
                var m=(from tmp in rdc.tbl_Match where tmp.IsDelete==false select tmp).ToList();
                return m;
            }
        }
        /// <summary>
        /// 赛事列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Match> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MMatch searchModel)
        {
            string FieldsList = " * ";
            string TableName = "tbl_Match";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(IsDelete=0)";
            if (!string.IsNullOrEmpty(searchModel.MatchName))
            {
                strWhere = strWhere + " AND (MatchName like '%" + searchModel.MatchName + "%')";
            }

            if (searchModel.BeginDate.HasValue)
            {
                strWhere += " and (BeginDate>='" + searchModel.BeginDate + "')";
            }
            if (searchModel.EndDate.HasValue)
            {
                strWhere += " and (EndDate<='" + searchModel.EndDate + "')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_Match> q = rdc.ExecuteQuery<tbl_Match>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 赛事列表(根据城市查询)
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Match> GetCityMatchList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MMatch searchModel)
        {
            string FieldsList = " * ";
            string TableName = "tbl_Match";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(IsDelete=0)";
            if (!string.IsNullOrEmpty(searchModel.MatchName))
            {
                strWhere = strWhere + " AND (MatchName like '%" + searchModel.MatchName + "%')";
            }

            if (searchModel.BeginDate.HasValue)
            {
                strWhere += " and (BeginDate>='" + searchModel.BeginDate + "')";
            }
            if (searchModel.EndDate.HasValue)
            {
                strWhere += " and (EndDate<='" + searchModel.EndDate + "')";
            }
            if (searchModel.cityid!=0)
            {
                strWhere += " and (CityId=" + searchModel.cityid + ")";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_Match> q = rdc.ExecuteQuery<tbl_Match>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 取得赛事信息实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_Match GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Match.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }

        /// <summary>
        /// 获取赛事信息列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Match> GetMatchList()
        {
            using (FWDC rdc=new FWDC())
            {
                var query = from c in rdc.tbl_Match select c;
                return query.ToList();
            }
        }

        /// <summary>
        /// 更新已参赛球队数
        /// </summary>
        /// <param name="matchId">赛事表编号</param>
        /// <returns></returns>
        public static bool UpdateSignNumber(string matchId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Match.FirstOrDefault(n => n.Id == matchId);
                if (m != null)
                {
                    m.SignUpNumber = m.SignUpNumber + 1;
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
        /// 减少已参赛球队数
        /// </summary>
        /// <param name="matchId">赛事表编号</param>
        /// <returns></returns>
        public static bool SubtractSignNumber(string matchId)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Match.FirstOrDefault(n => n.Id == matchId);
                if (m != null)
                {
                    m.SignUpNumber = m.SignUpNumber - 1;
                    rdc.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }    
    }
}
