using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Mall
{
    public partial class GoodsEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("dotype");
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                //InitStandardList();
                //InitTypeList();
                GetRoleclass();
                switch (doType)
                {
                    case "update":
                        InitPage();
                        break;
                    case "view":
                        this.linkBtnSave.Visible = false;
                        InitPage();
                        break;
                }
            }
            InitStatus();
        }
        /// <summary>
        /// 查询分类
        /// </summary>
        private void GetRoleclass()
        {
            var list = BRoleClass.GetTypeList(3);
            dropyjclass.DataSource = list;
            dropyjclass.DataBind();
        }
        private void InitPage()
        {
            string id = Utils.GetQueryStringValue("id");
            var model = BMallGoods.GetModel(id);
            if (model != null)
            {
                //if (model.GoodsClassId!=-1)
                //{
                //    if (ddlType.Items.FindByValue(model.GoodsClassId.ToString()) != null)
                //    {
                //        ddlType.Items.FindByValue(model.GoodsClassId.ToString()).Selected = true;
                //    }
                //    else
                //    {
                //        ddlType.SelectedValue = "-1";
                //    }
                //}
                #region 商品附件
                IList<Model.MFileInfo> files = new List<Model.MFileInfo>();
                files.Add(new Model.MFileInfo() { FilePath = model.GoodsPhoto });
                UploadPhoto.YuanFiles = files;
                #endregion
                txtGoodsName.Text = model.GoodsName;
                if (ddlStatu.Items.FindByValue(model.Status.ToString()) != null)
                { 
                    ddlStatu.Items.FindByValue(model.Status.ToString()).Selected = true;
                }
                //ddlStandard.Items.FindByValue(model.Standard.ToString()).Selected = true;
                if (model.RoleClass != null)
                {
                    if (dropyjclass.Items.FindByValue(model.RoleClass.ToString()) != null)
                    {
                        dropyjclass.Items.FindByValue(model.RoleClass.ToString()).Selected = true;
                    }
                }
                rdoIsGood.Items.FindByValue(model.IsGood==true?"1":"0").Selected = true;
                txtProducer.Text = model.Producer;
                txtPrice.Text = model.MemberPrice.ToString();
                txtCurrencyPrice.Text = model.MarketPrice.ToString();
                rdoIsFreight.Items.FindByValue(model.IsFreight== true ? "1" : "0").Selected = true;
                txtStock.Text = model.StockNum.ToString();
                txtFreightFee.Text = model.FreightFee.ToString();
                txtIntroduce.Text = model.GoodsIntroduce;

            }
        }
        /// <summary>
        /// 绑定规格下拉框
        /// </summary>
        //private void InitStandardList()
        //{
        //    var list = BGoodsClass.GetStandardList();
        //    if (list.Count > 0)
        //    {
        //        ddlStandard.DataSource = list;
        //        ddlStandard.DataTextField = "ClassName";
        //        ddlStandard.DataValueField = "Id";
        //        ddlStandard.DataBind();
        //        this.ddlStandard.Items.Insert(0, new ListItem("请选择规格", "-1"));
        //    }
        //}
        /// <summary>
        /// 绑定商品类别
        /// </summary>
        private void InitTypeList()
        {
            //var list = BGoodsClass.GetTypeList();
            //if (list.Count > 0)
            //{
            //    ddlType.DataSource = list;
            //    ddlType.DataTextField = "ClassName";
            //    ddlType.DataValueField = "Id";
            //    ddlType.DataBind();
               

            //}
            //this.ddlType.Items.Insert(0, new ListItem("请选择商品类别", "-1"));

        }

        /// <summary>
        /// 商品销售状态
        /// </summary>
        private void InitStatus()
        {
            Array array = Enum.GetValues(typeof(Model.商品销售状态));
            foreach (var arr in array)
            {
                int value = (int)Enum.Parse(typeof(Model.商品销售状态), arr.ToString());
                string text = Enum.GetName(typeof(Model.商品销售状态), arr);
                this.ddlStatu.Items.Add(new ListItem() { Text = text, Value = value.ToString() });
            }
            this.ddlStatu.Items.Insert(0, new ListItem("请选择销售状态", "-1"));
            ddlStatu.SelectedValue = "1";

        }
        /// <summary>
        /// 上传的附件
        /// </summary>
        /// <returns></returns>
        protected string GetAttachFile()
        {
            var files1 = UploadPhoto.Files;
            var files2 = UploadPhoto.YuanFiles;
            if (files1 != null && files1.Count > 0)
            {
                return files1[0].FilePath;
            }
            if (files2 != null && files2.Count > 0)
            {
                return files2[0].FilePath;
            }
            return string.Empty;
        }
        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            string GoodsPhoto = GetAttachFile();
            bool isResult = false;
            ManagerList manageModel = ManageUserAuth.GetManageUserModel();
            int OperatorId = manageModel.Id;
            string OperatorName = manageModel.ContactName;
            manageModel = null;
            string doType = Utils.GetQueryStringValue("dotype").ToLower();
            string Id = Utils.GetQueryStringValue("id");
            //int TypeId = Utils.GetInt(Utils.GetFormValue(ddlType.UniqueID));
            int TypeId = (int)Enow.TZB.Model.商品分类.培训课程;
          
            string GoodsName = Utils.GetFormValue(txtGoodsName.UniqueID);
          
            int Status = Utils.GetInt(Utils.GetFormValue(ddlStatu.UniqueID));
          
            string Producer = Utils.GetFormValue(txtProducer.UniqueID);
            //会员价
            decimal Price = Utils.GetDecimal(Utils.GetFormValue(txtPrice.UniqueID));
            //市场价
            decimal CurrencyPrice = Utils.GetDecimal(Utils.GetFormValue(txtCurrencyPrice.UniqueID));
            int Stock = Utils.GetInt(Utils.GetFormValue(txtStock.UniqueID));
            string Introduce = txtIntroduce.Text;

            bool IsFreight=rdoIsFreight.SelectedValue=="0"?false :true ;
            decimal freightFee=0;
            if(!IsFreight){

                //divFreight.Style["display"] = "block;";
                //divFreight.Attributes.Add("style","display:block");
                freightFee = Utils.GetDecimal(Utils.GetFormValue(txtFreightFee.UniqueID));
            }
            int roleclass = Utils.GetInt(Utils.GetFormValue(dropyjclass.UniqueID),0);
            #region 判断
            if (roleclass <= 0)
            {

                MessageBox.ShowAndReload("请选择商品类别!");
                return;
            }
            if (TypeId < 0)
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
                OperatorId = OperatorId,
                OperatorName = OperatorName,
                Producer = Producer,

                GoodsPhoto = GoodsPhoto,
                IsGood = rdoIsGood.SelectedValue == "1" ? true : false,
                IsFreight = IsFreight,
                FreightFee = freightFee,
                IssueTime = DateTime.Now
            };
            switch (doType)
            {
                case "add":
                    mginfo.Id = Guid.NewGuid().ToString();
                    isResult = BMallGoods.Add(mginfo);
                    break;
                case "update":
                    mginfo.Id = Id;
                    isResult = BMallGoods.Update(mginfo);
                    break;
            }
            if (isResult)
            {

                MessageBox.ShowAndParentReload("操作成功");

            }
            else
            {

                MessageBox.ShowAndReturnBack("操作失败!");
                return;

            }
        }

       

      

       
    }
}