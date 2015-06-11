<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayPasswordSet.aspx.cs" Inherits="Enow.TZB.Web.WX.Fund.PayPasswordSet" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>支付密码</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="支付密码" runat="server" />
<div class="warp">

  <div class="msg_tab"  id="n4Tab">

        <div class="TabTitle">
           <ul class="fixed">
              <li><a href="Default.aspx">充值</a></li>
              <li class="active"><a href="PayPassword.aspx">支付密码</a></li>
              <li style="width:34%;"><a href="PayBill.aspx">账单</a></li>
           </ul>
        </div>

        <div class="TabContent">
        <div id="n4Tab_Content1">
 
                <div class="msg_list">
                  <ul>
                            <li><label>支付密码：</label><asp:TextBox ID="txtPayPassword" CssClass="formsize100 input_bk" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写支付密码!" 
                                    ControlToValidate="txtPayPassword">*</asp:RequiredFieldValidator></li>
                            <li><label>确认密码：</label><asp:TextBox ID="txtPayPassword2" CssClass="formsize100 input_bk" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写确认密码!" 
                                    ControlToValidate="txtPayPassword2">*</asp:RequiredFieldValidator><asp:CompareValidator
                                        ID="CompareValidator1" runat="server" ErrorMessage="确认密码与支付密码不一致!" 
                                    ControlToCompare="txtPayPassword" ControlToValidate="txtPayPassword2">*</asp:CompareValidator></li>
                            
                   </ul>
                   
                </div>
                
                <div class="msg_btn">
                    <asp:Button ID="btnSave" runat="server" CssClass="basic_btn" Text="设置支付密码" 
                        onclick="btnSave_Click" /></div>
            
            </div>
         </div>
   
  </div>  
</div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
