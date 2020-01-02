using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ProjectOneStockManagement.BLL;
using ProjectOneStockManagement.Models;

namespace ProjectOneStockManagement.UI
{
    public partial class StockInLayout : System.Web.UI.Page
    {
        ItemManager itemManager = new ItemManager();
        CompanyManager companyManager = new CompanyManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)   //If page load for first time then execute this block
            {
                BindAllCompany();   //Get All Company List inside DropDownList

                ListItem liCompany = new ListItem("Select Company", "-1");
                ddlStockInCompany.Items.Insert(0, liCompany);                 //Send "Select Company" to first position of DropDownList

                ddlStockInItem.Enabled = false;      //Set Disable to Item DropDownList at page load time
            }
            
        }

        protected void ddlStockInCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlStockInCompany.SelectedIndex != 0)   //If Select any Company then execute this block
            {
                ddlStockInItem.Enabled = true;         //Enable Item DropDownList
                BindAllItems();                        //Get All Category List inside DropDownList
            }
            
        }

        private void BindAllCompany()
        {
            ddlStockInCompany.DataSource = companyManager.GetCompanyList();   //Get All Company Name and CompanyID inside DDL
            ddlStockInCompany.DataBind();
        }
        public void BindAllItems()
        {
            Company aCompany = new Company();
            aCompany.ID = Convert.ToInt32(ddlStockInCompany.SelectedValue);  //Take CompanyID from DropDownList by Selected Value
            List<Item> dbItem = itemManager.GetItemListByCompany(aCompany);  //Send All Items From DataBase to a List
            ddlStockInItem.DataSource = dbItem;                              //Send All Items inside DropDownList
            ddlStockInItem.DataBind();

            ListItem liItem = new ListItem("Select Item", "-1");
            ddlStockInItem.Items.Insert(0, liItem);                          //Send "Select Company" to first position of DropDownList
        }

        protected void ddlStockInItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item aItem = new Item();
            Item existingItem = new Item();

            aItem.ID = Convert.ToInt32(ddlStockInItem.SelectedValue);       //Take ItemID from DDL Selected Value
            existingItem = itemManager.GetItemInfoByID(aItem);   //Get ItemID, AvailableQuantity, ReorderLevel from DataBase using aItem.ID

            ViewState["existingItem"] = existingItem;            //Save ExistingItem to ViewState for taking ID for next work

            textBoxAvailableQty.Text = Convert.ToString(existingItem.AvailableQuantity);  //Send Available Quantity to TextBox
            textBoxReorderLevel.Text = Convert.ToString(existingItem.ReorderLevel);       //Send Level to TextBox
            textBoxAvailableQty.Enabled = false;                                          //Disable the TextBox Field
            textBoxReorderLevel.Enabled = false;                                          //Disable the TextBox Field
        }

        protected void btnSaveStockIn_Click(object sender, EventArgs e)
        {
            int stockInQuantity = Convert.ToInt32(textBoxStockInQty.Text);    //Take Stock Out Quantity from User

            Item newItem = new Item();
            newItem = (Item) ViewState["existingItem"];                       //Retrieve Selected Item ID at DropDownList
            newItem.StockInQuantity = stockInQuantity;                        //Pass Stock In Quantity

            string msg = itemManager.AddQuantity(newItem);                    //Add Given Stock In Quantity to Items Table
            lblStockInStatus.Text = msg;

            newItem = itemManager.GetItemInfoByID(newItem);                   //Get Updated Item Info

            textBoxAvailableQty.Text = Convert.ToString(newItem.AvailableQuantity);  //Send Updated Available Quantity to TextBox
            textBoxStockInQty.Text = Convert.ToString(0);

        }
    }
}