using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.Utility;
using Enow.TZB.BLL;
using System.Web.UI.HtmlControls;

namespace Enow.TZB.Web.WX.Team
{
    public partial class GathersResult : System.Web.UI.Page
    {
        private string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
        string gatherId = "";

        /// <summary>
        /// 会员编号
        /// </summary>
        protected string MemberId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BMemberAuth.LoginCheck();
                var AuthModel = BMemberAuth.GetUserModel();
                string OpenId = AuthModel.OpenId;
                InitMember(OpenId);
               
            }
             gatherId = Utils.GetQueryStringValue("gatherId");
             InitPage(gatherId);
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="OpenId"></param>
        private void InitMember(string OpenId)
        {
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                MemberId = model.Id;
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), "../Register/RegisterChoose.aspx");
                return;
            }
        }
        private void InitPage(string gatherid)
        {
            tbl_Gathers gatherModel = BGathers.GetModel(gatherId);
            if (gatherModel!=null)
            {
                txtZTeam.Text = gatherModel.TeamName;
                txtCTeam.Text = gatherModel.AcceptTeamName;
            }
        }
        /// <summary>
        /// 建立目录
        /// </summary>
        /// <param name="DirectoryName">目录名</param>
        /// <returns>返回数字,0:目录建立成功, 1:目录已存在,2:目录建立失败</returns>
        private int CreateDirectory(string DirectoryName)
        {
            try
            {
                if (!System.IO.Directory.Exists(DirectoryName))
                {
                    System.IO.Directory.CreateDirectory(DirectoryName);
                    return 0;
                }
                else
                {

                    return 1;
                }
            }
            catch
            {
                return 2;
            }
        }
        /// <summary>
        /// 检测字符串是否是数组中的一项
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="arrData"></param>
        /// <returns></returns>
        private bool IsStringExists(string inputData, string[] arrData)
        {
            if (null == inputData || string.Empty == inputData)
            {
                return false;
            }
            foreach (string tmpStr in arrData)
            {
                if (inputData == tmpStr)
                {
                    return true;
                }
            }
            return false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            var AuthModel = BMemberAuth.GetUserModel();
            string OpenId = AuthModel.OpenId;
            string strErr = "";
            if (string.IsNullOrEmpty(txtGatherInfo.Text))
            {
                strErr += "赛报内容不能为空!/n";
            }
            #region 得分验证

            if (!Utility.StringValidate.IsNumberSign(txtZUp.Text))
            {
                strErr += "赛报得分需为数字!/n";
            }
            if (!Utility.StringValidate.IsNumberSign(txtZDown.Text))
            {
                strErr += "赛报得分需为数字!/n";
            }
            if (!Utility.StringValidate.IsNumberSign(txtCUp.Text))
            {
                strErr += "赛报得分需为数字!/n";
            }
            if (!Utility.StringValidate.IsNumberSign(txtCDown.Text))
            {
                strErr += "赛报得分需为数字!/n";
            }
            #endregion
            if (!String.IsNullOrEmpty(strErr))
            {
                MessageBox.ShowAndReturnBack(strErr);
                return;
            }

            #region 文件上传
            string MemberPhoto = "";
            string _UploadFileExt = ".gif,.bmp,.png,.jpg,.jpeg";
            int _UpFolderSize = 2024;//KB
            string tmpFileExt = _UploadFileExt;
            string[] strFileExt = tmpFileExt.Split(',');
            if (imgFileUpload.HasFile)
            {
                System.Web.HttpPostedFile file = imgFileUpload.PostedFile;
                //判断文件大小
                if (file.ContentLength > _UpFolderSize * 1024)
                {
                    MessageBox.ShowAndReturnBack("图片不能超过2MB！");
                    return;
                }
                //检验后缀名
                if (!String.IsNullOrWhiteSpace(file.FileName))
                {
                    if (IsStringExists(System.IO.Path.GetExtension(file.FileName).ToLower().Trim(), strFileExt) == false)
                    {
                        MessageBox.ShowAndReturnBack("图片格式不正确！");
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowAndReturnBack("请上传图片文件！");
                    return;
                }
                //保存文件
                string path = UploadPath + DateTime.Now.ToString("yyyyMMdd") + "/";
                CreateDirectory(Server.MapPath(path));
                string fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                MemberPhoto = path + fileName;
                try
                {
                    file.SaveAs(Server.MapPath(MemberPhoto));
                }
                catch
                {
                    MessageBox.ShowAndReturnBack("图片上传失败！");
                    return;
                }
            }
            #endregion


            string MemberId = "", TeamId = Model.MCommon.DefaultGuidId;
            string NickName = "", MemberName = "",TeamName="";
            string AcceptTeamId="",AcceptTeamName="";
            var model = BMember.GetModelByOpenId(OpenId);
            if (model != null)
            {
                MemberId = model.Id;
                NickName = model.NickName;
                MemberName = model.ContactName;
                var TeamModel = BTeamMember.GetModel(MemberId);
                if (TeamModel != null)
                {
                    TeamId = TeamModel.TeamId;
                    TeamName=TeamModel.TeamName;
                }
               

                tbl_Gathers gatherModel = BGathers.GetModel(gatherId);
                if (gatherModel != null)
                {
                    //发起的约战
                    if (gatherModel.TeamId == TeamId)
                    {
                        AcceptTeamId = gatherModel.AcceptTeamId;
                        AcceptTeamName = gatherModel.AcceptTeamName;
                    }
                    else//收到的约战
                    {
                        
                        TeamId=gatherModel.TeamId;
                        TeamName=gatherModel.TeamName;


                        AcceptTeamId = TeamModel.TeamId;
                        AcceptTeamName = TeamModel.TeamName;
                    }
                }

                BGatherResult.Add(new tbl_GathersResult
                {
                    Id = Guid.NewGuid().ToString(),
                    GatherId = gatherId,
                    TeamId = TeamId,
                    TeamName=TeamName ,
                    AcceptTeamId=AcceptTeamId ,
                    AcceptTeamName=AcceptTeamName,
                    ZTeamUp=int.Parse(txtZUp.Text ),
                    ZTeamDowm=int.Parse(txtZDown.Text ),
                    CTeamUp=int.Parse(txtCUp.Text ),
                    CTeamDown = int.Parse(txtCDown.Text ),
                    GatherPhoto=MemberPhoto,
                    GatherInfo = txtGatherInfo.Text,
                    MemberId = MemberId,
                    NickName = NickName,
                    MemberName = MemberName,
                  
                   

                });
                BGathers.IsGatherResult(gatherId);
                MessageBox.ShowAndRedirect("发起战报成功！", "MyGathers.aspx");
            }
            else { MessageBox.ShowAndReturnBack(CacheSysMsg.GetMsg(8)); }

        }
    }
}