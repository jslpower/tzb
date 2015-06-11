<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BazaarEdit.aspx.cs" Inherits="Enow.TZB.App.WX.Member.BazaarEdit" %>

<%@ Register Src="../UserControls/UserHome.ascx" TagName="UserHome" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <script src="../../Js/jquery-1.10.2.min.js" type="text/javascript"></script>
    
    <title>义卖商品信息</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdfyfbool" Value="0" runat="server" />
    <uc1:UserHome ID="UserHome1" Userhometitle="商品信息" runat="server" />
    <div class="warp">
        <div class="msg_tab" id="n4Tab">
            <div style="width: 100%">
                <div class="TabContent">
                    <div id="n4Tab_Content0">
                        <div class="msg_list">
                            <ul>
                             <li>
                                    <label>
                                        分&nbsp;&nbsp;&nbsp;&nbsp;类：</label>
                                    <asp:DropDownList ID="dropyjclass" DataTextField="Rolename" DataValueField="Id"  runat="server"></asp:DropDownList></li>
                                <li>
                                    <label>
                                        名&nbsp;&nbsp;&nbsp;&nbsp;称：</label>
                                    <asp:TextBox ID="txtname" CssClass="input_bk formsize150" runat="server"></asp:TextBox></li>
                                <li>
                                    <label>
                                        图&nbsp;&nbsp;&nbsp;&nbsp;片：</label><asp:FileUpload ID="imgFileUpload" runat="server"
                                            Style="width: 200px" /><asp:Literal ID="ltrHead" runat="server"></asp:Literal></li>
                                <li>
                                    <label>
                                        市场价：</label><asp:TextBox ID="txtscj" CssClass="input_bk formsize150" runat="server"></asp:TextBox></li>
                                <li>
                                    <label>
                                        会员价：</label>
                                    <asp:TextBox ID="txthyj" CssClass="input_bk formsize150" runat="server"></asp:TextBox></li>
                                             <li>
                                    <label>
                                        生产商：</label>
                                    <asp:TextBox ID="txtscs" CssClass="input_bk formsize150" runat="server"></asp:TextBox></li>
                                    <li>
                                    <label>
                                        数量：</label>
                                    <asp:TextBox ID="txtsuliang" CssClass="input_bk formsize150" runat="server"></asp:TextBox></li>
                                <li>
                                    <label>
                                        包含运费：</label>
                                    <input id="radtrue" name="yunfei" class="yfrad" type="radio" value="1" />是
                                    <input id="radfalse" name="yunfei" class="yfrad" type="radio" value="0" />否
                                </li>
                                <li id="trfreiht">
                                    <label>
                                        运&nbsp;&nbsp;&nbsp;&nbsp;费：</label><asp:TextBox ID="txtyunfei" CssClass="input_bk formsize150" runat="server"></asp:TextBox></li>
                                <li>
                                    <div style="width: 58px; float: left">
                                        简&nbsp;&nbsp;&nbsp;&nbsp;介：</div>
                                    <asp:TextBox ID="txtjianjie" runat="server" Style="width: 80%; height: 200px;" TextMode="MultiLine"></asp:TextBox></li>
                            </ul>
                        </div>
                        <div class="msg_btn">
                            <asp:Button CssClass="basic_btn" ID="Button1" 
                                OnClientClick="return yanzheng.btnys()" runat="server" Text="保 存" 
                                onclick="Button1_Click" /></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        var yanzheng = {
            btnys: function () {
                var tname = $("#<%=txtname.ClientID %>").val(); //标题
                var tscj = $("#<%=txtscj.ClientID %>").val();
                var thyj = $("#<%=txthyj.ClientID %>").val();
                var tsl = $("#<%=txtsuliang.ClientID %>").val();
                var tyf = $("#<%=txtyunfei.ClientID %>").val();
                var tcon = $("#<%=txtjianjie.ClientID %>").val();
                if (tname == "" || tname.length <= 0) {
                    alert("标题不能为空或!");
                    return false;
                }
                if (parseFloat(tscj) <= 0) {
                    alert("市场价不正确!");
                    return false;
                }
                if (parseFloat(thyj) <= 0) {
                    alert("会员价不正确!");
                    return false;
                }
                if (parseInt(tsl) <= 0) {
                    alert("数量不能小于0!");
                    return false;
                }
                if ($("#<%=hdfyfbool.ClientID %>").val() == "0" && parseFloat(tyf) <= 0) {
                    alert("运费不正确!");
                    return false;
                }
                if (tcon == "" || tcon.length <= 0) {
                    alert("简介不能为空或!");
                    return false;
                }
                return true;
            },
            Lodrad: function () {
                var xz = $("#<%=hdfyfbool.ClientID %>").val();
                if (xz == "1") {
                    $("#trfreiht").css("display", "none");
                    $("#radtrue").attr("checked", true);
                }
                else {
                    $("#trfreiht").css("display", "");
                    $("#radfalse").attr("checked", true);
                }
            }
        }
        $(function () {
            yanzheng.Lodrad();
            $(".yfrad").each(function () {
                var obj = this;
                $(obj).click(function () {
                    $("#<%=hdfyfbool.ClientID %>").val($(obj).val());
                    if ($(obj).val() == "1") {

                        $("#trfreiht").css("display", "none");
                    }
                    else {
                        $("#trfreiht").css("display", "");
                    }
                });
            });
        })
    </script>
</body>
</html>
