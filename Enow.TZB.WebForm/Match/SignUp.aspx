<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/WebMaster.Master" CodeBehind="SignUp.aspx.cs" Inherits="Enow.TZB.WebForm.Match.SignUp" %>
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
                                <li class="w50">赛事保证金：<asp:Literal ID="ltrDepositMoney" runat="server"></asp:Literal></li>
                                <li class="w50">报名费：<asp:Literal ID="ltrEarnestMoney" runat="server"></asp:Literal></li>
                            </ul>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
        <div class="game_Tab mt10" id="n4Tab3">
            <div class="game_Tabtitle">
                <ul class="fixed">
                    <li id="n4Tab3_Title0" class="active"><a href="javascript:void(0);">
                        赛事报名</a></li>
                </ul>
            </div>
            <div class="game_TabContent">
                <div id="n4Tab3_Content0">
                    <div class="game_cont">
                        <p class="mt20 pb10">
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
                            <strong>选择球场</strong></p>
                            <ul class="fixed">
                            <li>比赛球场：<asp:DropDownList ID="ddlFieldId" runat="server">
                           </asp:DropDownList></li>
                           <li>球场地址：<span id="spanFieldAddress"><asp:Literal ID="ltrFieldAddress" runat="server"></asp:Literal></span></li>
                           <li><label>报名数：</label><asp:Literal ID="ltrTeamNumber" runat="server"></asp:Literal></li>
                            </ul>
                        <p class="mt20 pb10">
                            <strong>球队成员</strong></p>
