using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.WebForm.News
{
    public partial class BDcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void butclisk_Click(object sender, EventArgs e)
        {
            var model = BWebMemberAuth.GetUserModel();
            if (model != null)
            {
                if (Bcode.Getcodeky(Utils.GetFormValue(txtcode.UniqueID),1))
                {
                    tbl_CodelModel codemodel = new tbl_CodelModel();
                    codemodel.Codenum = Utils.GetFormValue(txtcode.UniqueID);
                    codemodel.Codestate = 1;
                    codemodel.Usid = model.Id;
                    codemodel.Usname = model.ContactName;
                    codemodel.Usnc = model.NickName;
                    codemodel.Ustel = model.MobilePhone;
                    bool retbol = Bcode.UpdateIDmodel(codemodel);
                    if (retbol)
                    {
                        MessageBox.ShowAndRedirect("绑定成功！", "TieShare.aspx?ClassId=100");
                    }  
                }
                else
                {
                    MessageBox.ShowAndRedirect("视频码不存在或已绑定！", "BDcode.aspx");
                }
                
            }
            else
            {
                MessageBox.ShowAndRedirect("请先登录！", "/Default.aspx");
            }
        }
    }
}