using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Team
{
    /// <summary>
    /// 创建球队
    /// </summary>
    public partial class CreateTeam : System.Web.UI.Page
    {
        private string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitModel(OpenId);
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitModel(string OpenId)
        {
            var MemberModel = BMember.GetModelByOpenId(OpenId);
            if (MemberModel != null)
            {
                if (BTeamMember.GetModel(MemberModel.Id) != null)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(15), "/WX/Member/Default.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                this.ltrCreateName.Text = MemberModel.ContactName;
                //查询自己所在的球队
                var TeamModel = BTeamMember.GetModel(MemberModel.Id);
                if (TeamModel != null)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(15), "/WX/Member/Default.aspx");
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            int TypeId = 0;
            #region 文件上传
            string TeamPhoto = "";
            string _UploadFileExt = ".gif,.bmp,.png,.jpg,.jpeg";
            int _UpFolderSize = 2024;//KB
            string tmpFileExt = _UploadFileExt;
            string[] strFileExt = tmpFileExt.Split(',');
            if (!imgFileUpload.HasFile)
            {
                MessageBox.ShowAndReturnBack("请上传图片！");
                return;
            }
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
            #endregion
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            string TeamName = Utils.GetFormValue("txtTeamName");
            string SQWZ = Convert.ToString((Model.EnumType.球员位置)Utils.GetInt(Utils.GetFormValue("SQWZ")));
            int QYHM = Utils.GetInt(Utils.GetFormValue("txtQYHM"));
            string TeamInfo = Utils.GetFormValue("txtRemark");
            if(BTeam.IsCreated(TeamName))
            {
                MessageBox.ShowAndReturnBack("该球队已创建，请创建其他球队！");
                return;
            }
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                string TeamId = System.Guid.NewGuid().ToString();
                //添加球队
                BTeam.Add(new tbl_BallTeam {
                    Id = TeamId,
                    TypeId = TypeId,
                    MemberId = model.Id,
                    MemberName = model.ContactName,
                    CountryId = model.CountryId,
                    CountryName = model.CountryName,
                    ProvinceId =model.ProvinceId,
                    ProvinceName = model.ProvinceName,
                    CityId = model.CityId,
                    CityName = model.CityName,
                    AreaId=model.AreaId,
                    AreaName=model.AreaName,
                    TeamName = TeamName,
                    TeamPhoto = TeamPhoto,
                    TeamInfo = TeamInfo,
                    State = (int)Model.EnumType.球队审核状态.审核中,
                    IssueTime = DateTime.Now
                });
                //添加队长
                BTeamMember.Add(new tbl_TeamMember
                {
                    Id = System.Guid.NewGuid().ToString(),
                    TeamId = TeamId,
                    MemberId = model.Id,
                    RoleType = (int)Model.EnumType.球员角色.队长,
                    SQWZ = SQWZ,
                    SQQYHM = QYHM,
                    JRYS = "",
                    DNWZ = SQWZ,
                    DNQYHM = QYHM,
                    State = (int)Model.EnumType.球队审核状态.初审通过,
                    IssueTime = DateTime.Now
                });
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(16), "/WX/Member/Default.aspx");
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