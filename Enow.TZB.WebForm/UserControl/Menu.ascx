<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="Enow.TZB.WebForm.UserControl.Menu" %>
<div class="head fixed" id="div_headMenu">
    <div class="logo floatL">
        <img src="/images/logo.png"></div>
    <div class="headR floatR">
        <div class="head_R01">
            <div class="h_searchbar">
                <input type="text" id="search_input" class="search_input" onkeypress="Search();"
                    value="" />
                <input type="button" id="search_btn" name="search_btn" class="search_btn" value=""
                    onclick="Search();" />
            </div>
        </div>
        <div class="nav">
            <ul>
                <li><a href="/Default.aspx" data-index="0" <%if(HeadMenuIndex==0){ %>
                    <%} %>>首页</a></li>
                <li class="menu_more"><a href="#" data-index="1" <%if(HeadMenuIndex==1){ %> 
                    <%} %>>一起玩吧</a>
                    <div class="menu2" style="margin-left: -86px;">
                        <a href="/Team/Default.aspx">铁丝球队</a> <a href="#" class="wait">铁丝约战</a> <a href="/Match/Default.aspx">
                            杯赛联赛</a> <a href="#" class="wait">铁丝聚会</a> <a href="#" class="wait">足球旅游</a>
                        <a href="#" class="wait">相聚球星</a> <a href="#" class="wait">投票抽奖</a> <a href="/News/DZList.aspx">
                            舵主风采</a>
                    </div>
                </li>
                <li class="menu_more"><a href="#" data-index="2" <%if(HeadMenuIndex==2){ %> class="on"
                    <%} %>>球场大联盟</a>
                    <div class="menu2" style="margin-left: -180px;">
                        <a href="/News/FieldList.aspx">联盟球场</a>
                    </div>
                </li>
                <li class="menu_more"><a href="#" data-index="3" <%if(HeadMenuIndex==3){ %> class="on"
                    <%} %>>铁众享</a>
                    <div class="menu2" style="margin-left: -274px;">
                        <a href="/News/TieShare.aspx">爱上足球</a> <a href="#" class="wait">培训报名</a> <a href="/News/TZList.aspx">
                            堂主风采</a>
                            <a href="/News/TieShare.aspx?ClassId=100">铁子帮爱高</a> 
                    </div>
                </li>
                <li class="menu_more"><a href="#" data-index="4" <%if(HeadMenuIndex==4){ %> class="on"
                    <%} %>>铁公益</a>
                    <div class="menu2" style="margin-left: -368px;">
                        <a href="/News/TieGongYi.aspx">大爱无疆</a> <a href="#" class="wait">公益拍卖</a> <a href="#"
                            class="wait">爱心义卖</a>
                    </div>
                </li>
                <li class="menu_more"><a href="/Notice/Notice.aspx" data-index="5" <%if(HeadMenuIndex==5){ %>
                    class="on" <%} %>>铁子购</a>
                    <div class="menu2" style="margin-left: -462px; display: none;">
                        <a href="#" class="wait">G.U.T</a> <a href="#" class="wait">Adidas</a> <a href="#"
                            class="wait">其他</a>
                    </div>
                </li>
                <li style="display: none;" class="last"><a href="/News/NewsList.aspx?ClassId=21&HmId=6"
                    data-index="6" <%if(HeadMenuIndex==6){ %> class="on" <%} %>>铁文集</a></li>
            </ul>
            <div class="clear">
            </div>
        </div>
    </div>
</div>
<div class="head_line">
</div>
<script type="text/javascript">

    $(document).ready(function () {

        $("#div_headMenu").find("li").each(function () {
            if ($(this).find("a").attr("data-index") == "<%= HeadMenuIndex %>") {
                $(this).addClass("on");
            }
            else {
                $(this).removeClass("on");
            }
        });

        $(".wait").attr("href", "/Notice/Notice.aspx");
    });

    function Search() {
        if ($("#search_input").val() != "") {

            var keyword = $("#search_input").val();
            // window.location.href=;
            window.open("/News/search.aspx?keywords=" + encodeURI(keyword) + "");
        }
    }
 
</script>
