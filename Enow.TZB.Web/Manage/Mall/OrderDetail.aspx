<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" CodeBehind="OrderDetail.aspx.cs" Inherits="Enow.TZB.Web.Manage.Mall.OrderDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/js/moveScroll.js"></script>
    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="contentbox">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    订单金额：
                    <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                    &nbsp;&nbsp;
                 联系电话：<asp:Literal ID="litMobile" runat="server"></asp:Literal>
                   &nbsp;&nbsp;物流编号：<asp:Literal ID="litaddressNo" runat="server"></asp:Literal>
                   &nbsp;&nbsp;收货地址：<asp:Literal ID="litAddress" runat="server"></asp:Literal>
              
                </p>
            </span>
        </div>
        <div class="listbox">
           
            <!--列表表格-->
            <div class="tablelist-box">
                <table width="100%" id="liststyle">
                    <tr>
                        <th align="center">
                        编号
                        </th>
                        <th align="center">
                           商品名称
                        </th>  
                        <th align="center">
                            件数
                        </th> 
                        <th align="center">
                            商品费
                        </th>
                        <th align="center">
                           运费
                        </th>
                        <th align="center">
                           购买时间
                        </th>
                        <th align="center">
                            支付方式
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                    <%#Eval("GoodsName")%></a>
                                </td>
                                <td align="center">
                                    <%#Eval("ShoppingNum")%>
                                </td>
                               
                                <td align="center">
                                    <%#Eval("GoodsFee")%>
                                </td>
                              
                                <td align="center">
                                    <%#Eval("FreightFee")%>
                                </td>
                                <td align="center">
                                     <%#Eval("JoinTime", "{0:yyyy-MM-dd}")%>
                                </td>
                                <td align="center">
                                    <%#(Enow.TZB.Model.商城支付方式)Convert.ToInt32(Eval("PayType"))%>
                                </td>
                                
                             
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="phNoData" runat="server" Visible="false">
                        <tr>
                            <td colspan="8" align="center">
                                暂无数据
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
            </div>
            <!--列表结束-->
            <div class="tablehead botborder">
                <script type="text/javascript">
                    document.write(document.getElementById("tablehead").innerHTML);
                </script>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var PageJsDataObj = {
            Query: {/*URL参数对象*/
                sl: ''
            },
            //浏览弹窗关闭后刷新当前浏览数
            BindClose: function () {
                $("a[data-class='a_close']").unbind().click(function () {
                    window.location.reload();
                    return false;
                })
            },
            DataBoxy: function () {/*弹窗默认参数*/
                return {
                    url: '/Manage/Mall',
                    title: "",
                    width: "520px",
                    height: "300px"
                }
            },
            ShowBoxy: function (data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            GoAjax: function (url) {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function (result) {
                        if (result.result == "1") {
                            tableToolbar._showMsg(result.msg, function () {
                                window.location.reload();
                            });
                        }
                        else { tableToolbar._showMsg(result.msg); }
                    },
                    error: function () {
                        //ajax异常--你懂得
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
          
            BindBtn: function () {
               
//                //启用
//                $(".toolbar_enable2").click(function () {
//                    var rows = tableToolbar.getSelectedCols();
//                    PageJsDataObj.Enable2(rows);
//                    return true;
//                });
               
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "订单管理"
                });
            },
            PageInit: function () {
                //绑定功能按钮
                this.BindBtn();
                //当列表页面出现横向滚动条时使用以下方法 $("需要滚动最外层选择器").moveScroll();
                $('.tablelist-box').moveScroll();
            },
            CallBackFun: function (data) {
                newToobar.backFun(data);
            }

        }
        $(function () {

            PageJsDataObj.PageInit();
            PageJsDataObj.BindClose();
            return false;
        })
    </script>
</asp:Content>

