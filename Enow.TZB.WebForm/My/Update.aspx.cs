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
    public partial class Update : System.Web.UI.Page
    {
        #region 省市联动下拉框初始化
        protected int CountryId = 0, ProvinceID = 0, CityId = 0, AreaId = 0;
        protected string PhoneNumber = "";
        protected string Password = "";
        protected string MemberPhoto = "";
        private string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            BWebMemberAuth.LoginCheck();
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            this.Master.Page.Title = "会员中心-修改资料";
            var model = BWebMemberAuth.GetUserModel();
            string MemberId = "";
            if (model != null)
            {
                MemberId = model.Id;

                var UserModel = BMember.GetModel(MemberId);
                if (UserModel != null)
                {
                    ltrContactName.Text = UserModel.UserName;
                    CountryId = UserModel.CountryId;
                    ProvinceID = UserModel.ProvinceId;
                    CityId = UserModel.CityId;
                    AreaId = UserModel.AreaId;
                    if (UserModel.Gender == 0)
                    {
                        radWoman.Checked = true;
                    }
                    else
                    {
                        radMan.Checked = true;
                    }
                    txt_Phone.Text = UserModel.MobilePhone;
                    PhoneNumber = UserModel.MobilePhone;
                    txt_Email.Text = UserModel.Email;
                    txt_ContractName.Text = UserModel.ContactName;
                    txt_PersonalId.Text = UserModel.PersonalId;
                    txt_Address.Text = UserModel.Address;
                    Password = UserModel.Password;
                    txtNickName.Text = UserModel.NickName;
                    MemberPhoto = UserModel.MemberPhoto;
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            var Umodel = BWebMemberAuth.GetUserModel();
            string MemberId = Umodel.Id;
            var MemberModel = BMember.GetModel(MemberId);
            PhoneNumber = MemberModel.MobilePhone;
            #region 取值

            int countryId = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID));
            int provinceId = Utils.GetInt(Utils.GetFormValue(ddlProvince.UniqueID));
            int cityId = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID));
            int areaId = Utils.GetInt(Utils.GetFormValue(ddlArea.UniqueID));
            int Gender = 0;
            if (radMan.Checked == true)
            {
                Gender = 1;
            }
            string NickName = Utils.GetFormValue(txtNickName.UniqueID);
            string phone = Utils.GetFormValue(txt_Phone.UniqueID);
            string email = Utils.GetFormValue(txt_Email.UniqueID);
            string contractName = Utils.GetFormValue(txt_ContractName.UniqueID);
            string personalId = Utils.GetFormValue(txt_PersonalId.UniqueID);
            string address = Utils.GetFormValue(txt_Address.UniqueID);
            string valCode = Utils.GetFormValue(txt_valCode.UniqueID);

            string CountryName = "";
            var CountryModel = BMSysProvince.GetCountryModel(countryId);
            if (countryId > 0 && CountryModel != null)
            {
                CountryName = CountryModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(50));
                return;
            }
            string ProvinceName = "";
            var ProvinceModel = BMSysProvince.GetProvinceModel(provinceId);
            if (provinceId > 0 && ProvinceModel != null)
            {
                ProvinceName = ProvinceModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(51));
                return;
            }
            string CityName = "";
            var CityModel = BMSysProvince.GetCityModel(cityId);
            if (cityId > 0 && CityModel != null)
            {
                CityName = CityModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(52));
                return;
            }
            string AreaName = "";
            var AreaModel = BMSysProvince.GetAreaModel(areaId);
            if (areaId > 0 && AreaModel != null)
            {
                AreaName = AreaModel.Name;
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(53));
                return;
            }
            if (countryId == 1 && provinceId != 190 && provinceId != 191 && provinceId != 988)               
            {
                if (phone != PhoneNumber)
                {
                    var model = BLL.BMemberVerCode.GetInfo(phone, valCode, 0);
                    if (model == null || model.VerCode != valCode)
                    {
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(22));
                        return;
                    }

                    if (model.IsUsed)
                    {
                        MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(23));
                        return;
                    }
                }
                if (!StringValidate.IsMobilePhone(phone))
                {
                    MessageBox.ShowAndReturnBack("手机号码不正确!");
                    return;
                }
                if (!StringValidate.IsIDcard(personalId))
                {
                    MessageBox.ShowAndReturnBack("身份证号码不正确!");
                    return;
                }
            }
            if (!StringValidate.IsEmail(email))
            {
                MessageBox.ShowAndReturnBack("邮箱格式不正确!");
                return;
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.ShowAndReturnBack("请填写手机号!");
                return;
            }

            if (string.IsNullOrWhiteSpace(contractName))
            {
                MessageBox.ShowAndReturnBack("请输入真实姓名!");
                return;
            }
            if (string.IsNullOrWhiteSpace(personalId))
            {
                MessageBox.ShowAndReturnBack("请填写身份证号!");
                return;
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.ShowAndReturnBack("请填写详细地址!");
                return;
            }

            #region 头像上传

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
                    //  MessageBox.ShowAndReturnBack("请上传图片文件！");
                    MessageBox.ShowAndSelfClose("请上传图片文件");
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
            else
            {
                MemberPhoto = MemberModel.MemberPhoto;
            }
            #endregion

            #endregion

            #region 保存数据
            HashCrypto has = new HashCrypto();
            BMember.Update(new tbl_Member
            {

                Id = MemberId,
                CountryId = countryId,
                CountryName = CountryName,
                ProvinceId = provinceId,
                ProvinceName = ProvinceName,
                CityId = cityId,
                CityName = CityName,
                AreaId = areaId,
                AreaName = AreaName,
                Gender = Gender,
                MobilePhone = phone,
                Email = email,
                Password = Password,
                ContactName = contractName,
                PersonalId = personalId,
                Address = address,
                NickName = NickName,
                State=(int)Model.EnumType.会员状态.审核中,
                MemberPhoto = MemberPhoto,
                LastModifyTime = DateTime.Now

            });
            MessageBox.ShowAndRedirect("操作成功", "Default.aspx");
            #endregion

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