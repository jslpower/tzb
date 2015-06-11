var tableToolbar = {
    //处理键盘事件 禁止后退键（Backspace）密码或单行、多行文本框除外
    forbidBackSpace: function (e) {
        var ev = e || window.event; //获取event对象
        var obj = ev.target || ev.srcElement; //获取事件源
        var t = obj.type || obj.getAttribute('type');
        //获取事件源类型
        //获取作为判断条件的事件类型
        var vReadOnly = obj.readOnly;
        var vDisabled = obj.disabled;
        //处理undefined值情况
        vReadOnly = (vReadOnly == undefined) ? false : vReadOnly;
        vDisabled = (vDisabled == undefined) ? true : vDisabled;
        //当敲Backspace键时，事件源类型为密码或单行、多行文本的，
        //并且readOnly属性为true或disabled属性为true的，则退格键失效
        var flag1 = ev.keyCode == 8 && (t == "password" || t == "text" || t == "textarea") && (vReadOnly == true || vDisabled == true);
        //当敲Backspace键时，事件源类型非密码或单行、多行文本的，则退格键失效
        var flag2 = ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea";
        //判断
        if (flag2 || flag1) return false;
    },
    init: function (options) {
        this.options = jQuery.extend(tableToolbar.DEFAULTS, options || {});

        this.container = $(this.options.tableContainerSelector);


        if (this.options.allSelectedCheckBoxSelector == "") {
            if (this.options.isHaveAllSelectedFun) {
                this.allSelectedBox = this.container.find("input[type='checkbox']").eq(0);
            } else {
                this.allSelectedBox = $([]);
            }
        } else {
            this.allSelectedBox = $(this.options.allSelectedCheckBoxSelector);
        }

        if (this.options.checkBoxSelector == "") {
            if (this.options.isHaveAllSelectedFun) {
                this.checkboxList = this.container.find("input[type='checkbox']:gt(0)");
            } else {
                this.checkboxList = this.container.find("input[type='checkbox']");
            }
        } else {
            this.checkboxList = $(this.options.checkBoxSelector);
        }

        this._initSelectedFun();
        this._initButtonFun();
        this._initTableStyle();
    },
    /* public */
    getSelectedColCount: function () {
        var count = 0;
        this.checkboxList.each(function () {
            if (this.checked) {
                count++;
            }
        });

        return count;
    },
    getSelectedCols: function () {
        var cols = [];
        this.checkboxList.each(function () {
            if (this.checked) {
                cols.push($(this).closest("tr"));
            }
        });

        return cols;
    },
    /* private */
    _initSelectedFun: function () {
        var self = this;
        if (this.allSelectedBox.length > 0) {
            this.allSelectedBox.click(function () {
                var check = this.checked;
                if (check) {
                    self.checkboxList.attr("checked", "checked");
                    self.container.find("tr").addClass("selected");
                } else {
                    self.checkboxList.removeAttr("checked");
                    self.container.find("tr").removeClass("selected");
                }
            });
        }

        this.checkboxList.click(function () {
            var check = this.checked;
            if (check) {
                self.allSelectedBox.attr("checked", "checked");
                $(this).closest("tr").addClass('selected');
            } else {
                $(this).closest("tr").removeClass('selected');
            }
        });
    },
    _initButtonFun: function () {
        var self = this;
        //this.add = $(this.add_selector);
        this.updateB = $(this.options.update_selector);
        if (this.updateB.length > 0) {
            this.updateB.click(function () {
                var count = self.getSelectedColCount();
                var isSucess = false;
                var msg;
                if (count == 0) {
                    isSucess = false;
                    msg = self.options.update_msg1.replace(new RegExp(self.options.objectNamePlaceHolder, "gm"), self.options.objectName);
                } else if (count > 1) {
                    isSucess = false;
                    msg = self.options.update_msg2.replace(new RegExp(self.options.objectNamePlaceHolder, "gm"), self.options.objectName);
                } else {
                    isSucess = true;
                }

                if (isSucess) {
                    if (self.options.updateCallBack != null) {
                        self.options.updateCallBack(self.getSelectedCols());
                        return false;
                    }
                } else {
                    //tip msg.
                    self._showMsg(msg);
                    return false;
                }
            });
        }

        this.deleteB = $(this.options.delete_selector);
        if (this.deleteB.length > 0) {
            this.deleteB.click(function () {
                var count = self.getSelectedColCount();
                var isSucess = false;
                var msg;
                if (count == 0) {
                    msg = self.options.delete_msg.replace(new RegExp(self.options.objectNamePlaceHolder, "gm"), self.options.objectName);
                } else {
                    isSucess = true;
                }



                if (isSucess) {
                    var rows = self.getSelectedCols();
                    if (self.IsHandleElse == "false") {
                        var msgList = new Array();
                        for (var i = 0; i < rows.length; i++) {
                            if (rows[i].find("input[type='checkbox']").val() != "on") {
                                if (rows[i].find("input[name='ItemUserID']").val() != self.UserID) {
                                    msgList.push("你没有当前选中项中第" + (i + 1) + "行数据的操作权限!");
                                }
                            }
                        }
                        if (msgList.length > 0) {
                            self._showMsg(msgList.join("<br />"));
                            return false;
                        }
                    }
                    var confirmMsg = self._formatMsg(self.options.delete_confirm_msg);
                    var _html = '<div style="padding: 10px 0 20px 10px;cursor: default;"><h1 style="margin:10px 5px;"><img src="/images/y-tanhao.gif" style=" vertical-align:middle; margin-right:5px;" />  {{msg}}</h1><input type="button" onclick="$.unblockUI();return false;"  value="取消" " style="width:64px; height:24px; border:0 none; background:url(/images/cx.gif);" /> <input type="button" id="BLOCKUI_YES" value="确 定" style="width:64px; height:24px; border:0 none; margin-left:20px; background:url(/images/cx.gif);" /> </div>';
                    $.blockUI({
                        message: _html.replace(/{{msg}}/, confirmMsg),
                        css: { backgroundColor: "#E9F4F9", borderColor: "#00446b", borderWidth: '1px', cursor: "pointer", color: "#ed0000", width: '375px' }
                    });
                    $("#BLOCKUI_YES").click(function () {
                        $.unblockUI();
                        if (self.options.deleteCallBack != null) {
                            self.options.deleteCallBack(rows);
                            return false;
                        }
                    });
                    return false;

                } else {
                    //tip msg. 
                    self._showMsg(msg);
                    return false;
                }
            });
        }

        this.cancel = $(this.options.cancel_selector);
        if (this.cancel.length > 0) {
            this.cancel.click(function () {
                var count = self.getSelectedColCount();
                var isSucess = false;
                var msg;
                if (count == 0) {
                    msg = self.options.cancel_msg.replace(new RegExp(self.options.objectNamePlaceHolder, "gm"), self.options.objectName);
                } else {
                    isSucess = true;
                }

                if (isSucess) {
                    var rows = self.getSelectedCols();
                    if (self.IsHandleElse == "false") {
                        var msgList = new Array();
                        for (var i = 0; i < rows.length; i++) {
                            if (rows[i].find("input[type='checkbox']").val() != "on") {
                                if (rows[i].find("input[type='hidden'][name='ItemUserID']").val() != self.UserID) {
                                    msgList.push("你没有当前选中项中第" + (i + 1) + "行数据的操作权限!");
                                }
                            }
                        }
                        if (msgList.length > 0) {
                            self._showMsg(msgList.join("<br />"));
                            return false;
                        }
                    }
                    if (self.options.cancelCallBack != null) {
                        self.options.cancelCallBack(rows);
                        return false;
                    }
                } else {
                    //tip msg.
                    self._showMsg(msg);
                    return false;
                }
            });
        }

        this.copy = $(this.options.copy_selector);
        if (this.copy.length > 0) {
            this.copy.click(function () {
                var count = self.getSelectedColCount();
                var isSucess = false;
                var msg;
                if (count == 0) {
                    isSucess = false;
                    msg = self.options.copy_msg1.replace(new RegExp(self.options.objectNamePlaceHolder, "gm"), self.options.objectName);
                } else if (count > 1) {
                    isSucess = false;
                    msg = self.options.copy_msg2.replace(new RegExp(self.options.objectNamePlaceHolder, "gm"), self.options.objectName);
                } else {
                    isSucess = true;
                }

                if (isSucess) {
                    if (self.options.copyCallBack != null) {
                        self.options.copyCallBack(self.getSelectedCols());
                        return false;
                    }
                } else {
                    //tip msg.
                    self._showMsg(msg);
                    return false;
                }
            });
        }
        //other buttons.
        for (var i = 0; i < this.options.otherButtons.length; i++) {
            var button = this.options.otherButtons[i];
            var jButton = $(button.button_selector);
            if (jButton.length > 0) {
                var fun = (function () {
                    var index = i;
                    return function () {
                        var button = self.options.otherButtons[index];
                        var count = self.getSelectedColCount();
                        var isSucess = false;
                        var msg = button.msg;
                        var msg2 = button.msg2; ;
                        if (self.sucessRule[button.sucessRulr] == 1) {
                            if (count == 1) {
                                isSucess = true;
                            }
                        } else if (self.sucessRule[button.sucessRulr] == 2) {
                            if (count > 0) {
                                isSucess = true;
                            }
                        }

                        if (isSucess) {
                            if (button.buttonCallBack != null) {
                                button.buttonCallBack(self.getSelectedCols());
                                return false;
                            }
                        } else {
                            //tip msg.
                            if (count > 1 && msg2) {
                                self._showMsg(msg2);
                            } else
                                self._showMsg(msg);
                            return false;
                        }
                    };
                })();
                jButton.click(fun);
            }
        }
    },
    _showMsg: function (msg, callbackfun) {
        var self = this;
        $.blockUI({
            message: self._dialogHtml.replace(/{{msg}}/, msg),
            showOverlay: false,
            centerX: true,
            centerY: false,
            css: { top: '170px', backgroundColor: "#FEF7CB", borderColor: "#D59228", borderWidth: '1px', cursor: "pointer", color: "#ed0000", "z-index": "9999" },
            timeout: 2500,
            onUnblock: callbackfun
        });

    },
    ShowConfirmMsg: function (msg, callbackfun, callbackfun2) {
        var confirmMsg = msg;
        var _html = '<div style="padding: 10px 0 20px 10px;cursor: default;"><h1 style="margin:10px 5px;"><img src="/images/y-tanhao.gif" style=" vertical-align:middle; margin-right:5px;" />  {{msg}}</h1><input type="button" id="BLOCKUI_NO" value="取消" " style="width:64px; height:24px; border:0 none; background:url(/images/cx.gif);" /> <input type="button" id="BLOCKUI_YES" value="确 定" style="width:64px; height:24px; border:0 none; margin-left:20px; background:url(/images/cx.gif);" /> </div>';
        $.blockUI({
            message: _html.replace(/{{msg}}/, confirmMsg),
            css: { backgroundColor: "#E9F4F9", borderColor: "#00446b", borderWidth: '1px', cursor: "pointer", color: "#ed0000", width: '375px' }
        });
        $("#BLOCKUI_YES").click(function () {
            $.unblockUI();
            if (callbackfun != null) {
                callbackfun();
                return false;
            }
        });
        $("#BLOCKUI_NO").click(function () {
            $.unblockUI();
            if (callbackfun2) {
                callbackfun2();
                return false;
            }
        })
        return false;
    },

    _formatMsg: function (msg) {
        return msg.replace(new RegExp(this.options.objectNamePlaceHolder, "gm"), this.options.objectName);
    },
    _dialogHtml: '<h3 style="padding:20px 0">{{msg}}</h3>',
    _initTableStyle: function () {
        //隔行,滑动,点击 变色.+ 单选框选中的行 变色:
        this.container.find("tr:even").addClass('odd');
        this.container.find("tr").hover(
			function () { $(this).addClass('highlight'); },
			function () { $(this).removeClass('highlight'); }
		);
        // 如果单选框默认情况下是选择的，变色.
        //$('#liststyle input[type="radio"]:checked').parents('tr').addClass('selected');

    },
    sucessRule: [0, 1, 2], /* 0index作为占位存在不起作用，1index代表只能选中一个,2index代表可以选择多个  */
    DEFAULTS: {
        tableContainerSelector: "#liststyle",
        allSelectedCheckBoxSelector: "",
        checkBoxSelector: "",
        isHaveAllSelectedFun: true,
        objectName: "列",
        objectNamePlaceHolder: "{{colName}}",
        add_selector: ".toolbar_add",
        update_selector: ".toolbar_update",
        delete_selector: ".toolbar_delete",
        cancel_selector: ".toolbar_cancel",
        copy_selector: ".toolbar_copy",


        /* example */
        /*
        otherButton:[{
        button_selector:'',
        sucessRulr:1
        msg:'fssdsdfd',
        buttonCallBack:function(){}
        }]
			
        */
        otherButtons: [],

        update_msg1: "未选中任何{{colName}}",
        update_msg2: "只能选择一条{{colName}} 进行修改",
        delete_msg: "未选中任何{{colName}}",
        delete_confirm_msg: "确定删除选中的{{colName}}？删除后不可恢复！",
        cancel_msg: "未选中任何{{colName}}",
        copy_msg1: "未选中任何{{colName}}",
        copy_msg2: "只能选择一条 {{colName}} 进行复制",
        stop_msg1: "确定停用选中的{{colName}}？",
        start_msg1: "确定启用选中的{{colName}}？",
        blacklist_msg1: "确定选中的{{colName}}要加入黑名单吗？",
        updateCallBack: null,
        deleteCallBack: null,
        cancelCallBack: null,
        copyCallBack: null
    },
    //转换int值，转换失败返回0
    getInt: function (o) {
        if (parseInt(o)) {
            return parseInt(o);
        }
        return 0;
    },
    /*
    //转换float值，转换失败返回0
    getFloat: function(o) {
    if (parseFloat(o)) {
    return parseInt(parseFloat(o) * 100) / 100;
    }
    return 0;
    },
    //精确两位小数的计算a=value,b=value ,c= + || - || * || /
    calculate: function(a, b, c) {
    switch (c) {
    case "+":
    return this.getInt((this.getFloat(a) * 100 + this.getFloat(b) * 100)) / 100;
    case "-":
    return this.getInt((this.getFloat(a) * 100 - this.getFloat(b) * 100)) / 100;
    case "*":
    return this.getInt(this.getFloat(a) * this.getFloat(b) * 100) / 100;
    case "/":
    b = this.getFloat(b);
    if (b == 0) { return 0; }
    return this.getInt(this.getFloat(a) / this.getFloat(b) * 100) / 100;
    }

    return 0;
    },*/
    //转换float值，转换失败返回0,保留二位小数（四舍五入）
    getFloat: function (o) {
        if (isNaN(o)) return 0;

        if (parseFloat(o)) {
            var _v = parseFloat(o) * 100;
            _v = _v / 100;

            return Math.round(parseFloat(_v.toFixed(2)) * 100) / 100;
        }

        return 0;
    },
    getPhone: function (UNPHONE) {
        UNPHONE = UNPHONE.toString();
        if (UNPHONE != "" && UNPHONE.length > 8) {
            return UNPHONE.substr(0, 3) + "****" + UNPHONE.substr(UNPHONE.length - 4, 4);
        } else {
            return "";
        }
    },
    //精确两位小数的计算a=value,b=value ,c= + || - || * || /
    calculate: function (a, b, c) {
        var _v = 0;
        switch (c) {
            case "+":
                _v = (this.getFloat(a) * 100 + this.getFloat(b) * 100) / 100;
                break;
            case "-":
                _v = (this.getFloat(a) * 100 - this.getFloat(b) * 100) / 100;
                break;
            case "*":
                _v = (this.getFloat(a) * this.getFloat(b) * 100) / 100;
                break;
            case "/":
                b = this.getFloat(b);
                if (b == 0) { return 0; }
                _v = (this.getFloat(a) / this.getFloat(b) * 100) / 100;
                break;
            default: _v = 0;
        }

        return this.getFloat(_v);
    },
    //数据操作模式
    IsHandleElse: true,
    errorMsg: "连接服务器失败，请刷新后再试!"
};

