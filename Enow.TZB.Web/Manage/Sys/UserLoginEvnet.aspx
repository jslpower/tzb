<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" AutoEventWireup="true" CodeBehind="UserLoginEvnet.aspx.cs" Inherits="Enow.TZB.Web.Manage.Sys.UserLoginEvnet" %>
<%@ Register Assembly="Enow.TZB.Utility" Namespace="Enow.TZB.Utility.ExportPageSet"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
<link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/js/jquery.boxy.js"></script>
<!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->
<script type="text/javascript" src="/js/jquery.blockUI.js"></script>
<script type="text/javascript" src="/js/moveScroll.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
<div class="listbox">
      <div class="tablehead">
        <div class="pages">
            <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
        </div>
      </div>
      <!--列表表格-->
      <div class="tablelist-box">
      <table width="100%"  id="liststyle">
          <tr>
            <th align="center" >编号</th>
            <th align="left" >操作人</th>
            <th align="center" >日志内容</th>
            <th align="center" >操作IP</th>
            <th align="center" >操作时间</th>
          </tr>
      <asp:Repeater ID="rptList" runat="server">
      <ItemTemplate><tr>
            <td align="center"><%#Container.ItemIndex+1 %></td>
            <td align="center"><%#Eval("OperatorName")%></td>
            <td align="left"><%#Eval("EventTitle")%></td>
            <td align="center"><%#Eval("Ip")%></td>
            <td align="center"><%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm:ss}")%></td>
          </tr></ItemTemplate>
      </asp:Repeater>
      <asp:PlaceHolder ID="phNoData" runat="server" Visible="false">
      <tr><td colspan="5" align="center">暂无数据</td></tr></asp:PlaceHolder>
      </table>
      </div>
      <!--列表结束-->
      <div class="tablehead botborder">
        <div class="pages"><cc1:ExportPageInfo ID="ExportPageInfo2" runat="server" /></div>
      </div>
    </div>
  </div>
  <script type="text/javascript">
      var PageJsDataObj = {
          Query: {/*URL参数对象*/
              sl: ''
          },
          PageInit: function () {
              //当列表页面出现横向滚动条时使用以下方法 $("需要滚动最外层选择器").moveScroll();
              $('.tablelist-box').moveScroll();
          }
      }
      $(function () {
          PageJsDataObj.PageInit();
          return false;
      })
    </script>
</asp:Content>
