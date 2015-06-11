<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleEdit.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.ArticleEdit" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>我的日志-发表日志</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen">
<link rel="stylesheet" href="/WX/css/tangzhu.css" type="text/css" media="screen">
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="发表日志" runat="server" />
<div class="warp">
  
  <div class="padd10">标题：
  <div class="fabiao_title">
    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
       </div><%--图片：
       <div class="fabiao_title">
    <asp:FileUpload ID="imgFileUpload" runat="server" />
       </div>--%>内容：
       <div class="fabiao_box">
        <asp:TextBox ID="txtArticleInfo"  runat="server" TextMode="MultiLine"></asp:TextBox>
       </div>
    
  </div>
  
  <div class="cent padd10">
      <asp:Button ID="btnEditArticle" runat="server" Text="保存" CssClass="basic_btn"  OnClick="btnEditArticle_Click"
          /></div>
  
</div>

    </form>
</body>
</html>