<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoAttention.aspx.cs" Inherits="Enow.TZB.Web.WX.Notice.NoAttention" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>关注铁子帮微信</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="帮主温馨提示" runat="server" />
<div class="warp">
   <div class="qiu_box mt10">
       <div class="qiu-cont">
       <img src="/WX/images/NoAtt.png" width="100%" border="0" />
       </div> 
   </div>
   
</div>
    </form>
</body>
</html>