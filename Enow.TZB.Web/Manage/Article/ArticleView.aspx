<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleView.aspx.cs" Inherits="Enow.TZB.Web.Manage.Article.ArticleView" %>

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
                    文章标题：
                </th>
                <td>
                    <asp:Label ID="lblTitle" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    资讯类别：
                </th>
                <td align="left" bgcolor="#ffffff">
                    <asp:Label ID="lblType" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    文章类别：
                </th>
                <td align="left" bgcolor="#ffffff">
                    <asp:Label ID="lblTarget" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    审核状态：
                </th>
                <td>
                    <asp:Label ID="lblStatus" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    操作人：
                </th>
                <td>
                    <asp:Label ID="lblOperatorName" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <th width="150" height="40" align="right">
                    文章内容：
                </th>
                <td>
                    <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
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
