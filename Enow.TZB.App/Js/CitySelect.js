//国家、省份、城市、县区联动菜单
var gscx = {
    _loadHTML: "<option value=\"\">加载中...</option>",
    _defaultHTML: "<option value=\"\">--请选择--</option>",
    //获取对象 objId:jQuery#选择器
    _getObj: function (objId) {
        if (objId.length == 0) return null;
        var _$obj = $(objId);
        if (_$obj.length == 0) return null;
        return _$obj;
    },
    //设置下拉菜单项 _$obj:下拉菜单$object items:数据项集合 sv:要选中的值
    _setSelectOptions: function (_$obj, items, sv) {
        _$obj.html("").html(this._defaultHTML);
        if (items.length == 0) return;

        var _s = [];
        for (var i = 0; i < items.length; i++) {
            if (sv == items[i].id) _s.push("<option selected=\"selected\" value=\"" + items[i].id + "\">" + items[i].name + "</option>");
            else _s.push("<option value=\"" + items[i].id + "\">" + items[i].name + "</option>");
        }

        _$obj.append(_s.join(""));
    },
    //发起请求 t:请求类型 g国家 s省份 c城市 x县区 pv:上一级 fn:回调函数
    _post: function (t, options, pv, fn) {
        var _self = this; var _$obj = null; var _data = {}; var _sv = "";

        switch (t) {
            case "g": _$obj = this._getObj(options.gid); _data = { "get": "g" }; _sv = options.gv; break;
            case "s": _$obj = this._getObj(options.sid); _data = { "get": "p", "gid": pv }; _sv = options.sv; break;
            case "c": _$obj = this._getObj(options.cid); _data = { "get": "c", "pid": pv }; _sv = options.cv; break;
            case "x": _$obj = this._getObj(options.xid); _data = { "get": "x", "cid": pv }; _sv = options.xv; break;
            default: break;
        }

        if (!_$obj) { if (fn) fn(); return; }

        _$obj.html("").html(_self._loadHTML);

        $.ajax({
            type: "get", cache: false, dataType: "json",
            url: "/ashx/GetWxProvinceAndCity.ashx?companyId=" + options.comid + "&isCy=" + options.t + "&lng=" + options.lng + "&" + $.param(_data),
            success: function (response) {
                if (response && response.list != null) {
                    _self._setSelectOptions(_$obj, response.list, _sv);
                    if (fn) { fn(); }
                }
                else {
                    tableToolbar._showMsg("服务器忙！");
                    return;
                }
            },
            error: function () {
                tableToolbar._showMsg("服务器忙！");
                return;
            }
        });
    },
    //设置县区
    _setX: function (options) {
        var _cv = "1";
        var _$c = this._getObj(options.cid);
        if (_$c) _cv = _$c.val();
        this._post("x", options, _cv, null);
    },
    //设置城市
    _setC: function (options) {
        var _sv = "1";
        var _$s = this._getObj(options.sid);
        if (_$s) _sv = _$s.val();
        var _self = this;
        this._post("c", options, _sv, function () { _self._setX(options); });
    },
    //设置省份
    _setS: function (options) {
        var _gv = "1";
        var _$g = this._getObj(options.gid);
        if (_$g) _gv = _$g.val();
        var _self = this;
        this._post("s", options, _gv, function () { _self._setC(options); });
    },
    //设置国家
    _setG: function (options) {
        var _self = this;
        this._post("g", options, "", function () { _self._setS(options); });
    },
    //options={gid:"#国家下拉菜单",sid:"#省份下拉菜单",cid:"#城市下拉菜单",xid:"#县区下拉菜单",gv:"国家选中的值",sv:"省份选中的值",cv:"城市选中的值",xv:"县区选中的值",comid:"公司编号",t:"0所有城市 1常用城市",lng:"语言"}
    init: function (options) {
        var _options = { gid: "", sid: "", cid: "", xid: "", gv: "", sv: "", cv: "", xv: "", comid: "", t: "0", lng: "0" };
        options = $.extend(_options, options);
        var _$g = this._getObj(options.gid);
        var _$s = this._getObj(options.sid);
        var _$c = this._getObj(options.cid);
        var _$x = this._getObj(options.xid);
        var _self = this;
        if (_$g) { _$g.bind("change", function () { _self._setS(options); }); }
        if (_$s) { _$s.bind("change", function () { _self._setC(options); }); }
        if (_$c) { _$c.bind("change", function () { _self._setX(options); }); }
        if (_$x) { }

        this._setG(options);
    }
};
var pcToobar = {
    ginit: function (options) {
        this.init(options);
    },
    init: function (options) {
        var _options = { gid: options.gID, sid: options.pID, cid: options.cID, xid: options.xID, gv: options.gSelect, sv: options.pSelect, cv: options.cSelect, xv: options.xSelect, comid: options.comID, t: options.isCy, lng: options.lng }
        gscx.init(_options);
    }
};