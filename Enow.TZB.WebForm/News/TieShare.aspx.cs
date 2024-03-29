﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.News
{
    public partial class TieShare : System.Web.UI.Page
    {
        #region 页面变量
        protected int pageSize = 20, pageIndex = 1, recordCount = 0;
        protected string PageTitle = "";
        protected int ClassId = 13;
        protected int HmId = 0;
        protected int ImgCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ClassId =Utils.GetInt(Utils.GetQueryStringValue("ClassId"), 13);
            HmId = Utils.GetInt(Utils.GetQueryStringValue("HmId"), 0);
            if (!IsPostBack)
            {
                if (ClassId == 100)
                {
                    GetUsdl();
                }
                else
                {
                    InitList(ClassId);
                }
                
            }
        }
        public void GetUsdl()
        {
            var model = BWebMemberAuth.GetUserModel();
            if (model!=null)
            {
                if (Bcode.Getuscode(model.Id,1))
                {
                    InitList(ClassId);
                }
                else
                {
                    MessageBox.ShowAndRedirect("请先绑定视频码！", "BDcode.aspx");
                }
            }
            else
            {
                MessageBox.ShowAndRedirect("请先登录！", "/Default.aspx");
                
            }
        }
        #region 按照文章类型Id绑定文章列表页
        private void InitList(int ClassId)
        {

            if (ClassId < 1)
            {
                MessageBox.ShowAndReturnBack("文章分类不存在！");
                return;
            }
            if (ClassId == 100)
            {
                PageTitle = "铁子帮爱高";
            }
            else
            {
                var ClassModel = ArticleClass.GetModel(ClassId);
                #region 根据ClassId获取当前页面的title
                if (ClassModel != null)
                {
                    PageTitle = ClassModel.TypeName;
                }
                else
                {
                    PageTitle = "文章列表";
                }
            }
            
            #endregion
            MArticleSearch searchModel = new MArticleSearch();

            searchModel.ClassId = ClassId;
            searchModel.IsValid = true;
            // searchModel.PublicTarget = Model.发布对象.网站;
            UtilsCommons.Paging(pageSize, ref pageIndex, recordCount);
            string Page = Request.QueryString["Page"];
            if (!String.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                pageIndex = int.Parse(Page);
                if (pageIndex < 1)
                    pageIndex = 1;
            }
            var list = BLL.Common.sys.Article.GetList(ref recordCount, pageSize, pageIndex, searchModel);
            if (recordCount > 0)
            {
                rptNewsList.DataSource = list;
                rptNewsList.DataBind();
                ExportPageInfo1.LinkType = 3;
                ExportPageInfo1.intPageSize = pageSize;
                ExportPageInfo1.intRecordCount = recordCount;
                ExportPageInfo1.CurrencyPage = pageIndex;
                ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
                ExportPageInfo1.UrlParams = Request.QueryString;
            }
            searchModel.ShowPhoto = true;
            recordCount = 0;
            var photoList = BLL.Common.sys.Article.GetList(ref recordCount, 5, 1, searchModel);
            ImgCount = recordCount;
            if (recordCount > 0)
            {
                rptPhotoList.DataSource = photoList;
                rptPhotoList.DataBind();
                rptCount.DataSource = photoList;
                rptCount.DataBind();
            }

        }
        #endregion
    }
}