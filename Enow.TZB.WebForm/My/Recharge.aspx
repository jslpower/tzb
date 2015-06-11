<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master"
    AutoEventWireup="true" CodeBehind="Recharge.aspx.cs" Inherits="Enow.TZB.WebForm.My.Recharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
    <div class="user_box">
        <h3>
            充值</h3>
        <div class="reg_form">
            <ul class="botline">
                <li><span class="name">充值账号</span><asp:TextBox ID="txtUserName" runat="server" CssClass="input_bk formsize225"
                    Enabled="false"></asp:TextBox>
                </li>
                <li><span class="name">真实姓名</span><asp:TextBox ID="txtContractName" runat="server"
                    CssClass="input_bk formsize225" Enabled="false"></asp:TextBox>
                </li>
                <li><span class="name">充值数量</span><asp:TextBox ID="txtCurrNo" runat="server" Text="0"
                    CssClass="formsize100 input_bk" MaxLength="6"></asp:TextBox>
                    铁丝币<asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server"
                        ErrorMessage="铁丝币只能为数字!" ValidationExpression="^\d+(\.\d+)?$" ControlToValidate="txtCurrNo">
                   <span class="font_yellow" >*</span><span class="error_txt">铁丝币只能为数字!</span></asp:RegularExpressionValidator></li>
                <li><span class="name">应付金额</span><span class="font_yellow font24 price floatL"><span
                    id="PayMoney">0</span></span><span style="display: inline-block;">元</span></li>
            </ul>
        </div>
        <div class="reg_btn pb10">
            <asp:Button ID="btnPay" runat="server" Text="支付宝支付" OnClick="btnPay_Click" />
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        $(function () {

            $('#<%=txtCurrNo.ClientID%>').bind("change", function () {

                if ($('#<%=txtCurrNo.ClientID%>').val() != "") {

                    $("#PayMoney").html($('#<%=txtCurrNo.ClientID%>').val());
                }
                else {

                    $("#PayMoney").html() = 0;
                }
            });
        });
    </script>
</asp:Content>
