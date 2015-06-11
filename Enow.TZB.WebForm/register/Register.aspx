<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Enow.TZB.WebForm.register.Register"
    MasterPageFile="/MasterPage/RegisterMaster.Master" %>
    <asp:Content ID="ContentTop" ContentPlaceHolderID="cph_Top" runat="server">
    
     <div class="reg_step">
             <ul class="fixed">
                <li><img src="/images/u-step01_on.gif"><p class="paddL5 font_yellow">创建账号</p></li>
                <li><img src="/images/u-step02.gif"><p class="cent">实名设置</p></li>
                <li><img src="/images/u-step03.gif"><p class="paddR5 t_R">偏好设置</p></li>
             </ul>
          </div>
    </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <div class="reg_form">
        <ul>
            <li><span class="name">国家</span><select name="ddlCountry" id="ddlCountry" runat="server">
            </select><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                ErrorMessage="请选择国家!" ControlToValidate="ddlCountry"><span class="font_yellow" >*</span><span class="error_txt">请选择国家</span></asp:RequiredFieldValidator></li>
            <li><span class="name">省份</span>
                <select name="ddlProvince" id="ddlProvince" runat="server">
                </select><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ErrorMessage="请选择省份!" ControlToValidate="ddlProvince"><span class="font_yellow" >*</span><span class="error_txt">请选择省份</span></asp:RequiredFieldValidator></li>
            <li><span class="name">城市</span><select id="ddlCity" name="ddlCity" runat="server">
            </select><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                ErrorMessage="请选择城市!" ControlToValidate="ddlCity"><span class="font_yellow" >*</span><span class="error_txt">请选择城市</span></asp:RequiredFieldValidator></li>
            <li><span class="name">区县</span><select id="ddlArea" name="ddlArea" runat="server">
            </select><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                ErrorMessage="请选择区县!" ControlToValidate="ddlArea"><span class="font_yellow" >*</span><span class="error_txt">请选择区县</span></asp:RequiredFieldValidator></li>
            <li><span class="name">性别</span><a id="man" onclick="cGender(this.id);"><img id="imgMan"
                src="/images/ManOn.png" border="0"></a><a id="woman" onclick="cGender(this.id);"><img
                    id="imgWoman" src="/images/Woman.png" border="0"></a><input type="hidden" value="男"
                        name="hidSex" id="hidSex"/></li>
            <li><span class="name">注册手机</span><input name="txt_phone" type="text" id="txt_phone"
                runat="server" class="input_bk formsize150" onfocus="javascript:if(this.value=='请输入手机号码')this.value='';"
                onblur="javascript:if(this.value=='')this.value='请输入手机号码';" /><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" InitialValue="请输入手机号码" ErrorMessage="请填写手机号码!" ControlToValidate="txt_phone"><span class="font_yellow" >*</span><span class="error_txt">请填写正确的手机号码</span></asp:RequiredFieldValidator></li>
            <li id="liVerCode"><span class="name">验证码</span><input name="txtcode" id="txtcode"
                type="text" class="input_bk formsize100" runat="server" /><a class="code" id="btnGetVerCode">获取验证码</a><span
                    id="msg"></span></li>
            <div style="display: none;">
                <li><span class="name">用户名</span><input type="text" id="txt_name" name="txt_name"
                    class="input_bk formsize225" runat="server" /></li>
            </div>
            <li><span class="name">密码</span><input name="txtpwd" runat="server" id="txtpwd" type="password"
                value="" class="input_bk formsize150" /><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                    runat="server" ErrorMessage="请填写密码!" ControlToValidate="txtpwd"><span class="font_yellow" >*</span><span class="error_txt">请填写密码</span></asp:RequiredFieldValidator></li>
            <li><span class="name">确认密码</span><input name="txtSurePwd" id="txtSurePwd" type="password"
                runat="server" class="input_bk formsize150" /><asp:CompareValidator ID="CompareValidator1"
                    runat="server" ErrorMessage="确认密码与密码不一致！" ControlToCompare="txtpwd" ControlToValidate="txtSurePwd"><span class="font_yellow" >*</span><span class="error_txt">确认密码与密码不一致</span></asp:CompareValidator></li>
        </ul>
        <div class="reg_fxk pb10" style="display: none;">
            <label>
                <input name="chkok" id="chkok" type="checkbox" value="" />我已阅读并同意遵守 <a href="tiaokuan_boxy.html"
                    id="link01">《铁子帮用户服务协议》</a></label></div>
        <div class="reg_btn pb10 mt10">
            <asp:Button ID="btnSave" runat="server" Text=" 下 一 步 " OnClick="btnSave_Click" />
        </div>
    </div>
    <script language="javascript" type="text/javascript">

        function cGender(Sex) {
            if (Sex == "man") {
                $("#hidSex").val("男");
                $("#imgMan").attr("src", "/images/ManOn.png");
                $("#imgWoman").attr("src", "/images/Woman.png");
            }
            else {
                $("#hidSex").val("女");
                $("#imgMan").attr("src", "/images/Man.png");
                $("#imgWoman").attr("src", "/images/WomanOn.png");
            }
        }

        var shuoJiReg = /^(13|15|18|14|17)\d{9}$/;
        var cdtime = 240; //全局时间变量（秒数）
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
            var shouji = $("#<%=txt_phone.ClientID %>").val();
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
                        alert(html.msg);
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
                gID: "#<%=ddlCountry.ClientID %>",
                pID: "#<%=ddlProvince.ClientID %>",
                cID: "#<%=ddlCity.ClientID %>",
                xID: "#<%=ddlArea.ClientID %>",
                comId:'',
                gSelect: "<%=CountryId %>",
                pSelect: "<%=ProvinceID %>",
                cSelect: "<%=CityId %>",
                xSelect: "<%=AreaId %>"

            });
            $("#<%=ddlCountry.ClientID %>").change(function () {
                if ($(this).val() != "1") {
                    $("#liVerCode").hide();
                } else {
                    $("#liVerCode").show();
                }
            });
            $("#<%=ddlProvince.ClientID %>").change(function () {
                if ($("#<%=ddlCountry.ClientID %>").find("option:selected").val() == "1") {
                    if ($(this).val() == "190" || $(this).val() == "191" || $(this).val() == "988") {
                        $("#liVerCode").hide();
                    } else {
                        $("#liVerCode").show();
                    }
                } else {
                    $("#liVerCode").hide();
                }
            });
        });

    </script>
</asp:Content>
