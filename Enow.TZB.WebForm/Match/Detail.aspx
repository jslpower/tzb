<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true"
    CodeBehind="Detail.aspx.cs" Inherits="Enow.TZB.WebForm.Match.Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cph_Left" runat="server">
    <h3>
        一起玩吧</h3>
    <div class="left_nav">
        <ul>
            <li><a href="/Team/Default.aspx">铁丝球队</a></li>
            <li><a href="#" class="wait">铁丝约战</a></li>
            <li><a href="Default.aspx" class="on">杯赛联赛</a></li>
            <li><a href="#" class="wait">铁丝聚会</a></li>
            <li><a href="#" class="wait">足球旅游</a></li>
            <li><a href="#" class="wait">相聚球星</a></li>
            <li><a href="#" class="wait">投票抽奖</a></li>
            <li><a href="#" class="wait">舵主风采</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cph_Body" runat="server">
    <div class="game_box">
        <div class="sideT fixed">
            <h3>
                一起玩吧</h3>
            <div class="Rtit">
                首页 > 一起玩吧 > 杯赛联赛</div>
        </div>
        <div class="game_list">
            <ul>
                <li>
                    <div class="game_img">
                        <asp:Literal ID="LtrMatchPhoto" runat="server"></asp:Literal></div>
                    <div class="Rbox">
                        <div class="Rbox_T">
                            <asp:Literal ID="ltrMatchName" runat="server"></asp:Literal></div>
                        <div class="Rcont">
                            <ul>
                                <li>报名时间：<asp:Literal ID="ltrSignDate" runat="server"></asp:Literal></li>
                                <li>比赛时间：<asp:Literal ID="ltrMatchDate" runat="server"></asp:Literal></li>
                                <li class="w50">举办城市：<asp:Literal ID="ltrMatchArea" runat="server"></asp:Literal></li>
                                <li class="w50"></li>
                                <li class="w50">赛事保证金：<asp:Literal ID="ltrDepositMoney" runat="server"></asp:Literal></li>
                                <li class="w50">报名费：<asp:Literal ID="ltrEarnestMoney" runat="server"></asp:Literal></li>
                            </ul>
                        </div>
                        <asp:PlaceHolder ID="PhSignUp" runat="server">
                            <div class="Rbtn">
                                <a href="SignUp.aspx?id=<%=Enow.TZB.Utility.Utils.GetQueryStringValue("id") %>" class="yellowbg">
                                    赛事报名</a>
                            </div>
                        </asp:PlaceHolder>
                    </div>
                </li>
            </ul>
        </div>
        <div class="game_Tab mt10" id="n4Tab3">
            <div class="game_Tabtitle">
                <ul class="fixed">
                    <li id="n4Tab3_Title0" onclick="nTabs('n4Tab3',this);" class="active"><a href="javascript:void(0);">
                        赛事信息</a></li>
                    <li id="n4Tab3_Title1" onclick="nTabs('n4Tab3',this);"><a href="javascript:void(0);">
                        赛事规程</a></li>
                    <li id="n4Tab3_Title2" onclick="nTabs('n4Tab3',this);"><a href="javascript:void(0);">
                        赛事战报</a></li>
                </ul>
            </div>
            <div class="game_TabContent">
                <div id="n4Tab3_Content0">
                    <div class="game_cont">
                        <p class="mt20 pb10">
                            <strong>主办方</strong></p>
                        <ul class="fixed">
                            <li class="wid50">主办方：<asp:Literal ID="ltrMasterOrganizer" runat="server"></asp:Literal></li>
                            <li class="wid50"><asp:Literal ID="ltrCoOrganizers" runat="server"></asp:Literal></li>
                            <li class="wid50"><asp:Literal ID="ltrOrganizer" runat="server"></asp:Literal></li>
                            <li class="wid50"><asp:Literal ID="ltrSponsors" runat="server"></asp:Literal></li>
                            <li class="wid25">每队报名人数：<asp:Literal ID="ltrSignUpNumber" runat="server"></asp:Literal></li>
                            <li class="wid25">足球宝贝数：<asp:Literal ID="ltrBayNumber" runat="server"></asp:Literal></li>
                            <li class="wid25">比赛总时间：<asp:Literal ID="ltrTotalTime" runat="server"></asp:Literal></li>
                            <li class="wid25">中场休息时间：<asp:Literal ID="ltrBreakTime" runat="server"></asp:Literal></li>
                        </ul>
                        <p class="mt20 pb10">
                            <strong>参赛球队</strong></p>
                        <div class="game_list">
                            <ul>
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <div class="game_img">
                                                <a href="SignUp.aspx?id=<%#Eval("id")%>">
                                                    <img src="<%#Enow.TZB.Utility.PhotoThumbnail.F1(Eval("TeamPhoto").ToString(),211,131,DIRPATH)%>" width="211" height="131"></a></div>
                                            <div class="Rbox">
                                                <div class="Rbox_T">
                                                    <a href="SignUp.aspx?id=<%#Eval("id")%>">
                                                        <%#Eval("TeamName") %></a></div>
                                                <div class="Rcont">
                                                    <ul>
                                                        <li class="date w50">创建时间：<%#Eval("IssueTime","{0:yyyy年MM月dd日}") %></li>
                                                        <li class="date w50">创始人：<%#Eval("MemberName")%></li>
                                                        <li class="contxt">
                                                            <%#Eval("TeamInfo") %></li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
                <div id="n4Tab3_Content1" class="none">
                    <div class="game_txt">
                        <p>
                            <asp:Literal ID="ltrRemark" runat="server"></asp:Literal>
                        </p>
                    </div>
                </div>
                <div id="n4Tab3_Content2" class="none">
                    <asp:Repeater ID="rptGameList" runat="server" OnItemDataBound="InitMatchSchedule">
                        <ItemTemplate>
                            <div class="game_zb">
                                <div class="th_first th_bg0<%#Container.ItemIndex+1 %>">
                                    <%#Eval("GameName") %></div>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th width="100">
                                            &nbsp;
                                        </th>
                                        <th>
                                            时间
                                        </th>
                                        <th>
                                            编号
                                        </th>
                                        <th>
                                            对阵
                                        </th>
                                        <th>
                                            比赛场地
                                        </th>
                                        <th>
                                            其他
                                        </th>
                                    </tr>
                                    <asp:Repeater ID="rptScheduleList" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="td_coll">
                                                    <%#Eval("OrdinalNumber")%>场
                                                </td>
                                                <td class="td_coll">
                                                    <%#Eval("GameStartTime", "{0:MM月dd日 HH:mm}")%>-<%#Eval("GameEndTime", "{0:HH:mm}")%>
                                                </td>
                                                <td>
                                                    <%#Eval("HomeMatchCode")%>-<%#Eval("AwayMatchCode")%>
                                                </td>
                                                <td>
                                                    <%#MatchTeamScheduleView(Convert.ToString(Eval("Id")), Convert.ToString(Eval("HomeTeamName")), Eval("HomeMatchCode").ToString(), Convert.ToBoolean(Eval("HomeIsBallot")), Convert.ToString(Eval("AwayTeamName")), Convert.ToString(Eval("AwayMatchCode")), Convert.ToBoolean(Eval("AwayIsBallot")), Convert.ToInt32(Eval("PublishState")), Convert.ToInt32(Eval("HomeGoals")), Convert.ToInt32(Eval("AwayGoals")))%>
                                                </td>
                                                <td>
                                                    <%#Eval("FieldName")%>
                                                </td>
                                                <td class="td_collR">
                                                    <%#ResultView(Convert.ToString(Eval("Id")), Convert.ToInt32(Eval("PublishState")))%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function nTabs(tabObj, obj) {
            var tabList = $("#" + tabObj).find("li");
            for (i = 0; i < tabList.length; i++) {
                if (tabList[i].id == obj.id) {
                    $("#" + tabObj + "_Title" + i).attr("class", "active");
                    $("#" + tabObj + "_Content" + i).show();
                } else {
                    $("#" + tabObj + "_Title" + i).attr("class", "normal");
                    $("#" + tabObj + "_Content" + i).hide();
                }
            }
        }
    </script>
</asp:Content>
