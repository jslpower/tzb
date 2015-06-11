<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatchScheduleResultAdd.aspx.cs" MasterPageFile="~/MasterPage/FinaWinBackPage.Master" Inherits="Enow.TZB.Web.Manage.Match.MatchScheduleResultAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
  <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
  <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="/js/jquery.boxy.js"></script>
  <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->
  <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>
  <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
  <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
  <script src="/Js/TableUtil.js" type="text/javascript"></script>
  <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
  <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
  <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
  <div class="clear"> </div>
  <div class="contentbox">
    <div class="firsttable">
      <div class="firsttable"> <span class="firsttableT">战报管理</span>
        <table width="100%" cellspacing="0" cellpadding="0" border="0">
          <tr>
            <th width="100"> 赛事阶段：</th>
            <td><asp:Literal ID="ltrGameName" runat="server"></asp:Literal></td>
            <th width="100"> 球场：</th>
            <td><asp:Literal ID="ltrFieldName" runat="server"></asp:Literal></td>
          </tr>
          <tr>
            <th width="100"> 比赛时间：</th>
            <td colspan="3"><asp:Literal ID="ltrMatchTime" runat="server"></asp:Literal></td>
          </tr>
          <tr>
            <th width="100"> 主队：</th>
            <td><asp:Literal ID="ltrHomeTeamName" runat="server"></asp:Literal></td>
            <th> 客队：</th>
            <td><asp:Literal ID="ltrAwayTeamName" runat="server"></asp:Literal></td>
          </tr>
          <tr>
            <th width="100"> 战报状态： </th>
            <td colspan="3"><asp:Literal ID="ltrPublishTarget" runat="server"></asp:Literal></td>
          </tr>
          <tr>
            <th width="100"> 主队得分：</th>
            <td valign="top"><asp:TextBox ID="txtHomeScore" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写主队得分!|请填写主队得分" valid="required|isInt">0</asp:TextBox>分</td>
            <th>客队得分：</th>
            <td valign="top"><asp:TextBox ID="txtAwayScore" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写客队得分!|请填写客队得分" valid="required|isInt">0</asp:TextBox>分</td>
          </tr>
          <tr>
            <th width="100"> 主队战绩：</th>
            <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="100">总进球数：</td>
                  <td><asp:TextBox ID="txtHomeGoals" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写总进球数!|请填写总进球数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>上半场进球数：</td>
                  <td><asp:TextBox ID="txtHomeFirstGoals" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写上半场进球数!|请填写上半场进球数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>下半场进球数：</td>
                  <td><asp:TextBox ID="txtHomeSecondGoals" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写下半场进球数!|请填写下半场进球数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>加时赛进球数：</td>
                  <td><asp:TextBox ID="txtHomeOvertimePenaltys" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写加时赛进球数!|请填写加时赛进球数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>点球数：</td>
                  <td><asp:TextBox ID="txtHomePenaltys" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写点球数!|请填写点球数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>犯规数：</td>
                  <td><asp:TextBox ID="txtHomeFouls" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写犯规数!|请填写犯规数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>红牌数：</td>
                  <td><asp:TextBox ID="txtHomeReds" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写红牌数!|请填写红牌数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>黄牌数：</td>
                  <td><asp:TextBox ID="txtHomeYellows" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写黄牌数!|请填写黄牌数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
              </table></td>
            <th>客队战绩：</th>
            <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="100">总进球数：</td>
                  <td><asp:TextBox ID="txtAwayGoals" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写总进球数!|请填写总进球数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>上半场进球数：</td>
                  <td><asp:TextBox ID="txtAwayFirstGoals" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写上半场进球数!|请填写上半场进球数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>下半场进球数：</td>
                  <td><asp:TextBox ID="txtAwaySecondGoals" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写下半场进球数!|请填写下半场进球数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>加时赛进球数：</td>
                  <td><asp:TextBox ID="txtAwayOvertimePenaltys" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写加时赛进球数!|请填写加时赛进球数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>点球数：</td>
                  <td><asp:TextBox ID="txtAwayPenaltys" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写点球数!|请填写点球数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>犯规数：</td>
                  <td><asp:TextBox ID="txtAwayFouls" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写犯规数!|请填写犯规数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>红牌数：</td>
                  <td><asp:TextBox ID="txtAwayReds" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写红牌数!|请填写红牌数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
                <tr>
                  <td>黄牌数：</td>
                  <td><asp:TextBox ID="txtAwayYellows" runat="server" MaxLength="2" CssClass="input-txt formsize80"
                            errmsg="请填写黄牌数!|请填写黄牌数" valid="required|isInt">0</asp:TextBox></td>
                </tr>
              </table></td>
          </tr>
          <tr>
            <th width="100"> 主队个人战绩：</th>
            <td valign="top"><table border="0" cellspacing="0" cellpadding="0" width="100%" align="center" id="tblHomePersonalPoint"
                class="table_C autoAdd">
                <tbody>
                  <tr>
                    <th> 类型 </th>
                    <th> 队员 </th>
                    <th> 进球数 </th>
                    <th> 操作 </th>
                  </tr>
                  <asp:Repeater ID="rptList" runat="server">
                  <ItemTemplate>
                  <tr class="tempRow">
                    <td><select id="ddlHomeTypeId" name="ddlHomeTypeId">
                    <option value="0">请选择</option>
                            <%#TechnicalOptionList(Eval("TypeId").ToString())%>
                        </select></td>
                    <td><select id="ddlHomeTeamMember" name="ddlHomeTeamMember"><%#MatchMemberList(Eval("MatchId").ToString(), Eval("MatchTeamId").ToString(), Eval("MatchTeamMemberId").ToString())%></select></td>
                    <td><input type="text" id="txtHomeTechnicals" name="txtHomeTechnicals" maxlength="2" class="input-txt formsize40" errmsg="请填写分数!|请填写分数" valid="required|isInt" value="<%#Eval("Technicals") %>" /></td>
                    <td align="center"><div class="caozuo">
                        <ul>
                          <li><s class="add"></s><a class="addbtnFinaPlan" href="javascript:void(0)"><span>添加</span></a></li>
                          <li><s class="delete"></s><a class="delbtnFinaPlan" href="javascript:void(0)"><span> 删除</span></a></li>
                        </ul>
                      </div></td>
                  </tr>
                  </ItemTemplate>
                  </asp:Repeater>
                  <asp:PlaceHolder ID="phNoData" runat="server">
                  <tr class="tempRow">
                    <td><select id="ddlHomeTypeId" name="ddlHomeTypeId">
                    <option value="0">请选择</option>
                            <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.进球类型)))
                            %>
                        </select></td>
                    <td><select id="ddlHomeTeamMember" name="ddlHomeTeamMember"><%=HomeTeamMemberList %></select></td>
                    <td><input type="text" id="txtHomeTechnicals" name="txtHomeTechnicals" maxlength="2" class="input-txt formsize40" errmsg="请填写分数!|请填写分数" valid="required|isInt" value="0" /></td>
                    <td align="center"><div class="caozuo">
                        <ul>
                          <li><s class="add"></s><a class="addbtnFinaPlan" href="javascript:void(0)"><span>添加</span></a></li>
                          <li><s class="delete"></s><a class="delbtnFinaPlan" href="javascript:void(0)"><span> 删除</span></a></li>
                        </ul>
                      </div></td>
                  </tr>
                  </asp:PlaceHolder>
                </tbody>
              </table></td>
            <th>客队个人战绩：</th>
            <td valign="top"><table border="0" cellspacing="0" cellpadding="0" width="100%" align="center" id="tblAwayPersonalPoint"
                class="table_C autoAdd">
                <tbody>
                  <tr>
                    <th> 类型 </th>
                    <th> 队员 </th>
                    <th> 进球数 </th>
                    <th> 操作 </th>
                  </tr>
                  <asp:Repeater ID="rptList2" runat="server">
                  <ItemTemplate>
                  <tr class="tempRow">
                    <td><select id="ddlAwayTypeId" name="ddlAwayTypeId">
                    <option value="0">请选择</option>
                    <%#TechnicalOptionList(Eval("TypeId").ToString())%>
                        </select></td>
                    <td><select id="ddlAwayTeamMember" name="ddlAwayTeamMember"><%#MatchMemberList(Eval("MatchId").ToString(), Eval("MatchTeamId").ToString(), Eval("MatchTeamMemberId").ToString())%></select></td>
                    <td>
                    <input type="text" id="txtAwayTechnicals" name="txtAwayTechnicals" maxlength="2" class="input-txt formsize40" errmsg="请填写分数!|请填写分数" valid="required|isInt" value="<%#Eval("Technicals") %>" /></td>
                    <td align="center"><div class="caozuo">
                        <ul>
                          <li><s class="add"></s><a class="addbtnFinaPlan" href="javascript:void(0)"><span>添加</span></a></li>
                          <li><s class="delete"></s><a class="delbtnFinaPlan" href="javascript:void(0)"><span> 删除</span></a></li>
                        </ul>
                      </div></td>
                  </tr>
                  </ItemTemplate></asp:Repeater>
                  <asp:PlaceHolder ID="phNoData2" runat="server">
                  <tr class="tempRow">
                    <td><select id="ddlAwayTypeId" name="ddlAwayTypeId">
                    <option value="0">请选择</option>
                            <%=Enow.TZB.Utility.Function.UtilsCommons.GetEnumDDL
                                                   (Enow.TZB.Utility.EnumObj.GetList(typeof(Enow.TZB.Model.EnumType.进球类型)))
                            %>
                        </select></td>
                    <td><select id="ddlAwayTeamMember" name="ddlAwayTeamMember"><%=AwayTeamMemberList%></select></td>
                    <td>
                    <input type="text" id="txtAwayTechnicals" name="txtAwayTechnicals" maxlength="2" class="input-txt formsize40" errmsg="请填写分数!|请填写分数" valid="required|isInt" value="0" /></td>
                    <td align="center"><div class="caozuo">
                        <ul>
                          <li><s class="add"></s><a class="addbtnFinaPlan" href="javascript:void(0)"><span>添加</span></a></li>
                          <li><s class="delete"></s><a class="delbtnFinaPlan" href="javascript:void(0)"><span> 删除</span></a></li>
                        </ul>
                      </div></td>
                  </tr>
                  </asp:PlaceHolder>
                </tbody>
              </table></td>
          </tr>
          <tr>
            <th width="100"> 战报标题：</th>
            <td colspan="3"><asp:TextBox ID="txtTitle" CssClass="input-txt formsize450" MaxLength="200" runat="server"></asp:TextBox></td>
          </tr>
          <tr>
            <th width="100"> 战报内容：</th>
            <td colspan="3"><asp:TextBox ID="txtContent" runat="server" class="editText" Width="85%"></asp:TextBox></td>
          </tr>
        </table>
      </div>
      <div class="Basic_btn fixed">
        <ul>
          <li> <a href="javascript:void(0);" id="btnSave" onclick="javascript:void(0);">保 存 &gt;&gt;</a></li>
          <li><a href="javascript:void(0);" onclick="Javascript: window.history.go(-1);">返 回 &gt;&gt;</a></li>
        </ul>
        <div class="hr_10"> </div>
      </div>
    </div>
  </div>
  <script type="text/javascript">
        var PageJsDataObj = {
            Data: {
                MId: '<%=Request.QueryString["MId"] %>',
                SMId: '<%=Request.QueryString["SMId"] %>',
                act: '<%=Request.QueryString["act"] %>',
                CId: '<%=Request.QueryString["CId"] %>',
                id: '<%=Request.QueryString["id"] %>'
            },
            DataBoxy: function () {/*弹窗默认参数*/
                return {
                    url: '',
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
            CreatePlanEdit: function () {
                KEditer.init("<%=txtContent.ClientID %>", { resizeMode: 1, items: keMore_HaveImage, height: "450px" });
            },
            HomeAddRowCallBack: function (tr) {
                var $tr = tr;
                $tr.find("input[name='txtHomeTechnicals']").val("0");
                return true;
            },
            AwayAddRowCallBack: function (tr) {
                var $tr = tr;
                $tr.find("input[name='txtAwayTechnicals']").val("0");
                return true;
            },
            CheckForm: function () {
                var form = $("form").get(0);
                return ValiDatorForm.validator(form, "alert");

            },
            Form: null,
            Save: function () {
                $("#btnSave").text("提交中...");
                KEditer.sync();
                PageJsDataObj.submit();
            },
            submit: function () {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "MatchScheduleResultAdd.aspx?dotype=save&" + $.param(PageJsDataObj.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function (ret) {
                        //ajax回发提示
                        if (ret.result != "0") {
                            tableToolbar._showMsg(ret.msg, function () {
                                if (document.referrer != "" && document.referrer != "undefined")
                                    window.location.href = document.referrer;
                                else
                                    window.location.href = "Standings.aspx?" + $.param(PageJsDataObj.Data);
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                            $("#btnSave").text("保 存");
                            PageJsDataObj.BindBtn();
                        }
                    },
                    error: function () {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        $("#btnSave").text("保 存");
                        PageJsDataObj.BindBtn();
                    }
                });
            },
            PageInit: function () {
                PageJsDataObj.CreatePlanEdit();
                $("#tblHomePersonalPoint").autoAdd({ tempRowClass: "tempRow", addButtonClass: "addbtnFinaPlan", delButtonClass: "delbtnFinaPlan",addCallBack: PageJsDataObj.HomeAddRowCallBack, });
                $("#tblAwayPersonalPoint").autoAdd({ tempRowClass: "tempRow", addButtonClass: "addbtnFinaPlan", delButtonClass: "delbtnFinaPlan",addCallBack: PageJsDataObj.AwayAddRowCallBack, });
            },
            BindBtn: function () {
            $("#btnSave").unbind("click").click(function () {
                    if (PageJsDataObj.CheckForm()) {
                        PageJsDataObj.Save();
                    }
                });
                $("#btnCanel").unbind("click").click(function () {
                    window.history.go(-1);
                    return false;
                })
            }
        }
        $(function () {
            PageJsDataObj.PageInit();
            PageJsDataObj.BindBtn();
        });
    </script> 
</asp:Content>
