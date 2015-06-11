<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddressAdd.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.AddressAdd" %>

<%@ Register Src="../UserControls/UserHome.ascx" TagName="UserHome" TagPrefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>收货地址详情</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/WX/css/address.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/WX/css/user.css" type="text/css" media="screen" />
    <script src="../../Js/jquery-1.4.4.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Js/CitySelect.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hdfmrdz" />
    <uc1:UserHome ID="UserHome1" Userhometitle="收货地址详情" runat="server" />
    <div class="warp">
        <div class="form_list address_form">
            <ul id="dizhixx">
                <li><span class="label_name">收货人姓名</span>
                    <asp:TextBox ID="txtRecipient" CssClass="u-input" value="" placeholder="请填写" runat="server"></asp:TextBox>
                </li>
                <li><span class="label_name">手机号码</span>
                    <asp:TextBox ID="txtMobile" CssClass="u-input" value="" placeholder="请填写" runat="server"></asp:TextBox>
                </li>
                <li><span class="label_name">所在区域</span>
                    <div class="select">
                        <select id="ddlCountry" name="ddlCountry" runat="server">
                        </select>
                    </div>
                </li>
                <li>
                    <div class="select">
                        <select id="ddlProvince" name="ddlProvince" runat="server">
                        </select>
                    </div>
                </li>
                <li>
                    <div class="select">
                        <select id="ddlCity" name="ddlCity" runat="server">
                        </select>
                    </div>
                </li>
                <li>
                    <div class="select">
                        <select id="ddlArea" name="ddlArea" runat="server">
                        </select>
                    </div>
                </li>
                <li><span class="label_name">详细地址</span>
                    <asp:TextBox ID="txtAddress" CssClass="u-input" value="" placeholder="请填写" runat="server"></asp:TextBox>
                </li>
                <li>
                    <asp:Literal ID="litlmradderss" runat="server"></asp:Literal>
                    <span id="mrdzbol" class="fxk_on">设为默认收货地址</span> </li>
            </ul>
        </div>
        <div class="msg_btn">
            <asp:Button ID="btnSave" CssClass="basic_btn" OnClientClick="return PageObjload.yanzheng(); "
                runat="server" Text="保 存" OnClick="btnSave_Click" />
        </div>
    </div>
    </form>
    <script type="text/javascript" language="javascript">
            var PageObjload = {
                mrbol:<%=modz %>,
                yanzheng: function () {
                    var MPhone = $("#<%=txtMobile.ClientID %>").val();
                    var Address = $("#<%=txtAddress.ClientID %>").val();
                    var Recipient = $("#<%=txtRecipient.ClientID %>").val();
                    var num = /^[0-9]{11}$/;
                    var cl = /^[a-zA-Z0-9\u4e00-\u9fa5]{1,100}$/;
                    var sjr = /^[a-zA-Z0-9\u4e00-\u9fa5]{1,50}$/;
                    if (Recipient==""||Recipient.length>=50) {
                        alert("收件人不能为空或超长！");
                        return false;
                    }
                    if (!num.test(MPhone)) {
                        alert("手机号码格式不正确！");
                        return false;
                    }
                    if ($("#ddlCountry").val() == "" || $("#ddlProvince").val() == "" || $("#ddlCity").val() == "" || $("#ddlArea").val() == "") {
                        alert("请选择地址信息！");
                        return false;
                    }
                    if (!cl.test(Address)) {
                        alert("详细地址不能为空或超长！");
                        return false;
                    }
                    return true;
                },
                mrdzsave:function(){
                $("#dizhixx").find("#mrdzbol").removeClass();
                if (PageObjload.mrbol) {
                    PageObjload.mrbol=false;
                    $("#dizhixx").find("#mrdzbol").addClass("fxk");
                    $("#<%=hdfmrdz.ClientID %>").val("0");
                }
                else {
                    PageObjload.mrbol=true;
                    $("#dizhixx").find("#mrdzbol").addClass("fxk_on");
                    $("#<%=hdfmrdz.ClientID %>").val("1");
                    
                }
                }
            }
            $(function () {
                
                pcToobar.init({
                    gID: "#ddlCountry",
                    pID: "#ddlProvince",
                    cID: "#ddlCity",
                    xID: "#ddlArea",
                    comID: '',
                    gSelect: '<%=CId %>',
                    pSelect: '<%=PId %>',
                    cSelect: '<%=CSId %>',
                    xSelect: '<%=AId %>'
                });
                PageObjload.mrdzsave();
                $("#dizhixx").find("#mrdzbol").live("click", function () {
                    PageObjload.mrdzsave();
                });

            });
    </script>
</body>
</html>
