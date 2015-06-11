
var shuoJiReg = /^(13|15|18|14|17)\d{9}$/;
var iLogin = {

    //获取注册验证码：data:{shouJi:"手机号码"}
    getZhuCeYanZhengMa: function (data) {
        var _v = { success: false, code: 0 };
        if (typeof data == "undefined" || data == null) return _v;
        if (typeof data.shouJi == "undefined" || data.shouJi.length == 0) { alert("请输入手机号码"); return _v; }
        if (!shuoJiReg.test(data.shouJi)) { alert("请输入正确的手机号码"); return _v; }

        var _data = { txt_phone: data.shouJi };

        $.ajax({ type: "POST", url: "/ashx/ValidCode.ashx?dotype=getCode", cache: false, async: false, dataType: "json", data: _data,
            success: function (response) {
                _v.code = response.result;
                if (response.result == 1) {
                    //alert(response.obj);
                    _v.success = true;
                } else {
                    alert(response.msg);
                }
            }
        });

        return _v;
    },
    zhuCe: function (data) {
        var _v = { success: false, code: 0 };
        if (typeof data == "undefined" || data == null) return _v;
        if (typeof data.OpenId == "undefined" || data.OpenId.length == 0) { alert("未知OpenId"); return _v; }
        if (typeof data.comID == "undefined" || data.comID.length == 0) { alert("请选择国家"); return _v; }
        if (typeof data.Pid == "undefined" || data.Pid.length == 0) { alert("请选择省份"); return _v; }
        if (typeof data.Cid == "undefined" || data.Cid.length == 0) { alert("请选择所在城市"); return _v; }
        if (typeof data.phone == "undefined" || data.phone.length == 0) { alert("请输入手机号码"); return _v; }
        if (!shuoJiReg.test(data.phone)) { alert("请输入正确的手机号码"); return _v; }
        if (typeof data.email == "undefined" || data.email.length == 0) { alert("请输入邮箱"); return _v; }
        if (typeof data.code == "undefined" || data.code.length == 0) { alert("请输入验证码"); return _v; }
        if (typeof data.pwd == "undefined" || data.pwd.length == 0) { alert("请输入密码"); return _v; }


        var _data = {OpenId:data.OpenId, txt_phone: data.phone, txtcode: data.code, ddlCountry: data.comID, ddlProvince: data.Pid, ddlCity: data.Cid, txtpwd: data.pwd, txtemail: data.email, hGender: data.Gender }
        $.ajax({ type: "POST", url: "/ashx/ValidCode.ashx?dotype=Register", cache: false, async: false, dataType: "json", data: _data,
            success: function (response) {
                _v.code = response.result;
                if (response.result == 1) {
                    _v.success = true;
                } else {
                    alert(response.msg);
                }
            }
        });

        return _v;
    }
};