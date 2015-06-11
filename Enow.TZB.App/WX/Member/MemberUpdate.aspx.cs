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
    /// <summary>
    /// 会员修改
    /// </summary>
    public partial class MemberUpdate : System.Web.UI.Page
    {
        #region 省市联动下拉框初始化
        protected int CountryId = 0;
        protected int ProvinceId = -1;
        protected int CityId = -1;
        protected int AreaId = 0;
        #endregion

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
                BMemberApp.LoginCheck();
                InitMember();
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        private void InitMember()
        {
            var model = BMemberApp.GetUsIdModel();
            if (model != null)
            {
                CountryId = model.CountryId;
                ProvinceId = model.ProvinceId;
                CityId = model.CityId;
                AreaId = model.AreaId;
                ltrUserName.Text = model.UserName;
                if (!String.IsNullOrWhiteSpace(model.MemberPhoto)) {
                    this.ltrHead.Text = "<img src=\"" +System.Configuration.ConfigurationManager.AppSettings["Uploadimgurl"]+ model.MemberPhoto + "\" width=\"55\" height=\"50\" border=\"0\" />";
                }
                if (model.Gender.HasValue)
                {
                    this.hidSex.Value = model.Gender.Value.ToString();
                    if (model.Gender.Value == 1)
                    {
                        this.ltrSex.Text = "<a id=\"man\" onclick=\"cGender(this.id);\"><img id=\"imgMan\" src=\"../images/ManOn.png\" border=\"0\"></a><a id=\"woman\" onclick=\"cGender(this.id);\"><img id=\"imgWoman\" src=\"../images/Woman.png\" border=\"0\"></a>";
                    }
                    else
                    {
                        this.ltrSex.Text = "<a id=\"man\" onclick=\"cGender(this.id);\"><img id=\"imgMan\" src=\"../images/Man.png\" border=\"0\"></a><a id=\"woman\" onclick=\"cGender(this.id);\"><img id=\"imgWoman\" src=\"../images/WomanOn.png\" border=\"0\"></a>";
                    }
                }
                else
                {
                    this.ltrSex.Text = "<a id=\"man\" onclick=\"cGender(this.id);\"><img id=\"imgMan\" src=\"../images/ManOn.png\" border=\"0\"></a><a id=\"woman\" onclick=\"cGender(this.id);\"><img id=\"imgWoman\" src=\"../images/Woman.png\" border=\"0\"></a>";
                }
                this.txtemail.Value = model.Email;

            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int CountryId = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID));
            int ProvinceId = Utils.GetInt(Utils.GetFormValue(ddlProvince.UniqueID));
            int CityId = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID));
            int AreaID = Utils.GetInt(Utils.GetFormValue(ddlArea.UniqueID));
            string strpwd = Utils.GetFormValue("txtpwd");
            string email = Utils.GetFormValue("txtemail");
            int Gender = Utils.GetInt(Utils.GetFormValue("hidSex"));

            #region 文件上传
            string MemberPhoto = "";
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
                MemberPhoto = path + fileName;
                try
                {
                    file.SaveAs(Server.MapPath(MemberPhoto));
                }
                catch
                {
                    MessageBox.ShowAndReturnBack("文件上传失败！");
                    return;
                }
            }
            #endregion
            HashCrypto has = new HashCrypto();
            if (!String.IsNullOrWhiteSpace(strpwd))
                strpwd = has.MD5Encrypt(strpwd);
            has.Dispose();
            string CountryName = "";
            var CountryModel = BMSysProvince.GetCountryModel(CountryId);
            if (CountryId > 0 && CountryModel != null)
            {
                CountryName = CountryModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(50));
                return;
            }
            string ProvinceName = "";
            var ProvinceModel = BMSysProvince.GetProvinceModel(ProvinceId);
            if (ProvinceId > 0 && ProvinceModel != null)
            {
                ProvinceName = ProvinceModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(51));
                return;
            }
            string CityName = "";
            var CityModel = BMSysProvince.GetCityModel(CityId);
            if (CityId > 0 && CityModel != null)
            {
                CityName = CityModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(52));
                return;
            }
            string AreaName = "";
            var AreaModel = BMSysProvince.GetAreaModel(AreaID);
            if (AreaID > 0 && AreaModel != null)
            {
                AreaName = AreaModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(53));
                return;
            }
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                if (!String.IsNullOrWhiteSpace(strpwd))
                    model.Password = strpwd;
                model.CountryId = CountryId;
                model.CountryName = CountryName;
                model.ProvinceId = ProvinceId;
                model.ProvinceName = ProvinceName;
                model.CityId = CityId;
                model.CityName = CityName;
                model.AreaId = AreaID;
                model.AreaName = AreaName;
                model.Gender = Gender;
                model.Email = email;
                model.MemberPhoto = MemberPhoto;
                BMember.Update(model);
                MessageBox.ShowAndRedirect("修改成功！", "Step2.aspx");
                return;
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
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
    }
}