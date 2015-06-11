<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamTransfer.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.TeamTransfer" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>队长转让</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
     
    <link rel="stylesheet" href="/WX/css/tangzhu.css" type="text/css" media="screen"/>
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
            <uc1:UserHome ID="UserHome1" Userhometitle="队长转让" runat="server" />
    <div class="warp">        
    <div class="msg_tab" id="n4Tab">
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0" class="active"><a href="MyTeamInfo.aspx">介绍</a></li>
              <li id="n4Tab_Title1"><a href="TeamInfo.aspx">队员</a></li>
              <li id="n4Tab_Title2"><a href="/WX/Member/TeamArticles.aspx">日志</a></li>
           </ul>
        </div>
        <div class="TabContent">
        <div id="n4Tab_Content0">
         <div class="msg_list">
                  <ul>
                  <li><label>球队名称：</label><asp:Literal ID="ltrTeamName" runat="server"></asp:Literal></li>
                  <li><label>新的队长：</label><asp:DropDownList ID="ddlTeamMember" runat="server">
                      </asp:DropDownList>
                  </li>
                            
                   </ul>
                   
                </div>
                
                <div class="msg_btn">
                    <asp:Button ID="btnSave" runat="server" CssClass="basic_btn" Text="立即转让" 
                        onclick="btnSave_Click" /></div>
            
            </div>
            </div>
      </div>
    </div>
    </div>
    </form>
</body>
</html>