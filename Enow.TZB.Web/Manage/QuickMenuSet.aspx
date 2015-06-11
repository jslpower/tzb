<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="QuickMenuSet.aspx.cs" Inherits="Enow.TZB.Web.Manage.QuickMenuSet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
<link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
<script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
<script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
<script src="/Js/TableUtil.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
<div class="contentbox">
    
       <div class="table_menu">
 				<ul class="fixed">
                	<li class="on"><s></s><a href="javascript:void(0)"><span>快捷菜单配置</span></a></li>
              </ul>                
        </div>
       <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td>
                <asp:Repeater id="dlOperatorPermission" runat="server" 
              onitemdatabound="dlOperatorPermission_ItemDataBound">
				<ItemTemplate>
					<table border="0" align="center" cellpadding="0" cellspacing="0">
						<tr>
							<td width="109" height="23" align="center" background="/Images/PerTopHeadLeft.gif">
							<span class="white"><strong><%# DataBinder.Eval(Container.DataItem,"MenuName") %></strong></span>
							</td>
							<td background="/Images/PerTopBack.gif" width="557"></td>
							<td width="14"><img src="/Images/PerTopRight.gif" width="14" height="23" /></td>
						</tr>
						<tr>
							<td colspan="3"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
									<tr>
										<td width="7" background="/Images/PerLeftBack.gif"></td>
										<td align="center" valign="top"><asp:DataList id="dlPermissionClass" runat="server" HorizontalAlign="Center" RepeatDirection="Horizontal"
				Width="98%" RepeatColumns="3" BorderWidth="0px" CellPadding="0" CellSpacing="0" ItemStyle-VerticalAlign="Top">
				<ItemTemplate>
				<table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td height="27" align="center"><span class="BlnFnt"><%# DataBinder.Eval(Container.DataItem,"ClassName") %></span></td>
            </tr>
            <tr>
              <td valign="top" align="left"><%# ShowOP(Convert.ToString(DataBinder.Eval(Container.DataItem,"MenuId")),Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Id"))) %></td>
            </tr>
          </table>          
				</ItemTemplate>
				</asp:DataList>
										</td>
										<td width="6" background="/Images/PerRightBack.gif"><img src="/Images/spacer.gif" width="1" height="1" /></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td colspan="3"><table width="100%" border="0" cellspacing="0" cellpadding="0">
									<tr>
										<td width="24" height="13"><img src="/Images/PerFootLeft.gif" width="24" height="13" /></td>
										<td background="/Images/PerFootBack.gif"><img src="/Images/spacer.gif" width="1" height="1" /></td>
										<td width="21"><img src="/Images/PerFootRight.gif" width="21" height="13" /></td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:Repeater></td>
          </tr>
		  </table>
       <div class="Basic_btn fixed">
          <ul>
               <li>
                   <asp:LinkButton ID="linkBtnSave" runat="server" onclick="linkBtnSave_Click">保 存 >></asp:LinkButton></li>
          </ul>
          <div class="hr_10"></div>
        </div>
       </div>
       <script type="text/javascript">
           var PageJsData = {
               //表单验证
               FormCheck: function () {
                   this.Form = $("#linkBtnSave").closest("form").get(0)
                   FV_onBlur.initValid(this.Form);
                   return ValiDatorForm.validator(this.Form, "alertspan");
               },
               Save: function () {
                   if (PageJsData.FormCheck()) {
                       //$("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                       return true;
                   } else { return false; }
               },
               BindBtn: function () {
                   $("#linkBtnSave").click(function () {
                       return PageJsData.Save();
                   })
                   $("#btnSave").attr("class", "").html("<s class=\"baochun\"></s>保 存");
               }
           }
           $(function () {
               PageJsData.BindBtn();
               //自动添加行
               $(".tableEdu").autoAdd({
                   tempRowClass: "IndicatorTR",
                   addButtonClass: "addbtn",
                   delButtonClass: "delbtn"
               })
           });
    </script>
</asp:Content>