jQuery.newAjax = function (options, noneedcheck) {
    var dataType = "text";
    if (options.dataType) {
        dataType = options.dataType;
    }
    var isPostType = false;
    if (options.type) {
        if (options.type.toUpperCase() == "POST") {
            isPostType = true;
        }
    }
    var orisucess;
    if (options.success) {
        orisucess = options.success;
    }
    options.success = function (result) {
        var isLogin = false, isCheck = false;
        if (dataType == "text") {
            if (result != "{Islogin:false}") {
                isLogin = true;
            }
            if (result != "{isCheck:false}") {
                isCheck = true;
            }
        } else {
            if (result.Islogin === undefined) {
                isLogin = true;
            }
            if (result.isCheck === undefined) {
                isCheck = true;
            }
        }
        if (isLogin === false) {
            alert('对不起你未登录，请登录！');
            top.window.location.href = "/?returnurl=" + encodeURIComponent(window.location.href);
            return false;
        } else if (isCheck === false) {
            alert("对不起，您还未通过审核，不能进行操作！");
            return false;
        } else {
            if (orisucess) {
                orisucess(result);
            }
        }
    };
    jQuery.ajax(options);
};
$(function(){
	$(window).resize(function(){$(".tablelist-box").height($("#liststyle").height()+20)});
});


