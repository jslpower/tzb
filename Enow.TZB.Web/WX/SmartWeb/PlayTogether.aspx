<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayTogether.aspx.cs" Inherits="Enow.TZB.Web.WX.SmartWeb.PlayTogether" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <title>一起玩吧</title>    
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<style type="text/css">
        /*main nav*/
        .nav-list{margin:8px 8px 0 2px;overflow:hidden;}
        .nav-list li{position:relative;float:left;width:50%;border-left:6px solid #fff;margin-bottom:6px;-webkit-box-sizing:border-box;-ms-box-sizing:border-box;box-sizing:border-box;}
        .nav-list li div{width:100%;height:60px;line-height:60px;border-radius:3px;color:#fff;font-size:15px;text-align:center;}
        .nav-list li div a{color:#FFF;}
        .nav-list li:nth-child(1),
        .nav-list li:nth-child(2),
        .nav-list li:nth-child(3),
        .nav-list li:nth-child(4),
        .nav-list li:nth-child(5),
        .nav-list li:nth-child(6),
        .nav-list li:nth-child(7),
        .nav-list li:nth-child(8),
        .nav-list li:nth-child(1) div{background-color:#62c500;}
        .nav-list li:nth-child(2) div{background-color:#84d018;}
        .nav-list li:nth-child(3) div{background-color:#ff6073;}
        .nav-list li:nth-child(4) div{background-color:#ff7081;}
        .nav-list li:nth-child(5) div{background-color:#3d98ff;}
        .nav-list li:nth-child(6) div{background-color:#3db4ff;}
        .nav-list li:nth-child(7) div{background-color:#8a95ff;}
        .nav-list li:nth-child(8) div{background-color:#82a8ff;}
        .nav-list li .point{margin:0 2px;font-size:14px;}
</style>
</head>
<body>
    <form id="form1" runat="server">
       <uc1:UserHome ID="UserHome1" Userhometitle="一起玩吧" runat="server" />
<div class="warp">
<div class="cent"><img src="../images/yqwb.png" width="100%" /></div>
<ul class="nav-list">
            <li onclick="javascript:window.location.href='/WX/Team/Default.aspx';">
                <div><a title="铁丝球队" href="/WX/Team/Default.aspx" data-href="/WX/Team/Default.aspx">铁丝球队</a></div>
            </li>
            <li onclick="javascript:window.location.href='/WX/Match/List.aspx';">
                <div><a title="铁丝约战" href="/WX/Match/List.aspx" data-href="/WX/Match/List.aspx">杯赛联赛</a></div>
            </li>
            <li onclick="javascript:window.location.href='/WX/Notice/Default.aspx';">
                <div><a title="杯赛联赛" href="/WX/AboutWar/Default.aspx" data-href="/WX/AboutWar/Default.aspx">铁丝约战</a></div>
            </li>
            <li onclick="javascript:window.location.href='/WX/Member/TZGathering.aspx?types=3';">
                <div><a title="铁丝聚会" href="/WX/Member/TZGathering.aspx?types=3" data-href="/WX/Member/TZGathering.aspx?types=3">铁丝聚会</a></div>
            </li>
            <li onclick="javascript:window.location.href='/WX/Mall/Mall_Type.aspx?type=4';">
                <div><a title="足球旅游" href="/WX/Mall/Mall_Type.aspx?type=4" data-href="/WX/Mall/Mall_Type.aspx?type=4">足球旅游</a></div>
            </li>
            <li onclick="javascript:window.location.href='/WX/Member/TZGathering.aspx?types=4';">
                <div><a title="相聚球星" href="/WX/Member/TZGathering.aspx?types=4" data-href="/WX/Member/TZGathering.aspx?types=4">相聚球星</a></div>
            </li>
            <li onclick="javascript:window.location.href='/WX/Vote/Defaut.aspx';">
                <div><a title="投票抽奖" href="/WX/Vote/Defaut.aspx" data-href="/WX/Vote/Defaut.aspx">投票抽奖</a></div>
            </li>
            <li onclick="javascript:window.location.href='/WX/Rudder/Default.aspx?types=1';">
                <div><a title="舵主风采" href="/WX/Rudder/Default.aspx?types=1" data-href="/WX/Rudder/Default.aspx?types=1">舵主风采</a></div>
            </li>
        </ul>
</div>
    </form>
</body>
</html>