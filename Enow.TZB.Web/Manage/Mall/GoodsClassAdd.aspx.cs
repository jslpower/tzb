using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Mall
{
    public partial class GoodsClassAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                Getclass();
                InitLoad();
               
            }
        }
        private void InitLoad()
        {
            var query = Utils.GetInt(Utils.GetQueryStringValue("ClassID"), 0);
            if (query != 0)
            {
                var retmodel= BRoleClass.GetModel(query);
                if (retmodel!=null)
                {
                    dropyjclass.SelectedValue = retmodel.Type.ToString();
                    txtTypeName.Text = retmodel.Rolename;
                }
            }
        }
        /// <summary>
        /// 二级类别
        /// </summary>
        private void Getclass()
        {
      
            var fblist = new List<EnumObj>();
            fblist.Add(new EnumObj { Value = "1", Text = "爱心义卖" });
            fblist.Add(new EnumObj { Value = "2", Text = "公益拍卖" });
            fblist.Add(new EnumObj { Value = "3", Text = "培训课程" });
            fblist.Add(new EnumObj { Value = "4", Text = "足球旅游" });
            fblist.Add(new EnumObj { Value = "5", Text = "铁丝购" });
            dropyjclass.DataSource = fblist;
            dropyjclass.DataBind();
        }
        /// <summary>
        /// 新增商品类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            var query =Utils.GetInt(Utils.GetQueryStringValue("ClassID"),0);
            if (query==0)
            {
                string StrTypeName = Utils.GetFormValue(txtTypeName.UniqueID);
                int types = Utils.GetInt(Utils.GetFormValue(dropyjclass.UniqueID), 5);
                BRoleClass.Add(new tbl_GoodsRoleClass()
                {
                    Type = types,
                    Rolename = StrTypeName
                });
                MessageBox.ShowAndParentReload("商品类别添加成功！");
                return;
            }
            else
            {
                string StrTypeName = Utils.GetFormValue(txtTypeName.UniqueID);
                int types = Utils.GetInt(Utils.GetFormValue(dropyjclass.UniqueID), 3);
                BRoleClass.Update(new tbl_GoodsRoleClass()
                {
                    Id=query,
                    Type = types,
                    Rolename = StrTypeName
                });
                MessageBox.ShowAndParentReload("商品类别添加成功！");
                return;
            }
            

        }
    }
}