<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamImport.aspx.cs" Inherits="Enow.TZB.Web.Manage.Team.TeamImport" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="/Js/CitySelect.js"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="contentbox">
    <div class="firsttable">
            <span class="firsttableT">球队导入</span>
            <table width="98%">
            <tr><td>请根据导入模板上传球队EXCEL文件，<a href="/Template/客户导入模板.xls" target="_blank">点击下载模板</a>。</td></tr>
            <tr><td>球队所在城市：国家<select name="ddlCountry" id="ddlCountry" runat="server">
                                        </select><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                            ErrorMessage="请选择国家!" ControlToValidate="ddlCountry">*</asp:RequiredFieldValidator>省份：<select name="ddlProvince" id="ddlProvince" runat="server">
                                    </select><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ErrorMessage="请选择省份!" ControlToValidate="ddlProvince">*</asp:RequiredFieldValidator>城市：<select id="ddlCity" name="ddlCity" runat="server">
                                        </select><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                            ErrorMessage="请选择城市!" ControlToValidate="ddlCity">*</asp:RequiredFieldValidator>区县：<select id="ddlArea" name="ddlArea" runat="server">
                                        </select><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                            ErrorMessage="请选择区县!" ControlToValidate="ddlArea">*</asp:RequiredFieldValidator>
                </td></tr>
            <tr><td>球队数据上传：<asp:FileUpload ID="fileUpload" runat="server" />
                </td></tr></table>
                <div class="Basic_btn fixed">
            <ul><li>
                <asp:LinkButton ID="linkSave" runat="server" onclick="linkSave_Click"><s class="baochun"></s>保 存</asp:LinkButton>
                </li>
                <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();" hidefocus="true">关 闭
                    >></a></li>
            </ul>
            <div class="hr_10">
            </div>
    </div>
     </div>
    </div><asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    </form>
</body>
</html><script language="javascript" type="text/javascript">
          
           $(function () {
               pcToobar.init({
                   gID: "#ddlCountry",
                   pID: "#ddlProvince",
                   cID: "#ddlCity",
                   xID: "#ddlArea",
                   comID: '',
                   gSelect: '',
                   pSelect: '',
                   cSelect: '',
                   xSelect: ''
               })
           })
</script>
