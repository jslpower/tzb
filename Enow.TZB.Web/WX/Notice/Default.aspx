<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.Web.WX.Notice.Default" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
<title>提示</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen">
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="提示" runat="server" />
<div class="warp s_warp">
<div style="width:100%;text-align:center;">
<img src="../images/ErrNotice.gif" border="0">
</div>
</div>
    </form>
</body>
</html>
