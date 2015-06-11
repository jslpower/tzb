using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;

namespace Enow.TZB.BLL
{
    public class BShoppingChart
    {
        public static List<tbl_ShoppingChart> GetShoppingList(string chartId)
        {
            string FieldsList = "*";
            string TableName = "tbl_ShoppingChart";
            string OrderString = " ORDER BY JoinTime desc";
            string strWhere = "(1=1)";

            string[] charts = chartId.Split(',');
            string inExpress = "";
            for (int i = 0; i < charts.Length;i++ )
            {
                inExpress = inExpress+"'"+charts[i]+"',";
            }
            inExpress = inExpress.Remove(inExpress.Length - 1, 1);
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);

                List<tbl_ShoppingChart> q = rdc.ExecuteQuery<tbl_ShoppingChart>(@"SELECT " + FieldsList + " FROM " + TableName + " WHERE Id in (" + inExpress + ") " + " " + OrderString).ToList();
                return q;
            }
        }

        /// <summary>
        /// 获取购物列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_ShoppingChart> GetShoppingGoodsList(ref int rowsCount, int intPageSize, int CurrencyPage, MMallGoodsSearch searchModel)
        {
            string FieldsList = "*";
            string TableName = "tbl_ShoppingChart";
            string OrderString = " ORDER BY JoinTime desc";
            string strWhere = "(1=1)";
            if (!string.IsNullOrWhiteSpace(searchModel.GoodsName)&&searchModel.GoodsName!="请输入")
            {
                strWhere += " and GoodsName like '%" + searchModel.GoodsName + "%'";
            }

            if (!string.IsNullOrWhiteSpace(searchModel.MemberId))
            {
                strWhere += " and MemberId = '" + searchModel.MemberId + "'";
            }
            strWhere += " and OrderId =''";
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_ShoppingChart> q = rdc.ExecuteQuery<tbl_ShoppingChart>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        public static bool Update(tbl_ShoppingChart model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_ShoppingChart.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.OrderId = model.OrderId;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }
        /// <summary>
        /// 给指定的购物车实体绑定订单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool UpdateCharModel(List<string> list,string OrderId)
        {
            using (FWDC rdc = new FWDC())
            {
                var charids = list;
                var model = (from temp in rdc.tbl_ShoppingChart where list.Contains(temp.Id) select temp).ToList();
                if (model.Count>0)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        model[i].OrderId = OrderId;
                    }
                    rdc.SubmitChanges();
                    return true;
                }
                return false;

            }
        }
        /// <summary>
        /// 修改购物车订单状态太
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool Update(List<tbl_ShoppingChart> list)
        {
            bool issuccess = false;
            using (FWDC rdc = new FWDC())
            {
                foreach (var m in list)
                {
                   issuccess = Update(m);
                   if (!issuccess)
                   {
                       return false;
                   }
                }
                return true;
               
            }
        }
        /// <summary>
        /// 根据orderid删除购物车
        /// </summary>
        /// <param name="orderid">购物车编号</param>
        /// <param name="MemberId">用户编号</param>
        /// <returns></returns>
        public static bool Delete(string orderid, string MemberId)
        {
            using (FWDC rdc = new FWDC())
            {

                var m = rdc.tbl_ShoppingChart.FirstOrDefault(n => n.Id == orderid && n.MemberId == MemberId);
                if (m != null)
                {
                    rdc.tbl_ShoppingChart.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }

        }
        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="model"></param>
        public static bool Add(tbl_ShoppingChart model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_ShoppingChart.InsertOnSubmit(model);
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
        /// 取得购物车实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_ShoppingChart GetModel(string  Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_ShoppingChart.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
        /// <summary>
        /// 取得购物车列列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static List<tbl_ShoppingChart> GetShoppingChartList(string orderid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from temp in rdc.tbl_ShoppingChart where temp.OrderId == orderid select temp);
                return model.ToList();
            }
        }
        /// <summary>
        /// 根据订单编号获取购物车信息
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <returns></returns>
        public static List<tbl_ShoppingChart> GetOrdermodels(string OrderId,string usid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from temp in rdc.tbl_ShoppingChart where temp.OrderId == OrderId && temp.MemberId==usid select temp);
                return model.ToList();
            }
        }
        /// <summary>
        /// 根据义卖商品订单编号和发布人获取购物车信息
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <param name="usid">用户编号</param>
        /// <returns></returns>
        public static List<dt_BazaarOrderView> GetBazaarOrdermodels(string OrderId, string usid)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from temp in rdc.dt_BazaarOrderView where temp.OrderId == OrderId && temp.GoodsMemberId == usid select temp);
                return model.ToList();
            }
        }
        /// <summary>
        /// 根据订单编号获取购物车信息
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <returns></returns>
        public static List<tbl_ShoppingChart> GetOrdermodels(string OrderId)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = (from temp in rdc.tbl_ShoppingChart where temp.OrderId == OrderId  select temp);
                return model.ToList();
            }
        }
        /// <summary>
        /// 查询购物车是否可用
        /// </summary>
        /// <param name="CharId">购物车编号</param>
        /// <returns></returns>
        public static bool Getcharbool(List<string> CharId)
        {
            using (FWDC rdc = new FWDC())
            {
                var list = CharId;
                var model = (from temp in rdc.tbl_ShoppingChart where temp.OrderId != "" && list.Contains(temp.Id) select temp).ToList();
                if (model.Count>0)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
