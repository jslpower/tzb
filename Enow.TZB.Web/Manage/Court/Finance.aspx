<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="Finance.aspx.cs" Inherits="Enow.TZB.Web.Manage.Court.Finance" %>
<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
<link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/js/moveScroll.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
<div class="contentbox">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>场所类型：<select id="ddlTypeId" name="ddlTypeId" >
                    </select>&nbsp;&nbsp;场所名称：<select id="ddlFieldId" name="ddlFieldId" >
                    </select>&nbsp;&nbsp;国家:
                    <select id="ddlCountry" name="ddlCountry" >
                    </select>省份:
                    <select id="ddlProvince" name="ddlProvince">
                    </select>城市:
                    <select id="ddlCity" name="ddlCity">
                    </select>区县:
                    <select id="ddlArea" name="ddlArea">
                    </select>交易日期：
                    <asp:DropDownList ID="ddlYear" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlMonth" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="search-btn" CausesValidation="False"
                        OnClick="btnSearch_Click" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                <asp:PlaceHolder ID="PhSettle" runat="server">
                    <li><s class="qiyicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_Settle">
                        结算</a></li>
                    <li class="line"></li>
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
                    <th width="30" class="thinputbg" rowspan="2">
                            <input type="checkbox" name="checkbox" id="checkbox" />
                        </th>
                    <th align="center" rowspan="2">
                            编号</th>
                        <th align="center" rowspan="2">
                            场所类型
                        </th>
                        <th align="center" rowspan="2">
                            场所名称
                        </th>
                        <th align="center" rowspan="2">
                            地域
                        </th>
                        <th align="center" rowspan="2">
                            交易月份
                        </th>
                        <th align="center" colspan="9">
                            交易情况
                        </th>
                         <th align="center" rowspan="2">
                            结算状态
                        </th>
                        <th align="center" rowspan="2">
                            结算日期
                        </th>
                        <th align="center" rowspan="2">
                            结算人
                        </th>
                    </tr>
                    <tr>
                        <th align="center">
                            现金收款</th>
                        <th align="center">
                            现金退款</th>
                        <th align="center">
                            刷卡收款</th>
                        <th align="center">
                            刷卡退款</th>
                        <th align="center">
                            现金充值</th>
                        <th align="center">
                            刷卡充值</th>
                        <th align="center">
                            铁丝币收款</th>
                        <th align="center">
                            铁丝币退款</th>
                        <th align="center">
                            结算金额</th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                            <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("Id") %>" data-state="<%#Convert.ToBoolean(Eval("IsSettle"))?"1":"0" %>" />
                                </td>
                            <td align="center">
                                    <%#(CurrencyPage-1) * intPageSize+Container.ItemIndex + 1%>
                                </td>
                                <td align="left">
                                    <%#Eval("FieldTypeName")%></td>
                                    <td align="left">
                                    <%#Eval("FieldName")%>
                                </td>
                                <td align="left">
                                    <%#Eval("ProvinceName")%>-<%#Eval("CityName")%>
                                </td>
                                <td align="center">
                                <%=intYear %>-<%=intMonth %>
                                </td>
                                <td align="center"><%#Eval("CashReceipts","{0:F2}")%></td>
                                <td align="center"><%#Eval("CashRefund", "{0:F2}")%></td>
                                <td align="center"><%#Eval("CardReceipts", "{0:F2}")%></td>
                                <td align="center"><%#Eval("CardRefund", "{0:F2}")%></td>
                                <td align="center"><%#Eval("CashRecharge", "{0:F2}")%></td>
                                <td align="center"><%#Eval("CardRecharge", "{0:F2}")%></td>
                                <td align="center"><%#Eval("TieSiCardReceipts", "{0:F2}")%></td>
                                <td align="center"><%#Eval("TieSiCardRefund", "{0:F2}")%></td>
                                <td align="center"><%#Eval("SettleMoney", "{0:F2}")%></td>
                                <td align="center"><%#Convert.ToBoolean(Eval("IsSettle"))?"已结算":"未结算"%></td>
                                <td align="center"><%#Eval("SettleTime","{0:yyyy-MM-dd}")%></td>
                                <td align="center"><%#Eval("SettleName")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="phNoData" runat="server" Visible="false">
                        <tr>
                            <td colspan="18" align="center">
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
                    url: '/Manage/Memeber',
                    title: "",
                    width: "710px",
                    height: "420px"
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
            //结算
            Settle: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                if (list.length < 1) {
                    tableToolbar._showMsg("请选择结算信息！");
                    return;
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '0') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录已结算，不能二次结算！");
                        return;
                    }
                }                
                var data = this.DataBoxy();
                data.url = "Finance.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Settle",
                    Year:<%=intYear %>,
                    Month:<%=intMonth %>,
                    id: list.join(",")
                });
                if (confirm("您是否确定对选中的场所进行结算？")) {
                    this.GoAjax(data.url);
                }
            },
            BindBtn: function () {
                $(".toolbar_Settle").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Settle(rows);
                    return true;
                });
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "场所结算管理"
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
            stToobar.init({
                gID: "#ddlTypeId",
                pID: "#ddlFieldId",
                gSelect: '<%=TypeId %>',
                pSelect: '<%=FieldId %>'
            });
            pcToobar.init({
                gID: "#ddlCountry",
                pID: "#ddlProvince",
                cID: "#ddlCity",
                xID: "#ddlArea",
                comID: '',
                gSelect: '<%=CountryId %>',
                pSelect: '<%=ProvinceId %>',
                cSelect: '<%=CityId %>',
                xSelect: '<%=AreaId %>'
            });
            PageJsDataObj.PageInit();
            PageJsDataObj.BindClose();
            return false;
        })
    </script>
</asp:Content>
