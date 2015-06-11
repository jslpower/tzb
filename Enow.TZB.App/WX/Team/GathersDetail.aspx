<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GathersDetail.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.GathersDetail" %>
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
</head>
<body>
    <uc1:UserHome ID="UserHome1" Userhometitle="约战详情" runat="server" />
    <form id="form1" runat="server">
       <div class="warp">
  <div class="msg_tab Tab_lie2"  id="n4Tab">
     
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0"  class="active"><a href="GathersDetail.aspx?do=<%=d%>&gatherid=<%=GatherId %>">约战详情</a></li>
              <li id="n4Tab_Title1" ><a href="GathersTeam.aspx?do=<%=d%>&gatherid=<%=GatherId %>">球队详情</a></li>
           </ul>
        </div>

        <div class="TabContent">
        
			<div id="n4Tab_Content0">
            
                <div class="juhui_cont padd10">
                    
                   <div>球队：<asp:Literal ID="litTeamName" runat="server"></asp:Literal></div>
                   <div>时间：<asp:Literal ID="litTime" runat="server"></asp:Literal></div>
                   <div>地点：<asp:Literal ID="litPlace" runat="server"></asp:Literal></div>
                   <div><span class="paddR10">赛制：<asp:Literal ID="litType" runat="server"></asp:Literal>人</span>费用：<asp:Literal ID="litFee" runat="server"></asp:Literal></div>
                   <div>战书：</div>
                   <div class="indent2 font_gray"  id="litBook"  runat="server">
                
                   </div>
                   
                </div>
                
                <div class="mt20 padd_bot"><a id="IsAccept" runat="server"  href="#" class="basic_btn" onclick="PageJsDataObj.AcceptWar('<%=GatherId %>')" >应战</a></div>
            
            </div>
 
 

			
           
        </div>

     
     
     
  </div>
</div>
    </form>
</body>
</html>
