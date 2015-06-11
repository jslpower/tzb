using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;
using System.Text;

namespace Enow.TZB.Web.Manage.Videocode
{
    public partial class CodeAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Gettypevlue();
            }
        }
        public void Gettypevlue()
        {
            var list = EnumObj.GetList(typeof(Model.EnumType.VideoCodeEnum));
            droptype.DataSource = list;
            droptype.DataBind();
        }
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            var types = Utils.GetInt(droptype.SelectedValue, 1);//分类
            var qianz = Utils.InputText(txtTypeName.Text);//前缀
            var tst = Utils.GetInt(txtsteat.Text, 0);//前区间码
            var tet = Utils.GetInt(txtend.Text, 1);//后区间码
            StringBuilder str = new StringBuilder();
            str.Append("<root>");
            for (int i = tst; i <= tet; i++)
            {
                if (Bcode.GetCodebool((qianz + (i.ToString("D5"))), types))
                {
                    //codelist.Add(new tbl_CodelModel() { Codetype = types, Codenum = qianz + (i.ToString("D5")), Codestate = 0, IsDelete=1 });
                    str.AppendFormat("<info Codetype=\"{0}\" Codenum=\"{1}\" Codestate=\"0\"  Usid=\"\" Usnc=\"\" Usname=\"\" Ustel=\"\" IsDelete=\"1\" />", types, (qianz + (i.ToString("D5"))));
                }
            }
            str.Append("</root>");
            bool ret = Bcode.Add(str.ToString());
            if (ret)
            {
                MessageBox.ShowAndParentReload("生成成功！");
            }
        }
    }
}