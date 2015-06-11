<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxBazaarList.aspx.cs" Inherits="Enow.TZB.App.CommonPage.AjaxBazaarList" %>
  <asp:Repeater ID="rptList" runat="server">
      <ItemTemplate>
         <li> 
            <div class="item-img"><img src="<%#System.Configuration.ConfigurationManager.AppSettings["Uploadimgurl"]+Eval("GoodsPhoto").ToString()%>"/></div>
            <div class="item-box" style=" padding:0px;margin-left: 110px;">
               <div class="btn" style=" width:65px">
                   <a href="BazaarEdit.aspx?Id=<%#Eval("Id") %>" style=" width:65px" class="basic_btn">编辑</a>
                   <a href="javascript:void(0);" style=" width:65px" data-id="<%#Eval("Id") %>" class="basic_ybtn btndelete">删除</a>
                 </div>
                <dl>
                    <dt><%#Eval("GoodsName")%></dt>
                    <dd class="date">会员价:<%#Eval("MemberPrice")%></dd>
                    <dd class="date">修改时间：<%#Eval("IssueTime", "{0:yyyy-MM-dd}")%></dd>
                    <dd class="date">状态：<%#Eval("Status")!=null?((Enow.TZB.Model.义卖商品销售状态)Eval("Status")).ToString():""%></dd>
                </dl>
            </div>
         </li>
         </ItemTemplate>
      </asp:Repeater>
