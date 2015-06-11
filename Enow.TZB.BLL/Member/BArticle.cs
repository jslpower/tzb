using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.BLL
{
    /// <summary>
    /// 会员日志
    /// </summary>
    public class BArticle
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Articles> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, MMemberArticleSearch SearchModel)
        {
            string FieldsList = "*";
            string TableName = "tbl_Articles";
            string OrderString = " ORDER BY IssueTime desc";
            string strWhere = "(1=1)";
            if (SearchModel.TypeId > 0)
            {
                strWhere = strWhere + " AND (TypeId = " + SearchModel.TypeId + ")";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.MemberId))
            {
                strWhere = strWhere + " AND (MemberId = '" +  StringValidate.CheckSql(SearchModel.MemberId.Trim()) + "')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.KeyWords))
            {
                strWhere = strWhere + " AND (ArticleTitle like '%" + SearchModel.KeyWords + "%')";
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.TeamId))
            {
                strWhere = strWhere + " AND (TeamId = '" + StringValidate.CheckSql(SearchModel.TeamId.Trim()) + "')";
            }
            if (SearchModel.IsEnable.HasValue)
            {
                string IsValid = SearchModel.IsEnable.Value ? "1" : "0";
                strWhere = strWhere + " AND (IsEnable = " + IsValid + ")";
            }
           
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_Articles> q = rdc.ExecuteQuery<tbl_Articles>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public static bool Add(tbl_Articles model)
        {
            using (FWDC rdc = new FWDC())
            {
                if (model != null)
                {
                    rdc.tbl_Articles.InsertOnSubmit(model);

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
        /// 取得文章实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_Articles GetModel(string Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Articles.FirstOrDefault(n => n.Id == Id);
                return model;
            }
        }
       
        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_Articles model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Articles.FirstOrDefault(n => n.Id == model.Id);
                if (m != null)
                {
                    m.ArticleTitle = model.ArticleTitle;
                    m.ArticlePhoto = model.ArticlePhoto;
                    m.ArticleInfo = model.ArticleInfo;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(string id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Articles.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    rdc.tbl_Articles.DeleteOnSubmit(m);
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 更新文章点击次数
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <param name="ArticleEnum">发布对象(网站、微信、APP)</param>
        /// <returns></returns>
        public static bool UpdateView(string id, Model.发布对象 ArticleEnum)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Articles.FirstOrDefault(n => n.Id == id);
                if (m != null)
                {
                    switch (ArticleEnum)
                    {
                        case 发布对象.网站:
                            m.Views = m.Views + 1;
                            break;
                        case 发布对象.微信:
                            m.WeChatViews = m.WeChatViews + 1;
                            break;
                        case 发布对象.APP:
                            m.AppViews = m.AppViews + 1;
                            break;
                        case 发布对象.点赞:
                            m.GreetNumber = m.GreetNumber + 1;
                            break;
                        default:
                            break;
                    }
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
