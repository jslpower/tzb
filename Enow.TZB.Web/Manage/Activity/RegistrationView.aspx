<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="RegistrationView.aspx.cs" Inherits="Enow.TZB.Web.Manage.Activity.RegistrationView" %>
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
                    发布目标:
                    <asp:DropDownList ID="dropfbmb" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                    活动分类:
                    <asp:DropDownList ID="droptype" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                    活动名称：
                    <input type="text" id="txtTitle" runat="server" name="txtTitle" class="input-txt" />
                    &nbsp;&nbsp;
                    报名状态:
                    <asp:DropDownList ID="dropstater" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                    <input type="button" id="btnSearch" class="search-btn" causesvalidation="False" value="搜索" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                    <asp:PlaceHolder ID="phadd" runat="server" Visible="false">
                      <li><s class="addicon"></s><a href="javascript:void(0)" class="Shtg">
                            审核通过</a></li>
                            <li class="line"></li>
                        <li><s class="updateicon"></s><a href="#" class="Shwtg">审核未通过</a></li>
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
                            序号
                        </th>
                        <th align="center">类型</th>
                        <th align="center">活动名称</th>
                        <th align="center">
                            姓名
                        </th>
                        <th align="center">
                            手机
                        </th>
                        <th align="center">
                            邮箱
                        </th>
                         <th align="center">
                            住址
                        </th>
                         <th align="center">
                            报名时间
                        </th>
                         <th align="center">
                            状态
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" data-title="<%#Eval("Title")%>" value="<%#Eval("id") %>" />
                                </td>
                                 <td align="center">
                                    <%#Container.ItemIndex+1%>
                                </td>
                                 <td align="center">
                                    <%#(Enow.TZB.Model.EnumType.ActivityEnum)(Enow.TZB.Utility.Utils.GetInt(Eval("Activitytypes").ToString()))%>
                                </td>
                                <td align="center">
                                    <%#Eval("Title")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ContactName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("MobilePhone")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Email")%>
                                </td>
                                <td align="center">
                                    <%#Eval("CountryName")%><%#Eval("ProvinceName")%><%#Eval("CityName")%><%#Eval("AreaName")%><%#Eval("Address")%>
                                </td>
                                 <td align="center">
                                   <%#Enow.TZB.Utility.Utils.GetDateTime(Eval("Indatetime").ToString()).ToString("yyyy-MM-dd")%>
                                </td>
                                <td align="center">
                                    <%#(Enow.TZB.Model.EnumType.ApplicantsStartEnum)(Enow.TZB.Utility.Utils.GetInt((Eval("IsState").ToString())))%>
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
                    url: '/Manage/Activity',
                    title: "",
                    width: "830px",
                    height: "620px"
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
            Update: function (objsstater) {
               var list = new Array();
                var sname="";
                $("#liststyle").find("input[type='checkbox'][name='checkbox']:checked").each(function () {
                    var vl=$(this).val();
                    if(vl!="on"&&vl!="")
                    list.push(vl);
                    var getname=$(this).attr("data-title");
                    if(getname!="")
                    sname=getname;
                });
                if (list.length <=0) {
                    tableToolbar._showMsg("未选择任何数据！");
                    return;
                }
                var data = this.DataBoxy();
                data.url += '/RegistrationView.aspx?';
                data.title = '修改活动';
                data.url += $.param({
                    sl: this.Query.sl,
                    MId:<%=Request.QueryString["MId"] %>,
                    SMId:<%=Request.QueryString["SMId"] %>,
                    CId:<%=Request.QueryString["CId"] %>,
                    doType:objsstater?"shtg":"shwtg",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            Search: function () {
                var Title = $("#<%=txtTitle.ClientID %>").val();
                var types=$("#<%=droptype.ClientID %>").val();
                var staters=$("#<%=dropstater.ClientID %>").val();
                var fbmb=$("#<%=dropfbmb.ClientID %>").val();
                window.location.href = "RegistrationView.aspx?"+$.param(PageJsDataObj.Query)+"&Title=" + Title + "&types=" + types + "&staters=" + staters+"&fbmb="+fbmb;
            },
            BindBtn: function () {
                //查看报名人数
                $(".Shtg").click(function() {
                   PageJsDataObj.Update(true);
                });
                $(".Shwtg").click(function() {
                   PageJsDataObj.Update(false);
                });
                //搜索
                $("#btnSearch").click(function () {
                    PageJsDataObj.Search();

                });
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "报名管理",
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
