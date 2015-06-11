<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZZJob.aspx.cs" Inherits="Enow.TZB.Web.WX.Job.ZZJob" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
<title>站长报名</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen">
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="站长报名" runat="server" />
<div class="search_head">
   <div class="search_form">
   <asp:TextBox ID="txtKeyWords" runat="server" CssClass="input_txt floatL" Text="岗位搜索" onfocus="if(this.value=='岗位搜索'){this.value='';}" onBlur="if(this.value==''){this.value='岗位搜索';}"></asp:TextBox><asp:Button
       ID="btnSearch" CssClass="input_btn icon_search_i floatR" runat="server" 
           onclick="btnSearch_Click" />
   </div>
</div>
<div class="warp s_warp">
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
</div>
    </form>
</body>
</html>
