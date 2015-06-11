using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using System.Web.UI.HtmlControls;

namespace Enow.TZB.Web.WX.Member
{
    public partial class AddressAdd : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        public string modz = "true";
        protected string CId = "", PId = "", CSId = "", AId = "", SiteID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Getusermodel();
                InitLoad();
            }
        }
        private void Getusermodel()
        {
            BMemberAuth.LoginCheck();
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            InitMember(OpenId);
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember(string OpenId)
        {
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitLoad()
        {
            var id = Utils.GetQueryStringValue("ID");
            tbl_SendAddress model = BSendAddress.GetSendIdUserIDModel(id, MemberId);
            if (model!=null&&model.IsDelete==0)
            {
                CId = model.CountyId.ToString();
                PId = model.ProvinceId.ToString();
                CSId = model.CityId.ToString();
                AId = model.AreaId.ToString();
                txtRecipient.Text = model.Recipient;
                txtMobile.Text = model.MobilePhone.Trim();
                txtAddress.Text = model.Address;
                if (model.IsDefaultAddress==true)
                {
                    modz = "false";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 配送信息
            var id = Utils.GetQueryStringValue("ID");
            bool usdl = false;
            var AuthModel = BMemberAuth.GetUserModel();
            if (AuthModel != null)
            {
                var model = BMember.GetModelByOpenId(AuthModel.OpenId);
                if (model != null)
                {
                    MemberId = model.Id;
                    usdl = true;
                }
            }
            if (usdl == false)
            {
                MessageBox.ShowAndRedirect("请先登录！", "AddressList.aspx");
                return;
            }
                
            string Recipient = "";
            //收货地址编号
            string AdddressId = Guid.NewGuid().ToString();
            //错误提示
            string strErr = "";
            //国家编号 名称
            int CountryId = Utils.GetInt(Utils.GetFormValue(ddlCountry.UniqueID));
            string Countryname = "";
            //省编号 名称
            int ProvinceId = Utils.GetInt(Utils.GetFormValue(ddlProvince.UniqueID));
            string Provincename = "";
            //市编号 名称
            int CityId = Utils.GetInt(Utils.GetFormValue(ddlCity.UniqueID));
            string Cityname = "";
            //区编号 名称
            int AreaId = Utils.GetInt(Utils.GetFormValue(ddlArea.UniqueID));
            string Areaname = "";
            //默认地址
            bool IsDefaultAddress = false;
            //收件人
            if (!string.IsNullOrEmpty(txtRecipient.Text))
            {
                Recipient = txtRecipient.Text;
            }
            #region 国家省市区名称查询
            var CountryModel = BMSysProvince.GetCountryModel(CountryId);
            if (CountryModel != null)
            {
                Countryname = CountryModel.Name;
            }
            else
            {
                strErr += "请选择国家！\n";
            }
            var ProModel = BMSysProvince.GetProvinceModel(ProvinceId);
            if (ProModel != null)
            {
                Provincename = ProModel.Name;
            }
            else
            {
                strErr += "请选择省份！\n";
            }
            var CityModel = BMSysProvince.GetCityModel(CityId);
            if (CityModel != null)
            {
                Cityname = CityModel.Name;
            }
            else
            {
                strErr += "请选择城市！\n";
            }
            var AreaModel = BMSysProvince.GetAreaModel(AreaId);
            if (AreaModel != null)
            {
                Areaname = AreaModel.Name;
            }
            else
            {
                strErr += "请选择区县！\n";
            }
            if (!string.IsNullOrEmpty(strErr))
            {
                MessageBox.Show(strErr);
                return;
            }
            #endregion

            //修改默认地址信息
            if (hdfmrdz.Value=="1")
            {
                IsDefaultAddress = true;
            }
            tbl_SendAddress adds = new tbl_SendAddress()
            {
                LogisticsNo = "",
                Recipient = Recipient,
                MobilePhone = txtMobile.Text,
                Telephone = "0",
                CountyId = CountryId,
                CountyName = Countryname,
                ProvinceId = ProvinceId,
                ProvinceName = Provincename,
                CityId = CityId,
                CityName = Cityname,
                AreaId = AreaId,
                AreaName = Areaname,
                Address = txtAddress.Text,
                IsDefaultAddress = IsDefaultAddress,
                MemberId = MemberId,
                IsDelete = 0
            };
            if (adds.IsDefaultAddress)
            {
                BSendAddress.UpdateDefaultAddress(MemberId);
            }
            if (!string.IsNullOrEmpty(id))
            {
                adds.Id = id;
                BSendAddress.UpdateModel(adds);
            }
            else
            {
                adds.Id = AdddressId;
                BSendAddress.Add(adds);
            }
            MessageBox.ShowAndRedirect("保存成功", "AddressList.aspx");
            #endregion
        }
    }
}