<div class="game_bmbox">
       <ul>
       <asp:Repeater ID= "rptList" runat="server" onitemdatabound="InitOperation">
      <ItemTemplate>
      <li class="bg">
                   <div class="game_Img"><asp:Literal ID="ltrPhoto" runat="server"></asp:Literal>
                   </div> 
                   <div class="game_R"><asp:Literal ID="ltrOperation" runat="server"></asp:Literal><input type="hidden" id="hidTMId" name="hidTMId" value="0" /></div>
                   <div class="game_L">
                      <p><strong><%#Eval("DNWZ")%>：<%#Eval("ContactName")%></strong></p>
                      <p><span class="<%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))==Enow.TZB.Model.EnumType.球员角色.队长?"bg_red":"bg_green" %>"><%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))%></span></p>
                      <p><%#Eval("DNQYHM")%>号</p>
                      <p><%#Eval("JRYS")%></p>
                   </div>
                   
                </li>
      </ItemTemplate>
      <AlternatingItemTemplate>
      <li>
                   <div class="game_Img"><asp:Literal ID="ltrPhoto" runat="server"></asp:Literal>
                   </div> 
                   <div class="game_R"><asp:Literal ID="ltrOperation" runat="server"></asp:Literal><input type="hidden" id="hidTMId" name="hidTMId" value="0" /></div>
                   <div class="game_L">
                      <p><strong><%#Eval("DNWZ")%>：<%#Eval("ContactName")%></strong></p>
                      <p><span class="<%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))==Enow.TZB.Model.EnumType.球员角色.队长?"bg_red":"bg_green" %>"><%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))%></span></p>
                      <p><%#Eval("DNQYHM")%>号</p>
                      <p><%#Eval("JRYS")%></p>
                   </div>
                   
                </li>
      </AlternatingItemTemplate>
      </asp:Repeater>
                
       </ul>
       
    </div>
    <input type="hidden" id="hidPlayerNumber" name="hidPlayerNumber" value="1" /><input type="hidden" id="hidBabyNumber" name="hidBabyNumber" value="0" />
   <div class="cent mt20"><a href="javascript:void()" id="btnSave" class="yellow_btn">确  认</a></div>   

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var PageJsDataObj = {
            Query: {/*URL参数对象*/
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
                    url: '/Match',
                    title: "",
                    width: $(window).width() - 10,
                    height: "380px"
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
            BindBtn: function () {
                $("#<%=ddlFieldId.ClientID %>").change(function(){
                    var FieldId = $(this).find("option:selected").val();
                 if(FieldId!="0"){
                 //取得球场信息
                $.ajax({
                    type: "get", 
                    cache: false, 
                    dataType: "json",
                    url: "/Ashx/GetFieldInfo.ashx?Id=" + FieldId,
                    success: function (response) {
                        if (response && response.IsResult == "1") {
                            var address = response.Data.Address;
                            $("#spanFieldAddress").text(address);
                        }
                        else {
                            alert("获取球场信息失败！");
                            return;
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(XMLHttpRequest.status);
                        //alert(XMLHttpRequest.readyState);
                        //alert(textStatus);
                        alert("获取球场信息失败！");
                        return;
                    }
                });  
                }else{$("#spanFieldAddress").text("请选择球场");}     
                });
                //参赛
                $(".join").change(function () {
                    var JoinNumber = parseInt($("#spanSignUpNumber").text());
                    var bischecked=$(this).is(':checked');
                    if(bischecked){
                    //加入
                        var TeamMemberId = $(this).attr("dataId");
                        var TeamMemberRoleType = $(this).attr("dataroletype");
                        $(this).closest("div").find("#hidTMId").val(TeamMemberId);
                        $("#spanSignUpNumber").text(parseInt($("#spanSignUpNumber").text()) + 1);
                        if(TeamMemberRoleType == <%=(int)Enow.TZB.Model.EnumType.球员角色.队员 %>){
                            $("#hidPlayerNumber").val(parseInt($("#hidPlayerNumber").val())+1);
                        }else{
                            $("#hidBabyNumber").val(parseInt($("#hidBabyNumber").val())+1);
                        }
                    }else{
                        $(this).closest("div").find("#hidTMId").val("0");
                        $("#spanSignUpNumber").text(parseInt($("#spanSignUpNumber").text()) - 1);
                        var TeamMemberRoleType = $(this).attr("dataroletype");
                        if(TeamMemberRoleType == <%=(int)Enow.TZB.Model.EnumType.球员角色.队员 %>){
                            $("#hidPlayerNumber").val(parseInt($("#hidPlayerNumber").val())-1);
                        }else{
                            $("#hidBabyNumber").val(parseInt($("#hidBabyNumber").val())-1);
                        }
                    }
                    return false;
                });
                //确认
                $("#btnSave").click(function () {
                //判断是否选择球场
                var FieldId = $("#<%=ddlFieldId.ClientID %>").find("option:selected").val();
                if(FieldId==0){
                    alert("请选择比赛球场!");
                    return false;
                }
                //判断报名人数是否符合规则
                var babyNumber = $("#hidBabyNumber").val();
                var playNumber = $("#hidPlayerNumber").val();
                var p = parseInt(babyNumber) + parseInt(playNumber);
                if(babyNumber<<%=BabysMin %> || babyNumber><%=BabysMax %>)
                {
                    alert("球队宝贝人数只能在<%=BabysMin %>和<%=BabysMax %>之间");
                    return false;
                }else if(playNumber<<%=PlayersMin %> || playNumber><%=PlayersMax %>)
                {
                    alert("球队成员人数只能在<%=PlayersMin %>和<%=PlayersMax %>之间");
                    return false;
                }else{
                    $(this).unbind("click");
                    $(this).css({ "background-position": "0 -57px", "text-decoration": "none" });
                    $(this).html("<s class=baochun></s>  提交中...");
                    $.ajax({
                        type: "post",
                        data: $("#btnSave").closest("form").serialize(),
                        cache: false,
                        async: false,
                        url: 'SignUp.aspx?dotype=doconfirm&Id=<%=Request.QueryString["Id"] %>',
                        dataType: "json",
                        success: function (r) {
                            if (r.ret == "1") {
                                alert("报名成功！");
                                window.location.href = "/";
                            }
                            else {
                                alert(r.msg);
                                PageJsDataObj.BindBtn();
                            }
                        },
                        error: function () {
                            alert("异常请重新提交！");
                            PageJsDataObj.BindBtn();
                        }
                    });
                    $(this).html("<s class=baochun></s>  确  认");
                    }
                });
            },
            PageInit: function () {
                //绑定功能按钮
                this.BindBtn();
            }
        }
    $(function () {
        PageJsDataObj.PageInit();
        PageJsDataObj.BindClose();
        return false;
    })
    </script>
</asp:Content>
