<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamerCheck.aspx.cs" Inherits="Enow.TZB.WebForm.My.TeamerCheck" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>球员审核</title>
    <link href="/Css/style.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="boxy">
        <div class="boxy_title">
            球员审核<a href="javascript:parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();"
                class="close-btn"></a></div>
        <div class="msg_list boxy_form">
            <div class="reg_form">
                <ul>
                    <li>
                        <label>
                            球衣号：</label><input id="txtQYHM" name="txtQYHM" type="text" runat="server" maxlength="4"
                                class="formsize50 input_bk" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                    runat="server" ErrorMessage="球衣号码只能为数字" ControlToValidate="txtQYHM" ValidationExpression="\d{1,4}">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写球衣号码" ControlToValidate="txtQYHM">*</asp:RequiredFieldValidator></li>
                    <li>
                        <label>
                            场上位置：</label><select id="SQWZ" name="SQWZ">
                                <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                    (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.球员位置),
                                                               new string[] { }), MeberSQWZ.ToString())
                                %>
                            </select></li>
                </ul>
            </div>
        </div>
        <div class="padd20">
            <input type="hidden" name="hidTMId" id="hidTMId" runat="server" />
            <asp:Button ID="btnCheck" runat="server" CssClass="basic_btn" Text="通 过" OnClick="btnCheck_Click" /></div>
        <div class="padd20 paddT0">
            <asp:Button ID="btnRefuse" runat="server" CssClass="basic_btn basic_ybtn" Text="婉 拒"
                OnClick="btnRefuse_Click" /></div>
    </div>
    </form>
</body>
</html>
