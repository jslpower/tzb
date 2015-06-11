<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobDetail.aspx.cs" Inherits="Enow.TZB.Web.WX.Job.JobDetail" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
<title>岗位详情</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen">
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="岗位详情" runat="server" />
   
<div class="warp">
      <div class="msg_list qiu_box job_cont nobot">
        <ul>
          <li><label>岗位名称：</label><asp:Literal ID="ltrJobName" runat="server"></asp:Literal></li>
          <!--<li><label>所属城市：</label><asp:Literal ID="ltrCity" runat="server"></asp:Literal></li>-->
          <li><label>招聘起止时间：</label><asp:Literal ID="ltrDate" runat="server"></asp:Literal></li>
          <li><label>招聘人数：</label><asp:Literal ID="ltrJobNumber" runat="server"></asp:Literal></li>
          <li><label>岗位职责：</label></li>
          <li class="last"><p><asp:Literal ID="ltrJobRule" runat="server"></asp:Literal></p></li>
          <li><label>岗位要求：</label></li>
          <li class="last"><p><asp:Literal ID="ltrJobInfo" runat="server"></asp:Literal></p></li>
          <li><label><asp:Literal ID="ltrRewardTitle" runat="server"></asp:Literal>：</label></li>
          <li class="last"><p><asp:Literal ID="ltrJobReward" runat="server"></asp:Literal></p></li>
        </ul>
      </div>  
      <div class="msg_btn"><a href="JobSignUp.aspx?Id=<%=Request.QueryString["Id"] %>" class="basic_btn">报  名</a></div>
</div>
    </form>
</body>
</html>
