<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gathers_Step2.aspx.cs" Inherits="Enow.TZB.Web.WX.Team.Gathers_Step2" %>
<%@ Register src="../UserControls/UserHome.ascx" tagname="UserHome" tagprefix="uc1" %>
<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

<title>约战队员选择</title>
<link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen"/>
<script language="javascript" type="text/javascript" src="/Js/jquery-1.10.2.min.js"></script>
 <script type="text/javascript">
     $(function () {
         var memberid = "";
         $(".gray").click(function () {
           
             if ($(".green").attr("data") == "" || typeof( $(".green").attr("data")) == "undefined") {
                 memberid += $(this).attr("data") + ",";
                 
             }
             else {
                 memberid = $(".green").attr("data") + ",";
                 memberid += $(this).attr("data") + ",";
             }
             $(this).html('已出战')
             $(this).removeClass("gray");
            
             $("#hdMemberId").val(memberid);
         })

        
     })
    </script>
</head>
<body>
    <uc1:UserHome ID="UserHome1" Userhometitle="约战队员选择" runat="server" />
    <form id="form1" runat="server">
      <input id="hdMemberId" type="hidden" runat="server"/>
    <div class="warp">

   <!----必战不可选  其他的单击一次变换一次 出战时橙色 不出战时灰色--------->
   
   <div class="qiu_list player_list mt10">
   
            <ul>
                <asp:Repeater ID="rptList" runat="server" OnItemDataBound="InitOperation">
                    <ItemTemplate>

      
         <li>
            <div class="item-img">
            <img alt=""  src="<%#Eval("HeadPhoto")%>"/>
            </div>
            <div class="item-box">
            <asp:Literal ID="ltrOperation" runat="server"></asp:Literal>
               
                <dl>
                    <dt><%#Eval("ContactName")%></dt>
                    <dd><span class="bg_red"><%#(Enow.TZB.Model.EnumType.球员角色)Convert.ToInt32(Eval("RoleType"))%></span></dd>
                     <dd>
                                        <%#Eval("DNQYHM")%>号</dd>
                                         <dd><%#Eval("DNWZ")%> </dd>
                                        <dd>
                                        <%#Eval("IssueTime","{0:yyyy-MM-dd}")%></dd>
                    <dd class="txt">  
                    <%#Eval("JRYS")%>
                                  </dd>
                </dl>
            </div>
         </li>
         
         </ItemTemplate>
        </asp:Repeater>

         
       
        
        
      
      </ul>
   </div>

   <div class="foot_fixed">
           <div class="foot_box">
                  <div class="paddL10 paddR10">
                <asp:Button CssClass="basic_btn" ID="btnSave" runat="server" Text="发起约战" OnClick="btnSave_Click" />
                 <%-- <a href="yuezhan_step3.html" class="basic_btn"></a>--%>
                  </div>
           </div>
   </div>

   
</div>

    </form>
</body>
</html>
