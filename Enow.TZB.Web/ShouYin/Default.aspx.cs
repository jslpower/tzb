using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.ShouYin
{
    /// <summary>
    /// 收银端首页
    /// </summary>
    public partial class Default : ShouYinYeMian
    {
        #region attributes
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Utility.Utils.GetQueryStringValue("dotype"))
            {
                case "gethuojiashangpin": GetHuoJiaShangPin(); break;
                case "getshangpinmingxi": GetShangPinMingXi(); break;
                default: break;
            }
        }

        #region private members
        /// <summary>
        /// get huojia shangpin
        /// </summary>
        void GetHuoJiaShangPin()
        {
            int recordCount = 0;
            var chaXun = new Model.MGoodsSearch();

            chaXun.Status = Model.商品上架状态.上架;
            chaXun.BallFieldId = YongHuInfo.FieldId;
           chaXun.MaxThenType = 1;
            var list = BGoodsType.GetTypeList();
            StringBuilder s = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                
                foreach (var lst in list)
                {
                    chaXun.TypeId = lst.ID;
                    var items = Enow.TZB.BLL.BGoods.GetViewList(ref recordCount, 10000, 1, chaXun);

                    if (items != null && items.Count > 0)
                    {
                        int i = 0;

                        foreach (var item in items)
                        {
                            if (i > 0 && i % 4 == 0) s.Append("</tr>");
                            if (i % 4 == 0) s.Append("<tr>");

                            s.AppendFormat("<td><a href=\"javascript:void(0)\" data-shangpinid=\"{0}\" data-class=\"shangpin\">{1}</a></td>", item.ID, item.GoodsName);

                            i++;
                        }

                        if (i % 4 > 0)
                        {
                            for (var j = 0; j < 4 - i % 4; j++)
                            {
                                s.Append("<td>&nbsp;</td>");
                            }
                        }

                        s.Append("</tr>");

                    }
                   
                }
            }
            else
            {
                s.AppendFormat("<tr><td>暂无商品</td></tr>");
            }

            RCWE(Utility.UtilsCommons.AjaxReturnJson("1", "", s.ToString()));
        }

        /// <summary>
        /// get shangpin mingxi
        /// </summary>
        void GetShangPinMingXi()
        {
            int shangPinId = Utility.Utils.GetInt(Utility.Utils.GetFormValue("txtShangPinId"));
            var info = Enow.TZB.BLL.BGoods.GetModel(shangPinId);

            if (info == null)
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("0", "暂无该商品信息"));
            }
            else
            {
                RCWE(Utility.UtilsCommons.AjaxReturnJson("1", "", info));
            }
        }
        #endregion
    }
}