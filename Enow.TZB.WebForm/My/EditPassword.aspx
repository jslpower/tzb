<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master"
    AutoEventWireup="true" CodeBehind="EditPassword.aspx.cs" Inherits="Enow.TZB.WebForm.My.EditPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
    <div class="user_box">
        <h3>
            修改密码</h3>
        <div class="reg_form">
            <ul class="botline">
                <li><span class="name">原密码</span>
                    <asp:TextBox ID="txtOldPassword" runat="server" CssClass="input_bk formsize225" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="原密码不可为空!"
                        ControlToValidate="txtOldPassword"><span class="font_yellow" >*</span><span class="error_txt">原密码不可为空</span></asp:RequiredFieldValidator>
                </li>
                <li><span class="name">新密码</span><asp:TextBox ID="txtNewPassword" runat="server"
                    CssClass="input_bk formsize225" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="新密码不可为空!"
                        ControlToValidate="txtNewPassword"><span class="font_yellow" >*</span><span class="error_txt">新密码不可为空</span></asp:RequiredFieldValidator></li>
                <li><span class="name">确认新密码</span><asp:TextBox ID="txtSurePassword" runat="server"
                    CssClass="formsize225 input_bk" TextMode="Password"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="确认密码不可为空!"
                        ControlToValidate="txtSurePassword"><span class="font_yellow" >*</span><span class="error_txt">确认密码不可为空</span></asp:RequiredFieldValidator>
                    <asp:CompareValidator
                        ID="CompareValidator1" runat="server" ErrorMessage="确认密码与密码不一致！" ControlToCompare="txtNewPassword"
                        ControlToValidate="txtSurePassword"><span class="font_yellow" >*</span><span class="error_txt">确认密码与新密码不一致</span></asp:CompareValidator></li>
            </ul>
            <div class="reg_btn pb10 mt10">
                <asp:Button ID="btnSave" runat="server" Text="确认" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>
