using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.register
{
    public partial class Step2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string id = Utils.GetQueryStringValue("id");
            string ContactName = Utils.GetFormValue(txt_ContractName.UniqueID);
            string PersonalId = Utils.GetFormValue(txt_PersonalId.UniqueID);
            string Address = Utils.GetFormValue(txt_Address.UniqueID);
            if (!string.IsNullOrWhiteSpace(id))
            {
                var model = BMember.GetModel(id);
                if (model != null)
                {
                    model.ContactName = ContactName;
                    model.PersonalId = PersonalId;
                    model.Address = Address;
                    if (model.CountryId==1&&model.ProvinceId!=190&&model.ProvinceId!=191&&model.ProvinceId!=988)
                    {
                        if (!Utils.IsIDcard(PersonalId))
                        {
                            MessageBox.ShowAndReturnBack("请输入正确的身份证号码！");
                            return;
                        }
                    }
                    BMember.Update(model);
                    Response.Redirect("Step3.aspx?Id=" + model.Id + "", true);
                }
                else
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(41));
                }

            }
            else
            {
                MessageBox.ShowAndRedirect("请注册", "Register.aspx");
            }
        }
    }
}