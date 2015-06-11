<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="CourtDefault.aspx.cs" Inherits="Enow.TZB.Web.Manage.Court.CourtDefault" %>

<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/js/moveScroll.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="contentbox">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    场所名称：
                    <input type="text" id="txtCourtName" name="txtCourtName" class="input-txt" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("CourtName")%>' />
                    &nbsp;&nbsp;
                    <input type="submit" id="btnSearch" class="search-btn" causesvalidation="False" value="搜索" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                <asp:PlaceHolder ID="PhAdd" runat="server">
                    <li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_add add_gg">
                        新增</a></li>
                    <li class="line"></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phUpdate" runat="server">
                    <li><s class="updateicon"></s><a href="#" hidefocus="true" class="toolbar_update">修改</a></li>
                    <li class="line"></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phDel" runat="server">
                    <li><s class="delicon"></s><a href="#" hidefocus="true" class="toolbar_delete">删除</a></li>
                    <li class="line"></li></asp:PlaceHolder>
                </ul>
                <div class="pages">
                    <cc1:exportpageinfo id="ExportPageInfo1" runat="server" />
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
                            场所名称
                        </th>
                        <th align="center">
                            地域
                        </th>
                        <th align="center">
                            地址
                        </th>
                        <th align="center">
                            数量
                        </th>
                        <th align="center">
                            价格
                        </th>
                        <th align="center">
                            营业时间
                        </th>
                        <th align="center">
                            场地大小
                        </th>
                        <th align="center">
                            添加日期
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <itemtemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("Id") %>" />
                                </td>
                                <td align="center">
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td>
                                <a href="javascript:void(0)" class="transactions" data-rel='<%#Eval("Id") %>'>
                                    <%#Eval("FieldName")%></a>
                                </td>
                                <td align="center">
                                <%#Eval("CountryName")%>-<%#Eval("ProvinceName")%>-<%#Eval("CityName")%>
                                  
                                </td>
                                <td align="center">
                                    <%#Eval("Address")%>
                                </td>
                                 <td align="center">
                                    <%#Eval("FieldNumber")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Price","{0:f2}") %>元/小时
                                </td>
                                <td align="center">
                                    <%#Eval("Hours")%>
                                </td>
                                <td align="center">
                                    <%#Eval("FieldSize")%>
                                </td>
                                
                                <td align="center">
                                    <%#Eval("IssueTime", "{0:yyyy-MM-dd}")%>
                                </td>
                               
                            </tr>
                        </itemtemplate>
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
                    url: '/Manage/Court',
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
            Add: function () {
                var data = this.DataBoxy();
                data.url += '/CourtAdd.aspx?';
                data.title = '添加场所信息';
                data.url += $.param({
                    sl: this.Query.sl,
                    MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
                    act:"add",
                    doType: "add"
                });
                window.location.href = data.url; ;
            },
            Update: function (objsArr) {
                var data = this.DataBoxy();
                data.url += '/CourtAdd.aspx?';
                data.title = '修改场所信息';
                data.url += $.param({
                    sl: this.Query.sl,
                    MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
                    act:"update",
                    doType: "update",
                    id: objsArr[0].find('input[type="checkbox"]').val()
                });
                window.location.href = data.url;
            },
            Delete: function (objsArr) {
                var list = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                }
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能删除一条信息");
                    return;
                }
                var data = this.DataBoxy();
                data.url += "/CourtDefault.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "delete",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            BindBtn: function () {
                //添加
                $(".add_gg").click(function () {
                    PageJsDataObj.Add();
                    return false;
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "场所管理",
                    updateCallBack: function (objsArr) {
                        PageJsDataObj.Update(objsArr);
                        return false;
                    },
                    deleteCallBack: function (objsArr) {
                        PageJsDataObj.Delete(objsArr);
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
            PageJsDataObj.PageInit();
            PageJsDataObj.BindClose();
            return false;
        })
    </script>
</asp:Content>
