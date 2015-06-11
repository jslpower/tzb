<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="ActivityAdd.aspx.cs" Inherits="Enow.TZB.Web.Manage.Activity.ActivityAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
<script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>
    <link rel="stylesheet" type="text/css" href="/Css/jquery.multiselect.css" />
    <link rel="stylesheet" type="text/css" href="/Css/jquery.multiselect.filter.css" />
    <link rel="stylesheet" type="text/css" href="/Css/UI/jquery-ui.min.css" />
    <script src="/Js/UI/jquery.ui.widget.min.js" type="text/javascript"></script>
    <script src="/Js/UI/jquery.effects.core.min.js" type="text/javascript"></script>
    <script src="/Js/UI/jquery.ui.position.min.js" type="text/javascript"></script>
    <script src="/Js/jquery.multiselect.min.js" type="text/javascript"></script>
    <script src="/Js/jquery.multiselect.filter.min.js" type="text/javascript"></script>
    <script src="/Js/jquery.multiselect.zh-cn.js" type="text/javascript"></script>
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>
    <style type="text/css">
      .yingcang{
       display:none;	
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:HiddenField ID="hdfsttime" runat="server" />
    <asp:HiddenField ID="hdfjztime" runat="server" />
    <asp:HiddenField ID="hdfgssq" runat="server" />
    <asp:HiddenField ID="hdfcdID" runat="server" />
    <asp:HiddenField ID="hdfcdname" runat="server" />
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <span class="firsttableT">活动信息</span>            
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                   <tr>
                    <th width="100">
                      类型：
                    </th>
                    <td>
                        <asp:DropDownList ID="droptypes" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        高亮：
                    </th>
                    <td>
                        <asp:TextBox ID="txtqx" runat="server" MaxLength="100" CssClass="input-txt formsize240"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtMatchName" runat="server" MaxLength="100" CssClass="input-txt formsize240"
                            errmsg="请填写名称!" valid="required"></asp:TextBox><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                      发布至：
                    </th>
                    <td>
                        <asp:DropDownList ID="dropfbmb" DataTextField="Text" DataValueField="Value" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        地区：
                    </th>
                    <td>
                        国家:
                        <select id="ddlCountry" class="seldiqu" name="ddlCountry" valid="required|RegInteger|isNo" novalue="0"
                            errmsg="请选择国家|请选择国家|请选择国家">
                        </select>省份:
                        <select id="ddlProvince" class="seldiqu" name="ddlProvince" valid="required|RegInteger|isNo" novalue="0"
                            errmsg="请选择省份|请选择省份|请选择省份">
                        </select>城市:
                        <select id="ddlCity" class="seldiqu" name="ddlCity" valid="required|RegInteger|isNo" novalue="0"
                            errmsg="请选择城市|请选择城市|请选择城市">
                        </select><span id="quxian">区县:</span>
                        <select id="ddlArea" class="seldiqu" name="ddlArea" valid="required|RegInteger|isNo" novalue="0"
                            errmsg="请选择区县|请选择区县|请选择区县">
                        </select><span class="fontred">*</span>
                </tr>
                <tr id="qiuchangtr">
                    <th width="100">
                        球场名称：
                    </th>
                    <td>
                       <select id="ddlqiuchang" class="selqc" name="ddlqiuchang">
                        </select>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        地址：
                    </th>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" CssClass="input-txt formsize240"
                            errmsg="请填活动地点!" valid="required"></asp:TextBox><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        费用：
                    </th>
                    <td>
                        <asp:TextBox ID="txtfeiyong" runat="server" MaxLength="100" CssClass="input-txt formsize240"
                            errmsg="请填写费用信息!" valid="required"></asp:TextBox><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        同城报名：
                    </th>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        报名截止日期：
                    </th>
                    <td>
                        日期：
                        <input type="text" id="txtbmendtime" name="txtStartDate" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtStartDate\')}'})" value='<%=endtime %>'  class="input-txt formsize80" valid="required|isDate" errmsg="请选择报名截止日期!|请选择报名截止日期!" />
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        活动日期：
                    </th>
                    <td>
                        日期：
                        <input type="text" id="txtStartDate" name="txtStartDate" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtbmendtime\')}'})" value='<%=stime %>'  class="input-txt formsize80" valid="required|isDate" errmsg="请选择活动日期!|请选择活动日期!" />
                        <span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        详情：
                    </th>
                    <td>
                        <asp:TextBox ID="txtContent" runat="server" Height="400px" TextMode="MultiLine" Width="85%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="Basic_btn fixed">
            <ul>
                <li><asp:LinkButton ID="btnSave" OnClientClick="return PageJsDataObj.CheckForm();" 
                        runat="server" onclick="btnSave_Click">保 存 &gt;&gt;</asp:LinkButton></li>
                <li><a href="javascript:void(0);" id="btnCanel">返 回 &gt;&gt;</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var dizhiarry=new Array();
        var PageJsDataObj = {
            objval: "",
            
            sublist: [],
            diquchaxun:[],
            changdilist: <%=changdilist %>,
            CreatePlanEdit: function () {
                KEditer.init("<%=txtContent.ClientID %>", { resizeMode: 1, items: keMore_HaveImage, height: "450px" });
            },
            CheckForm: function () {
                var form = $("form").get(0);
                $("#<%=hdfsttime.ClientID %>").val($("#txtStartDate").val());
                $("#<%=hdfjztime.ClientID %>").val($("#txtbmendtime").val());
                var sublist =[$("#ddlCountry").val(), $("#ddlProvince").val(), $("#ddlCity").val(), $("#ddlArea").val()];
                $("#<%=hdfcdID.ClientID %>").val($("#ddlqiuchang").val());
                $("#<%=hdfgssq.ClientID %>").val(sublist.join(","));
                KEditer.sync();
                return ValiDatorForm.validator(form, "alert");


            },
            Getdqgj: function (Intstr) {
               var guojia=[];
                for (var i = 0; i < PageJsDataObj.changdilist.length; i++) {
                    if (PageJsDataObj.changdilist[i].CountryId == Intstr) {
                        guojia.push(PageJsDataObj.changdilist[i]);
                    }
                }
                PageJsDataObj.diquchaxun=guojia;
                PageJsDataObj.Getdqshen(PageJsDataObj.sublist[1])
            },
            Getdqshen: function (Intstr) {
                var sheng=[];
                for (var i = 0; i < PageJsDataObj.diquchaxun.length; i++) {
                    if (PageJsDataObj.diquchaxun[i].ProvinceId == Intstr) {
                        sheng.push(PageJsDataObj.diquchaxun[i]);
                    }
                }
                PageJsDataObj.diquchaxun=sheng;
                PageJsDataObj.Getdqshi(PageJsDataObj.sublist[2])
            },
            Getdqshi: function (Intstr) {
                var shi=[];
                for (var i = 0; i < PageJsDataObj.diquchaxun.length; i++) {
                    if (PageJsDataObj.diquchaxun[i].CityId == Intstr) {
                        shi.push(PageJsDataObj.diquchaxun[i]);
                    }
                }
                PageJsDataObj.diquchaxun=shi;
                PageJsDataObj.Getdqqu(PageJsDataObj.sublist[3])
            },
            Getdqqu: function (Intstr) {
                var qu=[];
                for (var i = 0; i < PageJsDataObj.diquchaxun.length; i++) {
                    if (PageJsDataObj.diquchaxun[i].CountyId == Intstr) {
                        qu.push(PageJsDataObj.diquchaxun[i]);
                    }
                }
                PageJsDataObj.diquchaxun=qu;
            },
            Getdiqu: function () {
                $("#ddlqiuchang").html("");
                PageJsDataObj.Getdqgj(PageJsDataObj.sublist[0]);
                var stropen="<option value=\"-1\">请选择</option>";
                for (var i = 0; i < PageJsDataObj.diquchaxun.length; i++) {
                   stropen+="<option value=\""+PageJsDataObj.diquchaxun[i].Id+"\">"+PageJsDataObj.diquchaxun[i].FieldName+"</option>";
                }
                $("#ddlqiuchang").append(stropen);   
            },
            Getqcdis: function () {
                PageJsDataObj.objval = $("#<%=droptypes.ClientID %>").val();
                if (PageJsDataObj.objval == "1" || PageJsDataObj.objval == "2") {
                    $("#qiuchangtr").removeClass("yingcang");
                    $("#<%=txtAddress.ClientID %>").attr("readonly","readonly"); 
                }
                else {
                    $("#qiuchangtr").addClass("yingcang");
                    $("#<%=txtAddress.ClientID %>").attr("readonly",""); 
                }

            },
            Getadname:function(strval){
              if(strval!="-1")
              {
                 $("#<%=txtAddress.ClientID%>").val(dizhiarry[strval]);
              }
            },
            Initlistload:function(){
              if (PageJsDataObj.changdilist!=null) {
                  for (var i = 0; i < PageJsDataObj.changdilist.length; i++) {
                      dizhiarry[PageJsDataObj.changdilist[i].Id]=PageJsDataObj.changdilist[i].Address;
                  }
              }
            },
            InitqiuchangLoad:function(){
              PageJsDataObj.sublist = ['<%=CId %>', '<%=PId %>', '<%=CSId %>','<%=AId %>'];
              PageJsDataObj.Getdiqu();
              $("#ddlqiuchang").val('<%=SiteID %>');
            },
            PageInit: function () {
                PageJsDataObj.CreatePlanEdit();
                PageJsDataObj.Getqcdis();
                PageJsDataObj.InitqiuchangLoad();
                PageJsDataObj.Initlistload();
            },
            BindBtn: function () {
                $("#btnCanel").unbind("click").click(function () {
                    window.history.go(-1);
                    return false;
                });
                $(".seldiqu").change(function () {
                    PageJsDataObj.Getqcdis();
                    if (PageJsDataObj.objval == "1" || PageJsDataObj.objval == "2")
                    {
                        PageJsDataObj.sublist = [$("#ddlCountry").val(), $("#ddlProvince").val(), $("#ddlCity").val(),$("#ddlArea").val()];
                        PageJsDataObj.Getdiqu();
                    }
                });
                $("#ddlqiuchang").change(function () {
                    var objvl=$("#ddlqiuchang").val();
                    PageJsDataObj.Getadname(objvl);
                });
                $("#<%=droptypes.ClientID %>").change(function () {
                    PageJsDataObj.Getqcdis();
                });
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
