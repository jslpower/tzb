<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="Enow.TZB.Web.WX.Match.Detail" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <title>赛事详情</title>
    <link rel="stylesheet" href="/wx/css/style.css" type="text/css" media="screen">
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
    <uc1:UserHome ID="UserHome1" Userhometitle="赛事详情" runat="server" />
    <div class="warp">
        <div class="msg_tab" id="n4Tab">
            <div class="TabTitle">
                <ul class="fixed">
                    <li id="n4Tab_Title0" onclick="nTabs('n4Tab',this);" class="active"><a href="javascript:void(0);">
                        赛事信息</a></li>
                    <li id="n4Tab_Title1" onclick="nTabs('n4Tab',this);"><a href="javascript:void(0);">赛事规程</a></li>
                </ul>
            </div>
            <div class="TabContent">
                <div id="n4Tab_Content0">
                    <div class="msg_list">
                        <ul>
                            <li>
                                <label>
                                    赛事名称：</label><asp:Literal ID="ltrMatchName" runat="server"></asp:Literal></li>
                            <li>
                                <label>
                                    主办方：</label><asp:Literal ID="MasterOrganizer" runat="server"></asp:Literal></li>
                            <asp:Literal ID="CoOrganizers" runat="server"></asp:Literal>
                            <asp:Literal ID="Organizer" runat="server"></asp:Literal>
                            <asp:Literal ID="Sponsors" runat="server"></asp:Literal>
                            <li>
                                <label>
                                    报名时间：</label><asp:Literal ID="ltrSignUpTime" runat="server"></asp:Literal></li>
                            <li>
                                <label>
                                    比赛时间：</label><asp:Literal ID="LtrMatchTime" runat="server"></asp:Literal></li>
                            <li style="display: none;">
                                <div class="lie">
                                    <label>
                                        参赛队伍数：</label><asp:Literal ID="SignUpNumber" runat="server"></asp:Literal></div>
                                <div class="lie2">
                                    <label>
                                        参赛队员数：</label><asp:Literal ID="PlayersMax" runat="server"></asp:Literal></div>
                            </li>
                            <li>
                                <div class="lie">
                                    <label>
                                        每队报名人数：</label><asp:Literal ID="LtrPlayerNumber" runat="server"></asp:Literal></div>
                                <div class="lie2">
                                    <label>
                                        足球宝贝数：</label><asp:Literal ID="LtrBayNumber" runat="server"></asp:Literal></div>
                            </li>
                            <li style="display: none;">
                                <div class="lie">
                                    <label>
                                        足球宝贝数：</label><asp:Literal ID="BayMin" runat="server"></asp:Literal>-<asp:Literal
                                            ID="BayMax" runat="server"></asp:Literal></div>
                                <div class="lie2">
                                    <label>
                                        报名年龄：</label><asp:Literal ID="MinAge" runat="server"></asp:Literal>-<asp:Literal
                                            ID="MaxAge" runat="server"></asp:Literal></div>
                            </li>
                            <li>
                                <div class="lie">
                                    <label>
                                        比赛总时间：</label><asp:Literal ID="TotalTime" runat="server"></asp:Literal></div>
                                <div class="lie2">
                                    <label>
                                        中场休息时间：</label><asp:Literal ID="BreakTime" runat="server"></asp:Literal></div>
                            </li>
                             <li>
                           <div class="lie"><label>报名年龄：</label><asp:Literal
                                            ID="MinAge2" runat="server"></asp:Literal>-<asp:Literal ID="MaxAge2" runat="server"></asp:Literal></div>
                          <div class="lie2"> <label>
                                        参赛队伍数：</label><asp:Literal ID="SignUpNumber2" runat="server"></asp:Literal></div>
                          </li>
                        </ul>
                    </div>
                    <asp:PlaceHolder ID="phSignUp" runat="server">
                        <div class="mt20 padd_bot">
                            <a href="SignUp.aspx?Id=<%=Request.QueryString["Id"] %>" class="basic_btn">报 名</a></div>
                    </asp:PlaceHolder>
                </div>
                <div id="n4Tab_Content1" class="none">
                    <!--
            <div class="guize">
                 <asp:Repeater ID="rptFieldList" runat="server">
                 <ItemTemplate><%#Eval("FieldName")%> 地址：<%#Eval("FieldAddress")%><br /></ItemTemplate>
                 </asp:Repeater>
              </div>-->
                    <div class="guize">
                        <asp:Literal ID="Remark" runat="server"></asp:Literal>
                    </div>
                    <asp:PlaceHolder ID="phSignUp1" runat="server">
                        <div class="mt20 padd_bot">
                            <a href="SignUp.aspx?Id=<%=Request.QueryString["Id"] %>" class="basic_btn">报 名</a></div>
                    </asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
