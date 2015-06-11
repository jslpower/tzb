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
    public partial class Step3 : System.Web.UI.Page
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
            string CSWZ = Utils.GetFormValue("txtCSWZ");
            string CYQY = Utils.GetFormValue("txtCYQY");
            string CYZB = Utils.GetFormValue("txtCYZB");
            string MZTQS = Utils.GetFormValue("txtMZTQS");
            string GZQD = Utils.GetFormValue("txtGZQD");
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                BMemberInterest.Update(new tbl_MemberInterest {
                    MemberId = model.Id,
                    CSWZ = CSWZ,
                    CYQYH = CYQY,
                    CYZBPP = CYZB,
                    MZTQS = MZTQS,
                    GZQD = GZQD
                });
                //重写登陆凭证
                MessageBox.ShowAndRedirect("注册成功！", "/WX/Member/Default.aspx");
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8));
            }
        }
    }
}