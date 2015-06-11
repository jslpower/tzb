//收银端js 2014-11-27
//require:/js/jquery-1.4.4.js
//require:/js/jquery.boxy.js
//require:/js/table-toolbar.js

var shouYin = {
    //_T类型:[A：收银,B：退款,C:流水,O:其它]
    _T: "O",
    //设置类型
    setT: function (t) {
        if (t != "A" && t != "B" && t != "C" && t != "O") return;
        this._T = t;
    },
    //获取单个商品明细信息
    _getShangPinMingXi: function (obj) {
        this.setT("A");
        $(".div_shouyin_qita").hide();
        $("#div_shouyin_shouyintai").show();
        var _$obj = $(obj);
        var _data = { txtShangPinId: _$obj.attr("data-shangpinid") };
        //查找是否存大该商品
        var _$input = $("#table_shouyin_shouyintai_mingxi").find("input[name='txtShangPinId'][value='" + _data.txtShangPinId + "']");
        //存在选中商品数量++
        if (_$input.length > 0) {
            var __$jia = _$input.closest("tr").find(".shuliang_jia");
            __$jia.click();
            return;
        }
        //设置收银商品头部宽度 有无滚动条
        function _setTableHeadWidth() {
            var __h1 = $("#table_shouyin_shouyintai_mingxi").height();
            var __h2 = $("#div_shouyin_shouyintai_mingxi").height();

            if (__h1 < __h2) {
                $("#table_shouyin_shouyintai_toubu").find("th").eq(6).width(50);
            } else {
                $("#table_shouyin_shouyintai_toubu").find("th").eq(6).width(50 + 17);
            }
        }
        //删除商品
        function _shanChu(obj) {
            $(obj).closest("tr").remove();
            _setTableHeadWidth();
            _heJiJinE();
        }
        //商品数量增加
        function _jian(obj) {
            var __$input = $(obj).closest("td").find("input[name='txtShuLiang']");
            var __v = tableToolbar.getInt(__$input.val());
            __v--;
            if (__v < 0) __v = 0;
            __$input.val(__v);
            __$input.change();
        }
        //商品数量减少
        function _jia(obj) {
            var __$input = $(obj).closest("td").find("input[name='txtShuLiang']");
            var __v = tableToolbar.getInt(__$input.val());
            __v++;
            __$input.val(__v);
            __$input.change();
        }
        //合计收银总金额
        function _heJiJinE() {
            var __shiChangJinEHeJi = 0; var __tieSiJinEHeJi = 0;
            $("#table_shouyin_shouyintai_mingxi").find("tr").each(function () {
                var __shiChangJinE = $(this).find("input[name='txtShiChangJinE']").val();
                var __tieSiJinE = $(this).find("input[name='txtTieSiJinE']").val();
                __shiChangJinEHeJi = tableToolbar.calculate(__shiChangJinEHeJi, __shiChangJinE, "+");
                __tieSiJinEHeJi = tableToolbar.calculate(__tieSiJinEHeJi, __tieSiJinE, "+");
            });

            $("#span_shouyin_shouyintai_heji").html(__shiChangJinEHeJi.toFixed(2));
        }
        //合计单个商品金额
        function _heJiShangPinJinE(obj) {
            var __$input = $(obj);
            var __$tr = __$input.closest("tr");
            var __shuLiang = tableToolbar.getInt(__$input.val());
            var __shiChangJiaGe = tableToolbar.getFloat(__$tr.find("input[name='txtShiChangJiaGe']").val());
            var __tieSiJiaGe = tableToolbar.getFloat(__$tr.find("input[name='txtTieSiJiaGe']").val());
            var __shiChangJinE = tableToolbar.calculate(__shuLiang, __shiChangJiaGe, "*");
            var __teiSiJinE = tableToolbar.calculate(__shuLiang, __tieSiJiaGe, "*");
            __$tr.find("input[name='txtShiChangJinE']").val(__shiChangJinE.toFixed(2));
            __$tr.find("input[name='txtTieSiJinE']").val(__teiSiJinE.toFixed(2));

            _heJiJinE();
        }
        //异常获取商品信息callback
        function _callback(data) {
            var __s = [];

            __s.push('<tr>');
            __s.push('<td>' + data.GoodsName + '<input type="hidden" name="txtShangPinId" value="' + data.ID + '" /></td>');
            __s.push('<td width="50">' + data.Unit + '</td>');
            __s.push('<td width="90"><span class="dindan_num"><a href="javascript:void(0)" class="shuliang_jian">-</a><input type="text" value="1" name="txtShuLiang" /><a href="javascript:void(0)" class="shuliang_jia">+</a></span></td>');
            __s.push('<td width="70">' + data.Price.toFixed(2) + '<input type="hidden" name="txtShiChangJiaGe" value="' + data.Price + '"/><input type="hidden" name="txtTieSiJiaGe" value="' + data.CurrencyPrice + '"/></td>');
            __s.push('<td width="80"><input type="text" value="' + data.Price.toFixed(2) + '" class="inputbk formsize70" name="txtShiChangJinE" readonly="readonly"></td>');
            __s.push('<td width="80"><input type="text" value="' + data.CurrencyPrice.toFixed(2) + '" class="inputbk formsize70" name="txtTieSiJinE" readonly="readonly"></td>');
            __s.push('<td width="50"><a href="javascript:void(0);" class="shanchu"><img width="16" height="20" src="images/del_icon.png"></a></td>');
            __s.push('</tr>');

            var __$tr = $(__s.join(''));
            __$tr.find('.shanchu').click(function () { _shanChu(this); });
            __$tr.find('input[name="txtShuLiang"]').change(function () { _heJiShangPinJinE(this); });
            __$tr.find('.shuliang_jian').click(function () { _jian(this); });
            __$tr.find('.shuliang_jia').click(function () { _jia(this); });

            __$tr.data("shangpininfo", data);

            $("#table_shouyin_shouyintai_mingxi").append(__$tr);
            _heJiJinE();
            _setTableHeadWidth();
        }
        //获取商品信息
        $.ajax({ type: "post", url: "/shouyin/default.aspx?doType=getshangpinmingxi", data: _data, cache: false, dataType: "json", async: false,
            success: function (response) {
                if (response.result != "1") { alert(response.msg); return; }
                _callback(response.obj);
            },
            error: function () { }
        });
    },
    //初始化货架商品信息
    initHuoJiaShangPin: function () {
        var _self = this;
        $.ajax({ type: "get", url: "/shouyin/default.aspx?doType=gethuojiashangpin", cache: false, dataType: "json", async: false,
            success: function (response) {
                if (response.result != "1") { alert(response.msg); return; }
                $("#table_shouyin_huojia").html(response.obj);
                $("#table_shouyin_huojia").find("tr:odd").addClass("even");
                $("#table_shouyin_huojia").find("a[data-class='shangpin']").click(function () { _self._getShangPinMingXi(this); });
            },
            error: function () { }
        });
    },
    //获取收银商品信息
    getShouYinShangPin: function () {
        var _items = [];
        $("#table_shouyin_shouyintai_mingxi").find("tr").each(function () {
            var _$tr = $(this);
            var _shangPinInfo = _$tr.data("shangpininfo");
            var _item = { shangpinid: _shangPinInfo.ID
                , shangpinname: _shangPinInfo.GoodsName
                , shangpindanwei: _shangPinInfo.Unit
                , qiuchangid: _shangPinInfo.BallFieldId
                , shichangjiage: _shangPinInfo.Price
                , tiesijiage: _shangPinInfo.CurrencyPrice
                , shuliang: 0
                , shichangjine: 0
                , tiesijine: 0
            };

            _item.shuliang = tableToolbar.getInt(_$tr.find('input[name="txtShuLiang"]').val());
            _item.shichangjine = tableToolbar.calculate(_item.shuliang, _item.shichangjiage, "*");
            _item.tiesijine = tableToolbar.calculate(_item.shuliang, _item.tiesijiage, "*");

            _items.push(_item);
        });
        return _items;
    },
    //获取需要收银的商品数量
    getShouYinShangPinShuLiang: function () {
        var _shuLiangHeJi = 0;
        var _items = this.getShouYinShangPin();
        for (var i = 0; i < _items.length; i++) {
            _shuLiangHeJi = tableToolbar.calculate(_shuLiangHeJi, _items[i].shuliang, "+");
        }
        return _shuLiangHeJi;
    },
    //获取需要收银的商品金额信息
    getShouYinShangPinJinE: function () {
        var _info = { tiesijine: 0, shichangjine: 0 };
        var _items = this.getShouYinShangPin();
        for (var i = 0; i < _items.length; i++) {
            _info.shichangjine = tableToolbar.calculate(_info.shichangjine, _items[i].shichangjine, "+");
            _info.tiesijine = tableToolbar.calculate(_info.tiesijine, _items[i].tiesijine, "+");
        }
        return _info;
    },
    //获取需要退款的商品信息集合
    getTuiKuanItems: function () {
        var _items = [];
        $("#table_shouyin_dingdanmingxi").find(".dingdanmingxi_item").each(function () {
            var _$tr = $(this);
            if (_$tr.find(".tuikuan_on1").length > 0) {
                _items.push(_$tr.attr("data-mingxiid"));
            }
        });
        return _items;
    },
    //现金收款
    xianJinShouKuan: function () {
        if (this._T != "A") { alert("请选择需要收银收款的商品"); return; }
        if (this.getShouYinShangPinShuLiang() < 1) { alert("请选择需要收银收款的商品"); return; }
        top.Boxy.iframeDialog({ iframeUrl: "/shouyin/shoukuan.aspx?skfs=xianjinshoukuan", title: "现金收款", modal: true, width: "460px", height: "260px", closeable: false });
        return false;
    },
    //铁丝收款
    tieSiShouKuan: function () {
        if (this._T != "A") { alert("请选择需要收银收款的商品"); return; }
        if (this.getShouYinShangPinShuLiang() < 1) { alert("请选择需要收银收款的商品"); return; }
        top.Boxy.iframeDialog({ iframeUrl: "/shouyin/shoukuan.aspx?skfs=tiesishoukuan", title: "铁丝二维码收款", modal: true, width: "460px", height: "260px", closeable: false });
        return false;
    },
    //广发收款
    guangFaShouKuan: function () {
        if (this._T != "A") { alert("请选择需要收银收款的商品"); return; }
        if (this.getShouYinShangPinShuLiang() < 1) { alert("请选择需要收银收款的商品"); return; }
        top.Boxy.iframeDialog({ iframeUrl: "/shouyin/shoukuan.aspx?skfs=guangfashoukuan", title: "广发银行收款", modal: true, width: "460px", height: "260px", closeable: false });
        return false;
    },
    //现金退款
    xianJinTuiKuan: function () {
        if (this._T != "B") { alert("请选择需要退款的商品"); return; }
        var PayType = $("#hidPayType").val();
        var _items = this.getTuiKuanItems();
        if (_items.length == 0) { alert("请选择需要退款的商品"); return; }
        if (PayType == 3) { alert("该订单为铁丝卡付款，请选择铁丝卡退款！"); return; }
        //alert("退款操作暂未实现");
        top.Boxy.iframeDialog({ iframeUrl: "/shouyin/Refund.aspx?Type=Cash", title: "现金退款", modal: true, width: "460px", height: "260px", closeable: false });
        return false;

    },
    //铁丝退款
    tieSiTuiKuan: function () {
        if (this._T != "B") { alert("请选择需要退款的商品"); return; }
        var PayType = $("#hidPayType").val();
        var _items = this.getTuiKuanItems();
        if (_items.length == 0) { alert("请选择需要退款的商品"); return; }
        if (PayType != 3) { alert("该订单非铁丝卡付款，请选择现金退款！"); return; }
        //alert("退款操作暂未实现");
        top.Boxy.iframeDialog({ iframeUrl: "/shouyin/Refund.aspx?Type=Card", title: "二维码退款", modal: true, width: "460px", height: "260px", closeable: false });
        return false;
    },
    //取消收银
    quXiaoShouYin: function () {
        this.setT("A");
        $(".div_shouyin_qita").hide();
        $("#div_shouyin_shouyintai").show();
        $("#table_shouyin_shouyintai_mingxi").find("tr").remove();
        $("#span_shouyin_shouyintai_heji").html("0.00");
    },
    //铁丝充值
    tieSiChongZhi: function () {
        top.Boxy.iframeDialog({ iframeUrl: "/shouyin/chongzhi.aspx", title: "铁丝充值", modal: true, width: "460px", height: "245px", closeable: false });
        return false;
    },
    //退出
    logout: function () {
        if (!confirm("是否退出当前登录？")) return false;
        window.location.href = "/userlogout.aspx";
        return false;
    }
};
