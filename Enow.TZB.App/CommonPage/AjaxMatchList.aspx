<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMatchList.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxMatchList" %>
<asp:Repeater ID="rptList" runat="server">
        <ItemTemplate>
        <li>
      <div class="msg_list qiu_box nobot">
        <ul>
          <li><label>赛事名称：</label><%#Eval("MatchName")%></li>
          <li><label>赛事时间：</label><%#Eval("BeginDate","{0:yyyy-MM-dd HH:mm}")%></li>
          <li>
             <div class="lie"><label>举办城市：</label><%#Eval("CityName")%></div>
             <div class="lie2"><label>报名费：</label><%#Eval("RegistrationFee","{0:C2}")%> 元</div>
          </li>
          <li>
             <div class="lie"><label>报名队数：</label><%#Eval("SignUpNumber")%>/<%#Eval("TeamNumber")%></div>
             <div class="lie2"><label>报名人数：</label><%#Eval("PlayersMin")%>/<%#Eval("PlayersMax")%></div>
          </li>
        </ul>
      </div>
  
      <div class="qiu-caozuo"><a href="Detail.aspx?Id=<%#Eval("Id") %>" class="basic_btn">查看详情</a> <a href="SignUp.aspx?Id=<%#Eval("Id") %>" class="basic_btn basic_ybtn">点击报名</a></div>
        </li>
        </ItemTemplate>
    </asp:Repeater>