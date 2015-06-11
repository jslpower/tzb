<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RegisterMaster.Master"
    AutoEventWireup="true" CodeBehind="Step2.aspx.cs" Inherits="Enow.TZB.WebForm.register.Step2" %>
       <asp:Content ID="ContentTop" ContentPlaceHolderID="cph_Top" runat="server">
    
     <div class="reg_step">
             <ul class="fixed">
              <li><img src="../images/u-step02_1.gif"><p class="paddL5">创建账号</p></li>
                <li><img src="../images/u-step02_on.gif"><p class="cent font_yellow">实名设置</p></li>
                <li><img src="../images/u-step03.gif"><p class="paddR5 t_R">偏好设置</p></li>
             </ul>
          </div>
    </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <div class="reg_form">
        <ul>
            <li><span class="name">真实姓名</span><asp:TextBox ID="txt_ContractName" runat="server"
                CssClass="formsize225 input_bk"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="请填写真实姓名!" ControlToValidate="txt_ContractName"><span class="font_yellow" >*</span><span class="error_txt">请填写真实姓名</span></asp:RequiredFieldValidator></li>
            <li><span class="name">身份证号</span><asp:TextBox ID="txt_PersonalId" runat="server"
                CssClass="formsize225 input_bk"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ErrorMessage="请填写身份证号码!" ControlToValidate="txt_PersonalId"><span class="font_yellow" >*</span><span class="error_txt">请填写身份证号码</span></asp:RequiredFieldValidator></li>
            <li><span class="name">详细地址</span><asp:TextBox ID="txt_Address" runat="server" CssClass="formsize400 input_bk"></asp:TextBox><asp:RequiredFieldValidator
                ID="RequiredFieldValidator3" runat="server" ErrorMessage="请填写详细地址!" ControlToValidate="txt_Address"><span class="font_yellow" >*</span><span class="error_txt">请填写详细地址</span></asp:RequiredFieldValidator>
            </li>
        </ul>
        <div class="reg_btn pb10 mt10">
            <asp:Button ID="btnSave" runat="server" Text=" 下 一 步 " 
                onclick="btnSave_Click" />
        </div>
    </div>
</asp:Content>
