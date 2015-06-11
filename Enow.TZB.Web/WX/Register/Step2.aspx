<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step2.aspx.cs" Inherits="Enow.TZB.Web.WX.Register.Step2" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>铁丝注册 step 2</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="铁丝注册" runat="server" />
     <div class="warp">
        <div class="msg_tab" id="n4Tab">
            <div class="TabTitle">
                <ul class="fixed">
                <li id="n4Tab_Title0"><a>账号</a></li>
                    <li id="n4Tab_Title1" class="active"><a>实名</a></li>
                    <li id="n4Tab_Title2" style="width: 34%;"><a>偏好</a></li>
                </ul>
            </div>
            <div class="TabContent">                
                <div id="n4Tab_Content1">
                    <div class="msg_list">
                        <ul>
                             <li class="tishi">铁丝本人的真实姓名、身份证号、通讯地址作为铁丝实名认证的依据，只有通过实名认证的铁丝，才能享受完整的铁丝特权，请认真填写。</li>
                            <li>
                                <label>
                                    真实姓名：</label><input id="txtContactName" name="txtContactName" type="text" runat="server" class="input_bk formsize150" /><asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtContactName" 
                                    ErrorMessage="请填写真实姓名">*</asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label>
                                    身份证号：</label><input id="txtPersonalId" name="txtPersonalId" type="text" runat="server" class="input_bk formsize150" /><asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPersonalId" 
                                    ErrorMessage="请填写身份证号码！">*</asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label>
                                    通讯地址：</label><input id="txtAddress" name="txtAddress" type="text" runat="server" class="input_bk formsize150" /><asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress" 
                                    ErrorMessage="请填写通讯地址！">*</asp:RequiredFieldValidator>
                            </li>    
                        </ul>
                    </div>
                    <div class="msg_btn">
                                            <asp:Button CssClass="basic_btn" ID="Button1" runat="server" Text="下一步" 
                                onclick="btnSave_Click" />
                </div>
            </div>
        </div>
    </div>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
