<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master"
    AutoEventWireup="true" CodeBehind="SiteSet.aspx.cs" Inherits="Enow.TZB.WebForm.My.SiteSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
    <div class="user_box">
        <h3>
            偏好设置</h3>
        <div class="reg_form">
            <ul>
                <li><span class="name">球场位置</span><asp:TextBox ID="txtQCWZ" runat="server" CssClass="input_bk formsize225"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="required1" runat="server" ErrorMessage="请填写球场位置!"
                        ControlToValidate="txtQCWZ"><span class="font_yellow" >*</span><span class="error_txt">请填写球场位置</span></asp:RequiredFieldValidator>
                </li>
                <li><span class="name">常用球衣号</span><asp:TextBox ID="txtQYHM" runat="server" CssClass="input_bk formsize225"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写球衣号码!"
                        ControlToValidate="txtQYHM"><span class="font_yellow" >*</span><span class="error_txt">请填写球衣号码</span></asp:RequiredFieldValidator>
                    </li>
                <li><span class="name">常用装备品牌</span><asp:TextBox id="txtZBPP" runat="server" CssClass="input_bk formsize225"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写常用装备品牌!"
                        ControlToValidate="txtZBPP"><span class="font_yellow" >*</span><span class="error_txt">请填写常用装备品牌</span></asp:RequiredFieldValidator>
                    </li>
                <li><span class="name">每周踢球次数</span><asp:TextBox ID="txtMZCS" runat="server" CssClass="input_bk formsize225"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请填写每周踢球次数!"
                        ControlToValidate="txtMZCS"><span class="font_yellow" >*</span><span class="error_txt">请填写每周踢球次数</span></asp:RequiredFieldValidator>
                    </li>
                <li><span class="name">关注球队</span><asp:TextBox ID="txtGZQD" runat="server" CssClass="input_bk formsize225"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请填写关注球队!"
                        ControlToValidate="txtGZQD"><span class="font_yellow" >*</span><span class="error_txt">请填写关注球队</span></asp:RequiredFieldValidator>
                    </li>
            </ul>
            <div class="reg_btn pb10 mt10">
                <asp:Button ID="btnSave" runat="server" Text="确认" OnClick="btnSave_Click" /></div>
        </div>
    </div>
</asp:Content>
