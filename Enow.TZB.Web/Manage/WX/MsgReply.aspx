<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgReply.aspx.cs" Inherits="Enow.TZB.Web.Manage.WX.MsgReply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" content="IE=EmulateIE8" http-equiv="X-UA-Compatible" />
    <title>留言回复</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="contentbox">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tableInfo">
            <tr>
                <th width="50" height="40" align="right">
                    留言人：
                </th>
                <td><asp:Literal ID="ltrName" runat="server"></asp:Literal></td>
                <th>
                    留言时间：</th>
                <td><asp:Literal ID="ltrTime" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    留言内容：</th>
                <td colspan="3"><asp:Literal ID="ltrInfo" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    回复内容：</th>
                <td colspan="3">
                    <asp:TextBox ID="txtReplyInfo" runat="server" Height="100px" 
                        TextMode="MultiLine" Width="80%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtReplyInfo" ErrorMessage="请填写回复内容！">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <div class="Basic_btn fixed">
            <ul>
                <li>
                    <asp:LinkButton ID="linkBtnSave" runat="server" OnClick="linkBtnSave_Click">回 复 >></asp:LinkButton>
                </li>
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
