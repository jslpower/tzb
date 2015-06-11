<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaEdit.aspx.cs" Inherits="Enow.TZB.Web.Manage.basic.AreaEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                <th width="150" height="40" align="right">
                    地区 ：
                </th>
                <td align="left" bgcolor="#ffffff">
                    <font class="fontred"></font>国家:
                    <select id="selCountry" name="selCountry">
                    </select>
                    <font class="fontred"></font>省份:
                    <select id="selProvince" name="selProvince">
                    </select>
                    <font class="fontred"></font>城市:
                    <select id="selCity" name="selCity">
                    </select>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    区县名称：
                </th>
                <td>
                    <asp:TextBox ID="txtAreaName" CssClass="inputtext formsize240" runat="server">
                    </asp:TextBox><span class="fontred">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAreaName"
                        ErrorMessage="请填写国家名称！">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    区县简拼：
                </th>
                <td>
                    <asp:TextBox ID="txtJP" runat="server" CssClass="inputtext formsize240">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    区县全拼：
                </th>
                <td>
                    <asp:TextBox ID="txtQP" runat="server" CssClass="inputtext formsize240">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    英文名称：
                </th>
                <td>
                    <asp:TextBox ID="txtEnName" runat="server" CssClass="inputtext formsize240">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="Basic_btn fixed">
            <ul>
                <li>
                    <asp:LinkButton ID="linkBtnSave" runat="server" onclick="linkBtnSave_Click">保 存 >></asp:LinkButton>
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
<script language="javascript" type="text/javascript">
    $(function () {

        pcToobar.init({
            gID: "#selCountry",
            pID: "#selProvince",
            cID: "#selCity",
            gSelect: "<%=CountryId %>",
            pSelect: "<%=ProvinceID %>",
            cSelect: "<%=CityId %>"

        });
    });
</script>
