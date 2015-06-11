<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxTeamList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxTeamList" %>
      <asp:Repeater ID="rptList" runat="server">
      <ItemTemplate>
         <li> 
            <div class="item-img"><a href="MyTeamInfo.aspx?TeamId=<%#Eval("Id") %>"><img src="<%#System.Configuration.ConfigurationManager.AppSettings["Uploadimgurl"]+Enow.TZB.Utility.PhotoThumbnail.F1(Eval("TeamPhoto").ToString(),80,111,DIRPATH)%>"/></a></div>
            <div class="item-box" style=" padding:0px;margin-left: 110px;">
               <div class="btn" style=" width:65px">
                   <a href="SignUp.aspx?TeamId=<%#Eval("Id") %>" style=" width:65px" class="basic_btn">加入</a>
                   <a href="/WX/AboutWar/AboutWarAdd.aspx?Teamid=<%#Eval("Id") %>" style=" width:65px" class="basic_ybtn">约战</a>
                   <a href="javascript:void(0);" data-jid="<%#Eval("Id") %>" style=" width:65px" class="basic_btn Agzbtn"><%#Selgzyf(Eval("Id").ToString())%></a>
                 </div>
                <dl>
                    <dt><a href="MyTeamInfo.aspx?TeamId=<%#Eval("Id") %>"><%#Eval("TeamName") %></a></dt>
                    <dd class="date"><%#Eval("CityName")%>-<%#Eval("AreaName")%></dd>
                    <dd class="date">创建于：<%#Eval("IssueTime","{0:yyyy-MM-dd}") %></dd>
                    <dd class="date">创始人：<%#Eval("MemberName")%></dd>
                </dl>
            </div>
         </li>
         </ItemTemplate>
      </asp:Repeater>