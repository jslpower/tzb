<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMatchSet.aspx.cs" Inherits="Enow.TZB.Web.CommonPage.AjaxMatchSet" %>
 <asp:Repeater ID="rptList" runat="server">
        <ItemTemplate>
      <div class="msg_list qiu_box nobot">
        <ul id="linelist">
          <li style="padding:25px 0px 25px 0px"><strong><%#Eval("MatchName")%></strong></li>
          <li><label>报名时间：</label><%#Eval("SignBeginDate","{0:yyyy-MM-dd}")%>至<%#Eval("SignEndDate","{0:yyyy-MM-dd}")%></li>
          <li><label>比赛时间：</label><%#Eval("BeginDate","{0:yyyy-MM-dd}")%>至<%#Eval("EndDate","{0:yyyy-MM-dd}")%></li>
          <li><label>举办城市：</label><%#Eval("CityName")%></li>
          <li>
          <div class="lie"><label>保证金：</label><%#Eval("RegistrationFee","{0:C2}")%> 元</div> 
          <div class="lie2"><label>报名费：</label><%#Eval("EarnestMoney", "{0:C2}")%> 元</div></li>
          <li  style="padding:25px 0px 25px 10px">
             <!-- <div class="lie"><label>报名队数：</label><%#Eval("SignUpNumber")%>/<%#Eval("TeamNumber")%></div>  
             <div class="lie2"><label>报名人数：</label><%#Eval("PlayersMin")%>至<%#Eval("PlayersMax")%>人</div> !-->
             每队报名人数：</label><%#Eval("PlayersMin")%>-<%#Eval("PlayersMax")%>人
          </li>
        </ul>
      </div>
  
      <div class="qiu-caozuo"><a href="Detail.aspx?Id=<%#Eval("Id") %>" class="basic_btn">查看详情</a><%#SignUp(Eval("Id").ToString(), Convert.ToDateTime(Eval("SignBeginDate")), Convert.ToDateTime(Eval("SignEndDate")))%></div>
        
        </ItemTemplate>
    </asp:Repeater>
