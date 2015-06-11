using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.SMS;
using Enow.TZB.Utility;
using Enow.TZB.BLL;

namespace Enow.TZB.Web.WX.Member
{
    /// <summary>
    /// 用户绑定
    /// </summary>
    public partial class MemberBind : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                var model = BMember.GetModelByOpenId(OpenId);
                if (model != null)
                {
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(130), "/WX/Member/Default.aspx");
                    return;
                }
            }
        }
        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            string phone = Utils.GetFormValue("txt_phone");
            string valCode = Utils.GetFormValue("txtcode");

            BLL.BMember bll = new BLL.BMember();
            var MemberModel = bll.GetModelByPhone(phone);
            if (MemberModel==null)
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(41));
                return;
            }
            var model = BLL.BMemberVerCode.GetInfo(phone, valCode, 0);
            if (model == null || model.VerCode != valCode)
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(22));
                return;
            }

            if (model.IsUsed)
            {
                MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(23));
                return;
            }

            HashCrypto has = new HashCrypto();
            string MemberId = MemberModel.Id;
            string MemberPhoto = "";
            if(BMemberWeiXin.IsExists(MemberId)){
                //存在用户但未绑定
                BMemberWeiXin.UpdateOpenId(MemberId, OpenId);
            }else{
            //没有任何信息
                //添加微信信息
                //取得微信用户信息
                tbl_MemberWeiXin MemberWeiXinModel = new tbl_MemberWeiXin();
                var WeiXinModel = BWeiXin.GetUserInfo(OpenId);
                if (WeiXinModel != null)
                {
                    MemberPhoto = WeiXinModel.HeadImgUrl.Replace("\\/", "/");
                    MemberWeiXinModel.Id = System.Guid.NewGuid().ToString();
                    MemberWeiXinModel.MemberId = MemberId;
                    MemberWeiXinModel.OpenId = OpenId;
                    MemberWeiXinModel.NickName = WeiXinModel.NickName;
                    MemberWeiXinModel.HeadPhoto = WeiXinModel.HeadImgUrl.Replace("\\/", "/");
                    MemberWeiXinModel.IssueTime = DateTime.Now;
                }
                else
                {
                    MemberWeiXinModel.Id = System.Guid.NewGuid().ToString();
                    MemberWeiXinModel.MemberId = MemberId;
                    MemberWeiXinModel.OpenId = OpenId;
                    MemberWeiXinModel.NickName = "";
                    MemberWeiXinModel.HeadPhoto = "";
                    MemberWeiXinModel.IssueTime = DateTime.Now;
                }
                //添加微信信息
                BMemberWeiXin.Add(MemberWeiXinModel);
                BMember.UpdateWxOpenId(MemberId, OpenId);
            }            
            MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(131),"Default.aspx");
            return;
        }
    }
}