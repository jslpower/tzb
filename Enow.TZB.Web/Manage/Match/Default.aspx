<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.Web.Manage.Match.Default" %>

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
                    <input type="text" id="txtName" name="txtName" class="input-txt" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("name")%>' />
                    &nbsp;&nbsp;赛事起止日期：
                    <input type="text" onfocus="WdatePicker()" name="txtStartTime" id="txtStartTime" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("startdate")%>'
                        size="10" cssclass="inputtext" />-<input type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtStartTime\')}'})"
                            cssclass="inputtext" name="txtEndTime" id="txtEndTime" size="10" value='<%=Enow.TZB.Utility.Utils.GetQueryStringValue("enddate")%>' />
                    <input type="button" id="btnSearch" name="btnSearch" class="search-btn" value="搜索" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                    <li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_add add_gg">
                        新增</a></li>
                    <li class="line"></li>
                    <li><s class="updateicon"></s><a href="#" hidefocus="true" class="toolbar_update">修改</a></li>
                    <li class="line"></li>
                    <li><s class="delicon"></s><a href="#" hidefocus="true" class="toolbar_delete">删除</a></li>
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
                            赛制
                        </th>
                        <th align="center">
                            地域
                        </th>
                        <th align="center">
                            赛事类型
                        </th>
                        <th align="center">
                            最大报名数量
                        </th>
                        <th align="center">
                        审核通过报名数
                        </th>
                        <th align="center">
                            保证金
                        </th>
                        <th align="center">报名费
                        </th>
                        <th align="center">
                            开始日期
                        </th>
                        <th align="center">
                            结束日期
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
                                        <%#Eval("MatchName")%></a>
                                </td>
                                <td align="center">
                                    <%#(Enow.TZB.Model.EnumType.赛制)int.Parse(Eval("typeId").ToString())%>
                                </td>
                                <td align="center">
                                    <%#Eval("CountryName")%>-<%#Eval("ProvinceName")%>-<%#Eval("CityName")%>
                                </td>
                                <td align="center">
                                    <%#(Enow.TZB.Model.EnumType.赛事类型)int.Parse(Eval("MatchAreaType").ToString())%>
                                </td>
                                <td align="center">
                                    <%#Eval("TeamNumber")%>
                                </td>
                                <td align="center">
                                <%#Eval("SignUpNumber")%>
                                </td>
                                <td align="center">
                                    <%#Eval("RegistrationFee","{0:f2}") %>元
                                </td>
                                <td align="center">
                                    <%#Eval("EarnestMoney", "{0:f2}")%>元
                                </td>
                                <td align="center">
                                    <%#Eval("BeginDate", "{0:yyyy-MM-dd}")%>
                                </td>
                                <td align="center">
                                    <%#Eval("EndDate", "{0:yyyy-MM-dd}")%>
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
            Add: function () {
                var data = this.DataBoxy();
                data.url += '/MatchAdd.aspx?';
                data.title = '添加比赛信息';
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
                data.url += '/MatchAdd.aspx?';
                data.title = '修改比赛信息';
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
                data.url += "/Default.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "delete",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
              Search: function () {
                var name = $("#txtName").val();               
                var startDate = $("#txtStartTime").val();
                var endDate = $("#txtEndTime").val();
                url = "Default.aspx?name=" + name + "&startdate=" + startDate + "&enddate=" + endDate + "&"+$.param(PageJsDataObj.Query);
                window.location.href = url;
            },
            BindBtn: function () {
                //添加
                $(".add_gg").click(function () {
                    PageJsDataObj.Add();
                    return false;
                });
                 //搜索
                $("#btnSearch").click(function () {
                    PageJsDataObj.Search();
                    return false;
                });         
                
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "赛事管理",
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
