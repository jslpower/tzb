<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.WebForm.Match.Default" %>

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
            <li><a href="#" class="wait" >相聚球星</a></li>
            <li><a href="#" class="wait">投票抽奖</a></li>
            <li><a href="#" class="wait">舵主风采</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Cph_Body" runat="server">
    <div class="game_box mt10">
        <div class="sideT fixed">
            <h3>
                一起玩吧</h3>
            <div class="Rtit">
                首页 > 一起玩吧 > 杯赛联赛</div>
        </div>
        <div class="game_list">
            <ul>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <li>
                            <div class="game_img">
                                <a href="Detail.aspx?id=<%#Eval("id")%>">
                                    <img src="<%#Enow.TZB.Utility.PhotoThumbnail.F1(Eval("MatchPhoto").ToString(),211,131,DIRPATH)%>" width="211" height="131"></a></div>
                            <div class="Rbox">
                                <div class="Rbox_T">
                                    <a href="Detail.aspx?id=<%#Eval("id")%>"><%#Eval("MatchName") %></a></div>
                                <div class="Rcont">
                                    <ul>
                                        <li>报名时间：<%#Eval("SignBeginDate","{0:yyyy年MM月dd日}") %> 至 <%#Eval("SignEndDate","{0:yyyy年MM月dd日}") %></li>
                                        <li>比赛时间：<%#Eval("BeginDate","{0:yyyy年MM月dd日}") %> 至 <%#Eval("EndDate","{0:yyyy年MM月dd日}") %></li>
                                        <li class="w50">举办城市：<%#Eval("CountryName") %>-<%#Eval("ProvinceName") %>-<%#Eval("CityName") %>-<%#Eval("AreaName") %></li>
                                        <li class="w50"></li>
                                        <li class="w50">赛事保证金：<%#Eval("RegistrationFee","{0:C2}")%> 元</li>
                                        <li class="w50"><!--报名人数：<%#Eval("PlayersMin")%>-<%#Eval("PlayersMax")%>人--></li>
                                    </ul>
                                </div>
                                <div class="Rbtn">
                                    <a  href="Detail.aspx?id=<%#Eval("id")%>" class="greenbg">查看详情</a> <%#SignUp(Eval("Id").ToString(), Convert.ToDateTime(Eval("SignBeginDate")), Convert.ToDateTime(Eval("SignEndDate")))%>
                                </div>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="page" id="div_fenye">
           
        </div>
    </div>
        <script type="text/javascript" src="/Js/fenye.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var fenYePeiZhi = { pageSize: 4, pageIndex: 1, recordCount: 1, showPrev: true, showNext: true, showDisplayText: false, cssClassName: 'page' };
            fenYePeiZhi.pageSize = parseInt("<%=pageSize %>");
            fenYePeiZhi.pageIndex = parseInt("<%=pageIndex %>");
            fenYePeiZhi.recordCount = parseInt("<%=recordCount %>");

            if (fenYePeiZhi.recordCount > 0) AjaxPageControls.replace("div_fenye", fenYePeiZhi);
        })
    </script>
</asp:Content>
