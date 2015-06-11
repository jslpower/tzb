<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mall_ShoppingChart.aspx.cs"
    Inherits="Enow.TZB.Web.WX.Mall.Mall_ShoppingChart" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>购物车</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/WX/css/user.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/WX/css/mall.css" type="text/css" media="screen" />
    <script src="../../Js/jq.mobi.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="购物车" runat="server" />
    <div class="search_head home_search">
        <div class="search_form">
            <asp:TextBox ID="txtGoodsName" CssClass="input_txt floatL" Text="" runat="server"
                placeholder="商品搜索"></asp:TextBox>
            <asp:Button ID="btnSearch" CssClass="input_btn icon_search_i floatR" runat="server"
                Text="" OnClick="btnSerch_Click" />
        </div>
        <input id="hidChartId" type="hidden"  runat="server"/>

    </div>
    <div class="home_warp">
        <div class="u-dindan-list">
            <ul>
        
                <asp:Repeater ID="rptList" runat="server" >
                    <ItemTemplate>
                        <li>
                            <div class="user-item">
                                <%--<span class="fxk_on">
                    </span>--%>
                                <input class="checklist" type="checkbox" style="padding-left: 22px; height: 20px;" name="ckbContent" data="<%#Eval("Id")%>" data-num="<%#Eval("ShoppingNum")%>" data-mone="<%#Eval("GoodsFee")%>" data-fre="<%#Eval("FreightFee")%>" />
                                <span class="R_jiantou" onclick="location.href='Mall_Detail.aspx?id=<%#Eval("GoodsId")%>'">
                                    <%#Eval("GoodsName")%>
                                </span>
                            </div>
                            <div class="u-dindan-item">
                               
                                <img alt="" src="<%#Eval("GoodsPhoto")%>" />
                                <p class="font_gray">
                                    <%#Eval("JoinTime","{0:yyyy-MM-dd}")%></p>
                                <p>
                                    共<%#Eval("ShoppingNum")%>件商品 实付：¥<%#Enow.TZB.Utility.Utils.GetDecimal(Eval("GoodsFee").ToString()) * Enow.TZB.Utility.Utils.GetDecimal(Eval("ShoppingNum").ToString()) + Enow.TZB.Utility.Utils.GetDecimal(Eval("FreightFee").ToString())%>
                                    (含运费<%#Enow.TZB.Utility.Utils.GetDecimal(Eval("FreightFee").ToString())%>
                                    元)</p>
                                <p class="font_gray">
                                    支付方式：<%#(Enow.TZB.Model.商城支付方式)Convert.ToInt32(Eval("PayType"))%><a href="javascript:void(0);" data-cid="<%#Eval("Id")%>" class="gray_btn">删除</a></p>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <!--mall_bot------------start-->
        <div class="mall_bot gwc_bot">
            <div class="gwc_total">
                <div class="L paddL10">
                
                    <input type="checkbox" id="fxkxz"/>全选
                </div>
                <div class="R">
                    <a href="javascript:void(0);" class="basic_ybtn">结算</a> <span style=" width:100%;"><p style=" margin-left:-100px;">合计：<em class="font_yellow">¥0</em></p><p class="p_yf" style=" margin-left:-115px;">
                        含运费：¥0</p>
                    </span>
                </div>
            </div>
            <div class="mall_menu">
                <ul class="fixed">
                    <li onclick="location.href='Mall_Type.aspx'"><span><s class="icon01"></s>商品分类</span></li>
                    <li onclick="location.href='Mall_ShoppingChart.aspx'" class="on"><span><s class="icon02">
                    </s>购物车</span></li>
                </ul>
            </div>
        </div>
        <!--mall_bot------------end-->
    </div>
    </form>
    <script type="text/javascript">
        var shopchar = {
            ckbAllClick: function () {
                /// <summary>全选按钮事件</summary>
                if ($("#fxkxz").prop("checked")) {
                    $("input[type='checkbox'][name='ckbContent']").prop("checked", true);
                } else {
                    $("input[type='checkbox'][name='ckbContent']").removeAttr("checked");
                }
                shopchar.getCkeckedCheckboxValue();
            },
            GoAjax: function (url) {
                $.ajax({
                    type: "get",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function (result) {
                        if (result.result == "1") {
                            alert(result.msg);
                            window.parent.location.reload();

                        }
                        else { alert(result.msg) }
                    },
                    error: function () {
                        //ajax异常--你懂得
                        alert("删除失败请刷新后重试！");
                    }
                });
            },
            Delete: function (obj) {
                var charid = $(obj).attr("data-cid");
                if (charid) {
                    var url = "Mall_ShoppingChart.aspx?action=delete&charid=" + charid;
                    shopchar.GoAjax(url);
                }
                else {
                    alert("编号不正确！");
                }

            },
            getCkeckedCheckboxValue: function () {
                /// <summary>获取所有选中的Checkbox的值</summary>data-num

                var datamone = 0;
                var datafre = 0;
                var strChartid = [];
                $(".u-dindan-list").find("input[type='checkbox'][name='ckbContent']:checked").each(function () {
                    datamone = datamone + parseFloat($(this).attr("data-mone")) * parseFloat($(this).attr("data-num"));
                    datafre = datafre + parseFloat($(this).attr("data-fre"));
                    strChartid.push($(this).attr("data"));
                });
                $("#hidChartId").val(strChartid.join(","));

                $(".font_yellow").html("¥" + (datamone + datafre));
                $(".p_yf").html("含运费：¥" + datafre);
            },
            GoClearing: function () {
                var hdf = $("#hidChartId").val();
                if (hdf.length > 0) {
                    if (window.confirm("您确定要结算吗？")) {
                        var tzhref = "Mall_Orders.aspx?doType=total&chartid=" + hdf;
                        window.location.href = tzhref;
                    }
                }
                else {
                    alert("请先选择商品！");
                }
            },
            initFoodInfo: function () {
                /// <summary>初始化页面 脚本执行入口</summary>
                $("#fxkxz").click(function () {
                    shopchar.ckbAllClick();
                });
                $(".basic_ybtn").click(function () {
                    shopchar.GoClearing();
                });
                $(".checklist").each(function () {
                    var obj = this;
                    $(obj).click(function () {
                        shopchar.getCkeckedCheckboxValue();
                    });

                });
                $(".gray_btn").each(function () {
                    var obj = this;
                    $(obj).click(function () {
                        shopchar.Delete(obj);
                    });

                });
            }
        }
        $(document).ready(function () {
            shopchar.initFoodInfo();
        });
    </script>
</body>
</html>
