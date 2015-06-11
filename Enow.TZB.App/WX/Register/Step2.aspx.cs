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
            var model =BMemberApp.GetUserModel();
            if (model != null) {
                var wzmodel = BMember.GetModel(model.Id);
                if (wzmodel == null)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8));
                    return;
                }
                if (wzmodel.CountryId == 1 && wzmodel.ProvinceId != 190 && wzmodel.ProvinceId != 191 && wzmodel.ProvinceId != 988)
                {
                    if (!Utils.IsIDcard(PersonalId))
                    {
                        MessageBox.ShowAndReturnBack("请输入正确的身份证号码！");
                        return;
                    }
                }
                wzmodel.ContactName = ContactName;
                wzmodel.PersonalId = PersonalId;
                wzmodel.Address = Address;
                BMember.Update(wzmodel);
                BMemberApp.UserLogin(wzmodel.Id);
                Response.Redirect("Step3.aspx", true);
            } else {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8));
            }
        }
    }
}