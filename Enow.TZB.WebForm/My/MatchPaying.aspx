<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MemberMaster.Master"
    CodeBehind="MatchPaying.aspx.cs" Inherits="Enow.TZB.WebForm.My.MatchPaying" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
    <div class="user_box">
        <h3>
            赛事报名修改</h3>
        <div class="game_list">
            <ul>
                <li>
                    <div class="game_img">
                        <img src="images/game-img01.gif" width="211" height="131"></div>
                    <div class="Rbox">
                        <div class="Rbox_T">
                            <asp:Literal ID="ltrMatchName" runat="server"></asp:Literal></div>
                        <div class="Rcont">
                            <ul>
                                <li>报名时间：<asp:Literal ID="ltrSignDate" runat="server"></asp:Literal></li>
                                <li>比赛时间：<asp:Literal ID="ltrMatchDate" runat="server"></asp:Literal></li>
                                <li class="w50">举办城市：<asp:Literal ID="ltrMatchArea" runat="server"></asp:Literal></li>
                                <li class="w50"></li>
                                <li class="w50 font14 fontred">赛事保证金：<asp:Literal ID="ltrDepositMoney" runat="server"></asp:Literal></li><li
                                    class="w50 font14 fontred">报名费：<asp:Literal ID="ltrEarnestMoney" runat="server"></asp:Literal>
                                    元</li>
                                <li class="w50">报名队数：<asp:Literal ID="ltrSignTeamCount" runat="server"></asp:Literal></li>
                                <li class="w50"></li>
                            </ul>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
        <div class="reg_btn cent mt20">
            <asp:Button ID="btnAliPay" runat="server" Text="支付宝支付" OnClick="btnAliPay_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:PlaceHolder ID="phHide" runat="server" Visible="false">
                <asp:Button ID="btnOffLinePay" runat="server" Text="线下支付" OnClick="btnOffLinePay_Click" /></asp:PlaceHolder>
        </div>
        <div class="game_cont">
            <p class="pb10">
                <strong>主办方</strong></p>
            <ul class="fixed">
                <li class="wid50">主办方：<asp:Literal ID="ltrMasterOrganizer" runat="server"></asp:Literal></li>
                <li class="wid50">协办方：<asp:Literal ID="ltrCoOrganizers" runat="server"></asp:Literal></li>
                <li class="wid50">承办方：<asp:Literal ID="ltrOrganizer" runat="server"></asp:Literal></li>
                <li class="wid50">赞助方：<asp:Literal ID="ltrSponsors" runat="server"></asp:Literal></li>
                <li class="wid25">每队报名人数：<asp:Literal ID="ltrSignUpNumber" runat="server"></asp:Literal></li>
                <li class="wid25">足球宝贝数：<asp:Literal ID="ltrBayNumber" runat="server"></asp:Literal></li>
                <li class="wid25">比赛总时间：<asp:Literal ID="ltrTotalTime" runat="server"></asp:Literal></li>
                <li class="wid25">中场休息时间：<asp:Literal ID="ltrBreakTime" runat="server"></asp:Literal></li>
            </ul>
            <p class="mt20 pb10">
                <strong>参赛球场</strong></p>
            <ul class="fixed">
                <li>比赛球场：<asp:Literal ID="ltrFieldName" runat="server"></asp:Literal></li>
                <li>球场地址：<span id="spanFieldAddress"><asp:Literal ID="ltrFieldAddress" runat="server"></asp:Literal></span></li>
                <li>
                    <label>
                        报名数：</label><asp:Literal ID="ltrTeamNumber" runat="server"></asp:Literal></li>
            </ul>
            <p class="mt20 pb10">
                <strong>球队成员</strong></p>
            <div class="game_bmbox">
                <ul>
                    <asp:Repeater ID="rptList" runat="server" OnItemDataBound="InitOperation">
                        <ItemTemplate>
                            <li class="bg">
                                <div class="game_Img">
                                    <asp:Literal ID="ltrPhoto" runat="server"></asp:Literal>
                                </div>
                                <div class="game_R">
                                    <asp:Literal ID="ltrOperation" runat="server"></asp:Literal><input type="hidden"
                                        id="hidTMId" name="hidTMId" value="0" /></div>
                                <div class="game_L">
                                    <p>
                                        <strong>
                                            <%#Eval("PlayerPosition")%>：<%#Eval("ContactName")%></strong></p>
                                    <p>
                                        <span class="<%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))==Enow.TZB.Model.EnumType.球员角色.队长?"bg_red":"bg_green" %>">
                                            <%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))%></span></p>
                                    <p>
                                        <%#Eval("JerseyNumber")%>号</p>
                                </div>
                            </li>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <li>
                                <div class="game_Img">
                                    <asp:Literal ID="ltrPhoto" runat="server"></asp:Literal>
                                </div>
                                <div class="game_L">
                                    <p>
                                        <strong>
                                            <%#Eval("PlayerPosition")%>：<%#Eval("ContactName")%></strong></p>
                                    <p>
                                        <span class="<%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))==Enow.TZB.Model.EnumType.球员角色.队长?"bg_red":"bg_green" %>">
                                            <%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))%></span></p>
                                    <p>
                                        <%#Eval("JerseyNumber")%>号</p>
                                </div>
                            </li>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
