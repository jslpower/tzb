using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Member
{
    public partial class ArticleAdd : System.Web.UI.Page
    {
        private string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitModel();
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        private void InitModel()
        {
            var MemberModel = BMemberApp.GetUserModel();
            if (MemberModel != null)
            {
                if (String.IsNullOrWhiteSpace(MemberModel.ContactName))
                {
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }

                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
     
        /// <summary>
        /// 建立目录
        /// </summary>
        /// <param name="DirectoryName">目录名</param>
        /// <returns>返回数字,0:目录建立成功, 1:目录已存在,2:目录建立失败</returns>
        private int CreateDirectory(string DirectoryName)
        {
            try
            {
                if (!System.IO.Directory.Exists(DirectoryName))
                {
                    System.IO.Directory.CreateDirectory(DirectoryName);
                    return 0;
                }
                else
                {

                    return 1;
                }
            }
            catch
            {
                return 2;
            }
        }
        /// <summary>
        /// 检测字符串是否是数组中的一项
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="arrData"></param>
        /// <returns></returns>
        private bool IsStringExists(string inputData, string[] arrData)
        {
            if (null == inputData || string.Empty == inputData)
            {
                return false;
            }
            foreach (string tmpStr in arrData)
            {
                if (inputData == tmpStr)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddArticle_Click1(object sender, EventArgs e)
        {
            int types = Utils.GetInt(Utils.GetQueryStringValue("types"),0);
            if (types>2)
            {
                types = 0;
            }
          
            string strErr = "";
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                strErr += "日志标题不能为空!/n";
            }
            if (string.IsNullOrWhiteSpace(txtArticleInfo.Text))
            {
                strErr += "日志内容不能为空!/n";

            }
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }
            #region 文件上传
            //string MemberPhoto = "";
            //string _UploadFileExt = ".gif,.bmp,.png,.jpg,.jpeg";
            //int _UpFolderSize = 2024;//KB
            //string tmpFileExt = _UploadFileExt;
            //string[] strFileExt = tmpFileExt.Split(',');
            //if (imgFileUpload.HasFile)
            //{
            //    System.Web.HttpPostedFile file = imgFileUpload.PostedFile;
            //    //判断文件大小
            //    if (file.ContentLength > _UpFolderSize * 1024)
            //    {
            //        MessageBox.ShowAndReturnBack("图片不能超过2MB！");
            //        return;
            //    }
            //    //检验后缀名
            //    if (!String.IsNullOrWhiteSpace(file.FileName))
            //    {
            //        if (IsStringExists(System.IO.Path.GetExtension(file.FileName).ToLower().Trim(), strFileExt) == false)
            //        {
            //            MessageBox.ShowAndReturnBack("图片格式不正确！");
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.ShowAndReturnBack("请上传图片文件！");
            //        return;
            //    }
            //    //保存文件
            //    string path = UploadPath + DateTime.Now.ToString("yyyyMMdd") + "/";
            //    CreateDirectory(Server.MapPath(path));
            //    string fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
            //    MemberPhoto = path + fileName;
            //    try
            //    {
            //        file.SaveAs(Server.MapPath(MemberPhoto));
            //    }
            //    catch
            //    {
            //        MessageBox.ShowAndReturnBack("图片上传失败！");
            //        return;
            //    }
            //}
            #endregion

            string MemberId = "",TeamId = Model.MCommon.DefaultGuidId;
            string NickName = "",MemberName = "";
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                if (types != 0 && model.Jobtyoe != null && types != model.Jobtyoe)
                {
                    types =Utils.GetInt(model.Jobtyoe.ToString());
                }
                if (model.Jobtyoe==null)
                {
                    types = 0;
                }
                MemberId = model.Id;
                NickName = model.NickName;
                MemberName = model.ContactName;
                var TeamModel = BTeamMember.GetModel(MemberId);
                if (TeamModel != null)
                {
                    TeamId = TeamModel.TeamId;
                }
                TeamModel = null;
                BArticle.Add(new tbl_Articles
                {
                    Id = Guid.NewGuid().ToString(),
                    TypeId = types,
                    MemberId = MemberId,
                    NickName = NickName,
                    MemberName = MemberName,
                    TeamId = TeamId,
                    ArticleTitle = txtTitle.Text,
                    ArticlePhoto = "",
                    ArticleInfo = txtArticleInfo.Text,
                    Views = 0,
                    AppViews = 0,
                    WeChatViews = 0,
                    LeaveNumber = 0,
                    GreetNumber = 0,
                    IsEnable = true,
                    IssueTime = DateTime.Now

                });
                MessageBox.ShowAndRedirect("日志添加成功！", "Articles.aspx?" + (types != 0 ? "types=" + types : ""));
            }
            else { MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8)); }
        }

    }
}
