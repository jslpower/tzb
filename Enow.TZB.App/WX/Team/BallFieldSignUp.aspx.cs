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
    public partial class BallFieldSignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Gethuiyuan();
                Initload();
            }
        }
        private void Gethuiyuan()
        {
            BMemberApp.LoginCheck();
            InitMember();
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
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
            }
            else
            {
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Initload()
        {
            string AcId = Utils.GetQueryStringValue("QID");
            if (!string.IsNullOrEmpty(AcId))
            {
               tbl_BallField balmodel=BBallField.GetModel(AcId);
               if (balmodel != null)
                   txtcd.Text = balmodel.FieldName;
            }
            else
            {
                MessageBox.ShowAndRedirect("请先选择场地!", "BallField.aspx");
                return;
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string AcId = Utils.GetQueryStringValue("QID");
            if (string.IsNullOrEmpty(AcId))
            {
                MessageBox.ShowAndRedirect("请先选择场地!", "BallField.aspx");
                return;
            }
            var AuthModel = BMemberApp.GetUserModel();
            if (AuthModel == null)
            {
                MessageBox.ShowAndRedirect("请先选择场地!", BMemberApp.RespUrl);
                return;
            }
            var applic = new tbl_Applicants();
            applic.IsDelete = 0;
            applic.IsState = 0;
            applic.Number = Utils.InputText(Utils.GetFormEditorValue(txtrenshu.UniqueID));
            applic.Contracttime = Utils.InputText(Utils.GetFormEditorValue(txttime.UniqueID));
            applic.Remarks = Utils.InputText(Utils.GetFormEditorValue("txtbeizhu"));
            tbl_Applicants retbol = BApplicants.GetUsCdbool(AcId, AuthModel.Id);
            if (retbol != null)
            {
                if (retbol.IsDelete == 0)
                {
                    MessageBox.ShowAndRedirect("您已预约此场地请等待管理员审核!", "/WX/Member/SiteList.aspx");
                    return;
                }
                else
                {
                    applic.Id = retbol.Id;
                    applic.Indatetime = DateTime.Now;
                    bool upbol = BApplicants.Update(applic);
                    MessageBox.ShowAndRedirect(upbol?"报名成功!":"报名失败！请刷新后重试！", "BallField.aspx?QID=" + AcId);
                   
                }
            }
            else
            {

                applic.ActivityID = AcId;
                applic.Usid = AuthModel.Id;
                applic.Indatetime = DateTime.Now;
                BApplicants.Add(applic);
                MessageBox.ShowAndRedirect("报名成功!", "/WX/Member/SiteList.aspx");
            }
        }
    }
}