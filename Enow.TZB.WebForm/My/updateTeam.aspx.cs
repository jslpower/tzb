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
    public partial class updateTeam : System.Web.UI.Page
    {
        private string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
        protected int MeberSQWZ = 0;
        protected int CountryId = 0, ProvinceID = 0, CityId = 0, AreaId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            BWebMemberAuth.LoginCheck();
            if (!IsPostBack)
            {
                var model = BWebMemberAuth.GetUserModel();
                InitPage(model.Id);
            }
        }

        #region 加载球队基本信息
        /// <summary>
        /// 加载球队基本信息
        /// </summary>
        /// <param name="Id">会员ID</param>
        private void InitPage(string Id)
        {
            this.Master.Page.Title = "会员中心-球队信息修改";
            string TeamId = "";
            if (!string.IsNullOrWhiteSpace(Id))
            {
                //查询自己所在的球队
                var model = BTeamMember.GetModel(Id);
                if (model != null)
                {
                    hidTeamId.Value = model.TeamId;
                    hidTeamMemberId.Value = model.Id;
                    TeamId = model.TeamId;
                    if (!string.IsNullOrWhiteSpace(model.DNWZ))
                    {
                        MeberSQWZ = Convert.ToInt32((Model.EnumType.球员位置)Enum.Parse(typeof(Model.EnumType.球员位置), model.DNWZ));
                    }
                    txtQYHM.Text = model.DNQYHM.ToString();
                    if ((Model.EnumType.球员角色)model.RoleType!=Model.EnumType.球员角色.队长)
                    {
                          MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(47), "Default.aspx");
                        return;
                    }
                    var teamModel = BTeam.GetModel(TeamId);
                    if (teamModel!=null)
                    {
                        CountryId = teamModel.CountryId;
                        ProvinceID = teamModel.ProvinceId;
                        CityId = teamModel.CityId;
                        AreaId = teamModel.AreaId;
                        this.txtTeamName.Text = teamModel.TeamName;
                        this.ltrContactName.Text = teamModel.MemberName;
                        this.TeamInfo.Value = teamModel.TeamInfo;
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
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "/Default.aspx");
                return;
            }
        }
        #endregion

        #region 保存球队信息

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string TeamId = Utils.GetFormValue(hidTeamId.UniqueID);
            string TeamMemberId = Utils.GetFormValue(hidTeamMemberId.UniqueID);
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

            var model = BWebMemberAuth.GetUserModel();
            string TeamName = Utils.GetFormValue(txtTeamName.UniqueID);
            string SQWZ = Convert.ToString((Model.EnumType.球员位置)Utils.GetInt(Utils.GetFormValue("SQWZ")));
            int QYHM = Utils.GetInt(Utils.GetFormValue(txtQYHM.UniqueID));
            string TeamInfo = Utils.GetFormEditorValue(this.TeamInfo.UniqueID);
            #region 修改球队所在地区
            int countryId = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID));
            int provinceId = Utils.GetInt(Utils.GetFormValue(ddlProvince.UniqueID));
            int cityId = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID));
            int areaId = Utils.GetInt(Utils.GetFormValue(ddlArea.UniqueID));

            string countryName = "";
            var CountryModel = BMSysProvince.GetCountryModel(countryId);
            if (CountryModel!=null&&countryId>0)
            {
                countryName = CountryModel.Name;
            }

            string provinceName = "";
            var Pmodel = BMSysProvince.GetProvinceModel(provinceId);
            if (Pmodel!=null&&provinceId>0)
            {
                provinceName = Pmodel.Name;
            }

            string cityName = "";
            var CityModel = BMSysProvince.GetCityModel(cityId);
            if (cityId>0&&CityModel!=null)
            {
                cityName = CityModel.Name;
            }

            string areaName = "";
            var areaModel = BMSysProvince.GetAreaModel(areaId);
            if (areaModel!=null&&areaId>0)
            {
                areaName = areaModel.Name;
            }
            #endregion

            //添加球队
            BTeam.Update(new tbl_BallTeam
            {
                Id = TeamId,
                TeamName = TeamName,
                CountryId=countryId,
                CountryName=countryName,
                ProvinceId=provinceId,
                ProvinceName=provinceName,
                CityId=cityId,
                CityName=cityName,
                AreaId=areaId,
                AreaName=areaName,
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
        #endregion

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