var PageSubmitForm = function() {
    this.Validate = function(frm) {
        if (ValiDatorForm && ValiDatorForm.validator) {
            if (frm) {
                return ValiDatorForm.validator($(frm)[0], "alert");
            }
            else {
                return ValiDatorForm.validator($('form')[0], "alert");
            }
        }
        return true;
    };

    this.SetButtonUnableClick = function(btn, text, clickEvents) {
        if (!btn) {
            throw 'btn不能为空';
        }
        if ($(btn).length > 1) { throw '不支持多个'; }

        if ($(btn).attr('disabled') == true) {
            return;
        }

        if (!clickEvents) {
            clickEvents = [];
        }
        if (clickEvents.constructor == Function) {
            clickEvents = [clickEvents];
        }
        else if (clickEvents.constructor !== Array) {
            throw '事件参数类型错误';
        }
        if (typeof text !== 'string') {
            try {
                for (var csskey in text) {
                }
                $(btn)[0].tempcssinfo = text;
                text = '处理中';
            }
            catch (e) {
                throw '第二个参数类型错误';
            }
        }
        else {
            if (text === undefined) {
                text = '处理中';
            }
            $(btn)[0].tempcssinfo = $(btn).css('color');
        }

        $(btn).css({ "color": "#999999" });
        var isA = $(btn).attr('tagName').toLocaleLowerCase() == 'a';
        var isButton = $(btn).attr('tagName').toLocaleLowerCase() == 'input' && ($(btn).attr('type').toLocaleLowerCase() == 'button' || $(btn).attr('type').toLocaleLowerCase() == 'submit');

        if (isA) {
            $(btn).attr('temphrefinfo', $(btn).attr('href'));
            $(btn).attr('href', 'javascript:;');
            $(btn).attr('temphtmlinfo', $(btn).html());
            $(btn).html(text);
        }
        else if (isButton) {
            $(btn).attr('disabled', true);
            $(btn).attr('tempvalueinfo', $(btn).val());
            $(btn).val(text);
        }
        else {
            throw '对不支持的元素进行的操作无效';
        }
        $(btn)[0].tempListenEventsinfo = clickEvents;
        if ($(btn)[0].onclick) {
            $(btn)[0].tempeventsinfo = $(btn)[0].onclick;
        }
        $(btn).unbind('click');
        $(btn)[0].onclick = null;
        $(btn)[0].cusiter = self.setInterval(function() {
            if (text[text.length - 1] != '.' || text[text.length - 3] != '.') {
                text += '.';
            }
            else {
                if (text[text.length - 3] == '.') {
                    text = text.replace(/\./g, '');
                }
            }
            if (isA) {
                $(btn).html(text);
            }
            if (isButton) {
                $(btn).val(text);
            }
        }, 700);
    };
    this.SetButtonEnableClick = function(btn) {
        if (!btn) {
            throw 'btn不能为空';
        }
        if ($(btn).length > 1) { throw '不支持多个'; }
        var isA = $(btn).attr('tagName').toLocaleLowerCase() == 'a';
        var isButton = $(btn).attr('tagName').toLocaleLowerCase() == 'input' && ($(btn).attr('type').toLocaleLowerCase() == 'button' || $(btn).attr('type').toLocaleLowerCase() == 'submit');
        if (isA) {
            $(btn).attr('href', $(btn).attr('temphrefinfo'));
            $(btn).html($(btn).attr('temphtmlinfo'));
            $(btn).removeAttr('temphrefinfo');
            $(btn).removeAttr('temphtmlinfo');
        }
        else if (isButton) {
            $(btn).attr('disabled', false);
            $(btn).val($(btn).attr('tempvalueinfo'));
            $(btn).removeAttr('tempvalueinfo');
        }
        else {
            throw '对不支持的元素进行的操作无效';
        }

        var csstext = $(btn)[0].tempcssinfo;
        try {
            if (typeof csstext !== 'string') {
                try {
                    for (var csskey in csstext) {
                        $(btn).css(csskey, csstext[csskey]);
                    }
                }
                catch (e) {
                }
            }
            else {
                throw '';
            }
        }
        catch (e2) {
            $(btn).css("color", $(btn)[0].tempcssinfo);

        }
        $(btn)[0].tempcssinfo = null;
        if ($(btn)[0].tempeventsinfo) {
            $(btn)[0].onclick = $(btn)[0].tempeventsinfo;
        }
        for (var i = 0; i < $(btn)[0].tempListenEventsinfo.length; i++) {
            $(btn).bind('click', $(btn)[0].tempListenEventsinfo[i]);
        }

        $(btn).removeAttr('tempeventsinfo');
        $(btn).removeAttr('tempListenEventsinfo');
        if ($(btn)[0].cusiter) {
            self.clearInterval($(btn)[0].cusiter);
        }
    };

    this.triggerEvent = function(obj, eventName, deEve) {
        eventName = eventName || 'click';
        obj = $(obj)[0];
        if (obj && obj.nodeType === 1) {
            if (document.createEventObject) {
                eventName = 'on' + eventName;
                if (deEve) {
                    var eve = document.createEventObject();
                    eve.type = eventName;
                    obj.fireEvent(eve.type, eve);
                }
                else {
                    obj.fireEvent(eventName);
                }
            }
            else if (document.createEvent) {
                var eve = document.createEvent('MouseEvents');
                eve.initEvent(eventName);
                obj.dispatchEvent(eve);
            }
            else {
                throw '不支持此操作';
            }
        }
    };
};

