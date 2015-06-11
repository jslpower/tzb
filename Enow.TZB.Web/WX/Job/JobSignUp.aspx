<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobSignUp.aspx.cs" Inherits="Enow.TZB.Web.WX.Job.JobSignUp" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <title>岗位报名</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Js/CitySelect.js"></script>
</head>
<body>
    <form id="form1" runat="server">

    <uc1:UserHome ID="UserHome1" Userhometitle="岗位报名" runat="server" />

    <div class="warp">
        <div class="msg_list qiu_box nobot">
            <ul>
                <li>
                    <label>
                        岗位名称：</label><asp:Literal ID="ltrJobName" runat="server"></asp:Literal></li>
                <li>
                    <label>
                        工作国家：</label><select name="ddlCountry" id="ddlCountry" runat="server">
                        </select><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                            ErrorMessage="请选择国家!" ControlToValidate="ddlCountry">*请选择工作国家</asp:RequiredFieldValidator></li>
                <li>
                    <label>
                        工作省份：</label>
                    <select name="ddlProvince" id="ddlProvince" runat="server">
                    </select><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ErrorMessage="请选择省份!" ControlToValidate="ddlProvince">*请选择工作省份</asp:RequiredFieldValidator></li>
                <li>
                    <label>
                        工作城市：</label><select id="ddlCity" name="ddlCity" runat="server">
                        </select><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                            ErrorMessage="请选择城市!" ControlToValidate="ddlCity">*请选择工作城市</asp:RequiredFieldValidator></li>
                <li>
                    <label>
                        工作区县：</label><select id="ddlArea" name="ddlArea" runat="server">
                        </select><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                            ErrorMessage="请选择区县!" ControlToValidate="ddlArea">*请选择工作区县</asp:RequiredFieldValidator></li>
                <li>
                    <label>
                        工作年限：</label><asp:TextBox ID="txtWorkYear" runat="server" CssClass="input_bk formsize100"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator4" runat="server" ErrorMessage="请填写工作年限!" ControlToValidate="txtWorkYear">*请填写工作年限</asp:RequiredFieldValidator></li>
                <li>
                    <label>
                        球&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;龄：</label><asp:TextBox ID="txtBallYear" runat="server"
                            CssClass="input_bk formsize100"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server" ErrorMessage="请填写球龄!" ControlToValidate="txtBallYear">*请填写球龄</asp:RequiredFieldValidator></li>
                <li>
                    <label>
                        专&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;业：</label><asp:TextBox ID="txtSpecialty" runat="server"
                            CssClass="input_bk formsize100"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                runat="server" ErrorMessage="请填写专业!" ControlToValidate="txtSpecialty">*请填写专业</asp:RequiredFieldValidator></li>
                <li class="last">
                    <label>
                        报名感言：</label><br />
                    <asp:TextBox ID="txtBMGY" runat="server" Width="90%" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Requ3" runat="server" ErrorMessage="请填写报名感言!"
                        ControlToValidate="txtBMGY">*报名感言不能为空，长度必须大于100个汉字</asp:RequiredFieldValidator>
                </li>
                <li class="last">
                    <label>
                        个人简历：</label><br />
                    <asp:TextBox ID="txtApplyInfo" runat="server" Width="90%" TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator8" runat="server" ErrorMessage="请填写个人简历!" ControlToValidate="txtBMGY">*个人简历不能为空，长度必须大于50个汉字</asp:RequiredFieldValidator>
                </li>
            </ul>
        </div>
        <div class="msg_btn">
            <asp:Button ID="btnSave" CssClass="basic_btn" runat="server" Text="报  名" OnClick="linkSave_Click" /></div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">
    $(function () {
        pcToobar.init({
            gID: "#ddlCountry",
            pID: "#ddlProvince",
            cID: "#ddlCity",
            xID: "#ddlArea",
            comID: '',
            gSelect: '',
            pSelect: '',
            cSelect: '',
            xSelect: ''
        })
    });
</script>
