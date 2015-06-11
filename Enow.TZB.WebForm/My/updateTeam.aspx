<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master"
    AutoEventWireup="true" CodeBehind="updateTeam.aspx.cs" Inherits="Enow.TZB.WebForm.My.updateTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
    <div class="game_box mt10">
        <div class="sideT fixed">
            <h3>
                修改球队信息</h3>
        </div>
        <div class="reg_form qiudui_form">
            <ul class="fixed">
                <li><span class="name">球队名称</span><asp:TextBox ID="txtTeamName" runat="server" CssClass="input_bk formsize400"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入球队名称。" ControlToValidate="txtTeamName">*请输入球队名称</asp:RequiredFieldValidator></li>
                     <li><span class="name">所在地区</span>
                    <select name="ddlCountry" id="ddlCountry" runat="server">
                    </select><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                        ErrorMessage="请选择国家!" ControlToValidate="ddlCountry">*</asp:RequiredFieldValidator>
                    <select name="ddlProvince" id="ddlProvince" runat="server">
                    </select><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ErrorMessage="请选择省份!" ControlToValidate="ddlProvince">*</asp:RequiredFieldValidator>
                    <select id="ddlCity" name="ddlCity" runat="server">
                    </select><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ErrorMessage="请选择城市!" ControlToValidate="ddlCity">*</asp:RequiredFieldValidator>
                    <select id="ddlArea" name="ddlArea" runat="server">
                    </select><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                        ErrorMessage="请选择区县!" ControlToValidate="ddlArea">*</asp:RequiredFieldValidator>
                </li>
                <li><span class="name">球队照片</span><asp:FileUpload ID="imgFileUpload" runat="server"
                    CssClass="input_bk formsize400" /></li>
                <li class="w50"><span class="name">我的位置</span><select id="SQWZ" name="SQWZ">
                    <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                    (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.球员位置),
                                                        new string[] { }), MeberSQWZ)
                    %>
                </select></li>
                <li class="w50"><span class="name">我的球衣号</span><asp:TextBox ID="txtQYHM" runat="server"
                    MaxLength="4" CssClass=" input_bk formsize60"></asp:TextBox><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ErrorMessage="球衣号码只能为数字" ControlToValidate="txtQYHM"
                        ValidationExpression="\d{1,4}">*球衣号码只能为数字</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写球衣号码" ControlToValidate="txtQYHM">*请填写球衣号码</asp:RequiredFieldValidator></li>
                <li><span class="name">球队介绍</span>
                    <textarea class="input_bk" id="TeamInfo" cols="90" rows="30" name="TeamInfo" runat="server" style="height: 300px;
                        width: 600px;"></textarea>
                </li>
                <li><span class="name">创始人</span><strong><asp:Literal ID="ltrContactName" runat="server"></asp:Literal></strong></li>
                <input type="hidden" name="hidTeamId" id="hidTeamId" runat="server" /><input type="hidden" name="hidTeamMemberId" id="hidTeamMemberId" runat="server" />
            </ul>
            <div class="reg_btn pb10 mt10" style="text-align: center;">
                <asp:Button ID="btnCreate" runat="server" Text="保存" onclick="btnCreate_Click" /></div>
        </div>
    </div>
    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            pcToobar.init({
                gID: "#<%=ddlCountry.ClientID %>",
                pID: "#<%=ddlProvince.ClientID%>",
                cID: "#<%=ddlCity.ClientID %>",
                xID: "#<%=ddlArea.ClientID %>",
                comId: '',
                gSelect: "<%=CountryId %>",
                pSelect: "<%=ProvinceID %>",
                cSelect: "<%=CityId %>",
                xSelect: "<%=AreaId %>"

            });
            KEditer.init("<%=TeamInfo.ClientID %>", { resizeMode: 1, items: keSimple, height: "300px" });

        });
    </script>
</asp:Content>
