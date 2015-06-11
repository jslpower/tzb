<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master"
    AutoEventWireup="true" CodeBehind="MyMatch.aspx.cs" Inherits="Enow.TZB.WebForm.My.MyMatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
    <div class="user_box">
        <h3>
            我的比赛</h3>
                <div class="game_list">
                    <div class="game_list">
                        <ul>
                        <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                         <li>
                         <div class="game_img">
                                <a href="../Match/Detail.aspx?id=<%#Eval("id")%>">
                                    <img src="<%#Eval("MatchPhoto")%>" width="211" height="131"></a></a></div>
                            <div class="Rbox">
                                <div class="Rbox_T">
                                    <a href="../Match/Detail.aspx?id=<%#Eval("id")%>"><%#Eval("MatchName") %></a></div>
                                <div class="Rcont">
                                    <ul>
                                        <li>报名时间：<%#Eval("SignBeginDate","{0:yyyy年MM月dd日}") %> 至 <%#Eval("SignEndDate","{0:yyyy年MM月dd日}") %></li>
                                        <li>比赛时间：<%#Eval("BeginDate","{0:yyyy年MM月dd日}") %> 至 <%#Eval("EndDate","{0:yyyy年MM月dd日}") %></li>
                                        <li class="w50">举办城市：<%#Eval("CountryName") %>-<%#Eval("ProvinceName") %>-<%#Eval("CityName") %>-<%#Eval("AreaName") %></li>
                                        <li class="w50"></li>
                                        <li class="w50">赛事保证金：<%#Eval("RegistrationFee","{0:C2}")%> 元</li>
                                        <li class="w50">报名费：<%#Eval("EarnestMoney", "{0:C2}")%> 元</li>
                                        <li class="w50">报名人数：<%#Eval("JoinNumber")%>人</li>
                                        <li class="w50"></li>
                                    </ul>
                                </div>
                                <div class="Rbtn"><a  href="../Match/Detail.aspx?id=<%#Eval("MatchId")%>" class="greenbg">查看详情</a><%#UpdateOpt(Eval("Id").ToString(), (Enow.TZB.Model.EnumType.参赛审核状态)Convert.ToInt32(Eval("State")), (Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType")))%></div>
                            </div>
                            </li>
                        </ItemTemplate>
                        </asp:Repeater>
                           
                        </ul>
                    </div>
                </div>
    </div>
</asp:Content>
