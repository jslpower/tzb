<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Refund.aspx.cs" Inherits="Enow.TZB.Web.ShouYin.Refund" MasterPageFile="~/ShouYin/Boxy.Master" %>
<asp:Content ContentPlaceHolderID="CPH_ZhuTi" ID="CPH_ZhuTi1" runat="server">
    <style type="text/css">
    .boxy_form li{padding-bottom:10px;}
    .dn{display:none;}
    </style>
    <form id="form1">    
    <div style="display:none;">
        <input type="hidden" name="txtRefundMoney" id="txtRefundMoney" value="0" />
        <input type="hidden" name="txtZheKou" id="txtZheKou" value="<%=GFK_ZheKou %>" />
        <input type="hidden" name="hidRefundTypeId" id="hidRefundTypeId" value="<%=SKFS %>" />
        <input type="hidden" name="hidId" id="hidId" value="0" />
    </div>
    <div class="boxy_form" id="div_shoukuan_form">
        <ul>
            <li id="li_RefundMoney" class=""><span>退款金额：</span>0.00 元整</li>
        </ul>
        <div class="cent mt10">
            <input type="button" value="退款并打印小票" class="boxy_btn" id="btnRefund" /><input
                type="button" value="取  消" class="boxy_btn" id="btnQuXiaoShouKuan" /></div>
    </div>
    </form>

    <div class="print_box" id="div_shoukuan_danju" style="display:none;">
        <div class="print_head clearfix">
            <div class="print_logo floatL" style="text-align:center;">
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
                var RefundMoney = 0;
                var shangPinJinEInfo = top.shouYin.getTuiKuanItems();
                if (shangPinJinEInfo.length < 1) {
                    alert("请选择要退款的商品！");
                    this.close();
                    return false;
                } else {
                    //循环退款商品，并取得退款商品信息及金额 
                    for (var i = 0; i < shangPinJinEInfo.length; i++) {

                        $.ajax({
                            type: "get",
                            url: "/Ashx/GetOrderDetail.ashx?Id=" + shangPinJinEInfo[i] + "&t=" + Math.random(),
                            cache: false,
                            dataType: "json",
                            async: false,
                            success: function (response) {
                                if (response.result == "1") {
                                    //var ReM = tableToolbar.calculate(response.data.JinE, response.data.ZheKou / 10, "*");
                                    RefundMoney = tableToolbar.calculate(RefundMoney, response.data.JinE, "+");
                                } else {
                                    alert("不正确的商品信息！");
                                    this.close();
                                    return false;
                                }
                            },
                            error: function () {
                                alert("请求异常");
                                this.close();
                                return false;
                            }
                        });

                    }
                    switch (this.SKFS) {
                        case "Cash":
                            $("#txtRefundMoney").val(RefundMoney.toFixed(2));
                            $("#li_RefundMoney").html('<span>退款金额：</span>' + RefundMoney.toFixed(2) + ' 元整');
                            break;
                        case "Card":
                            $("#li_RefundMoney").html('<span>退款金额：</span>' + RefundMoney.toFixed(2) + ' 元整');
                            break;
                        default:
                            alert("请求异常");
                            this.close();
                            break;
                    }
                }
            },
            //初始化购买商品信息
            initShangPin: function () {
                var shangPinJinEInfo = top.shouYin.getTuiKuanItems();
                $("#hidId").val(shangPinJinEInfo.join(","));
            },
            //验证表单
            yanZhengForm: function () {
                if (this.SKFS != "Cash" && this.SKFS != "Card") {
                    alert("请求异常！");
                    return false;
                }
                return true;
            },
            //退款
            Refund: function (obj) {
                var _self = this;
                function _callback(data) {
                    $("#div_shoukuan_form").hide();
                    $("#div_shoukuan_danju").show();

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
                    _s.push('<tr><td>品名</td><td>数量*单价</td><td>实收</td></tr>');
                    var _items = data.ProductList;
                    //循环退款商品，并取得退款商品信息及金额 
                    for (var i = 0; i < _items.length; i++) {

                        _s.push('<tr><td colspan="3">' + _items[i].GoodsName + '</td></tr>');
                        var _danJia = _items[i].Price;
                        var _xiaoJi = _items[i].JinE;

                        if (_self.SKFS == "Card") {
                            _danJia = _items[i].Cprice;
                        }

                        _s.push('<tr><td>&nbsp;</td><td>' + _items[i].Amount + _items[i].GoodUnit + '*' + _danJia.toFixed(2) + '</td><td>' + _xiaoJi.toFixed(2) + '</td></tr>');

                    }

                    _s.push('</table>');
                    _s.push('</div>');
                    _s.push('<div class="print_table mt10">');
                    _s.push('<table width="100%" border="0" cellpadding="0" cellspacing="0">');
                    _s.push('<tr><td>退款金额：' + data.shishoutiesijine.toFixed(2) + '</td></tr>');
                    _s.push('<tr><td>累计积分：' + data.zongjifen + '</td></tr>');
                    _s.push('</table>');
                    _s.push('</div>');

                    _s.push('<div class="cent mt10"><input type="button" value="打     印" class="boxy_btn" id="btnDaYinWindow" /></div>');
                    _s.push('<div class="cent mt10"><input type="button" value="关闭窗口" class="boxy_btn" id="btnGuanBiWindow" /></div>');
                    var _$html = $(_s.join(''));
                    _$html.find("#btnDaYinWindow").click(function () { _self.daYin(); });
                    _$html.find("#btnGuanBiWindow").click(function () { top.shouYin.quXiaoShouYin(); _self.close(); });
                    $("#div_shoukuan_danju").append(_$html);
                    _self.daYin();
                }

                if (!this.yanZhengForm()) return;

                $(obj).unbind("click").css({ "color": "#999999" });
                $("#btnQuXiaoShouKuan").unbind("click").css({ "color": "#999999" });

                $.ajax({ type: "post", url: window.location.href + "&doType=Refund", data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                    success: function (response) {
                        if (response.result != "1") {
                            alert(response.msg);
                            $(obj).click(function () { iPage.shouKuan(this); }).css({ "color": "" });
                            $("#btnQuXiaoShouKuan").click(function () { iPage.close(); }).css({ "color": "" });
                            return;
                        }
                        _callback(response.obj[0]);
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
            $("#btnRefund").click(function () { iPage.Refund(this); });
        });
    </script>
</asp:Content>