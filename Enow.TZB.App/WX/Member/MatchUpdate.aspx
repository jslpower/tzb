<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatchUpdate.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.MatchUpdate" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
<title>赛事报名</title>
<link rel="stylesheet" href="/wx/css/style.css" type="text/css" media="screen">
<script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
<script type="text/javascript">
    function nTabs(tabObj, obj) {
        var tabList = document.getElementById(tabObj).getElementsByTagName("li");
        for (i = 0; i < tabList.length; i++) {
            if (tabList[i].id == obj.id) {
                document.getElementById(tabObj + "_Title" + i).className = "active";
                document.getElementById(tabObj + "_Content" + i).style.display = "block";
            } else {
                document.getElementById(tabObj + "_Title" + i).className = "normal";
                document.getElementById(tabObj + "_Content" + i).style.display = "none";
            }
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="赛事报名" runat="server" />
<div class="warp">
  <div class="msg_tab"  id="n4Tab">
     
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0" onClick="nTabs('n4Tab',this);" class="active"><a href="javascript:void(0);">主办方</a></li>
              <li id="n4Tab_Title1" onClick="nTabs('n4Tab',this);"><a href="javascript:void(0);">赛事规程</a></li>
           </ul>
        </div>

        <div class="TabContent">
        
			<div id="n4Tab_Content0">
            
              <div class="msg_list">
                 <ul>
                       <li><label>主办方：</label><asp:Literal ID="MasterOrganizer" runat="server"></asp:Literal></li>
                       <li><label>协办方：</label><asp:Literal ID="CoOrganizers" runat="server"></asp:Literal></li>
                       <li><label>承办方：</label><asp:Literal ID="Organizer" runat="server"></asp:Literal></li>
                       <li><label>赞助方：</label><asp:Literal ID="Sponsors" runat="server"></asp:Literal></li>
                       <li><label>报名日期：</label><asp:Literal ID="ltrSignUpTime" runat="server"></asp:Literal></li>
                       <li><label>参赛队伍数：</label><asp:Literal ID="SignUpNumber" runat="server"></asp:Literal></li>
                       <li>
                          <div class="lie"><label>足球宝贝数：</label><asp:Literal ID="BayMin" runat="server"></asp:Literal>-<asp:Literal ID="BayMax" runat="server"></asp:Literal></div>
                          <div class="lie2"><label>报名年龄：</label><asp:Literal ID="MinAge" runat="server"></asp:Literal>-<asp:Literal ID="MaxAge" runat="server"></asp:Literal></div>
                       </li>
                       <li>
                       <div class="lie"><label>比赛总时间：</label><asp:Literal ID="TotalTime" runat="server"></asp:Literal></div>
                       <div class="lie2"><label>中场休息时间：</label><asp:Literal ID="BreakTime" runat="server"></asp:Literal></div>
                       </li>
                       <li><label>报名数：</label><asp:Literal ID="ltrTeamNumber" runat="server"></asp:Literal></li>
                       <li><label>比赛球场：</label><asp:DropDownList ID="ddlFieldId" runat="server">
                           </asp:DropDownList>
                       </li>
                       <li><label>球场地址：</label><span id="spanFieldAddress"><asp:Literal ID="ltrFieldAddress" runat="server"></asp:Literal></span></li>
                 </ul>
              </div>
              <div class="qiu_list player_list mt10">
          <ul>
      <asp:Repeater ID= "rptList" runat="server" onitemdatabound="InitOperation">
      <ItemTemplate>
      <li>
            <div class="item-img"><img src="<%#Eval("HeadPhoto")%>" width="80" height="111" /></div>
            <div class="item-box">
                <div class="Rbtn padd01">
                <asp:Literal ID="ltrOperation" runat="server"></asp:Literal>
                </div>
                <dl>
                    <dt><%#Eval("ContactName")%></dt>
                    <dd><span class="<%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))==Enow.TZB.Model.EnumType.球员角色.队长?"bg_red":"bg_green" %>"><%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))%></span></dd>
                    <dd><%#Eval("DNQYHM")%>号</dd>
                    <dd><%#Eval("DNWZ")%></dd>
                    <dd><%#Eval("IssueTime","{0:yyyy-MM-dd}")%></dd>
                </dl>
            </div>
         </li>
      </ItemTemplate>
      </asp:Repeater>
          </ul>
      </div>
            
            </div>
 			<div id="n4Tab_Content1" class="none">
            
              <div class="guize">
                 <asp:Literal ID="Remark" runat="server"></asp:Literal>
              </div>
            </div>
            <div class="mt20 padd_bot"><a href="javascript:void()" id="btnSave" class="basic_btn">保  存</a></div>
           
    </div>
  </div>
</div><input type="hidden" id="hidPlayerNumber" name="hidPlayerNumber" value="<%=hidPalyerNumber %>" /><input type="hidden" id="hidBabyNumber" name="hidBabyNumber" value="<%=hidBabyNumber %>" />
    </form>
<script type="text/javascript">
    var PageJsDataObj = {
        Query: {/*URL参数对象*/
            OpenId: ''
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
                url: '/WX/Team',
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
            $(".join").click(function () {
                var V = $(this).closest("div").find("#hidTMId").val();
                if (V == "0") {//未加入
                    //判断是否已加入,已加入移除，未加入添加
                    $(this).text("已参加");
                    $(this).attr("class", "join");
                    //存入input
                    var TeamMemberId = $(this).attr("dataId");
                    $(this).closest("div").find("#hidTMId").val(TeamMemberId);
                    $("#spanSignUpNumber").text(parseInt($("#spanSignUpNumber").text()) + 1);
                    var TeamMemberRoleType = $(this).attr("dataroletype");
                    if(TeamMemberRoleType == <%=(int)Enow.TZB.Model.EnumType.球员角色.队员 %>){
                            $("#hidPlayerNumber").val(parseInt($("#hidPlayerNumber").val())+1);
                        }else{
                            $("#hidBabyNumber").val(parseInt($("#hidBabyNumber").val())+1);
                        }
                } else {
                    $(this).text("未参加");
                    $(this).attr("class", "gray join");
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
            $(".basic_btn").click(function () {
            //判断是否选择球场
                var FieldId = $("#<%=ddlFieldId.ClientID %>").find("option:selected").val();
                if(FieldId=="0"){
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
                }else if(p<<%=PlayersMin %> || p><%=PlayersMax %>)
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
                    url: 'MatchUpdate.aspx?dotype=doconfirm&Id=<%=Request.QueryString["Id"] %>',
                    dataType: "json",
                    success: function (r) {
                        alert(r.msg);
                        if (r.ret == "1") {
                            window.location.href = "/WX/Member/Default.aspx";
                        }
                        else {
                            PageJsDataObj.BindBtn();
                        }
                    },
                    error: function () {
                        alert("异常请重新提交！");
                        PageJsDataObj.BindBtn();
                    }
                });               
                obj.html("<s class=baochun></s>  确  认");
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
</body>
</html>