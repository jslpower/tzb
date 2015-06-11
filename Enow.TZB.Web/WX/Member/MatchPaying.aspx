<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatchPaying.aspx.cs" Inherits="Enow.TZB.Web.WX.Member.MatchPaying" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>参赛支付</title>
    <meta http-equiv="Content-Type" content="text/html;" />
    <meta id="viewport" name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1; user-scalable=no;" />
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:UserHome ID="UserHome1" Userhometitle="参赛支付" runat="server" />
    <div class="warp">
        <div class="msg_tab">
            <div class="TabContent">
                <div id="n4Tab_Content0">
                    <div class="msg_list">
                        <ul>
                            <li>
                                <label>
                                    赛事名称：</label><asp:Literal ID="ltrMatchName" runat="server"></asp:Literal></li>
                            <li>
                                <label>
                                    赛事保证金：</label><span id="spanMoney"><asp:Literal ID="ltrMoney" runat="server"></asp:Literal></span>元</li>
                            <li>
                                <label>
                                    报名费：</label><span id="span1"><asp:Literal ID="ltrEarnestMoney" runat="server"></asp:Literal></span>元</li>
                        </ul>
                    </div>
                    <div class="padd20">
                        <asp:Button ID="btnTenPay" CssClass="basic_btn" runat="server" Text="微信支付" OnClick="btnTenPay_Click" /></div>
                    <asp:PlaceHolder ID="phHide" runat="server" Visible="false">
                        <div class="padd20 paddT0">
                            <asp:Button ID="btnLinePay" CssClass="basic_btn" runat="server" Text="线下支付" OnClick="btnLinePay_Click" />
                        </div>
                    </asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
