<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopBar.ascx.cs" Inherits="Enow.TZB.WebForm.UserControl.TopBar" %>
<div class="top">
<asp:PlaceHolder ID="plnlogin" runat="server">
   <div class="topbox">
      <ul>
          <li><a href="/Default.aspx">登录</a></li>
          <li class="line">|</li>
          <li><a href="/register/default.aspx">注册</a></li>
      </ul>
   </div>
   </asp:PlaceHolder>
   <asp:PlaceHolder ID="plnLoginDetail" runat="server" Visible="false">
      <div class="topbox quick_menu">
      <ul>
          <li>积分：<asp:Label ID="lblScore" runat="server"></asp:Label></li>
          <li>铁丝币：<asp:Label ID="lblCurr" runat="server"></asp:Label></li>
          <li><asp:Label ID="ClassName" runat="server"></asp:Label></li>
          <li id="top_menu">
                <b><asp:Label ID="lblUserName" runat="server"></asp:Label><s></s></b>
                <ul class="more">
                      <li><a href="/My/Default.aspx" target="_self">基本信息</a></li>
                      <li><a href="/My/Recharge.aspx" target="_self">账号充值</a></li>
                      <li><a href="/My/MyTeam.aspx" target="_self">我的球队</a></li>
                      <li><a href="/My/MyMatch.aspx" target="_self">我的比赛</a></li>
                </ul>
          </li>
      </ul>
   </div>
   </asp:PlaceHolder>
</div>