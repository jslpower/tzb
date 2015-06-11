<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobEdit.aspx.cs" Inherits="Enow.TZB.Web.Manage.Job.JobEdit" %>

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
            类型：
            </th>
            <td align="left" bgcolor="#ffffff">
           <asp:DropDownList ID="ddlTypeId" runat="server">
           <asp:ListItem Value="0">招聘</asp:ListItem>
           <asp:ListItem Value="1">招募</asp:ListItem>
           </asp:DropDownList>
            </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    岗位名称：
                </th>
                <td>
                    <asp:TextBox ID="txtJobName" CssClass="inputtext formsize240" runat="server"></asp:TextBox><span
                        class="fontred">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJobName"
                        ErrorMessage="请填写岗位名称！">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    区域限制：
                </th>
                <td align="left" bgcolor="#ffffff">
                <font class="fontred">*</font>国家:
                    <select id="selCountry" class="inputselect" valid="required" errmsg="请选择国家!" name="selCountry">
                    </select>
                    <font class="fontred">*</font>省份:
                    <select id="txtShengFenId" class="inputselect" valid="required" errmsg="请选择省份!" name="txtShengFenId">
                    </select>
                    <font class="fontred">*</font>城市:
                    <select id="txtChengShiId" class="inputselect" valid="required" errmsg="请选择城市!" name="txtChengShiId">
                    </select>
                     <font class="fontred">*</font>区县:
                    <select id="selArea" class="inputselect" valid="required" errmsg="请选择区县!" name="selArea">
                    </select>
                </td>
            </tr>
            <tr>
            <th width="150" height="40" align="right">
            招聘类别：
            </th>
            <td align="left" bgcolor="#ffffff">
            <select class="inputselect" name="ddlJobType">
            <option value="">请选择</option>
                    <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL(Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.JobType),new string[] { "0" }), JobType)%>
                </select>
            </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    招聘开始时间：
                </th>
                <td>
 <asp:TextBox ID="txtStartDate" onfocus="WdatePicker()" runat="server" CssClass="inputtext"
                            valid="isDate" errmsg="请正确填写生日!"></asp:TextBox>
                    <span class="fontred">*</span>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    招聘结束时间：
                </th>
                <td>
                   <asp:TextBox ID="txtEndDate"  runat="server" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtStartDate\')}'})" 
                   errmsg="请正确填写生日!" CssClass="inputtext"></asp:TextBox>
                    <span class="fontred">*</span>
                </td>
            </tr>
            <tr>
            <th width="150" height="40" align="right">
            招聘人数：
            </th>
            <td>
            <asp:TextBox ID="txtNumber" runat="server" CssClass="inputtext"></asp:TextBox>
            </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    岗位职责：
                </th>
                <td>
                    <asp:TextBox ID="txtJobRule" runat="server" class="editText"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    岗位要求：
                </th>
                <td>
                    <asp:TextBox ID="txtJobInfo" runat="server" class="editText"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th width="150" height="40" align="right">
                    岗位奖励：
                </th>
                <td>
                    <asp:TextBox ID="txtJobReward" runat="server" class="editText"></asp:TextBox>
                </td>
            </tr>
        </table>
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
<script language="javascript" type="text/javascript">
    var JobInfo = {
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
         PageInit: function() {
                $(".errmsg").hide();
                pcToobar.init({gID:"#selCountry", pID: "#txtShengFenId", cID: "#txtChengShiId",xID:"#selArea",gSelect:"<%=CountryId %>", pSelect: "<%=ShengFenId %>", cSelect: "<%=ChengShiId %>",xSelect:"<%=AreaId %>" });
            },
    };
    $(document).ready(function () {

        JobInfo.InitEdit();
        JobInfo.PageInit();
        $("#<%= linkBtnSave.ClientID %>").click(function () {
            return JobInfo.CheckForm();
        });
    });

</script>
