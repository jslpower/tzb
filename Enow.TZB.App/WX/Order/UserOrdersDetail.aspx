<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserOrdersDetail.aspx.cs" Inherits="Enow.TZB.Web.WX.Order.UserOrdersDetail" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>我的订单</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/user.css" type="text/css" media="screen"/>


</head>
<body>
   
    <form id="form1" runat="server">
     <uc1:UserHome ID="UserHome1" Userhometitle="我的订单" runat="server" />
  <div class="user_warp">

    <div class="user-state">
       <p>交易状态:<asp:Literal ID="litljyzt" runat="server"></asp:Literal></p>
       <p>订单金额：¥ <%=allMoney %></p>
    </div>
    
    <div class="user-address">收货地址：<asp:Literal ID="litAddressInfo" runat="server"></asp:Literal></div>
    
    <div class="u-dindan-list u-dindan-xx mt10">
      <ul>
      <asp:Repeater ID="rptList" runat="server">
              <ItemTemplate>
         <div class="user-item font14"><%#Eval("GoodsName")%></div>
         <div class="u-dindan-item">
                   <img alt="" src="<%#Eval("GoodsPhoto")%>"/>
                   <p class="font_gray"><%#Eval("JoinTime", "{0:yyyy-MM-dd}")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;共<%#Eval("ShoppingNum")%>件商品 </p>
                   <p>实付：¥<%#Enow.TZB.Utility.Utils.GetDecimal(Eval("GoodsFee").ToString()) * Enow.TZB.Utility.Utils.GetDecimal(Eval("ShoppingNum").ToString()) + Enow.TZB.Utility.Utils.GetDecimal(Eval("FreightFee").ToString())%>(含运费¥ <%#Enow.TZB.Utility.Utils.GetDecimal(Eval("FreightFee").ToString())%>元)</p>
                   <p class="font_gray">支付方式：<%#(Enow.TZB.Model.商城支付方式)Convert.ToInt32(Eval("PayType"))%></p>
         </div>
          </ItemTemplate>
           </asp:Repeater>
         </ul>
         <div class="paddL10 paddT10">商品订单号：  <asp:Literal ID="litOrderNo" runat="server"></asp:Literal></div>
         <asp:Literal ID="litaddressNo" runat="server"></asp:Literal>
         <div class="paddL10 paddT10">　联系电话：<asp:Literal ID="litMobile" runat="server"></asp:Literal></div>
         <div class="paddT10 paddR10"></div>
    </div>



</div>

    </form>
</body>
</html>
