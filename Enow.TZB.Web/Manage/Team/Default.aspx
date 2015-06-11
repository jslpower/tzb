<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.Web.Manage.Team.Default" %>

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
                    审核状态：<select id="State" name="State">
                        <option value="-1">请选择</option>
                        <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.球队审核状态),
                                                               new string[] { }), Enow.TZB.Utility.Utils.GetInt(Request.QueryString["State"],-1))
                        %></select>&nbsp;&nbsp;球队名称：
                    <asp:TextBox ID="txtKeyWord" CssClass="formsize120 input-txt" runat="server" />
                    &nbsp;&nbsp;国家:
                    <select id="ddlCountry" name="ddlCountry" >
                    </select>省份:
                    <select id="ddlProvince" name="ddlProvince">
                    </select>城市:
                    <select id="ddlCity" name="ddlCity">
                    </select>区县:
                    <select id="ddlArea" name="ddlArea">
                    </select>
                    </p>
                    <p>
                    &nbsp;&nbsp;队长姓名：
                    <asp:TextBox ID="txtTeamOwner" CssClass="formsize100 input-txt" runat="server" />&nbsp;&nbsp;队长手机：
                    <asp:TextBox ID="txtOwnerMobile" CssClass="formsize100 input-txt" runat="server" MaxLength="11" />&nbsp;&nbsp;&nbsp;&nbsp;注册时间：
                    <asp:TextBox ID="txtIBeginDate" onfocus="WdatePicker()" runat="server" CssClass="formsize80 input-txt" MaxLength="10"></asp:TextBox>-<asp:TextBox ID="txtIEndDate"  onfocus="WdatePicker()" runat="server" CssClass="formsize80 input-txt" MaxLength="10"></asp:TextBox>
                    </p>
                    <p>
                       &nbsp;&nbsp;审核时间：
                       <asp:TextBox ID="txtCBeginDate" onfocus="WdatePicker()" runat="server" CssClass="formsize80 input-txt" MaxLength="10"></asp:TextBox>-<asp:TextBox ID="txtCEndDate"     onfocus="WdatePicker()" runat="server" CssClass="formsize80 input-txt" MaxLength="10"></asp:TextBox>&nbsp;&nbsp;是否参加比赛：
                      <select id="ddlJoinMatch" name="ddlJoinMatch">
                      <option value="0">请选择</option>
                      <option value="1">否</option>
                      <option value="2">是</option>
                      </select>
                      &nbsp;&nbsp;是否有队员：
                      <select id="ddlTeamer" name="ddlTeamer">
                      <option value="0">请选择</option>
                      <option value="1">否</option>
                      <option value="2">是</option>
                      </select>
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="search-btn" CausesValidation="False"
                        OnClick="btnSearch_Click" />
                         &nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="导出excel" CssClass="search-btn"
                        CausesValidation="false" onclick="btnExport_Click" />
                    
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">
                <li><s class="jinyicon"></s><a href="#" hidefocus="true" class="toolbar_StateBack">退回重审</a></li>
                    <asp:PlaceHolder ID="PhDZCheck" runat="server" Visible="false">
                        <li><s class="qiyicon"></s><a href="#" hidefocus="true" class="toolbar_enable">初审通过</a></li>
                        <li><s class="jinyicon"></s><a href="#" hidefocus="true" class="toolbar_disabled">初审拒绝</a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phEndCheck" runat="server" Visible="false">
                        <li><s class="qiyicon"></s><a href="#" hidefocus="true" class="toolbar_enable2">终审通过</a></li>
                        <li><s class="jinyicon"></s><a href="#" hidefocus="true" class="toolbar_disabled2">终审拒绝</a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phDisband" runat="server" Visible="false">
                        <li><s class="qiyicon"></s><a href="#" hidefocus="true" class="toolbar_Disband">同意解散</a></li>
                        <li><s class="jinyicon"></s><a href="#" hidefocus="true" class="toolbar_NoDisband">拒绝解散</a></li>
                        <li class="line"></li>
                    </asp:PlaceHolder>
                     <li><s class="updateicon"></s><a href="javascript:;" class="Import">导入</a></li>
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
                            编号</th>
                            <th align="center">
                                球队名称
                            </th>
                            <th align="center">
                                地域
                            </th>
                            <th align="center">
                                队长姓名
                            </th>
                            <th align="center">
                               队长手机号
                            </th>
                             <th align="center">
                               审核状态
                            </th>
                            <th align="center">
                                参加比赛数
                            </th>
                            <th align="center">
                                荣誉值
                            </th>
                            <th align="center">
                                队员数
                            </th>
                            <th align="center">
                                创建时间
                            </th>
                           
                            <th align="center">
                                审核时间
                            </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("Id") %>" data-state="<%#Eval("State") %>" />
                                </td>
                                <td align="center">
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                 <a href="javascript:void(0)" class="transactions" data-rel='<%#Eval("Id") %>'>
                                    <%#Eval("TeamName")%></a>
                                </td>
                                <td align="center">
                                    <%#Eval("CountryName")%>-<%#Eval("ProvinceName")%>-<%#Eval("CityName")%><%#Eval("AreaName") %></td>
                                <td align="center">
                                    <%#Eval("ContactName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("MobilePhone")%>
                                </td>
                                <td align="center">
                                     <%#(Enow.TZB.Model.EnumType.球队审核状态)Convert.ToInt32(Eval("State"))%>
                                </td>
                                <td align="center">
                                    <%#GetMatchCount(Eval("Id").ToString())%>
                                </td>
                                <td align="center">
                                    <%#Eval("HonorNumber")%>
                                </td>
                                 <td align="center">
                                  <%#GetTeamerCount(Eval("Id").ToString())%>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
                                </td>  
                                <td align="center">
                                    <%#Eval("CheckTime","{0:yyyy-MM-dd}")%>
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
                    url: '/Manage/Team',
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
            //审核退回
            StateBack: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] == '<%=(int)Enow.TZB.Model.EnumType.球队审核状态.审核中 %>') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录未审核，不能退回初审状态！");
                        return;
                    }
                }

                var data = this.DataBoxy();
                data.url += "/Default.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "StateBack",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            Enable: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '<%=(int)Enow.TZB.Model.EnumType.球队审核状态.审核中 %>') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录已初审，不能二次审核！");
                        return;
                    }
                }
              
                var data = this.DataBoxy();
                data.url += "/Default.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Enable",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            Disabled: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '<%=(int)Enow.TZB.Model.EnumType.球队审核状态.审核中 %>') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录已初审，不能二次审核！");
                        return;
                    }
                }
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能操作一条信息");
                    return;
                }
                var data = this.DataBoxy();
                data.url += "/RefuseReason.aspx?";
                data.title = "初审拒绝";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Disabled",
                    id: list.join(",")
                });
                this.ShowBoxy(data);
            },
            Enable2: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '<%=(int)Enow.TZB.Model.EnumType.球队审核状态.初审通过 %>') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录初审未通过，不能二次审核！");
                        return;
                    }
                }
               
                var data = this.DataBoxy();
                data.url += "/Default.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Enable2",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            Disabled2: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '<%=(int)Enow.TZB.Model.EnumType.球队审核状态.初审通过 %>') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录已未通过，不能二次审核！");
                        return;
                    }
                }
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能操作一条信息");
                    return;
                }
                var data = this.DataBoxy();
                data.url += "/RefuseReason.aspx?";
                data.title = "终审拒绝";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Disabled2",
                    id: list.join(",")
                });
                this.ShowBoxy(data);
            },
            Disband: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '<%=(int)Enow.TZB.Model.EnumType.球队审核状态.解散申请 %>') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录未提交解散申请，不能进行审核！");
                        return;
                    }
                }
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能操作一条信息");
                    return;
                }
                var data = this.DataBoxy();
                data.url += "/Default.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Disband",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            NoDisband: function (objsArr) {
                var list = new Array();
                var state = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                    state.push(objsArr[i].find("input[type='checkbox']").attr("data-state"));
                }
                //检查状态
                for (var j = 0; j < state.length; j++) {
                    if (state[j] != '<%=(int)Enow.TZB.Model.EnumType.球队审核状态.解散申请 %>') {
                        tableToolbar._showMsg("第" + (j + 1) + "条记录未提交解散申请，不能进行审核！");
                        return;
                    }
                }
                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能操作一条信息");
                    return;
                }
                var data = this.DataBoxy();
                data.url += "/RefuseReason.aspx?";
                data.title = "拒绝解散";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "NoDisband",
                    id: list.join(",")
                });
                this.ShowBoxy(data);
            },
            View: function (Id) {
                var data = this.DataBoxy();
                data.url += '/TeamEdit.aspx?';
                data.title = '查看球队信息';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "view",
                    id: Id
                });
                window.location.href = data.url;
            },
            Import: function () {
                var data = this.DataBoxy();
                data.url += '/TeamImport.aspx?';
                data.title = '导入球队信息';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "Import"
                });
                this.ShowBoxy(data);
            },
            BindBtn: function () {
                //退回初审
                $(".Import").click(function () {
                    PageJsDataObj.Import();
                    return true;
                });
                //退回初审
                $(".toolbar_StateBack").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.StateBack(rows);
                    return true;
                });
                //启用
                $(".toolbar_enable").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Enable(rows);
                    return true;
                });
                //禁用
                $(".toolbar_disabled").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Disabled(rows);
                    return false;
                });
                //启用
                $(".toolbar_enable2").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Enable2(rows);
                    return true;
                });
                //禁用
                $(".toolbar_disabled2").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Disabled2(rows);
                    return false;
                });
                //解散审批
                $(".toolbar_Disband").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.Disband(rows);
                    return false;
                });
                //解散审批
                $(".toolbar_NoDisband").click(function () {
                    var rows = tableToolbar.getSelectedCols();
                    PageJsDataObj.NoDisband(rows);
                    return false;
                });
                //查看球队详细信息
                $(".transactions").click(function () {
                    PageJsDataObj.View($(this).attr("Data-rel"));
                    return false;
                });
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "球队管理"
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
