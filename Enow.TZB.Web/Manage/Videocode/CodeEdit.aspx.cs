using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Videocode
{
    public partial class CodeEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                Getuslist();
                Getcode();
                
            }
        }
        /// <summary>
        /// 错误提醒
        /// </summary>
        private void ErrMsg()
        {
            MessageBox.ShowAndBoxClose("未找到您要修改的信息!", Request.QueryString["iframeId"]);
            return;
        }
        public void Getuslist()
        {
            List<dt_MemberView> list = Bcode.Getuslist();
            if (list.Count>0)
            {
                list.Insert(0, new dt_MemberView() { Id = "0", ContactName="未选择" });
            }
            dropusname.DataSource = list;
            dropusname.DataBind();
        }
        public void Getcode()
        {
            var id =Utils.GetInt(Utils.GetQueryStringValue("id"),0);
            if (id!=0)
            {
                tbl_CodelModel codemodel= Bcode.GetModel(id);
                if (codemodel!=null)
                {
                    litecode.Text = codemodel.Codenum;
                    if (!string.IsNullOrEmpty(codemodel.Usid))
                    {
                        dropusname.SelectedValue = codemodel.Usid;
                    }
                }
            }
            else
            {
                ErrMsg();
            }
            
        }

        /// <summary>
        /// 保存资讯类别信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            var id = Utils.GetInt(Utils.GetQueryStringValue("id"), 0);
            var usid = Utils.InputText(Utils.GetFormValue(dropusname.UniqueID));
            if (!string.IsNullOrEmpty(usid)&& usid!="0")
            {
                tbl_Member usmodel = BMember.GetModel(usid);
                if (usmodel!=null)
                {
                    tbl_CodelModel codemodel = new tbl_CodelModel();
                    codemodel.id = id;
                    codemodel.Codestate = 1;
                    codemodel.Usid = usid;
                    codemodel.Usname = usmodel.ContactName;
                    codemodel.Usnc = usmodel.NickName;
                    codemodel.Ustel = usmodel.MobilePhone;
                    bool retbol = Bcode.Update(codemodel);
                    if (retbol)
                    {
                        MessageBox.ShowAndParentReload("绑定成功！");
                    }  
                }
                else
                {
                    MessageBox.ShowAndParentReload("请选择要绑定的用户！");
                }
              
            }
            else
            {
                MessageBox.ShowAndParentReload("请选择要绑定的用户！");
            }
            
            
            
            


        }
    }
}