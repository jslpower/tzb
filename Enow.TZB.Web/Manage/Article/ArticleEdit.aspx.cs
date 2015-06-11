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
    public partial class ArticleEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Request.QueryString["doType"].ToString();
            string Id = Request.QueryString["Id"].ToString();
          
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitPublishTarget();
                InitClass();
                if ( StringValidate.IsInteger(Id))
                {
                    if (doType!="update")
                    {
                        this.linkBtnSave.Visible = false;
                    }
                    InitPage(int.Parse(Id));
                }
            }
            
        }

        private void InitPage(int Id)
        {
            var model = TZB.BLL.Common.sys.Article.GetModel(Id);
            if (model!=null)
            {
                this.txtTitle.Text = model.Title;
                this.txtTitleSulg.Text = model.TitleSulg;
                this.ddlClass.SelectedValue = model.ClassId.ToString();
                
                this.ddlPublishTarget.SelectedValue = model.PublishTarget.ToString();
                this.txtContent.Text = model.ContentInfo;
                #region 文章图片
                IList<Model.MFileInfo> files = new List<Model.MFileInfo>();
                files.Add(new Model.MFileInfo() { FilePath = model.PhotoUrl });
                UploadControl1.YuanFiles = files;
                #endregion

            }
        }

        /// <summary>
        /// 绑定文章类型
        /// </summary>
        private void InitPublishTarget()
        {
            Array array = Enum.GetValues(typeof(Model.发布对象));
            foreach (var arr in array)
            {
                int value = (int)Enum.Parse(typeof(Model.发布对象), arr.ToString());
                string text = Enum.GetName(typeof(Model.发布对象), arr);
                this.ddlPublishTarget.Items.Add(new ListItem() { Text = text, Value = value.ToString() });
            }
            this.ddlPublishTarget.Items.Insert(0, new ListItem("请选择文章类型", "0"));
            this.ddlPublishTarget.Items[0].Selected = true;
        }

        /// <summary>
        /// 绑定资讯类别
        /// </summary>
        private void InitClass()
        {
            Model.MarticleTypeSeach searchModel = new Model.MarticleTypeSeach();
            int rowsCount = 0;
            var list = ArticleClass.GetList(ref rowsCount, 999, 1, searchModel);
            if (rowsCount > 0)
            {
                ddlClass.DataSource = list;
                ddlClass.DataTextField = "TypeName";
                ddlClass.DataValueField = "Id";
                ddlClass.DataBind();
                this.ddlClass.Items.Insert(0, new ListItem("请选择资讯类别", "0"));
                this.ddlClass.Items.Insert(list.Count, new ListItem("铁之帮爱高", "100"));
            }
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            var model = ManageUserAuth.GetManageUserModel();
            string strErr = "";
            string Id = Request.QueryString["Id"].ToString();
            if (string.IsNullOrEmpty(Id)||!StringValidate.IsInteger(Id))
            {
                strErr = "参数错误，修改失败";
            }
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                strErr += "文章标题不能为空" ;
            }
            if (string.IsNullOrEmpty(txtContent.Text))
            {
                strErr = "文章内容不能为空" ;
            }
            if (string.IsNullOrWhiteSpace(txtTitleSulg.Text))
            {
                strErr = "文章概要不能为空";
            }
            int ClassId = -1, Publishtarget = -1;
         
            int.TryParse(Request.Form["ddlClass"].ToString(), out ClassId);
            int.TryParse(Request.Form["ddlPublishTarget"].ToString(), out Publishtarget);
         
            if (ClassId < 1)
            {
                strErr = "请选择资讯类别" ;
            }
            if (Publishtarget < 0)
            {
                strErr = "请选择文章类型" ;
            }
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }
            bool IsSucess = BLL.Common.sys.Article.Update(new tbl_Announcement {
                ID=int.Parse(Id),
                OperatorId = model.Id,
                OperatorName = model.ContactName,
                ClassId = ClassId,
                Title = txtTitle.Text,
                TitleSulg=txtTitleSulg.Text,
                PhotoUrl=GetAttachFile(),
                ContentInfo = txtContent.Text,
                IsRemind =2,
                Views = 0,
                IsValid = true,
                PublishTarget = Publishtarget,
                IssueTime = DateTime.Now
            });
            if (IsSucess)
            {
                MessageBox.ShowAndParentReload("修改成功！");
            }
            else
            {
                MessageBox.ShowAndReturnBack("修改失败！");
                return;
            }
        }
        /// <summary>
        /// 上传的附件
        /// </summary>
        /// <returns></returns>
        protected string GetAttachFile()
        {
            var files1 = UploadControl1.Files;
            var files2 = UploadControl1.YuanFiles;
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
    }
}