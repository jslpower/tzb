<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TZGathering.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.TZGathering" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8"/>

<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />

<title><%=Aptitle%></title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/juhui.css" type="text/css" media="screen"/><link rel="stylesheet" href="/wx/css/IScrol.css" type="text/css" />
<script src="../../Js/jq.mobi.min.js" type="text/javascript"></script>
<script src="../../Js/IScroll/IScroll4.2.5.js" type="text/javascript"></script>
<script type="text/javascript">
    var myScroll, pullDownEl, pullDownOffset,
	pullUpEl, pullUpOffset,
	generatedCount = 0;

    function pullDownAction() {
        setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
            var el, li, i;
            el = document.getElementById('fiullist');

            for (i = 0; i < 8; i++) {
                $(el).append("<li>Generated row " + (++generatedCount) + "</li>")
            }

            myScroll.refresh(); 	// Remember to refresh when contents are loaded (ie: on ajax completion)
        }, 1000); // <-- Simulate network congestion, remove setTimeout from production!
    }

    function pullUpAction() {
        setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
            var el, li, i;
            el = document.getElementById('fiullist');
            /*==============================================================*/
            var index = parseInt($("#pageindex").val()) + 1;
            var pageEnd = $("#hidPageend").val();
            var title = $("#<%=txtGoodsName.ClientID %>").val();
            var types = '<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("types"), 1) %>';
            var CityId = '<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("CityId"), 0) %>';
            if (pageEnd == "0") {
                $("#pageindex").val(index);
                if (title == "")
                    title = "";
                var para = { title: title, Page: index, types: types, CityId: CityId };
                $.ajax({
                    type: "Get",
                    url: "/CommonPage/AjaxActivityList.aspx?" + $.param(para),
                    cache: false,
                    success: function (result) {
                        if (result != "") {
                            $(el).append(result);
                        } else {
                            $("#pullUp").hide();
                            $("#hidPageend").val("1");
                        }
                    },
                    error: function () {
                        alert("异常请重新提交！");
                    }
                });
                myScroll.refresh(); 	// Remember to refresh when contents are loaded (ie: on ajax completion)
            }
        }, 1000);             // <-- Simulate network congestion, remove setTimeout from production
    }

    function loaded() {

        pullUpEl = document.getElementById('pullUp');
        if (pullUpEl == null) return;
        pullUpOffset = pullUpEl.offsetHeight

        myScroll = new iScroll('wrapper', {
            mouseWheel: true,
            click: true,
            checkDOMChanges: true,
            onRefresh: function () {
                if (pullUpEl.className.match('loading')) {
                    pullUpEl.className = '';
                    pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
                }
            },
            onScrollMove: function () {
                if (this.y < (this.maxScrollY - 5) && !pullUpEl.className.match('flip')) {
                    pullUpEl.className = 'flip';
                    pullUpEl.querySelector('.pullUpLabel').innerHTML = '放开刷新..';
                    this.maxScrollY = this.maxScrollY;
                } else if (this.y > (this.maxScrollY + 5) && pullUpEl.className.match('flip')) {
                    pullUpEl.className = '';
                    pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
                    this.maxScrollY = pullUpOffset;
                }
            },
            onScrollEnd: function () {
                if (pullUpEl.className.match('flip')) {
                    pullUpEl.className = 'loading';
                    pullUpEl.querySelector('.pullUpLabel').innerHTML = '正在加载...';
                    pullUpAction(); // Execute custom function (ajax call?)
                }
            }
        });
    }

    document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);
    document.addEventListener('DOMContentLoaded', loaded, false);
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <input id="pageindex" type="hidden" value="<%=CurrencyPage %>" />
    <input id="hidPageend" type="hidden" value="0" />
    <uc1:UserHome ID="UserHome1" runat="server" />
<div class="search_head juhui_search">
   <div class="search_form">
    <span class="city_name" onClick="location.href='/WX/city.aspx?action=Member&fhdz=TZGathering&types=<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("types"), 1) %>'"><asp:Literal runat="server" ID="litcityname"></asp:Literal></span>
       <asp:TextBox ID="txtGoodsName" runat="server"  CssClass="input_txt floatL" Text=""
                placeholder="搜索"></asp:TextBox>
            <asp:Button ID="btnSerch" runat="server" CssClass="input_btn icon_search_i floatR"
                Text="" OnClick="btnSerch_Click" />
   </div>
</div>

<div class="warp s_warp" id="wrapper" style="margin-top:110px;">

   <div class="juhui_list">
      <ul id="fiullist">
        <asp:Repeater ID="rptList" runat="server">
          <ItemTemplate>
         <li>
            <div class="juhui-title"><%# string.IsNullOrEmpty(Eval("Starname").ToString())?"":"<em>["+Eval("Starname")+"]</em>"%><%#Eval("Title")%></div>
            <div class="juhui-box">
                <div class="btn">
                   <a href="TZGatheringDetail.aspx?ActId=<%#Eval("Id")%>&types=<%#Eval("Activitytypes")%>" class="basic_btn">查看</a>
                   <a href="javascript:void(0);" data-ActId="<%#Eval("Id")%>" class="basic_ybtn butbaom">报名</a>
                </div>
                <p class="font_gray">时间：<%#Enow.TZB.Utility.Utils.GetDateTime((Eval("StartDatetime")).ToString()).ToString("yyyy-MM-dd")%></p>
                <%#Enow.TZB.Utility.Utils.GetInt(Eval("Activitytypes").ToString()) <= 2 ? "<p>球场:" + Eval("SiteName") + "</p>" : ""%>
                <p>地址：<%#Eval("Countryname")%><%#Eval("Provincename")%><%#Eval("Cityname")%><%#Enow.TZB.Utility.Utils.GetInt(Eval("Activitytypes").ToString()) <= 2 ? "" : Eval("Areaname")%><%#Eval("Address")%></p>
                <p class="font_gray">费用：<%#Eval("CostNum")%></p>
            </div>
         </li>
         </ItemTemplate>
         </asp:Repeater>
        


      </ul>
           <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                        <div id="pullUp">
                            <span class="pullUpIcon"></span><span class="pullUpLabel">上拉更新...</span>
                        </div>
                    </asp:PlaceHolder>
   </div>
   

</div>

    </form>
        <script type="text/javascript">
            var PageJsDataObj = {
                AcpID: '<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("ActId"), 0) %>',
                GoAjax: function (url) {
                    $.ajax({
                        type: "get",
                        url: url,
                        dataType: "json",
                        success: function (result) {
                            if (result.result == "1") {
                                alert(result.msg);
                            }
                            else {
                                alert(result.msg);
                            }
                        },
                        error: function (data) {
                            //ajax异常--你懂得
                            alert("报名失败！");
                        }
                    });
                },
                Add: function (obj) {
                    var atid = $(obj).attr("data-ActId");
                    if (atid) {
                        var dataurl = "TZGatheringDetail.aspx?ation=inter&ActId=" + atid;
                        this.GoAjax(dataurl);
                    }
                    else {
                        alert("请选择要报名的活动!");
                    }
                },
                BindBtn: function () {
                    //添加
                    $(".butbaom").each(function () {
                        var obj = this;
                        $(obj).click(function () {
                            if (window.confirm("确定要报名吗？")) {
                                PageJsDataObj.Add(obj);
                            }
                        });


                    })
                },
                PageInit: function () {
                    //绑定功能按钮
                    this.BindBtn();
                }
            }
            $(function () {
                PageJsDataObj.PageInit();
                return false;
            })
        </script>
</body>
</html>
