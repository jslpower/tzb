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
    public partial class SiteSet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Master.Page.Title = "偏好设置";
                BWebMemberAuth.LoginCheck();
                InitPage(); 
            }
        }

        private void InitPage()
        {
            var model = BWebMemberAuth.GetUserModel();
            if (model!=null)
            {
                var Imodel = BMemberInterest.GetModel(model.Id);
                if (Imodel!=null)
                {
                   txtQCWZ.Text = Imodel.CSWZ;
                    txtQYHM.Text= Imodel.CYQYH;
                    txtZBPP.Text = Imodel.CYZBPP;
                    txtMZCS.Text = Imodel.MZTQS;
                    txtGZQD.Text = Imodel.GZQD;
                }

            }
        
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 取值
            var model = BWebMemberAuth.GetUserModel();
            string MemberId = model.Id;
            string poistion = Utils.GetFormValue(txtQCWZ.UniqueID);
            string number = Utils.GetFormValue(txtQYHM.UniqueID);
            string shirtBrand = Utils.GetFormValue(txtZBPP.UniqueID);
            string WeekCount = Utils.GetFormValue(txtMZCS.UniqueID);
            string attentionTeam = Utils.GetFormValue(txtGZQD.UniqueID);
            #endregion

            #region

            var Mmodel = BMemberInterest.GetModel(MemberId);
            if (Mmodel == null)
            {
                BMemberInterest.Add(new tbl_MemberInterest
                {
                    Id=Guid.NewGuid().ToString(),
                    MemberId = MemberId,
                    CSWZ = poistion,
                    CYQYH = number,
                    CYZBPP = shirtBrand,
                    MZTQS = WeekCount,
                    GZQD = attentionTeam,
                    State=1,
                    LastModifyTime = DateTime.Now,
                    IssueTime = DateTime.Now

                });
                Response.Redirect("Default.aspx", true);
                return;
            }
            else
            {
                string Id = Mmodel.Id;
                BMemberInterest.Update(new tbl_MemberInterest { 
                Id=Id,
                MemberId = MemberId,
                CSWZ = poistion,
                CYQYH = number,
                CYZBPP = shirtBrand,
                MZTQS = WeekCount,
                GZQD = attentionTeam,
                LastModifyTime = DateTime.Now
                });
                MessageBox.ShowAndRedirect("操作成功", "Default.aspx");

            }

            #endregion

        }


    }
}