<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Match.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.Match" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
<title>我的赛事</title>
<link rel="stylesheet" href="/wx/css/style.css" type="text/css" media="screen">
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="我的赛事" runat="server" />
<div class="search_head">
   <div class="search_form">
   <asp:TextBox ID="txtKeyWords" runat="server" CssClass="input_txt floatL" Text="" placeholder="赛事搜索"></asp:TextBox><asp:Button
       ID="btnSearch" CssClass="input_btn icon_search_i floatR" runat="server" 
           onclick="btnSearch_Click" />
   </div>
</div>

<div class="warp s_warp">
    <asp:Repeater ID="rptList" runat="server">
        <ItemTemplate>
      <div class="msg_list qiu_box nobot">
        <ul>
          <li><label>赛事名称：</label><%#Eval("MatchName")%></li>
          <li><label>赛事时间：</label><%#Eval("BeginDate","{0:yyyy-MM-dd HH:mm}")%></li>
          <li><label>举报城市：</label><%#Eval("CityName")%>-<%#Eval("AreaName")%></li>
          <li><label>报名人数：</label><%#Eval("JoinNumber")%>名</li>
        </ul>
      </div>
  
      <div class="qiu-caozuo"><a href="MatchDetail.aspx?Id=<%#Eval("Id") %>" class="basic_btn">查看详情</a><%#UpdateOpt(Eval("Id").ToString(), (Enow.TZB.Model.EnumType.参赛审核状态)Convert.ToInt32(Eval("State")), (Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType")))%></div>
        
        </ItemTemplate>
    </asp:Repeater>
</div>
    </form>
</body>
</html>
