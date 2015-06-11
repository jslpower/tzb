<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GathersTeam.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.GathersTeam" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>约战详情</title>
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
    <uc1:UserHome ID="UserHome1" Userhometitle="约战详情" runat="server" />
    <form id="form1" runat="server">
         <div class="warp">
  <div class="msg_tab Tab_lie2"  id="n4Tab">
     
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0"  ><a href="GathersDetail.aspx?do=<%=d%>&gatherid=<%=GatherId %>">约战详情</a></li>
              <li id="n4Tab_Title1" class="active"><a href="GathersTeam.aspx?do=<%=d%>&gatherid=<%=GatherId %>">球队详情</a></li>
           </ul>
        </div>

        <div class="TabContent">
        
			
 
 			<div id="n4Tab_Content1" >
            
                <div class="juhui_cont padd10">
                   
                   <div>约战球队：<asp:Literal ID="litTeamName" runat="server"></asp:Literal></div>
                   <div class="indent2 font_gray" id="divTeamInfo" runat="server">
                    
                   </div>
                   
                </div>
            
                <div class="qiu_list player_list u_yuezhan_list mt10">

                  <ul>
                   <asp:Repeater ID="rptTeamList" runat="server">
                        <ItemTemplate>
                     <li>
                        <div class="item-img"><img alt="" src="<%#Eval("HeadPhoto")%>"/></div>
                        <div class="item-box">
                            <dl>
                                <dt><%#Eval("ContactName")%></dt>
                                <dd><span class="bg_red"><%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))%></span></dd>
                                 <dd>
                                        <%#Eval("DNQYHM")%>号</dd>
                                         <dd><%#Eval("DNWZ")%> </dd>
                                        <dd>
                                        <%#Eval("IssueTime","{0:yyyy-MM-dd}")%></dd>
                                <dd class="txt">  <%#Eval("JRYS")%></dd>
                            </dl>
                        </div>
                     </li>
                     </ItemTemplate>
                     </asp:Repeater>
                   
                     
                    
                  </ul>
                </div>
                   

               <div class="foot_fixed">
                    <div class="foot_box">
                        <div class="paddL10 paddR10"><a href="#" id="IsAccept" runat="server" onclick="PageJsDataObj.AcceptWar('<%=GatherId %>')"  class="basic_btn">应战</a></div>
                    </div>
               </div>

            
            </div>

			
           
        </div>

     
     
     
  </div>
</div>

    </form>
</body>
</html>
