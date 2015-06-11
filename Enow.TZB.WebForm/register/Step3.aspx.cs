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
    public partial class Step3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Id = Utils.GetQueryStringValue("id");

            string QCWZ = Utils.GetFormValue(txtQCWZ.UniqueID);
            string QYHM = Utils.GetFormValue(txtQYHM.UniqueID);
            string ZBPP = Utils.GetFormValue(txtZBPP.UniqueID);
            string MZCS = Utils.GetFormValue(txtMZCS.UniqueID);
            string GZQD = Utils.GetFormValue(txtGZQD.UniqueID);
            var model = BMember.GetModel(Id);
            if (model != null)
            {
                BMemberInterest.Add(new tbl_MemberInterest
                {
                    Id = Guid.NewGuid().ToString(),
                    MemberId = model.Id,
                    CYQYH=QYHM,
                     CSWZ=QCWZ,
                     CYZBPP=ZBPP,
                     MZTQS=MZCS,
                     GZQD=GZQD,
                     State=1,
                     LastModifyTime=DateTime.Now,
                     IssueTime=DateTime.Now

                });
                Response.Redirect("RegSuccess.aspx", true);
            }
            else
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8));
            }

        }
    }
}