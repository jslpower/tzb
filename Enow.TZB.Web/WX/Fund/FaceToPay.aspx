<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FaceToPay.aspx.cs" Inherits="Enow.TZB.Web.WX.Fund.FaceToPay" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>当面付-支付二维码</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="支付二维码" runat="server" />
<div class="warp">
  <div class="msg_tab"  id="n4Tab">
        <div class="TabContent">
        <div id="n4Tab_Content1">
 
                <div class="msg_list cent">
                    <asp:Image ID="imgPayQrCode" runat="server" Width="80%" />
                </div>
                
                <div class="msg_btn"><a href="/WX/Member/Default.aspx" class="basic_btn">支付完成</a></div>
            
            </div>
         </div>
   
  </div>  
</div> 
    </form>
</body>
</html>
