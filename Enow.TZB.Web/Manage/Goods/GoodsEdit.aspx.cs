using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enow.TZB.BLL;
using Enow.TZB.Model;
using Enow.TZB.Utility;

namespace Enow.TZB.Web.Manage.Goods
{
    public partial class GoodsEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("dotype");
            if (!IsPostBack)
            {
                ManageUserAuth.ManageLoginCheck();
                InitFieldList();
                InitTypeList();
                InitStatus();
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
        }

        private void InitPage()
        {
            int id = Utils.GetInt(Utils.GetQueryStringValue("id"), 0);
            var model = BGoods.GetModel(id);
            if (model != null)
            {
                if (model.TypeId>1)
                {
                    ddlType.Items.FindByValue(model.TypeId.ToString()).Selected = true;
                }
               
                txtGoodsName.Text = model.GoodsName;
                txtUnit.Text = model.Unit;
                ddlStatus.Items.FindByValue(model.Status.ToString()).Selected = true;
                ddlFieldName.Items.FindByValue(model.BallFieldId).Selected = true;
                txtProducer.Text = model.Producer;
                txtPrice.Text = model.Price.ToString();
                txtCurrencyPrice.Text = model.CurrencyPrice.ToString();
                txtStock.Text = model.Stock.ToString();
                txtIntroduce.Text = model.GoodsIntroduce;

            }
        }
        /// <summary>
        /// 绑定球场下拉框
        /// </summary>
        private void InitFieldList()
        {
            var list = BBallField.GetFieldList();
            if (list.Count > 0)
            {
                ddlFieldName.DataSource = list;
                ddlFieldName.DataTextField = "FieldName";
                ddlFieldName.DataValueField = "Id";
                ddlFieldName.DataBind();
                this.ddlFieldName.Items.Insert(0, new ListItem("请选择球场", "0"));
            }
        }
        /// <summary>
        /// 绑定商品类别
        /// </summary>
        private void InitTypeList()
        {
            var list = BGoodsType.GetTypeList();
            if (list.Count > 0)
            {
                ddlType.DataSource = list;
                ddlType.DataTextField = "TypeName";
                ddlType.DataValueField = "Id";
                ddlType.DataBind();
                this.ddlType.Items.Insert(0, new ListItem("请选择商品类别", "0"));
            }
        }

        /// <summary>
        /// 商品状态
        /// </summary>
        private void InitStatus()
        {
            Array array = Enum.GetValues(typeof(Model.商品上架状态));
            foreach (var arr in array)
            {
                int value = (int)Enum.Parse(typeof(Model.商品上架状态), arr.ToString());
                string text = Enum.GetName(typeof(Model.商品上架状态), arr);
                this.ddlStatus.Items.Add(new ListItem() { Text = text, Value = value.ToString() });
            }
            this.ddlStatus.Items.Insert(0, new ListItem("请选择商品状态", "0"));

        }

        protected void linkBtnSave_Click(object sender, EventArgs e)
        {
            bool isResult = false;
            ManagerList manageModel = ManageUserAuth.GetManageUserModel();
            int OperatorId = manageModel.Id;
            string OperatorName = manageModel.ContactName;
            manageModel = null;
            string doType = Utils.GetQueryStringValue("dotype").ToLower();
            int Id = Utils.GetInt(Utils.GetQueryStringValue("id"), 0);
            int TypeId = Utils.GetInt(Utils.GetFormValue(ddlType.UniqueID));
            string GoodsName = Utils.GetFormValue(txtGoodsName.UniqueID);
            string Unit = Utils.GetFormValue(txtUnit.UniqueID);
            int Status = Utils.GetInt(Utils.GetFormValue(ddlStatus.UniqueID));
            string BallFieldId = Utils.GetFormValue(ddlFieldName.UniqueID);
            string Producer = Utils.GetFormValue(txtProducer.UniqueID);
            decimal Price = Utils.GetDecimal(Utils.GetFormValue(txtPrice.UniqueID));
            decimal CurrencyPrice = Utils.GetDecimal(Utils.GetFormValue(txtCurrencyPrice.UniqueID));
            int Stock = Utils.GetInt(Utils.GetFormValue(txtStock.UniqueID));
            string Introduce = txtIntroduce.Text;
            #region 判断

            if (TypeId < 1)
            {

                MessageBox.ShowAndReload("请选择商品类别!");
                return;
            }
            if (string.IsNullOrWhiteSpace(GoodsName))
            {
                MessageBox.ShowAndReload("请填写商品名称!");
                return;
            }
            if (string.IsNullOrWhiteSpace(Unit))
            {
                MessageBox.ShowAndReload("请填写商品单位!");
                return;
            }
            if (Status < 1)
            {
                MessageBox.ShowAndReload("请选择商品状态");
                return;
            }
            if (BallFieldId == "0")
            {
                MessageBox.ShowAndReload("请填写所属球场!");
                return;
            }
            if (!StringValidate.IsDecimal(Price.ToString()))
            {

                MessageBox.ShowAndReload("市场价只能为无符号的小数或者整数,请输入正确的市场价!");
                return;
            }
            if (!StringValidate.IsDecimal(CurrencyPrice.ToString()))
            {

                MessageBox.ShowAndReload("铁丝价只能为无符号的小数或者整数，请输入正确的铁丝价!");
                return;
            }
            if (Stock < 1)
            {

                MessageBox.ShowAndReload("库存数必须大于1,请重新输入!");
                return;
            }

            #endregion

            switch (doType)
            {
                case "add":
                    isResult = BGoods.Add(new tbl_Goods
                    {
                        TypeId = TypeId,
                        GoodsName = GoodsName,
                        Unit = Unit,
                        Status = Status,
                        BallFieldId = BallFieldId,
                        Producer = Producer,
                        Price = Price,
                        CurrencyPrice = CurrencyPrice,
                        Stock = Stock,
                        OperatorId = OperatorId,
                        OperatorName = OperatorName,
                        GoodsIntroduce = Introduce,
                        IssueTime = DateTime.Now
                    });
                    break;
                case "update":
                    isResult = BGoods.Update(new tbl_Goods
                    {
                        ID = Id,
                        TypeId = TypeId,
                        GoodsName = GoodsName,
                        Unit = Unit,
                        BallFieldId = BallFieldId,
                        Producer = Producer,
                        Price = Price,
                        Status=Status,
                        CurrencyPrice = CurrencyPrice,
                        Stock = Stock,
                        OperatorId = OperatorId,
                        OperatorName = OperatorName,
                        GoodsIntroduce = Introduce,
                        IssueTime = DateTime.Now
                    });
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