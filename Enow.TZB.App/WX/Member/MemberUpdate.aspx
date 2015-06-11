<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberUpdate.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.MemberUpdate" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>帐号信息修改</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Js/CitySelect.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:UserHome ID="UserHome1" Userhometitle="帐号信息修改" runat="server" />
    <div class="warp">
        <div class="msg_tab" id="n4Tab">
            <div class="TabTitle">
                <ul class="fixed">
                    <li id="n4Tab_Title0" class="active"><a href="MemberUpdate.aspx">账号</a></li>
                    <li id="n4Tab_Title1"><a href="Step2.aspx">实名</a></li>
                    <li id="n4Tab_Title2" style="width: 34%;"><a href="Step3.aspx">偏好</a></li>
                </ul>
            </div>
            <div style="width: 100%">
                <div class="TabContent">
                    <div id="n4Tab_Content0">
                        <div class="msg_list">
                            <ul>
                                <li>
                                    <label>
                                        铁丝帐号：</label><asp:Literal ID="ltrUserName" runat="server"></asp:Literal></li>
                                <li>
                                    <label>
                                        用户头像：</label><asp:FileUpload ID="imgFileUpload" runat="server" Style="width: 200px" /><asp:Literal ID="ltrHead" runat="server"></asp:Literal></li>
                                <li>
                                    <label>
                                        国家：</label><select name="ddlCountry" id="ddlCountry" runat="server">
                                        </select><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                            ErrorMessage="请选择国家!" ControlToValidate="ddlCountry">*</asp:RequiredFieldValidator></li>
                                <li>
                                    <label>
                                        省份：</label>
                                    <select name="ddlProvince" id="ddlProvince" runat="server">
                                    </select><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ErrorMessage="请选择省份!" ControlToValidate="ddlProvince">*</asp:RequiredFieldValidator></li>
                                <li>
                                    <label>
                                        城市：</label><select id="ddlCity" name="ddlCity" runat="server">
                                        </select><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                            ErrorMessage="请选择城市!" ControlToValidate="ddlCity">*</asp:RequiredFieldValidator></li>
                                <li>
                                    <label>
                                        区县：</label><select id="ddlArea" name="ddlArea" runat="server">
                                        </select><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                            ErrorMessage="请选择区县!" ControlToValidate="ddlArea">*</asp:RequiredFieldValidator></li>
                                <li>
                                    <label>
                                        性别：</label><asp:Literal ID="ltrSex" runat="server"></asp:Literal><input type="hidden"
                                            value="男" name="hidSex" id="hidSex" runat="server" /></li>
                                <li>
                                    <label>
                                        邮箱：</label><input name="txtemail" id="txtemail" type="text" runat="server" class="input_bk formsize150" /><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator3" runat="server" ErrorMessage="请填写邮箱!" ControlToValidate="txtemail">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator1" runat="server" ErrorMessage="请填写正确的EAMIL格式!"
                                                ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></li>
                                <li class="tishi">邮箱作为铁子帮与铁丝联系的主要途径，请认真填写！</li>
                                <li>
                                    <label>
                                        铁丝账号密码：</label><input name="txtpwd" id="txtpwd" type="password" runat="server" class="input_bk formsize150" />不修改请置空</li>
                                <li>
                                    <label>
                                        铁丝账号确认密码：</label><input name="txtSurePwd" id="txtSurePwd" type="password" runat="server"
                                            class="input_bk formsize150" /></li>
                            </ul>
                        </div>
                        <div class="msg_btn">
                            <asp:Button CssClass="basic_btn" ID="Button1" runat="server" Text="保 存" OnClick="btnSave_Click" /></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    function nTabs(tabObj, obj) {
        var tabList = $("#" + tabObj).find("li");
        for (i = 0; i < tabList.length; i++) {
            if (tabList[i].id == obj.id) {
                $("#" + tabObj + "_Title" + i).attr("class", "active");
                $("#" + tabObj + "_Content" + i).show();
            } else {
                $("#" + tabObj + "_Title" + i).attr("class", "normal");
                $("#" + tabObj + "_Content" + i).hide();
            }
        }
    }
    function cGender(Sex) {
        if (Sex == "man") {
            $("#hidSex").val("男");
            $("#imgMan").attr("src", "../images/ManOn.png");
            $("#imgWoman").attr("src", "../images/Woman.png");
        }
        else {
            $("#hidSex").val("女");
            $("#imgMan").attr("src", "../images/Man.png");
            $("#imgWoman").attr("src", "../images/WomanOn.png");
        }
    }
    var shuoJiReg = /^(13|15|18|14|17)\d{9}$/;
    //获取注册验证码：data:{shouJi:"手机号码"}
    function getZhuCeYanZhengMa(obj) {
        var _v = { success: false, code: 0 };
        var shouji = $("#txt_phone").val();
        if (shouji == "undefined" || shouji.length == 0) { alert("请输入手机号码"); return _v; }
        if (!shuoJiReg.test(shouji)) { alert("请输入正确的手机号码"); return _v; }
        $(obj).css({ color: "#dadada" }).text("验证码已发送");
        //alert(shouji);
        //alert("/ashx/GetVerCode.ashx?MobilePhone=" + shouji);
        $.ajax({
            type: "GET",
            url: "/Ashx/GetVerCode.ashx?MobilePhone=" + shouji,
            dataType: "json",
            cache: false,
            async: false,
            success: function (html) {
                _v.code = response.result;
                if (response.result == 1) {
                    //alert(response.obj);
                    _v.success = true;
                } else {
                    alert(response.msg);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(XMLHttpRequest.status);
                alert(XMLHttpRequest.readyState);
                alert(textStatus);
                return;
            }
        });
        return _v;
    }
    $(function () {
        pcToobar.init({
            gID: "#ddlCountry",
            pID: "#ddlProvince",
            cID: "#ddlCity",
            xID: "#ddlArea",
            comID: '',
            gSelect: '<%=CountryId %>',
            pSelect: '<%=ProvinceId %>',
            cSelect: '<%=CityId %>',
            xSelect: '<%=AreaId %>'
        })
    })
</script>
