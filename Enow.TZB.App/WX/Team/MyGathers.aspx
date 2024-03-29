﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyGathers.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.MyGathers" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>我的约战</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/juhui.css" type="text/css" media="screen"/>
<script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        var PageJsDataObj = {
            Query: {/*URL参数对象*/
                sl: ''
            },
            GoAjax: function (url) {
                $.ajax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function (result) {
                        if (result.result == "1") {
                            //tableToolbar._showMsg(result.msg, function () {
                            alert(result.msg);
                            window.location.href = "GathersGoing.aspx";
                            // });
                        }
                        else { alert(result.msg) }
                    },
                    error: function () {
                        //ajax异常--你懂得
                        //tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            DataBoxy: function () {/*弹窗默认参数*/
                return {
                    url: '/WX/Team',
                    title: "",
                    width: "830px",
                    height: "620px"
                }
            },
            AcceptWar: function (recordid) {
                if (window.confirm("您确定应战吗？")) {

                    var data = this.DataBoxy();
                    data.url += '/MyGathers.aspx?';

                    data.url += $.param({
                        sl: this.Query.sl,
                        doType: "war",
                        id: recordid
                    });

                }
                this.GoAjax(data.url);

            }




        }
    </script>
<script type="text/javascript">
   
</script>

</head>
<body>
    <uc1:UserHome ID="UserHome1" Userhometitle="我的约战" runat="server" />
    <form id="form1" runat="server">
       <div class="search_head juhui_search">
   <div class="search_form">
       <span class="city_name" onclick="location.href='city.html'">杭州</span>
       <asp:TextBox ID="txtTeamName" CssClass="input_txt floatL" placeholder="球队搜索"  runat="server"></asp:TextBox>
       <asp:Button ID="btnSerch" runat="server" 
           CssClass="input_btn icon_search_i floatR"  Text="" 
           onclick="btnSerch_Click" />


   </div>
</div>

<div class="warp s_warp">

  <div class="msg_tab"  id="n4Tab">
     
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0"  class="active"><a href="MyGathers.aspx">约战中</a></li>
              <li id="n4Tab_Title1" ><a href="GathersGoing.aspx">进行中</a></li>
              <li id="n4Tab_Title2" ><a href="GathersResult.aspx">战报</a></li>
           </ul>
        </div>

        <div class="TabContent">
        
			<div id="n4Tab_Content0">
            
                   <div class="juhui_list u_yuezhan mt10">


                      <ul>
                        <asp:Repeater ID="rptList" runat="server" >
                        <ItemTemplate>
                         <li>
                            <div class="juhui-title"><span class="green">发起的约战</span>向<em><%#Eval("AcceptTeamName")%></em>发起的约战</div>
                            <div class="juhui-box">
                                <div class="btn btn01">
                                   <a href="GathersDetail.aspx?do=request&gatherid=<%#Eval("Id") %>" class="basic_btn">查看</a>
                                </div>
                                <p class="font_gray">时间：<%#Eval("WarTime", "{0:yyyy-MM-dd}")%></p>
                                <p>地点：<%#Eval("WarPlace")%></p>
                                <p class="font_gray"><span class="paddR10">赛制：<%#Eval("MatchType")%>人</span> 费用：<%#(Enow.TZB.Model.EnumType.GathersEnum.比赛费用)Convert.ToInt32(Eval("MatchFee"))%></p>
                            </div>
                         </li>
                       </ItemTemplate>
                       </asp:Repeater>
                       </ul>
                       <ul>
                        <asp:Repeater ID="rptAcceptList" runat="server">
                        <ItemTemplate>

                         <li>
                            <div class="juhui-title"><span class="red">收到的约战</span><em><%#Eval("TeamName")%></em>向您发起的约战</div>
                            <div class="juhui-box">
                                <div class="btn">
                                   <a href="GathersDetail.aspx?do=response&gatherid=<%#Eval("Id") %>" class="basic_btn">查看</a>
                                   <a href="#" onclick="PageJsDataObj.AcceptWar('<%#Eval("Id")%>')"  class="basic_ybtn">应战</a>
                                </div>
                                <p>地点：<%#Eval("WarPlace")%></p>
                                <p class="font_gray">时间：<%#Eval("WarTime", "{0:yyyy-MM-dd}")%></p>
                                <p>地点：<%#Eval("WarPlace")%></p>
                                <p>球场：<%#Eval("WarPlace")%></p>
                                <p class="font_gray"><span class="paddR10">赛制：<%#Eval("MatchType")%>人</span> 费用：<%#(Enow.TZB.Model.EnumType.GathersEnum.比赛费用)Convert.ToInt32(Eval("MatchFee"))%></p>
                            </div>
                         </li>
                         
                          </ItemTemplate>
                       </asp:Repeater>
                         
                        

                      </ul>
                   </div>
                   
                   <div class="foot_fixed">
                       <div class="foot_box">
                          <div class="paddL10 paddR10"><a href="Default.aspx" class="basic_btn">发布约战</a></div>
                       </div>
                   </div>
                   
            
            </div>
            

			<%--<div id="n4Tab_Content1" class="none">
            
            </div>
            

			<div id="n4Tab_Content2" class="none">
            
            </div>--%>
            
            
        </div>
   
  </div>
 

</div>

    </form>
</body>
</html>
