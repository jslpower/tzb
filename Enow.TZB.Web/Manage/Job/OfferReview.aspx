<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfferReview.aspx.cs" Inherits="Enow.TZB.Web.Manage.Job.OfferReview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="contentbox">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tableInfo">
            <tr>
                <th width="150" height="40" align="right">
                    申请职位：
                </th>
                <td>
                    <asp:Label ID="lblJobName" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    用户名：
                </th>
                <td align="left" bgcolor="#ffffff">
                    <asp:Label ID="lblUserName" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    手机号码：
                </th>
                <td align="left" bgcolor="#ffffff">
                    <asp:Label ID="lblPhone" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    姓名：
                </th>
                <td>
                    <asp:Label ID="lblContactName" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    身份证号：
                </th>
                <td>
                    <asp:Label ID="lblPersonalId" CssClass="inputtext formsize240" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    地址：
                </th>
                <td>
                    <asp:Label ID="lblAddress" runat="server" CssClass="inputtext formsize240"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    Email：
                </th>
                <td>
                    <asp:Label ID="lblEmail" runat="server" CssClass="inputtext formsize240"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    工作年限：
                </th>
                <td>
                    <asp:Label ID="lblWorkYear" runat="server" CssClass="inputtext formsize240"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    球龄：
                </th>
                <td>
                    <asp:Label ID="lblBallYear" runat="server" CssClass="inputtext formsize240"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    专业：
                </th>
                <td>
                    <asp:Label ID="lblSpecialty" runat="server" CssClass="inputtext formsize240"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <th width="150" height="40" align="right">
                    报名感言：
                </th>
                <td>
                    <asp:Literal ID="ltrBMGY" runat="server" ></asp:Literal>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    个人简历：
                </th>
                <td>
                    <asp:Literal ID="ltrApplyInfo" runat="server"></asp:Literal>
                </td>
            </tr>
             <tr>
                <th width="150" height="40" align="right">
                    应聘状态：
                </th>
                <td>
                    <select id="Select1" name="selState">
                    <%=InitOfferState(OfferState)%>
                    </select>
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
      var OfferInfo = {
        InitEdit: function () {
            $("#tableInfo").find(".editText").each(function () {
                KEditer.init($(this).attr("id"), {
                    resizeMode: 0,
                    items: keSimple,
                    height: $(this).attr("data-height") == undefined ? "300px" : $(this).attr("data-height"),
                    width: $(this).attr("data-width") == undefined ? "660px" : $(this).attr("data-width")
                });
            });
        },
        CheckForm: function () {
            KEditer.sync();

            return true;
        },
    };
    $(document).ready(function () {

        OfferInfo.InitEdit();
       
        $("#<%= linkBtnSave.ClientID %>").click(function () {
            return OfferInfo.CheckForm();
        });
    });

</script>
