using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.My
{
    public partial class TeamUpdate : System.Web.UI.Page
    {
        private string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
        protected int MeberSQWZ = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (BWebMemberAuth.IsLoginCheck())
                {
                    this.Master.Page.Title = "修改球队";
                    var MemberModel = BWebMemberAuth.GetUserModel();
                    InitModel(MemberModel.Id);
                }
                else
                {
                    MessageBox.ShowAndRedirect("请登录", "/Default.aspx");
                }

            }

        }

        /// <summary>
        /// 加载球队信息
        /// </summary>
        /// <param name="memberId"></param>
        private void InitModel(string memberId)
        {
            var MemberModel = BMember.GetModel(memberId);
            if (MemberModel != null)
            {
                string TeamId = "";
                var TeamModel = BTeamMember.GetModel(memberId);
                if (TeamModel != null)
                {
                    HidTeamId.Value = TeamModel.TeamId;
                    HidTeamMemberId.Value = TeamModel.Id;
                 
                    TeamId = TeamModel.TeamId;
                    if (!String.IsNullOrWhiteSpace(TeamModel.DNWZ))
                        MeberSQWZ = Convert.ToInt32((Model.EnumType.球员位置)Enum.Parse(typeof(Model.EnumType.球员位置), TeamModel.DNWZ));
                    txtQYHM.Text = TeamModel.DNQYHM.ToString();
                    if ((Model.EnumType.球员角色)TeamModel.RoleType != Model.EnumType.球员角色.队长)
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(47), "/WX/Member/Default.aspx");
                        return;
                    }
                    var model = BTeam.GetModel(TeamId);
                    if (model != null)
                    {
                        this.txtTeamName.Text = model.TeamName;
                        this.ltrContactName.Text = model.MemberName;
                        this.TeamInfo.Value = model.TeamInfo;
                        HidTeamPhoto.Value = model.TeamPhoto;
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(7), "Default.aspx");
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "/Register/Register.aspx");
                    return;
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string TeamId = Utils.GetFormValue(HidTeamId.UniqueID);
            string TeamMemberId = Utils.GetFormValue(HidTeamMemberId.UniqueID);
            if (String.IsNullOrWhiteSpace(TeamId) || String.IsNullOrWhiteSpace(TeamMemberId))
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(43));
                return;
            }
            #region 文件上传
            string TeamPhoto = "";
            string _UploadFileExt = ".gif,.bmp,.png,.jpg,.jpeg";
            int _UpFolderSize = 2024;//KB
            string tmpFileExt = _UploadFileExt;
            string[] strFileExt = tmpFileExt.Split(',');
            if (imgFileUpload.HasFile)
            {
                System.Web.HttpPostedFile file = imgFileUpload.PostedFile;
                //判断文件大小
                if (file.ContentLength > _UpFolderSize * 1024)
                {
                    MessageBox.ShowAndReturnBack("图片不能超过2MB！");
                    return;
                }
                //检验后缀名
                if (!String.IsNullOrWhiteSpace(file.FileName))
                {
                    if (IsStringExists(System.IO.Path.GetExtension(file.FileName).ToLower().Trim(), strFileExt) == false)
                    {
                        MessageBox.ShowAndReturnBack("图片格式不正确！");
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndReturnBack("请上传图片文件！");
                    return;
                }
                //保存文件
                string path = UploadPath + DateTime.Now.ToString("yyyyMMdd") + "/";
                CreateDirectory(Server.MapPath(path));
                string fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                TeamPhoto = path + fileName;
                try
                {
                    file.SaveAs(Server.MapPath(TeamPhoto));
                }
                catch
                {
                    MessageBox.ShowAndReturnBack("文件上传失败！");
                    return;
                }
            }
            else
            {
                TeamPhoto = Utils.GetFormValue(HidTeamPhoto.UniqueID);
            }
            #endregion
            var MemberModel = BWebMemberAuth.GetUserModel();
            string MemberId = MemberModel.Id;
            string TeamName = Utils.GetFormValue(txtTeamName.UniqueID);
            string SQWZ = Convert.ToString((Model.EnumType.球员位置)Utils.GetInt(Utils.GetFormValue("SQWZ")));
            int QYHM = Utils.GetInt(Utils.GetFormValue(txtQYHM.UniqueID));
            string TeamInfo = Utils.GetFormValue(this.TeamInfo.UniqueID);
            var model = BMember.GetModel(MemberId);
            if (model != null)
            {
                //添加球队
                BTeam.Update(new tbl_BallTeam
                {
                    Id = TeamId,
                    TeamName = TeamName,
                    TeamPhoto = TeamPhoto,
                    TeamInfo = TeamInfo,
                    State = (int)Model.EnumType.球队审核状态.审核中
                });
                //修改队长信息
                BTeamMember.UpdateBaseInfo(new tbl_TeamMember
                {
                    Id = TeamMemberId,
                    DNWZ = SQWZ,
                    DNQYHM = QYHM
                });
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(16), "Default.aspx");
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8));
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
    }
}