<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jobView.aspx.cs" Inherits="Enow.TZB.Web.Manage.Job.jobView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="contentbox">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tableInfo">
            <tr>
                <th width="150" height="40" align="right">
                    职位名称：
                </th>
                <td>
                    <asp:Label ID="lblJobName" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    地区：
                </th>
                <td align="left" bgcolor="#ffffff">
                    <asp:Label ID="lblArea" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    招聘开始日期：
                </th>
                <td align="left" bgcolor="#ffffff">
                    <asp:Label ID="lblStartDate" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    招聘结束日期：
                </th>
                <td>
                    <asp:Label ID="lblEndDate" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    招聘人数：
                </th>
                <td>
                    <asp:Label ID="lblNum" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    招聘类型：
                </th>
                <td>
                    <asp:Label ID="lblJobType" runat="server" CssClass="inputtext formsize240"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    前台发布：
                </th>
                <td>
                    <asp:Label ID="lblIsValid" runat="server" CssClass="inputtext formsize240"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    岗位职责：
                </th>
                <td>
                    <asp:Literal ID="ltrJobRule" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    招聘要求：
                </th>
                <td>
                    <asp:Literal ID="ltrJonInfo" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    岗位奖励：
                </th>
                <td>
                    <asp:Literal ID="ltrJobReward" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <div class="Basic_btn fixed">
            <ul>
                <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"
                    hidefocus="true">关 闭 >></a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    </form>
</body>
</html>
