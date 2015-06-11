<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.WebForm.Team.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cph_Left" runat="server">
    <h3>
        一起玩吧</h3>
    <div class="left_nav">
        <ul>
            <li><a href="Default.aspx" class="on">铁丝球队</a></li>
            <li><a href="#" class="wait">铁丝约战</a></li>
            <li><a href="/Match/Default.aspx">杯赛联赛</a></li>
            <li><a href="#" class="wait">铁丝聚会</a></li>
            <li><a href="#" class="wait">足球旅游</a></li>
            <li><a href="#" class="wait">相聚球星</a></li>
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
                首页 > 一起玩吧 > 铁丝球队</div>
        </div>
        <div class="game_list">
            <ul>
                <asp:Repeater ID="rptList" runat="server" OnItemDataBound="InitOperation">
                    <ItemTemplate>
                        <li>
                            <div class="game_img">
                                <a href="SignUp.aspx?id=<%#Eval("id")%>">
                                    <img src="<%#Enow.TZB.Utility.PhotoThumbnail.F1(Eval("TeamPhoto").ToString(),211,131,DIRPATH)%>" width="211" height="131"></a></div>
                            <div class="Rbox">
                                <div class="Rbox_T">
                                    <a href="SignUp.aspx?id=<%#Eval("id")%>"><%#Eval("TeamName") %></a></div>
                                <div class="Rcont">
                                    <ul>
                                        <li class="date w50">创建时间：<%#Eval("IssueTime","{0:yyyy年MM月dd日}") %></li>
                                        <li class="date w50">创始人：<%#Eval("MemberName")%></li>
                                        <li class="contxt"><%#Eval("TeamInfo") %></li>
                                    </ul>
                                </div>
                                <asp:PlaceHolder ID="PhCreateJoin" runat="server">
                                <div class="Rbtn">
                                    <a href="SignUp.aspx?id=<%#Eval("id")%>" class="greenbg">申请加入</a> <a href="CreateTeam.aspx" class="yellowbg">
                                        创建球队</a>
                                </div>
                                </asp:PlaceHolder>
                                
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
