using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ProjectOneStockManagement.BLL;
using ProjectOneStockManagement.Models;

namespace ProjectOneStockManagement.UI
{
    public partial class StockOutLayout : System.Web.UI.Page
    {
        ItemManager itemManager = new ItemManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAllCompany();

                ListItem liCompany = new ListItem("Select Company", "-1");
                ddlStockOutCompany.Items.Insert(0, liCompany);

                ddlStockOutItem.Enabled = false;

                itemManager.ClearStockOutTable();
            }
        }

        protected void ddlStockOutCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStockOutCompany.SelectedIndex != 0)
            {
                ddlStockOutItem.Enabled = true;
                BindAllItems();
            }
        }
        private void BindAllCompany()
        {
            CompanyManager companyManager = new CompanyManager();
            ddlStockOutCompany.DataSource = companyManager.GetCompanyList();
            ddlStockOutCompany.DataBind();
        }
        public void BindAllItems()
        {
            Company aCompany = new Company();
            aCompany.ID = Convert.ToInt32(ddlStockOutCompany.SelectedValue); //Take CompanyID from Item DropDownList
            List<Item> dbItem = itemManager.GetItemListByCompany(aCompany);
            ddlStockOutItem.DataSource = dbItem;
            ddlStockOutItem.DataBind();

            ListItem liItem = new ListItem("Select Item", "-1");
            ddlStockOutItem.Items.Insert(0, liItem);
        }

        protected void ddlStockOutItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item aItem = new Item();
            Item existingItem = new Item();

            aItem.ID = Convert.ToInt32(ddlStockOutItem.SelectedValue);  //Take ItemID from Item DropDownList
            existingItem = itemManager.GetItemInfoByID(aItem); // Bring from DB -- ItemID, AvailableQuantity, ReorderLevel

            ViewState["existingItem"] = existingItem;

            textBoxAvailableQty.Text = Convert.ToString(existingItem.AvailableQuantity);
            textBoxReorderLevel.Text = Convert.ToString(existingItem.ReorderLevel);
            textBoxAvailableQty.Enabled = false;
            textBoxReorderLevel.Enabled = false;
        }

        protected void btnAddStockOut_Click(object sender, EventArgs e)
        {
            int stockOutQuantity = Convert.ToInt32(textBoxStockOutQty.Text);

            
            Item newItem = new Item();
            Item presentItem = new Item();
            newItem = (Item) ViewState["existingItem"];  // Inside newItem = ItemID, AvailableQuantity, ReorderLevel
            newItem.AvailableQuantity = stockOutQuantity;

            string msg = itemManager.AddStockOutQuantity(newItem); // Inside newItem = Given Stock Out Quantity
            lblStockOutStatus.Text = msg;

            presentItem = itemManager.GetItemInfoByID(newItem);  //From Items Table= ItemID, AvailableQuantity, ReorderLevel

            textBoxAvailableQty.Text = Convert.ToString(presentItem.AvailableQuantity);

            gridViewStockOut.DataSource = itemManager.GetStockOutItemListByID(newItem);
            gridViewStockOut.DataBind();
            textBoxStockOutQty.Text = Convert.ToString(0);
        }

        protected void btnSell_Click(object sender, EventArgs e)
        {
            string msg = itemManager.AddSellItems();
            lblStockOutStatus.Text = msg;
            itemManager.ClearStockOutTable();
            gridViewStockOut.DataSource = "";
            gridViewStockOut.DataBind();
        }

        protected void btnDamage_Click(object sender, EventArgs e)
        {
            string msg = itemManager.AddDamageItems();
            lblStockOutStatus.Text = msg;
            itemManager.ClearStockOutTable();
            gridViewStockOut.DataSource = "";
            gridViewStockOut.DataBind();

        }

        protected void btnLost_Click(object sender, EventArgs e)
        {
            string msg = itemManager.AddLostItems();
            lblStockOutStatus.Text = msg;
            itemManager.ClearStockOutTable();
            gridViewStockOut.DataSource = "";
            gridViewStockOut.DataBind();
        }
    }
}