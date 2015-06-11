using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
    public class BMSysProvince
    {
        #region 微信注册
        /// <summary>
        /// 获取国家信息集合
        /// </summary>
        /// <returns></returns>
        public static IList<tbl_SysCountry> GetWxCountryList()
        {
            var list = CacheSysBaseCity.GetCountryList();
            var query = from q in list
                        where q.Name != "全球"
                        orderby q.SequenceId descending
                        select q;
            return query.ToList();
        }

        /// <summary>
        /// 获取省份信息列表
        /// </summary>
        /// <returns></returns>
        public static IList<tbl_SysProvince> GetWxProvinceList(MProvinceSearch SearchModel)
        {
            var list = CacheSysBaseCity.GetProvinceList();
            if (SearchModel.CountryId.HasValue)
            {
                var query = from q in list
                            where q.CountryId == SearchModel.CountryId && q.Name != "全国"
                            select q;
                return query.ToList();
            }
            else
            {
                var query = from q in list
                            where q.Name != "全国"
                            select q;
                return query.ToList();
            }
        }

        /// <summary>
        /// 根据省份ID获取城市信息集合
        /// </summary>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static IList<tbl_SysCity> GetWxCityList(MCitySearch SearchModel)
        {
            var list = CacheSysBaseCity.GetCityList();
            if (SearchModel.ProvinceId.HasValue)
            {
                var query = from q in list
                            where q.ProvinceId == SearchModel.ProvinceId && q.Name != "全省"
                            select q;
                return query.ToList();
            }
            else
            {
                var query = from q in list
                            where q.Name != "全省"
                            select q;
                return query.ToList();
            }
        }
        /// <summary>
        /// 获取区县信息集合
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static IList<tbl_SysArea> GetWxAreaList(MAreaSearch searchModel)
        {
            var list = CacheSysBaseCity.GetCountyAreaList();
            if (searchModel.CityId.HasValue)
            {
                var query = from q in list
                            where q.CityId == searchModel.CityId && q.Name != "全市"
                            select q;
                return query.ToList();
            }
            else
            {
                var query = from q in list
                            where q.Name != "全市"
                            select q;
                return query.ToList();
            }
        }
        #endregion
        /// <summary>
        /// 获取国家信息集合
        /// </summary>
        /// <returns></returns>
        public static IList<tbl_SysCountry> GetCountryList()
        {
            return CacheSysBaseCity.GetCountryList();
        }

        /// <summary>
        /// 获取省份信息列表
        /// </summary>
        /// <returns></returns>
        public static IList<tbl_SysProvince> GetProvinceList(MProvinceSearch SearchModel)
        {
            var list = CacheSysBaseCity.GetProvinceList();
            if (SearchModel.CountryId.HasValue)
            {
                var query = from q in list
                            where q.CountryId == SearchModel.CountryId
                            select q;
                return query.ToList();
            }
            else
            {
                return list;
            }
        }

        /// <summary>
        /// 根据省份ID获取城市信息集合
        /// </summary>
        /// <param name="SearchModel"></param>
        /// <returns></returns>
        public static IList<tbl_SysCity> GetCityList(MCitySearch SearchModel)
        {
            var list = CacheSysBaseCity.GetCityList();
            if (SearchModel.ProvinceId.HasValue)
            {
                var query = from q in list
                            where q.ProvinceId == SearchModel.ProvinceId
                            select q;
                return query.ToList();
            }
            else
            {
                return list;
            }
        }

        /// <summary>
        /// 获取区县信息集合
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static IList<tbl_SysArea> GetAreaList(MAreaSearch searchModel)
        {
            var list = CacheSysBaseCity.GetCountyAreaList();
            if (searchModel.CityId.HasValue)
            {
                var query = from q in list
                            where q.CityId == searchModel.CityId
                            select q;
                return query.ToList();
            }
            else
            {
                return list;
            }
        }

        /// <summary>
        /// 获取国家实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static tbl_SysCountry GetCountryModel(int id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SysCountry.FirstOrDefault(n => n.CountryId == id);
                return model;
            }
        }

        /// <summary>
        /// 获取省份的实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static tbl_SysProvince GetProvinceModel(int id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SysProvince.FirstOrDefault(n => n.ProvinceId == id);
                return model;
            }
        }
        /// <summary>
        /// 取得城市实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_SysCity GetCityModel(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SysCity.FirstOrDefault(n => n.CityId == Id);
                return model;
            }
        }

        /// <summary>
        /// 获取区县实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static tbl_SysArea GetAreaModel(int id)
        {
            using (FWDC rdc = new FWDC())
            {

                var model = rdc.tbl_SysArea.FirstOrDefault(n => n.CountyId == id);
                return model;
            }
        }
        /// <summary>
        /// 取得县区数据
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        public static dt_SysArea GetAreaViewModel(int CountyId)
        {
            using (FWDC rdc = new FWDC())
            {

                var model = rdc.dt_SysArea.FirstOrDefault(n => n.CountyId == CountyId);
                return model;
            }
        }
        /// <summary>
        /// 添加国家
        /// </summary>
        /// <param name="model"></param>
        public static void AddCountry(tbl_SysCountry model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_SysCountry.InsertOnSubmit(model);
                rdc.SubmitChanges();
                CacheSysBaseCity.CountryClear();
            }
        }

        /// <summary>
        /// 添加省份
        /// </summary>
        /// <param name="model"></param>
        public static void AddProvince(tbl_SysProvince model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_SysProvince.InsertOnSubmit(model);
                rdc.SubmitChanges();
                CacheSysBaseCity.ProvinceClear();
            }
        }
        /// <summary>
        /// 添加城市
        /// </summary>
        /// <param name="model"></param>
        public static void AddCity(tbl_SysCity model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_SysCity.InsertOnSubmit(model);
                rdc.SubmitChanges();
                CacheSysBaseCity.CityClear();
            }
        }
        /// <summary>
        /// 添加区县
        /// </summary>
        /// <param name="model"></param>
        public static void AddArea(tbl_SysArea model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_SysArea.InsertOnSubmit(model);
                rdc.SubmitChanges();
                CacheSysBaseCity.CountyAreaClear();
            }
        }

        /// <summary>
        /// 修改国家
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdateCountry(tbl_SysCountry model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SysCountry.FirstOrDefault(n => n.CountryId == model.CountryId);
                if (m != null)
                {

                    m.CountryId = model.CountryId;
                    m.CompanyId = model.CompanyId;
                    m.Name = model.Name;
                    m.JP = model.JP;
                    m.QP = model.QP;
                    m.EnName = model.EnName;
                    m.IsDefault = model.IsDefault;
                    rdc.SubmitChanges();
                    CacheSysBaseCity.CountryClear();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 修改省份
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdateProvince(tbl_SysProvince model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SysProvince.FirstOrDefault(n => n.ProvinceId == model.ProvinceId);
                if (m != null)
                {

                    m.CountryId = model.CountryId;

                    m.Name = model.Name;
                    m.JP = model.JP;
                    m.QP = model.QP;
                    m.EnName = model.EnName;
                    rdc.SubmitChanges();
                    CacheSysBaseCity.ProvinceClear();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 修改城市
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdateCity(tbl_SysCity model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SysCity.FirstOrDefault(n => n.CityId == model.CityId);
                if (m != null)
                {
                    m.ProvinceId = model.ProvinceId;
                    m.Name = model.Name;
                    m.JP = model.JP;
                    m.QP = model.QP;
                    m.EnName = model.EnName;
                    rdc.SubmitChanges();
                    CacheSysBaseCity.CityClear();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 修改区县
        /// </summary>
        /// <param name="model"></param>
        public static bool UpdateArea(tbl_SysArea model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SysArea.FirstOrDefault(n => n.CountyId == model.CountyId);
                if (m != null)
                {
                    m.CityId = model.CityId;
                    m.Name = model.Name;
                    m.JP = model.JP;
                    m.QP = model.QP;
                    m.EnName = model.EnName;
                    rdc.SubmitChanges();
                    CacheSysBaseCity.CountyAreaClear();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 删除国家
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool DeleteCountry(int id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SysCountry.FirstOrDefault(n => n.CountryId == id);
                if (m != null)
                {
                    rdc.tbl_SysCountry.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    CacheSysBaseCity.CountryClear();
                    return true;
                }
                else { return false; }
            }
        }


        /// <summary>
        /// 批量删除省份
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteProvince(int id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SysProvince.FirstOrDefault(n => n.ProvinceId == id);
                if (m != null)
                {
                    rdc.tbl_SysProvince.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    CacheSysBaseCity.CountryClear();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 批量删除城市
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteCity(int id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SysCity.FirstOrDefault(n => n.CityId == id);
                if (m != null)
                {
                    rdc.tbl_SysCity.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    CacheSysBaseCity.CountryClear();
                    return true;
                }
                else { return false; }
            }
        }


        /// <summary>
        /// 批量删除区县
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteArea(int id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_SysArea.FirstOrDefault(n => n.CountyId == id);
                if (m != null)
                {
                    rdc.tbl_SysArea.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    CacheSysBaseCity.CountryClear();
                    return true;
                }
                else { return false; }
            }

        }


        /// <summary>
        /// 国家列表分页
        /// </summary>
        /// <returns></returns>
        public static IList<tbl_SysCountry> CountryListByPage(ref int rowsCount, int intPageSize, int CurrencyPage, MCountrySearch SearchModel)
        {
            string FieldsList = " CountryId,CompanyId,Name,JP,QP,EnName,IsDefault";
            string TableName = " tbl_SysCountry ";
            string OrderString = " ORDER BY CountryId ASC";
            string strWhere = "(1=1)";
            if (!string.IsNullOrEmpty(SearchModel.Name))
            {
                strWhere = strWhere + " AND (Name  like '%" + SearchModel.Name + "%')";
            }
            if (!string.IsNullOrEmpty(SearchModel.EnName))
            {
                strWhere = strWhere + " AND (EnName  like '%" + SearchModel.EnName + "%')";
            }
            if (!string.IsNullOrEmpty(SearchModel.JP))
            {
                strWhere = strWhere + " AND ( JP  like '%" + SearchModel.JP + "%')";
            }
            if (!string.IsNullOrEmpty(SearchModel.QP))
            {
                strWhere = strWhere + " AND (QP  like '%" + SearchModel.QP + "%')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_SysCountry> q = rdc.ExecuteQuery<tbl_SysCountry>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 省份列表分页
        /// </summary>
        /// <returns></returns>
        public static IList<tbl_SysProvince> ProvinceListByPage(ref int rowsCount, int intPageSize, int CurrencyPage, MProvinceSearch SearchModel)
        {
            string FieldsList = " ProvinceId,CountryId,Name,JP,QP,EnName";
            string TableName = " tbl_SysProvince ";
            string OrderString = " ORDER BY ProvinceId ASC";
            string strWhere = "(1=1)";

            if (SearchModel.CountryId.HasValue)
            {
                strWhere += " and (CountryId=" + SearchModel.CountryId + ")";
            }
            if (!string.IsNullOrEmpty(SearchModel.Name))
            {
                strWhere = strWhere + " AND (Name  like '%" + SearchModel.Name + "%')";
            }
            if (!string.IsNullOrEmpty(SearchModel.EnName))
            {
                strWhere = strWhere + " AND (EnName  like '%" + SearchModel.EnName + "%')";
            }
            if (!string.IsNullOrEmpty(SearchModel.JP))
            {
                strWhere = strWhere + " AND ( JP  like '%" + SearchModel.JP + "%')";
            }
            if (!string.IsNullOrEmpty(SearchModel.QP))
            {
                strWhere = strWhere + " AND (QP  like '%" + SearchModel.QP + "%')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_SysProvince> q = rdc.ExecuteQuery<tbl_SysProvince>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 城市列表分页
        /// </summary>
        /// <returns></returns>
        public static IList<tbl_SysCity> CityListByPage(ref int rowsCount, int intPageSize, int CurrencyPage, MCitySearch SearchModel)
        {
            string FieldsList = " CityId,ProvinceId,Name,JP,QP,EnName";
            string TableName = " tbl_SysCity ";
            string OrderString = " ORDER BY ProvinceId ASC";
            string strWhere = "(1=1)";

            if (SearchModel.ProvinceId.HasValue)
            {
                strWhere += " and (ProvinceId=" + SearchModel.ProvinceId + ")";
            }
            if (!string.IsNullOrEmpty(SearchModel.Name))
            {
                strWhere = strWhere + " AND (Name  like '%" + SearchModel.Name + "%')";
            }
            if (!string.IsNullOrEmpty(SearchModel.EnName))
            {
                strWhere = strWhere + " AND (EnName  like '%" + SearchModel.EnName + "%')";
            }
            if (!string.IsNullOrEmpty(SearchModel.JP))
            {
                strWhere = strWhere + " AND ( JP  like '%" + SearchModel.JP + "%')";
            }
            if (!string.IsNullOrEmpty(SearchModel.QP))
            {
                strWhere = strWhere + " AND (QP  like '%" + SearchModel.QP + "%')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_SysCity> q = rdc.ExecuteQuery<tbl_SysCity>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 区县列表分页
        /// </summary>
        /// <returns></returns>
        public static IList<tbl_SysArea> AreaListByPage(ref int rowsCount, int intPageSize, int CurrencyPage, MAreaSearch SearchModel)
        {
            string FieldsList = " CountyId,CityId,Name,JP,QP,EnName,ThName ";
            string TableName = " tbl_SysArea ";
            string OrderString = " ORDER BY CountyId ASC";
            string strWhere = "(1=1)";

            if (SearchModel.CityId.HasValue)
            {
                strWhere += " and (CityId=" + SearchModel.CityId + ")";
            }
            if (!string.IsNullOrEmpty(SearchModel.Name))
            {
                strWhere = strWhere + " AND (Name  like '%" + SearchModel.Name + "%')";
            }
            if (!string.IsNullOrEmpty(SearchModel.EnName))
            {
                strWhere = strWhere + " AND (EnName  like '%" + SearchModel.EnName + "%')";
            }
            if (!string.IsNullOrEmpty(SearchModel.JP))
            {
                strWhere = strWhere + " AND ( JP  like '%" + SearchModel.JP + "%')";
            }
            if (!string.IsNullOrEmpty(SearchModel.QP))
            {
                strWhere = strWhere + " AND (QP  like '%" + SearchModel.QP + "%')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_SysArea> q = rdc.ExecuteQuery<tbl_SysArea>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 判断国家名称是否存在
        /// </summary>
        /// <param name="CName"></param>
        /// <returns></returns>
        public static bool IsExistsCountry(string CName)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SysCountry.FirstOrDefault(n => n.Name == CName);
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
        /// 判断相同国家下，省份名称是否有重复
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="PName"></param>
        /// <returns></returns>
        public static bool IsExistsProvince(int CountryId, string PName)
        {

            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SysProvince.FirstOrDefault(n => n.CountryId == CountryId);
                if (model != null)
                {
                    model = rdc.tbl_SysProvince.FirstOrDefault(n => n.Name == PName);
                    if (model != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
        }

        /// <summary>
        /// 相同省份下，判断城市名称是否重复
        /// </summary>
        /// <param name="ProvinceId"></param>
        /// <param name="Cname"></param>
        /// <returns></returns>
        public static bool IsExistsCity(int ProvinceId, string Cname)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SysCity.FirstOrDefault(n => n.ProvinceId == ProvinceId);
                if (model != null)
                {
                    model = rdc.tbl_SysCity.FirstOrDefault(n => n.Name == Cname);
                    if (model != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 相同城市下，判断区县名称是否重复
        /// </summary>
        /// <param name="CityId"></param>
        /// <param name="AName"></param>
        /// <returns></returns>
        public static bool IsExistsArea(int CityId, string AName)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_SysArea.FirstOrDefault(n => n.CityId == CityId);
                if (model != null)
                {
                    model = rdc.tbl_SysArea.FirstOrDefault(n => n.Name == AName);
                    if (model != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }


    }
}
