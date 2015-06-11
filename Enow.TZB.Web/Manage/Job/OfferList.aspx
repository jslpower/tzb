<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="OfferList.aspx.cs" Inherits="Enow.TZB.Web.Manage.Job.OfferList" %>

<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/js/moveScroll.js"></script>
    <script type="text/javascript" src="../../Js/datepicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="contentbox">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    职位名称：
                    <input type="text" id="txtJobName" name="txtJobName" class="input-txt" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("JobName")%>' />
                    &nbsp;&nbsp;姓名：
                    <input type="text" id="txtUserName" name="txtUserName" class="input-txt" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("UserName")%>' />
                    &nbsp;&nbsp;手机号码：
                    <input type="text" id="txtMobile" name="txtMobile" class="input-txt" />
                    &nbsp;&nbsp;应聘状态：
                    <select id="OfferState" name="OfferState">
                        <%=InitOfferState(Enow.TZB.Utility.Utils.GetQueryStringValue("OfferState")) %>
                    </select>
                </p>
                <p>
                    &nbsp;&nbsp;就职区域： &nbsp;国家
                    <select id="ddlCountry" name="ddlCountry">
                    </select>
                    &nbsp;省份
                    <select id="ddlProvince" name="ddlProvince">
                    </select>
                    &nbsp;城市
                    <select id="ddlCity" name="ddlCity">
                    </select>
                    &nbsp;区县
                    <select id="ddlArea" name="ddlArea">
                    </select>
                    &nbsp;&nbsp;申请日期：
                    <input type="text" onfocus="WdatePicker()" name="txtStartTime" id="txtStartTime"
                        value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("startDate")%>' size="10"
                        cssclass="inputtext" />-<input type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtStartTime\')}'})"
                            cssclass="inputtext" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("enddate")%>'
                            name="txtEndTime" id="txtEndTime" size="10" />
                    <input type="submit" id="btnSearch" name="btnSearch" class="search-btn" value="搜索" />
                    &nbsp;&nbsp;<asp:Button ID="btnExport" runat="server" CssClass="search-btn" Text="导出Excel"
                        OnClick="btnExport_Click" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                    <li><s class="qiyicon"></s><a href="#" hidefocus="true" class="toolbar_update">应聘状态审核</a></li>
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
                            申请职位
                        </th>
                        <th align="center">
                            就职区域
                        </th>
                        <th align="center">
                            用户名
                        </th>
                        <th align="center">
                            手机号码
                        </th>
                        <th align="center">
                            姓名
                        </th>
                        <th align="center">
                            身份证号
                        </th>
                        <th align="center">
                            地址
                        </th>
                        <th align="center">
                            Email
                        </th>
                        <th align="center">
                            工作年限
                        </th>
                        <th align="center">
                            球龄
                        </th>
                        <th align="center">
                            专业
                        </th>
                        <th align="center">
                            应聘状态
                        </th>
                        <th align="center">
                            申请日期
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("Id") %>" />
                                </td>
                                <td align="center">
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0)" class="transactions" data-rel='<%#Eval("Id") %>'>
                                        <%#Eval("JobName")%></a>
                                </td>
                                <td align="center">
                                <%#Eval("CountryName")%>-<%#Eval("ProvinceName")%>-<%#Eval("CityName")%>-<%#Eval("AreaName") %>
                                </td>
                                <td align="center">
                                    <%#Eval("UserName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("MobilePhone")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ContactName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("PersonalId")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Address")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Email")%>
                                </td>
                                <td align="center">
                                    <%#Eval("WorkYear")%>
                                </td>
                                <td align="center">
                                    <%#Eval("BallYear")%>
                                </td>
                                <td align="center">
                                    <%#Eval("specialty")%>
                                </td>
                                <td align="center">
                                    <%#(Enow.TZB.Model.EnumType.应聘状态)int.Parse(Eval("offerState").ToString())%>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime", "{0:yyyy-MM-dd}")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="phNoData" runat="server" Visible="false">
                        <tr>
                            <td colspan="7" align="center">
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
                    url: '/Manage/Job',
                    title: "",
                    width: "960px",
                    height: "800px"
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
            Update: function (objsArr) {
                var data = this.DataBoxy();
                data.url += '/OfferReview.aspx?';
                data.title = '应聘状态审核';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "update",
                    id: objsArr[0].find('input[type="checkbox"]').val()
                });
                this.ShowBoxy(data);
            },

            Search: function () {
                var Jobname = $("#txtJobName").val();
                var Username = $("#txtUserName").val();
                var Offerstate = $("#OfferState").val();
                var startDate = $("#txtStartTime").val();
                var endDate = $("#txtEndTime").val();
                var Mobile = $("#txtMobile").val();
                var Country = $("#ddlCountry").val();
                var Province = $("#ddlProvince").val();
                var City = $("#ddlCity").val();
                var Area = $("#ddlArea").val();
                window.location.href = "OfferList.aspx?JobName=" + Jobname + "&UserName=" + Username + "&OfferState=" + Offerstate + "&startdate=" + startDate + "&enddate=" + endDate + "&Mobile="+Mobile+"&Country="+Country+"&Province="+Province+"&City="+City+"&Area="+Area+"";
            },
            View: function (Id) {
                var data = this.DataBoxy();
                data.url += '/OfferReview.aspx?';
                data.title = '应聘信息查看';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "view",
                    id: Id
                });
                this.ShowBoxy(data);
            },
            BindBtn: function () {
                //查看应聘信息
                $(".transactions").click(function () {
                    PageJsDataObj.View($(this).attr("Data-rel"));
                    return false;
                });
                //搜索
                $("#btnSearch").click(function () {
                    PageJsDataObj.Search();
                    return false;
                });

                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "招聘信息管理",
                    updateCallBack: function (objsArr) {
                        PageJsDataObj.Update(objsArr);
                        return false;
                    }
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
            pcToobar.init({
                gID: "#ddlCountry",
                pID: "#ddlProvince",
                cID: "#ddlCity",
                xID: "#ddlArea",
                comID: '',
                gSelect: '<%=CId %>',
                pSelect: '<%=PId %>',
                cSelect: '<%=CSId %>',
                xSelect: '<%=AId %>'
            });
            PageJsDataObj.PageInit();
            PageJsDataObj.BindClose();
            return false;
        })
    </script>
</asp:Content>
