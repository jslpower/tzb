using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BMallGoods
    {

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<dt_MallGoodsView> GetGoodsList(ref int rowsCount, int intPageSize, int CurrencyPage, MMallGoodsSearch searchModel)
        {
            string FieldsList = "*";
            string TableName = "dt_MallGoodsView";
            string OrderString = " ORDER BY IssueTime desc";
            string strWhere = "(1=1)";
            if (!string.IsNullOrWhiteSpace(searchModel.GoodsName))
            {
                strWhere += " and GoodsName like '%" + searchModel.GoodsName + "%'";
            }
            if (searchModel.Status.HasValue)
            {
                strWhere += " and Status=" + (int)searchModel.Status + "";
            }
            if (searchModel.IsGood.HasValue)
            {
                string IsValid = searchModel.IsGood.Value ? "1" : "0";
                strWhere = strWhere + " AND (IsGood = " + IsValid + ")";
            }
            if (searchModel.IsDelete.HasValue)
            {
                string IsValid = searchModel.IsDelete.Value ? "1" : "0";
                strWhere = strWhere + " AND (IsDelete = " + IsValid + ")";
            }
            if (searchModel.TypeId.HasValue)
            {
                strWhere += " and GoodsClassId=" + searchModel.TypeId + "";
            }
           
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_MallGoodsView> q = rdc.ExecuteQuery<dt_MallGoodsView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 商品分页列表
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="intPageSize"></param>
        /// <param name="CurrencyPage"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static List<dt_MallGoodsView> GetViewList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MMallGoodsSearch searchModel)
        {
            string FieldsList = " *";
            string TableName = "dt_MallGoodsView";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(IsDelete=0)";
           
            if (!string.IsNullOrWhiteSpace(searchModel.GoodsName))
            {
                strWhere += " and GoodsName like '%" + searchModel.GoodsName + "%'";
            }
            if (searchModel.Status.HasValue)
            {
                strWhere += " and Status=" + (int)searchModel.Status + "";
            }
            
            if (searchModel.TypeId.HasValue)
            {
                strWhere += " and GoodsClassId=" + searchModel.TypeId + "";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<dt_MallGoodsView> q = rdc.ExecuteQuery<dt_MallGoodsView>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 商品分页列表
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="intPageSize"></param>
        /// <param name="CurrencyPage"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static List<tbl_MallGoods> GetGoodsmodelList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MMallGoodsSearch searchModel)
        {
            string FieldsList = " *";
            string TableName = "tbl_MallGoods";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(IsDelete=0)";

            if (!string.IsNullOrWhiteSpace(searchModel.GoodsName))
            {
                strWhere += " and GoodsName like '%" + searchModel.GoodsName + "%'";
            }
            if (searchModel.Status.HasValue)
            {
                strWhere += " and Status=" + (int)searchModel.Status + "";
            }
            if (searchModel.TypeId != null)
            {
                strWhere += " and GoodsClassId=" + searchModel.TypeId + "";
            }
            if (searchModel.Roleype!=null)
            {
                strWhere += " and RoleClass=" + searchModel.Roleype + "";
            }


            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_MallGoods> q = rdc.ExecuteQuery<tbl_MallGoods>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 会员发布商品分页列表(义卖商品)
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="intPageSize"></param>
        /// <param name="CurrencyPage"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public static List<tbl_MallGoods> GetMemberViewList(ref int rowsCount, int intPageSize, int CurrencyPage, Model.MMallGoodsSearch searchModel)
        {
            string FieldsList = " *";
            string TableName = "tbl_MallGoods";
            string OrderString = " ORDER BY IssueTime Desc";
            string strWhere = "(IsDelete=0)";

            if (!string.IsNullOrWhiteSpace(searchModel.GoodsName))
            {
                strWhere += " and ( GoodsName like '%" + searchModel.GoodsName + "%')";
            }
            if (searchModel.TypeId.HasValue)
            {
                strWhere += " and ( GoodsClassId=" + searchModel.TypeId + ")";
            }
            if (!string.IsNullOrEmpty(searchModel.MemberId))
            {
                strWhere += " and ( MemberId='" + searchModel.MemberId + "')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_MallGoods> q = rdc.ExecuteQuery<tbl_MallGoods>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(string id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MallGoods.FirstOrDefault(n => n.Id == id);
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
        /// 删除会员商品
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static bool DeleteMemberGod(string id,string memberid)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MallGoods.FirstOrDefault(n => n.Id == id && n.MemberId == memberid);
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
        /// <summary>
        /// 停售商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool StopSellGoods(string id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MallGoods.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    m.Status = 0;

                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// <summary>
        /// 恢复销售商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool EnableSellGoods(string id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MallGoods.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    m.Status = 1;

                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        
        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="model"></param>
        public static bool Add(tbl_MallGoods model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_MallGoods.InsertOnSubmit(model);
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
        public static bool Update(tbl_MallGoods model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MallGoods.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.GoodsClassId = model.GoodsClassId;
                    m.GoodsName = model.GoodsName;
                    m.StockNum=model.StockNum;
                    m.MemberPrice = model.MemberPrice;
                    m.MarketPrice = model.MarketPrice;
                    m.GoodsIntroduce = model.GoodsIntroduce;
                    m.Status = model.Status;
                    m.OperatorId = model.OperatorId;
                    m.OperatorName = model.OperatorName;
                    m.Producer = model.Producer;
                    m.Standard = model.Standard;
                    m.StandardInfo = model.StandardInfo;
                    m.GoodsPhoto=model.GoodsPhoto;
                    m.IsGood=model.IsGood;
                    m.IsFreight=model.IsFreight;
                    m.FreightFee=model.FreightFee;
                    m.IssueTime = DateTime.Now;
                    m.RoleClass = model.RoleClass;
                    m.RoleClassName = model.RoleClassName;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 会员修改义卖商品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateMemberGood(tbl_MallGoods model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MallGoods.FirstOrDefault(n => n.Id == model.Id && n.MemberId == model.MemberId && n.IsDelete==false);
                if (m != null)
                {
                    m.GoodsClassId = model.GoodsClassId;
                    m.GoodsName = model.GoodsName;
                    m.StockNum = model.StockNum;
                    m.MemberPrice = model.MemberPrice;
                    m.MarketPrice = model.MarketPrice;
                    m.GoodsIntroduce = model.GoodsIntroduce;
                    m.Status = model.Status;
                    m.OperatorId = model.OperatorId;
                    m.OperatorName = model.OperatorName;
                    m.Producer = model.Producer;
                    m.Standard = model.Standard;
                    m.StandardInfo = model.StandardInfo;
                    if (!string.IsNullOrEmpty(model.GoodsPhoto))
                    {
                        m.GoodsPhoto = model.GoodsPhoto;
                    }
                    m.IsFreight = model.IsFreight;//是否包含运费
                    m.FreightFee = model.FreightFee;//运费
                    m.IssueTime = DateTime.Now;//添加时间
                    m.RoleClass = model.RoleClass;
                    m.RoleClassName = model.RoleClassName;
                    m.MemberId = model.MemberId;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 修改商品销售数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateSellNum(tbl_MallGoods model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_MallGoods.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {

                    m.StockNum = m.StockNum - model.SellNum;
                    m.SellNum = m.SellNum + model.SellNum;
                   
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 获取商品实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_MallGoods GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_MallGoods.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
    }
}
