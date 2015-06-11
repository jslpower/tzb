using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BActivity
    {
        /// <summary>
        /// 添加活动
        /// </summary>
        /// <param name="model"></param>
        public static bool Add(tbl_Activity model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_Activity.InsertOnSubmit(model);
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
        /// 删除活动
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(List<string> listid)
        {
            using (FWDC rdc = new FWDC())
            {
                var liid = listid;
                var m = (from tmp in rdc.tbl_Activity where liid.Contains(tmp.Id.ToString()) select tmp).ToList();
                if (m.Count > 0)
                {
                    for (int i = 0; i < m.Count; i++)
                    {
                        m[i].IsDelete = 1;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 修改活动
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_Activity model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Activity.FirstOrDefault(n => n.Id == model.Id && n.UpUserId==model.UpUserId);
                if (m != null)
                {
                    m.Activitytypes = model.Activitytypes;//分类
                    m.Starname = model.Starname;//高亮字段
                    m.Title = model.Title;//标题
                    m.Address = model.Address;//活动地点
                    m.CostNum = model.CostNum;//费用
                    m.ActivityTxt = model.ActivityTxt;//活动内容
                    m.Condition = model.Condition;//注意事项
                    m.Innumber = model.Innumber;//参加人数
                    m.Rsdatetime = model.Rsdatetime;//报名开始时间
                    m.Redatetime = model.Redatetime;//报名结束时间
                    m.StartDatetime = model.StartDatetime;//开始时间
                    m.EndDatetime = model.EndDatetime;//结束时间
                    m.UpUserId = model.UpUserId;//修改人ID
                    m.UpUserName = model.UpUserName;//修改人名称
                    m.UpDatetime = model.UpDatetime;//修改时间
                    m.CountryId = model.CountryId;//国家ID
                    m.Countryname = model.Countryname;//国家名称
                    m.ProvinceId = model.ProvinceId;//省ID
                    m.Provincename = model.Provincename;//省名称
                    m.CityId = model.CityId;//市ID
                    m.Cityname = model.Cityname;//市名称
                    m.AreaId = model.AreaId;//区ID
                    m.Areaname = model.Areaname;//区名称
                    m.Cityyuzhan = model.Cityyuzhan;//是否同城
                    m.Release = model.Release;//发布至其他
                    m.SiteID = model.SiteID;//场地编号
                    m.SiteName = model.SiteName;//场地名称
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
        public static tbl_Activity GetModel(long Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Activity.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static List<tbl_Activity> GettypeList()
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from tmp in rdc.tbl_Activity where tmp.IsDelete == 0 select tmp).ToList();
                return model;
            }
        }
        /// <summary>
        /// 根据查询条件获取分页列表信息
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Activity> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, MActivity SearchModel)
        {
            string FieldsList = "*";
            string TableName = " tbl_Activity ";
            string OrderString = " ORDER BY Id desc";
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("(1=1)");
            strWhere.Append(" AND (IsDelete=0)");
            #region 查询条件
            if (SearchModel.fbmb != -1)
                strWhere.Append(" AND ( Release=" + SearchModel.fbmb + ")");
            if (SearchModel.types != -1)
                strWhere.Append(" AND ( Activitytypes=" + SearchModel.types + ")");
            if (!string.IsNullOrEmpty(SearchModel.title))
            {
                strWhere.Append(" AND ( Title like '%" + SearchModel.title + "%' or  Starname like '%" + SearchModel.title + "%')");
            }
            if(SearchModel.Sttime!=null)
                strWhere.Append(" AND ( StartDatetime >= '" + SearchModel.Sttime + "')");
            if (SearchModel.Edtime != null)
                strWhere.Append(" AND ( EndDatetime <= '" + SearchModel.Edtime + "')");
            if(SearchModel.CityId>0)
                strWhere.Append(" AND ( CityId = " + SearchModel.CityId + ")");
            #endregion
            
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere.ToString());
                rowsCount = query.First<int>();
                List<tbl_Activity> q = rdc.ExecuteQuery<tbl_Activity>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere.ToString() + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
    }
}
