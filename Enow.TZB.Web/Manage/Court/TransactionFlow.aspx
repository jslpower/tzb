<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="TransactionFlow.aspx.cs" Inherits="Enow.TZB.Web.Manage.Court.TransactionFlow" %>
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
                </ul>
                <div class="pages">
                    <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </div>
            </div>
            <!--列表表格-->
            <div class="tablelist-box">
                <table width="100%" id="liststyle">
                    <tr>
                    <th align="center">
                            编号</th>
                        <th align="center">
                            场所类型
                        </th>
                        <th align="center">
                            场所名称
                        </th>
                        <th align="center">
                            地域
                        </th>
                        <th align="center">
                                    流水号
                                </th>
                     <th align="center">
                                    收银项目
                                </th>
                                <th align="center">
                                    数量
                                </th>
                                <th align="center">
                                    收款金额
                                </th>
                                <th align="center">
                                    收款方式
                                </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
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
                               <td>
                                            <%#Eval("JiaoYiHao") %>
                                        </td>
                                        <td>
                                            <%#Eval("GoodsName") %>
                                        </td>
                                        <td>
                                            <%#Eval("State").ToString() == "0" ? Eval("Amount") : "-"+Eval("Amount")%>
                                        </td>
                                        <td>
                                            <%#Eval("State").ToString() == "0" ?Eval("JinE","{0:F2}"):"-"+Eval("JinE","{0:F2}")%>
                                        </td>
                                        <td>
                                            <%#(Enow.TZB.Model.收款类型)(int)Eval("PayType") %>
                                        </td>
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
            BindBtn: function () {
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "场所流水管理"
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
