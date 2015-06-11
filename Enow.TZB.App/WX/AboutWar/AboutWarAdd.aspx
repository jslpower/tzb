<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutWarAdd.aspx.cs" Inherits="Enow.TZB.Web.WX.AboutWar.AboutWarAdd" %>

<%@ Register Src="../UserControls/UserHome.ascx" TagName="UserHome" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>约战</title>
    <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/WX/css/user.css" type="text/css" media="screen" />
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Js/CitySelect.js"></script>
    <script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdfqcid" runat="server"/>
    <asp:HiddenField ID="hdfadderss" runat="server"/>
    <uc1:UserHome ID="UserHome1" Userhometitle="约战" runat="server" />
    <input id="hdType" type="hidden" value="" runat="server" />
    <input id="hdFee" type="hidden" value="" runat="server" />
    <div class="warp">
        <div class="form_list">
            <ul>
               <asp:Literal runat="server" ID="litqd"></asp:Literal>
                <li><span class="label_name">约战名称:</span>
                    <asp:TextBox ID="txtkouhao" runat="server" CssClass="u-input" placeholder="请输入"></asp:TextBox>
                </li>
                <li><span class="label_name">约战时间:</span>
                    <asp:TextBox ID="txttime" onfocus="WdatePicker({dateFmt: 'yyyy-MM-dd HH:mm'})" runat="server" CssClass="u-input"
                        placeholder="请输入"></asp:TextBox>
                </li>
                <li><span class="label_name">球场:</span>
                    <asp:DropDownList ID="dropqiuchang" runat="server" DataTextField="FieldName" DataValueField="Id" ></asp:DropDownList>
                </li>
                <li><span class="label_name">地址:</span>
                    <asp:TextBox ID="txtdizhi" ReadOnly="true" runat="server" CssClass="u-input" 
                        Rows="2"></asp:TextBox>
                </li>
                <li><span class="label_name">赛制:</span> <span class="feiyong">
                    <asp:DropDownList ID="dropsaizhi" runat="server">
                        <asp:ListItem Value="5">5人制</asp:ListItem>
                        <asp:ListItem Value="8">8人制</asp:ListItem>
                        <asp:ListItem Value="11">11人制</asp:ListItem>
                        <asp:ListItem Value="100">其他</asp:ListItem>
                    </asp:DropDownList>
                </span></li>
                <li><span class="label_name">费用:</span>
                    <asp:DropDownList ID="dropfeiyong" runat="server">
                        <asp:ListItem Value="1">AA制</asp:ListItem>
                        <asp:ListItem Value="2">主队</asp:ListItem>
                        <asp:ListItem Value="3">客队</asp:ListItem>
                        <asp:ListItem Value="4">胜方</asp:ListItem>
                        <asp:ListItem Value="5">败方</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li><span class="label_name">战书:</span>
                    <%--<asp:TextBox ID="txtWarBook"   runat="server" CssClass="u-input" TextMode="MultiLine" placeholder="请输入约战说明"></asp:TextBox>--%>
                    <textarea id="txtWarBook" class="u-input" runat="server"></textarea>
                </li>
            </ul>
        </div>
        <div class="foot_fixed">
            <div class="foot_box">
                <div class="paddL10 paddR10">
                    <asp:Button CssClass="basic_btn" ID="btnSave" runat="server" Text="下一步" OnClick="btnSave_Click" OnClientClick="return PageJsDataObj.CheckForm()" />
                    <%-- <input type="button" value="下一步" class="basic_btn" onclick="location.href='Gathers_Step2.aspx'"/>--%>
                </div>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        var dizhiarry = new Array();
        var PageJsDataObj = {
            objval: "",
            sublist: [],
            diquchaxun: [],
            changdilist: <%=changdilist %>,
            CheckForm: function () {
                var title = $("#<%=txtkouhao.ClientID %>").val();
                var stime = $("#<%=txttime.ClientID %>").val();
                var zhanshu = $("#<%=txtWarBook.ClientID %>").val();
                var tdizhi = $("#<%=txtdizhi.ClientID %>").val();
                if (title=="") {
                    alert("请输入约战名称！");
                    return false;
                }
                if (stime=="") {
                    alert("请输入约战时间！");
                    return false;
                }
                if (zhanshu=="") {
                    alert("请输入战书内容！");
                    return false;
                }
                if (tdizhi=="") {
                    alert("请选择球场！");
                    return false;
                }
                return true;
            },
            Getadname: function (strval) {
                if (strval != "-1"&&strval!="") {
                    $("#<%=txtdizhi.ClientID%>").val(dizhiarry[strval]);
                }
            },
            Initlistload: function () {
                if (PageJsDataObj.changdilist != null) {
                    for (var i = 0; i < PageJsDataObj.changdilist.length; i++) {
                        dizhiarry[PageJsDataObj.changdilist[i].Id] = PageJsDataObj.changdilist[i].Address;
                    }
                    PageJsDataObj.Getadname("<%=Enow.TZB.Utility.Utils.GetQueryStringValue("QID") %>");
                }
            },
            PageInit: function () {
                PageJsDataObj.Initlistload();
            },
            BindBtn: function () {
                PageJsDataObj.PageInit();
                $("#btnCanel").unbind("click").click(function () {
                    window.history.go(-1);
                    return false;
                });
                $("#<%=dropqiuchang.ClientID %>").change(function () {
                    var objvl = $("#<%=dropqiuchang.ClientID %>").val();
                    PageJsDataObj.Getadname(objvl);
                });
            }
        }
        $(function () {
            pcToobar.init({
                gID: "#ddlCountry",
                pID: "#ddlProvince",
                cID: "#ddlCity",
                xID: "#ddlArea",
                comID: ''
            });
            PageJsDataObj.BindBtn();
        });
    </script>
</body>
</html>
