<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleDetail.aspx.cs" Inherits="Enow.TZB.Web.WX.Article.ArticleDetail" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <title>文章详细</title>
    
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" runat="server" />
<div class="warp">
   <div class="qiu_box">
       <div class="qiu-cont font20 cent">
              <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
       </div>
   </div>  
    
   <div class="qiu_box mt10">
       <div class="qiu-cont">
           <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
       </div> 
   </div>
   
</div>
    </form>
</body>
</html>