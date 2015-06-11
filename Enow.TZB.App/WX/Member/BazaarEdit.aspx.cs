using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.App.WX.Member
{
    public partial class BazaarEdit : System.Web.UI.Page
    {
        private string userid;
        private string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
        protected void Page_Load(object sender, EventArgs e)
        {
            BMemberApp.LoginCheck();
            InitMember();
            if (!IsPostBack)
            {
                string id = Utils.GetQueryStringValue("Id");
                GetRoleclass();
                if (!string.IsNullOrEmpty(id))
                {
                    
                    InitLoad(id);
                }
               
            }
        }
        /// <summary>
        /// 加载会员信息
        /// </summary>
        private void InitMember()
        {
            var model = BMemberApp.GetUserModel();
            if (model != null)
            {
                if (String.IsNullOrWhiteSpace(model.ContactName))
                {
                    //您未填写实名信息,\n请补充填写！
                    MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(35), "/WX/Member/Step2.aspx");
                    return;
                }
                BMember.StateCheck((Model.EnumType.会员状态)model.State);
                userid = model.Id;
            }
            else
            {
                //未找到您的用户信息!
                MessageBox.ShowAndRedirect(CacheSysMsg.GetMsg(8), BMemberApp.RespUrl);
                return;
            }
        }
        /// <summary>
        /// 查询分类
        /// </summary>
        private void GetRoleclass()
        {
            var list = BRoleClass.GetTypeList(1);
            dropyjclass.DataSource = list;
            dropyjclass.DataBind();
        }
        private void InitLoad(string id)
        {
            var model = BMallGoods.GetModel(id);
            if (model!=null)
            {
                txtname.Text = model.GoodsName;//名称
                if (!string.IsNullOrEmpty(model.GoodsPhoto))
                    ltrHead.Text = "<img src=\"" + model.GoodsPhoto + "\" width=\"55\" height=\"50\" border=\"0\" />";  //图片
                if (model.RoleClass != null)//分类
                {
                    if (dropyjclass.Items.FindByValue(model.RoleClass.ToString()) != null)
                    {
                        dropyjclass.Items.FindByValue(model.RoleClass.ToString()).Selected = true;
                    }
                }
                txtscj.Text = model.MarketPrice.ToString();//市场价
                txthyj.Text = model.MemberPrice.ToString();//会员价
                txtsuliang.Text = model.StockNum.ToString();//数量
                txtscs.Text = model.Producer;
                if (model.IsFreight)
                {
                    hdfyfbool.Value = "1";
                }
                else
                {
                    txtyunfei.Text = model.FreightFee.ToString();
                }
                txtjianjie.Text = model.GoodsIntroduce;
            }
            else
            {
                Response.Redirect("BazaarList.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string id = Utils.GetQueryStringValue("Id");
            bool isResult = false;
            #region 数据集合
            int TypeId = (int)Enow.TZB.Model.商品分类.爱心义卖;

            string GoodsName = Utils.GetFormValue(txtname.UniqueID);

            int Status = (int)(Model.义卖商品销售状态.待审核);

            string Producer = Utils.GetFormValue(txtscs.UniqueID);
            //会员价
            decimal Price = Utils.GetDecimal(Utils.GetFormValue(txtscj.UniqueID));
            //市场价
            decimal CurrencyPrice = Utils.GetDecimal(Utils.GetFormValue(txthyj.UniqueID));
            int Stock = Utils.GetInt(Utils.GetFormValue(txtsuliang.UniqueID));
            string Introduce = txtjianjie.Text;

            bool IsFreight = hdfyfbool.Value == "0" ? false : true;
            decimal freightFee = 0;
            if (!IsFreight)
            {

                //divFreight.Style["display"] = "block;";
                //divFreight.Attributes.Add("style","display:block");
                freightFee = Utils.GetDecimal(Utils.GetFormValue(txtyunfei.UniqueID));
            }
            int roleclass = Utils.GetInt(Utils.GetFormValue(dropyjclass.UniqueID), 0);
            #endregion
            #region 判断
            if (roleclass <= 0)
            {

                MessageBox.ShowAndReload("请选择商品类别!");
                return;
            }
            if (string.IsNullOrWhiteSpace(GoodsName))
            {
                MessageBox.ShowAndReload("请填写商品名称!");
                return;
            }

            if (Status < 0)
            {
                MessageBox.ShowAndReload("请选择商品销售状态");
                return;
            }

            if (!StringValidate.IsDecimal(Price.ToString()))
            {

                MessageBox.ShowAndReload("会员价只能为无符号的小数或者整数,请输入正确的会员价!");
                return;
            }
            if (!StringValidate.IsDecimal(CurrencyPrice.ToString()))
            {

                MessageBox.ShowAndReload("市场价只能为无符号的小数或者整数，请输入正确的市场价!");
                return;
            }
            if (Stock < 0)
            {

                MessageBox.ShowAndReload("库存数必须大于0,请重新输入!");
                return;
            }
            #endregion
            #region 文件上传
            string GoodsPhoto = "";
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
                GoodsPhoto = path + fileName;
                try
                {
                    file.SaveAs(Server.MapPath(GoodsPhoto));
                }
                catch
                {
                    MessageBox.ShowAndReturnBack("文件上传失败！");
                    return;
                }
            }
            #endregion
            string rolename = BRoleClass.GetName(roleclass);
            tbl_MallGoods mginfo = new tbl_MallGoods()
            {
                RoleClass = roleclass,
                RoleClassName = rolename,
                GoodsClassId = TypeId,
                GoodsName = GoodsName,
                StockNum = Stock,
                MemberPrice = Price,
                MarketPrice = CurrencyPrice,
                GoodsIntroduce = Introduce,
                Status = Status,
                IsDelete = false,
                OperatorId = 0,
                OperatorName = "",
                Producer = Producer,
                MemberId = userid,
                GoodsPhoto = GoodsPhoto,
                IsGood =  false,
                IsFreight = IsFreight,
                FreightFee = freightFee,
                IssueTime = DateTime.Now
            };
            if (!string.IsNullOrEmpty(id))
            {
                mginfo.Id = id;
                isResult = BMallGoods.UpdateMemberGood(mginfo);
            }
            else
            {
                mginfo.Id = Guid.NewGuid().ToString();
                isResult = BMallGoods.Add(mginfo);
            }
            if (isResult)
            {

                MessageBox.ShowAndRedirect("操作成功", "BazaarList.aspx");

            }
            else
            {

                MessageBox.ShowAndReturnBack("操作失败!");
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
    }
}