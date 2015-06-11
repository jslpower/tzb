<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master"
    AutoEventWireup="true" CodeBehind="ChangePayPwd.aspx.cs" Inherits="Enow.TZB.WebForm.My.ChangePayPwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
    <asp:PlaceHolder ID="phPayPwdEdit" runat="server" Visible="false">
        <div class="user_box">
            <h3>
                修改支付密码</h3>
            <div class="reg_form">
                <ul class="botline">
                    <li><span class="name">当前密码</span>
                        <asp:TextBox ID="txtOldPayPwd" runat="server" CssClass="input_bk formsize225" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="原密码不可为空!"
                            ControlToValidate="txtOldPayPwd"><span class="font_yellow" >*</span><span class="error_txt">当前密码不可为空</span></asp:RequiredFieldValidator>
                    </li>
                    <li><span class="name">支付密码</span><asp:TextBox ID="txtNewPayPwd" runat="server" CssClass="input_bk formsize225"
                        TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="支付密码不可为空!"
                            ControlToValidate="txtNewPayPwd"><span class="font_yellow" >*</span><span class="error_txt">支付密码不可为空</span></asp:RequiredFieldValidator></li>
                    <li><span class="name">确认密码</span><asp:TextBox ID="txtSureNewPayPwd" runat="server"
                        CssClass="formsize225 input_bk" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="确认密码不可为空!"
                            ControlToValidate="txtSureNewPayPwd"><span class="font_yellow" >*</span><span class="error_txt">确认密码不可为空</span></asp:RequiredFieldValidator>
                        <asp:CompareValidator
                            ID="CompareValidator1" runat="server" ErrorMessage="确认密码与支付密码不一致！" ControlToCompare="txtNewPayPwd"
                            ControlToValidate="txtSureNewPayPwd"><span class="font_yellow" >*</span><span class="error_txt">确认密码与支付密码不一致</span></asp:CompareValidator></li>
                </ul>
                <div class="reg_btn pb10 mt10">
                    <asp:Button ID="btnEdit" runat="server" Text="修改支付密码" OnClick="btnEdit_Click" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phPaypwdAdd" runat="server" Visible="false">
        <div class="user_box">
            <h3>
                设置支付密码</h3>
            <div class="reg_form">
                <ul class="botline">
                    <li><span class="name">支付密码</span><asp:TextBox ID="txtPayPwd" runat="server" CssClass="input_bk formsize225"
                        TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="支付密码不可为空!"
                            ControlToValidate="txtPayPwd"><span class="font_yellow" >*</span><span class="error_txt">支付密码不可为空</span></asp:RequiredFieldValidator></li>
                    <li><span class="name">确认密码</span><asp:TextBox ID="txtSurePayPwd" runat="server"
                        CssClass="formsize225 input_bk" TextMode="Password"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="确认密码不可为空!"
                            ControlToValidate="txtSurePayPwd"><span class="font_yellow" >*</span><span class="error_txt">确认密码不可为空</span></asp:RequiredFieldValidator>
                        <asp:CompareValidator
                            ID="CompareValidator2" runat="server" ErrorMessage="确认密码与支付密码不一致！" ControlToCompare="txtPayPwd"
                            ControlToValidate="txtSurePayPwd"><span class="font_yellow" >*</span><span class="error_txt">确认密码与支付密码不一致</span></asp:CompareValidator></li>
                </ul>
                <div class="reg_btn pb10 mt10">
                    <asp:Button ID="btnAdd" runat="server" Text="设置支付密码" OnClick="btnAdd_Click" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
