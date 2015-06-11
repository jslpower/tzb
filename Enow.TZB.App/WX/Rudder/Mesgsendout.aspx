<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mesgsendout.aspx.cs" Inherits="Enow.TZB.Web.WX.Rudder.Mesgsendout" %>
<%@ Register Src="../UserControls/UserHome.ascx" TagName="UserHome" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <title>发送信息</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen">
    <link rel="stylesheet" href="/WX/css/tangzhu.css" type="text/css" media="screen">
</head>
<body>
    <form id="form1" runat="server">
    <uc1:userhome id="UserHome1" userhometitle="发送信息" runat="server" />
    <div class="warp">
        <div class="padd10">
            标题：
            <div class="fabiao_title">
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            </div>
             收件人：
            <div class="fabiao_title">
               <asp:Literal runat="server" ID="litsjr"></asp:Literal>
            </div>
            内容：
            <div class="fabiao_box">
                <asp:TextBox ID="txtArticleInfo" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <div class="cent padd10">
           <asp:Button ID="btnEditArticle" runat="server" Text="保存" CssClass="basic_btn" OnClick="btnEditArticle_Click" /></div>
    </div>
    </form>
</body>
</html>
