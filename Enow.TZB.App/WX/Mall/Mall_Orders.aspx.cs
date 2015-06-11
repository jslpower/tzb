using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Utility;
using System.Web.UI.HtmlControls;

namespace Enow.TZB.Web.WX.Mall
{
    public partial class Mall_Orders : System.Web.UI.Page
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        private string MemberId = "";
        private string MemberName = "";
        private string NickName = "";
        private string ChartId = "";
        private string MobilePhone = "";
        protected string CId = "", PId = "", CSId = "", AId = "", SiteID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            BMemberApp.LoginCheck();
            InitModel();
            if (InitList())
            {
                ChartId = Utils.GetQueryStringValue("chartid");
                string doType = Utils.GetQueryStringValue("doType");
                switch (doType)
                {
                    case "shop": InitOrder(); break;
                    case "total": InitOrderList(); break;
                }
            } 
           
        }
        public void Getchartid()
        {
                var charlist = Utils.GetStringArray(ChartId, ",");
                bool retbol = BShoppingChart.Getcharbool(charlist);
                if (!retbol)
                {
                    //未找到您的用户信息!
                    MessageBox.ShowAndRedirect("请先选择商品!", "Mall.aspx");
                    return;
                }
           
            
        }
        /// <summary>
        /// 加载地址信息
        /// </summary>
        private bool InitList()
        {
            var list = BSendAddress.GetMemberIdList(MemberId);
            if (list!=null&&list.Count() > 0)
            {
                this.Repeater1.DataSource = list;
                this.Repeater1.DataBind();
                return true;
            }
            else
            {
                MessageBox.ShowAndRedirect("请先填写收货地址!", "/WX/Member/AddressAdd.aspx");
                return false;
            }

        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        private void InitModel()
        {
            var MemberModel = BMemberApp.GetUserModel();
            if (MemberModel != null)
            {

                BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                MemberId = MemberModel.Id;
                MemberName = MemberModel.ContactName != null ? MemberModel.ContactName : "";
                NickName = MemberModel.NickName!=null?MemberModel.NickName:"";
                MobilePhone =MemberModel.MobilePhone!=null?MemberModel.MobilePhone.Trim():"";

                

            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
        /// <summary>
        /// 初始化订单列表
        /// </summary>
        private void InitOrderList()
        {
            if (string.IsNullOrEmpty(ChartId))
            {
                Getchartid();
            }
            order.Style.Add("display", "none");
            if (!string.IsNullOrEmpty(ChartId))
            {
                var list = BShoppingChart.GetShoppingList(ChartId);
                if (list.Count() > 0)
                {
                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                }
            }
            else
            {
                Utils.ShowAndRedirect("请先选择商品！", "Mall_Type.aspx");
            }
        }
        protected void HandleTotal(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                tbl_ShoppingChart model = (tbl_ShoppingChart)e.Item.DataItem;
                Literal litTotal = e.Item.FindControl("litTotal") as Literal;
                if (model!=null)
                {
                    if (!model.IsFreight)
                    {
                        litTotal.Text = ((model.GoodsFee * model.ShoppingNum) + model.FreightFee).ToString() + "(含运费¥" + model.FreightFee + ")";
                    }
                    else
                    {
                        litTotal.Text = (model.GoodsFee * model.ShoppingNum).ToString();
                    }
                }
                
            }
        }
        /// <summary>
        /// 初始化订单
        /// </summary>
        private void InitOrder()
        {
            string goodsid = Utils.GetQueryStringValue("gid");
            int num =Utils.GetInt(Utils.GetQueryStringValue("num"),1);
            tbl_MallGoods model = BMallGoods.GetModel(goodsid);
            if (model!=null)
            {
                //if (num>model.StockNum)
                //{
                //    MessageBox.ShowAndReturnBack("商品库存不足请重新选购！");
                //    return;
                //}
                litGoodsName.Text = model.GoodsName;
                litPrice.Text = model.MemberPrice.ToString();
                litShoppingNum.Text = num.ToString() ;
                if(model.IsFreight)
                {
                    litTotal.Text=model.MemberPrice.ToString();
                }
                else{
                    litTotal.Text = (model.MemberPrice + model.FreightFee).ToString() + "(含运费¥" + model.FreightFee + ")";
                }
            }
            else
            {
                MessageBox.ShowAndReturnBack("请选择商品！");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {


            string gid = Utils.GetQueryStringValue("gid");
            string doType = Request.QueryString["doType"];
           //用户基本信息
             var MemberModel = BMemberApp.GetUserModel();
             if (MemberModel != null)
             {
                 BMember.StateCheck((Model.EnumType.会员状态)MemberModel.State);
                 MemberId = MemberModel.Id;
                 MemberName = MemberModel.ContactName != null ? MemberModel.ContactName : "";
                 NickName = MemberModel.NickName != null ? MemberModel.NickName : "";
                 MobilePhone = MemberModel.MobilePhone != null ? MemberModel.MobilePhone.Trim() : "";
             }
             else
             {
                 //未找到您的用户信息!
                 MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                 return;
             }
            //物流编号
            string AdddressId = Utils.GetFormValue(hdfadderid.UniqueID);
            var Addressmodel = BSendAddress.GetSendIdUserIDModel(AdddressId, MemberId);
            if (Addressmodel == null)
            {
                MessageBox.Show("配送信息不正确!");
                return;
            }
            //订单Id
            string OrderId = Guid.NewGuid().ToString();
            //订单编号
            string OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            //物流编号
            string LogisticsNo = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string Operator = MemberName;
            #region 订单信息

            bool orderAdd = BMallOrders.Add(new tbl_MallOrder
            {
                OrderId = OrderId,
                OrderNo = OrderNo,
                PayStatus = (int)Model.商城订单状态.未支付,
                AddressId = AdddressId,
                CreatTime = DateTime.Now,
                MemberId = MemberId,
                MemberName = Operator,
                MobilePhone = MobilePhone,
                PayType = (int)Model.商城支付方式.APP支付,
                IsDelete = false
            }
            );
            #endregion
            if (doType == "shop")
            {
                //#region 配送信息
                //bool addressAdd = false;
                ////判断该用户是否创建过默认地址
                //tbl_SendAddress adds = BSendAddress.GetDefaultAddress(MemberId);
                //if (adds != null)
                //{
                //    addressAdd = true;
                //    AdddressId = adds.Id;
                //}
                //else
                //{
                //    addressAdd = BSendAddress.Add(new tbl_SendAddress
                //    {
                //        Id = AdddressId,
                //        LogisticsNo = LogisticsNo,
                //        Recipient = Recipient,
                //        MobilePhone = txtMobile.Text,
                //        Telephone = "0",
                //        CountyId = CountryId,
                //        CountyName = CountryName,
                //        ProvinceId = ProvinceId,
                //        ProvinceName = ProvinceName,
                //        CityId = CityId,
                //        CityName = CityName,
                //        AreaId = AreaId,
                //        AreaName = AreaName,
                //        Address = txtAddress.Text,
                //        IsDefaultAddress = IsDefaultAddress,
                //        MemberId = MemberId,
                //        IsDelete = 0
                //    });
                //}
                //#endregion
                //#region 订单信息

                //bool orderAdd = BMallOrders.Add(new tbl_MallOrder
                //{
                //    OrderId = OrderId,
                //    OrderNo = OrderNo,
                //    PayStatus = (int)Model.商城订单状态.待审核,
                //    AddressId = AdddressId,
                //    CreatTime = DateTime.Now,
                //    MemberId = MemberId,
                //    MemberName = Operator,
                //    MobilePhone = MobilePhone,
                //    PayType = (int)Model.商城支付方式.微信支付,
                //    IsDelete = false
                //}
                //);
                //#endregion
                #region 购物车信息
                tbl_MallGoods goods = BMallGoods.GetModel(gid);
                bool IsFreight = false;
                decimal GoodsFee = 0;
                decimal? FreightFee = 0;
                string GoodsPhoto = "";
                if (goods != null)
                {
                    IsFreight = goods.IsFreight;
                    GoodsFee = 1 * goods.MemberPrice;
                    FreightFee = goods.IsFreight == false ? goods.FreightFee : 0;
                    GoodsPhoto = goods.GoodsPhoto;
                }
                int num = Utils.GetInt(Utils.GetQueryStringValue("num"), 1);
                bool chartAdd = BShoppingChart.Add(
                    new tbl_ShoppingChart
                    {
                        Id = Guid.NewGuid().ToString(),
                        GoodsId = gid,
                        GoodsName = goods.GoodsName,
                        ShoppingNum = num,
                        PayType = (int)Model.商城支付方式.APP支付,
                        JoinTime = DateTime.Now,
                        IsFreight = IsFreight,
                        GoodsFee = GoodsFee,
                        FreightFee = FreightFee,
                        GoodsPhoto = GoodsPhoto,
                        MemberId = MemberId,
                        MemberName = MemberName,
                        NickName = NickName,
                        OrderId = OrderId
                    }
                  );
                if ( orderAdd && chartAdd)
                {

                    BMallGoods.UpdateSellNum(new tbl_MallGoods
                    {
                        Id = gid,
                        SellNum = 1
                    });
                    MessageBox.ShowAndRedirect("订单提交成功!", "/WX/Order/Pay.aspx?OrderId=" + OrderId);

                }
                else
                {
                    MessageBox.ShowAndReturnBack("订单提交失败!");
                }
                #endregion
            }
            else
            {
                //bool addAdd = false;
                ////判断该用户是否创建过默认地址
                //List<tbl_SendAddress> adds = BSendAddress.GetDefaultAddress(MemberId, true);
                //if (adds != null && adds.Count > 0)
                //{
                //    addAdd = true;
                //    AdddressId = adds[0].Id;
                //}
                //else
                //{
                //    addAdd = BSendAddress.Add(new tbl_SendAddress
                //        {
                //            Id = AdddressId,
                //            LogisticsNo = LogisticsNo,
                //            Recipient = Recipient,
                //            MobilePhone = txtMobile.Text,
                //            Telephone = "0",
                //            CountyId = CountryId,
                //            CountyName = CountryName,
                //            ProvinceId = ProvinceId,
                //            ProvinceName = ProvinceName,
                //            CityId = CityId,
                //            CityName = CityName,
                //            AreaId = AreaId,
                //            AreaName = AreaName,
                //            Address = txtAddress.Text,
                //            IsDefaultAddress = IsDefaultAddress,
                //            MemberId = MemberId,
                //            IsDelete = 0
                //        });
                //}
                //bool orderAdd = BMallOrders.Add(new tbl_MallOrder
                // {
                //     OrderId = OrderId,
                //     OrderNo = OrderNo,
                //     PayStatus = (int)Model.商城订单状态.待审核,
                //     AddressId = AdddressId,
                //     CreatTime = DateTime.Now,
                //     MemberId = MemberId,
                //     MemberName = Operator,
                //     MobilePhone = MobilePhone,
                //     PayType = (int)Model.商城支付方式.微信支付,
                //     IsDelete = false

                // });
                var charidlist = Utils.GetStringArray(ChartId, ",");
                bool chartAdd = BShoppingChart.UpdateCharModel(charidlist, OrderId);
                if (orderAdd && chartAdd)
                {
                    for (int s = 0; s < charidlist.Count; s++)
                    {
                        tbl_ShoppingChart model = BShoppingChart.GetModel(charidlist[s]);
                        if (model != null)
                        {
                            BMallGoods.UpdateSellNum(new tbl_MallGoods
                            {
                                Id = model.GoodsId,
                                SellNum = model.ShoppingNum
                            });
                        }

                    }
                    MessageBox.ShowAndRedirect("订单提交成功!", "/WX/Order/Pay.aspx?OrderId=" + OrderId);
                }
                else
                {
                    MessageBox.ShowAndReturnBack("订单提交失败!");
                }


            }
        }
    }
}