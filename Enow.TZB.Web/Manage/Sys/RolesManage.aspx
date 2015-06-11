<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="RolesManage.aspx.cs" Inherits="Enow.TZB.Web.Manage.Sys.RolesManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
<link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/js/jquery.boxy.js"></script>
<!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->
<script type="text/javascript" src="/js/jquery.blockUI.js"></script>
<script type="text/javascript" src="/js/moveScroll.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
<div class="contentbox">
    <div class="searchbox fixed"><span class="searchT">
      <p> 角色名称：
        <asp:TextBox ID="txtKeyWord" CssClass="formsize120 input-txt" runat="server" />
        &nbsp;&nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="search-btn" 
                  CausesValidation="False" onclick="btnSearch_Click"/>
        </asp:Button>
      </p>
      </span> </div>
    <div class="listbox">
      <div id="tablehead" class="tablehead">
        <ul class="fixed">
          <li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_add add_gg">新增</a></li>
          <li class="line"></li>
          <li><s class="updateicon"></s><a href="#" hidefocus="true" class="toolbar_update">修改</a></li>
          <li class="line"></li>
          <li><s class="delicon"></s><a href="#" hidefocus="true" class="toolbar_delete" class="toolbar_delete">删除</a></li>
        </ul>
        <div class="pages"></div>
      </div>
      <!--列表表格-->
      <div class="tablelist-box">
      <table width="100%"  id="liststyle">
          <tr>
            <th width="30" class="thinputbg"><input type="checkbox" name="checkbox" id="checkbox" /></th>
            <th align="center" width="60px">编号</th>
            <th align="left" width="100px">角色名称</th>
            <th align="center">备注</th>
          </tr>
      <asp:Repeater ID="rptList" runat="server">
      <ItemTemplate><tr>
            <td align="center"><input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("Id") %>" /></td>
            <td align="center"><%#Container.ItemIndex+1 %></td>
            <td align="center"><%#Eval("RoleName")%></td>
            <td align="left"><%#Eval("Remark") %></td>
          </tr></ItemTemplate>
      </asp:Repeater>
      <asp:PlaceHolder ID="phNoData" runat="server" Visible="false">
      <tr><td colspan="4" align="center">暂无数据</td></tr></asp:PlaceHolder>
      </table>
      </div>
      <!--列表结束-->
      <div class="tablehead botborder">
        <script type="text/javascript">
            document.write(document.getElementById("tablehead").innerHTML);
            </script>
      </div>
    </div>
  </div>
  <script type="text/javascript">
      var PageJsDataObj = {
          Query: {/*URL参数对象*/
              sl: ''
          },
          //浏览弹窗关闭后刷新当前浏览数
          BindClose: function () {
              $("a[data-class='a_close']").unbind().click(function () {
                  window.location.reload();
                  return false;
              })
          },
          DataBoxy: function () {/*弹窗默认参数*/
              return {
                  url: '/Manage/Sys',
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
          GoAjax: function (url) {
              $.newAjax({
                  type: "post",
                  cache: false,
                  url: url,
                  dataType: "json",
                  success: function (result) {
                      if (result.result == "1") {
                          tableToolbar._showMsg(result.msg, function () {
                              window.location.href = window.location.href;
                          });
                      }
                      else { tableToolbar._showMsg(result.msg); }
                  },
                  error: function () {
                      //ajax异常--你懂得
                      tableToolbar._showMsg(tableToolbar.errorMsg);
                  }
              });
          },
          Add: function () {
              var data = this.DataBoxy();
              data.url += '/RoleAdd.aspx?';
              data.title = '添加角色';
              data.url += $.param({
                  sl: this.Query.sl,
                  doType: "add"
              });
              this.ShowBoxy(data);
          },
          Update: function (objsArr) {
              var data = this.DataBoxy();
              data.url += '/RoleEdit.aspx?';
              data.title = '修改角色信息';
              data.url += $.param({
                  sl: this.Query.sl,
                  doType: "update",
                  id: objsArr[0].find('input[type="checkbox"]').val()
              });
              this.ShowBoxy(data);
          },
          Delete: function (objsArr) {
              var list = new Array();
              for (var i = 0; i < objsArr.length; i++) {
                  list.push(objsArr[i].find("input[type='checkbox']").val());
              }
              if (list.length > 1) {
                  tableToolbar._showMsg("一次只能删除一条信息");
                  return;
              }
              var data = this.DataBoxy();
              data.url += "/RolesManage.aspx?";
              data.url += $.param({
                  sl: this.Query.sl,
                  doType: "delete",
                  id: list.join(",")
              });
              this.GoAjax(data.url);
          },
          BindBtn: function () {
              //添加
              $(".add_gg").click(function () {
                  PageJsDataObj.Add();
                  return false;
              })
              tableToolbar.init({
                  tableContainerSelector: "#liststyle", //表格选择器
                  objectName: "角色管理",
                  updateCallBack: function (objsArr) {
                      PageJsDataObj.Update(objsArr);
                      return false;
                  },
                  deleteCallBack: function (objsArr) {
                      PageJsDataObj.Delete(objsArr);
                  }
              });
          },
          PageInit: function () {
              //绑定功能按钮
              this.BindBtn();
              //当列表页面出现横向滚动条时使用以下方法 $("需要滚动最外层选择器").moveScroll();
              $('.tablelist-box').moveScroll();
          },
          CallBackFun: function (data) {
              newToobar.backFun(data);
          }

      }
      $(function () {
          PageJsDataObj.PageInit();
          PageJsDataObj.BindClose();
          return false;
      })
    </script>
</asp:Content>
