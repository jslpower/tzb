<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" CodeBehind="ScheduleList.aspx.cs" Inherits="Enow.TZB.Web.Manage.Match.ScheduleList" %>
<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/js/moveScroll.js"></script>
    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="contentbox">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    赛事名称：
                    <input type="text" id="txtMatchName" name="txtMatchName" class="input-txt" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("MatchName")%>' />
                    &nbsp;&nbsp;赛事阶段名称：
                    <input type="text" id="txtGameName" name="txtGameName" class="input-txt" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("GameName")%>' />
                    &nbsp;&nbsp;球场名称：
                    <input type="text" id="txtFieldName" name="txtFieldName" class="input-txt" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("FieldName")%>' />
                    <input type="button" id="btnSearch" name="btnSearch" class="search-btn" value="搜索" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                    <li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_AddSchedule">
                        排赛</a></li>
                    <li class="line"></li>
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
                            赛事名称
                        </th>
                        <th align="center">
                            球场名称
                        </th>
                        <th align="center">
                            阶段名称
                        </th>
                        <th align="center">
                            排赛详情
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
                                <td align="left"><%#Eval("MatchName")%>
                                </td>     
                                <td align="center">
                                    <%#Eval("FieldName")%>
                                </td>                           
                                <td align="center">
                                   <%#Eval("GameName")%>
                                </td>  
                                <td align="center">
                                   <a href="ScheduleDetail.aspx?id=<%#Eval("Id") %>">查看</a>
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
                    MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
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
                        url: '/Manage/Match',
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
                                    window.location.href = window.location.href;
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
                Schedule: function (objsArr) {
            var list = new Array();
                    for (var i = 0; i < objsArr.length; i++) {
                        list.push(objsArr[i].find("input[type='checkbox']").val());
                    }
                    if (list.length == 0) {
                        tableToolbar._showMsg("请选择要操作的数据");
                        return false;
                    } else if (list.length > 1) {
                        tableToolbar._showMsg("只能选中一条数据操作");
                        return false;
                 }
                var data = this.DataBoxy();
                data.url += '/MatchScheduleAdd.aspx?';
                data.title = '新增赛事日程信息';
                data.url += $.param({
                    sl: this.Query.sl,
                    MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
                    id: objsArr[0].find('input[type="checkbox"]').val()
                });
                window.location.href = data.url;
            },

                Search: function () {
                    var MatchName = escape($("#txtMatchName").val());
                    var GameName = escape($("#txtGameName").val());
                    var FieldName = escape($("#txtFieldName").val());
                    url = "ScheduleList.aspx?MatchName=" + MatchName + "&GameName=" + GameName + "&FieldName=" + FieldName + "&"+$.param(PageJsDataObj.Query);
                    window.location.href = url;
                },
                BindBtn: function () {
                    //赛事日程
                    $(".toolbar_AddSchedule").click(function () {
                        var rows = tableToolbar.getSelectedCols();
                        PageJsDataObj.Schedule(rows);
                        return false;
                    });
                    //搜索
                    $("#btnSearch").click(function () {
                        PageJsDataObj.Search();
                        return false;
                    });
                    tableToolbar.init({
                        tableContainerSelector: "#liststyle", //表格选择器
                        objectName: "排赛管理"
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