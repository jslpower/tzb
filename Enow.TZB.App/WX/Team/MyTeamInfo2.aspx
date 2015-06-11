<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyTeamInfo2.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.MyTeamInfo2" %>
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

    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <link rel="stylesheet" href="/WX/css/tangzhu.css" type="text/css" media="screen"/>
    <script type="text/javascript">
        $(function () {
            $(".log_date a[flag='zan']").click(function () {
                $(this).removeClass("zan").addClass("zan_ok");

            })
        })
    </script>

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
                        url: '/WX/Member',
                        title: "",
                        width: "830px",
                        height: "620px"
                    }
                },
              
                Greet: function (recordid) {
                    if (window.confirm("点赞吗？")) {

                        var data = this.DataBoxy();
                        data.url += '/Articles.aspx?';

                        data.url += $.param({
                            sl: this.Query.sl,
                            doType: "greet",
                            id: recordid
                        });

                    }
                    this.GoAjax(data.url);

                }
              

            }
    </script>
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
            <uc1:UserHome ID="UserHome1" Userhometitle="我的球队" runat="server" />
   <%-- <header class="head">
        <a href="/WX/SmartWeb/"><b class="icon_home"></b></a>
        <h1>
            <asp:Literal ID="ltrTeamName" runat="server"></asp:Literal></h1>
        <a href="javascript:history.back();"><i class="returnico"></i></a>
    </header>--%>
    <div class="warp">
       <%-- <div class="qiu_box">
            <div class="qiu-bigimg">
                <asp:Literal ID="ltrImg" runat="server"></asp:Literal></div>
            <div class="font_gray">
                <span class="mar_R20">&nbsp;&nbsp;&nbsp;&nbsp;创建时间：<asp:Literal ID="ltrCreateDate"
                    runat="server"></asp:Literal></span>
            </div>
            <div class="font_gray">
                <span class="mar_R20">&nbsp;&nbsp;&nbsp;&nbsp;城&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;市：<asp:Literal
                    ID="ltrCity" runat="server"></asp:Literal></span></div>
            <div class="qiu-cont">
                <asp:Literal ID="ltrTeamInfo" runat="server"></asp:Literal>
            </div>
        </div>--%>
      
     
        
    <div class="msg_tab" id="n4Tab">
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0" ><a href="MyTeamInfo.aspx">介绍</a></li>
              <li id="n4Tab_Title1"  class="active"><a href="TeamInfo.aspx">队员</a></li>
              <li id="n4Tab_Title2" ><a href="/WX/Member/TeamArticles.aspx">日志</a></li>
           </ul>
        </div>
        <div class="TabContent">
        <div id="n4Tab_Content0" style="display:none" >
            
                <div class="tangzhu_jianli padd20">
                
                   <h3 class="qiudui_title">
                   <span id="ltrTeamName" runat="server"></span>
                   <em style="display:none;"></em></h3><!--em标签里面是编辑按钮，点击可以编辑标题---->
                   
                   <div class="cent" id="teamimg" runat="server">
                   
                  <%-- <img class="qiudui_xximg" src="images/qiudui-img.jpg"/>--%>
                   </div>
                   
                   <div class="tangzhu_jieshao" id="ltrTeamInfo" runat="server">
                       

                   </div>
                   
               
                   <div class="foot_btn">
                       <ul>
                           <li class="box-siz">
                           <asp:HyperLink ID="hyModifyInfo" NavigateUrl="TeamUpdate.aspx"
                Visible="true" runat="server">修改</asp:HyperLink>
                          
                           </li>
                           <li class="box-siz">
                              <asp:HyperLink ID="hyTransfer" CssClass="basic_ybtn" NavigateUrl="TeamTransfer.aspx"
                Visible="true" runat="server">队长转让</asp:HyperLink>
                        
                           </li>
                       </ul>
                   </div>
                   
                </div>
            
            </div>
        <div id="n4Tab_Content1" >
             
          <div class="qiu_list player_list mt10">
 
            <ul>
                <asp:Repeater ID="rptTeamList" runat="server" OnItemDataBound="InitOperation">
                    <ItemTemplate>

                      <li>
                            <div class="item-img"><img src="<%#Eval("HeadPhoto")%>"/></div>
                            <div class="item-box">
                               
                                <asp:Literal ID="ltrOperation" runat="server"></asp:Literal>
                                <dl>
                                    <dt> <%#Eval("ContactName")%></dt>
                                    <dd><span flag="teamrole" class="bg_red"><%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))%></span></dd>
                                       <dd>
                                        <%#Eval("DNQYHM")%>号</dd>
                                        <dd><%#Eval("DNWZ")%> </dd>
                                        <dd>
                                        <%#Eval("IssueTime","{0:yyyy-MM-dd}")%></dd>
                                    <%--<dd class="txt"> --%>
                                   <%-- <%#Eval("JRYS")%>--%>
                                  
                                   <%-- </dd>--%>
                                </dl>
                            </div>
                         </li>

                     
                    </ItemTemplate>
                </asp:Repeater>
             </ul>
            </div>
            <div id="n4Tab_Content2" class="none">
                  <div class="tangzhu_log qiudui_log">
                <ul>
                    <asp:Repeater ID="rptLogList" runat="server">
                        <ItemTemplate>
                            <li>
                              <div class="log_title" onclick="window.location.href ='/WX/Member/TeamArticleView.aspx?Id=<%#Eval("Id") %>'">
                                    <%#Eval("ArticleTitle")%>
                                </div>
                                <div class="log_date">
                                    <a href="/WX/Member/ArticleLeaveWord.aspx?Id=&articleId=<%#Eval("Id") %>&flag=leave&leaveid=" class="liuyan"></a>
                                  <%--  <a href="" class="fenxiang"></a>--%>
                                       
                                    <a href="#" flag="zan" onclick="PageJsDataObj.Greet('<%#Eval("Id") %>')"
                                            class="zan"></a>
                                    <%#Eval("IssueTime")%>
                                </div>
                               
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                    <%-- <div id="pullUp">
                            <span class="pullUpIcon"></span><span class="pullUpLabel">上拉更新...</span>
                        </div>--%>
                </asp:PlaceHolder>
            </div>

        </div>
            <div class="foot_fixed">
                       <div class="foot_box">
                          <div class="paddL10 paddR10"><asp:Literal ID="ltrSignOut" runat="server"></asp:Literal></div>
                       </div>
            </div>
                  
        </div>
        <%--<div class="mt20 padd_bot">
            </div>
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
