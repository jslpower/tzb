using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Article
{
    public partial class TZArticleEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("dotype");
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
               
               
                switch (doType)
                {
                    case "update":
                        InitPage();
                        break;
                    case "view":
                        this.linkBtnSave.Visible = false;
                        InitPage();
                        break;
                }
            }
        }
        /// <summary>
        /// 上传的附件
        /// </summary>
        /// <returns></returns>
        protected string GetAttachFile()
        {
            var files1 = UploadPhoto.Files;
            var files2 = UploadPhoto.YuanFiles;
            if (files1 != null && files1.Count > 0)
            {
                return files1[0].FilePath;
            }
            if (files2 != null && files2.Count > 0)
            {
                return files2[0].FilePath;
            }
            return string.Empty;
        }
        private void InitPage()
        {
            string id = Utils.GetQueryStringValue("id");
            var model = BArticle.GetModel(id);
            if (model != null)
            {
                
                #region 日志附件
                IList<Model.MFileInfo> files = new List<Model.MFileInfo>();
                files.Add(new Model.MFileInfo() { FilePath = model.ArticlePhoto });
                UploadPhoto.YuanFiles = files;
                #endregion
                txtTitle.Text = model.ArticleTitle;
                txtContent.Text = model.ArticleInfo;

               

            }
        }
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string ArticlePhoto = GetAttachFile();
            bool isResult = false;
            ManagerList manageModel = ManageUserAuth.GetManageUserModel();
            int OperatorId = manageModel.Id;
            string OperatorName = manageModel.ContactName;
            manageModel = null;
            string doType = Utils.GetQueryStringValue("dotype").ToLower();
            string Id = Utils.GetQueryStringValue("id");

            string strErr = "";
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                strErr += "日志标题不能为空!/n";
            }
            if (string.IsNullOrWhiteSpace(txtContent.Text))
            {
                strErr += "日志内容不能为空!/n";

            }
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }
          
            switch (doType)
            {
                case "add":
                    isResult = BArticle.Add(new tbl_Articles
                    {
                        Id = Guid.NewGuid().ToString(),
                        //堂主日志
                        TypeId = 2,
                        MemberId = OperatorId.ToString(),
                        NickName = "",
                        MemberName = OperatorName,
                        TeamId = "",
                        ArticleTitle = txtTitle.Text,
                        ArticlePhoto = ArticlePhoto,
                        ArticleInfo = txtContent.Text,
                        Views = 0,
                        AppViews = 0,
                        WeChatViews = 0,
                        LeaveNumber = 0,
                        GreetNumber = 0,
                        IsEnable = true,
                        IssueTime = DateTime.Now
                        
                    });
                    break;
                case "update":
                    isResult = BArticle.Update(new tbl_Articles
                    {
                       
                        Id = Id,
                        //堂主日志
                        TypeId = 2,
                        MemberId = OperatorId.ToString(),
                        NickName ="",
                        MemberName = OperatorName,
                        ArticleTitle = txtTitle.Text,
                        ArticlePhoto = ArticlePhoto,
                        ArticleInfo = txtContent.Text,
                        Views = 0,
                        AppViews = 0,
                        WeChatViews = 0,
                        LeaveNumber = 0,
                        GreetNumber = 0,
                        IsEnable = true,
                        IssueTime = DateTime.Now
                       
                    });
                    break;
            }
            if (isResult)
            {

                MessageBox.ShowAndParentReload("操作成功");

            }
            else
            {

                MessageBox.ShowAndReturnBack("操作失败!");
                return;

            }
        }
    }
}