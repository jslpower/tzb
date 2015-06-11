<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobDetail.aspx.cs" Inherits="Enow.TZB.WebForm.Job.JobDetail" %>

<%@ Register Src="~/UserControl/TopBar.ascx" TagName="TopBar1" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/Menu.ascx" TagName="Menu1" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta charset="utf-8">
    <meta name="keywords" content="" />
    <title>舵主招聘</title>
    <link href="/css/style.css" rel="stylesheet" />
    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>
    <link href="/Css/boxy.css" rel="Stylesheet" />
    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Js/CitySelect.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:TopBar1 ID="topbar1" runat="server" />
    <uc2:Menu1 ID="menu1" runat="server" />
    <div class="head_line">
    </div>
    <div class="warp">
        <div class="reg_form">
            <ul class="botline">
                <li><span class="name">岗位名称</span>
                    <asp:Label ID="lblJobName" runat="server"></asp:Label>
                </li>
                <li><span class="name">所在城市</span>
                    <asp:Label ID="lblArea" runat="server"></asp:Label>
                </li>
                <li><span class="name">申请有效期</span> <span class="font_yellow">
                    <asp:Label ID="lblDate" runat="server"></asp:Label></span></li>
                <li><span class="name">招聘人数</span><asp:Label ID="lblNum" runat="server"></asp:Label></li>
                <li><span class="name">岗位职责</span><div class="textinfo_text">
                    <asp:Literal ID="ltrJobrule" runat="server"></asp:Literal>
                </div>
                </li>
                <li><span class="name">岗位要求</span><div class="textinfo_text">
                    <asp:Literal ID="ltrJobInfo" runat="server"></asp:Literal>
                </div>
                </li>
            </ul>
            <asp:PlaceHolder ID="phSignUp" runat="server">
            <ul>
            <li>
                    <span class="name">
                        工作国家：</span><select name="ddlCountry" id="ddlCountry" runat="server">
                        </select><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                            ErrorMessage="请选择国家!" ControlToValidate="ddlCountry"><span class="error_txt">请选择工作国家</span></asp:RequiredFieldValidator></li>
                <li>
                    <span class="name">
                        工作省份：</span>
                    <select name="ddlProvince" id="ddlProvince" runat="server">
                    </select><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ErrorMessage="请选择省份!" ControlToValidate="ddlProvince"><span class="error_txt">请选择工作省份</span></asp:RequiredFieldValidator></li>
                <li>
                    <span class="name">
                        工作城市：</span><select id="ddlCity" name="ddlCity" runat="server">
                        </select><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                            ErrorMessage="请选择城市!" ControlToValidate="ddlCity"><span class="error_txt">请选择工作城市</span></asp:RequiredFieldValidator></li>
                <li>
                   <span class="name">
                        工作区县：</span><select id="ddlArea" name="ddlArea" runat="server">
                        </select><asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"
                            ErrorMessage="请选择区县!" ControlToValidate="ddlArea"><span class="error_txt">请选择工作区县</span></asp:RequiredFieldValidator></li>
                <li>
                <li><span class="name">工作年限</span>
                    <asp:TextBox ID="txtJobYear" runat="server" CssClass="input_bk formsize225"></asp:TextBox><asp:RequiredFieldValidator
                        ID="required1" runat="server" ErrorMessage="请填写工作年限!" ControlToValidate="txtJobYear"><span class="font_yellow" >*</span><span class="error_txt">请填写工作年限</span></asp:RequiredFieldValidator>
                </li>
                <li><span class="name">球龄</span><asp:TextBox ID="txtBallYear" runat="server" CssClass="input_bk formsize225"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写球龄!"
                        ControlToValidate="txtBallYear"><span class="font_yellow" >*</span><span class="error_txt">请填写球龄</span></asp:RequiredFieldValidator></li>
                <li><span class="name">专业</span><asp:TextBox ID="txtSpecialty" runat="server" CssClass="input_bk formsize255"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写专业!"
                        ControlToValidate="txtSpecialty"><span class="font_yellow" >*</span><span class="error_txt">请填写专业</span></asp:RequiredFieldValidator></li>
                <li><span class="name">报名感言</span><asp:TextBox ID="txtBMGY" runat="server" Height="100px"
                    CssClass="text_bk" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请填写报名感言!"
                        ControlToValidate="txtBMGY"><span class="font_yellow" >*</span><span class="error_txt">请填写报名感言</span></asp:RequiredFieldValidator></li>
                <li><span class="name">个人简历</span><asp:TextBox ID="txtApplyInfo" runat="server" Height="100px"
                    CssClass="text_bk" TextMode="MultiLine">
                </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请填写个人简历!"
                        ControlToValidate="txtApplyInfo"><span class="font_yellow" >*</span><span class="error_txt">请填写个人简历</span></asp:RequiredFieldValidator>
                </li>
            </ul>
            <div class="reg_btn pb10 mt10">
                <asp:Button ID="btnSave" runat="server" Text="提交申请" OnClick="btnSave_Click" />
            </div>
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

            </asp:PlaceHolder>
        </div>
    </div>
    <uc3:Footer ID="footer" runat="server" />
    </form>
</body>
</html>