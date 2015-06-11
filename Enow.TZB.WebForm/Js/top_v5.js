
$.CNCN = {
	hover_tag : function(control, show, class1, i) {
		$(control + "> a").hover(function() {
			var c = $(control + "> a").index($(this));
			if (c == i || $(this).hasClass("more")) {
				return false;
			}
			$(this).addClass(class1).siblings().removeClass(class1);
			$(show + "> div").eq(c).show().siblings().hide();
		})
	},
	hover_show : function(control, hideBox){
		$(control).hover(function(){ 
			$(this).find(hideBox).show();
		},function(){ 
			$(this).find(hideBox).hide();
		}) 
	},
	hover_class : function(control, class1) {
		$(control).hover(function() {
			$(this).addClass(class1)
		},function(){
			$(this).removeClass(class1)
		})
	},
	hover_class_show : function(control, hoverBox, class1, hideBox){ 
		$(control).hover(function(){ 
			$(this).find(hoverBox).addClass(class1);
			$(this).find(hideBox).show();
		},function(){ 
			$(this).find(hoverBox).removelass(class1);
			$(this).find(hideBox).hide();
		})
	},
	hover_load : function(control, hover, class1) {
		$(control).on("mouseover",hover,function(){
			$(this).addClass(class1);
		})
		$(control).on("mouseout",hover,function(){
			$(this).removeClass(class1);
		})
	}
}

if($("#top_menu")) {
	$.CNCN.hover_class(".quick_menu li", "hov");
}

