<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HonorManage.aspx.cs" Inherits="Enow.TZB.Web.Manage.Memeber.HonorManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>会员荣誉管理</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="contentbox"><div class="firsttable">
        <table width="100%" border="1" cellspacing="0" cellpadding="0">                 
            <tr>
                <th width="150" height="40" align="right">
                    姓名：
                </th><td>
                    <asp:Literal ID="ltrContactName" runat="server"></asp:Literal></td>
                <th width="150" height="40" align="right">
                    手机号码：</th>
                <td>
                    <asp:Literal ID="ltrMobile" runat="server"></asp:Literal></td>
            </tr> 
            <tr>
                <th width="150" height="40" align="right">
                    所属球队：</th>
                <td colspan="3">
                    <asp:Literal ID="ltrTeamName" runat="server"></asp:Literal></td>
            </tr> 
            <tr>
                <th width="150" height="40" align="right">
                    现有积分：</th>
                <td>
                   <asp:Literal ID="ltrTotalInter" runat="server"></asp:Literal></td>
                <th width="150" height="40" align="right">
                    现有荣誉值：</th>
                <td>
                    <asp:Literal ID="ltrHonorNumber" runat="server"></asp:Literal></td>
            </tr> <tr>
            <th width="150" height="40" align="right">
                操作类型：
            </th>
            <td align="left" bgcolor="#ffffff" colspan="3">
           <asp:DropDownList ID="ddlTypeId" runat="server">
           <asp:ListItem Value="3">奖励</asp:ListItem>
           <asp:ListItem Value="4">惩罚</asp:ListItem>
           </asp:DropDownList>
            </td>
            </tr>   <tr>
                <th width="150" height="40" align="right">
                    荣誉：</th>
                <td colspan="3">
                    <asp:TextBox ID="txtHonorNumber" runat="server" CssClass=" formsize80 input-txt" MaxLength="6"></asp:TextBox>
                      <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="荣誉值必须大于0！"
                            ControlToValidate="txtHonorNumber" MaximumValue="100000" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    说明：</th>
                <td colspan="3">
                    <asp:TextBox ID="txtRemark" runat="server" CssClass=" formsize240 input-txt"></asp:TextBox>
                </td>
            </tr>
            </table>
         </div>
        <div class="Basic_btn fixed">
            <ul>
                <li>
                    <asp:LinkButton ID="linkBtnSave" runat="server" onclick="linkBtnSave_Click" >保 存 >></asp:LinkButton>
                </li>
                <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"
                    hidefocus="true">返 回 >></a></li>
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
