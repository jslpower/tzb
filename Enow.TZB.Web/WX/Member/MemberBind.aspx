<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberBind.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.MemberBind" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>帐号绑定</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="帐号绑定" runat="server" />
     <div class="warp">
        <div class="msg_tab" id="n4Tab">            
            <div class="TabContent">                
                <div id="n4Tab_Content1">
                    <div class="msg_list">
                        <ul>
                            <li class="tishi">请填写您的手机号码并通过短信(邮件)验证码进行帐号绑定</li>
                            <li id="liPhone">
                                    <label>
                                        手机：</label><input name="txt_phone" type="text" id="txt_phone" runat="server" class="input_bk formsize150"
                                            onfocus="javascript:if(this.value=='请输入手机号')this.value='';" onblur="javascript:if(this.value=='')this.value='请输入手机号';" /><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写手机号码!" ControlToValidate="txt_phone">*</asp:RequiredFieldValidator>
                                </li>
                            <li id="liVerCode">
                                    <label>
                                        验证码：</label><input name="txtcode" id="txtcode" type="text" class="input_bk formsize100"
                                            runat="server" /><a class="code" onclick="getZhuCeYanZhengMa(this);">获取验证码</a><span
                                                id="msg"></span>
                                </li>  
                        </ul>
                    </div>
                    <div class="msg_btn">
                                            <asp:Button CssClass="basic_btn" ID="btnSave" runat="server" Text="保 存" 
                                onclick="btnSave_Click" />
                </div>
            </div>
        </div>
    </div>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var shuoJiReg = /^(13|15|18|14|17)\d{9}$/;
    var cdtime = 240; //全局时间变量（秒数）
    function Countdown() { //加时函数        
        if (cdtime > 0) {
            $(".code").text(cdtime + "秒后重新发送");
            setTimeout("Countdown()", 1000); //设置1000毫秒以后执行一次本函数
            cdtime--;
        } else {
            cdtime = 10;
            getZhuCeYanZhengMa($(".code"));
        }
    }
    //获取注册验证码：data:{shouJi:"手机号码"}
    function getZhuCeYanZhengMa(obj) {
        var _v = { success: false, code: 0 };
        var shouji = $("#txt_phone").val();
        if (shouji == "undefined" || shouji.length == 0) { alert("请输入手机号码"); return _v; }
        if (!shuoJiReg.test(shouji)) { alert("请输入正确的手机号码"); return _v; }
        $(obj).text(cdtime + "秒后重新发送");
        Countdown();
        $.ajax({
            type: "GET",
            url: "/Ashx/PassVerCodeReset.ashx?MobilePhone=" + shouji,
            dataType: "json",
            cache: false,
            async: false,
            success: function (html) {
                _v.code = html.result;
                switch (html.result) {
                    case "1":
                        _v.success = true;
                        break;
                    case "2":
                        alert("已经验证码发至您的邮箱，请查收邮件并填写验证码！");
                        _v.success = true;
                        break;
                    default:
                        alert(html.msg);
                        break;
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
</script>