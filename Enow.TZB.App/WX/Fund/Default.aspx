<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.Web.WX.Fund.Default" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>钱包</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="钱包" runat="server" />

<div class="warp">

  <div class="msg_tab"  id="n4Tab">

        <div class="TabTitle">
           <ul class="fixed">
              <li class="active"><a href="Default.aspx">充值</a></li>
              <li><a href="PayPassword.aspx">支付密码</a></li>
              <li style="width:34%;"><a href="PayBill.aspx">账单</a></li>
           </ul>
        </div>

        <div class="TabContent">
        
			<div id="n4Tab_Content0">
            
                <div class="msg_list">
                  <ul>
                            <li><label>充值账号：</label><asp:Literal ID="ltrUserName" runat="server"></asp:Literal> <!--<a href="" class="gai_btn">给好友充值</a>--></li>
                            <li><label>充值数量：</label><asp:TextBox ID="txtMoney" CssClass="formsize100 input_bk" MaxLength="6" runat="server"></asp:TextBox>铁丝币<asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写充值数量!" 
                                    ControlToValidate="txtMoney">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator1" runat="server" 
                                    ErrorMessage="充值数量只能为数字!" ControlToValidate="txtMoney" 
                                    ValidationExpression="^\d+(\.\d+)?$">*</asp:RegularExpressionValidator></li>
                            <li><label>应付金额：</label><span id="spanMoney">0</span>元</li>
                            
                   </ul>
                   
                </div>
                
                <div class="msg_btn">
                    <asp:Button ID="btnSave" runat="server" CssClass="basic_btn" Text="立即充值" 
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
<script language="javascript" type="text/javascript">
    $(function () {
        $("#<%=txtMoney.ClientID %>").change(function () {
            $("#spanMoney").text($("#<%=txtMoney.ClientID %>").val());
        })
    });
</script>