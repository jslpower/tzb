<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamInfo.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.TeamInfo" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>球队信息</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript">
        $(function () {

//            $("span[flag='teamrole']").each(function () {
//                if ($(this).text() == '队长') {
//                    $(this).addClass("bg_red");
//                } 
//                if ($(this).text() == '队员') {
//                    $(this).removeClass("bg_red").addClass("bg_green");
//                } 
//                if ($(this).text() == '足球宝贝') {
//                    $(this).removeClass("bg_red").addClass("bg_zi");
//                }
//               

//            })


        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <uc1:UserHome ID="UserHome1" runat="server" />
    <div class="warp">
      <div class="msg_tab" id="n4Tab">
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0" ><a href="MyTeamInfo.aspx<%=dhurl %>">介绍</a></li>
              <li id="n4Tab_Title1" class="active"><a href="TeamInfo.aspx<%=dhurl %>">队员</a></li>
              <li id="n4Tab_Title2"><a href="/WX/Member/TeamArticles.aspx<%=dhurl %>">日志</a></li>
           </ul>
        </div>
       <div class="TabContent">
        <div id="n4Tab_Content1" >
             
          <div class="qiu_list player_list mt10">
 
            <ul>
                <asp:Repeater ID="rptList" runat="server" OnItemDataBound="InitOperation">
                    <ItemTemplate>

                      <li>
                            <div class="item-img"><img src="<%#Eval("HeadPhoto")%>"/></div>
                            <div class="item-box">
                               
                                <asp:Literal ID="ltrOperation" runat="server"></asp:Literal>
                                <dl>
                                    <dt> <%#Eval("ContactName")%></dt>
                                    <dd><span flag="teamrole" class="bg_red"><%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))%></span></dd>
                                    <dd>
                                        <%#Eval("DNQYHM")%>号 <%#Eval("DNWZ")%></dd>
                                        <dd>
                                        <%#Eval("IssueTime","{0:yyyy-MM-dd}")%></dd>
                                   <%-- <dd class="txt"> 
                                    <%#Eval("JRYS")%>
                                  
                                    </dd>--%>
                                </dl>
                            </div>
                         </li>

                     
                    </ItemTemplate>
                </asp:Repeater>
             </ul>
            </div>
            <div class="foot_fixed">
                       <div class="foot_box">
                          <div class="paddL10 paddR10"><asp:Literal ID="ltrSignOut" runat="server"></asp:Literal></div>
                       </div>
            </div>
                  
        </div>
        <%--<div class="mt20 padd_bot">
            <asp:Literal ID="ltrSignOut" runat="server"></asp:Literal></div>
        <div class="mt20 padd_bot">
            <asp:HyperLink ID="hyModifyInfo" CssClass="basic_btn" NavigateUrl="TeamUpdate.aspx"
                Visible="false" runat="server"></asp:HyperLink></div>--%>
     </div>
     </div>
    </div>
    
    </form>
</body>
</html>
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
        Check: function (MemberId) {
            var data = this.DataBoxy();
            data.url += '/MemberCheck.aspx?';
            //data.title = '队员审核';
            data.url += $.param({
                MemberId: MemberId,
                doType: "Check"
            });
            this.ShowBoxy(data);
        },
        Update: function (MemberId) {
            var data = this.DataBoxy();
            data.url += '/MemberUpdate.aspx?';
            //data.title = '修改信息';
            data.url += $.param({
                MemberId: MemberId,
                doType: "Update"
            });
            this.ShowBoxy(data);
        },
        KickedOut: function (MemberId) {
            var data = this.DataBoxy();
            data.url += '/MemberKickedOut.aspx?';
            //data.title = '踢出球队';
            data.url += $.param({
                MemberId: MemberId,
                doType: "Update"
            });
            this.ShowBoxy(data);
        },
        ExistCheck: function (MemberId) {
            var data = this.DataBoxy();
            data.url += '/MemberExistCheck.aspx?';
            //data.title = '退出审核';
            data.url += $.param({
                MemberId: MemberId,
                doType: "Update"
            });
            this.ShowBoxy(data);
        },
       
        BindBtn: function () {
            //审核
            $(".MemberCheck").click(function () {
                var MemberId = $(this).attr("DataId");
                PageJsDataObj.Check(MemberId);
                return false;
            });
            //修改
            $(".MemberUpate").click(function () {
                var MemberId = $(this).attr("DataId");
                PageJsDataObj.Update(MemberId);
                return false;
            });
            //踢出
            $(".MemberKickedOut").click(function () {
                var MemberId = $(this).attr("DataId");
                PageJsDataObj.KickedOut(MemberId);
                return false;
            });
            //退出审核
            $(".ExistCheck").click(function () {
                var MemberId = $(this).attr("DataId");
                PageJsDataObj.ExistCheck(MemberId);
                return false;
            })
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
