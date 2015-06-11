<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="Enow.TZB.Web.WX.Match.Result" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
<title>赛事详情</title>
<link rel="stylesheet" href="/wx/css/style.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="赛事详情" runat="server" />

<div class="warp">
  <div class="msg_tab"  id="n4Tab">
     
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0"><a href="MatchDetail.aspx?Id=<%=Request.QueryString["Id"] %>">主办方</a></li>
              <li id="n4Tab_Title1"><a href="MatchDetail.aspx?Id=<%=Request.QueryString["Id"] %>">赛事规程</a></li>
              <li class="active"><a href="MatchTeamBallotResult.aspx?Id=<%=Request.QueryString["Id"] %>">赛事赛程</a></li>
           </ul>
        </div>

        <div class="TabContent">          
              <div class="MatchSchedule">
                 <div class="cent" style="font-size:16px; font-weight:bold;"><asp:Literal ID="ltrMatchName" runat="server"></asp:Literal></div>
                 <strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Literal ID="ltrGameName" runat="server"></asp:Literal></strong>
                 <ul class="ScheduleResult">
                  <li>
                  <asp:Literal ID="ltrMatchScheduleInfo" runat="server"></asp:Literal>
                  <div class="clearfix"></div>
                  <span class="floatL cent"><asp:Literal ID="ltrHomeTeam" runat="server"></asp:Literal></span><span class="cent"><asp:Literal ID="ltrAwayTeam" runat="server"></asp:Literal></span>
                  <div class="clearfix"></div>
                  <span class="floatL cent"><asp:Literal ID="ltrHomeGolas" runat="server"></asp:Literal></span><span class="cent"><asp:Literal ID="ltrAwayGolas" runat="server"></asp:Literal></span>
                  <div class="clearfix"></div>
                  <span class="floatL">上半场进球：<asp:Literal ID="ltrHomeFirstGoals" runat="server"></asp:Literal></span><span>上半场进球：<asp:Literal ID="ltrAwayFirstGoals" runat="server"></asp:Literal></span>
                  <div class="clearfix"></div>
                  <span class="floatL">下半场进球：<asp:Literal ID="ltrHomeSecondGoals" runat="server"></asp:Literal></span><span>下半场进球：<asp:Literal ID="ltrAwaySecondGoals" runat="server"></asp:Literal></span>
                  <div class="clearfix"></div>
                  <span class="floatL">加时赛进球数：<asp:Literal ID="txtHomeOvertimePenaltys" runat="server"></asp:Literal></span><span>加时赛进球数：<asp:Literal ID="txtAwayOvertimePenaltys" runat="server"></asp:Literal></span>
                  <div class="clearfix"></div>
                  <span class="floatL">点球数：<asp:Literal ID="txtHomePenaltys" runat="server"></asp:Literal></span><span>点球数：<asp:Literal ID="txtAwayPenaltys" runat="server"></asp:Literal></span>
                  <div class="clearfix"></div>
                  <span class="floatL">犯规数：<asp:Literal ID="txtHomeFouls" runat="server"></asp:Literal></span><span>犯规数：<asp:Literal ID="txtAwayFouls" runat="server"></asp:Literal></span>
                  <div class="clearfix"></div>
                  <span class="floatL">红牌数：<asp:Literal ID="txtHomeReds" runat="server"></asp:Literal></span><span>红牌数：<asp:Literal ID="txtAwayReds" runat="server"></asp:Literal></span>
                  <div class="clearfix"></div>
                  <span class="floatL">黄牌数：<asp:Literal ID="txtHomeYellows" runat="server"></asp:Literal></span><span>黄牌数：<asp:Literal ID="txtAwayYellows" runat="server"></asp:Literal></span>
                 </li>
                  </ul>
              </div>

    </div>
</div>
    </form>
</body>
</html>
