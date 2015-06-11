<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.Web.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>铁子帮管控系统</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>
</head>
<body style="background:#e0e6e7">
    <form id="form1" runat="server">
     <div class="loginbar">
        <div class="login_logo" style="text-align:center"><h1>铁子帮管控系统</h1></div>
        <div class="login-form fixed">
              <div class="left_form">
                  <ul>
                         <li><i>用户名</i>
                        <asp:TextBox ID="txtUid" Text="" CssClass="inputbg formsize180" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入用户名"
                            ControlToValidate="txtUid">*</asp:RequiredFieldValidator></li>
                         <li><i>密　码</i>
                       <asp:TextBox TextMode="Password" ID="txtPwd" CssClass="inputbg formsize180" Text="请输入用户密码"
                            runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入密码"
                            ControlToValidate="txtPwd">*</asp:RequiredFieldValidator></li>
                  </ul>
              </div>
              <div class="loginbtn">
                  <asp:Button ID="btnSave" CssClass="loginbutton" runat="server" Text="登 录" 
                      onclick="linkBtnLogin_Click" /></div>
        </div>
  </div>
  
  <div class="login_foot">浙江铁子帮体育交流策划发展有限公司  备案证编号：浙ICP备14039964号-1  许可证号：L-ZJ01409</div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    </form>
    <script type="text/javascript">
        $(function () {
            $("#txtUid,#txtPwd").keypress(function (e) {
                if (e.keyCode == 13) {
                    WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("linkBtnLogin", "", true, "", "", false, true));
                }
            });
        });
    </script>
</body>
</html>
