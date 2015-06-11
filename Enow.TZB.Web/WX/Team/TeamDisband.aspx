<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamDisband.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.TeamDisband" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>解散球队</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:UserHome ID="UserHome1" Userhometitle="解散球队" runat="server" />
    <div class="warp">
        <div class="msg_list qiu_box">
            <ul>
                <li class="last">
                    <label>
                        解散原因：</label><br />
                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入解散原因。" ControlToValidate="txtRemark">*</asp:RequiredFieldValidator></li>
            </ul>
            <div class="mt20 padd20">
                <input type="hidden" name="hidTeamId" id="hidTeamId" runat="server" /><input type="hidden"
                    name="hidTeamMemberId" id="hidTeamMemberId" runat="server" />
                <asp:Button CssClass="basic_btn" ID="btnSave" runat="server" Text=" 确  定 " OnClick="btnSave_Click" /></div>
        </div>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    </form>
</body>
</html>
