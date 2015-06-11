<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxDZJob.aspx.cs" Inherits="Enow.TZB.App.CommonPage.AjaxDZJob" %>
<asp:Repeater ID="rptList" runat="server">      
      <ItemTemplate>
      <div class="msg_list qiu_box nobot">
        <ul>
          <li><label>岗位名称：</label><%#Eval("JobName")%></li>
          <!--<li><label>所属城市：</label><%#Eval("ProvinceName")%>-<%#Eval("CityName")%></li>-->
          <li><label>招聘起止时间：</label><%#Eval("StartDate","{0:yyyy-MM-dd}")%>至<%#Eval("EndDate", "{0:yyyy-MM-dd}")%></li>
          <li><label>招聘人数：</label><%#Eval("JobNumber")%></li>
        </ul>
      </div>  
      <div class="qiu-caozuo"><a href="JobDetail.aspx?Id=<%#Eval("Id") %>" class="basic_btn">查看详情</a> <a href="JobSignUp.aspx?Id=<%#Eval("Id") %>" class="basic_btn basic_ybtn">点击报名</a></div>
      </ItemTemplate>
  </asp:Repeater>
