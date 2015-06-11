<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxPatteamList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxPatteamList" %>
      <asp:Repeater ID="rptList" runat="server">
      <ItemTemplate>
         <li> 
            <div class="item-img"><img src="<%#System.Configuration.ConfigurationManager.AppSettings["Uploadimgurl"]+Enow.TZB.Utility.PhotoThumbnail.F1(Eval("TeamPhoto").ToString(),80,111,DIRPATH)%>"/></div>
            <div class="item-box">
              
               <div class="btn">
                   <a href="SignUp.aspx?TeamId=<%#Eval("Id") %>" class="basic_btn">加入</a>
                   <a href="/WX/AboutWar/AboutWarAdd.aspx?Teamid=<%#Eval("Id") %>" class="basic_ybtn">约战</a>
                   <a href="SignUp.aspx?TeamId=<%#Eval("Id") %>" class="basic_btn">取消关注</a>
                 </div>
                <dl>
                    <dt><%#Eval("TeamName") %></dt>
                    <dd class="date"><%#Eval("CityName")%>-<%#Eval("AreaName")%></dd>
                    <dd class="date">创建于：<%#Eval("IssueTime","{0:yyyy-MM-dd}") %></dd>
                    <dd class="date">创始人：<%#Eval("MemberName")%></dd>
                </dl>
            </div>
         </li>
         </ItemTemplate>
      </asp:Repeater>
