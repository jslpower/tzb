<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" 
AutoEventWireup="true" CodeBehind="CourtAdd.aspx.cs" Inherits="Enow.TZB.Web.Manage.Court.CourtAdd" %>
<%@ Register src="~/UserControls/UploadControl.ascx" tagname="UploadControl" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
<script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
<link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/js/jquery.boxy.js"></script>
<!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->
<script type="text/javascript" src="/js/jquery.blockUI.js"></script>
<script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
<link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
<script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>
<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=v5eVZYKwoCUtgiLrzHoVx3Bi"></script>
<style type="text/css">
	#allmap {float:left;position:absolute; width:80%;height:400px;overflow: hidden;margin:0;font-family:"微软雅黑";}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
<div class="clear"></div>
 <div class="contentbox">
        <div class="firsttable">
        <span class="firsttableT">场地信息</span>
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
            <tr>
                        <th width="100">
                            场地类型：</th>
                        <td>
                            <asp:DropDownList ID="ddlFieldTypeId" runat="server">
                            </asp:DropDownList>
                            <span class="fontred">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            场地名称：</th>
                        <td>
                            <asp:TextBox ID="txtBallFieldName" runat="server" MaxLength="100" 
                                CssClass="input-txt formsize240" runat="server" errmsg="请填写场地名称!" 
                                valid="required"></asp:TextBox><span class="fontred">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            名称缩写：</th>
                        <td>
                            <asp:TextBox ID="txtShortName" runat="server" MaxLength="40" 
                                CssClass="input-txt formsize240" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            场地形象图：</th>
                        <td>
                            <uc2:UploadControl ID="UploadControl1" runat="server" 
                                IsUploadMore="false" IsUploadSelf="true"
                                                
                                FileTypes="*.jpg;*.gif;*.jpeg;*.png;*.doc;*.ppt;*.xls;*.docx;*.pptx;*.xlsx;*.wps;*.pdf" /><span class="fontred">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            场地数：</th>
                        <td>
                            <asp:TextBox ID="txtFieldNumber" runat="server" MaxLength="6" 
                                CssClass="input-txt formsize30" runat="server" errmsg="场地数只能为数字!" 
                                valid="isNumber" Width="54px">1</asp:TextBox></td>
                    </tr>
                    <tr>
                        <th width="100">
                            运营类型：</th>
                        <td>
                        <select id="ddlTypeId" name="ddlTypeId">
                           <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.CourtEnum),
                                                                                              new string[] { }), TypeIdV)
                                %></select><span class="fontred">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            地区：</th>
                        <td>国家:
                    <select id="ddlCountry" name="ddlCountry" valid="required|RegInteger|isNo" novalue="0" errmsg="请选择国家|请选择国家|请选择国家">
                    </select>省份:
                    <select id="ddlProvince" name="ddlProvince" valid="required|RegInteger|isNo" novalue="0" errmsg="请选择省份|请选择省份|请选择省份">
                    </select>城市:
                    <select id="ddlCity" name="ddlCity" valid="required|RegInteger|isNo" novalue="0" errmsg="请选择城市|请选择城市|请选择城市">
                    </select>区县:
                    <select id="ddlArea" name="ddlArea" valid="required|RegInteger|isNo" novalue="0" errmsg="请选择区县|请选择区县|请选择区县">
                    </select><span class="fontred">*</span></tr>
                    <tr>
                        <th width="100">
                            场地地址：</th>
                        <td><asp:TextBox ID="txtAddress" runat="server" MaxLength="45" 
                                CssClass="input-txt formsize180" runat="server" errmsg="请填写场地地址!" 
                                valid="required"></asp:TextBox><span class="fontred">*</span></tr>
                    <tr>
                        <th width="100">
                            场地经纬度：</th>
                        <td>经度：<asp:TextBox ID="txtLongitude" runat="server" MaxLength="6" 
                                CssClass="input-txt formsize30" runat="server" Width="54px"></asp:TextBox>纬度：<asp:TextBox ID="txtLatitude" runat="server" MaxLength="6" 
                                CssClass="input-txt formsize30" runat="server" Width="54px"></asp:TextBox><a href="javascript:void(0);" id="MapSelectPoint">地图点选</a><div id="allmap" style="z-index:999; vertical-align:middle;display:none;"></div></td></tr>
                    <tr>
                        <th width="100">
                            联系电话：</th>
                        <td><asp:TextBox ID="txtContactTel" runat="server" MaxLength="45" 
                                CssClass="input-txt formsize150" runat="server" errmsg="请填写联系电话!" 
                                valid="required"></asp:TextBox><span class="fontred">*</span></tr>
                    <tr>
                        <th width="100">
                            市场价：</th>
                        <td><asp:TextBox ID="txtMarketPrice" runat="server" MaxLength="6" 
                                CssClass="input-txt formsize30" runat="server" errmsg="请填写市场价|市场价只能为数字!" 
                                valid="required|isNumber" Width="54px"></asp:TextBox>元/2小时<span class="fontred">*</span></tr>
                    <tr>
                        <th width="100">
                            铁丝价：</th>
                        <td><asp:TextBox ID="txtPrice" runat="server" MaxLength="6" 
                                CssClass="input-txt formsize30" runat="server" errmsg="请填写铁丝价|市场价只能为数字!" 
                                valid="required|isNumber" Width="54px"></asp:TextBox>元/2小时<span class="fontred">*</span></tr>
                    <tr>
                        <th width="100">
                            营业时间：</th>
                        <td><asp:TextBox ID="txtHour" runat="server" MaxLength="45" 
                                CssClass="input-txt formsize180" runat="server" errmsg="请填写营业时间!" 
                                valid="required"></asp:TextBox><span class="fontred">*</span></tr>
                    <tr>
                        <th width="100">
                            场地大小：</th>
                        <td><asp:TextBox ID="txtFieldSize" runat="server" MaxLength="45" 
                                CssClass="input-txt formsize180" runat="server"></asp:TextBox></tr>
                    <tr>
                        <th width="100">
                            场地介绍：</th>
                        <td><asp:TextBox ID="txtRemark" runat="server" Height="400px" TextMode="MultiLine" 
                                Width="85%"></asp:TextBox></tr>
                    <tr>
                        <th>场地照片：</th>
                        <td><uc2:Uploadcontrol ID="UploadControl2" runat="server" 
                                IsUploadMore="true" IsUploadSelf="true"
                                                
                                FileTypes="*.jpg;*.gif;*.jpeg;*.png;*.doc;*.ppt;*.xls;*.docx;*.pptx;*.xlsx;*.wps;*.pdf" /></td>
                    </tr>
            </table>
        </div>         
         <div class="Basic_btn fixed">
            <ul>
                <li><a href="javascript:void(0);" id="btnSave">保 存 &gt;&gt;</a></li>
                <li><a href="javascript:void(0);" id="btnCanel">返 回 &gt;&gt;</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
