<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatchWallet.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.MatchWallet" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">

<title>铁丝中心</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/WX/css/user.css" type="text/css" media="screen">

</head>
<body>
    <form id="form1" runat="server">
   <uc1:UserHome ID="UserHome1" Userhometitle="钱包" runat="server" />

<div class="user_warp">

      <div class="user-item  user-item01 R_jiantou" onclick="window.location.href='MemberUpdate.aspx'">
        <div class="user_head radius"><asp:Literal ID="ltrHead" runat="server"></asp:Literal></div>
        <div class="user_n"><asp:Literal ID="ltrNickName" runat="server"></asp:Literal><a href="Message.aspx"><span class="xiaoxi"><em class="xiaoxi_num"><asp:Literal ID="ltrMsgNumber" runat="server"></asp:Literal></em></span></a></div>
        <div class="user_jf">积分：<asp:Literal ID="ltrPoint" runat="server"></asp:Literal>  <asp:Literal ID="ltrTitle" runat="server"></asp:Literal><br /><span style=" padding-left:0px;">球队：<asp:Literal ID="ltrTeamName" runat="server"
        ></asp:Literal></span></div>
    </div>

    <div class="user-cont">
  
         <div class="user-bi">可用铁丝币：<i class="font30"><asp:Literal ID="litltsbnum" runat="server"></asp:Literal></i> 个</div>
         <div class="user-fu"><a href="/WX/Fund/FacePayStep1.aspx"></a></div>
         <div class="user-bi mt10">点击使用铁丝币在铁丝网柜台使用</div>

    </div>
     
     
     
</div>
    </form>
</body>
</html>
