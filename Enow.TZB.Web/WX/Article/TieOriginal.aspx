<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TieOriginal.aspx.cs" Inherits="Enow.TZB.Web.WX.Article.TieOriginal" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
<title>铁原创</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
</head>
<body>
    <form id="form1" runat="server">
    <input id="pageindex" type="hidden" value="<%=CurrencyPage %>" />
<input id="hidPageend" type="hidden" value="0" />

    <uc1:UserHome ID="UserHome1" Userhometitle="铁原创" runat="server" />
  
<div class="warp">
  <div class="msg_tab">
        <div class="TabTitle">
           <ul class="fixed">
              <li<%if(TypeId==15){%> class="active"<%} %>><a href="/WX/Article/TieOriginal.aspx?TypeId=15">铁文集</a></li>
              <li<%if(TypeId==16){%> class="active"<%} %>><a href="/WX/Article/TieOriginal.aspx?TypeId=16">铁资讯</a></li>
              <li<%if(TypeId==17){%> class="active"<%} %>><a href="/WX/Article/TieOriginal.aspx?TypeId=17">铁漫画</a></li>
           </ul>
        </div><!--
        <br />
   <div class="search_form">
   <asp:TextBox ID="txtKeyWords" runat="server" CssClass="input_txt floatL" Text=""></asp:TextBox><asp:Button
       ID="btnSearch" CssClass="input_btn icon_search_i floatR" runat="server" 
           onclick="btnSearch_Click" />
   </div>-->
<div class="TabContent">
<div id="n4Tab_Content0">
<asp:PlaceHolder ID="phHead" runat="server">
   <div class="qiu_box">
       <div class="qiu-bigimg"><asp:Literal ID="ltrTypePhoto" runat="server"></asp:Literal></div>
       <div class="qiu-cont">
              <asp:Literal ID="ltrTypeRemark" runat="server"></asp:Literal>
       </div>
   </div>  
   </asp:PlaceHolder>
   <div class="qiu_list mt10">
   <div class="item-ArticleBox">
       <ul>
      <asp:Repeater ID="rptList" runat="server">      
      <ItemTemplate>            
                    <li><a href="ArticleDetail.aspx?Id=<%#Eval("Id") %>"><%#Enow.TZB.Utility.Utils.GetText2(Eval("Title").ToString(),12,true)%></a></li>
      </ItemTemplate>      
  </asp:Repeater>  
  </ul>
            </div>
   </div> 
</div>
</div>
</div>
</div>
    </form>
</body>
</html>
