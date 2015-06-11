<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master"
    AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="Enow.TZB.WebForm.My.Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
    <div class="user_box">
        <h3>
            修改信息</h3>
        <div class="reg_form">
            <ul class="botline">
                <li><span class="name">用户名</span>
                   <asp:Literal ID="ltrContactName" runat="server"></asp:Literal></li>
                   <li><span class="name">用户昵称</span><asp:TextBox ID="txtNickName" runat="server" CssClass="input_bk formsize225"></asp:TextBox></li>
                    <li><span class="name">用户头像</span><asp:FileUpload ID="imgFileUpload" runat="server" CssClass="input_bk formsize120" /></li>
                <li><span class="name">所在城市</span>
                    <select name="ddlCountry" id="ddlCountry" runat="server">
                    </select><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                        ErrorMessage="请选择国家!" ControlToValidate="ddlCountry">*</asp:RequiredFieldValidator>
                    <select name="ddlProvince" id="ddlProvince" runat="server">
                    </select><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ErrorMessage="请选择省份!" ControlToValidate="ddlProvince">*</asp:RequiredFieldValidator>
                    <select id="ddlCity" name="ddlCity" runat="server">
                    </select><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ErrorMessage="请选择城市!" ControlToValidate="ddlCity">*</asp:RequiredFieldValidator>
                    <select id="ddlArea" name="ddlArea" runat="server">
                    </select><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                        ErrorMessage="请选择区县!" ControlToValidate="ddlArea">*</asp:RequiredFieldValidator>
                </li>
                <li><span class="name">性别</span>
                    <input name="radGender" type="radio" id="radMan" runat="server" value="1" />
                    男
                    <input name="radGender" type="radio" value="0" id="radWoman" runat="server" />
                    女</li>
                <li><span class="name">手机</span><asp:TextBox ID="txt_Phone" runat="server" CssClass="input_bk formsize225"
                    valid="required" errmsg="请填写手机号码!"></asp:TextBox>
                    <span class="font_yellow">*</span></li>
                <li id="liVerCode"><span class="name">验证码</span><asp:TextBox ID="txt_valCode" runat="server" CssClass="formsize100 input_bk"
                    valid="required" errmsg="请填写验证码!"></asp:TextBox><a href="#" class="code">获取验证码</a></li>
                <li><span class="name">邮箱</span><asp:TextBox ID="txt_Email" runat="server" CssClass="formsize225 input_bk"
                    valid="required" errmsg="请填写邮箱!"></asp:TextBox></li>
            </ul>
            <ul>
                <li><span class="name">真实姓名</span><asp:TextBox ID="txt_ContractName" runat="server"
                    CssClass="formsize225 input_bk" valid="required" errmsg="请填写真实姓名!"></asp:TextBox></li>
                <li><span class="name">身份证号</span><asp:TextBox ID="txt_PersonalId" runat="server"
                    CssClass="formsize225 input_bk" valid="required" errmsg="请填写身份证号!"></asp:TextBox></li>
                <li><span class="name">详细地址</span><asp:TextBox ID="txt_Address" runat="server" CssClass="formsize400 input_bk"
                    valid="required" errmsg="请填写详细地址!"></asp:TextBox>
                </li>
               
            </ul>
            <div class="reg_btn pb10 mt10">
                <asp:Button ID="btnSave" runat="server" Text="确认" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
      <script language="javascript" type="text/javascript">

          var shuoJiReg = /^(13|15|18|14|17)\d{9}$/;
          //获取注册验证码：data:{shouJi:"手机号码"}
          function getZhuCeYanZhengMa(obj) {
              var _v = { success: false, code: 0 };
              var shouji = $("#<%=txt_Phone.ClientID %>").val();
              if (shouji == "undefined" || shouji.length == 0) { alert("请输入手机号码"); return _v; }
              if (!shuoJiReg.test(shouji)) { alert("请输入正确的手机号码"); return _v; }
              $(obj).css({ color: "#dadada" }).text("验证码已发送");
              //alert(shouji);
              //alert("/ashx/GetVerCode.ashx?MobilePhone=" + shouji);
              $.ajax({
                  type: "GET",
                  url: "/Ashx/GetVerCode.ashx?MobilePhone=" + shouji,
                  dataType: "json",
                  cache: false,
                  async: false,
                  success: function (html) {
                      _v.code = response.result;
                      if (response.result == 1) {
                          //alert(response.obj);
                          _v.success = true;
                      } else {
                          alert(response.msg);
                      }
                  },
                  error: function (XMLHttpRequest, textStatus, errorThrown) {
                      alert(XMLHttpRequest.status);
                      alert(XMLHttpRequest.readyState);
                      alert(textStatus);
                      return;
                  }
              });
              return _v;
          }

          $(function () {
              pcToobar.init({
                  gID: "#<%=ddlCountry.ClientID %>",
                  pID: "#<%=ddlProvince.ClientID%>",
                  cID: "#<%=ddlCity.ClientID %>",
                  xID: "#<%=ddlArea.ClientID %>",
                  comId: '',
                  gSelect: "<%=CountryId %>",
                  pSelect: "<%=ProvinceID %>",
                  cSelect: "<%=CityId %>",
                  xSelect: "<%=AreaId %>"

              });

              $("#<%=txt_Phone.ClientID%>").mouseleave(function () {
                  if ($("#<%=txt_Phone.ClientID%>").val() == "<%=PhoneNumber %>") {
                      $("#liVerCode").hide();
                  } else {
                      $("#liVerCode").show();
                  }
             
              });

              if ($("#<%=ddlProvince.ClientID%>").find("option:selected").val() == "1") {
                  if ($(this).val() == "190" || $(this).val() == "191" || $(this).val() == "988") {
                      $("#liVerCode").hide();
                  } else {
                      $("#liVerCode").show();
                  }
              } else {
                  $("#liVerCode").hide();
              }

              $("#<%=ddlCountry.ClientID %>").change(function () {
                  if ($(this).val() != "1") {
                      $("#liVerCode").hide();
                  } else {
                      $("#liVerCode").show();
                  }
              });
              $("#<%=ddlProvince.ClientID%>").change(function () {
                  if ($("#<%=ddlProvince.ClientID%>").find("option:selected").val() == "1") {
                      if ($(this).val() == "190" || $(this).val() == "191" || $(this).val() == "988") {
                          $("#liVerCode").hide();
                      } else {
                          $("#liVerCode").show();
                      }
                  } else {
                      $("#liVerCode").hide();
                  }
              });
          });
      </script>
</asp:Content>
