/* 首页-V2 样式控制JS，数据相关JS不放这个*/
$(function(){
	 $(".logo i").hover(function() {
		$(this).addClass("hover");
	},function() {
		$(".logo i").removeClass("hover");
	});
	
	 $(".city ul li.site").hover(function() {
		$(this).find("ul").css("display","block");
	},function() {
		$(this).find("ul").css("display","none");
	});
	
	 $(".company li img").hover(function() {
		$(this).addClass("hover");
	},function() {
		$(".company li img").removeClass("hover");
	});
	
	var url = window.location.href;
	var urls = url.split('/');
	var navStatus = 0;
	$('.nav .navm li a').each(function(){
		var href = $(this).attr("href");
		href = href.substring(0, href.indexOf('.'));
		if (url.indexOf("/" + href) >= 0) {
			$(this).parent().addClass("cur");
			return false;
		}
	});
	
	if (document.getElementById("webpage") != null) {
		var css = document.createElement("link"),script = document.createElement("script");            
		css.href = '/skin/manager.v1/css/simplePagination.css';
		css.type = 'text/css';
		css.rel = 'stylesheet';
		script.src = '/skin/manager.v1/plugins/simplePagination/simplePagination.js';
		script.type = 'text/javascript';           
		$("head").find("link").after(css);
		$("head").find("script").after(script);
		$.loadpage();
	}
});



$.fn.YEXfocus = function (opts) {
    var opt = $.extend({
        speed: 5000,
        direction: 'left',
        eventType: 'click',
        theme: 'number',
        isPre: false,
        isOpacity: false,
        isTotal: false,
        isAutoPlay: true
    }, opts || {});
    return this.each(function () {
        var o = $(this);
        var sWidth = o.width(); //获取焦点图的宽度（显示面积）
        var sHeight = o.height(); //获取焦点图的宽度（显示面积）
        var len = $("ul li", o).length; //获取焦点图个数
        if (len < 2) { return; }
        var lis = $("img", o);
        var index = 0;
        var picTimer;

        //添加数字按钮和按钮后的半透明条，还有上一页、下一页两个按钮
		if (o.find("ol").length <= 0) {
			var btn = "<ol class='btn'>";
			for (var i = 0; i < len; i++) {
				switch (opt.theme) {
					case 'title':
						btn += '<li>' + lis.eq(i).attr('alt') + '</li>';
						break;
					case 'thumb':
						btn += '<li><img src="' + lis.eq(i).attr('src') + '"/></li>';
						break;
					case 'number':
						btn += '<li>' + (i + 1) + '</li>';
						break;
				}
			}
			btn += "</ol>";
			o.append(btn);
		}

        //为小按钮添加鼠标滑入事件，以显示相应的内容
        $(".btn li", o).mouseenter(function () {
            index = $(".btn li", o).index(this);
            showPics(index);
        }).eq(0).trigger("mouseenter");


        //鼠标滑上焦点图时停止自动播放，滑出时开始自动播放
        if (opt.isAutoPlay) {
            o.hover(function () {
                clearInterval(picTimer);
            }, function () {
                picTimer = setInterval(function () {
                    showPics(index);
                    index ++;
                    if (index == len) { index = 0; }
                }, opt.speed);
            }).trigger("mouseleave");
        }
        //显示图片函数，根据接收的index值显示相应的内容
        function showPics(index) { //普通切换
			if (opt.direction == 'top') {
				var nowTop = -index * sHeight;
				//$("ul", o).stop(true, false).animate({ "top": nowTop }, 500);
				$("ul", o).css('opacity', 0).css('top', nowTop).animate({opacity:'1'},500);
			} else {
				var nowLeft = -index * sWidth; //根据index值计算ul元素的left值
				$("ul", o).stop(true, false).animate({ "left": nowLeft }, 500); //通过animate()调整ul元素滚动到计算出的position
			}
			$(".btn li", o).stop(true, false).removeClass('select').eq(index).addClass('select');
		
        }
    })
}

