using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Register
{
    public partial class Step2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            string ContactName = Utils.GetFormValue("txtContactName");
            string PersonalId = Utils.GetFormValue("txtPersonalId");
            string Address = Utils.GetFormValue("txtAddress");
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null) {
                if (model.CountryId==1&&model.ProvinceId!=190&&model.ProvinceId!=191&&model.ProvinceId!=988)
                {
                    if (!Utils.IsIDcard(PersonalId))
                    {
                        MessageBox.ShowAndReturnBack("请输入正确的身份证号码！");
                        return;
                    }
                }
                model.ContactName = ContactName;
                model.PersonalId = PersonalId;
                model.Address = Address;
                BMember.Update(model);
                Response.Redirect("Step3.aspx", true);
            } else {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8));
            }
        }
    }
}