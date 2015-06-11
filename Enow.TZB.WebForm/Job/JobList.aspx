<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="Enow.TZB.WebForm.Job.JobList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/Menu.ascx" TagName="Menu1" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/TopBar.ascx" TagName="Topbar1" TagPrefix="uc2" %>
<%@ Register Src="/UserControl/Footer.ascx" TagName="footer1" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta charset="utf-8">
    <meta name="keywords" content="" />
    <title>舵主招聘</title>
    <link href="/Css/style.css" rel="Stylesheet" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="/Js/ajaxpagecontrols.js"></script>
</head>
<body>
    <uc2:Topbar1 ID="topBar1" runat="server" />
    <uc1:Menu1 ID="menu1" runat="server" />
    <div class="head_line">
    </div>
    <div class="warp fixed">
        <div class="leftside">
            <h3>
                舵主招聘</h3>
        </div>
        <div class="rightsied">
            <div class="banner nei_banner">
                <div class="bannerimg">
                    <ul style="opacity: 1; top: 0px;">
                        <li><span><a href="#">
                            <img src="/images/n_banner.jpg"></a></span></li>
                        <li><span><a href="#">
                            <img src="/images/n_banner.jpg"></a></span></li>
                        <li><span><a href="#">
                            <img src="/images/n_banner.jpg"></a></span></li>
                    </ul>
                    <ol class="btn">
                        <li class="select">1</li>
                        <li class="">2</li>
                        <li class="">3</li>
                    </ol>
                </div>
            </div>
            <div class="news_box mt10">
                <div class="sideT fixed">
                    <h3>
                        舵主招聘</h3>
                    <div class="Rtit">
                        首页 > 舵主招聘</div>
                </div>
                <div class="news">
                    <table width="100%" border="0" cellpadding="0" cellspacing="1">
                        <tr>
                            <th width="20%" align="center">
                                招聘职位
                            </th>
                            <th width="30%" align="center">
                                地点
                            </th>
                            <th width="20%" align="center">
                                招聘开始日期
                            </th>
                            <th width="20%" align="center">
                                招聘结束日期
                            </th>
                            <th width="10%" align="center">
                            </th>
                        </tr>
                        <asp:Repeater ID="rptJobList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td height="35" align="center">
                                        <a href="JobDetail.aspx?id=<%#Eval("id")%>" target="_blank">
                                            <%#Enow.TZB.Utility.Utils.GetText2(Eval("JobName").ToString() ,20,true)%></a>
                                    </td>
                                    <td height="35" align="center">
                                    <%#Eval("CoutryName") + "-" + Eval("ProvinceName") + "-" + Eval("CityName")%>
                                    </td>
                                    <td height="35" align="center">
                                        <%#Convert.ToDateTime(Eval("StartDate")).ToString("yyyy-MM-dd") %>
                                    </td>
                                    <td height="35" align="center">
                                        <%#Convert.ToDateTime(Eval("EndDate")).ToString("yyyy-MM-dd") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div class="page" id="page" style="width: 100%; text-align: center; margin: 0px auto 0px;
                    clear: both; margin-top: 15px; margin-bottom: 15px;">
                </div>
            </div>
        </div>
    </div>
    <uc3:footer1 ID="footer1" runat="server" />
</body>
</html>
<script type="text/javascript">
    var pagingConfig = { pageSize: 10, pageIndex: 1, recordCount: 0, showPrev: true, showNext: true, showDisplayText: false, cssClassName: 'page' };
    $(function () {
        if (pagingConfig.recordCount > 0) AjaxPageControls.replace("page", pagingConfig);
    });
</script>
