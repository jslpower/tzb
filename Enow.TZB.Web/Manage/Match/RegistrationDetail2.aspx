<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationDetail2.aspx.cs" Inherits="Enow.TZB.Web.Manage.Match.RegistrationDetail2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <style>
        .qiu_list
        {
            width: 100%;
            overflow: hidden;
        }
        .qiu_list li
        {
            display: block;
            height: 111px;
            padding: 10px;
            overflow: hidden;
            border: #e8e8e8 solid 1px;
            background: #f9f9f9;
            clear: both;
            cursor: pointer;
            margin-bottom: 10px;
        }
        .qiu_list li .item-img
        {
            width: 80px;
            height: 111px;
            float: left;
            overflow: hidden;
        }
        .qiu_list li .item-img img
        {
            width: 80px;
            height: 111px;
        }
        .qiu_list li .item-box
        {
            padding: 0 70px 0 85px;
            position: relative;
        }
        .qiu_list li .item-box .btn
        {
            width: 63px;
            height: 82px;
            padding-top: 27px;
            background: #ffab1a;
            border: #a98300 solid 1px;
            border-radius: 2px;
            display: inline-block;
            position: absolute;
            right: 0;
            top: 0;
            font-size: 18px;
            text-align: center;
            color: #fff;
        }
        .qiu_list li .item-box dl
        {
            color: #191919;
            overflow: hidden;
        }
        .qiu_list li .item-box dd
        {
            line-height: 22px;
        }
        .qiu_list li .item-box dd.date
        {
            color: #a1a1a1;
        }
        .qiu_list li .item-box dd.txt
        {
            height: 66px;
            overflow: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="contentbox">
        <div class="firsttable">
            <span class="firsttableT">报名信息</span>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tableInfo">
                <tr>
                    <th width="80" height="40" align="right">
                        赛事名称：
                    </th>
                    <td>
                        <asp:Label ID="lblMatchName" CssClass="inputtext formsize240" runat="server"></asp:Label>
                    </td>
                    <th width="80" height="40" align="right">
                        举办地点：
                    </th>
                    <td>
                        <asp:Label ID="lblMatchArea" CssClass="inputext formsize240" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="80" height="40" align="right">
                        报名日期:
                    </th>
                    <td align="center">
                        <asp:Label ID="lblSignDate" CssClass="inputtext formsize240" runat="server"></asp:Label>
                    </td>
                    <th width="80" height="40" align="right">
                        比赛日期：
                    </th>
                    <td align="center">
                        <asp:Label ID="lblMatchDate" CssClass="inputtext formsize240" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="80" height="40" align="right">
                        赛事保证金：
                    </th>
                    <td align="center">
                        <asp:Label ID="ltrFee1" CssClass="inputtext formsize240" runat="server"></asp:Label>
                    </td>
                    <th width="80" height="40" align="right">
                        赛事报名费：
                    </th>
                    <td align="center">
                        <asp:Label ID="lblFee" CssClass="inputtext formsize240" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="80" height="40" align="right">
                        主 办 方：
                    </th>
                    <td align="left" bgcolor="#ffffff">
                        <asp:Label ID="lblMaster" CssClass="inputtext formsize240" runat="server"></asp:Label>
                    </td>
                    <th width="80" height="40" align="right">
                        协 办 方：
                    </th>
                    <td align="left" bgcolor="#ffffff">
                        <asp:Label ID="lblCoor" CssClass="inputtext formsize240" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="80" height="40" align="right">
                        承 办 方：
                    </th>
                    <td>
                        <asp:Label ID="lblOrganizer" CssClass="inputtext formsize240" runat="server"></asp:Label>
                    </td>
                    <th width="80" height="40" align="right">
                        赞 助 方：
                    </th>
                    <td>
                        <asp:Label ID="lblSponsors" CssClass="inputtext formsize240" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="80" height="40" align="right">
                        球队名称：
                    </th>
                    <td>
                        <asp:Label ID="lblTeamName" runat="server" CssClass="inputtext formsize240"></asp:Label>
                    </td>
                    <th width="80" height="40" align="right">
                        队 长：
                    </th>
                    <td>
                        <asp:Label ID="lblOnwerName" runat="server" CssClass="inputtext formsize240"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="80" height="40" align="right">
                        参赛人数：
                    </th>
                    <td>
                        <asp:Label ID="lblTeamCount" runat="server" CssClass="inputtext formsize240"></asp:Label>
                    </td>
                       <th width="80" height="40" align="right">
                        参赛球队数：
                    </th>
                    <td>
                        <asp:Label ID="lblSignCount" runat="server" CssClass="inputtext formsize240"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="80" height="40" align="right">
                        参赛球场：
                    </th>
                    <td>
                        <asp:Label ID="ltrFieldName" runat="server" CssClass="inputtext formsize240"></asp:Label>
                    </td>
                       <th width="80" height="40" align="right">
                        球场地址：
                    </th>
                    <td>
                        <asp:Label ID="ltrFieldAddress" runat="server" CssClass="inputtext formsize240"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="qiu_list player_list mt10">
                <ul>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <li>
                                <div class="item-img">
                                    <img src="<%#Eval("HeadPhoto")%>" width="80" height="111" /></div>
                                <div class="item-box">
                                    <dl>
                                        <dt>
                                            <%#Eval("ContactName")%></dt>
                                        <dd>
                                            <span>
                                                <%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))%></span></dd>
                                        <dd>
                                            <%#Eval("PlayerPosition")%></dd>
                                    </dl>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="Basic_btn fixed">
            <ul>
             <li>
                    <asp:LinkButton ID="linkBtnEnabled" runat="server" 
                        onclick="linkBtnEnabled_Click"> 审核通过 >></asp:LinkButton>
                </li>
                <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"
                    hidefocus="true">关 闭 >></a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    </form>
</body>
</html>
