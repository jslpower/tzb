<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatchScheduleUpdate.aspx.cs"
    MasterPageFile="~/MasterPage/FinaWinBackPage.Master" Inherits="Enow.TZB.Web.Manage.Match.MatchScheduleUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->
    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Js/TableUtil.js" type="text/javascript"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <div class="firsttable">
                <span class="firsttableT">赛事日程修改</span>
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <th width="100">
                            赛事阶段：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlGameId" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            场次号：
                        </th>
                        <td>
                            <asp:TextBox ID="txtOrdinalNumber" runat="server" MaxLength="40" CssClass="input-txt formsize50"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            比赛开始时间：
                        </th>
                        <td>
                            <asp:TextBox ID="txtStartTime" runat="server" CssClass="input-txt formsize140 Wdate"
                                onfocus="WdatePicker({startDate:'%y-%M-%d',dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            比赛结束时间：
                        </th>
                        <td>
                            <asp:TextBox ID="txtEndTime" runat="server" CssClass="input-txt formsize140 Wdate"
                                onfocus="WdatePicker({txtEndTime:'%y-%M-%d',dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            主队编号：
                        </th>
                        <td>
                            <asp:TextBox ID="txtHomeMatchCode" runat="server" CssClass="input-txt formsize50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            主队名称：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlHomeTeamId" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            客队编号：
                        </th>
                        <td>
                            <asp:TextBox ID="txtAwayMatchCode" runat="server" CssClass="input-txt formsize50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            客队名称：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlAwayTeamId" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            赛程状态：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="radGameState" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="0">未发布</asp:ListItem>
                                <asp:ListItem Value="1">已发布</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            结果状态：
                        </th>
                        <td>
                            <asp:RadioButtonList ID="radPublishTarget" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="0">未发布</asp:ListItem>
                                <asp:ListItem Value="1">已发布</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="Basic_btn fixed">
                <ul>
                    <li>
                        <asp:LinkButton ID="linkBtnSave" runat="server" onclick="linkBtnSave_Click">保 存 >></asp:LinkButton></li>
                    <li><a href="javascript:void(0);" onclick="Javascript: window.history.go(-1);">返 回 &gt;&gt;</a></li>
                </ul>
                <div class="hr_10">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
