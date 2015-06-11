<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master" AutoEventWireup="true" CodeBehind="Transfers.aspx.cs" Inherits="Enow.TZB.WebForm.My.Transfers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
    <div class="user_box">
        <h3>
            充值</h3>
        <div class="reg_form">
            <ul class="botline">
                <li><span class="name">转入队友名</span><asp:DropDownList ID="ddlTeamMember" runat="server">
                      </asp:DropDownList>
                </li>
                <li><span class="name">转入铁丝币</span><asp:TextBox ID="txtMoney" CssClass="formsize100 input_bk" MaxLength="6"
                                            runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ErrorMessage="请填写转账金额!" 
                                    ControlToValidate="txtMoney">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator1" runat="server" 
                                    ErrorMessage="转入铁丝币数量只能为数字!" ControlToValidate="txtMoney" 
                                    ValidationExpression="^\d+(\.\d+)?$">*</asp:RegularExpressionValidator>
                </li>
                <li><span class="name">支&nbsp;付&nbsp;密&nbsp;码：</span><asp:TextBox ID="txtPayPassword" CssClass="formsize100 input_bk" runat="server" MaxLength="150" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写支付密码!" 
                                    ControlToValidate="txtPayPassword">*</asp:RequiredFieldValidator></li>
            </ul>
        </div>
        <div class="reg_btn pb10">
            <asp:Button ID="btnPay" runat="server" Text="立即转账" onclick="btnPay_Click" />
        </div>
    </div>
</asp:Content>