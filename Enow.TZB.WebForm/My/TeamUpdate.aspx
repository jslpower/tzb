<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master" AutoEventWireup="true" CodeBehind="TeamUpdate.aspx.cs" Inherits="Enow.TZB.WebForm.My.TeamUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">

    <div class="game_box mt10">
        <div class="sideT fixed">
            <h3>
                修改球队信息</h3>
            <div class="Rtit">
                首页 >会员中心 >修改球队</div>
        </div>
        <div class="reg_form qiudui_form">
            <ul class="fixed">
                <li><span class="name">球队名称</span><asp:TextBox ID="txtTeamName" runat="server" CssClass="input_bk formsize400"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入球队名称。" ControlToValidate="txtTeamName">*请输入球队名称</asp:RequiredFieldValidator></li>
                <li><span class="name">球队照片</span><asp:FileUpload ID="imgFileUpload" runat="server"
                    CssClass="input_bk formsize400" /><input type="hidden" name="HidTeamPhoto" id="HidTeamPhoto" runat="server" /></li>
                <li class="w50"><span class="name">我的位置</span><select id="SQWZ" name="SQWZ">
                    <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                    (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.球员位置),
                                new string[] { }), "")
                    %>
                </select></li>
                <li class="w50"><span class="name">我的球衣号</span><asp:TextBox ID="txtQYHM" runat="server"
                    MaxLength="4" CssClass=" input_bk formsize60"></asp:TextBox><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ErrorMessage="球衣号码只能为数字" ControlToValidate="txtQYHM"
                        ValidationExpression="\d{1,4}">*球衣号码只能为数字</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写球衣号码" ControlToValidate="txtQYHM">*请填写球衣号码</asp:RequiredFieldValidator></li>
                <li><span class="name">球队介绍</span>
                    <textarea class="input_bk" id="TeamInfo" name="TeamInfo" runat="server" style="height: 300px; width: 600px;"></textarea>
                </li>
                <li><span class="name">创始人</span><strong><asp:Literal ID="ltrContactName" runat="server"></asp:Literal></strong></li>
            </ul>
            <div class="reg_btn pb10 mt10" style="text-align:center;">
            <input type="hidden" name="HidTeamId" id="HidTeamId" runat="server" /><input type="hidden" name="HidTeamMemberId" id="HidTeamMemberId" runat="server" />
                <asp:Button ID="btnCreate" runat="server" Text="保存并重新认证" 
                    onclick="btnCreate_Click" /></div>
        </div>
       </div>
       <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
       <script language="javascript" type="text/javascript">
           $(function () {

               KEditer.init("<%=TeamInfo.ClientID %>", { resizeMode: 1, items: keSimple, height: "300px" });
           });
       </script>
</asp:Content>
