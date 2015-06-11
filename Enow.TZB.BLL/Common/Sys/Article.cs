using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enow.TZB.Model;
using Enow.TZB.Utility;


namespace Enow.TZB.BLL.Common.sys
{
    /// <summary>
    /// 文章资讯管理
    /// </summary>
    public class Article
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static List<tbl_Announcement> GetList(ref int rowsCount, int intPageSize, int CurrencyPage, MArticleSearch SearchModel)
        {
            string FieldsList = "ID,OperatorId,OperatorName,ClassId,Title,titleSulg,PhotoUrl,ContentInfo,Views,WeChatViews,APPViews,IsValid,IsRemind,PublishTarget,IssueTime";
            string TableName = "tbl_Announcement";
            string OrderString = " ORDER BY IssueTime desc";
            string strWhere = "(1=1)";
            if (SearchModel.ClassId > 0)
            {
                strWhere = strWhere + " AND (ClassId = " + SearchModel.ClassId + ")";
            }
            if (SearchModel.PublicTarget.HasValue)
            {
                strWhere = strWhere + " AND (PublishTarget = " + (int)SearchModel.PublicTarget.Value + ")";
            }
            if (SearchModel.IsValid.HasValue)
            {
                string IsValid = SearchModel.IsValid.Value ? "1" : "0";
                strWhere = strWhere + " AND (IsValid = " + IsValid + ")";
            }
            if (SearchModel.ShowPhoto.HasValue)
            {
                if (SearchModel.ShowPhoto.Value == true)
                {
                    strWhere = strWhere + " AND (PhotoUrl is not null and PhotoUrl !='')";
                }
            }
            if (!String.IsNullOrWhiteSpace(SearchModel.KeyWords))
            {
                strWhere = strWhere + " AND (Title LIKE '%" + SearchModel.KeyWords + "%')";
            }
            int PageLowerBound = (CurrencyPage - 1) * intPageSize + 1;
            int PageUpperBound = PageLowerBound + intPageSize - 1;
            int skipRows = (CurrencyPage - 1) * intPageSize;
            using (FWDC rdc = new FWDC())
            {
                var query = rdc.ExecuteQuery<int>(@"SELECT COUNT(1) FROM " + TableName + " WHERE " + strWhere);
                rowsCount = query.First<int>();
                List<tbl_Announcement> q = rdc.ExecuteQuery<tbl_Announcement>(@"SELECT " + FieldsList + " FROM (SELECT " + FieldsList + ",ROW_NUMBER() OVER (" + OrderString + ") AS RowNumber FROM " + TableName + " WHERE " + strWhere + ")Table_StocPage WHERE RowNumber BETWEEN " + PageLowerBound.ToString() + " AND " + PageUpperBound.ToString() + " " + OrderString).ToList();
                return q;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public static void Add(tbl_Announcement model)
        {
            using (FWDC rdc = new FWDC())
            {
                rdc.tbl_Announcement.InsertOnSubmit(model);
                rdc.SubmitChanges();
            }
        }

        /// <summary>
        /// 取得文章实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static tbl_Announcement GetModel(int Id)
        {
            using (FWDC rdc = new FWDC())
            {
                var model = rdc.tbl_Announcement.FirstOrDefault(n => n.ID == Id);
                return model;
            }
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model"></param>
        public static bool Update(tbl_Announcement model)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Announcement.FirstOrDefault(n => n.ID == model.ID);
                if (m != null)
                {

                    m.OperatorId = model.OperatorId;
                    m.OperatorName = model.OperatorName;
                    m.Title = model.Title;
                    m.TitleSulg = model.TitleSulg;
                    m.PhotoUrl = model.PhotoUrl;
                    m.PublishTarget = model.PublishTarget;
                    m.IsValid = model.IsValid;
                    m.IsRemind = model.IsRemind;
                    m.IssueTime = model.IssueTime;
                    m.ClassId = model.ClassId;
                    m.ContentInfo = model.ContentInfo;
                    rdc.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            using (FWDC rdc = new FWDC())
            {
                var m = rdc.tbl_Announcement.FirstOrDefault(n => n.ID == id);
                if (m != null)
                {
                    rdc.tbl_Announcement.DeleteOnSubmit(m);
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
        public static bool UpdateView(int id, Model.发布对象 ArticleEnum)
        {
            using (FWDC rdc=new FWDC())
            {
                var m = rdc.tbl_Announcement.FirstOrDefault(n => n.ID == id);
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
