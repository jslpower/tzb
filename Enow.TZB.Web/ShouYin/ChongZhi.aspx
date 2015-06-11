<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChongZhi.aspx.cs" Inherits="Enow.TZB.Web.ShouYin.ChongZhi"
    MasterPageFile="~/ShouYin/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="CPH_ZhuTi" ID="CPH_ZhuTi1" runat="server">
    <style type="text/css">
    .boxy_form li{padding-bottom:10px;}
    </style>
    <form id="form1">
    <div style="display: none;">
        <input type="hidden" id="txtYingShouJinE" name="txtYingShouJinE" value="0.00" />
    </div>
    <div class="boxy_form" id="div_chongzhi_form">
        <ul>
            <li>类型：<asp:Literal ID="ltrPayType" runat="server"></asp:Literal></li>
            <li><span>会员手机号：</span><input name="txtShouJiHao" id="txtShouJiHao" type="text" class="formsize180 input_style"
                value="" /></li>
            <li><span>充值数量：</span><input name="txtShuLiang" id="txtShuLiang" type="text" class="formsize180 input_style" /></li>
            <li id="li_yingfujine"><span>应付金额：</span>0.00 元</li>            
        </ul>
        <div class="cent mt10">
            <input type="button" value="充值并打印小票" class="boxy_btn" id="btnChongZhi" /><input type="button"
                value="取  消" class="boxy_btn" id="btnQuXiaoChongZhi" /></div>
    </div>
    </form>
    <div class="print_box" id="div_chongzhi_danju" style="display: none;">
        <div class="print_head clearfix">
            <div class="print_logo floatL" style="text-align:center;">
                <img src="images/print_logo.gif"></div>
            <div class="print_title floatR">
                <%=QiuChangName %></div>
        </div>
    </div>
    <OBJECT classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="wb" name="wb" width="0"></OBJECT>
    <script type="text/javascript">
        var iPage = {
            //关闭窗口
            close: function () {
                top.Boxy.getIframeDialog('<%=Request["iframeId"] %>').hide();
                return false;
            },
            //打印窗口
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
            //验证表单
            yanZhengForm: function () {
                var _shuLiang0 = $.trim($("#txtShuLiang").val());
                var _shuLiang = tableToolbar.getInt(_shuLiang0);
                var _shouJiHao = $.trim($("#txtShouJiHao").val());

                if (_shuLiang <= 0) { alert("请输入充值数量"); return false; }
                if (_shouJiHao.length <= 0) { alert("请输入会员手机号"); return false; }

                return true;
            },
            //充值
            chongZhi: function (obj) {
                var _self = this;
                function _callback(data) {
                    $("#div_chongzhi_form").hide();
                    $("#div_chongzhi_danju").show();

                    var _s = [];

                    _s.push('<div class="print_table mt10">');
                    _s.push('<table width="100%" border="0" cellpadding="0" cellspacing="0">');
                    _s.push('<tr><td>流水号：' + data.liushuihao + '</td></tr>');
                    _s.push('<tr><td>收银员：' + data.shoukuanrenid + '</td></tr>');
                    _s.push('<tr><td>时　间：' + data.shoukuanshijian1 + '</td></tr>');
                    _s.push('</table>');
                    _s.push('</div>');

                    _s.push('<div class="print_table mt10">');
                    _s.push('<table width="100%" border="0" cellpadding="0" cellspacing="0">');
                    _s.push('<tr><td>充值数量：' + data.shuliang + '</td></tr>');
                    var _zheKou = "无";
                    if (data.zhekou != 10) _zheKou = data.zhekou.toFixed(1) + '折';
                    _s.push('<tr><td>充值折扣：' + _zheKou + '</td></tr>');
                    _s.push('<tr><td>应收金额：' + data.yingshoujine.toFixed(2) + '</td></tr>');
                    _s.push('<tr><td>实收现金：' + data.yingshoujine.toFixed(2) + '</td></tr>');
                    _s.push('<tr><td>铁丝币余额：' + data.zongshuliang.toFixed(2) + '</td></tr>');
                    _s.push('</table>');
                    _s.push('</div>');

                    _s.push('<div class="cent mt10"><input type="button" value="打     印" class="boxy_btn" id="btnDaYinWindow" /></div>');
                    _s.push('<div class="cent mt10"><input type="button" value="充值成功" class="boxy_btn" id="btnGuanBiWindow" /></div>');
                    var _$html = $(_s.join(''));
                    _$html.find("#btnDaYinWindow").click(function () { _self.daYin(); });
                    _$html.find("#btnGuanBiWindow").click(function () { top.shouYin.quXiaoShouYin(); _self.close(); });
                    $("#div_chongzhi_danju").append(_$html);

                    _self.daYin();
                }
                this.jiSuanYingShouJinE();
                if (!this.yanZhengForm()) return;

                $(obj).unbind("click").css({ "color": "#999999" });
                $("#btnQuXiaoChongZhi").unbind("click").css({ "color": "#999999" });

                $.ajax({ type: "post", url: window.location.href + "&doType=chongzhi", data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                    success: function (response) {
                        if (response.result != "1") {
                            alert(response.msg);
                            $(obj).click(function () { iPage.chongZhi(this); }).css({ "color": "" });
                            $("#btnQuXiaoChongZhi").click(function () { iPage.close(); }).css({ "color": "" });
                            return;
                        }
                        _callback(response.obj);
                    },
                    error: function () {
                        $(obj).click(function () { iPage.chongZhi(this); }).css({ "color": "" });
                        $("#btnQuXiaoChongZhi").click(function () { iPage.close(); }).css({ "color": "" });
                    }
                });
            },
            //计算应收金额
            jiSuanYingShouJinE: function () {
                var _payType = $('input:radio[name="rbPayType"]:checked').val();
                var _shuLiang0 = $.trim($("#txtShuLiang").val());
                var _shuLiang = tableToolbar.getInt(_shuLiang0);
                var _jinE = 0;
                if (_payType == "<%=(int)Enow.TZB.Model.收款类型.工商银行卡 %>")
                {
                    _jinE = tableToolbar.calculate(_shuLiang, "<%=GFK_ZheKou/10 %>", "*");
                }else{
                    _jinE = tableToolbar.calculate(_shuLiang, "<%=CZ_ZheKou/10 %>", "*");
                }
                $("#txtYingShouJinE").val(_jinE);
                $("#li_yingfujine").html('<span>应付金额：</span>' + _jinE.toFixed(2) + ' 元');
            }
        }

        $(document).ready(function () {
            $("#btnQuXiaoChongZhi").click(function () { iPage.close(); });
            $("#btnChongZhi").click(function () { iPage.chongZhi(this); });
            $("#txtShuLiang").change(function () { iPage.jiSuanYingShouJinE(); });
            $("input:radio[name='rbPayType']").change(function () { iPage.jiSuanYingShouJinE(); });
        })
    </script>
</asp:Content>
