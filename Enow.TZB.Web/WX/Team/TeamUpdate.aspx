<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamUpdate.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.TeamUpdate" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>球队信息修改</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Js/CitySelect.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:UserHome ID="UserHome1" Userhometitle="球队信息修改" runat="server" />
    <div class="warp">
        <div class="msg_list qiu_box">
            <ul>
                <li>
                    <label>
                        球队名称：</label><input name="txtTeamName" runat="server" id="txtTeamName" type="text"
                            class="input_bk formsize150"><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server" ErrorMessage="请输入球队名称。" ControlToValidate="txtTeamName">*</asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        所在地区：</label>
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
                <li>
                    <label>
                        球队照片：</label><asp:FileUpload ID="imgFileUpload" runat="server" Style="width: 200px" /></li>
                <li>
                    <label>
                        创建人：</label><asp:Literal ID="ltrCreateName" runat="server"></asp:Literal></li>
                <li>
                    <label>
                        我的位置：</label><select id="SQWZ" name="SQWZ">
                            <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                    (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.球员位置),
                                                               new string[] { }), MeberSQWZ.ToString())
                            %>
                        </select></li>
                <li>
                    <label>
                        我的球衣号码：</label><input id="txtQYHM" name="txtQYHM" type="text" runat="server" maxlength="4"
                            class="input_bk formsize50" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                runat="server" ErrorMessage="球衣号码只能为数字" ControlToValidate="txtQYHM" ValidationExpression="\d{1,4}">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写球衣号码" ControlToValidate="txtQYHM">*</asp:RequiredFieldValidator></li>
                <li class="last">
                    <label>
                        球队介绍：</label><br />
                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox></li>
            </ul>
            <div class="mt20 padd20">
                <input type="hidden" name="hidTeamId" id="hidTeamId" runat="server" /><input type="hidden"
                    name="hidTeamMemberId" id="hidTeamMemberId" runat="server" />
                <asp:Button CssClass="basic_btn" ID="btnSave" runat="server" Text="保存并重新认证" OnClick="linkBtnSave_Click" /></div>
        </div>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    </form>
</body>
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
    });
</script>
</html>
