using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Member
{
    public partial class AddressList : System.Web.UI.Page
    {
        public string Typetitle = "";
        /// <summary>
        /// 每页条数
        /// </summary>
        protected int intPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WxPageSize"]);
        /// <summary>
        /// 当前页号
        /// </summary>
        protected int CurrencyPage = 1;
        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            var ation = Utils.GetQueryStringValue("ation").ToLower().Trim();
            switch (ation)
            {
                case "delete":
                    DeleteAdres();
                    break;
            }
            if (!IsPostBack)
            {
                Getusermodel();
                InitList();
            }
        }
        /// <summary>
        /// 查询登录状态
        /// </summary>
        private void Getusermodel()
        {
            BMemberAuth.LoginCheck();
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            InitMember(OpenId);
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
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        /// <summary>
        /// 加载商品列表
        /// </summary>
        private void InitList()
        {
            int rowCounts = 0;
            string Page = Request.QueryString["Page"];
            if (!string.IsNullOrEmpty(Page) && StringValidate.IsInteger(Page))
            {
                int.TryParse(Page, out CurrencyPage);
                if (CurrencyPage < 1)
                {
                    CurrencyPage = 1;
                }
            }
            tbl_SendAddress SearchModel = new tbl_SendAddress();
            SearchModel.MemberId = MemberId;
            var list =BSendAddress.GetList(ref rowCounts, 100, CurrencyPage, SearchModel);
            if (list.Count() > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }

        }
        private void DeleteAdres()
        {
            var Id = Utils.GetQueryStringValue("AdresId");
            if (!string.IsNullOrEmpty(Id))
            {
                var AuthModel = BMemberAuth.GetUserModel();
                if (AuthModel!=null)
                {
                    var model = BMember.GetModelByOpenId(AuthModel.OpenId);
                    if (model!=null)
                    {
                        MemberId = model.Id;
                    }
                    else
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请先登录！"));
                    }
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请先登录！"));
                }
                bool retdelete = BSendAddress.Deletebol(Id, MemberId);
                if (retdelete)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1","删除成功！"));
                }
                else
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功！"));
                }
            }
        }
    }
}