<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="Enow.TZB.WebForm.Match.Result" %>
<%@ Register Src="/UserControl/TopBar.ascx" TagName="TopBar" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/Menu.ascx" TagName="menu" TagPrefix="uc2"%>
<%@ Register Src="/UserControl/Footer.ascx" TagName="footer" TagPrefix="uc3"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="keywords" content="" />
    <title>赛事战报</title>
    <link href="/css/style.css" rel="stylesheet" />
     <link href="/Css/boxy.css" rel="Stylesheet" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="/Js/table-toolbar.js"></script>
    <script type="text/javascript" src="/Js/jquery.boxy.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:TopBar ID="topbar1" runat="server" />
 <uc2:menu ID="menu1" runat="server" />
    <div class="head_line">
    </div>
    <div class="warp">

  <div class="game_zb_cont">
    <div class="zb_title cent"><asp:Literal ID="ltrMatchName" runat="server"></asp:Literal></div>
    <div class="zb_stitle cent"><asp:Literal ID="ltrGameName" runat="server"></asp:Literal>  第<asp:Literal ID="ltrON" runat="server"></asp:Literal>场</div>
    <div class="cent"><asp:Literal ID="ltrMatchTime" runat="server"></asp:Literal>  [<asp:Literal ID="ltrFieldName" runat="server"></asp:Literal>]</div>
    <div class="game_vs botline">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td width="383"><asp:Literal ID="ltrHomeTeamPic" runat="server"></asp:Literal></td>
          <td align="center" valign="middle" class="vs">VS</td>
          <td width="383"><asp:Literal ID="ltrAwayTeamPic" runat="server"></asp:Literal></td>
        </tr>
        <tr>
          <td align="center"  class="font20"><asp:Literal ID="ltrHomeTeamName" runat="server"></asp:Literal></td>
          <td>&nbsp;</td>
          <td align="center" class="font20"><asp:Literal ID="ltrAwayTeamName" runat="server"></asp:Literal></td>
        </tr>
        <tr>
          <td align="center" class="font30"><asp:Literal ID="ltrHomeGoals" runat="server"></asp:Literal></td>
          <td>&nbsp;</td>
          <td align="center" class="font30"><asp:Literal ID="ltrAwayGoals" runat="server"></asp:Literal></td>
        </tr>
        <tr>
          <td align="center" valign="top">
          <table width="100%" border="0" cellpadding="0" cellspacing="0" class="vs_table">
            <tr>
              <th>上场名单</th>
              <th>球衣</th>
              <th>进球</th>
              <th>犯规</th>
              <th>红牌</th>
              <th>黄牌</th>
            </tr>
            <asp:Literal ID="ltrHomeMember" runat="server"></asp:Literal>
          </table>
          </td>
          <td>&nbsp;</td>
          <td align="center" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" class="vs_table">
            <tr>
              <th>上场名单</th>
              <th>球衣</th>
              <th>进球</th>
              <th>犯规</th>
              <th>红牌</th>
              <th>黄牌</th>
            </tr>
            <asp:Literal ID="ltrAwayMember" runat="server"></asp:Literal>
          </table></td>
        </tr>
      </table>
    
    </div>
    
    <div class="news_title cent mt10"><asp:Literal ID="ltrArticleTitle" runat="server"></asp:Literal></div>  
    <div class="cent"><asp:Literal ID="ltrResutlArticleTime" runat="server"></asp:Literal></div>
    <div class="mt10">
      <asp:Literal ID="ltrContentInfo" runat="server"></asp:Literal></div>
    

  </div>
  
</div>
   <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
