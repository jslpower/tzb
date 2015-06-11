<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisbandReason.aspx.cs"
    Inherits="Enow.TZB.WebForm.My.DisbandReason" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>解散原因</title>
    <link href="/Css/style.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="boxy">
        <div class="boxy_title">
            解散原因<a href="javascript:parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();"
                class="close-btn"></a></div>
        <div class="reg_form">
            <ul>
                <li>
                    <label>
                        解散原因：</label><asp:TextBox ID="txtDisbandReason" runat="server" CssClass="formsize225 input_bk" Height="120"
                            TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                runat="server" ErrorMessage="请填写解散原因" ControlToValidate="txtDisbandReason">*</asp:RequiredFieldValidator></li>
            </ul>
        </div>
        <div>
            <asp:Button ID="btnCheck" runat="server" CssClass="basic_btn" Text="确  认" 
                onclick="btnCheck_Click" /></div>
    </div>
    </form>
</body>
</html>
