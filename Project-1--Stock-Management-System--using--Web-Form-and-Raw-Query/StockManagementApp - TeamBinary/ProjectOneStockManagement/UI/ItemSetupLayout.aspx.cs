using System;
using System.Web.UI.WebControls;
using ProjectOneStockManagement.BLL;
using ProjectOneStockManagement.Models;


namespace ProjectOneStockManagement.UI
{
    public partial class ItemSetupLayout : System.Web.UI.Page
    {
        CategoryManager categoryManager = new CategoryManager();
        CompanyManager companyManager = new CompanyManager();
        Item aItem = new Item();
        ItemManager itemManager = new ItemManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)  //If page load for first time then execute this block
            {
                BindDdlCategory();  //Get All Category List inside DropDownList
                BindDdlCompany();   //Get All Company List inside DropDownList

                GetDdlZeroIndex();   //Send a string "Select Company" and "Select Category" to first position

                textBoxReorderLevel.Text = Convert.ToString(0);  //Set Initial Reorder Value to Zero
            }
        }

        private void BindDdlCategory()
        {
            ddlItemCategory.DataSource = categoryManager.GetCategoryList();  //Get All Category Name From DB to DDL
            ddlItemCategory.DataBind();
        }

        private void BindDdlCompany()
        {
            ddlItemCompany.DataSource = companyManager.GetCompanyList();  //Get All Company Name From DB to DDL
            ddlItemCompany.DataBind();
        }

        protected void btnSaveItem_Click(object sender, EventArgs e)
        {
            int categoryID = Convert.ToInt32(ddlItemCategory.SelectedValue);   //Get CategoryID from Selected value from DDL
            int companyID = Convert.ToInt32(ddlItemCompany.SelectedValue);     //Get CompanyID from Selected value from DDL
            string itemName = textBoxItemName.Text;                            //Take Item Name from User
            int reorderLevel = Convert.ToInt32(textBoxReorderLevel.Text);      //Take Reorder Level from User


            aItem.Category = categoryID;
            aItem.Company = companyID;
            aItem.Name = itemName;
            aItem.ReorderLevel = reorderLevel;
            aItem.AvailableQuantity = 0;
            
            string msg = itemManager.SaveItem(aItem); //Save CategoryID, CompanyID, ItemName, ReorderLevel, AvailableQuantity, Date, Time
            lblItemSaveStatus.Text = msg;

            textBoxItemName.Text = "";
            textBoxReorderLevel.Text = Convert.ToString(0);  //Set again Reorder Value to Zero after saving an Item
        }

        private void GetDdlZeroIndex()
        {
            ListItem liCategory = new ListItem("Select Category", "-1"); 
            ddlItemCategory.Items.Insert(0, liCategory);                  //Send "Select Category" to first position of DropDownList
             
            ListItem liCompany = new ListItem("Select Company", "-1");
            ddlItemCompany.Items.Insert(0, liCompany);                    //Send "Select Company" to first position of DropDownList
        }
    }
}