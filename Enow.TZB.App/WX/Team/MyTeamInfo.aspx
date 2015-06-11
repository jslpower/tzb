<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyTeamInfo.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.MyTeamInfo" %>
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
     
    <link rel="stylesheet" href="/WX/css/tangzhu.css" type="text/css" media="screen"/>
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
            <uc1:UserHome ID="UserHome1" runat="server" />
    <div class="warp">        
    <div class="msg_tab" id="n4Tab">
        <div class="TabTitle">
           <ul class="fixed">
              <li id="n4Tab_Title0" class="active"><a href="MyTeamInfo.aspx<%=dhurl %>">介绍</a></li>
              <li id="n4Tab_Title1"><a href="TeamInfo.aspx<%=dhurl %>">队员</a></li>
              <li id="n4Tab_Title2"><a href="/WX/Member/TeamArticles.aspx<%=dhurl %>">日志</a></li>
           </ul>
        </div>
        <div class="TabContent">
        <div id="n4Tab_Content0">
            
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
                   <asp:Panel runat="server" CssClass="foot_btn">
                    <ul>
                           <li class="box-siz">
                             <a href="SignUp.aspx?TeamId=<%=Enow.TZB.Utility.Utils.GetQueryStringValue("TeamId") %>" class="basic_btn">加入</a>
                           </li>
                           <li class="box-siz">
                             <a href="javascript:void(0);" data-jid="<%=Enow.TZB.Utility.Utils.GetQueryStringValue("TeamId") %>" class="basic_ybtn Agzbtn"><%=Selgzyf(Enow.TZB.Utility.Utils.GetQueryStringValue("TeamId"))%></a>
                        
                           </li>
                       </ul>
                   </asp:Panel>
                </div>
            
            </div>
      </div>
    </div>
    </div>
    </form>
    <script type="text/javascript">
        var PageJsDataObj = {
            GoAjax: function (url) {
                $.ajax({
                    type: "get",
                    url: url,
                    dataType: "json",
                    success: function (result) {
                        if (result.result == "1") {
                            alert(result.msg);
                            window.parent.location.reload();
                        }
                        else {
                            alert(result.msg);
                        }
                    },
                    error: function (data) {
                        //ajax异常--你懂得
                        alert("关注失败！");
                    }
                });
            },
            Add: function (obj) {
                var atid = $(obj).attr("data-jid");
                if (atid) {
                    var dataurl = "Default.aspx?ation=inter&JobId=" + atid;
                    this.GoAjax(dataurl);
                }
                else {
                    alert("请刷新后重试!");
                }
            },
            BindBtn: function () {
                //添加
                $(".Agzbtn").each(function () {
                    var obj = this;
                    $(obj).click(function () {
                        if (window.confirm("确定要" + $(obj).html() + "吗？")) {
                            PageJsDataObj.Add(obj);
                        }
                    });
                })
            },
            PageInit: function () {
                //绑定功能按钮
                this.BindBtn();
            }
        }
        $(function () {
            PageJsDataObj.PageInit();
            return false;
        })
        </script>
</body>
</html>
