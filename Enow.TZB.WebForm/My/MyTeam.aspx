<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberMaster.Master"
    AutoEventWireup="true" CodeBehind="MyTeam.aspx.cs" Inherits="Enow.TZB.WebForm.My.MyTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Right" runat="server">
    <div class="user_box">
        <h3>
            我的球队</h3>
        <div class="bisai_Tab">
            <div class="bs_list">
                <ul>
                    <li>
                        <asp:Literal ID="ltrImg" runat="server"></asp:Literal>
                        <dl>
                            <dt>
                                <asp:Literal ID="ltrName" runat="server"></asp:Literal></dt>
                            <dd class="date">
                                <asp:Literal ID="ltrDate" runat="server"></asp:Literal></dd>
                            <dd class="mt10">
                                <asp:Literal ID="ltrTeamInfo" runat="server"></asp:Literal></dd>
                        </dl>
                    </li>
                </ul>
            </div>
            <div class="chongzhi_table mt20">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>
                            加入日期
                        </th>
                        <th>
                            球员角色
                        </th>
                        <th>
                            姓名
                        </th>
                        <th>
                            队内位置
                        </th>
                        <th>
                            球衣号码
                        </th>
                        <th>
                            状态
                        </th>
                        <th>
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server" OnItemDataBound="InitOperation">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("IssueTime","{0:yyyy年MM月dd日}") %>
                                </td>
                                <td>
                                    <%#(Enow.TZB.Model.EnumType.球员角色)(int)Eval("RoleType") %>
                                </td>
                                <td>
                                    <%#Eval("ContactName") %>
                                </td>
                                <td>
                                    <%#Eval("DNWZ") %>
                                </td>
                                <td>
                                    <%#Eval("DNQYHM") %>
                                </td>
                                <td>
                                    <%#(Enow.TZB.Model.EnumType.球员审核状态)(int)Eval("State") %>
                                </td>
                                <td>
                                    <asp:Literal ID="ltrOperation" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div class="cent mt20">
                    <asp:Literal ID="ltrDisband" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;<asp:PlaceHolder
                        ID="phHide" runat="server" Visible="false"><a href="updateTeam.aspx" class="green_btn">
                            修改球队信息</a></asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>
    <link href="/css/boxy.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script language="javascript" type="text/javascript">
        var PageJsDataObj = {
            Query: {/*URL参数对象*/
                OpenId: ''
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
                    url: '/My',
                    title: "",
                    width: "360px",
                    height: "320px"
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
            Check: function (MemberId) {
                var data = this.DataBoxy();
                data.url += '/TeamerCheck.aspx?';
                //data.title = '队员审核';
                data.url += $.param({
                    MemberId: MemberId,
                    doType: "Check"
                });
                this.ShowBoxy(data);
            },
            Update: function (MemberId) {
                var data = this.DataBoxy();
                data.url += '/UpdateTeamMember.aspx?';
                //data.title = '修改信息';
                data.url += $.param({
                    MemberId: MemberId,
                    doType: "Update"
                });
                this.ShowBoxy(data);
            },
            Transfer: function (MemberId, MemberName) {
                if (window.confirm("确定将队长转让给  " + MemberName + "？")) {
                    var data = this.DataBoxy();
                    data.url += "/MyTeam.aspx?";
                    data.url += $.param({
                        MemberId: MemberId,
                        doType: "transfer"
                    });
                    window.location.href = data.url;
                }
            },
            Disband: function (TeamId) {
                var data = this.DataBoxy();
                data.url += "/DisbandReason.aspx?";
                data.url += $.param({
                teamId:TeamId,
                doType:"Disband"
                });
                this.ShowBoxy(data);
            },
            BindBtn: function () {
                //审核
                $(".MemberCheck").click(function () {
                    var MemberId = $(this).attr("DataId");
                    PageJsDataObj.Check(MemberId);
                    return false;
                });
                //修改
                $(".MemberUpate").click(function () {
                    var MemberId = $(this).attr("DataId");
                    PageJsDataObj.Update(MemberId);
                    return false;
                });
                //队长转让
                $(".CaptainTransfer").click(function () {
                    var MemberId = $(this).attr("dataId");
                    var MemberName = $(this).attr("data_name");
                    PageJsDataObj.Transfer(MemberId, MemberName);
                    return false;
                });
                //解散申请
                $("#disband").click(function(){
                var teamId=$(this).attr("data_id");
                PageJsDataObj.Disband(teamId);
                return false;
                });
            },
            PageInit: function () {
                //绑定功能按钮
                this.BindBtn();
            }
        }
        $(function () {
            PageJsDataObj.PageInit();
            PageJsDataObj.BindClose();
            return false;
        })
    </script>
</asp:Content>
