<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShouYinJianPan.ascx.cs" Inherits="Enow.TZB.Web.ShouYin.ShouYinJianPan" %>
<%--收银键盘用户控位--%>
<ul>
    <li><a href="javascript:void(0)" id="shouyin_jianpan_xianjinshoukuan">散客现金收银</a></li>
    <li><a href="javascript:void(0)" id="shouyin_jianpan_tiesishoukuan">铁丝二维码收银</a></li>
    <li class="shoukuan"><a href="javascript:void(0)" id="shouyin_jianpan_guangfashoukuan">工商银行卡收银</a></li>
    <li><a href="javascript:void(0)" id="shouyin_jianpan_xianjintuikuan">现金退款</a></li>
    <li><a href="javascript:void(0)" id="shouyin_jianpan_tiesituikuan">二维码退款</a></li>
    <li class="quxiao"><a href="javascript:void(0)" id="shouyin_jianpan_quxiaoshouyin">收银主页</a></li>
    <li><a href="javascript:void(0)" id="shouyin_jianpan_tiesichongzhi">铁丝充值</a></li>
    <li><a href="/shouyin/dingdan.aspx">历史订单</a></li>
    <li><a href="/shouyin/liushui.aspx">收银流水</a></li>
</ul>

<script type="text/javascript">
    $(document).ready(function () {
        $("#shouyin_jianpan_xianjinshoukuan").click(function () { shouYin.xianJinShouKuan(); });
        $("#shouyin_jianpan_tiesishoukuan").click(function () { shouYin.tieSiShouKuan(); });
        $("#shouyin_jianpan_guangfashoukuan").click(function () { shouYin.guangFaShouKuan(); });
        $("#shouyin_jianpan_xianjintuikuan").click(function () { shouYin.xianJinTuiKuan(); });
        $("#shouyin_jianpan_tiesituikuan").click(function () { shouYin.tieSiTuiKuan(); });
        $("#shouyin_jianpan_quxiaoshouyin").click(function () { shouYin.quXiaoShouYin(); });
        $("#shouyin_jianpan_tiesichongzhi").click(function () { shouYin.tieSiChongZhi(); });
    })
</script>