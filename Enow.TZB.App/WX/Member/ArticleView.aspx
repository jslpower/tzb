<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleView.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.ArticleView" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html >
<head runat="server">
   <title>我的日志</title>
   <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
  <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
 <link rel="stylesheet" href="/WX/css/tangzhu.css" type="text/css" media="screen"/>
 <%--<script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>--%>
   <%-- <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />--%>
   <%-- <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>--%>
   <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Js/jq.mobi.min.js"></script>
   <script type="text/javascript">
       $(function () {
         
//           $("a[flag='zan']").click(function () {
//               $(this).removeClass("zan")
//               $(this).addClass("zan_ok");
//           })
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
                            return;
                            //});
                        }
                        else { alert(result.msg); ; }
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
                    data.url += '/ArticleView.aspx?';

                    data.url += $.param({
                        sl: this.Query.sl,
                        doType: "greetfast",
                        id: recordid
                    });

                }
                this.GoAjax(data.url);
                
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   
     <input id="pageindex" type="hidden" value="<%=CurrencyPage %>" />
    <input id="hidPageend" type="hidden" value="0" />
    <uc1:UserHome ID="UserHome1" Userhometitle="我的日志" runat="server" />
<div class="warp">

  <div class="msg_tab"  id="n4Tab">     

        <div class="TabContent"> 
 			
                <!------------日志详情页面--也显示在当前标签下----->
               <%-- <div class="tangzhu_jianli padd20" style="display:none;">--%>
                    <div class="tangzhu_jianli padd20" ">
                       <div class="font16 cent">  
                         <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                        </div>
                       <div class="font14 cent font_gray paddB10"> 
                         <asp:Literal ID="ltrIssueTime" runat="server"></asp:Literal>
                        </div>
                       
                       <div class="tangzhu_jieshao"><asp:Literal ID="ltrImg" runat="server"></asp:Literal><br />
                         <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                       </div>

                       <div class="qiudui_log_ly mt10">
                           <ul>
                              <asp:Repeater ID="rptList" runat="server" onitemdatabound="ReplyDataBound">
                              <ItemTemplate>
                               <li>
                                   <div class="user_tx">
                                          <img alt="" src="<%# GetMemberHeadPhoto(Eval("MemberId").ToString())%>"/>
                                          <p><%#Eval("NickName")%></p>
                                          <p class="font_gray font12"><%#Eval("IssueTime")%></p>
                                   </div>
                                   <div class="txt"><%#Eval("LeaveWord")%></div>
                                  <asp:Repeater ID="rptReplyList" runat="server">
                                  <ItemTemplate>
                                   <div class="txt">回复内容：<%#Eval("LeaveWord")%></div>
                                   </ItemTemplate>
                                   </asp:Repeater>
                                   <div class="log_date paddT10">
                            <a   href="/WX/Member/ArticleLeaveWord.aspx?articleId=<%=ArticleId%>&Id=<%#Eval("Id") %>&flag=reply&leaveid=" class="liuyan"></a>
                          
                             </div>
                               </li>

                              </ItemTemplate>
                              </asp:Repeater>
                           </ul>
                       </div>
                       
                </div>
                
                <!------------日志详情页面-end------>
                
                
                

        </div>
  </div>
</div>
    </form>
</body>
</html>
