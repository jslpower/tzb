<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.TZB.WebForm.My.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
       <div class="user_headbox">
       
            <div class="user_box01">
               <a href="Update.aspx" class="user_a">修改信息</a>
               <div class="user_tx floatL"><asp:Literal ID="ltrPhoto" runat="server"></asp:Literal></div>
               <p>
                 <span class="user_name"><asp:Label ID="lblUserName" runat="server"></asp:Label></span>
                 <span class="user_grow">
                    <span class="user_grow_bg">
                        <span class="user_grow_state" style="width:80px;"><asp:Label ID="lblHonorNumber" runat="server"></asp:Label></span>
                    </span>
                    <span class="user_grow_name"><asp:Literal ID="ltrhonorName" runat="server"></asp:Literal></span>
                 </span>
               </p>
               
               <p class="mt10"><img src="/images/user-dj01.gif">&nbsp;<asp:Label ID="lblTitle" runat="server"></asp:Label></p>
               
               <div class="mt10 fixed">
               
                  <div class="user_info">
                      <ul class="fixed">
                          <li class="Rline"><em><asp:Label ID="lblIntNo" runat="server"></asp:Label></em>积分</li>
                          <li><em><asp:Label ID="lblCurrNo" runat="server"></asp:Label></em>铁丝币</li>
                      </ul>
                  </div>
                  <!--
                  <div class="user_info_a"><a href="Recharge.aspx">充值</a></div>-->
                  
               </div>
               
            </div>
            
            <div class="user_tip mt10"><strong>我加入的球队</strong><asp:Label ID="lblTeam" runat="server"></asp:Label></div>
            
            <div class="user_tip mt10"><strong>我的球场位置</strong><asp:Label ID="lblPostion" runat="server"></asp:Label></div>
            
       </div>
       
       <div class="user_box mt10" style="display:none;">
       
         <%-- <h3>会员特色</h3>
          
          <div class="user_ts">
             <ul class="fixed">
                 <li><img src="<%=ImgUrl%>/WX/images/touxian/tx_01.png"><p><strong>铁矿石</strong></p><p>荣誉值0-50</p></li>
                 <li><img src="<%=ImgUrl%>/WX/images/touxian/tx_02.png"><p><strong>生铁</strong></p><p>荣誉值50-200</p></li>
                 <li><img src="<%=ImgUrl%>/WX/images/touxian/tx_03.png"><p><strong>熟铁</strong></p><p>荣誉值200-500</p></li>
                 <li><img src="<%=ImgUrl%>/WX/images/touxian/tx_04.png"><p><strong>钢铁</strong></p><p>荣誉值500-1000</p></li>
                 <li><img src="<%=ImgUrl%>/WX/images/touxian/tx_05.png"><p><strong>钨铁</strong></p><p>荣誉值1000-2000</p></li>
                 <li><img src="<%=ImgUrl%>/WX/images/touxian/tx_06.png"><p><strong>合金铁</strong></p><p>荣誉值2000-5000</p></li>
                 <li><img src="<%=ImgUrl%>/WX/images/touxian/tx_07.png"><p><strong>超合金铁</strong></p><p>荣誉值>5000</p></li>
             </ul>
          </div>--%>
       
       </div>
                 
     
</asp:Content>
