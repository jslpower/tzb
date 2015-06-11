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
                BMemberApp.LoginCheck();
                InitMember();
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember()
        {
            var model = BMemberApp.GetUserModel();
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
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8),BMemberApp.RespUrl);
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
            string ContactName = Utils.GetFormValue("txtContactName");
            string PersonalId = Utils.GetFormValue("txtPersonalId");
            string Address = Utils.GetFormValue("txtAddress");
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                var wzmodel= BMember.GetModel(model.Id);
                if (wzmodel==null)
                {
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8));
                    return;
                }
                if (wzmodel.CountryId == 1 && wzmodel.ProvinceId != 190 && wzmodel.ProvinceId != 191 && model.ProvinceId != 988)
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
                wzmodel.ContactName = ContactName;
                wzmodel.PersonalId = PersonalId;
                wzmodel.Address = Address;

                BMember.Update(wzmodel);
                BMember.Update(wzmodel.Id, Model.EnumType.会员状态.审核中);
                BMemberApp.UserLogin(wzmodel.Id);
                Response.Redirect("Step3.aspx", true);
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8));
            }
        }
    }
}