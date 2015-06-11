<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleLeaveWord.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.ArticleLeaveWord" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>我的日志-留言</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/tangzhu.css" type="text/css" media="screen"/>
</head>

<body>
       <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="留言" runat="server" />
<div class="warp">
  
  <div class="padd10">
  <asp:Literal ID="litIntroduce" runat="server" >留言</asp:Literal>
    
       <div class="fabiao_box">
        <asp:TextBox ID="txtLeaveWord"  runat="server" TextMode="MultiLine"></asp:TextBox>
       </div>
       
  </div>
  
  <div class="cent padd10">
      <asp:Button ID="btnAddArticle" runat="server" Text="保存" CssClass="basic_btn" onclick="btnAddArticle_Click1" 
          /></div>
  
</div>

    </form>
</body>
</html>
