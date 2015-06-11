/*
生成 左侧菜单
*/
function LeftMenuBind(MenuId) {
    var url = "/Ashx/GetSubMenu.ashx?MenuId=" + MenuId;
    $.ajax({
        dataType: "html",
        type: "GET",
        cache: false,
        url: url,
        success: function (menuInfo) {
            $(".leftmenu").html(menuInfo);
        },
        error: function () { alert("获取二级菜单信息失败！"); }
    });
}