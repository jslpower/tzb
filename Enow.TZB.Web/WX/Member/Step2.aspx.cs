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
    /// 修改第二步
    /// </summary>
    public partial class Step2 : System.Web.UI.Page
    {
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
                InitMember(OpenId);
            }
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
                if (model.State == (int)Model.EnumType.会员状态.通过)
                    //this.btnSave.Visible = false;
                    this.btnSave.Visible = true;
            
                if (model.State == (int)Model.EnumType.会员状态.拒绝)
                    this.btnSave.Text = "重新提交认证";                
                txtContactName.Value = model.ContactName;
                txtPersonalId.Value = model.PersonalId;
                txtAddress.Value = model.Address;
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            string ContactName = Utils.GetFormValue("txtContactName");
            string PersonalId = Utils.GetFormValue("txtPersonalId");
            string Address = Utils.GetFormValue("txtAddress");
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (model.CountryId == 1 && model.ProvinceId != 190 && model.ProvinceId != 191 && model.ProvinceId != 988)
                {
                    if (!Utils.IsIDcard(PersonalId))
                    {
                        MessageBox.ShowAndReturnBack("请输入正确的身份证号码！");
                        return;
                    }
                }
                if (BMember.GetPesonalModel(PersonalId)!=null)
                {
                    MessageBox.ShowAndReturnBack("该身份证号码已注册，请使用其他身份证号码！");
                    return;
                }
                model.ContactName = ContactName;
                model.PersonalId = PersonalId;
                model.Address = Address;
                
                BMember.Update(model);
                BMember.Update(model.Id,Model.EnumType.会员状态.审核中);
                Response.Redirect("Step3.aspx", true);
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8));
            }
        }
    }
}