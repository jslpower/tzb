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
    /// 球队信息修改
    /// </summary>
    public partial class TeamUpdate : System.Web.UI.Page
    {
        private string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
        protected int CountryId = 0, ProvinceID = 0, CityId = 0, AreaId = 0;
        protected int MeberSQWZ = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            BMemberApp.LoginCheck();
            InitModel();
        }
        #region 加载球队基本信息
        /// <summary>
        /// 加载球队基本信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitModel()
        {
            var MemberModel = BMemberApp.GetUserModel();
            if (MemberModel != null)
            {
                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                string MemberId = MemberModel.Id;
                string TeamId = "";
                //查询自己所在的球队
                var TeamModel = BTeamMember.GetModel(MemberId);
                if (TeamModel != null)
                {
                    hidTeamId.Value = TeamModel.TeamId;
                    hidTeamMemberId.Value = TeamModel.Id;
                    TeamId = TeamModel.TeamId;
                    if (!String.IsNullOrWhiteSpace(TeamModel.DNWZ))
                        MeberSQWZ = Convert.ToInt32((Model.EnumType.球员位置)Enum.Parse(typeof(Model.EnumType.球员位置), TeamModel.DNWZ));
                    txtQYHM.Value = TeamModel.DNQYHM.ToString();
                    if ((Model.EnumType.球员角色)TeamModel.RoleType != Model.EnumType.球员角色.队长)
                    {
                        MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(47), "/WX/Member/Default.aspx");
                        return;
                    }
                    var model = BTeam.GetModel(TeamId);
                    if (model != null)
                    {
                        CountryId = model.CountryId;
                        ProvinceID = model.ProvinceId;
                        CityId = model.CityId;
                        AreaId = model.AreaId;
                        this.txtTeamName.Value = model.TeamName;
                        this.ltrCreateName.Text = model.MemberName;
                        if (!String.IsNullOrWhiteSpace(model.TeamPhoto))
                        {
                            //hyImgLink.NavigateUrl = model.TeamPhoto;
                            //hyImgLink.Visible = true;
                        }
                        else
                        {
                            //hyImgLink.Visible = false;
                        }
                        this.txtRemark.Text = model.TeamInfo;
                    }
                    else
                    {
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(6));
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(7), "Default.aspx");
                    return;
                }
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/Default.aspx");
                return;
            }
        }
        #endregion
        /// <summary>
        /// 保存球队信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string TeamId = Utils.GetFormValue("hidTeamId");
            string TeamMemberId = Utils.GetFormValue("hidTeamMemberId");
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
            #endregion
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            string TeamName = Utils.GetFormValue(txtTeamName.UniqueID);
            string SQWZ = Convert.ToString((Model.EnumType.球员位置)Utils.GetInt(Utils.GetFormValue("SQWZ")));
            int QYHM = Utils.GetInt(Utils.GetFormValue(txtQYHM.UniqueID));
            string TeamInfo = Utils.GetFormValue(txtRemark.UniqueID);
            #region 修改球队所在地区
            int countryId = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID));
            int provinceId = Utils.GetInt(Utils.GetFormValue(ddlProvince.UniqueID));
            int cityId = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID));
            int areaId = Utils.GetInt(Utils.GetFormValue(ddlArea.UniqueID));

            string countryName = "";
            var CountryModel = BMSysProvince.GetCountryModel(countryId);
            if (CountryModel != null && countryId > 0)
            {
                countryName = CountryModel.Name;
            }

            string provinceName = "";
            var Pmodel = BMSysProvince.GetProvinceModel(provinceId);
            if (Pmodel != null && provinceId > 0)
            {
                provinceName = Pmodel.Name;
            }

            string cityName = "";
            var CityModel = BMSysProvince.GetCityModel(cityId);
            if (cityId > 0 && CityModel != null)
            {
                cityName = CityModel.Name;
            }

            string areaName = "";
            var areaModel = BMSysProvince.GetAreaModel(areaId);
            if (areaModel != null && areaId > 0)
            {
                areaName = areaModel.Name;
            }
            #endregion
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                //添加球队
                BTeam.Update(new tbl_BallTeam
                {
                    Id = TeamId,
                    TeamName = TeamName,
                    CountryId = countryId,
                    CountryName = countryName,
                    ProvinceId = provinceId,
                    ProvinceName = provinceName,
                    CityId = cityId,
                    CityName = cityName,
                    AreaId = areaId,
                    AreaName = areaName,
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