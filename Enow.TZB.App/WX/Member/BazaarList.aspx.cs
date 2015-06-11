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

namespace Enow.TZB.App.WX.Member
{
    public partial class BazaarList : System.Web.UI.Page
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
            BMemberApp.LoginCheck();
            InitMember();
            string ation = Utils.GetQueryStringValue("ation");
            switch (ation)
            {
                case "delgod":
                    Delgod();
                    break;
            }
            if (!IsPostBack)
            {
                InitList();
            }
        }
        private void Delgod()
        {
            string id = Utils.GetQueryStringValue("Id");
            if (string.IsNullOrEmpty(id))
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "删除对象不能为空！"));
            if (BMallGoods.DeleteMemberGod(id, MemberId))
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功！"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "删除失败！"));
            }

        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        private void InitMember()
        {
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
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
            //string name = Utils.GetFormValue(txtKeyWord.UniqueID);
            //if (!string.IsNullOrWhiteSpace(name))
            //{
            //    searchModel.GoodsName = name;
            //}
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
            else
            {
                PlaceHolder1.Visible = false;
            }
        }
         /// <summary>
        /// 查询
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            InitList();
        }
    }
}