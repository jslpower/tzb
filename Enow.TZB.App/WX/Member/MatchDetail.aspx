<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatchDetail.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.MatchDetail" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">

<title>赛事详情</title>
<link rel="stylesheet" href="/wx/css/style.css" type="text/css" media="screen">

<script type="text/javascript">
    function nTabs(tabObj, obj) {
        var tabList = document.getElementById(tabObj).getElementsByTagName("li");
        for (i = 0; i < tabList.length; i++) {
            if (tabList[i].id == obj.id) {
                document.getElementById(tabObj + "_Title" + i).className = "active";
                document.getElementById(tabObj + "_Content" + i).style.display = "block";
            } else {
                document.getElementById(tabObj + "_Title" + i).className = "normal";
                document.getElementById(tabObj + "_Content" + i).style.display = "none";
            }
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="赛事详情" runat="server" />
<div class="warp">
  <div class="msg_tab"  id="n4Tab">
     
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0" onClick="nTabs('n4Tab',this);" class="active"><a href="javascript:void(0);">主办方</a></li>
              <li id="n4Tab_Title1" onClick="nTabs('n4Tab',this);"><a href="javascript:void(0);">赛事规程</a></li>
              <li><a href="MatchTeamBallotResult.aspx?Id=<%=Request.QueryString["Id"] %>">赛事赛程</a></li>
           </ul>
        </div>

        <div class="TabContent">
        
			<div id="n4Tab_Content0">
            
              <div class="msg_list">
                 <ul>
                       <li><label>主办方：</label><asp:Literal ID="MasterOrganizer" runat="server"></asp:Literal></li>
                       <li><label></label><asp:Literal ID="CoOrganizers" runat="server"></asp:Literal></li>
                       <li><label></label><asp:Literal ID="Organizer" runat="server"></asp:Literal></li>
                       <li><label></label><asp:Literal ID="Sponsors" runat="server"></asp:Literal></li>
                       <li><label>报名日期：</label><asp:Literal ID="ltrSignUpTime" runat="server"></asp:Literal></li>
                       <li>
                          <div class="lie"><label>参赛队伍数：</label><asp:Literal ID="SignUpNumber" runat="server"></asp:Literal></div>
                          <div class="lie2"><label>参赛队员数：</label><asp:Literal ID="PlayersMax" runat="server"></asp:Literal></div>
                       </li>
                       <li>
                          <div class="lie"><label>足球宝贝数：</label><asp:Literal ID="BayMin" runat="server"></asp:Literal>-<asp:Literal ID="BayMax" runat="server"></asp:Literal></div>
                          <div class="lie2"><label>报名年龄：</label><asp:Literal ID="MinAge" runat="server"></asp:Literal>-<asp:Literal ID="MaxAge" runat="server"></asp:Literal></div>
                       </li>
                       <li>
                       <div class="lie"><label>比赛总时间：</label><asp:Literal ID="TotalTime" runat="server"></asp:Literal></div>
                       <div class="lie2"><label>中场休息时间：</label><asp:Literal ID="BreakTime" runat="server"></asp:Literal></div>
                       </li>
                       <li><label>报名数：</label><asp:Literal ID="ltrTeamNumber" runat="server"></asp:Literal></li>
                 </ul>
              </div>
              <div class="qiu_list player_list mt10">
          <ul>
      <asp:Repeater ID= "rptList" runat="server">
      <ItemTemplate>
      <li>
            <div class="item-img"><img src="<%#Eval("HeadPhoto")%>" width="80" height="111" /></div>
            <div class="item-box">
                <div class="Rbtn padd01">
                <a>已参加</a>
                </div>
                <dl>
                    <dt><%#Eval("ContactName")%></dt>
                    <dd><span class="<%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))==Enow.TZB.Model.EnumType.球员角色.队长?"bg_red":"bg_green" %>"><%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))%></span></dd>
                    <dd><%#Eval("JerseyNumber")%>号</dd>
                    <dd><%#Eval("PlayerPosition")%></dd>
                </dl>
            </div>
         </li>
      </ItemTemplate>
      </asp:Repeater>
          </ul>
      </div>
              <div class="mt20 padd_bot"><a href="javascript:history.back(1)" class="basic_btn">返  回</a></div>
            
            </div>
 
 			<div id="n4Tab_Content1" class="none">
            
              <div class="guize">
                 <asp:Literal ID="Remark" runat="server"></asp:Literal>
              </div>
              
              <div class="mt20 padd_bot"><a href="javascript:history.back(1)" class="basic_btn">返  回</a></div>
            
            </div>

			
           
    </div>

     
     
     
  </div>
</div>

    </form>
</body>
</html>
