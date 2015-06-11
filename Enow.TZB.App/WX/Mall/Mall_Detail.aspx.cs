using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.WX.Mall
{
    public partial class Mall_Detail : System.Web.UI.Page
    {
        public string detitle = "";
        protected string GoodsId = "";
        /// <summary>
        /// 会员编号
        /// </summary>
        private string MemberId = "";
        private string MemberName = "";
        private string NickName = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
               
            
               
                string goodsid = Request.QueryString["id"];
                if (!String.IsNullOrWhiteSpace(goodsid))
                {
                    GoodsId = goodsid;
                    InitPage(goodsid);
                  
                }
                else
                {
                    //未找到您要查看的信息！
                    MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(34));
                }
            }
            string doType = Request.QueryString["doType"];
            switch (doType)
            {
                case "join": JoinShoppingChart(); break;
            }

        }
        /// <summary>
        /// 加入购物车
        /// </summary>
        public void JoinShoppingChart()
        {
           string gid = Utils.GetQueryStringValue("id");
           bool retfh= InitModel();
           if (!retfh)
           {
               Utils.RCWE(UtilsCommons.AjaxReturnJson("-99", "请先登录！"));
           }
           int shoppingNum=0;
     
           if (!Utility.StringValidate.IsInteger(Utils.GetQueryStringValue("num")))
           {
               //MessageBox.ShowAndRedirect("请输入数字！", "Mall_Detail.aspx?doType=join&gid="+gid+"");
               Utils.RCWE(UtilsCommons.AjaxReturnJson("-99", "请输入数字！"));
           }
           else
           {
               shoppingNum = int.Parse(Utils.GetQueryStringValue("num"));
           }
           if (shoppingNum<=0)
           {
               Utils.RCWE(UtilsCommons.AjaxReturnJson("-99", "购物数量应大于0！"));
           }
           tbl_MallGoods goods=BMallGoods.GetModel(gid);
           bool IsFreight=false;
           decimal GoodsFee=0;
           decimal? FreightFee=0;
           string GoodsPhoto="";
           if(goods!=null)
           {
               IsFreight=goods.IsFreight ;
               GoodsFee=goods.MemberPrice;
               FreightFee=goods.IsFreight==false?goods.FreightFee:0;
               GoodsPhoto=goods.GoodsPhoto;
           }
           //if (shoppingNum>goods.StockNum)
           //{
           //     Utils.RCWE(UtilsCommons.AjaxReturnJson("-99", "库存不足！请等待管理员添加！"));
           //}
           int bllRetCode = BShoppingChart.Add(
               new tbl_ShoppingChart
               {
                   Id=Guid.NewGuid().ToString(),
                   GoodsId=gid,
                   GoodsName = goods.GoodsName,
                   ShoppingNum=shoppingNum,
                   PayType = (int)Model.商城订单状态.未支付,
                   JoinTime=DateTime.Now,
                   IsFreight=IsFreight,
                   GoodsFee=GoodsFee,
                   FreightFee = FreightFee,
                   GoodsPhoto=GoodsPhoto,
                   OrderId="",
                   MemberId=MemberId,
                   MemberName=MemberName,
                   NickName=NickName

               }
               ) == true ? 1 : -99;

            if (bllRetCode == 1)
            {
               
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "成功加入购物车！"));
            }
            else
            {
                //MessageBox.ShowAndRedirect("", "Articles.aspx"); 
                Utils.RCWE(UtilsCommons.AjaxReturnJson("-99", "加入购物车失败！"));
            }
        }
        /// <summary>
        /// 初始化商品详情
        /// </summary>
        /// <param name="articleid"></param>
        private void InitPage(string goodsid)
        {
            var model = BMallGoods.GetModel(goodsid);
            if (model != null)
            {
                if (model.GoodsClassId!=((int)(Enow.TZB.Model.商品分类.爱心义卖)))
                {
                   Literal1.Text="<a href=\"#\" onclick=\"PageJsDataObj.JoinChart('"+model.Id+"')\"  class=\"basic_ybtn floatR mr5\">加入购物车</a>";
                }
                detitle = model.GoodsName;
                if (!String.IsNullOrEmpty(model.GoodsPhoto))
                {
                    this.ltrImg.Text = "<img src=\"" +System.Configuration.ConfigurationManager.AppSettings["Uploadimgurl"]+ model.GoodsPhoto + "\" width=\"100%\" />";
                }
                ltrGoodsName.Text = model.GoodsName ;
                ltrMarkerPrice.Text = model.MarketPrice.ToString();
                ltrSellNum.Text = model.SellNum.ToString();
                ltrMemberPrice.Text = model.MemberPrice.ToString();
                if (!String.IsNullOrEmpty(model.Standard))
                {
                    ltrStandard.Text = model.Standard;
                }
                if (!String.IsNullOrEmpty(model.StandardInfo))
                {
                    ltrStandardInfo.Text = "<a href=\"javascript:void(0);\" class=\"on\">" + model.StandardInfo + "</a>";
                }

                if(!String.IsNullOrEmpty(model.GoodsIntroduce))
                { 
                    divStandardInfo.InnerHtml=model.GoodsIntroduce;
                  
                }

            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        private bool InitModel()
        {
            var MemberModel = BMemberApp.GetUserModel();
            if (MemberModel != null)
            {

                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                MemberId = MemberModel.Id;
                MemberName = MemberModel.ContactName;
                NickName = MemberModel.NickName;
                return true;
            }
            else
            {
                //未找到您的用户信息!
              
                return false;
            }
        }
     
    }
}