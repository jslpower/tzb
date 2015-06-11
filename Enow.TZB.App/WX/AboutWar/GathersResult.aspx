<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GathersResult.aspx.cs" Inherits="Enow.TZB.Web.WX.AboutWar.GathersResult" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>我的约战</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<link rel="stylesheet" href="/WX/css/user.css" type="text/css" media="screen"/>
  <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="我的约战" runat="server" />
 
<div class="warp">
    
    <div class="msg_tab"  id="n4Tab">
     
        <div class="TabTitle">
           <ul class="fixed">
               <li id="n4Tab_Title0"><a href="UserAwList.aspx">约战中</a></li>
              <li id="n4Tab_Title1" ><a href="GathersGoing.aspx">进行中</a></li>
              <li id="n4Tab_Title2" class="active"><a href="BattlefieldList.aspx">战报</a></li>
           </ul>
        </div>
   <div class="form_list">
       <ul>
           <li>
               <span class="label_name">约战主队</span>
               <asp:TextBox ID="txtZTeam" CssClass="u-input" runat="server" placeholder="请输入"></asp:TextBox>
               
           </li>
           
           <li>
               <span class="label_name">上</span>
                <asp:TextBox ID="txtZUp" CssClass="u-input" runat="server" placeholder="请输入"></asp:TextBox>
               
               
           </li>

           <li>
            
               
               <span class="label_name">下</span>
               <asp:TextBox ID="txtZDown" CssClass="u-input" runat="server" placeholder="请输入"></asp:TextBox>
             
           </li>
           <li>
               <span class="label_name">约战客队</span>
                <asp:TextBox ID="txtCTeam" CssClass="u-input" runat="server" placeholder="请输入"></asp:TextBox>
               
              
           </li>

           <li>
               <span class="label_name">上</span>
                <asp:TextBox ID="txtCUp" CssClass="u-input" runat="server" placeholder="请输入"></asp:TextBox>
               
               
           </li>

           <li>
               <span class="label_name">下</span>
                <asp:TextBox ID="txtCDown" CssClass="u-input" runat="server" placeholder="请输入"></asp:TextBox>
               
              
           </li>

           <li>
               <span class="label_name">战报详情</span>
               <asp:TextBox ID="txtGatherInfo" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
           </li>
           
       </ul>
   </div>
    
   </div>
   
   <div class="msg_btn">
       <asp:Button ID="btnSave" CssClass="basic_btn" OnClientClick="return yanzheng(); " runat="server" Text="提交" 
           onclick="btnSave_Click" />
       <asp:Literal ID="litbtn" runat="server"></asp:Literal>
   </div>
   
   
</div>
    </form>
    <script type="text/javascript">
        function yanzheng() {
            var ms = parseInt($("#<%=txtZUp.ClientID %>").val());
            var mx = parseInt($("#<%=txtZDown.ClientID %>").val());
            var gs = parseInt($("#<%=txtCUp.ClientID %>").val());
            var gx = parseInt($("#<%=txtCDown.ClientID %>").val());
            var count = $("#<%=txtGatherInfo.ClientID %>").val()
            var num = /^[0-9]{1,5}$/;
            if (!num.test(ms) || !num.test(mx) || !num.test(gs) || !num.test(gx)) {
                alert("只能输入数字！");
                return false;
            }
            if (count=="") {
                alert("请填写战报详情！");
                return false;
            }
            return true;
        }
        var PageJsDataObj = {
            aid: '<%=Enow.TZB.Utility.Utils.GetQueryStringValue("AID") %>',
            GoAjax: function (url) {
                $.ajax({
                    type: "get",
                    url: url,
                    dataType: "json",
                    success: function (result) {
                        if (result.result == "1") {
                            alert(result.msg);
                            window.location.href = "BattlefieldList.aspx";
                        }
                        else {
                            alert(result.msg);
                        }
                    },
                    error: function (data) {
                        //ajax异常--你懂得
                        alert("请刷新后重试！");
                    }
                });
            },
            Update: function (strval, obj) {
                if (obj) {
                    if (PageJsDataObj.aid != "") {
                        var dataurl = "BattlefieldList.aspx?ation=" + strval + "&AID=" + PageJsDataObj.aid;
                        this.GoAjax(dataurl);
                    }
                    else {
                        alert("请刷新后重试！");
                    }
                }
                else {
                    alert("请刷新后重试！");
                }

            },
            BindBtn: function () {
                //确认
                $(".qrbtn").each(function () {
                    var obj = this;
                    $(obj).click(function () {
                        if (window.confirm("确定要确认吗？")) {
                            PageJsDataObj.Update("QueRen", obj);
                        }

                    });
                });

                //重填
                $(".ctbtn").each(function () {
                    var obj = this;
                    $(obj).click(function () {
                        if (window.confirm("确定要重填吗？")) {
                            PageJsDataObj.Update("ChongTian", obj);
                        }

                    });
                });
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
