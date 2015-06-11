<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterChoose.aspx.cs" Inherits="Enow.TZB.Web.WX.Register.RegisterChoose" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <title>成为铁丝</title>
    
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
</head>
<body>
    <form id="form1" runat="server">
     <uc1:UserHome ID="UserHome1" Userhometitle="成为铁丝" runat="server" />
<div class="warp">
<div class="qiu_box mt10">
       <div class="qiu-cont">
新用户请选择“铁丝注册”，已通过网站或线下注册的铁丝请选择“账号绑定”进行铁丝账号与微信账号的绑定。
</div></div>
   <div class="padd20 mt20"><a href="/WX/Register/Default.aspx" class="basic_btn">铁丝注册</a></div>
   <div class="padd20 mt20 paddT0"><a href="/WX/Member/MemberBind.aspx" class="basic_ybtn">帐号绑定</a></div>
</div>
    </form>
</body>
</html>