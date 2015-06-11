using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX
{
    public partial class InitMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WeiXinResult rv = BWeiXin.CreateMenu();
                if (rv.IsResult)
                {
                    Response.Write("操作成功：已成功初始化菜单！");
                }
                else
                {
                    Response.Write("操作失败：" + rv.ResultMsg + "！");
                }
            }
        }
    }
}