<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mall_Detail.aspx.cs" Inherits="Enow.TZB.Web.WX.Mall.Mall_Detail" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>产品详情-<%=detitle%></title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/mall.css" type="text/css" media="screen"/>
<script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
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
                        window.parent.location.reload();
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
                url: '/WX/Mall',
                title: "",
                width: "830px",
                height: "620px"
            }
        },
        JoinChart: function (recordid) {
            if (window.confirm("您确定要加入购物车吗？")) {

                var data = this.DataBoxy();
                data.url += '/Mall_Detail.aspx?';

                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "join",
                    id: recordid,
                    num: document.getElementById("txtShoppingNum").value
                });

            }
            this.GoAjax(data.url);

        }


    }
</script>

</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="产品详情" runat="server" />
<div class="user_warp">
 
    <div class="mall_banner_xx">
    <asp:Literal ID="ltrImg" runat="server"></asp:Literal>
    </div>
    
    <div class="cont gray_lineB font14"><asp:Literal ID="ltrGoodsName" runat="server"></asp:Literal></div>

    <div class="cont gray_lineB">
            <ul class="mall_price">
                <li class="L line_x">市场价：¥<asp:Literal ID="ltrMarkerPrice" runat="server"></asp:Literal></li>
                <li>已售：<asp:Literal ID="ltrSellNum" runat="server"></asp:Literal></li>
                <li><em>会员价：</em><span class="font_yellow">¥<i class="font16"><asp:Literal ID="ltrMemberPrice" runat="server"></asp:Literal></i></span></li>
            </ul>
    </div>
    
    <div class="mall_form">
              <ul>
                       <li class="color_box">
                          <span class="label_name"><asp:Literal ID="ltrStandard" runat="server"></asp:Literal></span>
                          <div class="color_list">
                           <%--  <a href="javascript:;" class="on">红色</a>--%>
                            <asp:Literal ID="ltrStandardInfo" runat="server"></asp:Literal>
                          </div>
                       </li>
                       
                       
                       <li class="paddR10">
                          <a href="#" id="goumai" class="basic_rbtn floatR ">购买</a>
                          <asp:Literal
                              ID="Literal1" runat="server"></asp:Literal>
                          <span class="label_name">数量：</span>
                          <span class="number"><i class="num-minus"></i>
                         <%-- <input type="tel" value="03">--%>
                          <input type="text" id="txtShoppingNum" value="1" />
                          <i class="num-add"></i></span>
                       </li>
                       
              </ul>
    </div>
    
    <div class="cont gray_lineT mt10">
       <div class="padd10">
         <div class="font14 paddB10">产品详情</div>
         <div id="divStandardInfo" class="indent2 font_gray" runat="server">

         </div>
       </div>
    </div>
    
    <!--mall_bot------------start-->
    <div class="mall_bot">
            <div class="mall_menu">
                <ul class="fixed">
                    <li onclick="location.href='Mall_Type.aspx'"><span><s class="icon01"></s>商品分类</span></li>
                    <li onclick="location.href='Mall_ShoppingChart.aspx'"><span><s class="icon02"></s>购物车</span></li>
                </ul>
            </div>
        </div>
    <!--mall_bot------------end-->
    

</div>

    </form>
    <script type="text/javascript">
        var Gouwuche = {
            godid: '<%=GoodsId%>',
            Add: function (obj) {
                if (Gouwuche.godid != "") {
                    var num = parseInt($("#txtShoppingNum").val());
                    if (num > 0) {
                        window.location.href = "Mall_Orders.aspx?doType=shop&gid=" + Gouwuche.godid + "&num=" + num;
                    }

                }
                else {
                    alert("请刷新后重试!");
                }
            },
            Operation: function (obj) {
                var num = parseInt($("#txtShoppingNum").val());
                if (obj == 1) {
                    $("#txtShoppingNum").val((num + 1))
                }
                else {
                    if (num > 0) {
                        $("#txtShoppingNum").val((num - 1))
                    }
                }

            },
            InitLoad: function () {
                $(".num-add").click(function () {
                    Gouwuche.Operation(1)
                });
                $(".num-minus").click(function () {
                    Gouwuche.Operation(0)
                });
                $("#goumai").click(function () {
                    Gouwuche.Add()
                });
            }
        }
        $(document).ready(function () {
            Gouwuche.InitLoad();
        });
    </script>
</body>
</html>