/****
选择弹窗    
newToobar.init({ className: ".select", hideName: "hide", showName: "text", iframeUrl: "test.aspx" })
****/
var newToobar = {
    init: function (options) {
        if (options.box == undefined) {
            tableToolbar._showMsg("指定BOX");
            return;
        }
        var _self = this;
        $(options.box).find("a[class='" + options.className + "']").each(function () {
            var _f = new _self.funt();
            $.extend(_f.options, options);
            _f.init(this);
        });
    },
    funt: function () {
        this.options = {
            box: null,
            className: "xuanyong",  //选用按钮className
            hideName: null,           //隐藏域name
            showName: null,           //文本框name
            iframeUrl: null,          //跳转地址,为空则取a标签的href值
            title: null,              //弹窗标题
            width: null,           //弹窗宽度
            height: null,          //弹窗高度
            para: null,               //参数{a:'',b:''}
            callFastFun: null,       //弹窗前执行的的函数
            callBackFun: null       //回调函数返回参数obj:{value:"",text:""}
        };
        this.click = function (args) {
            var win = top || window;
            win.Boxy.iframeDialog({
                iframeUrl: this.options.iframeUrl,
                title: this.options.title,
                modal: true,
                width: this.options.width,
                height: this.options.height
            });
        };
        this.init = function (args) {
            var _s = this, p;
            this.options.callBackFun = this.options.callBackFun || "newToobar.backFun";
            this.options.title = this.options.title || $(args).attr("title");
            this.options.width = this.options.width || $(args).attr("data-width");
            this.options.height = this.options.height || $(args).attr("data-height");
            if ($(args).attr("id").length == 0) { $(args).attr("id", "id" + parseInt(Math.random() * 10000)); }
            p = { callBackFun: this.options.callBackFun, id: $(args).attr("id"), hide: this.options.hideName, show: this.options.showName };
            this.options.iframeUrl = this.options.iframeUrl || $(args).attr("href");
            if (this.options.iframeUrl.indexOf("?") > 0) {
                this.options.iframeUrl += "&"
            } else {
                this.options.iframeUrl += "?"
            }
            this.options.iframeUrl += $.param(p) + (this.options.para ? "&" + $.param(this.options.para) : "");
            $(args).unbind("click");
            $(args).click(function () { if (_s.options.callFastFun) { window[_s.options.callFastFun](this); } _s.click(this); return false; });
        }
    },
    backFun: function (args) {
        var hide, show;
        hide = $("#" + args.id).parent().find("input[type='hidden']");
        show = $("#" + args.id).parent().find("input[type='text']");
        if (hide.length > 0) { hide.val($.trim(args.value)); }
        if (show.length > 0) { show.val($.trim(args.text)); }
    }
};

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
            url: "/ashx/GetProvinceAndCity.ashx?companyId=" + options.comid + "&isCy=" + options.t + "&lng=" + options.lng + "&" + $.param(_data),
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
//供应商类别选择
var SupplierType = {
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
    //发起请求 t:请求类型 g一级类别 s二级类别 p 供应商 pv:上一级 fn:回调函数
    _post: function (t, options, pv, fn) {
        var _self = this; var _$obj = null; var _data = {}; var _sv = "";

        switch (t) {
            case "g": _$obj = this._getObj(options.gid); _data = { "get": "g" }; _sv = options.gv; break;
            case "s": _$obj = this._getObj(options.sid); _data = { "get": "p", "gid": pv }; _sv = options.sv; break;
            default: break;
        }

        if (!_$obj) { if (fn) fn(); return; }

        _$obj.html("").html(_self._loadHTML);

        $.ajax({
            type: "get", cache: false, dataType: "json",
            url: "/Ashx/GetCourtList.ashx?" + $.param(_data),
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
    //设置二级类别
    _setS: function (options) {
        var _gv = "1";
        var _$g = this._getObj(options.gid);
        if (_$g) _gv = _$g.val();
        var _self = this;
        this._post("s", options, _gv, null);
    },
    //设置一级类别
    _setG: function (options) {
        var _self = this;
        this._post("g", options, "", function () { _self._setS(options); });
    },
    //options={gid:"#一级类别下拉菜单",sid:"#二级类别下拉菜单",cid:"#供应商下拉菜单",gv:"一级类别选中的值",sv:"二级类别选中的值",cv:"供应商选中的值"}
    init: function (options) {
        var _options = { gid: "", sid: ""};
        options = $.extend(_options, options);
        var _$g = this._getObj(options.gid);
        var _$s = this._getObj(options.sid);
        var _self = this;
        if (_$g) { _$g.bind("change", function () { _self._setS(options); }); }
        this._setG(options);
    }
};
var stToobar = {
    ginit: function (options) {
        this.init(options);
    },
    init: function (options) {
        var _options = { gid: options.gID, sid: options.pID, gv: options.gSelect, sv: options.pSelect }
        SupplierType.init(_options);
    }
};
var keSimple = ['source', '|', 'cut', 'copy', 'paste', 'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
        'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'clearhtml', 'selectall', '|', 'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', '|', 'table', 'hr', 'emoticons', 'link', 'unlink'];

var keSimple_HaveImage = ['source', '|', 'cut', 'copy', 'paste', 'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
        'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'clearhtml', 'selectall', '|', 'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', '|', 'table', 'hr', 'emoticons', 'link', 'unlink', '|', 'image'];
var keMore = ['source', '|', 'cut', 'copy', 'paste', 'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
        'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'clearhtml', 'quickformat', 'selectall', '|', 'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', '|', 'table', 'hr', 'emoticons', 'link', 'unlink'];

var keMore_HaveImage = ['source', '|', 'undo', 'redo', '|', 'preview', 'print', 'template', 'code', 'cut', 'copy', 'paste',
        'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
        'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
        'superscript', 'clearhtml', 'quickformat', 'selectall', '|', 'fullscreen', '/',
        'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
        'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|', 'image', 'multiimage',
        'flash', 'media', 'insertfile', 'table', 'hr', 'emoticons', 'baidumap', 'pagebreak',
        'anchor', 'link', 'unlink', '|', 'about'];

var KEditer = {
    //创建编辑器
    init: function (id, options) {
        var e = { id: id, edit: null }, op = options || { resizeMode: 0, items: keSimple, height: "150px", width: "800px" };
        e.edit = KindEditor.create('#' + id, op);
        KEditer.list.push(e);
    },
    //缓存编辑器对象
    list: [],
    //同步编辑器的值
    sync: function (id) {
        var i = 0, _self = this, e;
        if (_self.list.length > 0) {
            for (; i < _self.list.length; i++) {
                e = _self.list[i];
                if (id) {
                    if (e.id == id) {
                        e.edit.sync();
                        break;
                    }
                } else {
                    if (document.getElementById(e.id) != null) {
                        //同步数据
                        e.edit.sync();
                    } else {
                        //移除
                        _self.list.splice(i, 1);
                    }
                }
            }
        }
    },
    //移除指定编辑器
    remove: function (id) {
        var i = 0, _self = this, e;
        if (_self.list.length > 0) {
            for (; i < _self.list.length; i++) {
                e = _self.list[i];
                if (id == e.id) {
                    e.edit.remove();
                    _self.list.splice(i, 1);
                    break;
                }
            }
        }
    },
    html: function (id, val) {
        var i = 0, _self = this, e;
        if (_self.list.length > 0) {
            for (; i < _self.list.length; i++) {
                e = _self.list[i];
                if (id == e.id) {
                    e.edit.html(val);
                    break;
                }
            }
        }
    },
    isInit: function (id) {
        var _isInit = false;
        for (var i = 0; i < this.list.length; i++) {
            if (this.list[i].id == id) { _isInit = true; break; }
        }
        return _isInit;
    }
};