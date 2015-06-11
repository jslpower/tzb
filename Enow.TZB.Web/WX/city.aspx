<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="city.aspx.cs" Inherits="Enow.TZB.Web.WX.city" %>

<%@ Register Src="UserControls/UserHome.ascx" TagName="UserHome" TagPrefix="uc1" %>
<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>城市选择</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <link href="css/city.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <style type="text/css">
        .yangshi{
         display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UserHome ID="UserHome1" Userhometitle="城市列表" runat="server" />
    <div class="search_head home_search">
        <div class="search_form">
            <input type="text" id="txtsousuo" class="input_txt floatL" value=""  placeholder="城市搜索"/>
            <input name="" type="button" id="btn_sel" class="input_btn icon_search_i floatR" />
        </div>
    </div>
    <div class="home_warp" style="margin-top: 90px;" id="ssd1">
        <div class="city_list">
            <ul>
                <li class="city_li">
                    <div class="city_group_title">
                        搜索结果</div>
                    <ul class="city_group_box">
                        <asp:Repeater ID="rptList" runat="server">
                            <ItemTemplate>
                            <li class="tiaozhan" data-cid="<%#Eval("CityId") %>"><%#Eval("Name")%></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
    <div class="home_warp" id="lbd1">
        <div class="city_list">
            <ul>
                <li class="city_li">
                    <div class="city_group_title">
                        A</div>
                    <ul class="city_group_box">
                        
                        <li class="tiaozhan" data-cid="490">鞍山市</li>
                        <li class="tiaozhan" data-cid="556">安庆市</li>
                        <li class="tiaozhan" data-cid="607">安阳市</li>
                        <li class="tiaozhan" data-cid="704">阿坝藏族羌族自治州</li>
                        <li class="tiaozhan" data-cid="710">安顺市</li>
                        <li class="tiaozhan" data-cid="737">阿里地区</li>
                        <li class="tiaozhan" data-cid="747">安康市</li>
                        <li class="tiaozhan" data-cid="778">阿克苏地区</li>
                        <li class="tiaozhan" data-cid="784">阿勒泰地区</li>
                        <li class="tiaozhan" data-cid="786">阿拉尔市</li>
                        <li class="tiaozhan" data-cid="789">澳门特别行政区</li>
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        B</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="459">保定市</li>
                        <li class="tiaozhan" data-cid="477">包头市</li>
                        <li class="tiaozhan" data-cid="483">巴彦淖尔市</li>
                        <li class="tiaozhan" data-cid="492">本溪市</li>
                        <li class="tiaozhan" data-cid="507">白山市</li>
                        <li class="tiaozhan" data-cid="509">白城市</li>
                        <li class="tiaozhan" data-cid="551">蚌埠市</li>
                        <li class="tiaozhan" data-cid="601">滨州市</li>
                        <li class="tiaozhan" data-cid="673">北海市</li>
                        <li class="tiaozhan" data-cid="678">百色市</li>
                        <li class="tiaozhan" data-cid="702">巴中市</li>
                        <li class="tiaozhan" data-cid="713">毕节地区</li>
                        <li class="tiaozhan" data-cid="719">保山市</li>
                        <li class="tiaozhan" data-cid="741">宝鸡市</li>
                        <li class="tiaozhan" data-cid="752">白银市</li>
                        <li class="tiaozhan" data-cid="776">博尔塔拉蒙古自治州</li>
                        <li class="tiaozhan" data-cid="777">巴音郭楞蒙古自治州</li>
                        <li class="tiaozhan" data-cid="793">北京市</li></ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        C</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="461">承德市</li>
                        <li class="tiaozhan" data-cid="462">沧州市</li>
                        <li class="tiaozhan" data-cid="468">长治市</li>
                        <li class="tiaozhan" data-cid="479">赤峰市</li>
                        <li class="tiaozhan" data-cid="500">朝阳市</li>
                        <li class="tiaozhan" data-cid="502">长春市</li>
                        <li class="tiaozhan" data-cid="528">常州市</li>
                        <li class="tiaozhan" data-cid="558">滁州市</li>
                        <li class="tiaozhan" data-cid="561">巢湖市</li>
                        <li class="tiaozhan" data-cid="564">池州市</li>
                        <li class="tiaozhan" data-cid="634">长沙市</li>
                        <li class="tiaozhan" data-cid="640">常德市</li>
                        <li class="tiaozhan" data-cid="643">郴州市</li>
                        <li class="tiaozhan" data-cid="666">潮州市</li>
                        <li class="tiaozhan" data-cid="682">崇左市</li>
                        <li class="tiaozhan" data-cid="686">成都市</li>
                        <li class="tiaozhan" data-cid="724">楚雄彝族自治州</li>
                        <li class="tiaozhan" data-cid="733">昌都地区</li>
                        <li class="tiaozhan" data-cid="775">昌吉回族自治州</li>
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        D</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="466">大同市</li>
                        <li class="tiaozhan" data-cid="489">大连市</li>
                        <li class="tiaozhan" data-cid="493">丹东市</li>
                        <li class="tiaozhan" data-cid="516">大庆市</li>
                        <li class="tiaozhan" data-cid="523">大兴安岭地区</li>
                        <li class="tiaozhan" data-cid="590">东营市</li>
                        <li class="tiaozhan" data-cid="599">德州市</li>
                        <li class="tiaozhan" data-cid="664">东莞市</li>
                        <li class="tiaozhan" data-cid="690">德阳市</li>
                        <li class="tiaozhan" data-cid="700">达州市</li>
                        <li class="tiaozhan" data-cid="728">大理白族自治州</li>
                        <li class="tiaozhan" data-cid="729">德宏傣族景颇族自治州</li>
                        <li class="tiaozhan" data-cid="731">迪庆藏族自治州</li>
                        <li class="tiaozhan" data-cid="759">定西市</li>
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        E</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="481">鄂尔多斯市</li>
                        <li class="tiaozhan" data-cid="625">鄂州市</li>
                        <li class="tiaozhan" data-cid="632">恩施土家族苗族自治州</li>
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        F</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="491">抚顺市</li>
                        <li class="tiaozhan" data-cid="496">阜新市</li>
                        <li class="tiaozhan" data-cid="559">阜阳市</li>
                        <li class="tiaozhan" data-cid="566">福州市</li>
                        <li class="tiaozhan" data-cid="584">抚州市</li>
                        <li class="tiaozhan" data-cid="653">佛山市</li>
                        <li class="tiaozhan" data-cid="674">防城港市</li>
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        G</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="581">赣州市</li>
                        <li class="tiaozhan" data-cid="648">广州市</li>
                        <li class="tiaozhan" data-cid="671">桂林市</li>
                        <li class="tiaozhan" data-cid="676">贵港市</li>
                        <li class="tiaozhan" data-cid="692">广元市</li>
                        <li class="tiaozhan" data-cid="699">广安市</li>
                        <li class="tiaozhan" data-cid="705">甘孜藏族自治州</li>
                        <li class="tiaozhan" data-cid="707">贵阳市</li>
                        <li class="tiaozhan" data-cid="762">甘南藏族自治州</li>
                        <li class="tiaozhan" data-cid="768">果洛藏族自治州</li>
                        <li class="tiaozhan" data-cid="792">高雄</li>
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        H</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="457">邯郸市</li>
                        <li class="tiaozhan" data-cid="464">衡水市</li>
                        <li class="tiaozhan" data-cid="476">呼和浩特市</li>
                        <li class="tiaozhan" data-cid="482">呼伦贝尔市</li>
                        <li class="tiaozhan" data-cid="501">葫芦岛市</li>
                        <li class="tiaozhan" data-cid="511">哈尔滨市</li>
                        <li class="tiaozhan" data-cid="514">鹤岗市</li>
                        <li class="tiaozhan" data-cid="521">黑河市</li>
                        <li class="tiaozhan" data-cid="532">淮安市</li>
                        <li class="tiaozhan" data-cid="538">杭州市</li>
                        <li class="tiaozhan" data-cid="542">湖州市</li>
                        <li class="tiaozhan" data-cid="549">合肥市</li>
                        <li class="tiaozhan" data-cid="552">淮南市</li>
                        <li class="tiaozhan" data-cid="554">淮北市</li>
                        <li class="tiaozhan" data-cid="557">黄山市</li>
                        <li class="tiaozhan" data-cid="602">荷泽市</li>
                        <li class="tiaozhan" data-cid="608">鹤壁市</li>
                        <li class="tiaozhan" data-cid="621">黄石市</li>
                        <li class="tiaozhan" data-cid="629">黄冈市</li>
                        <li class="tiaozhan" data-cid="637">衡阳市</li>
                        <li class="tiaozhan" data-cid="645">怀化市</li>
                        <li class="tiaozhan" data-cid="658">惠州市</li>
                        <li class="tiaozhan" data-cid="661">河源市</li>
                        <li class="tiaozhan" data-cid="679">贺州市</li>
                        <li class="tiaozhan" data-cid="680">河池市</li>
                        <li class="tiaozhan" data-cid="683">海口市</li>
                        <li class="tiaozhan" data-cid="725">红河哈尼族彝族自治州</li>
                        <li class="tiaozhan" data-cid="745">汉中市</li>
                        <li class="tiaozhan" data-cid="764">海东地区</li>
                        <li class="tiaozhan" data-cid="765">海北藏族自治州</li>
                        <li class="tiaozhan" data-cid="766">黄南藏族自治州</li>
                        <li class="tiaozhan" data-cid="767">海南藏族自治州</li>
                        <li class="tiaozhan" data-cid="770">海西蒙古族藏族自治州</li>
                        <li class="tiaozhan" data-cid="774">哈密地区</li>
                        <li class="tiaozhan" data-cid="781">和田地区</li>
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        I</div>
                    <ul class="city_group_box">
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        J</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="469">晋城市</li>
                        <li class="tiaozhan" data-cid="471">晋中市</li>
                        <li class="tiaozhan" data-cid="494">锦州市</li>
                        <li class="tiaozhan" data-cid="503">吉林市</li>
                        <li class="tiaozhan" data-cid="513">鸡西市</li>
                        <li class="tiaozhan" data-cid="518">佳木斯市</li>
                        <li class="tiaozhan" data-cid="541">嘉兴市</li>
                        <li class="tiaozhan" data-cid="544">金华市</li>
                        <li class="tiaozhan" data-cid="576">景德镇市</li>
                        <li class="tiaozhan" data-cid="578">九江市</li>
                        <li class="tiaozhan" data-cid="582">吉安市</li>
                        <li class="tiaozhan" data-cid="586">济南市</li>
                        <li class="tiaozhan" data-cid="593">济宁市</li>
                        <li class="tiaozhan" data-cid="610">焦作市</li>
                        <li class="tiaozhan" data-cid="626">荆门市</li>
                        <li class="tiaozhan" data-cid="628">荆州市</li>
                        <li class="tiaozhan" data-cid="654">江门市</li>
                        <li class="tiaozhan" data-cid="667">揭阳市</li>
                        <li class="tiaozhan" data-cid="750">嘉峪关市</li>
                        <li class="tiaozhan" data-cid="751">金昌市</li>
                        <li class="tiaozhan" data-cid="757">酒泉市</li>
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        K</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="604">开封市</li>
                        <li class="tiaozhan" data-cid="716">昆明市</li>
                        <li class="tiaozhan" data-cid="772">克拉玛依市</li>
                        <li class="tiaozhan" data-cid="779">克孜勒苏柯尔克孜自治州</li>
                        <li class="tiaozhan" data-cid="780">喀什地区</li>
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        L</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="463">廊坊市</li>
                        <li class="tiaozhan" data-cid="474">临汾市</li>
                        <li class="tiaozhan" data-cid="475">吕梁市</li>
                        <li class="tiaozhan" data-cid="497">辽阳市</li><li class="tiaozhan" data-cid="505">辽源市</li><li
                            class="tiaozhan" data-cid="531">连云港市</li><li class="tiaozhan" data-cid="548">丽水市</li><li
                                class="tiaozhan" data-cid="562">六安市</li><li class="tiaozhan" data-cid="573">龙岩市</li><li
                                    class="tiaozhan" data-cid="597">莱芜市</li><li class="tiaozhan" data-cid="598">临沂市</li><li
                                        class="tiaozhan" data-cid="600">聊城市</li><li class="tiaozhan" data-cid="605">洛阳市</li><li
                                            class="tiaozhan" data-cid="646">娄底市</li><li class="tiaozhan" data-cid="670">柳州市</li><li
                                                class="tiaozhan" data-cid="681">来宾市</li><li class="tiaozhan" data-cid="695">乐山市</li><li
                                                    class="tiaozhan" data-cid="706">凉山彝族自治州</li><li class="tiaozhan" data-cid="708">六盘水市</li><li
                                                        class="tiaozhan" data-cid="721">丽江市</li><li class="tiaozhan" data-cid="723">临沧市</li><li
                                                            class="tiaozhan" data-cid="732">拉萨市</li><li class="tiaozhan" data-cid="738">林芝地区</li><li
                                                                class="tiaozhan" data-cid="749">兰州市</li><li class="tiaozhan" data-cid="760">陇南市</li><li
                                                                    class="tiaozhan" data-cid="761">临夏回族自治州</li></ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        M</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="520">牡丹江市</li><li class="tiaozhan" data-cid="553">马鞍山市</li><li
                            class="tiaozhan" data-cid="656">茂名市</li><li class="tiaozhan" data-cid="659">梅州市</li><li
                                class="tiaozhan" data-cid="691">绵阳市</li><li class="tiaozhan" data-cid="697">眉山市</li></ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        N</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="525">南京市</li><li class="tiaozhan" data-cid="530">南通市</li><li
                            class="tiaozhan" data-cid="539">宁波市</li><li class="tiaozhan" data-cid="572">南平市</li><li
                                class="tiaozhan" data-cid="574">宁德市</li><li class="tiaozhan" data-cid="575">南昌市</li><li
                                    class="tiaozhan" data-cid="615">南阳市</li><li class="tiaozhan" data-cid="669">南宁市</li><li
                                        class="tiaozhan" data-cid="694">内江市</li><li class="tiaozhan" data-cid="696">南充市</li><li
                                            class="tiaozhan" data-cid="730">怒江傈僳族自治州</li><li class="tiaozhan" data-cid="736">那曲地区</li></ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        O</div>
                    <ul class="city_group_box">
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        P</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="498">盘锦市</li><li class="tiaozhan" data-cid="568">莆田市</li><li
                            class="tiaozhan" data-cid="577">萍乡市</li><li class="tiaozhan" data-cid="606">平顶山市</li><li
                                class="tiaozhan" data-cid="688">攀枝花市</li><li class="tiaozhan" data-cid="756">平凉市</li></ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        Q</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="456">秦皇岛市</li><li class="tiaozhan" data-cid="512">齐齐哈尔市</li><li
                            class="tiaozhan" data-cid="519">七台河市</li><li class="tiaozhan" data-cid="570">泉州市</li><li
                                class="tiaozhan" data-cid="587">青岛市</li><li class="tiaozhan" data-cid="663">清远市</li><li
                                    class="tiaozhan" data-cid="675">钦州市</li><li class="tiaozhan" data-cid="712">黔西南布依族苗族自治州</li><li
                                        class="tiaozhan" data-cid="714">黔东南苗族侗族自治州</li><li class="tiaozhan" data-cid="715">黔南布依族苗族自治州</li><li
                                            class="tiaozhan" data-cid="717">曲靖市</li><li class="tiaozhan" data-cid="758">庆阳市</li></ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        R</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="596">日照市</li><li class="tiaozhan" data-cid="735">日喀则地区</li></ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        S</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="454">石家庄市</li><li class="tiaozhan" data-cid="470">朔州市</li><li
                            class="tiaozhan" data-cid="488">沈阳市</li><li class="tiaozhan" data-cid="504">四平市</li><li
                                class="tiaozhan" data-cid="508">松原市</li><li class="tiaozhan" data-cid="515">双鸭山市</li><li
                                    class="tiaozhan" data-cid="522">绥化市</li><li class="tiaozhan" data-cid="524">上海市</li><li
                                        class="tiaozhan" data-cid="529">苏州市</li><li class="tiaozhan" data-cid="537">宿迁市</li><li
                                            class="tiaozhan" data-cid="543">绍兴市</li><li class="tiaozhan" data-cid="560">宿州市</li><li
                                                class="tiaozhan" data-cid="569">三明市</li><li class="tiaozhan" data-cid="585">上饶市</li><li
                                                    class="tiaozhan" data-cid="614">三门峡市</li><li class="tiaozhan" data-cid="616">商丘市</li><li
                                                        class="tiaozhan" data-cid="622">十堰市</li><li class="tiaozhan" data-cid="631">随州市</li><li
                                                            class="tiaozhan" data-cid="633">神农架</li><li class="tiaozhan" data-cid="638">邵阳市</li><li
                                                                class="tiaozhan" data-cid="649">韶关市</li><li class="tiaozhan" data-cid="650">深圳市</li><li
                                                                    class="tiaozhan" data-cid="652">汕头市</li><li class="tiaozhan" data-cid="660">汕尾市</li><li
                                                                        class="tiaozhan" data-cid="684">三亚市</li><li class="tiaozhan" data-cid="693">遂宁市</li><li
                                                                            class="tiaozhan" data-cid="722">思茅市</li><li class="tiaozhan" data-cid="734">山南地区</li><li
                                                                                class="tiaozhan" data-cid="748">商洛市</li><li class="tiaozhan" data-cid="785">石河子市</li></ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        T</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="455">唐山市</li><li class="tiaozhan" data-cid="465">太原市</li><li
                            class="tiaozhan" data-cid="480">通辽市</li><li class="tiaozhan" data-cid="499">铁岭市</li><li
                                class="tiaozhan" data-cid="506">通化市</li><li class="tiaozhan" data-cid="536">泰州市</li><li
                                    class="tiaozhan" data-cid="547">台州市</li><li class="tiaozhan" data-cid="555">铜陵市</li><li
                                        class="tiaozhan" data-cid="594">泰安市</li><li class="tiaozhan" data-cid="711">铜仁地区</li><li
                                            class="tiaozhan" data-cid="740">铜川市</li><li class="tiaozhan" data-cid="753">天水市</li><li
                                                class="tiaozhan" data-cid="773">吐鲁番地区</li><li class="tiaozhan" data-cid="783">塔城地区</li><li
                                                    class="tiaozhan" data-cid="787">图木舒克市</li><li class="tiaozhan" data-cid="790">台湾省</li><li
                                                        class="tiaozhan" data-cid="791">台南</li><li class="tiaozhan" data-cid="794">台北市</li><li
                                                            class="tiaozhan" data-cid="29383">天津市</li></ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        U</div>
                    <ul class="city_group_box">
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        V</div>
                    <ul class="city_group_box">
                    </ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        W</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="478">乌海市</li>
                        <li class="tiaozhan" data-cid="484">乌兰察布市</li>
                        <li class="tiaozhan" data-cid="526">无锡市</li><li class="tiaozhan" data-cid="540">温州市</li><li
                            class="tiaozhan" data-cid="550">芜湖市</li><li class="tiaozhan" data-cid="592">潍坊市</li><li
                                class="tiaozhan" data-cid="595">威海市</li><li class="tiaozhan" data-cid="620">武汉市</li><li
                                    class="tiaozhan" data-cid="672">梧州市</li><li class="tiaozhan" data-cid="726">文山壮族苗族自治州</li><li
                                        class="tiaozhan" data-cid="743">渭南市</li><li class="tiaozhan" data-cid="754">武威市</li><li
                                            class="tiaozhan" data-cid="771">乌鲁木齐市</li><li class="tiaozhan" data-cid="788">五家渠市</li></ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        X</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="458">邢台市</li>
                        <li class="tiaozhan" data-cid="473">忻州市</li>
                        <li class="tiaozhan" data-cid="485">兴安盟</li>
                        <li class="tiaozhan" data-cid="486">锡林郭勒盟</li>
                        <li class="tiaozhan" data-cid="527">徐州市</li>
                        <li class="tiaozhan" data-cid="565">宣城市</li>
                        <li class="tiaozhan" data-cid="567">厦门市</li>
                        <li class="tiaozhan" data-cid="579">新余市</li>
                        <li class="tiaozhan" data-cid="609">新乡市</li>
                        <li class="tiaozhan" data-cid="612">许昌市</li>
                        <li class="tiaozhan" data-cid="617">信阳市</li>
                        <li class="tiaozhan" data-cid="624">襄樊市</li>
                        <li class="tiaozhan" data-cid="627">孝感市</li>
                        <li class="tiaozhan" data-cid="630">咸宁市</li>
                        <li class="tiaozhan" data-cid="636">湘潭市</li>
                        <li class="tiaozhan" data-cid="647">湘西土家族苗族自治州</li>
                        <li class="tiaozhan" data-cid="727">西双版纳傣族自治州</li>
                        <li class="tiaozhan" data-cid="739">西安市</li>
                        <li class="tiaozhan" data-cid="742">咸阳市</li>
                        <li class="tiaozhan" data-cid="763">西宁市</li>
                        <li class="tiaozhan" data-cid="29383">香港特别行政区</li></ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        Y</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="467">阳泉市</li>
                        <li class="tiaozhan" data-cid="472">运城市</li>
                        <li class="tiaozhan" data-cid="495">营口市</li>
                        <li class="tiaozhan" data-cid="510">延边朝鲜族自治州</li>
                        <li class="tiaozhan" data-cid="517">伊春市</li>
                        <li class="tiaozhan" data-cid="533">盐城市</li>
                        <li class="tiaozhan" data-cid="534">扬州市</li>
                        <li class="tiaozhan" data-cid="580">鹰潭市</li>
                        <li class="tiaozhan" data-cid="583">宜春市</li>
                        <li class="tiaozhan" data-cid="591">烟台市</li>
                        <li class="tiaozhan" data-cid="623">宜昌市</li>
                        <li class="tiaozhan" data-cid="639">岳阳市</li>
                        <li class="tiaozhan" data-cid="642">益阳市</li>
                        <li class="tiaozhan" data-cid="644">永州市</li>
                        <li class="tiaozhan" data-cid="662">阳江市</li>
                        <li class="tiaozhan" data-cid="668">云浮市</li>
                        <li class="tiaozhan" data-cid="677">玉林市</li>
                        <li class="tiaozhan" data-cid="698">宜宾市</li>
                        <li class="tiaozhan" data-cid="701">雅安市</li>
                        <li class="tiaozhan" data-cid="718">玉溪市</li>
                        <li class="tiaozhan" data-cid="744">延安市</li>
                        <li class="tiaozhan" data-cid="746">榆林市</li>
                        <li class="tiaozhan" data-cid="769">玉树藏族自治州</li>
                        <li class="tiaozhan" data-cid="782">伊犁哈萨克自治州</li>
                        <li class="tiaozhan" data-cid="796">义乌市</li>
                        <li class="tiaozhan" data-cid="29383">银川</li></ul>
                </li>
                <li class="city_li">
                    <div class="city_group_title">
                        Z</div>
                    <ul class="city_group_box">
                        <li class="tiaozhan" data-cid="460">张家口市</li>
                        <li class="tiaozhan" data-cid="535">镇江市</li>
                        <li class="tiaozhan" data-cid="546">舟山市</li>
                        <li class="tiaozhan" data-cid="571">漳州市</li>
                        <li class="tiaozhan" data-cid="588">淄博市</li>
                        <li class="tiaozhan" data-cid="589">枣庄市</li>
                        <li class="tiaozhan" data-cid="603">郑州市</li>
                        <li class="tiaozhan" data-cid="618">周口市</li>
                        <li class="tiaozhan" data-cid="619">驻马店市</li>
                        <li class="tiaozhan" data-cid="635">株洲市</li>
                        <li class="tiaozhan" data-cid="641">张家界市</li>
                        <li class="tiaozhan" data-cid="651">珠海市</li>
                        <li class="tiaozhan" data-cid="655">湛江市</li>
                        <li class="tiaozhan" data-cid="657">肇庆市</li>
                        <li class="tiaozhan" data-cid="665">中山市</li>
                        <li class="tiaozhan" data-cid="685">重庆市</li>
                        <li class="tiaozhan" data-cid="687">自贡市</li>
                        <li class="tiaozhan" data-cid="703">资阳市</li>
                        <li class="tiaozhan" data-cid="709">遵义市</li>
                        <li class="tiaozhan" data-cid="720">昭通市</li>
                        <li class="tiaozhan" data-cid="755">张掖市</li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        var tz = {
            CityName: '<%=Enow.TZB.Utility.Utils.GetQueryStringValue("CityName") %>',
            types:'<%=Enow.TZB.Utility.Utils.GetInt(Enow.TZB.Utility.Utils.GetQueryStringValue("types"), 0) %>',

            tzurl: '<%=url %>',
            Geturl: function (obj) {
                var cid = $(obj).attr("data-cid");
                var fhurl=tz.tzurl + "?CityId=" + cid;
                if(tz.types!="0")
                    fhurl += "&types=" + tz.types;
                window.location.href = fhurl;
            }
        }
        $(document).ready(function () {
            $(".tiaozhan").each(function () {
                $(this).click(function () {
                    tz.Geturl(this);
                });
            });
            $("#btn_sel").click(function () {
                var cname = $("#txtsousuo").val();
                if (cname != "") {
                    window.location.href = "city.aspx?action=<%=action %>&fhdz=<%=fhdz %>&CityName=" + cname;
                }
                else {
                    alert("请输入要查询的城市");
                }

            });
            if (tz.CityName != '') {
                $("#lbd1").addClass("yangshi");
                $("#txtsousuo").val(tz.CityName);
            }
            else {
                $("#ssd1").addClass("yangshi");
            }
        });
    </script>
</body>
</html>
