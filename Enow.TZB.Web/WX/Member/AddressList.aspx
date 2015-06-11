<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddressList.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.AddressList" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
<title>收货地址管理</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
<link rel="stylesheet" href="/WX/css/address.css" type="text/css" media="screen" />
    <script src="../../Js/jq.mobi.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<uc1:UserHome ID="UserHome1" Userhometitle="收货地址管理" runat="server" />


<div class="user_warp">
   
    <div class="address_box mt10">
        <ul>
        <asp:Repeater ID="rptList" runat="server">
          <ItemTemplate>
            <li>
               <p><%#Eval("Recipient")%> <%#Eval("MobilePhone")%> <%#Eval("IsDefaultAddress").ToString().ToLower()=="true"?"<span class=\"font_yellow paddL10\" style=\"float: right;\">默认地址</span>":""%></p>
               <p class="txt"><%#Eval("CountyName")%><%#Eval("ProvinceName")%><%#Eval("CityName")%><%#Eval("AreaName")%><%#Eval("Address")%></p>
               <p style="height: 25px;"><span class="floatR"><a href="AddressAdd.aspx?ID=<%#Eval("Id")%>" class="basic_ybtn">编辑</a><a href="javascript:void(0);" data-AdresId="<%#Eval("Id")%>" class="basic_gbtn butbaom">删除</a></span></p>
            </li>
            </ItemTemplate>
         </asp:Repeater>
        </ul>
    </div>
   
   
    <div class="foot_fixed">
           <div class="foot_box">
                  <div class="address_add" onClick="location.href='AddressAdd.aspx'"><img src="/WX/images/add_icon.png">新增</div>
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
                             alert("删除失败！");
                         }
                     });
                 },
                 Add: function (obj) {
                     var atid = $(obj).attr("data-AdresId");
                     if (atid) {
                         var dataurl = "AddressList.aspx?ation=delete&AdresId=" + atid;
                         this.GoAjax(dataurl);
                     }
                     else {
                         alert("请选择要删除的项!");
                     }
                 },
                 BindBtn: function () {
                     //添加
                     $(".butbaom").each(function () {
                         var obj = this;
                         $(obj).click(function () {
                             if (window.confirm("确定要删除吗？")) {
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
