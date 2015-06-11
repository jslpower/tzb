using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.Team
{
    public partial class CreateTeam : System.Web.UI.Page
    {
        /// <summary>
        /// 球队图片保存路径
        /// </summary>
        private string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (BWebMemberAuth.IsLoginCheck())
                {
                    var model=BWebMemberAuth.GetUserModel();
                    InitModel(model.Id);
                    this.Master.Page.Title = "创建球队";
                }
                else
                {
                    MessageBox.ShowAndRedirect("请登录!", "/Default.aspx");
                }
            }

        }

        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="MemberId"></param>
        private void InitModel(string MemberId)
        {
            var MemberModel = BMember.GetModel(MemberId);
            if (MemberModel!=null)
            {
                //检查会员状态
                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State, "/");
                if (BTeamMember.GetModel(MemberModel.Id) != null)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(15), "/my/Default.aspx");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(MemberModel.ContactName))
                {
                    this.ltrContactName.Text = MemberModel.ContactName;
                    //查询自己所在的球队
                    var TeamModel = BTeamMember.GetModel(MemberModel.Id);
                    if (TeamModel != null)
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(15), "/My/Default.aspx");
                        return;
                    }
                }
                else
                {

                    MessageBox.ShowAndRedirect("真实信息不完整，请补充!", "/My/Update.aspx");
                    return;
                }
            
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/Default.aspx");
                return;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
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
            var Membermodel = BWebMemberAuth.GetUserModel();
            string MemberId = Membermodel.Id;
            string TeamName = Utils.GetFormValue(txtTeamName.UniqueID);
            string SQWZ = Convert.ToString((Model.EnumType.球员位置)Utils.GetInt(Utils.GetFormValue("SQWZ")));
            int QYHM = Utils.GetInt(Utils.GetFormValue(txtQYHM.UniqueID));
            string TeamInfo = Utils.GetFormValue("TeamInfo");
            var model = BMember.GetModel(MemberId);
            if (model != null)
            {
                string TeamId = System.Guid.NewGuid().ToString();
                //添加球队
                BTeam.Add(new tbl_BallTeam
                {
                    Id = TeamId,
                    TypeId = TypeId,
                    MemberId = model.Id,
                    MemberName = model.ContactName,
                    CountryId = model.CountryId,
                    CountryName = model.CountryName,
                    ProvinceId = model.ProvinceId,
                    ProvinceName = model.ProvinceName,
                    CityId = model.CityId,
                    CityName = model.CityName,
                    AreaId = model.AreaId,
                    AreaName = model.AreaName,
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
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(16), "/My/Default.aspx");
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