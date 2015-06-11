<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mall_Type.aspx.cs" Inherits="Enow.TZB.Web.WX.Mall.Mall_Type" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
    <title><%=Typetitle%></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/mall.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" runat="server" />
    <div class="search_head home_search">
        <div class="search_form">
            <asp:TextBox ID="txtGoodsName" runat="server" CssClass="input_txt floatL" Text=""
                placeholder="分类搜索"></asp:TextBox>
            <asp:Button ID="btnSerch" runat="server" CssClass="input_btn icon_search_i floatR"
                Text="" OnClick="btnSerch_Click" />
        </div>
    </div>
    <div class="home_warp">
        <div class="list-item">
            <ul>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                    <li class="R_jiantou" onclick="location.href='Mall_Type_List.aspx?RoleType=<%#Eval("Id")%>'"><%#Eval("Rolename")%></li>
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
