<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/RegisterMaster.Master"
    AutoEventWireup="true" CodeBehind="Step3.aspx.cs" Inherits="Enow.TZB.WebForm.register.Step3" %>

<asp:Content ID="ContentTop" ContentPlaceHolderID="cph_Top" runat="server">
    <div class="reg_step">
        <ul class="fixed">
            <li>
                <img src="../images/u-step02_1.gif"><p class="paddL5">
                    创建账号</p>
            </li>
            <li>
                <img src="../images/u-step03-2.gif"><p class="cent">
                    实名设置</p>
            </li>
            <li>
                <img src="../images/u-step03_on.gif"><p class="paddR5 t_R font_yellow">
                    偏好设置</p>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <div class="reg_form">
        <div class="reg_form">
            <ul>
                <li><span class="name">球场位置</span><asp:TextBox ID="txtQCWZ" runat="server" CssClass="input_bk formsize225"></asp:TextBox>
                </li>
                <li><span class="name">常用球衣号</span><asp:TextBox ID="txtQYHM" runat="server" CssClass="input_bk formsize225"></asp:TextBox>
                </li>
                <li><span class="name">常用装备品牌</span><asp:TextBox ID="txtZBPP" runat="server" CssClass="input_bk formsize225"></asp:TextBox>
                </li>
                <li><span class="name">每周踢球次数</span><asp:TextBox ID="txtMZCS" runat="server" CssClass="input_bk formsize225"></asp:TextBox>
                </li>
                <li><span class="name">关注球队</span><asp:TextBox ID="txtGZQD" runat="server" CssClass="input_bk formsize225"></asp:TextBox>
                </li>
            </ul>
        </div>
        <div class="reg_btn pb10 mt10">
            <asp:Button ID="Button1" runat="server" Text=" 完 成 注 册 " OnClick="Button1_Click" />
        </div>
    </div>
</asp:Content>
