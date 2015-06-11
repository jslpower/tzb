<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step1.aspx.cs" Inherits="Enow.TZB.Web.WX.Register.Step1" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>铁丝注册</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Js/CitySelect.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="铁丝注册" runat="server" />
    <div class="warp">
        <div class="msg_tab" id="n4Tab">
            <div class="TabTitle">
                <ul class="fixed">
                    <li id="n4Tab_Title0" class="active"><a>账号</a></li>
                    <li id="n4Tab_Title1"><a>实名</a></li>
                    <li id="n4Tab_Title2" style="width: 34%;"><a>偏好</a></li>
                </ul>
            </div>
            <div style="width: 100%">
                <div class="TabContent">
                    <div id="n4Tab_Content0">
                        <div class="msg_list">
                            <ul>
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
                                        性别：</label><a id="man" onclick="cGender(this.id);"><img id="imgMan" src="../images/ManOn.png"
                                            border="0"></a><a id="woman" onclick="cGender(this.id);"><img id="imgWoman" src="../images/Woman.png"
                                                border="0"></a><input type="hidden" value="男" name="hidSex" id="hidSex" /></li>
                                <li id="liPhone">
                                    <label>
                                        手机：</label><input name="txt_phone" type="text" id="txt_phone" runat="server" class="input_bk formsize150"
                                            onfocus="javascript:if(this.value=='请输入手机号')this.value='';" onblur="javascript:if(this.value=='')this.value='请输入手机号';" /><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写手机号码!" InitialValue="请输入手机号" ControlToValidate="txt_phone">*</asp:RequiredFieldValidator>
                                </li>
                                <li class="tishi">手机号码同时作为铁子帮官网铁丝账号，注册后不能修改，请如实填写！</li>
                                <li id="liVerCode">
                                    <label>
                                        验证码：</label><input name="txtcode" id="txtcode" type="text" class="input_bk formsize100"
                                            runat="server" /><a class="code" id="btnGetVerCode">获取验证码</a><span
                                                id="msg"></span> </li>
                                <li>
                                    <label>
                                        邮箱：</label><input name="txtemail" runat="server" id="txtemail" type="text" value=""
                                            class="input_bk formsize150" /><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                                runat="server" ErrorMessage="请填写邮箱!" ControlToValidate="txtemail">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator1" runat="server" ErrorMessage="请填写正确的EAMIL格式!"
                                                    ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                </li>
                                <li class="tishi">邮箱作为铁子帮与铁丝联系的主要途径，请认真填写！</li>
                                <li>
                                    <label>
                                        铁丝账号密码：</label><input name="txtpwd" runat="server" id="txtpwd" type="password" value=""
                                            class="input_bk formsize150" /><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                                runat="server" ErrorMessage="请填写密码!" ControlToValidate="txtpwd">*</asp:RequiredFieldValidator>
                                </li>
                                <li>
                                    <label>
                                        铁丝账号确认密码：</label><input name="txtSurePwd" id="txtSurePwd" type="password" runat="server"
                                            class="input_bk formsize150" /><asp:CompareValidator ID="CompareValidator1" runat="server"
                                                ErrorMessage="确认密码与密码不一致！" ControlToCompare="txtpwd" ControlToValidate="txtSurePwd">*</asp:CompareValidator></li>
                            </ul>
                        </div>
                        <div class="msg_btn">
                            <asp:Button CssClass="basic_btn" ID="btnSave" runat="server" Text="下一步" OnClick="btnSave_Click" />
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
    var cdtime = 200; //全局时间变量（秒数）
    function Countdown() { //加时函数        
        if (cdtime > 0) {
            $(".code").text(cdtime + "秒后重新发送");
            setTimeout("Countdown()", 1000); //设置1000毫秒以后执行一次本函数
            cdtime--;
        } else {
            cdtime = 10;
            //getZhuCeYanZhengMa($(".code"));
            $(".code").text("获取验证码");
            $("#btnGetVerCode").unbind("click").click(function () {
                getZhuCeYanZhengMa(this);
            });
        }
    }
    //获取注册验证码：data:{shouJi:"手机号码"}
    function getZhuCeYanZhengMa(obj) {
        var _v = { success: false, code: 0 };
        var shouji = $("#txt_phone").val();
        if (shouji == "undefined" || shouji.length == 0) { alert("请输入手机号码"); return _v; }
        if (!shuoJiReg.test(shouji)) { alert("请输入正确的手机号码"); return _v; }
        $(obj).text(cdtime + "秒后重新发送");
        $("#btnGetVerCode").unbind("click");
        Countdown();
        $.ajax({
            type: "GET",
            url: "/Ashx/GetVerCode.ashx?MobilePhone=" + shouji,
            dataType: "json",
            cache: false,
            async: false,
            success: function (html) {
                _v.code = html.result;
                if (html.result == "1") {
                    //alert(response.obj);
                    _v.success = true;
                } else {
                    //alert(html.msg);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //alert(XMLHttpRequest.status);
                //alert(XMLHttpRequest.readyState);
                //alert(textStatus);
                alert("连接服务器失败！");
                return;
            }
        });
        return _v;
    }
    $(function () {
        $("#btnGetVerCode").unbind("click").click(function () {
            getZhuCeYanZhengMa(this);
        });
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
        $("#ddlCountry").change(function () {
            if ($(this).val() != "1") {
                $("#liVerCode").hide();
            } else {
                $("#liVerCode").show();
            }
        })
        $("#ddlProvince").change(function () {
            if ($("#ddlCountry").find("option:selected").val() == "1") {
                if ($(this).val() == "190" || $(this).val() == "191" || $(this).val() == "988") {
                    $("#liVerCode").hide();
                } else {
                    $("#liVerCode").show();
                }
            } else {
                $("#liVerCode").hide();
            }
        })
    })
</script>