</div>
<script type="text/javascript">
    var PageJsDataObj = {
        Data: {
            MId: '<%=Request.QueryString["MId"] %>',
            SMId: '<%=Request.QueryString["SMId"] %>',
            act: '<%=Request.QueryString["act"] %>',
            CId: '<%=Request.QueryString["CId"] %>',
            id: '<%=Request.QueryString["id"] %>'
        },
        DataBoxy: function () {/*弹窗默认参数*/
            return {
                url: '',
                title: "",
                width: "710px",
                height: "420px"
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
        CreatePlanEdit: function () {
            KEditer.init("<%=txtRemark.ClientID %>", { resizeMode: 1, items: keMore_HaveImage, height: "450px" });
        },
        MapPoint: function () {
            $("#allmap").show();
            // 百度地图API功能
            var Address = $("#<%=txtAddress.ClientID %>").val();
            var CityName = $("#ddlCity").find("option:selected").text();
            var map = new BMap.Map("allmap");    // 创建Map实例   
            var point = new BMap.Point(120.167633, 30.279955);
            map.centerAndZoom(point, 15);  // 初始化地图,设置中心点坐标和地图级别
            map.addControl(new BMap.MapTypeControl());   //添加地图类型控件
            map.setCurrentCity("");          // 设置地图显示的城市 此项是必须设置的
            //设置地图为当前地址
            // 创建地址解析器实例
            var myGeo = new BMap.Geocoder();
            // 将地址解析结果显示在地图上,并调整地图视野
            myGeo.getPoint(Address, function (point) {
                if (point) {
                    map.centerAndZoom(point, 16);
                    map.addOverlay(new BMap.Marker(point));
                }
            }, CityName);
            //单击获取点击的经纬度
            map.addEventListener("click", function (e) {
                //alert(e.point.lng + "," + e.point.lat);
                $("#<%=txtLongitude.ClientID %>").val(e.point.lng);
                $("#<%=txtLatitude.ClientID %>").val(e.point.lat);
                var marker = new BMap.Marker(new BMap.Point(e.point.lat, e.point.lng));
                map.addOverlay(marker);
                $("#allmap").hide();
            });
        },
        CheckForm: function () {
            var form = $("form").get(0);
            return ValiDatorForm.validator(form, "alert");

        },
        Form: null,
        Save: function () {
            $("#btnSave").text("提交中...");
            KEditer.sync();
            PageJsDataObj.submit();
        },
        submit: function () {
            $.newAjax({
                type: "post",
                cache: false,
                url: "CourtAdd.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
                data: $("#btnSave").closest("form").serialize(),
                dataType: "json",
                success: function (ret) {
                    //ajax回发提示
                    if (ret.result != "0") {
                        tableToolbar._showMsg(ret.msg, function () {
                            if (document.referrer != "" && document.referrer != "undefined")
                                window.location.href = document.referrer;
                            else
                                window.location.href = "CourtDefault.aspx?" + $.param(PageJsDataObj.Data);
                        });
                    } else {
                        tableToolbar._showMsg(ret.msg);
                        $("#btnSave").text("保 存");
                        PageJsDataObj.BindBtn();
                    }
                },
                error: function () {
                    tableToolbar._showMsg(tableToolbar.errorMsg);
                    $("#btnSave").text("保 存");
                    PageJsDataObj.BindBtn();
                }
            });
        },
        PageInit: function () {
            PageJsDataObj.CreatePlanEdit();
        },
        BindBtn: function () {
            $("#MapSelectPoint").unbind("click").click(function () {
                PageJsDataObj.MapPoint();
            });
            $("#btnSave").unbind("click").click(function () {
                if (PageJsDataObj.CheckForm()) {
                    PageJsDataObj.Save();
                }
            });
            $("#btnCanel").unbind("click").click(function () {
                window.history.go(-1);
                return false;
            })
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
        PageJsDataObj.PageInit();
        PageJsDataObj.BindBtn();
    });
</script>
</asp:Content>
