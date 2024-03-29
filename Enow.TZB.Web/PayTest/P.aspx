﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="P.aspx.cs" Inherits="Enow.TZB.Web.PayTest.P" %>
<!DOCTYPE html>
<html>
<head>
        <title>微支付</title>
		<meta http-equiv="Content-Type" content="text/html;"/>
		<script language="javascript" src="http://res.mail.qq.com/mmr/static/lib/js/jquery.js" type="text/javascript"></script>
        <script language="javascript" src="http://res.mail.qq.com/mmr/static/lib/js/lazyloadv3.js"  type="text/javascript"></script>
        <meta id="viewport" name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1; user-scalable=no;" />
        <link rel="stylesheet" href="/WX/css/style.css" type="text/css" media="screen" />
        <style type="text/css">
            
            
            body { margin:0;padding:0;background:#eae9e6; }
            body,p,table,td,th { font-size:14px;font-family:helvetica,Arial,Tahoma; }
            h1 { font-family:Baskerville,HelveticaNeue-Bold,helvetica,Arial,Tahoma; }
            a { text-decoration:none;color:#385487;}
            
            
            .container {  }
            .title { }
            #content {padding:30px 20px 20px;color:#111;background:#f7f2ed;  }
            .seeAlso { padding:15px 20px 30px; }
            
            .headpic div { margin:20px 0 0;}
            .headpic img { display:block;}
            
            .title h1 { font-size:22px;font-weight:bold;padding:0;margin:0;line-height:1.2;color:#1f1f1f; }
            .title p { color:#aaa;font-size:12px;margin:5px 0 0;padding:0;font-weight:bold;}
            .pic { margin:20px 0; }
            .articlecontent img { display:block;clear:both;margin:5px auto;}
            .articlecontent p { text-indent: 2em; font-family:Georgia,helvetica,Arial,Tahoma;line-height:1.4; font-size:16px; margin:20px 0;  }
            
            
            .seeAlso h3 { font-size:16px;color:#a5a5a5;}
            .seeAlso ul { margin:0;padding:0; }
            .seeAlso li {  font-size:16px;list-style-type:none;border-top:1px solid #ccc;padding:2px 0;}
            .seeAlso li a { border-bottom:none;display:block;line-height:1.1; padding:13px 0; }
            
            .clr{ clear:both;height:1px;overflow:hidden;}
            
            
            .fontSize1 .title h1 { font-size:20px; }
            .fontSize1 .articlecontent p {  font-size:14px; }
            .fontSize1 .weibo .nickname,.fontSize1 .weibo .comment  { font-size:11px; }
            .fontSize1 .moreOperator { font-size:14px; }
            
            .fontSize2 .title h1 { font-size:22px; }
            .fontSize2 .articlecontent p {  font-size:16px; }
            .fontSize2 .weibo .nickname,.fontSize2 .weibo .comment  { font-size:13px; }
            .fontSize2 .moreOperator { font-size:16px; }
            
            .fontSize3 .title h1 { font-size:24px; }
            .fontSize3 .articlecontent p {  font-size:18px; }
            .fontSize3 .weibo .nickname,.fontSize3 .weibo .comment  { font-size:15px; }
            .fontSize3 .moreOperator { font-size:18px; }
            
            .fontSize4 .title h1 { font-size:26px; }
            .fontSize4 .articlecontent p {  font-size:20px; }
            .fontSize4 .weibo .nickname,.fontSize4 .weibo .comment  { font-size:16px; }
            .fontSize4 .moreOperator { font-size:20px; }
            
            .jumptoorg { display:block;margin:16px 0 16px; }
            .jumptoorg a {  }
            
            .moreOperator a { color:#385487; }
            
            .moreOperator .share{ border-top:1px solid #ddd; }
            
            .moreOperator .share a{ display:block;border:1px solid #ccc;border-bottom-style:solid;background:#f8f7f1;color:#000; }
            
            .moreOperator .share a span{ display:block;padding:10px 10px;text-align:center;border-top:1px solid #eee;border-bottom:1px solid #eae9e3;font-weight:bold; }
            
            .moreOperator .share a:hover,
            .moreOperator .share a:active { background:#efedea; }
            @media only screen and (-webkit-min-device-pixel-ratio: 2) {
            }
            </style>
        <script language="javascript" type="text/javascript">
            function auto_remove(img) {
                div = img.parentNode.parentNode; div.parentNode.removeChild(div);
                img.onerror = "";
                return true;
            }

            function changefont(fontsize) {
                if (fontsize < 1 || fontsize > 4) return;
                $('#content').removeClass().addClass('fontSize' + fontsize);
            }

            // 当微信内置浏览器完成内部初始化后会触发WeixinJSBridgeReady事件。
            document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {

                //公众号支付
                jQuery('a#getBrandWCPayRequest').click(function (e) {
                    WeixinJSBridge.invoke('getBrandWCPayRequest', {
                        "appId": "<%=WxPayAPI.Tencent.Core.TenPaySystem.appkey%>", //公众号名称，由商户传入
                        "timeStamp": "<%= _TenPayTradeModel.TimeStamp %>", //时间戳
                        "nonceStr": "<%= _TenPayTradeModel.NonceStr %>", //随机串
                        "package": "prepay_id=<%= _TenPayTradeModel.PrepayId %>", //扩展包
                        "signType": "MD5", //微信签名方式:1.sha1
                        "paySign": "<%= _TenPayTradeModel.Sign %>" //微信签名
                    }, function (res) {

                        if (res.err_msg == "get_brand_wcpay_request:ok") {
                            alert("支付成功！");
                            window.location.href = '/WX/Member';
                        }
                        // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
                        //因此微信团队建议，当收到ok返回时，向商户后台询问是否收到交易成功的通知，若收到通知，前端展示交易成功的界面；若此时未收到通知，商户后台主动调用查询订单接口，查询订单的当前状态，并反馈给前端展示相应的界面。
                    });

                });

                //WeixinJSBridge.log('yo~ ready.');

            }, false)
            if (jQuery) {
                jQuery(function () {

                    var width = jQuery('body').width() * 0.87;
                    jQuery('img').error(function () {
                        var self = jQuery(this);
                        var org = self.attr('data-original1');
                        self.attr("src", org);
                        self.error(function () {
                            auto_remove(this);
                        });
                    });
                    jQuery('img').each(function () {
                        var self = jQuery(this);
                        var w = self.css('width');
                        var h = self.css('height');
                        w = w.replace('px', '');
                        h = h.replace('px', '');
                        if (w <= width) {
                            return;
                        }
                        var new_w = width;
                        var new_h = Math.round(h * width / w);
                        self.css({ 'width': new_w + 'px', 'height': new_h + 'px' });
                        self.parents('div.pic').css({ 'width': new_w + 'px', 'height': new_h + 'px' });
                    });
                });
            }						  
            </script>
    </head>
    <body>
    <header class="head">

    <a href="/WX/SmartWeb/"><b class="icon_home"></b></a>
    <h1>钱包</h1>
    <a href="javascript:history.back();"><i class="returnico"></i></a>
    
</header>

<div class="warp">

  <div class="msg_tab">

        <div class="TabContent">
        
			<div id="n4Tab_Content0">
            
                <div class="msg_list">
                  <ul>
                            <li><label>充值账号：</label><asp:Literal ID="ltrUserName" runat="server"></asp:Literal> <!--<a href="" class="gai_btn">给好友充值</a>--></li>
                            <asp:PlaceHolder ID="phNoView" runat="server">
                            <li><label>充值数量：</label><asp:Literal ID="ltrPoint" runat="server"></asp:Literal>铁丝币</li></asp:PlaceHolder>
                            <li><label>应付金额：</label><span id="spanMoney"><asp:Literal ID="ltrMoney" runat="server"></asp:Literal></span>元</li>
                            
                   </ul>
                   
                </div>
                
                <div class="msg_btn">
                <div class="WCPay">
            <a id="getBrandWCPayRequest" class="basic_btn" href="javascript:void(0);">确认支付</a>
           
        </div>
        </div>
                
            </div>

         </div>   
  </div>     

</div>
        
    </body>
</html>