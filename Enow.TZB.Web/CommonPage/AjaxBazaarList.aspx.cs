using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.App.CommonPage
{
    public partial class AjaxBazaarList : System.Web.UI.Page
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        //图片裁剪后保存的文件夹
        protected const string DIRPATH = "/ufiles/";
        /// <summary>
        /// 会员ID
        /// </summary>
        private string MemberId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var AuthModel = BMemberAuth.GetUserModel();
                if (AuthModel != null)
                {
                    string OpenId = AuthModel.OpenId;
                    InitMember(OpenId);
                }
                else
                {
                    Response.End();
                }
                InitList();
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember(string OpenId)
        {
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    //您未填写实名信息,\n请补充填写！
                    Response.End();
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
            }
            else
            {
                Response.End();
            }
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        private void InitList()
        {
            MMallGoodsSearch searchModel = new MMallGoodsSearch();

            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            string name = Utils.GetQueryStringValue("kewords");
            if (!string.IsNullOrWhiteSpace(name))
            {
                searchModel.GoodsName = name;
            }
            searchModel.TypeId = (int)Enow.TZB.Model.商品分类.爱心义卖;
            searchModel.MemberId = MemberId;
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }

            var list = BMallGoods.GetMemberViewList(ref rowCounts, intPageSize, CurrencyPage, searchModel);
            if (rowCounts > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
        }
    }
}