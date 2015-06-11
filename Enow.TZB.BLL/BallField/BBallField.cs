using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 球场管理
    /// </summary>
    public class BBallField
    {
        #region 场所类别
        /// <summary>
        /// 返回场所类别
        /// </summary>
        /// <returns></returns>
        public static List<tbl_FieldType> GetFieldTypeList()
        {
            using (FWDC rdc = new FWDC())
            {
                return rdc.tbl_FieldType.ToList();
            }
        }
        #endregion
        #region 球场
        /// <summary>
        /// 取得球场列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.MBallFieldLatitude> GetList()
        {
            using (FWDC rdc = new FWDC())
            {
                return GetCourtList(1);
            }
        }
        public static List<Model.MBallFieldLatitude> GetCourtList(int FieldTypeId)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.tbl_BallField
                            where q.FieldType == FieldTypeId&&q.State == (int)Model.EnumType.球场审核状态.已审核 && q.IsDeleted == false
                            orderby q.CityId ascending
                            select new Model.MBallFieldLatitude { Id = q.Id, FieldName = q.FieldName, Longitude = q.Longitude, Latitude = q.Latitude };
                return query.ToList();
            }
        }
        /// <summary>
        /// 根据城市获取球场列表
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public static List<Model.MBallFieldLatitude> GetList(int CityId)
        {
            using (FWDC rdc = new FWDC())
            {
                return GetList(CityId,1);
            }
        }
        /// <summary>
        /// 根据城市获取场地列表
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public static List<Model.MBallFieldLatitude> GetList(int CityId, int FieldTypeId)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from q in rdc.tbl_BallField
                            where q.FieldType == FieldTypeId && q.CityId == CityId && q.State == (int)Model.EnumType.球场审核状态.已审核 && q.IsDeleted == false
                            orderby q.CityId ascending
                            select new Model.MBallFieldLatitude { Id = q.Id, FieldName = q.FieldName, Longitude = q.Longitude, Latitude = q.Latitude };
                return query.ToList();
            }
        }
        /// <summary>
        /// 获取球场信息列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_BallField> GetFieldList()
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from p in rdc.tbl_BallField
                            where p.State == (int)Model.EnumType.球场审核状态.已审核 && p.IsDeleted==false
                            select p;
                return query.ToList();
            }
        }
        /// <summary>
        /// 获取球场列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_BallField> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MBallFieldSearch searchModel)
        {
            string FieldsList = "*";
            string TableName = "tbl_BallField";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(FieldType=1) AND (IsDeleted=0)";
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
            if (searchModel.Countyd > 0)
            {
                strWhere = strWhere + " AND (CountyId = " + searchModel.Countyd + ")";
            }
            if (searchModel.KeyWord != null)
            {
                strWhere = strWhere + " AND (FieldName like '%" + searchModel.KeyWord + "%')";
            }
            #endregion
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_BallField> q = rdc.ExecuteQuery<tbl_BallField>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 获取场地列表
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="intPageSize"></param>
        /// <param name="CurrencyPage"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static List<dt_BallField> GetCourList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MBallFieldSearch searchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_BallField";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(IsDeleted=0)";
            #region 查询条件
            if (searchModel.FieldType > 0)
            {
                strWhere = " AND (FieldTypeId = " + searchModel.FieldType + ")";
            }
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
            if (searchModel.Countyd > 0)
            {
                strWhere = strWhere + " AND (CountyId = " + searchModel.Countyd + ")";
            }
            #region 城市权限控制
            if (searchModel.IsAllCity == false)
            {
                if (!String.IsNullOrWhiteSpace(searchModel.CityLimitList))
                {
                    strWhere += " AND (CountyId IN (" + searchModel.CityLimitList + "))";
                }
                else { strWhere += " AND (CountyId = 0)"; }
            }
            #endregion
            if (searchModel.KeyWord != null)
            {
                strWhere = strWhere + " AND (FieldName like '%" + searchModel.KeyWord + "%')";
            }
            #endregion
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_BallField> q = rdc.ExecuteQuery<dt_BallField>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        #endregion

        /// <summary>
        /// 取得实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_BallField GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_BallField.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 取得实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static dynamic GetDynmaicModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var query = (from q in rdc.tbl_BallField
                             where q.Id == Id
                             select new { q.Id, q.FieldName, q.FieldPhoto, q.Price, q.FieldNumber, q.Hours, q.FieldSize, q.ContactTel, q.Address }).FirstOrDefault();
                return query;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(tbl_BallField model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_BallField.InsertOnSubmit(model);
                    rdc.SubmitChanges();
                    //写入球场充值卡产品
                    BGoods.Add(new tbl_Goods {
                        TypeId = 1,//1固化为充值卡类型
                        GoodsName = "会员充值",
                        Unit = "元",
                        Status = (int)Model.商品上架状态.上架,
                        BallFieldId = model.Id,
                        Producer = "铁子帮",
                        Price = 1,
                        CurrencyPrice = 1,
                        Stock = 1000,
                        OperatorId = model.UserId,
                        OperatorName = model.ContactName,
                        GoodsIntroduce = "会员充值产品",
                        IssueTime = DateTime.Now
                    });
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_BallField model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_BallField.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.FieldType = model.FieldType;
                    m.CountryId = model.CountryId;
                    m.CountryName = model.CountryName;
                    m.ProvinceId = model.ProvinceId;
                    m.ProvinceName = model.ProvinceName;
                    m.CityId = model.CityId;
                    m.CityName = model.CityName;
                    m.CountyId = model.CountyId;
                    m.CountyName = model.CountyName;
                    m.FieldName = model.FieldName;
                    m.ShortName = model.ShortName;
                    m.FieldPhoto = model.FieldPhoto;
                    m.FieldNumber = model.FieldNumber;
                    m.Address = model.Address;
                    m.Longitude = model.Longitude;
                    m.Latitude = model.Latitude;
                    m.ContactTel = model.ContactTel;
                    m.MarketPrice = model.MarketPrice;
                    m.Price = model.Price;
                    m.Hours = model.Hours;
                    m.FieldSize = model.FieldSize;
                    m.Remark = model.Remark;
                    m.OtherPhotoXml = model.OtherPhotoXml;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 删除球场信息
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(string id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_BallField.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    m.IsDeleted = true;
                    //rdc.tbl_BallField.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        #region 场所统计信息
        /// <summary>
        /// 获取场地统计
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="intPageSize"></param>
        /// <param name="CurrencyPage"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static List<Model.MFiledOrderSettle> GetCourStatistics(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MCourStatisticsSearch searchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_BallField";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(IsDeleted=0)";
            #region 查询条件
            if (searchModel.FieldTypeId > 0)
            {
                strWhere = strWhere + " AND (FieldTypeId = " + searchModel.FieldTypeId + ")";
            }
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
            if (searchModel.AreaId > 0)
            {
                strWhere = strWhere + " AND (CountyId = " + searchModel.AreaId + ")";
            }
            #region 城市权限控制
            if (searchModel.IsAllCity == false)
            {
                if (!String.IsNullOrWhiteSpace(searchModel.CityLimitList))
                {
                    strWhere += " AND (CountyId IN (" + searchModel.CityLimitList + "))";
                }
                else { strWhere += " AND (CountyId = 0)"; }
            }
            #endregion
            #endregion
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_BallField> q = rdc.ExecuteQuery<dt_BallField>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                List<Model.MFiledOrderSettle> list = new List<Model.MFiledOrderSettle>();
                string StartDate = searchModel.Year.ToString() + "-" + searchModel.Month.ToString() + "-1 00:00:00";
                string EndDate = DateTime.Parse(searchModel.Year.ToString() + "-" + searchModel.Month.ToString() + "-1 00:00:00").AddMonths(1).ToString("yyyy-MM-dd HH:mm:ss");                
                foreach (var model in q)
                {
                    Model.MFiledOrderSettle item = new Model.MFiledOrderSettle();
                    item.Id = model.Id;
                    item.FieldTypeId = model.FieldTypeId;
                    item.FieldTypeName = model.FieldTypeName;
                    item.CountryId = model.CountryId;
                    item.CountryName = model.CountryName;
                    item.ProvinceId = model.ProvinceId;
                    item.ProvinceName = model.ProvinceName;
                    item.CityId = model.CityId;
                    item.CityName = model.CityName;
                    item.CountyId = model.CountyId;
                    item.CountyName = model.CountyName;
                    item.FieldName = model.FieldName;
                    item.SettleDate = searchModel.Year.ToString() + "-" + searchModel.Month.ToString();
                    decimal Count = 0;
                    //TypeId=1: 会员充值卡                    
                    //现金收款 
                    var countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + model.Id + "') AND TypeId > 1 and paytype = " + (int)Model.收款类型.现金 + " and State=" + (int)Model.订单状态.已付款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                    Count = countQuery.First<decimal>();
                    item.CashReceipts = Count;
                    //现金退款
                    countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + model.Id + "') AND TypeId > 1 and paytype = " + (int)Model.收款类型.现金 + " and State=" + (int)Model.订单状态.已退款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                    Count = countQuery.First<decimal>();
                    item.CashRefund = Count;
                    // 刷卡收款
                    countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + model.Id + "') AND TypeId > 1 and (paytype IN (" + (int)Model.收款类型.借记卡 + "," + (int)Model.收款类型.信用卡 + "," + (int)Model.收款类型.工商银行卡 + ")) and State=" + (int)Model.订单状态.已付款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                    Count = countQuery.First<decimal>();
                    item.CardReceipts = Count;
                    // 刷卡退款
                    countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + model.Id + "') AND TypeId > 1 and (paytype IN (" + (int)Model.收款类型.借记卡 + "," + (int)Model.收款类型.信用卡 + "," + (int)Model.收款类型.工商银行卡 + ")) and State=" + (int)Model.订单状态.已退款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                    Count = countQuery.First<decimal>();
                    item.CardRefund = Count;
                    //现金充值
                    countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + model.Id + "') AND TypeId = 1 and paytype = " + (int)Model.收款类型.现金 + " and State=" + (int)Model.订单状态.已付款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                    Count = countQuery.First<decimal>();
                    item.CashRecharge = Count;
                    //刷卡充值
                    countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + model.Id + "') AND TypeId = 1 and (paytype IN (" + (int)Model.收款类型.借记卡 + "," + (int)Model.收款类型.信用卡 + "," + (int)Model.收款类型.工商银行卡 + ")) and State=" + (int)Model.订单状态.已付款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                    Count = countQuery.First<decimal>();
                    item.CardRecharge = Count;
                    //铁丝币收款
                    countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + model.Id + "') AND TypeId > 1 and paytype = " + (int)Model.收款类型.铁丝卡 + " and State=" + (int)Model.订单状态.已付款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                    Count = countQuery.First<decimal>();
                    item.TieSiCardReceipts = Count;
                    //铁丝币退款
                    countQuery = rdc.ExecuteQuery<decimal>(@"select IsNull(sum(JinE),0) from dt_OrderView WHERE (FieldId='" + model.Id + "') AND TypeId > 1 and paytype = " + (int)Model.收款类型.铁丝卡 + " and State=" + (int)Model.订单状态.已退款 + " and issuetime >'" + StartDate + "' and issuetime < '" + EndDate + "'");
                    Count = countQuery.First<decimal>();
                    item.TieSiCardRefund = Count;
                    item.SettleMoney = item.CardReceipts + item.TieSiCardReceipts - item.CardRefund - item.TieSiCardRefund - item.CashRecharge;
                    //取得当月结算情况
                    var SettleModel = BOrderSettle.GetModel(model.Id, searchModel.Year, searchModel.Month);
                    if (SettleModel != null)
                    {
                        item.IsSettle = true;
                        item.SettleOperatorId = SettleModel.SettleOperatorId;
                        item.SettleName = SettleModel.SettleName;
                        item.SettleTime = SettleModel.SettleTime;
                        SettleModel = null;
                    }
                    else {
                        item.IsSettle = false;
                    }
                    list.Add(item);
                }
                return list;
            }
        }
        #endregion
    }
}
