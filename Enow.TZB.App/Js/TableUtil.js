/*
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//自动添加行，使用方法:jquery容器.autoAdd({参数}); by 田想兵 2011.9.7
//options{tempRowClass:"模版行样式", addButtonClass:"添加按钮样式",delButtonClass:"删除按钮样式"}
//调用示例$(".content").autoAdd();
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
*/
(function ($) {
    $.fn.autoAdd = function (options) {
        var settings = { changeInput: $("#input"), tempRowClass: "tempRow", min: 1, max: 90, addButtonClass: "addbtn", delButtonClass: "delbtn", addCallBack: null, delCallBack: null, indexClass: "index", insertClass: "insertbtn", moveUpClass: "moveupbtn", moveDownClass: "movedownbtn", upCallBack: null, downCallBack: null };
        if (options) $.extend(settings, options);
        var content = this;
        var count = content.find("." + settings.tempRowClass).length;
        settings.changeInput.change(function () {
            var ival = tableToolbar.getInt(this.value);
            if (ival > 0) {
                if (ival > settings.max || ival < settings.min) {
                    tableToolbar._showMsg("请输入大于等于 " + settings.min + " 并且小于等于 " + settings.max + " 的数!");
                    if (ival > settings.max)
                        this.value = settings.max;
                    if (ival < settings.min)
                        this.value = settings.min;
                    $(this).change();
                } else {
                    addRow($(this).val());
                }
            } else {
                tableToolbar._showMsg("请输入有效的整数！");
                $(this).val("");
                $(this).focus();
            }
        });
        content.find("." + settings.tempRowClass).each(function () {
            $(this).find(".richText,input").each(function () {
                if ($(this).attr("id").length == 0)
                    $(this).attr("id", "txt_" + Math.round(Math.random() * new Date().getTime()))

            });
        });
        function addRow(num, isInsert, target) {
            //console.log(temp);
            //var strTemp = "";
            for (var i = 0; i < num - count; i++) {
                var temp = content.find("." + settings.tempRowClass).first().clone(true);
                temp.find("input:not(:checkbox,:radio)").val("");
                temp.find("select").val("-1");
                temp.find("textarea").val("");
                temp.find(":checkbox").attr("checked", false);
                temp.attr("id", "");
                temp.find("input,tr,textarea,select,:checkbox,tbody").attr("id", "").removeAttr("disabled");
                if (temp.find(".ke-container").length > 0) {
                    temp.find(".ke-container").remove();
                }
                temp.find(".richText,input").each(function () {
                    $(this).attr("id", "txt_" + Math.round(Math.random() * new Date().getTime()))
                    $(this).show();
                });
                if (isInsert) {
                    temp.insertBefore(target.closest("." + settings.tempRowClass));
                } else {
                    content.append(temp);
                }
                if (settings.addCallBack)
                    settings.addCallBack(temp);
            }
            if (num < count) {
                for (var j = count - 1; j >= num; j--) {
                    if (content.children().eq(j + 1).length > 0) {
                        delRow(content.children().eq(j + 1));
                    }
                }
            } else {
                //content.append($(strTemp));

            }
            count = content.find("." + settings.tempRowClass).length;
            settings.changeInput.val(count);
            sumIndex();
            showhideBtn();
            //console.log(strTemp);
        };
        function showhideBtn() {
            content.find("." + settings.tempRowClass).find("." + settings.moveUpClass + ",." + settings.moveDownClass).show();
            content.find("." + settings.tempRowClass).first().find("." + settings.moveUpClass).hide();
            content.find("." + settings.tempRowClass).last().find("." + settings.moveDownClass).hide();
        }
        content.find("." + settings.addButtonClass).bind("click", function () {
            //console.log(count);
            if (count >= settings.max) {
                tableToolbar._showMsg("最多只能添加 " + settings.max + " 行记录!");
            } else {
                addRow(count + 1);
            }
            return false;
        });
        content.find("." + settings.insertClass).bind("click", function () {
            if (count >= settings.max) {
                tableToolbar._showMsg("最多只能添加 " + settings.max + " 行记录!");
            } else {
                addRow(count + 1, true, $(this));
            }
            return false;
        });
        content.find("." + settings.moveUpClass).bind("click", function () {
            var tr = $(this).closest("." + settings.tempRowClass);
            tr.insertBefore($(this).closest("." + settings.tempRowClass).prev("." + settings.tempRowClass));
            showhideBtn();
            sumIndex();
            if (settings.upCallBack) { settings.upCallBack(tr); }
            return false;
        });
        content.find("." + settings.moveDownClass).bind("click", function () {
            var tr = $(this).closest("." + settings.tempRowClass);
            tr.insertAfter($(this).closest("." + settings.tempRowClass).next("." + settings.tempRowClass));
            showhideBtn();
            sumIndex();
            if (settings.downCallBack) { settings.downCallBack(tr) }
            return false;
        });
        content.find("." + settings.delButtonClass).bind("click", function () {
            //console.log(count);
            count = content.find("." + settings.tempRowClass).length; ;
            if (count > settings.min) {
                count--;
                delRow($(this).closest("." + settings.tempRowClass));
            } else {
                tableToolbar._showMsg("需至少保留 " + settings.min + " 行记录!");
            }
            return false;
        });
        function delRow(row) {
            if (row.find("object").length > 0) {
                try {
                    row.find("object").remove();
                } catch (e) {

                }
            }
            row.remove();
            count = content.find("." + settings.tempRowClass).length;
            settings.changeInput.val(count);
            if (settings.delCallBack)
                settings.delCallBack(row);
            showhideBtn();
            sumIndex();
        }
        function sumIndex() {
            content.find("." + settings.indexClass).each(function (index, domEle) {
                $(this).html(index + 1);
            });
        }
        showhideBtn();
    }
})(jQuery);
$(function () {
    $(".autoAdd").each(function () {
        $(this).autoAdd();
    });
});
function querystring(uri, val) {
    var re = new RegExp("" + val + "\=([^\&\?]*)", "ig");
    return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
}