<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="TeamView.aspx.cs"
    Inherits="Enow.TZB.Web.Manage.Team.TeamView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <span class="firsttableT">球队信息</span>
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <th width="100">
                        球队名称：
                    </th>
                    <td>
                        <asp:Label ID="lblTeamName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        球队创始人：
                    </th>
                    <td>
                        <asp:Label ID="lblMemberName" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <th width="100">
                        创始人电话：
                    </th>
                    <td>
                        <asp:Label ID="lblMobile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        所在地区：
                    </th>
                    <td>
                        <asp:Label ID="lblAreaName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        申请日期：
                    </th>
                    <td>
                        <asp:Label ID="lblIssueDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        申请状态：
                    </th>
                    <td>
                        <asp:Label ID="lblState" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        球队介绍：
                    </th>
                    <td>
                        <asp:Literal ID="LtrTeamInfo" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        球队照片：
                    </th>
                    <td>
                        <asp:Literal ID="LtrTeamPhoto" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div class="Basic_btn fixed">
            <ul>
               <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"
                    hidefocus="true">关 闭 >></a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
