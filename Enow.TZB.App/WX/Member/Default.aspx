<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.Default" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>铁丝中心</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/WX/css/user.css" type="text/css" media="screen">
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="铁丝会员中心" runat="server" />
<div class="user_warp paddB10">

    <div class="user-item  user-item01 R_jiantou" onclick="window.location.href='MemberUpdate.aspx'">
        <div class="user_head radius"><asp:Literal ID="ltrHead" runat="server"></asp:Literal></div>
        <div class="user_n"><asp:Literal ID="ltrNickName" runat="server"></asp:Literal><a href="Message.aspx"><span class="xiaoxi"><em class="xiaoxi_num"><asp:Literal ID="ltrMsgNumber" runat="server"></asp:Literal></em></span></a></div>
        <div class="user_jf">积分：<asp:Literal ID="ltrPoint" runat="server"></asp:Literal>  <asp:Literal ID="ltrTitle" runat="server"></asp:Literal><br /><span style=" padding-left:0px;">球队：<asp:Literal ID="ltrTeamName" runat="server"
        ></asp:Literal></span></div>
    </div>
    
    <div class="user-item font14 R_jiantou" onclick="location.href='/WX/Team/MyTeamInfo.aspx'">我的球队</div>
    <div class="user-menu">
       <ul class="fixed">
           <li onclick="location.href='/WX/Team/TeamInfo.aspx'"><s class="u-menu-icon01" ></s><h2>队员</h2></li>
           <li onclick="location.href='/WX/Member/Match.aspx'"><s class="u-menu-icon02"></s><h2>赛事</h2></li>
           <li onclick="location.href='/WX/AboutWar/UserAwList.aspx'"><s class="u-menu-icon03"></s><h2>约战</h2></li>
           <li onclick="location.href='/WX/Member/Articles.aspx'"><s class="u-menu-icon04"></s><h2>日志</h2></li>
       </ul>
    </div>

    <div class="user-item font14 R_jiantou" onclick="location.href='MatchWallet.aspx'">我的钱包</div>
    <div class="user-menu">
       <ul class="fixed">
           <li onclick="location.href='/WX/Fund/FacePayStep1.aspx'"><s class="u-menu-icon05"></s><h2>当面付</h2></li>
           <li onclick="location.href='/WX/Fund/PayBill.aspx'"><s class="u-menu-icon06"></s><h2>账单</h2></li>
           <li onclick="location.href='/WX/Fund/Transfers.aspx'"><s class="u-menu-icon07"  ></s><h2>转账</h2></li>
           <li onclick="location.href='/WX/Fund/Default.aspx'"><s class="u-menu-icon08"></s><h2>充值</h2></li>
       </ul>
    </div>

    <div class="user-item font14 R_jiantou" onclick="location.href='/WX/Order/UserOrders.aspx?type=<%= Convert.ToInt32(Enow.TZB.Model.商城订单状态.审核通过) %>'">我的订单</div>
    <div class="user-menu">
       <ul class="fixed">
           <li onclick="location.href='/WX/Order/UserOrders.aspx?type=<%= Convert.ToInt32(Enow.TZB.Model.商城订单状态.未支付) %>'"><s class="u-menu-icon09" ></s><h2>未付</h2></li>
           <li onclick="location.href='/WX/Order/UserOrders.aspx?type=<%= Convert.ToInt32(Enow.TZB.Model.商城订单状态.已支付) %>'"><s class="u-menu-icon10" ></s><h2>已付</h2></li>
           <li onclick="location.href='/WX/Order/UserOrders.aspx?type=<%= Convert.ToInt32(Enow.TZB.Model.商城订单状态.已发货) %>'"><s class="u-menu-icon11" ></s><h2>发货</h2></li>
           <li onclick="location.href='/WX/Order/UserOrders.aspx?type=<%= Convert.ToInt32(Enow.TZB.Model.商城订单状态.退款) %>'"><s class="u-menu-icon12" ></s><h2>退款</h2></li>
       </ul>
    </div>

    <div class="user-item font14 R_jiantou">我的预约</div>
    <div class="user-menu">
       <ul class="fixed">
           <li onclick="location.href='SiteList.aspx'"><s class="u-menu-icon13"></s><h2>场地</h2></li>
           <li onclick="location.href='ActivityList.aspx?type=2'"><s class="u-menu-icon14"></s><h2>培训</h2></li>
           <li onclick="location.href='ActivityList.aspx?type=3'"><s class="u-menu-icon15"></s><h2>聚会</h2></li>
           <li onclick="location.href='ActivityList.aspx?type=4'"><s class="u-menu-icon16"></s><h2>见面会</h2></li>
       </ul>
    </div>


   

   

    <div class="user-item user-item02 font14 R_jiantou" onclick="location.href='/WX/Member/BazaarList.aspx'"><i class="u-ico03"></i>我的义卖</div>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    
    <div class="user-item user-item02 font14 R_jiantou" onclick="location.href='/WX/Rudder/PatList.aspx'"><i class="u-ico04"></i>我的关注</div>
     <div class="user-item user-item02 font14 R_jiantou" onclick="location.href='/WX/Mall/Mall_ShoppingChart.aspx'"><i class="u-ico07"></i>我的购物车</div>
    <div class="user-item user-item02 font14 R_jiantou" onclick="location.href='/WX/Member/AddressList.aspx'"><i class="u-ico05"></i>收货地址</div>
     <div class="user-item user-item02 font14 R_jiantou" onclick="location.href='/WX/Vote/VoteUser.aspx?types=2'"><i class="u-ico01"></i>抽奖</div>
    <div class="user-item user-item02 font14 R_jiantou" onclick="location.href='/WX/Vote/VoteUser.aspx?types=1'"><i class="u-ico06"></i>投票</div>
    
</div>

    </form>
</body>
</html>