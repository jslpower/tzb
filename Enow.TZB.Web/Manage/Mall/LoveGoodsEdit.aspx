<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoveGoodsEdit.aspx.cs" Inherits="Enow.TZB.Web.Manage.Mall.LoveGoodsEdit" %>
<%@ Register src="~/UserControls/UploadControl.ascx" tagname="UploadControl" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>爱心义卖</title>
    
    
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
  <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
  <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
 
    <script type="text/javascript">

        $(function () {
            $("#isFreight :radio").click(function () {
                if ($(this).val() == "1") {
                    $("#trfreiht").css("display", "none");
                }
                else {

                    $("#trfreiht").css("display", "table-row");

                }
            })
            $("#txtStock").change(function () {
                if (parseInt($(this).val()) < 0) {
                    alert("库存数量应为大于0的数字!");
                }
            })

        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="clear">
    </div>
    <div class="contentbox">
        <div class="firsttable">
            <table  width="100%" cellspacing="0" cellpadding="0" border="0" id="tableInfo">

             <tr>
                    <th width="100">
                        商品类别：
                    </th>
                    <td>
                        <asp:DropDownList ID="dropyjclass" DataTextField="Rolename" DataValueField="Id"  runat="server"></asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <th width="100">
                        商品名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtGoodsName" runat="server"  errmsg="请填写商品名称!"  valid="required"  CssClass="inputtext formsize240"></asp:TextBox><span class="fontred">*</span>
                    </td>
                </tr>
               
                <tr>
                    <th width="100">
                        图片：
                    </th>
                    <td>
                       <uc2:UploadControl ID="UploadPhoto" runat="server" 
                                IsUploadMore="false" IsUploadSelf="true"
                                                
                                FileTypes="*.jpg;*.gif;*.jpeg;*.png;*.doc;*.ppt;*.xls;*.docx;*.pptx;*.xlsx;*.wps;*.pdf" /><span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        商品状态：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlStatu" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                  <th width="100">
                   精品推荐：
                  </th>
                  <td>
                      <asp:RadioButtonList ID="rdoIsGood" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" 
                           >
                      <asp:ListItem Value="1">推荐</asp:ListItem>
                      <asp:ListItem Value="0" Selected="True">不推荐</asp:ListItem>
                      </asp:RadioButtonList>
                  </td>
                </tr>
                <tr>
                    <th width="100">
                        生产厂商：
                    </th>
                    <td>
                        <asp:TextBox ID="txtProducer" runat="server" CssClass="inputtext formsize240"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        会员价：
                    </th>
                    <td>
                        <asp:TextBox ID="txtPrice" runat="server"  errmsg="请填写会员价!|会员价只能为数字!"  valid="required|isNumber" CssClass="inputtext formsize80" MaxLength="6"></asp:TextBox>&nbsp;元<span class="fontred">*</span>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        市场价：
                    </th>
                    <td>
                        <asp:TextBox ID="txtCurrencyPrice"  runat="server"  errmsg="请填写市场价!|市场价只能为数字!"  valid="required|isNumber"  CssClass="inputtext formsize80"  MaxLength="6"></asp:TextBox>&nbsp;元<span class="fontred">*</span>
                    </td>
                </tr>
                <tr id="isFreight">
                   <th width="100">是否包运费：</th>
                   <td>
                    <asp:RadioButtonList ID="rdoIsFreight" runat="server"  RepeatDirection="Horizontal" 
                            RepeatLayout="Flow">
                          
                      <asp:ListItem Value="1" >含运费</asp:ListItem>
                      <asp:ListItem Value="0" Selected="True">不含运费</asp:ListItem>
                      </asp:RadioButtonList>
                     
                   </td>
                </tr>
                <tr id="trfreiht" >
                  <th width="100">运费：</th>
                  <td>
                   <asp:TextBox ID="txtFreightFee" runat="server"  errmsg="运费应为数字!"  valid="isNumber"  CssClass="inputtext formsize80" MaxLength="6"></asp:TextBox>&nbsp;元
                  </td>
                </tr>
                <tr>
                    <th width="100">
                        库存数量：
                    </th>
                    <td>
                        <asp:TextBox ID="txtStock" runat="server" errmsg="库存数量应为大于0的数字!" valid="isNumber" CssClass="inputtext formsize80" Text="0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        产品介绍：
                    </th>
                    <td>
                        <asp:TextBox ID="txtIntroduce" runat="server" class="editText"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="Basic_btn fixed">
            <ul>
                <li>
                    <asp:LinkButton ID="linkBtnSave" runat="server" onclick="linkBtnSave_Click">保 存 >></asp:LinkButton>
                </li>
                <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"
                    hidefocus="true">关 闭 >></a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    var GoodsInfo = {
        InitEdit: function () {
            $("#tableInfo").find(".editText").each(function () {
                KEditer.init($(this).attr("id"), {
                    resizeMode: 0,
                    items: keSimple_HaveImage,
                    height: $(this).attr("data-height") == undefined ? "360px" : $(this).attr("data-height"),
                    width: $(this).attr("data-width") == undefined ? "660px" : $(this).attr("data-width")
                });
            });
        },
        CheckForm: function () {
            var form = $("form").get(0);
            KEditer.sync();
            return ValiDatorForm.validator(form, "alert");
        }
    };

    $(document).ready(function () {

        GoodsInfo.InitEdit();

        $("#<%= linkBtnSave.ClientID %>").click(function () {
            return GoodsInfo.CheckForm();
        });
    });
</script>
