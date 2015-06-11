<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.Web.WX.AboutWar.Default" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>约战列表</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/juhui.css" type="text/css" media="screen"/>
<script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="/Js/CitySelect.js"></script>
<link rel="stylesheet" href="/wx/css/IScrol.css" type="text/css" />
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
                var CityId = '<%=Enow.TZB.Utility.Utils.GetQueryStringValue("CityId") %>';
                if (pageEnd == "0") {
                    $("#pageindex").val(index);
                    if (title == "")
                        title = "";
                    var para = { title: title, Page: index, CityId: CityId };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/Ajaxaboutwarlist.aspx?" + $.param(para),
                        cache: false,
                        success: function (result) {
                            if (result != "") {
                                $(el).append(result);
                                PageJsDataObj.PageInit();
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
     <uc1:UserHome ID="UserHome1" Userhometitle="约战列表" runat="server" />
       <div class="search_head juhui_search">
   <div class="search_form">
       <span class="city_name" onClick="location.href='/WX/city.aspx?action=AboutWar&fhdz=Default'"><asp:Literal runat="server" ID="litcityname"></asp:Literal></span><asp:TextBox ID="txtGoodsName" runat="server" CssClass="input_txt floatL" Text=""
                placeholder="搜索"></asp:TextBox>
       <asp:Button ID="btnSerch" runat="server" CssClass="input_btn icon_search_i floatR"
                Text="" OnClick="btnSerch_Click" />

   </div>
</div>

<div class="warp s_warp">

  <div class="msg_tab"  id="n4Tab">
        <div class="TabContent">
        
			<div id="n4Tab_Content0">
            
                   <div class="juhui_list u_yuezhan mt10" id="wrapper" style=" margin-top:110px;">


                      <ul id="fiullist">
                        <asp:Repeater ID="rptList" runat="server" >
                        <ItemTemplate>
                         <li>
                            <div class="juhui-title"><span class="green">发起了约战</span><%#Eval("title")%></div>
                            <div class="juhui-box">
                                <div class="btn btn01">
                                   <a href="AboutWarView.aspx?ID=<%#Eval("Aid") %>" class="basic_btn">查看</a>
                                   <a href="javascript:void(0);" data-aid="<%#Eval("Aid") %>" class="basic_ybtn">挑战</a>
                                </div>
                                <p>主队：<%#Eval("TeamName")%></p>
                                <p class="font_gray">时间：<%#Eval("AboutTime", "{0:yyyy-MM-dd}")%></p>
                                <p>地点：<%#Eval("Address")%></p>
                                <p class="font_gray"><span class="paddR10">赛制：<%#Eval("Format").ToString()!="100"?Eval("Format")+"人":"其他"%></span> 费用：<%#(Enow.TZB.Model.EnumType.GathersEnum.比赛费用)Convert.ToInt32(Eval("Costnum"))%></p>
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
                   
                   <div class="foot_fixed">
                       <div class="foot_box">
                          <div class="paddL10 paddR10"><a href="AboutWarAdd.aspx" class="basic_btn">发布约战</a></div>
                       </div>
                   </div>
                   
            
            </div>
            
        </div>
   
  </div>
 

</div>

    </form>
   <script type="text/javascript">
       var PageJsDataObj = {
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
                       alert("挑战报名失败！");
                   }
               });
           },
           Add: function (obj) {
               var aid = $(obj).attr("data-aid");
               if (aid!="") {
                   var dataurl = "AboutWarView.aspx?ation=inter&AID=" + aid;
                   this.GoAjax(dataurl);
               }
               else {
                   alert("数据错误请刷新页面！");
               }
              
           },
           BindBtn: function () {
               //添加
               $(".basic_ybtn").each(function () {
                   var obj = this;
                   $(obj).click(function () {
                       if (window.confirm("确定要挑战吗？")) {
                           PageJsDataObj.Add(obj);
                       }

                   })
               });

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
