<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShouKuan.aspx.cs" Inherits="Enow.TZB.Web.ShouYin.ShouKuan" MasterPageFile="~/ShouYin/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="CPH_ZhuTi" ID="CPH_ZhuTi1" runat="server">
    <style type="text/css">
    .boxy_form li{padding-bottom:10px;}
    .dn{display:none;}
    </style>
    <form id="form1">    
    <div style="display:none;">
        <textarea name="txtShangPinJson" id="txtShangPinJson"></textarea>
        <input type="hidden" name="txtShangPinJinE" id="txtShangPinJinE" value="0" />
        <input type="hidden" name="txtZheKou" id="txtZheKou" value="<%=GFK_ZheKou %>" />
        <input type="hidden" name="txtYingShouJinE" id="txtYingShouJinE" value="0" />
    </div>
    <div class="boxy_form" id="div_shoukuan_form">
        <ul>
        <li><span>类型：</span><asp:Literal ID="ltrPayType" runat="server"></asp:Literal></li>
            <li id="li_shangpinjine" class=""><span>商品金额：</span>0.00 元整</li>            
            <li id="li_teisierweima" class="dn" ><span>扫描铁丝支付二维码：</span><input name="txtTieSiErWeiMa"
                id="txtTieSiErWeiMa" type="password" class="formsize180 input_style" /></li>
            <li id="li_shoukuanzhekou" class="dn"><span>收款折扣：</span><%=GFK_ZheKou %>折</li>
            <li id="li_yingshoujine" class=""><span>应收金额：</span>0.00 元整</li>
            <li id="li_shishoujine" class="dn"><span>实收金额：</span><input name="txtShiShouJinE"
                id="txtShiShouJinE" type="text" class="formsize180 input_style" /></li>
            <li id="li_zhaohuijine" class="dn"><span>找回金额：</span>0.00 元整</li>
        </ul>
        <div class="cent mt10">
            <input type="button" value="收款并打印小票" class="boxy_btn" id="btnShouKuan" /><input
                type="button" value="取  消" class="boxy_btn" id="btnQuXiaoShouKuan" /></div>
    </div>
    </form>

    <div class="print_box" id="div_shoukuan_danju" style="display:none;">
        <div class="print_head clearfix">
       
            <div class="print_logo floatL"  style=" text-align:center;">
                <img src="images/print_logo.gif"></div>
            <div class="print_title floatR"><%=QiuChangName %></div>
        </div>
    </div>
    <OBJECT classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="wb" name="wb" width="0"></OBJECT>
    <script type="text/javascript">
        var iPage = {
            SKFS: "<%=SKFS %>",
            close: function () {
                top.Boxy.getIframeDialog('<%=Request["iframeId"] %>').hide();
                return false;
            },
            formatJsonDateTime: function (jsonDateTime) {
                var _rgExp = /-?\d+/;
                var _matchResult = _rgExp.exec(jsonDateTime);
                var d = new Date(parseInt(_matchResult[0]));
                return d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();
            },
            daYin: function () {
                $("#btnDaYinWindow").hide();
                $("#btnGuanBiWindow").hide();

                //window.print();
                document.all.wb.ExecWB(6, 2)


                window.setTimeout(function () {
                    $(".print_table").hide();
                    //$("#btnDaYinWindow").show();
                    $("#btnGuanBiWindow").show();

                    //关闭付款窗口
                    //_self.close();
                }, 10);
            },
            //初始化金额信息
            initJinEXinXi: function () {
                var shangPinJinEInfo = top.shouYin.getShouYinShangPinJinE();

                switch (this.SKFS) {
                    case "xianjinshoukuan":                        
                        $("#li_shishoujine").show();
                        $("#li_zhaohuijine").show();

                        $("#li_shangpinjine").html('<span>商品金额：</span>' + shangPinJinEInfo.shichangjine.toFixed(2) + ' 元整');
                        $("#li_yingshoujine").html('<span>应收金额：</span>' + shangPinJinEInfo.shichangjine.toFixed(2) + ' 元整');
                        $("#txtShiShouJinE").val(shangPinJinEInfo.shichangjine);
                        $("#txtShangPinJinE").val(shangPinJinEInfo.shichangjine);
                        $("#txtYingShouJinE").val(shangPinJinEInfo.shichangjine);
                        $("#txtShiShouJinE").focus();
                        break;
                    case "tiesishoukuan":                        
                        $("#li_teisierweima").show();
                        $("#li_shangpinjine").html('<span>商品金额：</span>' + shangPinJinEInfo.tiesijine.toFixed(2) + ' 元整');
                        $("#li_yingshoujine").html('<span>应收金额：</span>' + shangPinJinEInfo.tiesijine.toFixed(2) + ' 元整');

                        $("#txtShangPinJinE").val(shangPinJinEInfo.tiesijine);
                        $("#txtYingShouJinE").val(shangPinJinEInfo.tiesijine);
                        $("#txtTieSiErWeiMa").focus();
                        break;
                    case "guangfashoukuan":
                        $("#li_shoukuanzhekou").show();
                        var _jine = tableToolbar.calculate(shangPinJinEInfo.shichangjine, "<%=GFK_ZheKou/10 %>", "*");
                        $("#li_shangpinjine").html('<span>商品金额：</span>' + shangPinJinEInfo.shichangjine.toFixed(2) + ' 元整');
                        $("#li_yingshoujine").html('<span>应收金额：</span>' + _jine.toFixed(2) + ' 元整');

                        $("#txtShangPinJinE").val(shangPinJinEInfo.shichangjine);
                        $("#txtYingShouJinE").val(_jine);
                        break;
                    default:
                        alert("请求异常");
                        this.close();
                        break;
                }
            },
            //初始化购买商品信息
            initShangPin: function () {
                var _items = top.shouYin.getShouYinShangPin();
                $("#txtShangPinJson").val(JSON.stringify(_items));
            },
            //计算找回金额
            jiSuanZhaoHuiJinE: function () {
                var _yingShouJinE = tableToolbar.getFloat($("#txtYingShouJinE").val());
                var _shiShouJinE = tableToolbar.getFloat($("#txtShiShouJinE").val());
                var _zhaoHuiJinE = tableToolbar.calculate(_shiShouJinE, _yingShouJinE, "-");
                if (_zhaoHuiJinE >= 0)
                    $("#li_zhaohuijine").html('<span>找回金额：</span>' + _zhaoHuiJinE.toFixed(2) + ' 元整');
                else
                    $("#li_zhaohuijine").html('<span>找回金额：</span><font style="color:#ff0000;">实收金额不足</font>');
            },
            //验证表单
            yanZhengForm: function () {
                if (this.SKFS == "xianjinshoukuan") {
                    var _yingShouJinE = tableToolbar.getFloat($("#txtYingShouJinE").val());
                    var _shiShouJinE0 = $.trim($("#txtShiShouJinE").val());
                    var _shiShouJinE = tableToolbar.getFloat(_shiShouJinE0);
                    if (_shiShouJinE0 == "") {
                        alert("请填写实收金额！");
                        return false;
                    }
                    if (_shiShouJinE0.length > 0 && _shiShouJinE < _yingShouJinE) {
                        alert("实收金额不能小于应收金额！");
                        return false;
                    }
                }

                if (this.SKFS == "tiesishoukuan") {
                    if ($.trim($("#txtTieSiErWeiMa").val()).length == 0) {
                        alert("请扫描铁丝二维码！");
                        return false;
                    }
                }

                return true;
            },
            //收款
            shouKuan: function (obj) {
                var _self = this;
                function _callback(data) {
                    $("#div_shoukuan_form").hide();
                    $("#div_shoukuan_danju").show();

                    var _s = [];

                    _s.push('<div class="print_table mt10">');
                    _s.push('<table width="100%"  border="0" cellpadding="0" cellspacing="0">');
                    _s.push('<tr><td>流水号：' + data.liushuihao + '</td></tr>');
                    _s.push('<tr><td>收银员：' + data.shoukuanrenid + '</td></tr>');
                    _s.push('<tr><td>时　间：' + data.shoukuanshijian1 + '</td></tr>');
                    _s.push('</table>');
                    _s.push('</div>');

                    _s.push('<div class="print_table mt10">');
                    _s.push('<table width="100%"  border="0" cellpadding="0" cellspacing="0">');
                    _s.push('<tr><td>品名</td><td>数量*单价</td><td>小计</td></tr>');
                    var _items = top.shouYin.getShouYinShangPin();
                    for (var i = 0; i < _items.length; i++) {
                        _s.push('<tr><td colspan="3">' + _items[i].shangpinname + '</td></tr>');
                        var _danJia = _items[i].shichangjiage;
                        var _xiaoJi = _items[i].shichangjine;

                        if (_self.SKFS == "tiesishoukuan") {
                            _danJia = _items[i].tiesijiage;
                            _xiaoJi = _items[i].tiesijiage;
                        }

                        _s.push('<tr><td>&nbsp;</td>&nbsp;<td>' + _items[i].shuliang + " " + _items[i].shangpindanwei + '*' + _danJia.toFixed(1) + '</td><td>' + _xiaoJi.toFixed(1) + '</td></tr>');
                    }
                    _s.push('</table>');
                    _s.push('</div>');

                    _s.push('<div class="print_table mt10">');
                    _s.push('<table width="160px"  border="0" cellpadding="0" cellspacing="0">');
                    _s.push('</br>');
                    _s.push('<tr><td>合计金额：' + data.shangpinjine.toFixed(2) + '</td></tr>');
                    var _zheKou = "无";
                    if (data.zhekou != 10) _zheKou = data.zhekou.toFixed(1) + '折';
                    _s.push('<tr><td>收款折扣：' + _zheKou + '</td></tr>');
                    _s.push('<tr><td>应收金额：' + data.yingshoujine.toFixed(2) + '</td></tr>');
                    _s.push('<tr><td>实收现金：' + data.shishoujine.toFixed(2) + '</td></tr>');
                    _s.push('<tr><td>实收铁丝币：' + data.shishoutiesijine.toFixed(2) + '</td></tr>');
                    _s.push('<tr><td>本次积分：' + data.jinfen + '</td></tr>');
                    _s.push('<tr><td>累计积分：' + data.zongjifen + '</td></tr>');
                    _s.push('</table>');
                    _s.push('</div>');

                    _s.push('<div class="cent mt10"><input type="button" value="打     印" class="boxy_btn" id="btnDaYinWindow" /></div>');
                    _s.push('<div class="cent mt10"><input type="button" value=" 收银成功 ! " class="boxy_btn" id="btnGuanBiWindow" /></div>');
                    var _$html = $(_s.join(''));
                    _$html.find("#btnDaYinWindow").click(function () { _self.daYin(); });
                    _$html.find("#btnGuanBiWindow").click(function () { top.shouYin.quXiaoShouYin(); _self.close(); });
                    $("#div_shoukuan_danju").append(_$html);
                    _self.daYin();
                }

                if (!this.yanZhengForm()) return;

                $(obj).unbind("click").css({ "color": "#999999" });
                $("#btnQuXiaoShouKuan").unbind("click").css({ "color": "#999999" });

                $.ajax({ type: "post", url: window.location.href + "&doType=shoukuan", data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                    success: function (response) {
                        if (response.result != "1") {
                            alert(response.msg);
                            $(obj).click(function () { iPage.shouKuan(this); }).css({ "color": "" });
                            $("#btnQuXiaoShouKuan").click(function () { iPage.close(); }).css({ "color": "" });
                            return;
                        }
                        _callback(response.obj);
                    },
                    error: function () {
                        $(obj).click(function () { iPage.shouKuan(this); }).css({ "color": "" });
                        $("#btnQuXiaoShouKuan").click(function () { iPage.close(); }).css({ "color": "" });
                    }
                });
            }
        };

        $(document).ready(function () {
            iPage.initJinEXinXi();
            iPage.initShangPin();

            $("#btnQuXiaoShouKuan").click(function () { iPage.close(); });
            $("#txtShiShouJinE").change(function () { iPage.jiSuanZhaoHuiJinE(); });
            $("#btnShouKuan").click(function () { iPage.shouKuan(this); });
        });
    </script>
</asp:Content>