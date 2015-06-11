<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamIntroduce.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.TeamIntroduce" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>球队介绍</title>
 <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/tangzhu.css" type="text/css" media="screen"/>
</head>
<body>
    <form id="form1" runat="server">
            <uc1:UserHome ID="UserHome1" Userhometitle="我的球队" runat="server" />
    <div class="warp">
     <div class="msg_tab">
        <div class="TabTitle">
            <ul class="fixed">
                <li class="active"><a href="javascript:void(0);">介绍</a></li>
                <li class="normal"><a href="javascript:void(0);">队员</a></li>
                <li class="normal"><a href="javascript:void(0);">日志</a></li>
            </ul>
        </div>
    </div>
    <div class="TabContent">
        
			<div id="n4Tab_Content0">
            
                <div class="tangzhu_jianli padd20">
                
                   <h3 class="qiudui_title">
                   <span id="ltrTeamName" runat="server"></span>
                   <em style="display:none;"></em></h3><!--em标签里面是编辑按钮，点击可以编辑标题---->
                   
                   <div class="cent" id="teamimg" runat="server">
                   
                  <%-- <img class="qiudui_xximg" src="images/qiudui-img.jpg"/>--%>
                   </div>
                   
                   <div class="tangzhu_jieshao" id="ltrTeamInfo" runat="server">
                       

                   </div>
                   
               
                   <div class="foot_btn">
                       <ul>
                           <li class="box-siz">
                           <asp:HyperLink ID="hyModifyInfo" NavigateUrl="TeamUpdate.aspx"
                Visible="true" runat="server">修改</asp:HyperLink>
                          
                           </li>
                           <li class="box-siz">
                              <asp:HyperLink ID="hyTransfer" CssClass="basic_ybtn" NavigateUrl="TeamTransfer.aspx"
                Visible="true" runat="server">队长转让</asp:HyperLink>
                        
                           </li>
                       </ul>
                   </div>
                   
                </div>
            
            </div>
       </div>
   </div>
    </form>
</body>
</html>
