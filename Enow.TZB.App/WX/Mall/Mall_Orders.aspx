<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mall_Orders.aspx.cs" Inherits="Enow.TZB.Web.WX.Mall.Mall_Orders" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>产品确认</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/mall.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/address.css" type="text/css" media="screen" />
<script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="/Js/CitySelect.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hdfadderid"/>
    <uc1:UserHome ID="UserHome1" Userhometitle="产品确认" runat="server" />


<div class="user_warp">
 
       <div class="mall_title">订单信息</div>
    
       <div id="order" class="cont gray_lineB" runat="server">
            <p class="font14 paddB10">
                <asp:Literal ID="litGoodsName" runat="server"></asp:Literal> </p>
            <p>单价：¥ <asp:Literal ID="litPrice" runat="server"></asp:Literal></p>
            <p>数量：<asp:Literal ID="litShoppingNum" runat="server"></asp:Literal></p>
            <p>总额：<span class="font_yellow">¥<i class="font18"><asp:Literal ID="litTotal" runat="server"></asp:Literal></i></span></p>
       </div>
       <ul>
             <asp:Repeater ID="rptList" runat="server" onitemdatabound="HandleTotal" >
               <ItemTemplate>
               <div class="cont gray_lineB">
                <p class="font14 paddB10"><%#Eval("GoodsName")%></p>
                <p>单价：¥<%#Eval("GoodsFee")%></p>
                <p>数量：<%#Eval("ShoppingNum")%></p>
                <p>总额：<span class="font_yellow">¥<i class="font18"><asp:Literal ID="litTotal" runat="server"></asp:Literal></i></span></p>
       </div>
            </ItemTemplate>
            </asp:Repeater>
       </ul>
       <div class="mall_title">配送信息</div>

       <div class="address_box">
            <ul>
  <asp:Repeater ID="Repeater1" runat="server">
          <ItemTemplate>
            <li>
               <p><input name="psradio" class="psxxrad" type="radio" <%#Eval("IsDefaultAddress").ToString().ToLower()=="true"?"checked=\"checked\"":""%>/ value="<%#Eval("Id")%>" > <%#Eval("MobilePhone")%> <%#Eval("IsDefaultAddress").ToString().ToLower()=="true"?"<span class=\"font_yellow paddL10\" style=\"float: right;\">默认地址</span>":""%></p>
               <p class="txt"><%#Eval("CountyName")%><%#Eval("ProvinceName")%><%#Eval("CityName")%><%#Eval("AreaName")%><%#Eval("Address")%><span class="floatR"></span></p>
               
            </li>
            </ItemTemplate>
         </asp:Repeater>
            </ul>
       </div>
    
          <div class="padd20 cent">
          <asp:Button ID="btnSave"  CssClass="basic_btn" OnClientClick="return PageObjload.yanzheng(); " runat="server" Text="提交订单"   onclick="btnSave_Click"/>
         
          </div>
    

</div>

    </form>
    <script type="text/javascript" language="javascript">
        var PageObjload = {
            yanzheng: function () {
                var psxxid = $(".psxxrad:checked").val();
                if (psxxid!="") {
                    $("#<%=hdfadderid.ClientID %>").val(psxxid);
                    return true;
                }
                else {
                    alert("请先填写配送信息！");
                    return false;
                }
                
            }
        }
        $(function () {

        });
    </script>
</body>
</html>
