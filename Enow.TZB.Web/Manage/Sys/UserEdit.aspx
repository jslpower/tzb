<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="Enow.TZB.Web.Manage.Sys.UserEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta charset="utf-8" content="IE=EmulateIE8" http-equiv="X-UA-Compatible" />
<title>修改</title>
<link href="/Css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
<link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
<script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
<script src="/Js/CitySelect.js" type="text/javascript"></script>
<script src="/Js/TableUtil.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="contentbox">
     <div class="firsttable">
       <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <th width="150" height="40" align="right">真实姓名：</th>
            <td>
                <asp:Literal ID="ltrContactName" runat="server"></asp:Literal>
              </td>
          </tr>          
          <tr>
            <th width="150" height="40" align="right">用户名：</th>
            <td>
                <asp:Literal ID="ltrUserName" runat="server"></asp:Literal>
              </td>
          </tr>
          <tr>
                <th width="150" height="40" align="right">
                    联系电话：
                </th>
                <td>
                    <asp:TextBox ID="txtTel" CssClass="input-txt formsize180" runat="server"></asp:TextBox><span
                        class="fontred">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTel"
                        ErrorMessage="请填写联系电话！">*</asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr>
            <th width="150" height="40" align="right">用户角色：</th>
            <td>
                <asp:DropDownList ID="ddlRole" runat="server">
                </asp:DropDownList>
              </td>
          </tr>
          <tr>
            <th width="150" height="40" align="right">用户密码：</th>
            <td>
                <asp:TextBox ID="txtPwd" MaxLength="50" CssClass="input-txt formsize180" 
                    runat="server" TextMode="Password"></asp:TextBox>如不修改请置空</td>
          </tr>
          <tr>
            <th width="150" height="40" align="right">重复密码：</th>
            <td>
                <asp:TextBox ID="txtPwd2" MaxLength="50" CssClass="input-txt formsize180" 
                    runat="server" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtPwd" ControlToValidate="txtPwd2" 
                    ErrorMessage="重复密码必须与用户密码一致">*</asp:CompareValidator>
              </td>
          </tr>
          <tr>
                <th align="right">
                    是否管理所有城市：
                </th>
                <td>
                    <asp:CheckBox ID="cbIsAllCity" runat="server" Text="管理所有城市" />
                </td>
            </tr>
            <tr id="trCity">
                <th align="right">
                    管理的城市：</th>
                <td><table border="0" cellspacing="0" cellpadding="0" width="100%" align="center" id="tblGoods"
                class="table_C autoAdd">
                <tbody>
                    <tr><th>国家</th><th>省份</th><th>城市</th><th>县区</th><th>操作</th></tr>
                    <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                    <tr class="tempRow"><td><select id="ddlCountry<%#Container.ItemIndex %>" name="ddlCountry" >
                    </select></td><td><select id="ddlProvince<%#Container.ItemIndex %>" name="ddlProvince">
                    </select></td><td><select id="ddlCity<%#Container.ItemIndex %>" name="ddlCity">
                    </select></td><td><select id="ddlArea<%#Container.ItemIndex %>" name="ddlArea">
                    </select></td><td><div class="caozuo">
                                <ul>
                                    <li><s class="add"></s><a class="addbtnFinaPlan" href="javascript:void(0)"><span>添加</span></a></li>
                                    <li><s class="delete"></s><a class="delbtnFinaPlan" href="javascript:void(0)"><span>
                                        删除</span></a></li>
                                </ul>
                            </div></td></tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="phNoData" runat="server">
                    <tr class="tempRow"><td><select id="ddlCountry" name="ddlCountry" >
                    </select></td><td><select id="ddlProvince" name="ddlProvince">
                    </select></td><td><select id="ddlCity" name="ddlCity">
                    </select></td><td><select id="ddlArea" name="ddlArea">
                    </select></td><td><div class="caozuo">
                                <ul>
                                    <li><s class="add"></s><a class="addbtnFinaPlan" href="javascript:void(0)"><span>添加</span></a></li>
                                    <li><s class="delete"></s><a class="delbtnFinaPlan" href="javascript:void(0)"><span>
                                        删除</span></a></li>
                                </ul>
                            </div></td></tr>
                            </asp:PlaceHolder></tbody></table></td>
            </tr>
          </table>
       <div class="Basic_btn fixed">
          <ul>
               <li>
                   <asp:LinkButton ID="linkBtnSave" runat="server" onclick="linkBtnSave_Click">保 存 >></asp:LinkButton>
               </li>
               <li><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()" hidefocus="true">关 闭 >></a></li>
          </ul>
          <div class="hr_10"></div>
        </div></div>
	  </div><asp:ValidationSummary 
              ID="ValidationSummary1" runat="server" ShowMessageBox="True" 
              ShowSummary="False" />
    </form>
    <script type="text/javascript">
        $(function () {
            $("#<%=cbIsAllCity.ClientID %>").change(function () {
                var bischecked = $('#<%=cbIsAllCity.ClientID %>').is(':checked');
                if (bischecked) {
                    $("#trCity").hide();
                } else { $("#trCity").show(); }
            });
            $("#tblGoods").autoAdd({ tempRowClass: "tempRow", addButtonClass: "addbtnFinaPlan", delButtonClass: "delbtnFinaPlan", addCallBack: RowCallBack, delCallBack: RowCallBack });
        })
        <%=strJs %>
        function RowCallBack(tr) {
            var $tr = tr;
            $("#tblGoods").find(".tempRow").each(function (index, domEle) {
                $(this).find("select[name='ddlCountry']").attr("id", "ddlCountry" + index);
                $(this).find("select[name='ddlProvince']").attr("id", "ddlProvince" + index);
                $(this).find("select[name='ddlCity']").attr("id", "ddlCity" + index);
                $(this).find("select[name='ddlArea']").attr("id", "ddlArea" + index);
            });
            pcToobar.init({
                gID: "#" + $tr.find("select[name='ddlCountry']").attr("id"),
                pID: "#" + $tr.find("select[name='ddlProvince']").attr("id"),
                cID: "#" + $tr.find("select[name='ddlCity']").attr("id"),
                xID: "#" + $tr.find("select[name='ddlArea']").attr("id"),
                comID: '',
                gSelect: '',
                pSelect: '',
                cSelect: '',
                xSelect: ''
            });
            //alert($tr.html());
            //alert($("#tblGoods").html());
        }
        function sumIndex() {
            return $("#tblGoods").find(".tempRow").length
        }
    </script>
</body>
</html>
