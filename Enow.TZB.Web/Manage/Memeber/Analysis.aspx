<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="Analysis.aspx.cs" Inherits="Enow.TZB.Web.Manage.Memeber.Analysis" %>
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
                <p>
                    审核状态：<select id="State" name="State">
                        <option value="-1">请选择</option>
                        <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.会员状态),
                                                               new string[] { }), Enow.TZB.Utility.Utils.GetInt(Request.QueryString["State"],-1).ToString())
                        %></select>&nbsp;&nbsp;姓名：
                    <asp:TextBox ID="txtContractName" CssClass="formsize50 input-txt" runat="server" />
                    &nbsp;&nbsp;手机号：
                    <asp:TextBox ID="txtMobile" CssClass="formsize80 input-txt" runat="server" />
                    &nbsp;&nbsp;昵称：
                    <asp:TextBox ID="txtNickName" CssClass="formsize80 input-txt" runat="server"></asp:TextBox>
                    <asp:PlaceHolder ID="phJoinTeam" runat="server" Visible="false">
                    &nbsp;&nbsp;是否参加球队：
                    <asp:DropDownList ID="ddlJoinTeam" runat="server">
                        <asp:ListItem Value="0">请选择</asp:ListItem>
                        <asp:ListItem Value="1">否</asp:ListItem>
                        <asp:ListItem Value="2">是</asp:ListItem>
                    </asp:DropDownList>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phJoinMatch" runat="server" Visible="false">
                     &nbsp;&nbsp;是否参加比赛：
                     <asp:DropDownList ID="ddlJoinMatch" runat="server">
                        <asp:ListItem Value="0">请选择</asp:ListItem>
                        <asp:ListItem Value="1">否</asp:ListItem>
                        <asp:ListItem Value="2">是</asp:ListItem>
                    </asp:DropDownList>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phArea" runat="server" Visible="false">
                    &nbsp;&nbsp;国家:
                    <select id="ddlCountry" name="ddlCountry" >
                    </select>省份:
                    <select id="ddlProvince" name="ddlProvince">
                    </select>城市:
                    <select id="ddlCity" name="ddlCity">
                    </select>区县:
                    <select id="ddlArea" name="ddlArea">
                    </select>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phRegTime" runat="server" Visible="false">
                    &nbsp;&nbsp;注册时间：
                    <asp:TextBox ID="txtIBeginDate" onfocus="WdatePicker()" runat="server" CssClass="formsize80 input-txt" MaxLength="10"></asp:TextBox>-<asp:TextBox ID="txtIEndDate"  onfocus="WdatePicker()" runat="server" CssClass="formsize80 input-txt" MaxLength="10"></asp:TextBox>
                       &nbsp;&nbsp;审核时间：
                       <asp:TextBox ID="txtCBeginDate" onfocus="WdatePicker()" runat="server" CssClass="formsize80 input-txt" MaxLength="10"></asp:TextBox>-<asp:TextBox ID="txtCEndDate"     onfocus="WdatePicker()" runat="server" CssClass="formsize80 input-txt" MaxLength="10"></asp:TextBox>
                       </asp:PlaceHolder>
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="search-btn" CausesValidation="False"
                        OnClick="btnSearch_Click" />
                </p>
            </span>
        </div>
        <div class="listbox">
            <div id="tablehead" class="tablehead">
                <ul class="fixed">                    
                    <li><s class="qiyicon"></s><asp:LinkButton ID="lbtnToExcel" 
                            CssClass="toolbar_ToExcel" runat="server" onclick="btnExport_Click">导出Excel</asp:LinkButton></li>
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
                            地域
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
                            昵称
                        </th>
                        <th align="center">
                            状态
                        </th>
                        <th align="center">
                            所属球队
                        </th>
                        <th align="center">
                            注册时间
                        </th>
                         <th align="center">
                            审核时间
                        </th>
                        <th align="center">
                            铁丝币
                        </th>
                        <th align="center">
                            积分
                        </th>
                        <th align="center">
                            荣誉值
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("Id") %>" data-state="<%#Eval("State") %>" />
                                </td>
                                <td align="left">
                                    <%#Eval("CountryName")%>-<%#Eval("ProvinceName")%>-<%#Eval("CityName")%>-<%#Eval("AreaName") %></td>
                                <td align="left">
                                    <%#Eval("MobilePhone")%>
                                </td>
                                <td align="left">
                                   <a href="Detail.aspx?Id=<%#Eval("Id") %>"> <%#Eval("ContactName")%></a>
                                </td>
                                <td align="left">
                                    <%#Eval("PersonalId")%>
                                </td>
                                <td align="center">
                                    <%#Eval("NickName")%>
                                </td>
                                <td align="center">
                                    <%#(Enow.TZB.Model.EnumType.会员状态)Convert.ToInt32(Eval("State"))%>
                                </td>
                                <td align="center">
                                    <%#Eval("TeamName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
                                </td>
                                 <td align="center">
                                    <%#Eval("CheckTime","{0:yyyy-MM-dd}")%>
                                </td>
                                <td align="center">
                                    <%#Eval("CurrencyNumber","{0:F0}")%>
                                </td>
                                <td align="center">
                                    <%#Eval("IntegrationNumber")%>
                                </td>
                                <td align="center">
                                    <%#Eval("HonorNumber")%>
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
                    objectName: "铁丝管理"
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
