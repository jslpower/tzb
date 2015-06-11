<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mall.aspx.cs" Inherits="Enow.TZB.Web.WX.Mall.Mall" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html>
<head runat="server">
   <meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>商城</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/mall.css" type="text/css" media="screen"/>


</head>
<body>
    <uc1:UserHome ID="UserHome1" Userhometitle="商城" runat="server" />

<div class="search_head home_search">
   <div class="search_form">
       <asp:TextBox ID="txtGoodsName" runat="server" CssClass="input_txt floatL"  Text="" placeholder="商品搜索"></asp:TextBox>
       <asp:Button ID="btnSerch" runat="server" CssClass="input_btn icon_search_i floatR"  Text=""  onclick="btnSerch_Click"/>

       
   </div>
</div>


<div class="home_warp">
 
    <div class="mall_banner"><img alt="" src="/WX/images/mall_banner.jpg"/></div>
    
    <div class="mall_list mt10">
    
          <h3>精品推荐</h3>

          <ul class="fixed">
            <asp:Repeater ID="rptList" runat="server">
              <ItemTemplate>
             <li onclick="location.href='Mall_Detail.aspx?id=<%#Eval("Id")%>'">
                <div class="img_box"><img alt="" src="<%#Eval("GoodsPhoto")%>"/></div>
                <div class="txt_box">
                    <dl>
                       <dt><%#Eval("GoodsName")%></dt>
                       <dd><i class="line_x">门市价：¥ <%#Eval("MarketPrice")%></i></dd>
                       <dd class="txt">会员价：<i class="font_yellow">¥<%#Eval("MemberPrice")%></i></dd>
                    </dl>
                </div>
             </li>
             </ItemTemplate>
             </asp:Repeater>
             

          
          </ul>
    </div>
    
    <!--mall_bot------------start-->
    <div class="mall_bot">
        <div class="mall_menu">
           <ul class="fixed">
               <li onclick="location.href='Mall_Type.aspx'"><span><s class="icon01"></s>商品分类</span></li>
               <li onclick="location.href='Mall_ShoppingChart.aspx'"><span><s class="icon02"></s>购物车</span></li>
           </ul>
        </div>
    </div>
    <!--mall_bot------------end-->
    
    

</div>

    </form>
</body>
</html>
