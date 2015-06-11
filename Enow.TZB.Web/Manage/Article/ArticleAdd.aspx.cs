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
    public partial class ArticleAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitPublishTarget();
                InitClass();
            }
        }

        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            var model = ManageUserAuth.GetManageUserModel();
            string strErr = "";
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                strErr += "文章标题不能为空!/n";
            }
            if (string.IsNullOrWhiteSpace(txtTitleSulg.Text))
            {
                strErr += "文章概要不能为空!/n";
            }
            if (string.IsNullOrEmpty(txtContent.Text))
            {
                strErr += "文章内容不能为空！/n";
            }
            int ClassId = -1, Publishtarget = -1;
          
            int.TryParse(Request.Form["ddlClass"].ToString(), out ClassId);
            int.TryParse(Request.Form["ddlPublishTarget"].ToString(), out Publishtarget);
         
            if (ClassId < 1)
            {
                strErr += "请选择资讯类别!/n";
            }
            if (Publishtarget<0)
            {
                strErr += "请选择文章类型!/n";
            }
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }
            BLL.Common.sys.Article.Add(new tbl_Announcement
            {
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
            MessageBox.ShowAndParentReload("添加成功！");
            return;

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
                this.ddlClass.Items.Insert(list.Count, new ListItem("铁子帮爱高", "100"));
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