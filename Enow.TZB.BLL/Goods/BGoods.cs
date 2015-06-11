using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
    public class BGoods
    {

        /// <summary>
        /// 商品分页列表
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="intPageSize"></param>
        /// <param name="CurrencyPage"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static List<dt_GoodsView> GetViewList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MGoodsSearch searchModel)
        {
            string FieldsList = " *";
            string TableName = "dt_GoodsView";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(IsDeleted=0)";
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
            if (!string.IsNullOrWhiteSpace(searchModel.GoodsName))
            {
                strWhere += " and GoodsName like '%" + searchModel.GoodsName + "%'";
            }
            if (searchModel.Status.HasValue)
            {
                strWhere += " and Status=" + (int)searchModel.Status + "";
            }
            if (!string.IsNullOrWhiteSpace(searchModel.BallFieldId))
            {
                strWhere += " and BallFieldId='" + searchModel.BallFieldId + "'";
            }
            if (searchModel.TypeId.HasValue)
            {
                strWhere += " and TypeId=" + searchModel.TypeId + "";
            }
            if (searchModel.MaxThenType.HasValue)
            {
                strWhere += " and TypeId >" + searchModel.MaxThenType + "";
            }

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_GoodsView> q = rdc.ExecuteQuery<dt_GoodsView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 获取商品实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_Goods GetModel(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Goods.FirstOrDefault(n => n.ID == Id);
                return model;
            }
        }

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="model"></param>
        public static bool Add(tbl_Goods model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_Goods.InsertOnSubmit(model);
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
        /// 修改商品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_Goods model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Goods.FirstOrDefault(n => n.ID == model.ID);
                if (m != null)
                {
                    m.TypeId = model.TypeId;
                    m.GoodsName = model.GoodsName;
                    m.Unit = model.Unit;
                    m.Status = model.Status;
                    m.BallFieldId = model.BallFieldId;
                    m.Producer = model.Producer;
                    m.Price = model.Price;
                    m.CurrencyPrice = model.CurrencyPrice;
                    m.Stock = model.Stock;
                    m.GoodsIntroduce = model.GoodsIntroduce;
                    m.OperatorId = model.OperatorId;
                    m.OperatorName = model.OperatorName;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 更新库存数量
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Operation"></param>
        /// <param name="IntegrationNumber"></param>
        /// <returns></returns>
        public static bool UpdateStock(int Id, Model.库存操作符号 Operation, int number)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Goods.FirstOrDefault(n => n.ID == Id);
                if (m != null)
                {
                    switch (Operation)
                    {
                        case Model.库存操作符号.加:
                            m.Stock = m.Stock + number;
                            rdc.SubmitChanges();
                            break;
                        case Model.库存操作符号.减:
                            m.Stock = m.Stock - number;
                            rdc.SubmitChanges();
                            break;
                    }
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        ///  获取充值卡实体
        /// </summary>
        /// <param name="FieldId">球场ID</param>
        /// <returns></returns>
        public static tbl_Goods GetChongzhiCardModel(string FieldId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Goods.FirstOrDefault(n => n.TypeId == 1 && n.BallFieldId == FieldId);
                return model;
            }
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Goods.FirstOrDefault(n => n.ID == id);
                if (m != null)
                {
                    m.IsDeleted = true;
                    //rdc.tbl_Goods.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }


    }


    public class BGoodsType
    {



        /// <summary>
        /// 获取商品类别列表，不显示充值卡类别
        /// </summary>
        /// <returns></returns>
        public static List<tbl_GoodsType> GetTypeList()
        {
            using (FWDC rdc = new FWDC())
            {
                var query = from T in rdc.tbl_GoodsType
                            where T.ID > 1
                            select T;
                return query.Distinct().ToList();
            }
        }
        /// <summary>
        /// 商品类别分页列表
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="intPageSize"></param>
        /// <param name="CurrencyPage"></param>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public static List<tbl_GoodsType> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, string TypeName)
        {
            string FieldsList = " ID,TypeName ";
            string TableName = "tbl_GoodsType";
            string OrderString = " ORDER BY ID Desc";
            string strWhere = "(1=1 and id>1)";

            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_GoodsType> q = rdc.ExecuteQuery<tbl_GoodsType>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 获取商品类别实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_GoodsType GetModel(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_GoodsType.FirstOrDefault(n => n.ID == Id);
                return model;
            }
        }

        /// <summary>
        /// 新增商品类别
        /// </summary>
        /// <param name="model"></param>
        public static void Add(tbl_GoodsType model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_GoodsType.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }
        /// <summary>
        /// 修改商品类别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(tbl_GoodsType model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_GoodsType.FirstOrDefault(n => n.ID == model.ID);
                if (m != null)
                {
                    m.TypeName = model.TypeName;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 删除商品类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_GoodsType.FirstOrDefault(n => n.ID == id);
                if (m != null)
                {
                    rdc.tbl_GoodsType.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

    }
}
