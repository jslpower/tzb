<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage/FinaWinBackPage.Master" CodeBehind="Orders.aspx.cs" Inherits="Enow.TZB.Web.Manage.Mall.Orders" %>

<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
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
                    订单状态：<select id="State" name="State">
                        <option value="-1">请选择</option>
                        <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                                                                               (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.商城订单状态),
                                                               new string[] { }), Enow.TZB.Utility.Utils.GetInt(Request.QueryString["State"],-1))
                        %></select>&nbsp;&nbsp;订单编号：
                    <asp:TextBox ID="txtOrderNo" CssClass="formsize120 input-txt" runat="server" />
                    &nbsp;&nbsp;
                 
               
                   &nbsp;&nbsp;&nbsp;&nbsp;创建时间：
                    <asp:TextBox ID="txtIBeginDate" onfocus="WdatePicker()" runat="server" CssClass="formsize80 input-txt" MaxLength="10"></asp:TextBox>-<asp:TextBox ID="txtIEndDate"  onfocus="WdatePicker()" runat="server" CssClass="formsize80 input-txt" MaxLength="10"></asp:TextBox>
               
                   
                    <input type="submit" id="btnSearch" class="search-btn" CausesValidation="False" value="搜索" />
                   
                         &nbsp;&nbsp;
                    
                    
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
      <%--           <asp:PlaceHolder ID="phpass" runat="server" Visible="true">
                <li><s class="qiyicon"></s><a href="#" hidefocus="true" class="toolbar_StateBack">审核通过</a></li>
                 <li class="line"></li>
                    </asp:PlaceHolder>
                     <asp:PlaceHolder ID="phnopass" runat="server" Visible="true">
                        <li><s class="jinyicon"></s><a href="#" hidefocus="true" class="toolbar_disabled">审核无效</a></li>
                         <li class="line"></li>
                         </asp:PlaceHolder>--%>
                         <asp:PlaceHolder ID="phnopass" runat="server" Visible="true">
                        <li><s class="jinyicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_tuikuan">退款</a></li>
                         <li class="line"></li>
                         </asp:PlaceHolder>
                          <asp:PlaceHolder ID="phsend" runat="server" Visible="true">
                        <li><s class="qiyicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_send">发货</a></li>
                   </asp:PlaceHolder>
                   
                </ul>
                <div class="pages">
                    <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </div>
            </div>
            <!--列表表格-->
            <div class="tablelist-box">
                <table width="100%" id="liststyle">
                    <tr>
                        <th width="30" class="thinputbg">
                            <input type="checkbox" name="checkbox" id="checkbox" />
                        </th>
                        <th align="center">
                            编号
                        </th>
                        <th align="center">
                            订单编号
                        </th>
                        <th align="center">
                            下单人
                        </th> 
                         <th align="center">
                            联系电话
                        </th> 
                        <th align="center">
                            订单状态
                        </th>
                        <th align="center">
                            提交时间
                        </th>
                        <th align="center">
                            审核时间
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" aress="<%#Eval("AddressId") %>" name="checkbox" id="checkbox" value="<%#Eval("OrderId") %>" data-state="<%#Eval("PayStatus") %>" />
                                </td>
                                <td align="center">
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                 <a href="OrderDetail.aspx?addr=<%#Eval("AddressId")%>&no=<%#Eval("OrderNo")%>&OrderId=<%#Eval("OrderId")%>" class="transactions"  data-rel='<%#Eval("OrderId") %>'>
                                    <%#Eval("OrderNo")%></a>
                                </td>
                                
                                <td align="center">
                                    <%#Eval("MemberName")%>
                                </td>
                                 <td align="center">
                                    <%#Eval("MobilePhone")%>
                                </td>
                                <td align="center">
                                    <%#(Enow.TZB.Model.商城订单状态)Convert.ToInt32(Eval("PayStatus"))%>
                                </td>
                                <td align="center">
                                     <%#Eval("CreatTime", "{0:yyyy-MM-dd}")%>
                                </td>
                                <td align="center">
                                     <%#Eval("ReviewTime", "{0:yyyy-MM-dd}")%>
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
            //修改订单状态
            UpdateOrderstate: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '<%=(int)Enow.TZB.Model.商城订单状态.已支付 %>') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录未支付或已发货,不能退款！");
                        return;
                    }

                }

                var data = this.DataBoxy();
                data.url += "/Orders.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "tuikuan",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            //审核通过
            CheckPass: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '<%=(int)Enow.TZB.Model.商城订单状态.待审核 %>') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录已审核通过,不能二次审核！");
                        return;
                    }

                }

                var data = this.DataBoxy();
                data.url += "/Orders.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "CheckPass",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            //审核无效
            NoPass: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '<%=(int)Enow.TZB.Model.商城订单状态.待审核 %>') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录已审核通过，不能二次审核！");
                        return;
                    }

                }

                var data = this.DataBoxy();
                data.url += "/Orders.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "NoPass",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            //填写物流信息
            Send: function (objsArr) {
                var list = new Array();
                var state = new Array();
                var d= new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                    d.push(objsArr[i].find("input[type='checkbox']").attr("aress")); 
                }
                
               
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能操作一条信息");
                    return;
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '<%=(int)Enow.TZB.Model.商城订单状态.已支付 %>') {
                        tableToolbar._showMsg("未支付或已发货，不能填写物流信息！");
                        return;
                    }
                    else {

                    }
                }
                var data = this.DataBoxy();
                data.url += "/LogisticsNo.aspx?";
                data.title = "物流信息填写";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Send",
                    id: d.join(","),
                    orderid: list.join(",")
                });
                this.ShowBoxy(data);
            },
            Search: function () {
                var orderNo = $("#cphMain_txtOrderNo").val();
                var ddlState = $("#State").val();
                var startDate = $("#cphMain_txtIBeginDate").val();
                var endDate = $("#cphMain_txtIEndDate").val();

                window.location.href = "Orders.aspx?orderNo=" + orderNo + "&ddlState=" + ddlState + "&startDate=" +
startDate + "&endDate=" + endDate + "";
            },


            BindBtn: function () {
                //审核通过
                $(".toolbar_StateBack").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.CheckPass(rows);
                    return true;
                });

                //审核无效
                $(".toolbar_disabled").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.NoPass(rows);
                    return true;
                });
                //退款
                $(".toolbar_tuikuan").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.UpdateOrderstate(rows);
                    return true;
                });
                //搜索
                $("#btnSearch").click(function () {
                    PageJsDataObj.Search();
                    return false;
                });
                //填写物流信息
                $(".toolbar_send").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Send(rows);
                    return true;
                });
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

