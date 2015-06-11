<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserOrders.aspx.cs" Inherits="Enow.TZB.Web.WX.Order.UserOrders" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>我的订单</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/user.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jq.mobi.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/IScrol.css" />
    <script src="../../Js/IScroll/IScroll4.2.5.js" type="text/javascript"></script>
    <script type="text/javascript">
        var myScroll, pullDownEl, pullDownOffset,
	pullUpEl, pullUpOffset,
	generatedCount = 0;

        function pullDownAction() {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, i;
                el = document.getElementById('udianjilist');

                for (i = 0; i < 8; i++) {
                    $(el).append("<li>Generated row " + (++generatedCount) + "</li>")
                }

                myScroll.refresh(); 	// Remember to refresh when contents are loaded (ie: on ajax completion)
            }, 1000); // <-- Simulate network congestion, remove setTimeout from production!
        }

        function pullUpAction() {
            setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                var el, li, i;
                el = document.getElementById('udianjilist');
                /*==============================================================*/
                var index = parseInt($("#pageindex").val()) + 1;
                var pageEnd = $("#hidPageend").val();
                var types = '<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("type"), 0) %>';
                if (pageEnd == "0") {
                    $("#pageindex").val(index);
                    var para = {Page: index, types: types };
                    $.ajax({
                        type: "Get",
                        url: "/CommonPage/AjaxUserOrderList.aspx?" + $.param(para),
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
    <script type="text/javascript">
        var PageJsDataObj = {

            Query: {/*URL参数对象*/
                sl: ''
            },
            GoAjax: function (url) {
                $.ajax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function (result) {
                        if (result.result == "1") {

                            alert(result.msg);

                            window.location.href = "UserOrders.aspx";
                        }
                        else { alert(result.msg) }
                    },
                    error: function () {
                        //ajax异常--你懂得
                        //tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            DataBoxy: function () {/*弹窗默认参数*/
                return {
                    url: '/WX/Order',
                    title: "",
                    width: "830px",
                    height: "620px"
                }
            },
            DeleteOrder: function (orderid, addressid) {
                if (window.confirm("您确定要删除订单吗？")) {

                    var data = this.DataBoxy();
                    data.url += '/UserOrders.aspx?';

                    data.url += $.param({
                        sl: this.Query.sl,
                        doType: "delete",
                        orderid: orderid,
                        address: addressid

                    });
                    this.GoAjax(data.url);
                }
                

            }


        }
</script>
</head>
<body>
    <form id="form1" runat="server">
         <input id="pageindex" type="hidden" value="<%=CurrencyPage %>" />
    <input id="hidPageend" type="hidden" value="0" />
    <uc1:UserHome ID="UserHome1" Userhometitle="我的订单" runat="server" />
<div class="user_warp paddB10" >

    <div class="user-menu user-menu2 mt10" >
       <ul class="fixed">
           <li class="liclick" data-type="<%= Convert.ToInt32(Enow.TZB.Model.商城订单状态.未支付) %>">未付</li>
           <li class="liclick" data-type="<%= Convert.ToInt32(Enow.TZB.Model.商城订单状态.已支付)  %>">已付</li>
           <li class="liclick" data-type="<%= Convert.ToInt32(Enow.TZB.Model.商城订单状态.已发货)  %>">发货</li>
           <li class="liclick" data-type="<%= Convert.ToInt32(Enow.TZB.Model.商城订单状态.退款)  %>">退款</li>
       </ul>
    </div>
    
    <div class="u-dindan-list" id="wrapper" style="margin-top:150px;">
       <ul id="udianjilist">
       <asp:Repeater ID="rptList" runat="server" onitemdatabound="HandleOperation">
              <ItemTemplate>
           <li>
           <div class="user-item font14 R_jiantou"><a href="UserOrdersDetail.aspx?addr=<%#Eval("AddressId")%>&no=<%#Eval("OrderNo")%>&OrderId=<%#Eval("OrderId")%>&total=<%=total%>"><%#Eval("OrderNo")%></a></div>
                <div class="u-dindan-item">
                   <img alt="" src="<%#Getimgurl(Eval("OrderId").ToString())%>"/>
                   <p class="font_gray">支付状态:<%#((Enow.TZB.Model.商城订单状态)(Enow.TZB.Utility.Utils.GetInt(Eval("PayStatus").ToString()))).ToString()%><span style="float: right;"><%#Eval("CreatTime")!=null?Enow.TZB.Utility.Utils.GetDateTime(Eval("CreatTime").ToString(),DateTime.MinValue).ToString("yyyy-MM-dd HH:mm"):DateTime.Now.ToString("yyyy-MM-dd HH:mm")%></span></p>
                   <p><%#Getsum(Eval("OrderId").ToString())%></p>
                   <p class="font_gray">支付方式：<%#(Enow.TZB.Model.商城支付方式)Convert.ToInt32(Eval("PayType"))%>  <asp:Literal ID="litOperation" runat="server"></asp:Literal>
                   </p>
                  
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
        var licl = {
            dqtyoe: '<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("type"),-1) %>',
            liclicl: function (obj) {
                var livalue = $(obj).attr("data-type");
                document.location.href = "UserOrders.aspx?type=" + $(obj).attr("data-type");
            },
            selli:function(obj){
                var livalue = $(obj).attr("data-type");
                if (livalue == licl.dqtyoe) {
                    $(obj).css("background-color", "#29caa9");
                }
            }
        }
        $(document).ready(function () {
            $(".liclick").each(function () {
                var obj = this;
                licl.selli(obj);
                $(obj).click(function () {
                    licl.liclicl(obj);
                });
            });
        });
    </script>
</body>
</html>
