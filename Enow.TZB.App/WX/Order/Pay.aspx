<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pay.aspx.cs" Inherits="Enow.TZB.Web.WX.Order.Pay" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>支付</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="支付" runat="server" />
<div class="warp">

  <div class="msg_tab"  id="n4Tab">

       <%-- <div class="TabTitle">
           <ul class="fixed">
              <li class="active"><a href="Pay.aspx">支付</a></li>
              <li><a href="/WX/Fund/PayPassword.aspx">支付密码</a></li>
              <li style="width:34%;"><a href="/WX/Fund/PayBill.aspx">账单</a></li>
           </ul>
        </div>--%>

        <div class="TabContent">
        
			<div id="n4Tab_Content0">
            
                <div class="msg_list">
                  <ul>
                    <li><label style="width: 90px;">订单总金额：</label><asp:Literal ID="litTotal" runat="server"></asp:Literal></li>
                    <li><label style="width: 90px;">商品金额：</label><asp:Literal ID="litGoodsFee" runat="server"></asp:Literal></li>
                    <li><label style="width: 90px;">运&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;费：</label><asp:Literal ID="litFreightFee" runat="server"></asp:Literal></li>
                    <li><label style="width: 90px;">应付金额：</label><asp:Literal ID="spanMoney" runat="server"></asp:Literal></li>
                            
                   </ul>
                   
                </div>
                
                <div class="msg_btn">
                    <asp:Button ID="btnSave" runat="server" CssClass="basic_btn" Text="立即支付" 
                        onclick="btnSave_Click" /></div>
                
            </div>

         </div>   
  </div>     

</div>
   
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(function () {
        $("#spanMoney").text($("#<%=litTotal.ClientID %>").val());
    });
</script>