//信托产品选择
var TrustProduct = {
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
    //发起请求 t:请求类型 g一级类别 s二级类别 p 产品 z:产品组合 m:投资金额 pv:上一级 fn:回调函数
    _post: function (t, options, pv, fn) {
        var _self = this; var _$obj = null; var _data = {}; var _sv = "";

        switch (t) {
            case "g": _$obj = this._getObj(options.gid); _data = { "get": "g" }; _sv = options.gv; break;
            case "s": _$obj = this._getObj(options.sid); _data = { "get": "s", "gid": pv, "deptid": options.dv }; _sv = options.sv; break;
            case "p": _$obj = this._getObj(options.pid); _data = { "get": "p", "sid": pv }; _sv = options.pv; break;
            case "z": _$obj = this._getObj(options.zid); _data = { "get": "z", "pid": pv }; _sv = options.zv; break;
            case "m": _$obj = this._getObj(options.mid); _data = { "get": "m", "zid": pv }; _sv = options.mv; break;
            default: break;
        }

        if (!_$obj) { if (fn) fn(); return; }

        _$obj.html("").html(_self._loadHTML);

        $.ajax({
            type: "get", cache: false, dataType: "json",
            url: "/ashx/GetTurstProduct.ashx?" + $.param(_data),
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
    //设置一级类别
    _setG: function (options) {
        var _self = this;
        this._post("g", options, "", function () { _self._setS(options); });
    },
    //设置二级类别
    _setS: function (options) {
        var _gv = "1";
        var _$g = this._getObj(options.gid);
        if (_$g) _gv = _$g.val();
        var _self = this;
        this._post("s", options, _gv, function () { _self._setP(options); });
    },
    //设置产品系列
    _setP: function (options) {
        var _sv = "1";
        var _$s = this._getObj(options.sid);
        if (_$s) _sv = _$s.val();
        var _self = this;
        this._post("p", options, _sv, function () { _self._setZ(options); });
    },
    //设置产品组合
    _setZ: function (options) {
        var _pv = "0";
        var _$p = this._getObj(options.pid);
        if (_$p) _pv = _$p.val();
        var _self = this;
        this._post("z", options, _pv, function () { _self._setM(options); });
    },
    //设置产品组合的投资金额
    _setM: function (options) {
        var _zv = "0";
        var _$z = this._getObj(options.zid);
        if (_$z) _zv = _$z.val();
        this._post("m", options, _zv, null);
    },
    //options={gid:"#一级类别下拉菜单",sid:"#二级类别下拉菜单",pid:"#产品系列下拉菜单,zid:"#产品组合下拉菜单,mid:"#产品组合金额下拉菜单",gv:"一级类别选中的值",sv:"二级类别选中的值",pv:"产品系列选中的值",zv:"产品组合选中的值",mv:"产品组合金额选中的值",dv:"部门编号值"}
    init: function (options) {
        var _options = { gid: "", sid: "", pid: "", zid: "", mid: "", gv: "", sv: "", pv: "", zv: "", mv: "",dv:"" };
        options = $.extend(_options, options);
        var _$g = this._getObj(options.gid);
        var _$s = this._getObj(options.sid);
        var _$p = this._getObj(options.pid);
        var _$z = this._getObj(options.zid);
        var _self = this;
        if (_$g) { _$g.bind("change", function () { _self._setS(options); }); }
        if (_$s) { _$s.bind("change", function () { _self._setP(options); }); }
        if (_$p) { _$p.bind("change", function () { _self._setZ(options); }); }
        if (_$z) { _$z.bind("change", function () { _self._setM(options); }); }
        this._setG(options);
    }
};
var TrustProductToobar = {
    ginit: function (options) {
        this.init(options);
    },
    init: function (options) {
        var _options = { gid: options.gID, sid: options.sID, pid: options.pID, zid: options.zID, mid: options.mID, gv: options.gSelect, sv: options.sSelect, pv: options.pSelect, zv: options.zSelect, mv: options.mSelect, dv: options.deptId }
        ProductSelect.init(_options);
    }
};